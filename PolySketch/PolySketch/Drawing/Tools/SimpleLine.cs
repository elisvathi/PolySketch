using Poly.Geometry;

namespace PolySketch.Drawing.Tools
{
    public class SimpleLine : AbstractTool
    {
        public SimpleLine()
        {
            BaseGeometry = new Line();
        }

        public override void Update( PVector point )
        {
            base.Update(point);
            RuledData.Clear();
        }
    }
}