using System;
using Ploeh.Samples.ProductManagement.DataAccess;
using Ploeh.Samples.ProductManagement.Domain;
using Ploeh.Samples.ProductManagement.PresentationLogic;
using Ploeh.Samples.ProductManagement.PresentationLogic.ViewModels;
using Ploeh.Samples.ProductManagement.UWPClient.CrossCuttingConcerns;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Ploeh.Samples.ProductManagement.UWPClient
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application, INavigationService
    {
        private readonly INavigationService navigationService;
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;

            this.navigationService = this;
            this.productRepository = 
                new CircuitBreakerProductRepositoryDecorator(
                    new CircuitBreaker(TimeSpan.FromMinutes(1)),
                    new FakeProductRepository());
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user. Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (Window.Current.Content == null)
            {
                Window.Current.Content = new Frame();
                Window.Current.Activate();

                this.NavigateTo<MainViewModel>();
            }
        }

        public void NavigateTo<TViewModel>(Action whenDone = null, object model = null) 
            where TViewModel : IViewModel
        {
            Page page = this.CreatePage(typeof(TViewModel));
            var viewModel = (IViewModel)page.DataContext;

            viewModel.Initialize(whenDone, model);

            var frame = (Frame)Window.Current.Content;
            frame.Content = page;
        }

        private Page CreatePage(Type viewModelType)
        {
            if (viewModelType == typeof(MainViewModel))
            {
                return new MainPage(
                    new MainViewModel(
                        this.navigationService,
                        this.productRepository));
            }
            else if (viewModelType == typeof(EditProductViewModel))
            {
                return new EditProductPage(
                    new EditProductViewModel(
                        this.productRepository));
            }
            else if (viewModelType == typeof(NewProductViewModel))
            {
                return new NewProductPage(
                    new NewProductViewModel(
                        this.productRepository));
            }
            else
            {
                throw new InvalidOperationException($"Unknown view model: {viewModelType}.");
            }
        }

        /// <summary>Invoked when Navigation to a certain page fails</summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}