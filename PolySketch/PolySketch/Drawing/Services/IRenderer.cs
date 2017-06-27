using Poly.Geometry;
using PolySketch.Drawing.Sprites;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PolySketch.Drawing.Services
{
    public interface IRenderer
    {
        bool DrawStroke { get; }
        bool DrawFill { get; }
        Color StrokeColor { get; }
        Color FillColor { get; }
        float StrokeWidth { get; }

        void DrawLine( PVector start , PVector end );

        void DrawArc( PVector start , float radius , float starAngle , float endAngle );

        void DrawArc( PVector start , PVector startPoint , float angle );

        void DrawCircle( PVector center , float radius );

        void DrawPolygon( List<PVector> points , bool closed );

        void DrawPolygon( PVector[] points , bool closed );

        void DrawCurve( List<PVector> points , bool closed );

        void DrawCurve( PVector[] points , bool closed );

        void DrawSprite( PSprite sprite , PVector position );

        void Clear();
    }
}