using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.WinRT.Platform;
using Windows.UI.Xaml.Controls;

namespace BrainThud.WinRT
{
    public class Setup
        : MvxBaseWinRTSetup
    {
        public Setup(Frame rootFrame)
            : base(rootFrame)
        {
        }

        protected override MvxApplication CreateApp()
        {
            var app = new Core.App();
            return app;
        }
    }
}