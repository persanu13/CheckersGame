using Dame.Core;
using Dame.Services;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Dame.MVVM.ViewModels
{
    class MainMenuVM : Core.ViewModel
    {
        public MainMenuVM(INavigationService navigationService) : base(navigationService) {}

        public RelayCommand NavigateToNewGameMenuCommand => new RelayCommand(execute => { NavigationService.NavigateMenuTo<NewGameMenuVM>(); });
        public RelayCommand NavigateToLoadGameMenuCommand => new RelayCommand(execute =>
        {
            NavigationService.NavigateMenuTo<LoadGameVM>();
            (NavigationService.CurrentMenuView as LoadGameVM).ReadGames();
        });
        public RelayCommand NavigateToHelpMenuCommand => new RelayCommand(execute => { NavigationService.NavigateMenuTo<HelpMenuVM>(); });
        public RelayCommand ExitAppCommand => new RelayCommand(execute => { Application.Current.Shutdown(); });
    }
}
