using CommonLib.Models.Exeptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CommonLib.Attributes
{
    public class HaveSSHAttribute : AuthorizeAttribute
    {
        public static readonly string ExpectedSSH = "ljAdC9nTEjcNVScwJT+NqI43OgXAKtbzamflkpzN+XpUzpEcTbicNwg2b9EbABbDPld+LXuH153wqYMU+UTk";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _actualSSH = context.HttpContext.Request.Headers["PrivateSSH"];
            if (ExpectedSSH != _actualSSH)
            {
                throw new OnlyPrivateUsageException();
            }
        }
    }
}
