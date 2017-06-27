using CocosSharp;
using Poly.Geometry;
using PolySketch.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySketch.Drawing.Sprites
{
    public class GradientBrush
    {
        struct Triangle {
           public PVector p1;
            public PVector p2;
            public PVector p3;
            public float a1;
            public float a2;
            public float a3;
        }
        public static CCDrawNode GetGradientBrush(float radius, float hardness) {
            int horizontalPrecision = 50;
            int verticalPrecison =30;
            var retval = new CCDrawNode();
            List < Triangle > treks= new List<Triangle>();
            for ( int i = 0 ; i < verticalPrecison ; i++ )
            {
                var startRadius = radius - ( radius / verticalPrecison ) * i;
                if ( startRadius < 0 )
                { startRadius = 0; }
                var endRadius = radius - ( radius / verticalPrecison ) * (i + 1);
                if ( endRadius < 0 )
                { endRadius = 0; }
                var c1 = new Circle(new PVector(0 , 0) , startRadius);
                var c2 = new Circle(new PVector(0 , 0) , endRadius);
                var points1 = c1.Divide(horizontalPrecision);
                var points2 = c2.Divide(horizontalPrecision);
                var a1 = ( ( float ) i ).Map(0 , verticalPrecison , 0 , 1) ;
                
                a1 = (float)Math.Pow(a1 , hardness);
                var a2 = ( ( float ) i + 1 ).Map(0 , verticalPrecison , 0 , 1);
                a2 = ( float ) Math.Pow(a2 , hardness);
                for ( int j = 0 ; j < points1.Length - 1 ; j++ )
                {
                    treks.Add(new Triangle { p1 = points1[j] , p2 = points1[j + 1] , p3 = points2[j] , a1 = a1 , a2 = a1 , a3 = a2 });
                    treks.Add(new Triangle { p1 = points1[j+1] , p2 = points2[j] , p3 = points2[j+1] , a1 = a1 , a2 = a2 , a3 = a2 });
                }
                var len = horizontalPrecision - 1;
                //treks.Add(new Triangle { p1 = points1[len] , p2 = points1[0] , p3 = points2[len] , a1 = a1 , a2 = a1 , a3 = a2 });
                //treks.Add(new Triangle { p1 = points1[0] , p2 = points2[len] , p3 = points2[0] , a1 = a1 , a2 = a2 , a3 = a2 });
            }
            List<CCV3F_C4B> verts = new List<CCV3F_C4B>();
            foreach ( var v in treks )
            {
                var col1 = new CCColor4F(0 , 0 , 0 , v.a1);
                var col2 = new CCColor4F(0 , 0 , 0 , v.a2);
                var col3 = new CCColor4F(0 , 0 , 0 , v.a3);
                var p1 = v.p1.GetPoint();
                var p2 = v.p2.GetPoint();
                var p3 = v.p3.GetPoint();
                verts.Add(new CCV3F_C4B(p1 , col1));
                verts.Add(new CCV3F_C4B(p2 , col2));
                verts.Add(new CCV3F_C4B(p3 , col3));
            }
            retval.DrawTriangleList(verts.ToArray());
            return retval;
        }
    }
}
