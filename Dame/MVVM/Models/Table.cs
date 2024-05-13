using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Dame.MVVM.Models
{
    [Serializable]
    public class Table
    {
        [XmlAttribute]
        public int Size {  get; set; }
        [XmlArray]
        public List<Piece> Pieces { get; set; }
        [XmlArray]
        public List<List<PieceColor>> TableSituation { get; set; }

        public Table() { }
        public Table(int size)
        {
            Size = size;
            Pieces = new List<Piece>();
            TableSituation = new List<List<PieceColor>>();
            for (int i = 0; i < Size; i++)
            {
                TableSituation.Add(new List<PieceColor>());
                for (int j = 0; j < Size; j++)
                {
                    PieceColor color = PieceColor.None;
                    if ((i + j) % 2 == 1 && i != 3 && i != 4)
                    {
                        if (i < 3) { color = PieceColor.Blue; }
                        if (i > 4) { color = PieceColor.Red; }
                        Pieces.Add(new Piece(new MyPoint(j, i), color));
                    }
                    TableSituation[i].Add(color);
                }
            }
        }
        public bool IsInTable(MyPoint coordinates)
        {
            return coordinates.X >= 0 && coordinates.X < Size && coordinates.Y >= 0 && coordinates.Y < Size;
        }

    }
}
