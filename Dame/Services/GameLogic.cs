using Dame.MVVM.Models;
using Dame.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Dame.Services
{
    [Serializable]
    public class GameLogic
    {
        [XmlElement]
        public Player playerRed;
        [XmlElement]
        public Player playerBlue;
        [XmlElement]
        public Player CurrentPlayer { get; set; }
        [XmlElement]
        public Table Table { get; set; }
        [XmlAttribute]
        public bool ForceJump;
        
        public GameLogic() {}
        public GameLogic(bool forceJump)
        {
            ForceJump = forceJump;
            playerRed = new Player(12,PieceColor.Red);
            playerBlue = new Player(12, PieceColor.Blue);
            CurrentPlayer = playerRed;
            Table = new Table(8);
        }

        private void SwichPlayer()
        {
            if(CurrentPlayer.PlayerColor == PieceColor.Red)
            {
                CurrentPlayer = playerBlue;
            }
            else
            {
                CurrentPlayer = playerRed;
            }
        }

        private Piece GetPieceAtPoint(MyPoint point)
        {
            foreach(Piece piece in Table.Pieces)
            {
                if (piece.Coordinates.X == point.X && piece.Coordinates.Y == point.Y) return piece;
            }
            return null;
        }
        public List<MoveVM> GetMoves(Piece selectedPiece)
        {
            List<MoveVM> moves = new List<MoveVM>();
            if (CurrentPlayer.PlayerColor != selectedPiece.Color) return moves;

            int[] directionX = { 1, -1, 1, -1 };
            int[] directionY = { 1, 1, -1, -1 };

            int semnY = 0;
            int maxMove = 0;
            if (selectedPiece.IsKing) { maxMove = 4; } else { maxMove = 2; }
            if(selectedPiece.Color == PieceColor.Red) { semnY = -1; } else {  semnY = 1; }


            //basic move
            for(int i = 0; i < maxMove; i++)
            {
                MyPoint newPosition = new MyPoint(selectedPiece.Coordinates.X + directionX[i], selectedPiece.Coordinates.Y + directionY[i] * semnY);
                if (Table.IsInTable(newPosition) && Table.TableSituation[newPosition.Y][newPosition.X] == PieceColor.None)
                {
                    moves.Add(new MoveVM(newPosition, "#A9DFAE"));
                }
            }
            
            //eating move
            List<MyPoint> points = new List<MyPoint>();
            List<Piece> eatenPieces = new List<Piece>();
            bool[,] eatedMatrice = new bool[8, 8];
            points.Add(selectedPiece.Coordinates);
            
            while (points.Count > 0)
            {
                MyPoint currentPoint = points[0];
                for (int i = 0; i < maxMove; i++)
                {
                    MyPoint enemyPiecePosition = new MyPoint(currentPoint.X + directionX[i], currentPoint.Y + directionY[i] * semnY);
                    MyPoint newPosition = new MyPoint(currentPoint.X + directionX[i] * 2, currentPoint.Y + directionY[i] * semnY * 2);

                    if (!Table.IsInTable(newPosition) || !Table.IsInTable(enemyPiecePosition)) continue;
                    if (Table.TableSituation[enemyPiecePosition.Y][enemyPiecePosition.X] == PieceColor.None) continue;
                    if (Table.TableSituation[enemyPiecePosition.Y][enemyPiecePosition.X] == selectedPiece.Color) continue;
                    if (Table.TableSituation[newPosition.Y][newPosition.X] != PieceColor.None) continue;
                    if (eatedMatrice[enemyPiecePosition.Y,enemyPiecePosition.X]) continue;

                    eatedMatrice[enemyPiecePosition.Y,enemyPiecePosition.X] = true;
                    eatenPieces.Add(GetPieceAtPoint(enemyPiecePosition));
                    moves.Add(new MoveVM(newPosition, "#D77272", new List<Piece>(eatenPieces)));

                    if (ForceJump)
                    {
                        points.Insert(0, newPosition);
                        break;
                    }
                    else
                    {
                        eatenPieces.RemoveAt(eatenPieces.Count - 1);
                    }
                }
                if(currentPoint == points[0])
                {
                    if (eatenPieces.Count > 0)
                    {
                        eatenPieces.RemoveAt(eatenPieces.Count - 1);
                    }
                    points.RemoveAt(0);
                }

            }

            return moves;
        }

        public void doMove(Piece piece, MoveVM move)
        {
            Table.TableSituation[piece.Coordinates.Y][piece.Coordinates.X] = PieceColor.None;
            Table.TableSituation[move.MoveCoordinates.Y][move.MoveCoordinates.X] = piece.Color;
            piece.Coordinates = move.MoveCoordinates;
            if(piece.Color == PieceColor.Red && piece.Coordinates.Y == 0) {
                piece.IsKing = true;
            }
            if (piece.Color == PieceColor.Blue && piece.Coordinates.Y == Table.Size - 1)
            {
                piece.IsKing = true;
            }

            if (move.EatenPiece != null)
            {
                foreach (Piece eatPiece in move.EatenPiece)
                {
                    Table.TableSituation[eatPiece.Coordinates.Y][eatPiece.Coordinates.X] = PieceColor.None;
                    Table.Pieces.Remove(eatPiece);
                    if (playerBlue.PlayerColor == eatPiece.Color) playerBlue.NumberPieces--;
                    if (playerRed.PlayerColor == eatPiece.Color) playerRed.NumberPieces--;
                }
            }
            SwichPlayer();
        }
        public bool IsEndGame()
        {
            return (playerBlue.NumberPieces == 0) || (playerRed.NumberPieces == 0);
        }
        
        public string GetWinerColor()
        {
            if(playerBlue.NumberPieces == 0) { return "red"; }
            if (playerRed.NumberPieces == 0) { return "blue"; }

            return "Nothing";
        }


    }
}
