using System;
using System.Collections.Generic;
using System.Text;
using Prism;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;

namespace PrismMVVMbyHand.ViewModels
{
    public class InitializeTabbedChildrenEvent : PubSubEvent<NavigationParameters>
    {
        //
    }

    public class ChildTabViewModelBase : BindableBase, IActiveAware, INavigationAware, IDestructible
    {
        IEventAggregator _ea { get; }

        public ChildTabViewModelBase(IEventAggregator eventAggregator)
        {
            _ea = eventAggregator;

            _ea.GetEvent<InitializeTabbedChildrenEvent>().Subscribe(OnInitializationEventFired);

            IsActiveChanged += (sender, e) => System.Diagnostics.Debug.WriteLine($"{Title} IsActive: {IsActive}");
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public event EventHandler IsActiveChanged;
        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }


        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                SetProperty(ref _isActive, value,
                    () => System.Diagnostics.Debug.WriteLine($"{Title} IsActive Changed: {value}"));
            }
        }

        void OnInitializationEventFired(NavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine($"{Title} Detected an initialization event");
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine($"{Title} is executing OnNavigatingTo");
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            //
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            //
        }

        public void Destroy()
        {
            System.Diagnostics.Debug.WriteLine($"{Title} is being Destroyed!");
            _ea.GetEvent<InitializeTabbedChildrenEvent>().Unsubscribe(OnInitializationEventFired);
        }




    }
}
