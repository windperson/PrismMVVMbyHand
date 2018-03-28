using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LibModel;
using Prism.Events;
using Prism.Navigation;

namespace PrismMVVMbyHand.ViewModels
{
	public class HistoryListPageViewModel : ChildTabViewModelBase
    {

        private readonly INavigationService _navigationService;
        private readonly WordHistoryModel _model;

        public HistoryListPageViewModel(IEventAggregator ea, INavigationService navigationService, WordHistoryModel model) : base(ea)
        {
            _navigationService = navigationService;
            _model = model;

            ListHistoryCommand = new DelegateCommand(GetWordHistory);
            ClearHistoryCommand = new DelegateCommand(DoClearHistory);

            GetWordHistory();
        }


        #region ViewModel Data-Bounded Properties

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private ObservableCollection<HistoryItem> _historyItems = new ObservableCollection<HistoryItem>();
        public ObservableCollection<HistoryItem> HistoryItems
        {
            get => _historyItems;
            set => SetProperty(ref _historyItems, value);
        }

        private HistoryItem _selectedHistoryItem;

        public HistoryItem SelectedHistoryItem
        {
            get => _selectedHistoryItem;
            set
            {
                SetProperty(ref _selectedHistoryItem, value);

                if (_selectedHistoryItem != null)
                {
                    
                    _navigationService.NavigateAsync(
                        new Uri($"http://abc/BasePrismTabbedPage?selectedTab=InputPage&selected={_selectedHistoryItem.Word}", UriKind.Absolute));
                }

                _selectedHistoryItem = null;
            }
        }

        #endregion


        #region ViewModel Commands

        public DelegateCommand ListHistoryCommand { get; set; }
        private void GetWordHistory()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            HistoryItems.Clear();
            var wordHistorylist = _model.GetAll();
            foreach (var historyItem in wordHistorylist)
            {
                HistoryItems.Add(historyItem);
            }

            IsBusy = false;
        }

        public DelegateCommand ClearHistoryCommand { get; set; }
        private void DoClearHistory()
        {
            HistoryItems = new ObservableCollection<HistoryItem>();
            _model.ClearAll();
        }

        #endregion

    }
}
