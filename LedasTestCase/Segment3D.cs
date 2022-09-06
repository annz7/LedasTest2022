using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using NUnit.Framework;

namespace LedasTestCase
{
    public class Segment3D
    {
        private Vector3D start;
        private Vector3D end;
        private Vector3D direction;

        private const double Epsilon = 0.000001;

        public Segment3D(Vector3D start, Vector3D end)
        {
            this.start = start;
            this.end = end;
            direction = end - start;
        }

        public static Vector3D Intersect(Segment3D first, Segment3D second)
        {
            var intersection = IntersectLines(first, second);

            if (intersection != null && first.PointBelong(intersection) && second.PointBelong(intersection))
            {
                return intersection;
            }

            return null;
        }

        private static Vector3D IntersectLines(Segment3D first, Segment3D second)
        {
            if (!Vector3D.IsСoplanar(first.direction, second.direction, first.start - second.start)) return null;

            if (Vector3D.IsСolinear(first.direction, second.direction))
            {
                if (first.direction.IsNull() && second.PointBelong(first.start)) return first.start;
                if (second.direction.IsNull() && first.PointBelong(second.start)) return second.start;
                
                // in this case the intersection is a segment, therefore return some point from segment  
                if (first.PointBelong(second.start)) return second.start;
                if (first.PointBelong(second.end)) return second.end;
                if (second.PointBelong(first.start)) return first.start;
                if (second.PointBelong(first.end)) return first.end;

                return null;
            }

            return FindLinesIntersection(first, second);
        }

        private static Vector3D FindLinesIntersection(Segment3D a, Segment3D b)
        {
            double t;
            var matrix = new Vector3D[3];
            matrix[0] = new Vector3D(a.direction.X, -b.direction.X, b.start.X - a.start.X);
            matrix[1] = new Vector3D(a.direction.Y, -b.direction.Y, b.start.Y - a.start.Y);
            matrix[2] = new Vector3D(a.direction.Z, -b.direction.Z, b.start.Z - a.start.Z);

            for (var i = 0; i < 3; i++)
            {
                if (matrix[i].X == 0 && matrix[i].Y != 0)
                {
                    t = matrix[i].Z / matrix[i].Y;
                    return new Vector3D(
                        b.direction.X * t + b.start.X,
                        b.direction.Y * t + b.start.Y,
                        b.direction.Z * t + b.start.Z
                    );
                }
                
                if (matrix[i].X != 0 && matrix[i].Y == 0)
                {
                    var k = matrix[i].Z / matrix[i].X;
                    return new Vector3D(
                        a.direction.X * k + a.start.X,
                        a.direction.Y * k + a.start.Y,
                        a.direction.Z * k + a.start.Z
                    );
                }
            }

            t = (matrix[1].X * matrix[0].Z - matrix[0].X * matrix[1].Z) /
                (matrix[1].X * matrix[0].Y - matrix[0].X * matrix[1].Y);
            return new Vector3D(
                b.direction.X * t + b.start.X,
                b.direction.Y * t + b.start.Y,
                b.direction.Z * t + b.start.Z
            );
        }

        public bool PointBelong(Vector3D point)
        {
            return PointBelongLine(point)
                   && point.X <= Math.Max(start.X, end.X)
                   && point.X >= Math.Min(start.X, end.X)
                   && point.Y <= Math.Max(start.Y, end.Y)
                   && point.Y >= Math.Min(start.Y, end.Y)
                   && point.Z <= Math.Max(start.Z, end.Z)
                   && point.Z >= Math.Min(start.Z, end.Z);
        }

        private bool PointBelongLine(Vector3D point)
        {
            double k;
            if (Math.Abs(direction.X) < Epsilon)
            {
                if (Math.Abs(point.X - start.X) > Epsilon) return false;

                if (Math.Abs(direction.Y) < Epsilon)
                {
                    return !(Math.Abs(point.Y - start.Y) > Epsilon);
                }

                k = (point.Y - start.Y) / direction.Y;
                return Math.Abs(point.Z - (direction.Z * k + start.Z)) < Epsilon;
            }

            k = (point.X - start.X) / direction.X;

            return Math.Abs(point.Y - (direction.Y * k + start.Y)) < Epsilon
                   && Math.Abs(point.Z - (direction.Z * k + start.Z)) < Epsilon;
        }
    }
}