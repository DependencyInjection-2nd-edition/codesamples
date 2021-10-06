using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ploeh.Samples.Commerce.Web.PureDI
{
    // ---- Start code snippet section 7.3.2, page 233 ----
    public class LoggingMiddleware
    {
        private readonly ILogger logger;

        public LoggingMiddleware(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context, Func<Task> next)
        {
            logger.LogInformation("Request started");
            await next();
            logger.LogInformation("Request ended");
        }
    }
    // ---- End code snippet section 7.3.2, page 233 ----
}