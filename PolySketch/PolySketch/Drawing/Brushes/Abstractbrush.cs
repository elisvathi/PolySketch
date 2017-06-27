using Ninject;
using Poly.Geometry;
using PolySketch.Drawing.Services;

namespace PolySketch.Drawing.Brushes
{
    public abstract class AbstractBrush : IBrush
    {
        [Inject]
        public IRenderer Renderer { get; set; }

        public abstract void Draw( PVector[] pointData );
    }
}