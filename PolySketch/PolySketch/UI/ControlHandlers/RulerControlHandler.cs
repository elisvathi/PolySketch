using PolySketch.Drawing;
using System.Collections.Generic;

namespace PolySketch.UI.ControlHandlers
{
    internal class RulerControlHandler
    {
        private DrawingToolsManager Manager;
        public List<string> RulerTypes => Manager.GetRulerTypes();

        public RulerControlHandler( DrawingToolsManager manager )
        {
            Manager = manager;
        }

        public void SetRuler( string ruler )
        {
            Manager.SetRuler(ruler);
        }

        public void SetRuler( int n )
        {
            SetRuler(RulerTypes[n]);
        }
    }
}