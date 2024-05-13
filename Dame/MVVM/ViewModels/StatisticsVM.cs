using Dame.Core;
using Dame.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dame.MVVM.ViewModels
{
    class StatisticsVM : Core.ViewModel
    {
        private string redScore;
        public string RedScore
        {
            get { return redScore; }
            set { redScore = value; OnPropertyChanged(); }
        }
        private string blueScore;
        public string BlueScore
        {
            get { return blueScore; }
            set { blueScore = value; OnPropertyChanged(); }
        }
        private string maxPieces;
        public string MaxPieces
        {
            get { return maxPieces; }
            set { maxPieces = value; OnPropertyChanged(); }
        }

        public StatisticsVM(INavigationService navigationService) : base(navigationService) { }
        public RelayCommand BackToHelpMenuCommand => new RelayCommand(execute => { NavigationService.NavigateMenuTo<HelpMenuVM>(); });

        public void GetScore()
        {
            try
            {
                using (StreamReader reader = new StreamReader("score.txt"))
                {
                    RedScore = reader.ReadLine();
                    BlueScore = reader.ReadLine();
                    MaxPieces = reader.ReadLine();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Eroare la citirea fișierului: " + e.Message);
            }
        }

    }
}
