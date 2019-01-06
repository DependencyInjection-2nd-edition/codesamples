using System;

namespace Ploeh.Samples.ProductManagement.UWPClient.CrossCuttingConcerns
{
    public class ClosedCircuitState : ICircuitState
    {
        private readonly TimeSpan timeout;
        private bool tripped;

        public ClosedCircuitState(TimeSpan timeout)
        {
            this.timeout = timeout;
        }
        
        public ICircuitState NextState() =>
            this.tripped
                ? new OpenCircuitState(this.timeout)
                : (ICircuitState)this;

        public void Guard() { }
        public void Succeed() { }
        public void Trip(Exception e) => this.tripped = true;
    }
}