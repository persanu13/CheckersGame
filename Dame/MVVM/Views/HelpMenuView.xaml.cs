using Dame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dame.MVVM.Views
{
    /// <summary>
    /// Interaction logic for HelpMenu.xaml
    /// </summary>
    public partial class HelpMenuView : UserControl
    {
        public HelpMenuView()
        {
            InitializeComponent();
        }
        private void ButtonHover(object sender, MouseEventArgs e)
        {
            Image image = (sender as Button).Content as Image;
            image.Source = UiServices.HoverImage(image.Source.ToString());

        }
        private void ButtonDefault(object sender, MouseEventArgs e)
        {
            Image image = (sender as Button).Content as Image;
            image.Source = UiServices.DefaultImage(image.Source.ToString());
        }
    }
}
