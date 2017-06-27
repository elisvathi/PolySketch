using Ninject;
using Poly.NinjectModules.Kernels;
using PolySketch.Drawing.Tools;
using PolySketch.UI.ControlHandlers;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PolySketch.UI.Tests.MenuTests
{
    public class MenuListData : List<MenuItem>
    {
        private Command ChangeToolToLine;
        private Command ChangeToolToPolygon;
        private KernelService Service;
        IKernel ActiveKernel => Service.ActiveKernel;
        List<string> BrushTypes => ActiveKernel.Get<BrushControlHandler>().BrushTypes;
        List<string> ToolTypes => ActiveKernel.Get<ToolControlHandler>().ToolTypes;
        List<string> RulerTypes => ActiveKernel.Get<RulerControlHandler>().RulerTypes;

        public MenuListData( KernelService service )
        {
            Service = service;
            ChangeToolToLine = new Command(ChangeToLIne);
            ChangeToolToPolygon = new Command(ChangeToPolygon);

            foreach ( var t in BrushTypes )
            {
                this.Add(new MenuItem()
                {
                    Text = t
                });
            }
            foreach ( var t in ToolTypes )
            {
                this.Add(new MenuItem()
                {
                    Text = t
                });
            }
            foreach ( var t in RulerTypes )
            {
                this.Add(new MenuItem()
                {
                    Text = t
                });
            }
        }

        private void ChangeToPolygon( object obj )
        {
            Service.ActiveKernel.Rebind<ITool>().To<PolygonTool>().InTransientScope();
        }

        private void ChangeToLIne( object obj )
        {
            Service.ActiveKernel.Rebind<ITool>().To<SimpleLine>().InTransientScope();
        }
    }
}