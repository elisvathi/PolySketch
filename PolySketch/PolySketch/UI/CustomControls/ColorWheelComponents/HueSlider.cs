using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PolySketch.Drawing.Services;
using Poly.Geometry;
using PolySketch.Helpers;

namespace PolySketch.UI.CustomControls.ColorWheelComponents
{
    public class HueSlider : RoundSlider
    {

        private ColorChangedEventDispatcher Dispatcher;
        private DrawingConfigService Service;
        private ColorWheelCocos Container;
        private bool Stroke { get { return Container.Stroke; } }
        private Color Col { get { return Stroke ? Service.StrokeColor : Service.FillColor; } }
        public HueSlider( ColorChangedEventDispatcher disp , DrawingConfigService service , ColorWheelCocos cont ) : base(cont.HueCircleRadius , cont.HueCircleWidth , 0 , 360 , 0 , 360 , GetHueData)
        {
            ChangableBackground = true;
            Dispatcher = disp;
            Container = cont;
            Service = service;
            Dispatcher.ColorChangeFinished += UpdateControl;
            SliderChanged += ChangeColor;
            UpdateControl(Stroke , Col);
            
        }

        private void ChangeColor( RoundSlider slider , float val )
        {
            var hsv = Col.ToHSV();
            hsv.Hue = val;
            Dispatcher.OnColorChangedRequest(Stroke , hsv.GetRGB());
        }

        public void UpdateControl( bool _Stroke , Color c )
        {
            if ( Stroke == _Stroke )
            {
                ReturnValue = (float)Col.ToHSV().Hue;
            }
        }

        private static Color GetHueData( float arg )
        {
            float temp = arg.Clamp(0 , 1);
            temp = 1 - temp;
            temp *= 360;
            var hsv = new HSVColor(temp , 1 , 1);
            return hsv.GetRGB();
        }
    }
}
