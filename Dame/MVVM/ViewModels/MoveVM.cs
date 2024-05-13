using Dame.Core;
using Dame.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dame.MVVM.ViewModels
{
    public class MoveVM
    {
        public MyPoint MoveCoordinates {  get; set; }
        public string Color {  get; set; }
        public List<Piece> EatenPiece { get; set; }
        public RelayCommand PressMoveCommand { get; set; }
        public MoveVM(MyPoint moveCoordinates,string Color,List<Piece> eatenPiece = null)
        {
            this.MoveCoordinates = moveCoordinates;
            this.Color = Color;
            this.EatenPiece = eatenPiece;
        }



    }
}
