using Dame.Core;
using Dame.MVVM.Models;
using Dame.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace Dame.MVVM.ViewModels
{
    class GameVM : Core.ViewModel
    {
        private GameLogic gameLogic;
        private PieceVM pressedPiece { get; set; }
        public ObservableCollection<PieceVM> PiecesUi { get; set; }
        public ObservableCollection<MoveVM> MovesUi { get; set; }

        private string winnerMesage;
        public string WinnerMesage
        {
            get { return winnerMesage; }
            set { winnerMesage = value; OnPropertyChanged(); }
        }

        private string saveName;
        public string SaveName
        {
            get { return saveName; }
            set { saveName = value; OnPropertyChanged(); }
        }

        private string swichButtonSource;
        public string SwichButtonSource 
        {
            get { return swichButtonSource; }
            set { swichButtonSource = value; OnPropertyChanged(); }
        }

        private Visibility endPanelVisibility = Visibility.Collapsed;
        public Visibility EndPanelVisibility
        {
            get { return endPanelVisibility; }
            set { endPanelVisibility = value; OnPropertyChanged(); }
        }

        private Visibility secondMenuVisibility = Visibility.Collapsed;
        public Visibility SecondMenuVisibility
        {
            get { return secondMenuVisibility; }
            set { secondMenuVisibility = value; OnPropertyChanged(); }
        }

        public GameVM(INavigationService navigationService) : base(navigationService)
        {
            MovesUi = new ObservableCollection<MoveVM>();
            PiecesUi = new ObservableCollection<PieceVM>();
        }
        public RelayCommand ExitButtonCommand => new RelayCommand(execute => { SecondMenuVisibility = Visibility.Collapsed; SaveName = null; });
        public void EscPress()
        {
            if(SecondMenuVisibility == Visibility.Visible)
            {
                SecondMenuVisibility = Visibility.Collapsed;
                SaveName = null;
            }
            else
            {
                SecondMenuVisibility = Visibility.Visible;
            }
        }

        public RelayCommand BackToMenuCommand => new RelayCommand(execute => { NavigationService.NavigateWindowTo<MenuVM>();SaveName = null; });

        public RelayCommand RestartGameCommand => new RelayCommand(execute => NewGame(gameLogic.ForceJump));
        public void NewGame(bool forceJump)
        {
            gameLogic = new GameLogic(forceJump);
            SaveName = null;
            pressedPiece = null;
            PiecesUi.Clear();
            MovesUi.Clear();
            for (int i = 0; i < gameLogic.Table.Pieces.Count; i++)
            {
                PiecesUi.Add(new PieceVM(gameLogic.Table.Pieces[i], PressPieceCommand));
            }
            SwichButtonSource = "/assets/swich_button_red.png";
            EndPanelVisibility = Visibility.Collapsed;
            SecondMenuVisibility = Visibility.Collapsed;
        }

        public void LoadGame(string fileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(GameLogic));
            FileStream file = new FileStream("../../gamesSaved/"+ fileName + ".xml", FileMode.Open);
            gameLogic = (xmlser.Deserialize(file) as GameLogic);
            file.Dispose();
            pressedPiece = null;
            PiecesUi.Clear();
            MovesUi.Clear();
            for (int i = 0; i < gameLogic.Table.Pieces.Count; i++)
            {
                PiecesUi.Add(new PieceVM(gameLogic.Table.Pieces[i], PressPieceCommand));
            }
            if(gameLogic.CurrentPlayer.PlayerColor == PieceColor.Red)
            {
                SwichButtonSource = "/assets/swich_button_red.png";
            }
            else
            {
                SwichButtonSource = "/assets/swich_button_blue.png";
            }
            EndPanelVisibility = Visibility.Collapsed;
            SecondMenuVisibility = Visibility.Collapsed;
        }
        public RelayCommand SaveGameCommand => new RelayCommand(SaveGame);
        public void SaveGame(object parameter)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(GameLogic));
            FileStream file = new FileStream("../../gamesSaved/" +SaveName+ ".xml", FileMode.Create);
            xmlser.Serialize(file, gameLogic);
            file.Dispose();
            SaveName = null;
            MessageBox.Show("Jocul a fost salvat!");
        }
        public RelayCommand PressPieceCommand => new RelayCommand(PressPiece);
        private void PressPiece(object parameter)
        {
            pressedPiece = (parameter as PieceVM);
            if (pressedPiece == null) { return; }
            MovesUi.Clear();
            foreach (MoveVM move in gameLogic.GetMoves(pressedPiece.Piece))
            {
                MovesUi.Add(move);
                move.PressMoveCommand = PressMoveCommand;
            }
        }

        public RelayCommand PressMoveCommand => new RelayCommand(PressMove);
        private void PressMove(object parameter)
        {
            MoveVM move = (parameter as MoveVM);
            if (move == null) { return; }
            MovesUi.Clear();
            gameLogic.doMove(pressedPiece.Piece, move);
            
            if(move.EatenPiece != null) {
                foreach (Piece eatPiece in move.EatenPiece)
                {
                    PiecesUi.Remove(GetPiecesVmAtPiece(eatPiece));
                }
            }
            pressedPiece = null;

            if (gameLogic.IsEndGame())
            {
                WinnerMesage = "Player " + gameLogic.GetWinerColor() + " win!";
                EndPanelVisibility = Visibility.Visible;
                UpdateScore(gameLogic.GetWinerColor());
            }
            else
            {
                SwichButtonSource = "/assets/swich_button_" +
                    gameLogic.CurrentPlayer.PlayerColor.ToString().ToLower()+".png";
            }
        }

        public void UpdateScore(string winerColor)
        {
            int redScore = 0;
            int blueScore = 0;
            int numberMaxim = 0;
            try
            {
                using (StreamReader reader = new StreamReader("score.txt"))
                {
                    redScore = int.Parse(reader.ReadLine());
                    blueScore = int.Parse(reader.ReadLine());
                    numberMaxim = int.Parse(reader.ReadLine());
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Eroare la citirea fișierului: " + e.Message);
            }

            if(winerColor == "red")
            {
                redScore++;
            }
            else
            {
                blueScore++;
            }

            if(numberMaxim < PiecesUi.Count())
            {
                numberMaxim = PiecesUi.Count();
            }
            using (StreamWriter writer = new StreamWriter("score.txt"))
            {
                writer.WriteLine(redScore);
                writer.WriteLine(blueScore);
                writer.Write(numberMaxim);
            }
        }

        public PieceVM GetPiecesVmAtPiece(Piece piece)
        {
            foreach (PieceVM pieceVm in PiecesUi)
            {
                if (pieceVm.Piece == piece) return pieceVm;
            }
            return null;
        }
    }
}
