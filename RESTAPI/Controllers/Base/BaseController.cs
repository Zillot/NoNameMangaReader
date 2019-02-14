using Microsoft.AspNetCore.Mvc;
using RESTAPI.Model.Exceptions;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace RESTAPI.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        public Claim GetClaimIfExist(string key)
        {
            var claimItem = User.Claims.FirstOrDefault(x => x.Type == key);
            if (claimItem == null)
            {
                throw new TokenMissmatchException();
            }

            return claimItem;
        }

        public T GetClaimValue<T>(string key)
        {
            try
            {
                return (T)Convert.ChangeType(GetClaimIfExist(key), typeof(T));
            }
            catch (Exception ex)
            {
                //TODO: ex to log
                throw new TokenMissmatchException();
            }
        }

        protected string getRawBody()
        {
            using (var streamReader = new StreamReader(Request.Body))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
