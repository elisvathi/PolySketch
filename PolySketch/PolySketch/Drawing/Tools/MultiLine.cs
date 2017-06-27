using PolySketch.Drawing.Tools.Interfaces;
using PolySketch.Geometry.Interfaces;
using System.Collections;

namespace PolySketch.Drawing.Tools
{
    public class MultiLine : AbstractTool, IMultilineTool
    {
        private int _Number;

        private float _Offset;

        private float SingleOffset
        { get { return _Offset / (_Number - 1); } }

        public int Number { get => _Number; set => _Number = value; }
        public float Offset { get => _Offset; set => _Offset = value; }

        public MultiLine(int nmber, float offset) : base()
        {
            this._Number = nmber;
            this._Offset = offset;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _Number; i++)
            {
                var newGeom = BaseGeometry.CopyGeometry();
                if (newGeom is IOffsetable)
                {
                    (newGeom as IOffsetable).Offset(SingleOffset * i);
                }
                yield return BaseGeometry;
            }
        }
    }
}