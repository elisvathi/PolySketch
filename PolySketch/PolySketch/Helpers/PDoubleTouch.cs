using Poly.Geometry;

namespace PolySketch.Helpers
{
    public class PDoubleTouch
    {
        private PTouch firstTouch;
        private PTouch secondTouch;

        public PDoubleTouch( PTouch t1 , PTouch t2 )
        {
            FirstTouch = t1;
            SecondTouch = t2;
        }

        public PTouch FirstTouch { get => firstTouch; set => firstTouch = value; }
        public PTouch SecondTouch { get => secondTouch; set => secondTouch = value; }

        public PVector Delta
        {
            get
            {
                return PVector.Sub(SecondTouch.ActualPoint , FirstTouch.ActualPoint);
            }
        }

        public double DeltaDistance
        {
            get
            {
                return Delta.Mag();
            }
        }

        public PVector PreviewsDelta
        {
            get
            {
                if ( HasPreviews )
                {
                    return PVector.Sub(SecondTouch.PrevPoint , FirstTouch.PrevPoint);
                } else
                { return null; }
            }
        }

        public double PreviewsDeltaDistance
        {
            get
            {
                if ( HasPreviews )
                { return PreviewsDelta.Mag(); } else
                { return 1; }
            }
        }

        public PVector ActualMidPoint
        {
            get
            {
                var v = Delta.Copy();
                v.Mult(0.5);
                v.Add(FirstTouch.ActualPoint);
                return v;
            }
        }

        public PVector PreviewsMidPoint
        {
            get
            {
                if ( !HasPreviews )
                { return null; }
                var v = PreviewsDelta.Copy();
                v.Mult(0.5);
                v.Add(FirstTouch.PrevPoint);
                return v;
            }
        }

        public PVector MoveVector
        {
            get
            {
                if ( HasPreviews )
                {
                    return PVector.Sub(ActualMidPoint , PreviewsMidPoint);
                } else
                { return null; }
            }
        }

        public float ScaleValue
        {
            get
            {
                if ( !HasPreviews )
                    return 1;
                return ( float ) DeltaDistance / ( float ) PreviewsDeltaDistance;
            }
        }

        public float RotationAngle
        {
            get
            {
                if ( !HasPreviews )
                    return 0;
                return ( float ) PVector.AngleBetween(Delta , PreviewsDelta);
            }
        }

        public bool HasPreviews
        {
            get
            {
                return FirstTouch.HasPreviews && SecondTouch.HasPreviews;
            }
        }
    }
}