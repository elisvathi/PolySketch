using CocosSharp;

using Microsoft.Xna.Framework;
using Poly.Geometry;
using PolySketch.Drawing.Services;
using PolySketch.Helpers;
using System;
using System.Collections.Generic;

namespace PolySketch.UI.CustomControls.ColorWheelComponents
{
    public class HueCircle
    {
        #region Fields

        private CCDrawNode _node;

        public CCDrawNode Node { get => _node; set => _node = value; }
        private ColorChangedEventDispatcher Dispatcher;
        private ColorWheelCocos Container;
        private DrawingConfigService Service;
        private bool Stroke { get { return Container.Stroke; } }
        private Color Col { get { return Stroke ? Service.StrokeColor : Service.FillColor; } }
        #endregion Fields

        #region Constructors

        public HueCircle( CCDrawNode node , ColorChangedEventDispatcher disp , DrawingConfigService service , ColorWheelCocos container )
        {
            Node = node;
            Dispatcher = disp;
            disp.ColorChangeFinished += RedrawNode;
            Container = container;
            Service = service;
            CCEventListenerTouchAllAtOnce touchListener = new CCEventListenerTouchAllAtOnce()
            {
                OnTouchesBegan = TouchesBegan ,
                OnTouchesMoved = TouchesMoved
            };
            Node.AddEventListener(touchListener);
            RedrawNode();
        }

        private void TouchesMoved( List<CCTouch> arg1 , CCEvent arg2 )
        {
            if ( arg1.Count == 1 )
            {
                var point = arg1[0].GetTouch();
                if ( HasPoint(point.ActualPoint) )
                {
                    UpdateModel(point.ActualPoint);
                }
            }
        }

        private void UpdateModel( PVector actualPoint )
        {
            var p = new PVector(0 , 1);
            var cen = Node.Position.GetVector();
            cen.Add(Node.Parent.Position.GetVector());
            var ac = actualPoint.Copy();
            ac.Sub(cen);
            var ang = PVector.AngleBetween(p, ac);
            
            if ( ang < 0 )
            {
                ang = 360 - -ang;
            }
            var hsv = Col.ToHSV();
            hsv.Hue = ang;
            var rgb = hsv.GetRGB();
            Dispatcher.OnColorChangedRequest(Stroke , rgb);

        }

        private void TouchesBegan( List<CCTouch> arg1 , CCEvent arg2 )
        {
            if ( arg1.Count == 1 )
            {
                var point = arg1[0].GetTouch();
                if ( HasPoint(point.ActualPoint) )
                {
                    UpdateModel(point.ActualPoint);
                }
            }
        }

        private void RedrawNode( bool _Stroke , Color c )
        {
            if ( Stroke == _Stroke )
            {
                RedrawNode();
            }

        }
        private void RedrawNode()
        {
            HueCircleDraw(Container.HueCircleRadius , Container.HueCircleWidth );
        }
        #endregion Constructors

        #region Color Arrays

        private Color[] GetHueArray( int precision , double sat , double val )
        {
            Color[] retval = new Color[precision];
            for ( int i = 0 ; i < precision ; i++ )
            {
                var angle = ( ( float ) i ).Map(0 , precision , 0 , 360);
                var hsv = new HSVColor(angle , ( float ) sat , ( float ) val);
                var a = hsv.GetRGB();
                retval[i] = hsv.GetRGB();
            }
            return retval;
        }

        private Color[] GetHueArray( int precision )
        {
            return GetHueArray(precision , 1 , 1);
        }

        #endregion Color Arrays

        #region DrawMethods

        public void HueCircleDraw( float radius , float width )
        {
           
            Node.Clear();
            Node.DrawCircle(new CCPoint(0 , 0) , radius , CCColor4B.White);
            Node.DrawCircle(new CCPoint(0 , 0) , radius - width , CCColor4B.White);
            var arclength = 2 * Math.PI * radius;
            var data = GetHueArray(( int ) arclength);
            var step = 360 / arclength;
            for ( int i = 0 ; i < data.Length ; i++ )
            {
                var point = new PVector(0 , radius);

                point.RotateVector(step * i);
                var pp2 = point.Copy();
                pp2.SetMag(radius - width);
                var col = data[i].CocosColor();
                Node.DrawLine(point.GetPoint() , pp2.GetPoint() , col);
            }
            var a = 1;
            var hsv = Col.ToHSV();
            var actualValue = hsv.Hue;
            var centerPos = actualValue * width - width / 2;
            var length = arclength / 50;
            var thetaDist = length / radius;
            thetaDist = thetaDist.ToDegrees();
            var startAngle = actualValue - thetaDist;
            var endAngle = actualValue + thetaDist;
            var p1 = new PVector(0 , radius);
            p1.RotateVector(startAngle);
            var p2 = new PVector(0 , radius);
            p2.RotateVector(endAngle);
            var p3 = p2.Copy();
            p3.SetMag(radius - width);
            var p4 = p1.Copy();
            p4.SetMag(radius - width);
            CCPoint[] verts = new CCPoint[] {
               p1.GetPoint(),p2.GetPoint(), p3.GetPoint(), p4.GetPoint()
            };
            Node.DrawPolygon(verts , count: verts.Length , fillColor: CCColor4B.Transparent , borderWidth: 2 , borderColor: CCColor4B.Black , closePolygon: true);
        }

        #endregion DrawMethods
        #region TouchEvents
        private bool HasPoint( CCPoint p )
        {
            var cent = Node.Position.GetVector();
            cent.Add(Node.Parent.Position.GetVector());
            var dist = p.GetVector().Dist(cent);
            return dist <= Container.HueCircleRadius * 1 && dist >= ( Container.HueCircleRadius - Container.HueCircleWidth ) * 1;
        }
        private bool HasPoint( PVector p )
        {
            return HasPoint(p.GetPoint());
        }


        #endregion
    }
}