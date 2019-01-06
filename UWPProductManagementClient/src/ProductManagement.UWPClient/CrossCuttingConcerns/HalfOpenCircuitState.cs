using System;

namespace Ploeh.Samples.ProductManagement.UWPClient.CrossCuttingConcerns
{
    public class HalfOpenCircuitState : ICircuitState
    {
        private readonly TimeSpan timeout;
        private bool tripped;
        private bool succeeded;

        public HalfOpenCircuitState(TimeSpan timeout)
        {
            this.timeout = timeout;
        }

        public ICircuitState NextState() =>
            this.succeeded
                ? new ClosedCircuitState(this.timeout)
                : this.tripped
                    ? new OpenCircuitState(this.timeout)
                    : (ICircuitState)this;

        public void Guard() { }
        public void Succeed() => this.succeeded = true;
        public void Trip(Exception e) => this.tripped = true;
    }
}