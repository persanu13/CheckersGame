using Dame.Core;
using Dame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dame.MVVM.ViewModels
{
    class MainWindowVM : Core.ViewModel
    {
        public MainWindowVM(INavigationService navigationService) : base(navigationService)
        {
            NavigationService.NavigateWindowTo<MenuVM>();
            NavigationService.NavigateMenuTo<MainMenuVM>();
        }
        public RelayCommand EscPressCommand => new RelayCommand(EscPress);

        private void EscPress(object sender)
        {
            GameVM gameVm = (NavigationService.CurrentWindowView as GameVM);
            if(gameVm != null)
            {
                gameVm.EscPress();
            }
        }
    }
}
