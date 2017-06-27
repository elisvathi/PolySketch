using Ninject;
using System.Collections.Generic;

namespace Poly.NinjectModules.Kernels
{
    public class KernelService
    {
        public delegate void AddFileDelegate( KernelService serv );

        public event AddFileDelegate AddFile;

        public virtual void OnAddFile()
        {
            AddFile?.Invoke(this);
        }

        internal void SetActive( int actual )
        {
            ActiveKernelIndex = actual;
        }

        private IKernel _GlobalKernel;
        public IKernel GlobalKernel { get { return _GlobalKernel; } set { _GlobalKernel = value; } }
        private List<IKernel> LocalKernels;

        private int ActiveKernelIndex;
        public IKernel ActiveKernel => ActiveKernelIndex >= 0 ? LocalKernels[ActiveKernelIndex] : null;

        public IKernel GetLocalKernel( int n )
        {
            return LocalKernels[n];
        }

        public KernelService()
        {
            LocalKernels = new List<IKernel>();
            ActiveKernelIndex = -1;
        }

        public void AddLocalKernel( IKernel kernel )
        {
            LocalKernels.Add(kernel);
            ActiveKernelIndex = LocalKernels.Count - 1;
        }

        public int NumKernels { get { return LocalKernels.Count; } }
    }
}