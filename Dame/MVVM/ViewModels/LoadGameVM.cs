using Dame.Core;
using Dame.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dame.MVVM.ViewModels
{
    class LoadGameVM : Core.ViewModel
    {
        public ObservableCollection<string> GamesSave { get; set; }

        public LoadGameVM(INavigationService navigationService) : base(navigationService) {
            GamesSave = new ObservableCollection<string>();
        }
        public RelayCommand BackToMainMenuCommand => new RelayCommand(execute => { NavigationService.NavigateMenuTo<MainMenuVM>(); });

        public RelayCommand LoadGameCommand => new RelayCommand(LoadGame);

        public void LoadGame(object sender)
        {
            NavigationService.NavigateWindowTo<GameVM>();
            NavigationService.NavigateMenuTo<MainMenuVM>();
            (NavigationService.CurrentWindowView as GameVM).LoadGame(sender as string);
        }


        public void ReadGames()
        {
            GamesSave.Clear();
            string currentDirectory = Directory.GetCurrentDirectory();
            string directoryPath = @"..\..\gamesSaved";
            directoryPath = Path.Combine(currentDirectory, directoryPath);
            try
            {
                // Obține toate numele de fișiere din directorul specificat
                string[] fileNames = Directory.GetFiles(directoryPath);
                foreach (string fileName in fileNames)
                {
                   GamesSave.Add(Path.GetFileNameWithoutExtension(fileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o excepție: " + ex.Message);
            }
        }


    }
}
