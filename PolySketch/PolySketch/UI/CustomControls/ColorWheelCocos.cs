using CocosSharp;
using Microsoft.Xna.Framework;
using Ninject;
using Poly.Geometry;
using Poly.NinjectModules.Kernels;
using PolySketch.Drawing.Services;
using PolySketch.UI.CustomControls.ColorWheelComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySketch.UI.CustomControls
{
    public class ColorWheelCocos : CCNode
    {
        CCDrawNode HueCircleNode { get { return HueController.Node; } }
        CCDrawNode ValueArcNode { get { return ValueController.Node; } }
        CCDrawNode SaturationArcNode { get { return SaturationController.Node; } }
        CCDrawNode SelectedColorNode;
        KernelService Service;
        private bool _Stroke;
        private DrawingConfigService ConfigService;

        public HueCircle HueController { get { return Service.ActiveKernel.Get<HueCircle>(); } }

        public SaturationBar SaturationController { get { return Service.ActiveKernel.Get<SaturationBar>(); } }

        public ValueBar ValueController { get { return Service.ActiveKernel.Get<ValueBar>(); } }

        public HueSlider HueSliderController { get { return Service.ActiveKernel.Get<HueSlider>(); } }
        public ValueSlider ValueSliderController { get { return Service.ActiveKernel.Get<ValueSlider>(); } }
        public SaturationSlider SaturationSliderController {get{ return Service.ActiveKernel.Get<SaturationSlider>(); } }

        public float HueCircleRadius = 100;
        public float HueCircleWidth = 20;   


        public float ValueWidth = 20;
        public float ValueHeight = 30;
        public float ValueRadius = 130;

        public float ValueStartAngle = 90;
        public float ValueEndAngle = 270;

        public float SaturationWidth = 20;
        public float SaturationHeight = 30;
        public float SaturationRadius = 160;

        public float SaturationStartAngle = 90;
        public float SaturationEndAngle = 270;

        Color ActualColor { get; set; }
        public bool Stroke { get => _Stroke; set => _Stroke = value; }

        public ColorWheelCocos( KernelService service , ColorChangedEventDispatcher disp , DrawingConfigService drawingService ) : base()
        {
            Service = service;
            ConfigService = drawingService;
            Stroke = true;
            disp.ColorChangeFinished += DrawSelectedColor;
            SelectedColorNode = new CCDrawNode();
        }

        private void DrawSelectedColor( bool Stroke , Color c )
        {

            DrawSelectedColor();

        }
        private void DrawSelectedColor()
        {
            SelectedColorNode.Clear();
            SelectedColorNode.DrawSolidCircle(new CCPoint(0 , 0) , ( HueCircleRadius - HueCircleWidth ) * 0.9F , ConfigService.StrokeColor.CocosColor());
            SelectedColorNode.DrawSolidCircle(new CCPoint(0 , 0) , ( HueCircleRadius - HueCircleWidth ) * 0.75F , ConfigService.FillColor.CocosColor());
            
        }

        private CCEaseSineInOut GenerateAction(float totalTime, bool inverse )
        {
            var scale1 = new CCScaleTo(totalTime/3.0F * 2 , 1.2F);
            var scale2 = inverse? new CCScaleTo(totalTime / 3.0F , 0):new CCScaleTo(totalTime / 3.0F , 1);
            var rotate1 = new CCRotateBy(totalTime / 3.0F * 2 , 400);
            var rotate2 = new CCRotateBy(totalTime / 3.0F , -40);
            var action1 = new CCParallel(new CCFiniteTimeAction[] { scale1 , rotate1 });
            var action2 = new CCParallel(new CCFiniteTimeAction[] { scale2 , rotate2 });
            var seq = new CCSequence(new CCFiniteTimeAction[] { action1 , action2 });
            var ease = new CCEaseSineInOut(seq);
            return ease;
        }
        public void PrepareToClose()
        {
           
            RemoveAllChildren();
        }
       

        protected override void AddedToScene()
        {
            base.AddedToScene();
            DrawSelectedColor();
            var bg = new CCDrawNode();
            var col = new Color(255 , 255 , 255 , 150);

            bg.DrawSolidCircle(new CCPoint(0 , 0) , SaturationRadius * 1.1F , col.CocosColor());
            bg.DrawCircle(new CCPoint(0 , 0) , SaturationRadius * 1.1F , CCColor4B.Black);
            HueCircleNode.Position = new CCPoint(0 , 0);
            SaturationArcNode.Position = new CCPoint(0 , 0);
            ValueArcNode.Position = new CCPoint(0 , 0);
            SelectedColorNode.Position = new CCPoint(0 , 0);
            HueCircleNode.Scale = 0;
            SaturationArcNode.Scale = 0;
            ValueArcNode.Scale = 0;
            AddChild(bg);
            AddChild(SelectedColorNode);
            //AddChild(HueCircleNode);
            //AddChild(ValueArcNode);
            //AddChild(SaturationArcNode);
            AddChild(HueSliderController);
            AddChild(ValueSliderController);
            AddChild(SaturationSliderController);
            HueSliderController.AddAction(GenerateAction(0.3F, false));
            SaturationArcNode.AddAction(GenerateAction(0.4F, false));
            ValueArcNode.AddAction(GenerateAction(0.2F,false));
            SelectedColorNode.AddAction(GenerateAction(0.15F,false));
            

        }

        
    }
}
