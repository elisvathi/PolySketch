using Poly.Geometry;

namespace PolySketch.Helpers
{
    public class PTouch
    {
        private PVector _ActualPoint;
        private PVector _PrevPoint;

        public PVector ActualPoint { get => _ActualPoint; set => _ActualPoint = value; }
        public PVector PrevPoint { get => _PrevPoint; set => _PrevPoint = value; }

        public PTouch( PVector p , PVector other )
        {
            ActualPoint = p;
            PrevPoint = other;
        }

        public PTouch( PVector p )
        {
            ActualPoint = p;
            PrevPoint = null;
        }

        public bool HasPreviews
        {
            get
            {
                return PrevPoint != null;
            }
        }

        public void AddTouch( PVector p )
        {
            PrevPoint = ActualPoint.Copy();
            ActualPoint = p;
        }

        public PVector Delta
        {
            get
            {
                return PVector.Sub(ActualPoint , PrevPoint);
            }
        }

        public double DeltaDistance
        {
            get
            {
                return Delta.Mag();
            }
        }
    }
}