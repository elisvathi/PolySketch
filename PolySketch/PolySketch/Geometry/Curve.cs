using Poly.Geometry;
using PolySketch.Geometry.Interfaces;
using System;

namespace PolySketch.Geometry
{
    public class Curve : IGeometry, IRotatable, IMovable, IScalable, IOffsetable, IDividable, IExtendLimiter
    {
        public PVector[] ControlPoints => throw new NotImplementedException();

        public void AddStartingPoint(PVector point)
        {
            throw new NotImplementedException();
        }

        public void AddUpdatePoint(PVector point)
        {
            throw new NotImplementedException();
        }

        public IGeometry CopyGeometry()
        {
            throw new NotImplementedException();
        }

        public PVector[] Divide(int segs)
        {
            throw new NotImplementedException();
        }

        public PVector[] GetDataToDraw(int precision)
        {
            throw new NotImplementedException();
        }

        public void Move(PVector vect)
        {
            throw new NotImplementedException();
        }

        public IGeometry Offset(double value)
        {
            throw new NotImplementedException();
        }

        public void RotateVector(double angle)
        {
            throw new NotImplementedException();
        }

        public void Rotate(PVector center, double angle)
        {
            throw new NotImplementedException();
        }

        public void Scale(double value)
        {
            throw new NotImplementedException();
        }

        public void Scale(PVector center, double value)
        {
            throw new NotImplementedException();
        }

        public void UpdateWithData(PVector[] data)
        {
            throw new NotImplementedException();
        }
    }
}