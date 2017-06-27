using CocosSharp;
using Microsoft.Xna.Framework;
using Poly.Geometry;
using System;
using System.Collections.Generic;

namespace PolySketch.UI.CustomControls
{
    public class RoundSlider : CCNode
    {
        #region Events and Delegeates Declaration

        public delegate void SliderChangedDelegate(RoundSlider slider, float val);

        public event SliderChangedDelegate SliderChanged;

        #endregion Events and Delegeates Declaration

        #region Fields

        private float _StartAngle = 0;
        private float _EndAngle = 0;
        private float _ActualValue = 0;
        private Func<float, Color> _BgFunction;

        #endregion Fields

        #region Private Properties

        private CCDrawNode Handler { get; set; }
        private CCDrawNode Background { get; set; }
        private float StartAngle { get { return _StartAngle; } set { _StartAngle = CheckStartAngle(value); EndAngle = CheckEndAngle(EndAngle); } }
        private float EndAngle { get { return _EndAngle; } set { _EndAngle = CheckEndAngle(value); } }
        private float ArcAngle { get { return EndAngle - StartAngle; } }
        private float ArcLength { get { return (float)(((double)ArcAngle).ToRadians() * Radius); } }

        #endregion Private Properties

        #region Public Properties

        public float Radius { get; set; }
        public float Width { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        public float Step { get; set; }
        public bool ChangableBackground { get; set; } = true;
        public Func<float, Color> BgFunction { get { return _BgFunction; } set { _BgFunction = value; RedrawSlider(); } }
        public float ActualValue { get { return _ActualValue; } set { _ActualValue = value.Clamp(0, 1); RedrawSlider(); } }
        public float ReturnValue { get { return ActualValue * (MaxValue - MinValue) + MinValue; } set { ActualValue = (value - MinValue) / (MaxValue - MinValue); } }

        #endregion Public Properties

        #region Constructor

        public RoundSlider(float radius, float width, float maxValueAngle = 0, float minValueAngle = 360, float min = 0, float max = 1, Func<float, Color> func = null, float step = 0)
        {
            if (func == null)
            { _BgFunction = DefaultBackgroundFunction; }
            else
            {
                _BgFunction = func;
            }
            Radius = radius;
            Width = width;
            StartAngle = maxValueAngle;
            EndAngle = minValueAngle;
            MinValue = min;
            MaxValue = max;
            Step = step;
            Handler = new CCDrawNode();
            Background = new CCDrawNode();
            var eventListenter = new CCEventListenerTouchAllAtOnce();
            eventListenter.OnTouchesBegan = TouchesBegan;
            eventListenter.OnTouchesMoved = TouchesMoved;
            AddEventListener(eventListenter);
            InitialDraw();
        }

        #endregion Constructor

        #region Private Methods

        private float CheckEndAngle(float end)
        {
            var temp = end;
            if (end < StartAngle)
            {
                temp = end + 360;
            }
            else if (temp - StartAngle > 360)
            {
                temp = CheckEndAngle((temp - StartAngle) % 360);
            }
            if (StartAngle == 0 && end == 360)
            { temp = 360; }
            return temp;
        }

        private float CheckStartAngle(float start)
        {
            var temp = start % 360;
            if (temp < 0)
            {
                temp = 360 + temp;
            }
            return temp % 360;
        }

        private void InitialDraw()
        {
            Background.Clear();
            Handler.Clear();
            ProcessBackground();
            ProcessHandler();
            UpdateHandler();
            Background.Position = new CCPoint(0, 0);
            Handler.Position = new CCPoint(0, 0);
        }

        private void ProcessHandler()
        {
            Handler.ContentSize = new CCSize(40, Radius);
            Handler.DrawLine(new CCPoint(20, Radius), new CCPoint(20, Radius - Width), 2, CCColor4B.Gray);
            Handler.DrawSolidCircle(new CCPoint(20, Radius), 10, CCColor4B.Gray);
            Handler.AnchorPoint = new CCPoint(0.5F, 0);
        }

        private void UpdateHandler()
        {
            Handler.Rotation = ArcAngle * (1 - ActualValue) + StartAngle;
        }

        private Color DefaultBackgroundFunction(float arg)
        {
            var clampedArg = arg.Clamp(0.0F, 1.0F);
            float value = 255.0F - clampedArg * 255.0F;
            var col = new Color((int)value, (int)value, (int)value);
            return col;
        }

        private bool HasPoint(PVector point)
        {
            PVector cent = Position.GetVector();
            cent.Add(Parent.Position.GetVector());
            PVector DifVector = PVector.Sub(point, cent);
            var test1 = DifVector.Mag() < Radius;
            var test2 = DifVector.Mag() > Radius - Width;
            var rad = new PVector(0, 1);
            var angle = PVector.AngleBetween(DifVector, rad);
            if (angle < 0)
            {
                angle = 360 + angle;
            }
            var test3 = angle >= StartAngle;
            var test4 = angle <= EndAngle;
            return test1 && test2 && test3 && test4;
        }

        protected virtual void OnSliderChanged()
        {
            SliderChanged?.Invoke(this, ReturnValue);
        }

        private void TouchesMoved(List<CCTouch> arg1, CCEvent arg2)
        {
            if (arg1.Count == 1)
            {
                var touch = arg1[0].GetTouch();
                if (HasPoint(touch.ActualPoint))
                {
                    UpdateModel(touch.ActualPoint);
                }
            }
        }

        private void TouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            TouchesMoved(arg1, arg2);
        }

        private void ProcessBackground()
        {
            int numSteps = (int)ArcLength;

            if (BgFunction != null)
            {
                for (int i = 0; i < numSteps; i++)
                {
                    float rap = (float)i / (float)numSteps;
                    var col = BgFunction(rap);
                    var cocosCol = col.CocosColor();
                    var rad = new PVector(0, Radius);
                    rad.RotateVector(-(rap * ArcAngle + StartAngle));
                    var vec2 = rad.Copy();
                    vec2.SetMag(Radius - Width);
                    Background.DrawLine(rad.GetPoint(), vec2.GetPoint(), cocosCol);
                }
            }
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            RemoveAllChildren();
            AddChild(Background);
            AddChild(Handler);
            UpdateHandler();
        }

        protected void UpdateModel(PVector point)
        {
            var cent = PositionWorldspace.GetVector();
            var rad = PVector.Sub(point, cent);
            var vertical = new PVector(0, Radius);
            var ang = PVector.AngleBetween(rad, vertical);
            if (ang < 0)
            { ang = 360 + ang; }
            var rap = 1 - ((ang - StartAngle) / ArcAngle);
            ActualValue = (float)rap;
            OnSliderChanged();
        }

        #endregion Private Methods

        #region Public Methods

        public void RedrawSlider()
        {
            if (ChangableBackground)
            { Background.Clear(); ProcessBackground(); }
            UpdateHandler();
        }

        #endregion Public Methods
    }
}