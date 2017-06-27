using CocosSharp;
using Ninject;
using Poly.Geometry;
using Poly.NinjectModules.Kernels;
using PolySketch.Drawing.Sprites;
using PolySketch.Layering;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace PolySketch.Drawing.Services
{
    public class CocosRenderer : IRenderer
    {
        //TODO: Inject properties from DrawingConfigService
        public object Node { get { return Service.ActiveKernel.Get<LayerManager>().ActveDrawNode.GetDrawNode(); } }

        public object SimpleNode { get { return Service.ActiveKernel.Get<LayerManager>().ActiveSimpleNode.GetNode(); } }

        [Inject]
        public DrawingConfigService ConfigService { get; set; }

        [Inject]
        public KernelService Service { get; set; }

        public bool DrawStroke { get { return ConfigService.DrawStroke; } }
        public bool DrawFill { get { return ConfigService.DrawFill; } }
        public Color StrokeColor { get { return ConfigService.StrokeColor; } }
        public Color FillColor { get { return ConfigService.FillColor; } }
        public float StrokeWidth { get { return ConfigService.StrokeWidth; } }

        public CCColor4B CocosFillColor { get { return FillColor.CocosColor(); } }
        public CCColor4B CocosStrokeColor { get { return StrokeColor.CocosColor(); } }

        public void Clear()
        {
            ( Node as CCDrawNode ).Cleanup();
            ( Node as CCDrawNode ).Clear();
        }

        public void DrawArc( PVector start , float radius , float starAngle , float endAngle )
        {
            if ( DrawFill )
            {
                ( Node as CCDrawNode ).DrawSolidArc(start.GetPoint() , radius , starAngle , endAngle - starAngle , CocosFillColor);
            } else
            {
                //TODO: IMPLEMENT NON SOLID ARC
            }
        }

        public void DrawArc( PVector start , PVector startPoint , float angle )
        {
            PVector rad = PVector.Sub(startPoint , start);
            float radius = ( float ) rad.Mag();
            float startAngle = ( float ) rad.Angle;
            DrawArc(start , radius , startAngle , angle + startAngle);
        }

        public void DrawCircle( PVector center , float radius )
        {
            if ( DrawFill )
            {
                ( Node as CCDrawNode ).DrawSolidCircle(center.GetPoint() , radius , CocosFillColor);
            }
             ( Node as CCDrawNode ).DrawCircle(center.GetPoint() , radius , CocosStrokeColor);
        }

        public void DrawCurve( List<PVector> points , bool closed )
        {
            var pts = new List<CCPoint>();
            foreach ( var p in points )
            {
                pts.Add(p.GetPoint());
            }
            //TODO: Parametrize DrawCatmullRom Curve precision
            ( Node as CCDrawNode ).DrawCatmullRom(pts , 100 , CocosStrokeColor);
        }

        public void DrawCurve( PVector[] points , bool closed )
        {
            DrawCurve(points.ToList() , closed);
        }

        public void DrawLine( PVector start , PVector end )
        {
            //TODO: Cast color
            ( Node as CCDrawNode ).DrawLine(start.GetPoint() , end.GetPoint() , StrokeWidth , CocosStrokeColor);
        }

        public void DrawPolygon( List<PVector> points , bool closed )
        {
            var pts = new List<CCPoint>();
            foreach ( var p in points )
            {
                pts.Add(p.GetPoint());
            }
            //( Node as CCDrawNode ).DrawPolygon(pts.ToArray() , pts.Count , CocosFillColor , StrokeWidth , CocosStrokeColor , closed);
            //TODO: Temporary replacement
            for ( int i = 0 ; i < points.Count - 1 ; i++ )
            {
                DrawLine(points[i] , points[i + 1]);
            }
        }

        public void DrawPolygon( PVector[] points , bool closed )
        {
            DrawPolygon(points.ToList() , closed);
        }

        //TODO: Implement DrawSprite
        public void DrawSprite( PSprite sprite , PVector position )
        {
            throw new NotImplementedException();
        }
    }
}