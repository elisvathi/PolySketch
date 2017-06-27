using PolySketch.Drawing.Tools.Interfaces;
using System;

namespace PolySketch.Drawing.Tools
{
    public class PolyLine : AbstractTool, ICompoundable
    {
        public PolyLine() : base()
        {
        }

        public void Finish()
        {
            throw new NotImplementedException();
        }

        public void StartNextPart()
        {
            throw new NotImplementedException();
        }
    }
}