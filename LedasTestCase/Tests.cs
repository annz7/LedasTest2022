using System;
using NUnit.Framework;

namespace LedasTestCase
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ParallelTest()
        {
            var a = new Vector3D(-4, -5, 6);
            var b = new Vector3D(-6, -1, 12);
            var c = new Vector3D(0, 1, -3);
            var d = new Vector3D(1, -1, -6);
            Assert.True(Segment3D.Intersect(new Segment3D(a, b), new Segment3D(c, d)) == null);
            Assert.True(Segment3D.Intersect(new Segment3D(c, d), new Segment3D(a, b)) == null);
        }
        
        [Test]
        public void PointsTest()
        {
            var a = new Vector3D(-1, -2, 3);
            var b = new Vector3D(-6, -1, 12);
            Assert.True(Segment3D.Intersect(new Segment3D(a, a), new Segment3D(a, a)).Equals(a));
            Assert.True(Segment3D.Intersect(new Segment3D(a, a), new Segment3D(b, b)) == null);
        }
        
        [Test]
        public void OnOneLineTest()
        {
            var a = new Vector3D(0, 0, 0);
            var b = new Vector3D(1, 2, 1);
            var c = new Vector3D(-1, -2, -1);
            var d = new Vector3D(2, 4, 2);
            Assert.True(Segment3D.Intersect(new Segment3D(a, a), new Segment3D(a, b)).Equals(a));
            Assert.True(Segment3D.Intersect(new Segment3D(a, b), new Segment3D(b, b)).Equals(b));
            Assert.True(Segment3D.Intersect(new Segment3D(a, b), new Segment3D(b, c)).Equals(b));
            Assert.True(Segment3D.Intersect(new Segment3D(a, c), new Segment3D(b, c)).Equals(c));
            Assert.True(Segment3D.Intersect(new Segment3D(a, c), new Segment3D(b, d)) == null);
            Assert.True(Segment3D.Intersect(new Segment3D(b, d), new Segment3D(a, c)) == null);
        }
        
        
        [Test]
        public void SkewLinesTest1()
        {
            var a = new Vector3D(2, -1, 0);
            var b = new Vector3D(4, -4, -1);
            var c = new Vector3D(-1, 0, 1);
            var d = new Vector3D(0, -2, 1);
            Assert.True(Segment3D.Intersect(new Segment3D(a, b), new Segment3D(c, d)) == null);
            Assert.True(Segment3D.Intersect(new Segment3D(c, d), new Segment3D(a, b)) == null);
        }
        
        [Test]
        public void SkewLinesTest2()
        {
            var a = new Vector3D(0, 0, 0);
            var b = new Vector3D(1, 1, 0);
            var c = new Vector3D(1, 2, 1);
            var d = new Vector3D(0, 6, 1);
            Assert.True(Segment3D.Intersect(new Segment3D(a, b), new Segment3D(c, d)) == null);
            Assert.True(Segment3D.Intersect(new Segment3D(c, d), new Segment3D(a, b)) == null);
        }
        
        [Test]
        public void IntersectLinesTest1()
        {
            var a = new Vector3D(-1, -1, -1);
            var b = new Vector3D(0, 0, 0);
            var c = new Vector3D(2, 2, 2);
            var d = new Vector3D(0, 2, 1);
            var e = new Vector3D(0, -2, -1);
            
            Assert.True(Segment3D.Intersect(new Segment3D(a, c), new Segment3D(e, d)).Equals(b));
            Assert.True(Segment3D.Intersect(new Segment3D(b, c), new Segment3D(e, d)).Equals(b));
            Assert.True(Segment3D.Intersect(new Segment3D(b, a), new Segment3D(e, d)).Equals(b));
            Assert.True(Segment3D.Intersect(new Segment3D(e, d), new Segment3D(a, c)).Equals(b));
            
            Assert.True(Segment3D.Intersect(new Segment3D(e, d), new Segment3D(a, d)).Equals(d));
            Assert.True(Segment3D.Intersect(new Segment3D(e, d), new Segment3D(d, a)).Equals(d));
        }
        
        [Test]
        public void IntersectLinesTest2()
        {
            var a = new Vector3D(3, -3, 2);
            var b = new Vector3D(2, -2, 4);
            var a2 = a - (b - a) * 5;
            
            var c = new Vector3D(-1, 4, -26);
            var d = new Vector3D(2, 0, -20);
            var c2 = c - (d - c) * 50;
            var d2 = d + (d - c) * 50;
            
            var e = new Vector3D(8, -8, -8);
            
             Assert.True(Segment3D.Intersect(new Segment3D(a, b), new Segment3D(c, d)) == null);
             Assert.True(Segment3D.Intersect(new Segment3D(c, d), new Segment3D(a, b)) == null);
             Assert.True(Segment3D.Intersect(new Segment3D(b, a), new Segment3D(c, d)) == null);
             Assert.True(Segment3D.Intersect(new Segment3D(d, c), new Segment3D(a, b))  == null);
            
            Assert.True(Segment3D.Intersect(new Segment3D(a2, b), new Segment3D(c2, d2)).Equals(e));
            Assert.True(Segment3D.Intersect(new Segment3D(c2, d2), new Segment3D(a2, b)).Equals(e));
            Assert.True(Segment3D.Intersect(new Segment3D(b, a2), new Segment3D(c2, d2)).Equals(e));
            Assert.True(Segment3D.Intersect(new Segment3D(d2, c2), new Segment3D(a2, b)).Equals(e));
        }
        
        [Test]
        public void NotCoplanarTest1()
        {
            var a = new Vector3D(0, 0, 1);
            var b = new Vector3D(0, 1, 0);
            var c = new Vector3D(1, 0, 0);
            Assert.True(!Vector3D.IsСoplanar(a, b, c));
        }
        
        [Test]
        public void NotCoplanarTest2()
        {
            var a = new Vector3D(1, 2, 3);
            var b = new Vector3D(1, 1, 1);
            var c = new Vector3D(1, 2, 1);
            Assert.True(!Vector3D.IsСoplanar(a, b, c));
        }
        
        [Test]
        public void CoplanarTest1()
        {
            var a = new Vector3D(1, 1, 1);
            var b = new Vector3D(3, 1, 1);
            var c = new Vector3D(2, 2, 2);
            Assert.True(Vector3D.IsСoplanar(a, b, c));
        }
        
        [Test]
        public void CoplanarTest2()
        {
            var a = new Vector3D(0, 0, 0);
            var b = new Vector3D(0, 1, 1);
            var c = new Vector3D(1, 0, 1);
            Assert.True(Vector3D.IsСoplanar(a, b, c));
        }
        
        [Test]
        public void ColinearTest()
        {
            var a = new Vector3D(0, 0, 1);
            var b = new Vector3D(0, 1, 2);
            var c = new Vector3D(3, 1, 2);
            Assert.True(Vector3D.IsСolinear(a, 2 * a));
            Assert.True(Vector3D.IsСolinear(2 * a, a));
            Assert.True(Vector3D.IsСolinear(a, a));
            Assert.True(Vector3D.IsСolinear(b, 2 * b));
            Assert.True(Vector3D.IsСolinear(2 * b, b));
            Assert.True(Vector3D.IsСolinear(c, 2 * c));
            Assert.True(Vector3D.IsСolinear(2 * c, c));
        }
        
        [Test]
        public void NotColinearTest()
        {
            var a = new Vector3D(0, 0, 1);
            var b = new Vector3D(0, 1, 2);
            var c = new Vector3D(3, 1, 2);
            Assert.True(!Vector3D.IsСolinear(a, b));
            Assert.True(!Vector3D.IsСolinear(a, c));
            Assert.True(!Vector3D.IsСolinear(b, a));
            Assert.True(!Vector3D.IsСolinear(b, c));
            Assert.True(!Vector3D.IsСolinear(c, a));
            Assert.True(!Vector3D.IsСolinear(c, b));
        }
        
        [Test]
        public void PointBelongSegmentTest1()
        {
            var a = new Vector3D(0, 0, 0);
            var b = new Vector3D(0, 1, 1);
            var c = new Vector3D(0, 0.5, 0.5);
            var s = new Segment3D(a, b);
            
            Assert.True(s.PointBelong(a));
            Assert.True(s.PointBelong(b));
            Assert.True(s.PointBelong(c));
        }
        
        [Test]
        public void PointNotBelongSegmentTest1()
        {
            var a = new Vector3D(0, 0, 0);
            var b = new Vector3D(0, 1, 1);
            var s = new Segment3D(a, b);
            
            Assert.True(!s.PointBelong(new Vector3D(1, 0.5, 0.5)));
            Assert.True(!s.PointBelong(new Vector3D(0, 1.5, 1.5)));
            Assert.True(!s.PointBelong(new Vector3D(0, 1, 0.5)));
        }
    }
}