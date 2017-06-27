using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using Poly.Geometry;
using PolySketch.Drawing.Services;
using PolySketch.Helpers;

namespace PolySketch.UI.CustomControls.ColorWheelComponents
{
    public class SaturationBar
    {
        #region fields

        private CCDrawNode _node;

        public CCDrawNode Node { get => _node; set => _node = value; }

        private DrawingConfigService Service;
        private ColorWheelCocos Container;
        private ColorChangedEventDispatcher Dispatcher;
        private bool Stroke { get { return Container.Stroke; } }
        private Color Col { get { return Stroke ? Service.StrokeColor : Service.FillColor; } }
        private float Width { get { return Container.SaturationWidth; } }
        private float Height { get { return Container.SaturationHeight; } }
        private float Radius { get { return Container.SaturationRadius; } }
        private float StartAngle { get { return Container.SaturationStartAngle; } }
        private float EndAngle { get { return Container.SaturationEndAngle; } }
        #endregion fields

        #region Constructors

        public SaturationBar( CCDrawNode node , ColorChangedEventDispatcher disp , DrawingConfigService service , ColorWheelCocos container )
        {
            Node = node;
            Dispatcher = disp;
            Service = service;
            Container = container;
            disp.ColorChangeFinished += RedrawNode;
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
            PVector cent = Node.Position.GetVector();
            cent.Add(Node.Parent.Position.GetVector());
            var difVector = PVector.Sub(actualPoint , cent);
            var rad = new PVector(0 , 1);
            var angl = PVector.AngleBetween(difVector , rad);
            if(angl < 0 )
            { angl = 360 - -angl; }
            angl = angl.Clamp(StartAngle , EndAngle);
            var rap = 1- (angl - StartAngle) / ( EndAngle - StartAngle );
            rap = rap.Clamp(0 , 1);
            var hsv = Col.ToHSV();
            hsv.Saturation = rap;
            var rgb = hsv.GetRGB();
            Dispatcher.OnColorChangedRequest(Stroke , rgb);
        }

        private bool HasPoint( PVector actualPoint )
        {
            PVector cent = Node.Position.GetVector();
            cent.Add(Node.Parent.Position.GetVector());
            PVector difVector = PVector.Sub(actualPoint , cent);
            var test1 = difVector.Mag() < Radius * 1;
            var test2 = difVector.Mag() >= ( Radius - Width ) * 1;
            var rad = new PVector(0 , 1);
            var angle = PVector.AngleBetween(difVector , rad);
            if ( angle < 0 )
            { angle = 360 - -angle; }
            var test3 = angle >= StartAngle;
            var test4 = angle <= EndAngle;
            var finalTest = test1 && test2 && test3 && test4;
           
            return finalTest;
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
        #endregion Constructors
        private void RedrawNode( bool _stroke , Color c )
        {
            if(_stroke == Stroke )
            {
                RedrawNode();
            }
        }

        private void RedrawNode()
        {
            DrawArc();
        }

        

        #region ColorArray

        public Color[] GetSaturationArray( double hue , double value , int precision )
        {
            Color[] retval = new Color[precision];
            for ( int i = 0 ; i < precision ; i++ )
            {
                var sat = ( ( float ) i ).Map(0 , precision , 0 , 1);
                retval[i] = new HSVColor(( float ) hue , ( float ) sat , ( float ) value).GetRGB();
            }
            return retval;
        }

        #endregion ColorArray

        #region DrawBar

        public void DrawSaturationBar()
        {
            var hsv = Col.ToHSV();
            var hue =hsv.Hue;
            var val =hsv.Value;
            
            Node.Clear();
            var data = GetSaturationArray(hue , val , ( int ) Width);
            for ( int i = 0 ; i < data.Length ; i++ )
            {
                var ind = i - Width / 2;
                var col = data[i].CocosColor();
                Node.DrawLine(new CCPoint(ind , ( float ) ( Height * -0.5 )) , new CCPoint(ind , ( float ) ( Height * 0.5 )) , col);
            }

            var actualValue = hsv.Saturation;
            var centerPos = actualValue * Width - Width / 2;
            CCPoint[] verts = new CCPoint[] {
                new CCPoint((float)(centerPos-Height/4), -Height/2),
                new CCPoint((float)(centerPos-Height/4), Height/2),
                new CCPoint((float)(centerPos+Height/4), Height/2),
                new CCPoint((float)(centerPos+Height/4), -Height/2),
            };
            Node.DrawPolygon(verts , count: verts.Length , fillColor: CCColor4B.Transparent , borderWidth: 2 , borderColor: CCColor4B.Black , closePolygon: true);
        }

        public void DrawArc()
        {
            var hsv = Col.ToHSV();
            var hue = hsv.Hue;
            var sat = hsv.Saturation;

            Node.Clear();

            var center = new PVector(0 , 0);
           
            var rad = new PVector(0 , Radius);
            var st = rad.Copy();
            st.RotateVector(StartAngle);
            var en = rad.Copy();
            en.RotateVector(EndAngle);
            double AngleDistance;
            if ( EndAngle < StartAngle )
            {
                AngleDistance = ( 360 - StartAngle ) + EndAngle;
            } else
            {
                AngleDistance = EndAngle - StartAngle;
            }
            double ArcLength = AngleDistance.ToRadians() * Radius;
            var data = GetSaturationArray(hue , sat , ( int ) ArcLength);
            double rap = AngleDistance / data.Length;
            for ( int i = 0 ; i < data.Length ; i++ )
            {
                var point = new PVector(0 , Radius);
                point.RotateVector(StartAngle + i*rap);
                var point2 = point.Copy();
                point2.SetMag(Radius - Width);
                var cocosColor = data[i].CocosColor();
                Node.DrawLine(point.GetPoint() , point2.GetPoint() , cocosColor);
            }

            var SelectedValue = hsv.Saturation;
            var CenterAngle = AngleDistance * SelectedValue + StartAngle;
            var SectionLength = ArcLength / 50;
            var ThetaDist = SectionLength / Radius;
            ThetaDist = ThetaDist.ToDegrees();
            var SectionStartAngle = CenterAngle - ThetaDist;
            var SectionEndAngle = CenterAngle + ThetaDist;
            var p1 = new PVector(0 , Radius);
            p1.RotateVector(SectionStartAngle);
            var p2 = new PVector(0 , Radius);
            p2.RotateVector(SectionEndAngle);
            var p3 = p1.Copy();
            p3.SetMag(Radius - Width);
            var p4 = p2.Copy();
            p4.SetMag(Radius - Width);
            CCPoint[] verts = new CCPoint[]
            {
                p1.GetPoint(), p2.GetPoint(), p4.GetPoint(), p3.GetPoint()
            };
            Node.DrawPolygon(verts , count: verts.Length , fillColor: CCColor4B.Transparent , borderWidth: 2 , borderColor: CCColor4B.Black , closePolygon: true);


        }
        #endregion DrawBar
    }
}