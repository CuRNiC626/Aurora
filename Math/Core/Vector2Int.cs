using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Math.Core
{
    public struct Vector2Int
    {
        public int X;
        public int Y;

        #region Константы

        public static readonly Vector2Int Zero = new Vector2Int(0, 0);
        public static readonly Vector2Int One = new Vector2Int(1, 1);

        public static readonly Vector2Int Up = new Vector2Int(0, 1);
        public static readonly Vector2Int Down = new Vector2Int(0, -1);
        public static readonly Vector2Int Left = new Vector2Int(-1, 0);
        public static readonly Vector2Int Right = new Vector2Int(1, 0);

        #endregion


        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        #region Операторы арифметики

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new Vector2Int(a.X + b.X, a.Y + b.Y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new Vector2Int(a.X - b.X, a.Y - b.Y);
        public static Vector2Int operator *(Vector2Int a, int scalar) => new Vector2Int(a.X * scalar, a.Y * scalar);
        public static Vector2Int operator *(int scalar, Vector2Int a) => a * scalar;
        public static Vector2Int operator /(Vector2Int a, int scalar) => new Vector2Int(a.X / scalar, a.Y / scalar);

        #endregion

        #region Сравнение

        public bool Equals(Vector2Int other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj) => obj is Vector2Int other && Equals(other);
        public override int GetHashCode() => (X * 397) ^ Y;

        public static bool operator ==(Vector2Int a, Vector2Int b) => a.Equals(b);

        public static bool operator !=(Vector2Int a, Vector2Int b) => !a.Equals(b);

        #endregion

        #region Утилиты

        public override string ToString() => $"({X}, {Y})";

        #endregion
    }
}
