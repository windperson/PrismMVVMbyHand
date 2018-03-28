﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace PrismMVVMbyHand.ViewModels
{
    public class BasePrismTabbedPageViewModel : ViewModelBase
    {
        public BasePrismTabbedPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
