using Poly.Geometry;
using PolySketch.Drawing.Rulers;
using PolySketch.Drawing.Tools.Interfaces;
using PolySketch.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace PolySketch.Drawing.Tools
{
    public abstract class AbstractTool : ITool, IRulable, IInitializableTool, IUpdatable
    {
        private IGeometry _BaseGeometry;
        public IGeometry BaseGeometry { get => _BaseGeometry; set => _BaseGeometry = value; }

        public AbstractTool()
        {
            RuledData = new List<PVector>();
        }

        private List<PVector> _RuledData;

        public List<PVector> RuledData { get => _RuledData; set => _RuledData = value; }

        private int RuledCount
        { get { return RuledData.Count; } }

        public void Initialize( PVector point )
        {
            BaseGeometry.AddStartingPoint(point);
        }

        public List<PVector> GetDataToDraw( int precision )
        {
            return BaseGeometry.GetDataToDraw(precision).ToList();
        }

        public virtual void Update( PVector point )
        {
            BaseGeometry.AddUpdatePoint(point);
        }

        public void UpdateWithRuler( IRuler ruler )
        {
            var newData = ruler.UpdateWithRuler(RuledData , UnRuledData);
            foreach ( var v in newData )
            {
                RuledData.Add(v);
            }

            BaseGeometry.UpdateWithData(_RuledData.ToArray());
        }

        public List<PVector> UnRuledData
        {
            get
            {
                var retval = BaseGeometry.ControlPoints.ToList();
                retval.RemoveRange(0 , RuledCount);
                return retval;
            }
        }
    }
}