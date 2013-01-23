using BrainThud.Win.Common;

namespace BrainThud.Win.ViewModels
{
    public class QuizViewModel : BindableBase
    {
        private QuizSummary quizSummary;
        public QuizSummary QuizSummary
        {
            get { return this.quizSummary; }
            set { this.SetProperty(ref this.quizSummary, value); }
        }
    }
}