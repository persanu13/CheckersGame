using Dame.Core;
using Dame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dame.MVVM.ViewModels
{
    class RulesVM : Core.ViewModel
    {
        public RulesVM(INavigationService navigationService) : base(navigationService) { }

        public RelayCommand BackToHelpMenuCommand => new RelayCommand(execute => { NavigationService.NavigateMenuTo<HelpMenuVM>(); });

    }

}
