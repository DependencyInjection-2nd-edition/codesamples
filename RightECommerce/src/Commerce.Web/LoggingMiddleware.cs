using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ploeh.Samples.Commerce.Web
{
    public class LoggingMiddleware
    {
        private readonly ILogger logger;

        public LoggingMiddleware(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context, Func<Task> next)
        {
            this.logger.LogInformation("Request started");
            await next();
            this.logger.LogInformation("Request ended");
        }
    }
}