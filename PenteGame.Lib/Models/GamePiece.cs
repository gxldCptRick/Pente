using PenteGame.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteGame.Lib.Models
{
    public class GamePiece: IEquatable<GamePiece> //Class to represent the game piece
    {
        //point and color of the game piece
        public Point Point { get; private set; }
        public PieceColor Color { get; private set; }

        //ctor
        public GamePiece(Point point, PieceColor color)
        {
            this.Color = color;
            this.Point = point;
        }

        //Another lovely ToString
        public override string ToString()
        {
            return $"{this.Color} {this.Point}";
        }


        public override int GetHashCode()
        {
            return Point.GetHashCode() ^ Color.GetHashCode();
        }

        //A beautiful override
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj == this) return true;
            if (obj is GamePiece otherPiece) return Equals(otherPiece);
            else return false;
        }

        public bool Equals(GamePiece other)
        {
            if (other is null) return false;
            return other.Point.Equals(this.Point) && other.Color == Color;
        }
    }
}
