using Dame.Core;
using Dame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dame.MVVM.ViewModels
{
    class HelpMenuVM : Core.ViewModel
    {
        public HelpMenuVM(INavigationService navigationService) : base(navigationService) {}

        public RelayCommand NavigateToAboutCommand => new RelayCommand(execute => { NavigationService.NavigateMenuTo<AboutVM>(); });
        public RelayCommand NavigateToRulesCommand => new RelayCommand(execute => { NavigationService.NavigateMenuTo<RulesVM>(); });
        public RelayCommand NavigateToStatisticsCommand => new RelayCommand(execute =>
        {
            NavigationService.NavigateMenuTo<StatisticsVM>();
            (NavigationService.CurrentMenuView as StatisticsVM).GetScore();
        });
        public RelayCommand BackToMainMenuCommand => new RelayCommand(execute => { NavigationService.NavigateMenuTo<MainMenuVM>(); });

    }
}
