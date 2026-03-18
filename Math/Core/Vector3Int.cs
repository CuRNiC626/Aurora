using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Math.Core
{
    using System;

    public struct Vector3Int : IEquatable<Vector3Int>
    {
        public int X;
        public int Y;
        public int Z;

        #region Конструкторы

        public Vector3Int(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Константы

        public static readonly Vector3Int Zero = new Vector3Int(0, 0, 0);
        public static readonly Vector3Int One = new Vector3Int(1, 1, 1);

        public static readonly Vector3Int UnitX = new Vector3Int(1, 0, 0);
        public static readonly Vector3Int UnitY = new Vector3Int(0, 1, 0);
        public static readonly Vector3Int UnitZ = new Vector3Int(0, 0, 1);

        #endregion

        #region Операторы

        public static Vector3Int operator +(Vector3Int a, Vector3Int b)
            => new Vector3Int(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Vector3Int operator -(Vector3Int a, Vector3Int b)
            => new Vector3Int(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static Vector3Int operator *(Vector3Int a, int scalar)
            => new Vector3Int(a.X * scalar, a.Y * scalar, a.Z * scalar);

        public static Vector3Int operator *(int scalar, Vector3Int a)
            => a * scalar;

        public static Vector3Int operator /(Vector3Int a, int scalar)
            => new Vector3Int(a.X / scalar, a.Y / scalar, a.Z / scalar);

        public static bool operator ==(Vector3Int a, Vector3Int b)
            => a.Equals(b);

        public static bool operator !=(Vector3Int a, Vector3Int b)
            => !a.Equals(b);

        #endregion

        #region Методы

        public int LengthSquared()
            => X * X + Y * Y + Z * Z;

        public double Length()
            => Math.Sqrt(LengthSquared());

        public static int Dot(Vector3Int a, Vector3Int b)
            => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        #endregion

        #region Equality

        public bool Equals(Vector3Int other)
            => X == other.X && Y == other.Y && Z == other.Z;

        public override bool Equals(object obj)
            => obj is Vector3Int other && Equals(other);

        public override int GetHashCode()
            => X ^ (Y << 2) ^ (Z >> 2);

        #endregion

        #region ToString

        public override string ToString()
            => $"({X}, {Y}, {Z})";

        #endregion
    }
}
