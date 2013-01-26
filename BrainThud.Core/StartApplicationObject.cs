using BrainThud.Core.ViewModels;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.ViewModels;

namespace BrainThud.Core
{
    public class StartApplicationObject
        : MvxApplicationObject
        , IMvxStartNavigation
    {
        public void Start()
        {
            RequestNavigate<QuizViewModel>();
        }

        public bool ApplicationCanOpenBookmarks
        {
            get { return false; }
        }
    }
}