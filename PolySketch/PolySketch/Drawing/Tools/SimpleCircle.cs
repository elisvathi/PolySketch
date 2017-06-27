using Poly.Geometry;
using PolySketch.Geometry;

namespace PolySketch.Drawing.Tools
{
    public class SimpleCircle : AbstractTool
    {
        public SimpleCircle()
        {
            BaseGeometry = new Circle();
        }

        public override void Update( PVector point )
        {
            base.Update(point);
            RuledData.Clear();
        }
    }
}