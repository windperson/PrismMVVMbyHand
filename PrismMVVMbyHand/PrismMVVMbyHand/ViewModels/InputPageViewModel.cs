using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using LibModel;
using Prism.Events;
using Prism.Navigation;

namespace PrismMVVMbyHand.ViewModels
{
    public class InputPageViewModel : ChildTabViewModelBase
    {
        protected bool HasInitialized { get; set; }

        private WordConverter _converter;
        private readonly WordHistoryModel _model;

        public InputPageViewModel(IEventAggregator ea, INavigationService navigationService, WordHistoryModel model)
            : base(ea)
        {
            _model = model;

            ConverterTypes = WordConverter.Types;

            ConverterChangeCommand = new DelegateCommand(DoChangeConverter);
            ConvertCommand = new DelegateCommand<string>(DoConvert);
            AddToHistoryCommand = new DelegateCommand(AddWordToHistory);
        }

        #region ViewModel Data-Bounded Properties

        public string[] ConverterTypes { get; }

        private int _selectedConverterType = -1;
        public int SelectedConvterType
        {
            get => _selectedConverterType;
            set => SetProperty(ref _selectedConverterType, value);
        }

        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }

        private string _displayText;
        public string DisplayText
        {
            get => _displayText;
            set => SetProperty(ref _displayText, value);
        }

        #endregion


        #region ViewModel Commands

        public DelegateCommand ConverterChangeCommand { get; set; }
        private void DoChangeConverter()
        {
            var selectedType = SelectedConvterType;
            if (selectedType != -1 && selectedType < ConverterTypes.Length)
            {
                _converter = new WordConverter(ConverterTypes[selectedType]);
            }
        }

        public DelegateCommand<string> ConvertCommand { get; set; }
        private void DoConvert(string input)
        {
            if (string.IsNullOrEmpty(input) || _converter == null)
            {
                //do nothing
                return;
            }

            DisplayText = _converter.Convert(input);
        }

        public DelegateCommand AddToHistoryCommand { get; set; }
        private void AddWordToHistory()
        {
            if (string.IsNullOrEmpty(DisplayText)) { return; }

            var historyItem = new HistoryItem
            {
                RecordAt = DateTime.Now,
                Word = DisplayText
            };

            _model.AddOne(historyItem);
        }

        #endregion

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters["selected"] is string oldText)
            {
                InputText = oldText;
            }
        }
    }
}
