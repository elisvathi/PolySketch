using PolySketch.Drawing;
using System.Collections.Generic;

namespace PolySketch.UI.ControlHandlers
{
    internal class ToolControlHandler
    {
        private DrawingToolsManager Manager;
        public List<string> ToolTypes => Manager.GetToolTypes();

        public ToolControlHandler( DrawingToolsManager manager )
        {
            Manager = manager;
        }

        public void SetTool( string tool )
        {
            Manager.SetTool(tool);
        }

        public void SetTool( int n )
        {
            SetTool(ToolTypes[n]);
        }
    }
}