using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Ploeh.Samples.ProductManagement.PresentationLogic.UICommands
{
    public static class ItemClickCommand
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(ItemClickCommand),
            new PropertyMetadata(null, ItemClickCommand.OnCommandPropertyChanged));

        public static void SetCommand(DependencyObject dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(ItemClickCommand.CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject dependencyObject)
        {
            return (ICommand)dependencyObject.GetValue(ItemClickCommand.CommandProperty);
        }

        private static void OnCommandPropertyChanged(
            DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var control = dependencyObject as ListViewBase;
            if (control != null)
            {
                control.ItemClick += ItemClickCommand.OnItemClick;
            }
        }

        private static void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var control = sender as ListViewBase;
            var command = ItemClickCommand.GetCommand(control);

            if (command != null && command.CanExecute(e.ClickedItem))
            {
                command.Execute(e.ClickedItem);
            }
        }
    }
}
