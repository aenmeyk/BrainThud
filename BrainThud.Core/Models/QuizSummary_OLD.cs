//using System;
//using System.Collections.ObjectModel;
//using System.Globalization;
//using BrainThud.Win.Common;
//
//namespace BrainThud.Win.ViewModels
//{
//    public class QuizSummary : BindableBase
//    {
//        public QuizSummary()
//        {
//            this.Title = "Today's Quiz";
//            this.CardCount = 10;
//            this.CardsCompleted = 5;
//            this.CardsCorrect = 3;
//            this.CardsIncorrect = 2;
//            this.QuizDate = DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern);;
//        }
//
//        private string title;
//        public string Title
//        {
//            get { return this.title; }
//            set { this.SetProperty(ref this.title, value); }
//        }
//
//        private string quizDate;
//        public string QuizDate
//        {
//            get { return this.quizDate; }
//            set { this.SetProperty(ref this.quizDate, value); }
//        }
//
//        private int cardCount;
//        public int CardCount
//        {
//            get { return this.cardCount; }
//            set
//            {
//                this.SetProperty(ref this.cardCount, value);
//                this.OnPropertyChanged("ProgressMessage");
//            }
//        }
//
//        private int cardsCompleted;
//        public int CardsCompleted
//        {
//            get { return this.cardsCompleted; }
//            set
//            {
//                this.SetProperty(ref this.cardsCompleted, value);
//                this.OnPropertyChanged("ProgressMessage");
//            }
//        }
//
//        private int cardsCorrect;
//        public int CardsCorrect
//        {
//            get { return this.cardsCorrect; }
//            set
//            {
//                this.SetProperty(ref this.cardsCorrect, value);
//                this.OnPropertyChanged("CorrectMessage");
//            }
//        }
//
//        private int cardsIncorrect;
//        public int CardsIncorrect
//        {
//            get { return this.cardsIncorrect; }
//            set
//            {
//                this.SetProperty(ref this.cardsIncorrect, value);
//                this.OnPropertyChanged("IncorrectMessage");
//            }
//        }
//
//        public string ProgressMessage
//        {
//            get { return string.Format("{0} of {1} cards completed", this.CardsCompleted, this.CardCount); }
//        }
//
//        public string CorrectMessage
//        {
//            get { return string.Format("{0} correct", this.CardsCorrect); }
//        }
//
//        public string IncorrectMessage
//        {
//            get { return string.Format("{0} incorrect", this.CardsIncorrect); }
//        }
//
//        public ObservableCollection<string> Items
//        {
//            get 
//            {
//                return new ObservableCollection<string> { "One", "Two", "Three", "One", "Two", "Three", "One", "Two", "Three", "One", "Two", "Three", "One", "Two", "Three" }; 
//            }
//        } 
//    }
//}
