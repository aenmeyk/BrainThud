using Cirrious.MvvmCross.Console.Platform;
using Cirrious.MvvmCross.Core;
using Cirrious.MvvmCross.Interfaces.IoC;
using Cirrious.MvvmCross.Interfaces.Platform.Diagnostics;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.IoC;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Platform.Diagnostics;

namespace BrainThudTest
{
    public abstract class GwtMvvmCross : Gwt
    {
        protected IMvxIoCProvider IoC { get; private set; }

        public override void SubInitialize()
        {
            // Fake set up of the IoC
            MvxSingleton.ClearAllSingletons();
            this.IoC = new MvxSimpleIoCServiceProvider();
            var serviceProvider = new MvxServiceProvider(this.IoC);
            this.IoC.RegisterServiceInstance<IMvxServiceProviderRegistry>(serviceProvider);
            this.IoC.RegisterServiceInstance<IMvxServiceProvider>(serviceProvider);
            this.IoC.RegisterServiceInstance<IMvxTrace>(new MvxDebugTrace());
            MvxTrace.Initialize();
            base.SubInitialize();
        }
    }
}