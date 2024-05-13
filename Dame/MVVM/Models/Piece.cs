using Dame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Dame.MVVM.Models
{
    [Serializable]
    public class MyPoint
    {
        [XmlAttribute]
        public int X { get; set; }
        [XmlAttribute]
        public int Y { get; set; }

        public MyPoint() { }
        public MyPoint(int x, int y) 
        {
            X = x;
            Y = y;
        }
    }
    [Serializable]
    public enum PieceColor
    {
        Blue,
        Red,
        None
    }
    [Serializable]
    public class Piece : ObservableObject
    {
        [XmlIgnore]
        private MyPoint coordinates;
        
        [XmlElement]
        public MyPoint Coordinates 
        { 
            get { return coordinates; }
            set
            {
                coordinates = value;
                OnPropertyChanged();
            }
        }
        [XmlIgnore]
        private bool isKing;
       
        [XmlAttribute]
        public bool IsKing
        { 
            get { return isKing; }
            set
            { 
                isKing = value;
                if (Color == PieceColor.Blue)
                {
                    SourceImage = "/assets/king_blue.png";
                }
                else
                {
                    SourceImage = "/assets/king_red.png";
                }
            }
        }

        [XmlAttribute]
        public PieceColor Color { get; set;}
        [XmlIgnore]
        private string sourceImage;
        [XmlAttribute]
        public string SourceImage {
            get { return sourceImage; }
            set {  sourceImage = value; OnPropertyChanged(); }
        }

        public Piece() { }
        public Piece(MyPoint coordinates, PieceColor color)
        {
            Coordinates = coordinates;
            Color = color;
            isKing = false;
            if(color == PieceColor.Blue)
            {
                SourceImage = "/assets/men_blue.png";
            }
            else
            {
                SourceImage = "/assets/men_red.png";
            }
        }   
    }
}
