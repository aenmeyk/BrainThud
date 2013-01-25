using BrainThud.Win.ViewModels;
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
            RequestNavigate<QuizSummary>();
        }

        public bool ApplicationCanOpenBookmarks
        {
            get { return false; }
        }
    }
}