using System;
using Ploeh.Samples.ProductManagement.PresentationLogic.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace Ploeh.Samples.ProductManagement.UWPClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditProductPage : Page
    {
        public EditProductPage(EditProductViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException("vm");

            this.InitializeComponent();

            this.DataContext = vm;
        }
    }
}