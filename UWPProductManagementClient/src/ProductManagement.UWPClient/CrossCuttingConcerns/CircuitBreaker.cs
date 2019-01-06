using System;

namespace Ploeh.Samples.ProductManagement.UWPClient.CrossCuttingConcerns
{
    // This implementation is not thread-safe, but really ought to be in a production scenario.
    public class CircuitBreaker : ICircuitBreaker
    {
        public CircuitBreaker(TimeSpan timeout)
        {
            this.State = new ClosedCircuitState(timeout);
        }

        public ICircuitState State { get; private set; }

        public void Guard()
        {
            this.State = this.State.NextState();
            this.State.Guard();
            this.State = this.State.NextState();
        }

        public void Trip(Exception exception)
        {
            this.State = this.State.NextState();
            this.State.Trip(exception);
            this.State = this.State.NextState();
        }

        public void Succeed()
        {
            this.State = this.State.NextState();
            this.State.Succeed();
            this.State = this.State.NextState();
        }
    }
}