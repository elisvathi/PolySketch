using System;
using Ninject;
using Poly.Geometry;
using Poly.NinjectModules.Kernels;
using Xamarin.Forms;
using PolySketch.Drawing.Tools;
using PolySketch.UI.Canvas;
using PolySketch.Helpers;
using CocosSharp;
using PolySketch.Layering;
using PolySketch.Drawing.Sprites;
using PolySketch.UI.CustomControls;
using PolySketch.UI.Canvas.Interfaces;

namespace PolySketch.UI.MainUI
{
    internal class MainCanvasPage : ContentPage
    {
        private KernelService Service;

        

        public MainCanvasPage( KernelService service )
        {
            Service = service;
            Title = "Canvas Page";
            var layout = new RelativeLayout();
            var can = service.ActiveKernel.Get<CanvasWrapperPage>();

           

           layout.AddNewElementFixedPosition(can  , 0 , 0 , 1 , 1);
            var vije = new Button
            {
                Text = "Line"    ,
                
            };
            vije.Clicked += ChangeToolToLine;
            var simple = new Button
            {
                Text = "Simple"
            };
            
            simple.Clicked += ChangeToolToSimple;
            var hue = new Button
            {
                Text = "HUE"
            };
            hue.Clicked += DrawHueCircle;
            layout.AddNewElementFixedSize(vije , 0.1 , 0.25 , 100, 50);
            layout.AddNewElementFixedSize(simple , 0.1 , 0.75 , 100, 50);
            layout.AddNewElementFixedSize(hue , 0.1 , 0.5 , 100 , 50);
            Content = layout;
        }

        private ColorWheelCocos Child { get { return Service.ActiveKernel.Get<ColorWheelCocos>(); } }
        private bool IsPickerDisplayed = false;
        private void DrawHueCircle( object sender , EventArgs e )
        {
            //var child = HSVtoRGB.SaturationBar(180 , 0.5F , 300 , 30);
            
            var node = Service.ActiveKernel.Get<LayerManager>().ActiveLayer.Node;
            var node2 = Service.ActiveKernel.Get<ICanvasLayerContainer>();
            var nn = node2.GetLayer() as CCLayer;
            if ( !IsPickerDisplayed )
            {
                nn.AddChild(Child);
                Child.ZOrder = 0;
                Child.Position = nn.BoundingBox.Center;
                ( Service.ActiveKernel.Get<ICanvasLayerContainer>().GetLayer() as CCLayer ).PauseListeners();
                IsPickerDisplayed = true;
            } else
            {
                Child.PrepareToClose();
                nn.RemoveChild(Child);
                IsPickerDisplayed = false;
                ( Service.ActiveKernel.Get<ICanvasLayerContainer>().GetLayer() as CCLayer ).ResumeListeners();
            }
            
        }

        private void ChangeToolToSimple( object sender , EventArgs e )
        {
            Service.ActiveKernel.Rebind<ITool>().To<SimpleCircle>();
        }

        private void ChangeToolToLine( object sender , EventArgs e )
        {
            Service.ActiveKernel.Rebind<ITool>().To<SimpleLine>();
        }
    }


}