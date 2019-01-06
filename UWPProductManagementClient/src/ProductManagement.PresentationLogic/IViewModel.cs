using System;

namespace Ploeh.Samples.ProductManagement.PresentationLogic
{
    public interface IViewModel
    {
        void Initialize(Action whenDone, object model);
    }
}