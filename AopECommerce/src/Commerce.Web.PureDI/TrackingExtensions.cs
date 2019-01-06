using System;
using Microsoft.AspNetCore.Mvc;

namespace Ploeh.Samples.Commerce.Web.PureDI
{
    public static class TrackingExtensions
    {
        // Ensures the provided disposable is disposed when the web request ends.
        public static TDisposable TrackDisposable<TDisposable>(
            this TDisposable disposable, ControllerContext context)
            where TDisposable : IDisposable
        {
            context.HttpContext.Response.RegisterForDispose(disposable);
            return disposable;
        }
    }
}