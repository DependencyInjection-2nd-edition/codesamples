using System;

namespace Ploeh.Samples.ProductManagement.UWPClient.CrossCuttingConcerns
{
    public static class CircuitBreakerExtensions
    {
        public static void Execute(this ICircuitBreaker breaker, Action action)
        {
            breaker.Execute<object>(() =>
            {
                action();
                return null;
            });
        }

        public static T Execute<T>(this ICircuitBreaker breaker, Func<T> action)
        {
            breaker.Guard();

            try
            {
                T result = action();
                breaker.Succeed();

                return result;
            }
            catch (Exception ex)
            {
                breaker.Trip(ex);
                throw;
            }
        }
    }
}