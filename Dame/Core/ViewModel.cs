using Dame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dame.Core
{
    public abstract class ViewModel : ObservableObject
    {
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get { return _navigationService; }
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }
        public ViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }



    }
}
