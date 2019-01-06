using System;

namespace Ploeh.Samples.ProductManagement.PresentationLogic
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>(Action whenDone = null, object model = null) where TViewModel : IViewModel;
    }
}