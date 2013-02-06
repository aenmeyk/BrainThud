using BrainThud.Core.ViewModels;
using BrainThud.Win.Common;

namespace BrainThud.Win.Views
{
    public sealed partial class LoginView : LayoutAwarePage
    {
        public LoginView()
        {
            this.InitializeComponent();
        }

        public new LoginViewModel ViewModel
        {
            get { return (LoginViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
