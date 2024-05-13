using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dame.MVVM.Models
{
    public class Player
    {
        [XmlAttribute]
        public int NumberPieces { get; set; }

        [XmlAttribute]
        public PieceColor PlayerColor { get; set; }

        public Player() { }
        public Player(int numberPieces, PieceColor playerColor)
        {
            NumberPieces = numberPieces;
            PlayerColor = playerColor;
        }
    }
}
