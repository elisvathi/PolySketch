using Poly.Geometry;
using PolySketch.Helpers;
using PolySketch.UI.Canvas.Interfaces;

namespace PolySketch.UI.Canvas
{
    public class CanvasCamera
    {
        private PVector _Center;
        private double _Zooom;
        private double _Rotation;
        private ICanvasLayerContainer _Node;

        public CanvasCamera( ICanvasLayerContainer container )
        {
            _Node = container;
            _Node.DoubleTouchMoved += DoubleTouchHandler;
            _Center = new PVector(0 , 0);
            _Zooom = 1;
            _Rotation = 0;
        }

        private void DoubleTouchHandler( PDoubleTouch doubleTouch )
        {
            RotateBy(doubleTouch.RotationAngle);
            MoveBy(doubleTouch.MoveVector);
            ScaleBy(doubleTouch.ScaleValue);
        }

        public PVector Center { get => _Center; set => Move(value); }
        public double Zoom { get => _Zooom; set => Scale(value); }
        public double Rotation { get => _Rotation; set => Rotate(value); }

        public void RotateBy( double angle )
        {
            var firstVal = Rotation;
            var newRot = firstVal + angle;
            Rotate(newRot);
        }

        public void Rotate( double angle )
        {
            //_Node.SetRotation(( float ) angle);
            _Rotation = angle;
        }

        public void ScaleBy( double value )
        {
            var oldScale = Zoom;
            var newScale = Zoom * value;
            Scale(newScale);
        }

        public void Scale( double value )
        {
            //_Node.SetScale(( float ) value);
            _Zooom = value;
        }

        public void Move( PVector newPos )
        {
            var oldVec = Center;
            var newVec = PVector.Sub(oldVec , newPos);
            MoveBy(newVec);
        }

        public void MoveBy( PVector vector )
        {
            var oldPosition = _Node.GetPosition().Copy();
            oldPosition.Add(vector);
            //_Node.SetPosition(oldPosition.Copy());
            _Center = PVector.Add(Center , vector);
        }
    }
}