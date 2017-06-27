using PolySketch.Drawing;
using System.Collections.Generic;

namespace PolySketch.UI.ControlHandlers
{
    internal class BrushControlHandler
    {
        private DrawingToolsManager Manager;
        public List<string> BrushTypes => Manager.GetBrushTypes();

        public BrushControlHandler( DrawingToolsManager manager )
        {
            Manager = manager;
        }

        public void SetBrush( string brush )
        {
            Manager.SetBrush(brush);
        }

        public void SetBrush( int n )
        {
            SetBrush(BrushTypes[n]);
        }
    }
}