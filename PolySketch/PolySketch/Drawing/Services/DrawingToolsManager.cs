using Ninject;
using Poly.NinjectModules.Kernels;
using PolySketch.Drawing.Brushes;
using PolySketch.Drawing.Rulers;
using PolySketch.Drawing.Tools;
using System;
using System.Collections.Generic;

namespace PolySketch.Drawing
{
    public class DrawingToolsManager
    {
        [Inject]
        public KernelService Service { get; set; }

        private Dictionary<String , Type> BrushTypes;
        private Dictionary<String , Type> RulerTypes;
        private Dictionary<String , Type> ToolTypes;

        public DrawingToolsManager()
        {
            //TODO: Update Available Brush Types
            BrushTypes = new Dictionary<string , Type>
            {
                { "pencil" , typeof(Pencil) }   ,
                {"technical", typeof(TechnicalBrush) },
                {"realistic", typeof(RealisticBrush) },
                {"custom", typeof(CustomBrush) },
                {"standart", typeof(StandartBrush) },
                { "eraser", typeof(Eraser) }
            };
            RulerTypes = new Dictionary<string , Type>
            {
                {"noruler", typeof(NoRuler) },
                {"standart", typeof(StandartRuler) }  ,
                {"parallel", typeof(ParallelRuler) } ,
                {"perspective", typeof(PerspectiveRuler) },
                {"onepoint" , typeof(OnePointPerspectiveRuler)},
                {"twopoint", typeof(TwoPointPerspectiveRuler) },
                {"threepoint", typeof(ThreePointPerspectiveRuler) }
            };
            ToolTypes = new Dictionary<string , Type>
            {
                {"simpleline", typeof(SimpleLine) },
                {"multiline", typeof(MultiLine) },
                {"polyline", typeof(PolyLine) } ,
                {"arc" , typeof(SimpleArc) },
                {"circle" , typeof(SimpleCircle)},
                {"polygon", typeof(PolygonTool) },
                {"spline", typeof(Spline) }
            };
        }

        public void SetBrush( string type )
        {
            if ( BrushTypes.ContainsKey(type.ToLower()) )
            {
                var bType = BrushTypes[type.ToLower()];
                Service.ActiveKernel.Rebind<IBrush>().To(bType).InSingletonScope();
            }
        }

        public void SetBrush( IBrush implementation )
        {
            Service.ActiveKernel.Rebind<IBrush>().ToConstant(implementation);
        }

        public void SetRuler( string type )
        {
            var lType = type.ToLower();
            if ( RulerTypes.ContainsKey(lType) )
            {
                Service.ActiveKernel.Rebind<IRuler>().To(RulerTypes[lType]).InSingletonScope();
            }
        }

        public void SetRuler( IRuler implementation )
        {
            Service.ActiveKernel.Rebind<IRuler>().ToConstant(implementation);
        }

        public void SetTool( string type )
        {
            var lType = type.ToLower();
            if ( ToolTypes.ContainsKey(lType) )
            {
                Service.ActiveKernel.Rebind<ITool>().To(ToolTypes[lType]).InTransientScope();
            }
        }

        public void SetTool( ITool implementation )
        {
            Service.ActiveKernel.Rebind<ITool>().ToConstant(implementation);
        }

        public List<string> GetBrushTypes()
        {
            return new List<string>(BrushTypes.Keys);
        }

        public List<string> GetRulerTypes()
        {
            return new List<string>(RulerTypes.Keys);
        }

        public List<string> GetToolTypes()
        {
            return new List<string>(ToolTypes.Keys);
        }
    }
}