using PolySketch.Geometry;

namespace PolySketch.Drawing.Tools
{
    public class PolygonTool : AbstractTool
    {
        public PolygonTool() : base()
        {
            BaseGeometry = new Polygon();
        }
    }
}