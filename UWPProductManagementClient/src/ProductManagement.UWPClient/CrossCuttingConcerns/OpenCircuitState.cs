using System;

namespace Ploeh.Samples.ProductManagement.UWPClient.CrossCuttingConcerns
{
    public class OpenCircuitState : ICircuitState
    {
        private readonly TimeSpan timeout;
        private readonly DateTime closedAt;

        public OpenCircuitState(TimeSpan timeout)
        {
            this.timeout = timeout;
            this.closedAt = DateTime.UtcNow;
        }

        public ICircuitState NextState() =>
            DateTime.UtcNow - this.closedAt >= this.timeout
                ? new HalfOpenCircuitState(this.timeout)
                : (ICircuitState)this;

        public void Guard() => throw new InvalidOperationException("The circuit is currently open.");
        public void Succeed() { }
        public void Trip(Exception e) { }
    }
}