using System;

namespace LedasTestCase
{
    public class Vector3D
    {
        public double X;
        public double Y;
        public double Z;

        private const double Epsilon = 0.000001;

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Vector3D vector)
                return
                    Math.Abs(vector.X - X) < Epsilon
                    && Math.Abs(vector.Y - Y) < Epsilon
                    && Math.Abs(vector.Z - Z) < Epsilon;
            return false;
        }

        public static Vector3D operator +(Vector3D a, Vector3D b)
            => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Vector3D operator -(Vector3D a, Vector3D b)
            => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static Vector3D operator *(double a, Vector3D b)
            => new(a * b.X, a * b.Y, a * b.Z);

        public static Vector3D operator *(Vector3D b, double a)
            => new(a * b.X, a * b.Y, a * b.Z);

        public bool IsNull() => (X == 0 && Y == 0 && Z == 0);

        public static bool IsСoplanar(Vector3D a, Vector3D b, Vector3D c)
        {
            var det = a.X * (b.Y * c.Z - b.Z * c.Y)
                      - a.Y * (b.X * c.Z - b.Z * c.X)
                      + a.Z * (b.X * c.Y - b.Y * c.X);
            return det == 0;
        }

        public static bool IsСolinear(Vector3D a, Vector3D b)
        {
            if (a.IsNull() || b.IsNull()) return true;

            if (b.X != 0) (a, b) = (b, a);

            double k;
            if (a.X != 0)
            {
                k = b.X / a.X;
                return Math.Abs(b.Y - a.Y * k) < Epsilon && Math.Abs(b.Z - a.Z * k) < Epsilon;
            }

            if (b.Y != 0) (a, b) = (b, a);

            if (a.Y == 0) return true;

            k = b.Y / a.Y;
            return Math.Abs(b.Z - a.Z * k) < Epsilon;
        }
    }
}