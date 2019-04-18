using CachingFramework.Redis;
using CachingFramework.Redis.Serializers;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebParser.DL.Repositories.Base
{
    public abstract class RedisBaseRepository : IRedisBaseRepository
    {
        protected static RedisContext _redisContext { get; set; }

        protected abstract string repositoryKey { get; }
        protected IConfiguration _configuration { get; set; }

        private string workingAreaName { get; set; }

        public RedisBaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            workingAreaName = "main";

            if (_redisContext == null)
            {
                _redisContext = new RedisContext(
                    configuration["RedisConnectionLine"],
                    new JsonSerializer());
            }
        }

        public void ClearAllData()
        {
            var keysToRemove = _redisContext.Cache.GetKeysByTag(new[] { workingAreaName });
            if (keysToRemove != null)
            {
                foreach (var keyPair in keysToRemove)
                {
                    var keys = keyPair.Split(":$_->_$:");
                    _redisContext.Cache.RemoveTagsFromHashField(keys[0], keys[1], new[] { workingAreaName });
                    _redisContext.Cache.RemoveHashed(keys[0], keys[1]);
                }

                _redisContext.Cache.Remove(keysToRemove.ToArray());
            }
        }

        public void SetIntegrationTestMode()
        {
            workingAreaName = "IntegrationTests";
        }

        public void SetWorkingArea(string newWorkingAreaName)
        {
            workingAreaName = newWorkingAreaName;
        }

        #region single object: Cache
        protected void save(string key, object value, int secondsAlive)
        {
            _redisContext.Cache.SetObject(
                $"{workingAreaName}:{key}",
                value,
                new[] { workingAreaName },
                TimeSpan.FromSeconds(secondsAlive));
        }

        protected T get<T>(string key)
        {
            return _redisContext.Cache.GetObject<T>(
                $"{workingAreaName}:{key}");
        }
        #endregion single object: Cache

        #region list of object: Hashed
        protected List<string> findAllHashesByPattern(string pattern)
        {
            var regex = new Regex($"{workingAreaName}:{pattern}.+:hash");

            //all keys will be in format <workingAreaName>:<ACTUAL KEY>:hash:$_->_$:<FIELD ID>
            var allKeys = _redisContext.Cache.GetKeysByTag(new[] { workingAreaName });

            return allKeys
                .Where(x => regex.IsMatch(x))
                .Select(x => x
                    .Replace($":hash", "")
                    .Replace($"{workingAreaName}:{pattern}", "")
                    .Split(":$_->_$:")[0])
                .Distinct()
                .ToList();
        }

        protected void saveHashed(string groupName, string key, object value, int secondsAlive)
        {
            _redisContext.Cache.SetHashed(
                $"{workingAreaName}:{groupName}",
                key,
                value,
                new[] { workingAreaName },
                TimeSpan.FromSeconds(secondsAlive));
        }

        protected IDictionary<string, T> getHashed<T>(string groupName)
        {
            return _redisContext.Cache.GetHashedAll<T>(
                $"{workingAreaName}:{groupName}");
        }

        protected T getHashed<T>(string groupName, string key)
        {
            return _redisContext.Cache.GetHashed<T>(
                $"{workingAreaName}:{groupName}",
                key);
        }
        #endregion list of object: Hashed
    }
}
