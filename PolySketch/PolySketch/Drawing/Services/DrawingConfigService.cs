using System;
using Microsoft.Xna.Framework;
using PolySketch.UI.CustomControls.ColorWheelComponents;

namespace PolySketch.Drawing.Services
{
    public class DrawingConfigService
    {
        public bool DrawStroke { get; set; } = true;
        public bool DrawFill { get; set; } = false;
        public Color StrokeColor { get; set; }
        public Color FillColor { get; set; }
        public float StrokeWidth { get; set; }
        private ColorChangedEventDispatcher Dispatcher;

        public DrawingConfigService( ColorChangedEventDispatcher disp )
        {
            DrawStroke = true;
            DrawFill = false;
            StrokeColor = new Color(120 , 130 ,78);
            FillColor = new Color(0 , 0 , 0);
            StrokeWidth = 1;
            Dispatcher = disp;
            Dispatcher.ColorChangeRequest += ChangeColor;
        }

        private void ChangeColor( bool Stroke , Color c )
        {
            if ( Stroke )
            { StrokeColor = c; } else
            { FillColor = c; }
            Dispatcher.OnColorChangedFinished(Stroke , c);
        }
    }
}