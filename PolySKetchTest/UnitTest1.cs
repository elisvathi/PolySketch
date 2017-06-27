using Ninject;
using Poly.Geometry;
using Poly.NinjectModules;
using Poly.NinjectModules.Kernels;
using PolySketch.UI.Canvas.Interfaces;
using PolySketch.UI.Tests;
using Xunit;

namespace PolySKetchTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            IKernel kernel = new StandardKernel(new GlobalModule());
            var service = kernel.Get<KernelService>();
            service.GlobalKernel = kernel;
            service.AddLocalKernel(new StandardKernel(new LocalModule(ref service)));
            service.AddLocalKernel(new StandardKernel(new LocalModule(ref service)));

            var local1 = service.GetLocalKernel(0);
            var local2 = service.GetLocalKernel(1);

            var testObject = service.GlobalKernel.Get<TestClassCounter>();
            testObject.TestNumber = 10;
            Assert.Equal(local1.Get<KernelService>().GlobalKernel.Get<TestClassCounter>().TestNumber , local2.Get<KernelService>().GlobalKernel.Get<TestClassCounter>().TestNumber);
            testObject.TestNumber = 22;
            Assert.Equal(local2.Get<KernelService>().GlobalKernel.Get<TestClassCounter>().TestNumber , 22);
        }

        [Fact]
        public void TestingCocosBuild()
        {
            IKernel kernel = new StandardKernel(new GlobalModule());
            var service = kernel.Get<KernelService>();
            service.AddLocalKernel(new StandardKernel(new LocalModule(ref service)));
            var local = service.ActiveKernel;
            var temp = local.Get<IPaintCanvasScene>();
            var a = 1;
        }
        [Fact]
        public void vectorTest() {
            PVector v = new PVector(0 , 1);
            v.RotateVector(90);
            Assert.Equal(v.X , 1);

        }
    }
}