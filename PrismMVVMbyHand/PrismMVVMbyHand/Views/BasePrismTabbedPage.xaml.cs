using Prism.Common;
using Prism.Navigation;
using Xamarin.Forms;

namespace PrismMVVMbyHand.Views
{
    public partial class BasePrismTabbedPage : TabbedPage, INavigatingAware
    {
        public BasePrismTabbedPage()
        {
            InitializeComponent();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            foreach (var child in Children)
            {
                (child as INavigatingAware)?.OnNavigatingTo(parameters);
                (child?.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
            }
        }
    }
}
