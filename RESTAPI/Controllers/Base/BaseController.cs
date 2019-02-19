using Microsoft.AspNetCore.Mvc;
using RESTAPI.Model.Exceptions;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace RESTAPI.Controllers.Base
{
    public static class ControllerBaseExtensions
    {
        public static Claim GetClaimIfExist(this ControllerBase ct, string key)
        {
            var claimItem = ct.User.Claims.FirstOrDefault(x => x.Type == key);
            if (claimItem == null)
            {
                throw new TokenMissmatchException();
            }

            return claimItem;
        }

        public static T GetClaimValue<T>(this ControllerBase ct, string key)
        {
            try
            {
                return (T)Convert.ChangeType(GetClaimIfExist(ct, key), typeof(T));
            }
            catch (Exception ex)
            {
                //TODO: ex to log
                throw new TokenMissmatchException();
            }
        }

        public static string GetRawBody(this ControllerBase ct)
        {
            using (var streamReader = new StreamReader(ct.Request.Body))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
