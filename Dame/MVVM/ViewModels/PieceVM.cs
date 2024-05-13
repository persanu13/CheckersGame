using Dame.Core;
using Dame.MVVM.Models;
using Dame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dame.MVVM.ViewModels
{
    class PieceVM
    {
        public Piece Piece { get; set; }

        public RelayCommand PressPieceCommand {  get; set; }

        public PieceVM(Piece piece, RelayCommand RemovePiece)
        {
            Piece = piece;
            PressPieceCommand = RemovePiece;
        }

    }
}
