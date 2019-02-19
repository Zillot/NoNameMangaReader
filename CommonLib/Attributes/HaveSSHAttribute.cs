using CommonLib.Models.Exeptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CommonLib.Attributes
{
    public class HaveSSHFilter : IActionFilter
    {
        public static readonly string ExpectedSSH = "ljAdC9nTEjcNVScwJT+NqI43OgXAKtbzamflkpzN+XpUzpEcTbicNwg2b9EbABbDPld+LXuH153wqYMU+UTk";

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var _actualSSH = context.HttpContext.Request.Headers["PrivateSSH"];
            if (ExpectedSSH != _actualSSH)
            {
                throw new OnlyPrivateUsageException();
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
