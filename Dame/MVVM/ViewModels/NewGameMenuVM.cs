using Dame.Core;
using Dame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dame.MVVM.ViewModels
{
    class NewGameMenuVM : Core.ViewModel
    {
        public NewGameMenuVM(INavigationService navigationService) : base(navigationService) { }

        public RelayCommand BackToMainMenuCommand => new RelayCommand(execute => { NavigationService.NavigateMenuTo<MainMenuVM>(); });

        private bool forceJump = false;
        public bool ForceJumpYes
        { 
            get => forceJump;
            set { forceJump = value; OnPropertyChanged(); }
        }
        public bool ForceJumpNo
        {
            get => !forceJump;
            set { forceJump = !value; OnPropertyChanged(); }
        }

        public RelayCommand NewGameCommand => new RelayCommand(NewGame);
        public void NewGame(object parameter)
        {
            NavigationService.NavigateWindowTo<GameVM>();
            NavigationService.NavigateMenuTo<MainMenuVM>();
            (NavigationService.CurrentWindowView as GameVM).NewGame(forceJump);
        }
    }
}
