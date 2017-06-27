using Poly.Geometry;

namespace PolySketch.Drawing.Brushes
{
    public class StandartBrush : AbstractBrush
    {
        private float _BrushRadius;

        public float BrushRadius { get => _BrushRadius; set => _BrushRadius = value; }

        public override void Draw( PVector[] pointData )
        {
            foreach ( var p in pointData )
            {
                Renderer.DrawCircle(p , BrushRadius);
            }
        }
    }
}