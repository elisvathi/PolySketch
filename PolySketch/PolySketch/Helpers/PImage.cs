using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace PolySketch.Helpers
{
    public class PImage
    {
        byte[] image;
        public PImage(Bitmap b )
        {
            image = b.ToArray<byte>();
        }
        public PImage(UIImage b )
        {
            image = b.AsJPEG().ToArray<byte>();
        }
    }
}
