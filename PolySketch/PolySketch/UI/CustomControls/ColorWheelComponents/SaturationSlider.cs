using PolySketch.Drawing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PolySketch.Helpers;
using Poly.Geometry;

namespace PolySketch.UI.CustomControls.ColorWheelComponents
{
   public class SaturationSlider   :RoundSlider
    {
        private ColorChangedEventDispatcher Dispatcher;
        private DrawingConfigService Service;
        private ColorWheelCocos Container;
        private bool Stroke { get { return Container.Stroke; } }
        private Color Col { get { return Stroke ? Service.StrokeColor : Service.FillColor; } }
        public SaturationSlider( ColorChangedEventDispatcher disp , DrawingConfigService service , ColorWheelCocos cont ) : base(cont.SaturationRadius , cont.SaturationWidth , cont.SaturationStartAngle ,cont.SaturationEndAngle, 0 , 1 )
        {
            ChangableBackground = true;
            Dispatcher = disp;
            Container = cont;
            Service = service;
            Dispatcher.ColorChangeFinished += UpdateControl;
            SliderChanged += ChangeColor;
            BgFunction = GetSaturationData;
            UpdateControl(Stroke , Col);
        }

        private void ChangeColor( RoundSlider slider , float val )
        {
            var hsv = Col.ToHSV();
            hsv.Saturation = val;
            Dispatcher.OnColorChangedRequest(Stroke , hsv.GetRGB());
        }

        private void UpdateControl( bool _Stroke , Color c )
        {
            if ( Stroke == _Stroke )
            {
                ReturnValue = ( float ) Col.ToHSV().Saturation;
            }
        }
       
        private  Color GetSaturationData( float arg )
        {
            float temp = arg.Clamp(0 , 1);
            temp = 1 - temp;
            var temphsv = Col.ToHSV();
            
            var hsv = new HSVColor((float)temphsv.Hue , temp , (float)temphsv.Value);
            return hsv.GetRGB();
        }
    }
}
