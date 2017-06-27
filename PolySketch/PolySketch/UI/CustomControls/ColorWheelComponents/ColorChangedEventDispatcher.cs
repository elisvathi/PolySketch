using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySketch.UI.CustomControls.ColorWheelComponents
{
    public class ColorChangedEventDispatcher
    {
        public delegate void ColorChangedDelegate( bool Stroke , Color c );
        public event ColorChangedDelegate ColorChangeRequest;
        public event ColorChangedDelegate ColorChangeFinished;
        public virtual void OnColorChangedRequest(bool stroke, Color c)
        {
            ColorChangeRequest?.Invoke(stroke , c);
        }
        public virtual void OnColorChangedFinished(bool stroke, Color c )
        {
            ColorChangeFinished?.Invoke(stroke , c);
        }
    }
}
