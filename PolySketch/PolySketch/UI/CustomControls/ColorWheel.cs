using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PolySketch.UI.CustomControls
{
    public class ColorWheel : View
    {
        #region fields
        private double Hue;
        private double Saturation;
        private double Value;
        Image image;
        Color pixelColor;
        #endregion
        #region bindings
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            propertyName: "Color" ,
            returnType: typeof(Color) ,
            declaringType: typeof(ColorWheel) ,
            defaultValue: Color.Black);
        #endregion
        #region constructors
        public ColorWheel()
        {
            pixelColor = Color.Red;
                  
        }
        #endregion
    }
}
