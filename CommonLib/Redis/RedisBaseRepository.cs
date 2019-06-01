using CachingFramework.Redis;
using CachingFramework.Redis.Serializers;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CommonLib.Redis
{
    public abstract class RedisBaseRepository : IRedisBaseRepository
    {
        protected static RedisContext _redisContext { get; set; }

        protected abstract string _repositoryKey { get; }
        protected IConfiguration _configuration { get; set; }

        private string _workingAreaName { get; set; }

        public RedisBaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _workingAreaName = "main";

            if (_redisContext != null)
            {
                _redisContext = new RedisContext(
                    configuration["RedisConnectionLine"],
                    new JsonSerializer());
            }
        }

        public void ClearAllData()
        {
            var keysToRemove = _redisContext.Cache.GetKeysByTag(new[] { _workingAreaName });
            if (keysToRemove != null)
            {
                foreach (var keyPair in keysToRemove)
                {
                    var keys = keyPair.Split(":$_->_$:");
                    _redisContext.Cache.RemoveTagsFromHashField(keys[0], keys[1], new[] { _workingAreaName });
                    _redisContext.Cache.RemoveHashed(keys[0], keys[1]);
                }

                _redisContext.Cache.Remove(keysToRemove.ToArray());
            }
        }

        public void SetIntegrationTestMode()
        {
            _workingAreaName = "IntegrationTests";
        }

        public void SetWorkingArea(string newWorkingAreaName)
        {
            _workingAreaName = newWorkingAreaName;
        }

        #region single object: Cache
        protected void save(string key, object value, int secondsAlive)
        {
            _redisContext.Cache.SetObject(
                $"{_workingAreaName}:{key}",
                value,
                new[] { _workingAreaName },
                TimeSpan.FromSeconds(secondsAlive));
        }

        protected T get<T>(string key)
        {
            return _redisContext.Cache.GetObject<T>(
                $"{_workingAreaName}:{key}");
        }
        #endregion single object: Cache

        #region list of object: Hashed
        protected List<string> findAllHashesByPattern(string pattern)
        {
            var regex = new Regex($"{_workingAreaName}:{pattern}.+:hash");

            //all keys will be in format <workingAreaName>:<ACTUAL KEY>:hash:$_->_$:<FIELD ID>
            var allKeys = _redisContext.Cache.GetKeysByTag(new[] { _workingAreaName });

            return allKeys
                .Where(x => regex.IsMatch(x))
                .Select(x => x
                    .Replace($":hash", "")
                    .Replace($"{_workingAreaName}:{pattern}", "")
                    .Split(":$_->_$:")[0])
                .Distinct()
                .ToList();
        }

        protected void saveHashed(string groupName, string key, object value, int secondsAlive)
        {
            _redisContext.Cache.SetHashed(
                $"{_workingAreaName}:{groupName}",
                key,
                value,
                new[] { _workingAreaName },
                TimeSpan.FromSeconds(secondsAlive));
        }

        protected IDictionary<string, T> getHashed<T>(string groupName)
        {
            return _redisContext.Cache.GetHashedAll<T>(
                $"{_workingAreaName}:{groupName}");
        }

        protected T getHashed<T>(string groupName, string key)
        {
            return _redisContext.Cache.GetHashed<T>(
                $"{_workingAreaName}:{groupName}",
                key);
        }
        #endregion list of object: Hashed
    }
}
