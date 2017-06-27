using Poly.Geometry;
using PolySketch.Geometry.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolySketch.Geometry
{
    public class Polygon : IDividable, IGeometry, IMovable, IRotatable, IOffsetable, IScalable, IMultiSegment, ICompoundGeometry
    {
        private List<PVector> _Vertices;
        private bool _Closed;
        public List<PVector> Vertices { get => _Vertices; set => _Vertices = value; }
        public int NumVertices { get { return Vertices.Count; } }
        public PVector Start { get { return Vertices[0]; } }
        public PVector End { get { return _Closed ? Vertices[0] : Vertices[NumVertices - 1]; } }
        public PVector Centroid { get { return GetCentroid(); } }

        public PVector[] ControlPoints => Vertices.ToArray();

        private PVector GetCentroid()
        {
            //TODO: IMplement Centroid Algorithm
            return new PVector();
        }

        public Polygon( PVector[] data , bool closed )
        {
            Vertices = data.ToList<PVector>();
            _Closed = closed;
        }

        public Polygon( PVector[] Vertices ) : this(Vertices , false)
        {
        }

        public Polygon() : this(new PVector[] { } , false)
        {
        }

        public void AddVertex( PVector vertex , int index )
        {
            Vertices.Insert(index , vertex);
        }

        public void AddVertex( PVector vertex )
        {
            Vertices.Add(vertex);
        }

        public IGeometry CopyGeometry()
        {
            return new Polygon(Vertices.ToArray() , _Closed);
        }

        public PVector[] Divide( int segs )
        {
            //TODO: Implement Divide of Polygon
            throw new NotImplementedException();
        }

        public PVector[] GetDataToDraw( int precision )
        {
            return ControlPoints;
        }

        public void Move( PVector vect )
        {
            foreach ( var v in Vertices )
            {
                v.Move(vect);
            }
        }

        public IGeometry Offset( double value )
        {
            //TODO: Implement Offset of polygon
            throw new NotImplementedException();
        }

        public void RotateVector( double angle )
        {
            Rotate(Centroid , angle);
        }

        public void Rotate( PVector center , double angle )
        {
            foreach ( var v in Vertices )
            {
                v.Rotate(center , angle);
            }
        }

        public void Scale( double value )
        {
            Scale(Centroid , value);
        }

        public void Scale( PVector center , double value )
        {
            foreach ( var v in Vertices )
            {
                v.Scale(center , value);
            }
        }

        public List<IGeometry> GetSegments()
        {
            var retVal = new List<IGeometry>();
            for ( int i = 0 ; i < NumVertices - 1 ; i++ )
            {
                retVal.Add(new Line(Vertices[i] , Vertices[i + 1]));
            }
            if ( _Closed )
            { retVal.Add(new Line(Vertices[NumVertices - 1] , Vertices[0])); }
            return retVal;
        }

        public double TotalLength()
        {
            double retVal = 0;
            foreach ( var v in GetSegments() )
            {
                var a = v as Line;
                retVal += a.Length();
            }
            return retVal;
        }

        public void AddStartingPoint( PVector point )
        {
            AddVertex(point);
        }

        public void AddUpdatePoint( PVector point )
        {
            AddVertex(point);
        }

        public void AddGeometry( IGeometry geometry )
        {
            AddVertex(geometry as PVector);
        }

        public void UpdateWithData( PVector[] data )
        {
            if ( data.Length > 2 )
            {
                List<Line> tempLines = new List<Line>();
                Line temp = new Line(data[0] , data[1]);
                tempLines.Add(temp);
                for ( int i = 1 ; i < data.Length - 1 ; i++ )
                {
                    Line l = new Line(data[i] , data[i + 1]);
                    if ( !Line.AreParallel(l , temp) )
                    {
                        tempLines.Add(l);
                        temp = l;
                    }
                }
                List<PVector> tempPoints = new List<PVector>();
                tempPoints.Add(data[0]);
                for ( int i = 0 ; i < tempLines.Count - 1 ; i++ )
                {
                    tempPoints.Add(tempLines[i].Intersection(tempLines[i + 1]));
                }
                if ( !_Closed )
                { tempPoints.Add(data[data.Length - 1]); }
                Vertices = tempPoints.ToList();
            }
        }
    }
}