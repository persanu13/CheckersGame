using Dame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dame.Services
{
    public interface INavigationService
    {
        ViewModel CurrentWindowView {  get; }
        ViewModel CurrentMenuView { get; }
        void NavigateWindowTo<T>() where T : ViewModel;
        void NavigateMenuTo<T>() where T : ViewModel;
    }

    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory;

        private ViewModel _currentWindowView;
        public ViewModel CurrentWindowView
        { 
            get { return _currentWindowView; }
            private set
            {
                _currentWindowView = value;
                OnPropertyChanged();
            }
        }
        private ViewModel _currentMenuView;
        public ViewModel CurrentMenuView
        {
            get { return _currentMenuView; }
            private set
            {
                _currentMenuView = value;
                OnPropertyChanged();
            }

        }

        public NavigationService(Func<Type,ViewModel> viewModelFactory)
        { 
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateWindowTo<TViewModel>() where TViewModel : ViewModel
        {
            var viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentWindowView = viewModel;
        }
        public void NavigateMenuTo<TViewModel>() where TViewModel : ViewModel
        {
            var viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentMenuView = viewModel;
        }
    }
}
