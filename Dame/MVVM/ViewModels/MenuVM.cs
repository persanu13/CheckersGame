using Dame.Core;
using Dame.Services;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dame.MVVM.ViewModels
{
    class MenuVM : Core.ViewModel
    {
        public MenuVM(INavigationService navigationService) : base(navigationService) {}
    }
}
