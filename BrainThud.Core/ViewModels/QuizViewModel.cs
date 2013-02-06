using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using BrainThud.Core.Models;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.ExtensionMethods;
using BrainThud.Core.AzureServices;
using Newtonsoft.Json;
using System.Net.Http;

namespace BrainThud.Core.ViewModels
{
    public class QuizViewModel : ViewModelBase
    {
        private IAccessControlService accessControlService;

        public QuizViewModel()
        {
            var card = new Card { Question = "This value was set on the card" };

            this.Title = card.Question;
            this.CardCount = 10;
            this.CardsCompleted = 5;
            this.CardsCorrect = 3;
            this.CardsIncorrect = 2;
            this.QuizDate = DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
            this.accessControlService = this.GetService<IAccessControlService>();

            //            var result = accessControlService.GetIdentityProviders().Result;
            //            this.Title = result.Select(x => x.Name).First();

            //            var httpClient = new HttpClient();
            //            var response = httpClient.GetAsync(Urls.IDENTITY_PROVIDERS).Result;
            //            var json = response.Content.ReadAsStringAsync().Result;
            //            var result = JsonConvert.DeserializeObject<IEnumerable<IdentityProvider>>(json);

        }

        public async void LoadData()
        {
            var result = await accessControlService.GetIdentityProviders();
            this.Title = result.Select(x => x.Name).First();
        }

        private string title;
        public string Title
        {
            get { return this.title; }
            set
            {
                this.title = value;
                this.RaisePropertyChanged("Title");
            }
        }

        private string quizDate;
        public string QuizDate
        {
            get { return this.quizDate; }
            set
            {
                this.quizDate = value;
                this.RaisePropertyChanged("QuizDate");
            }
        }

        private int cardCount;
        public int CardCount
        {
            get { return this.cardCount; }
            set
            {
                this.cardCount = value;
                this.RaisePropertyChanged("CardCount");
                this.RaisePropertyChanged("ProgressMessage");
            }
        }

        private int cardsCompleted;
        public int CardsCompleted
        {
            get { return this.cardsCompleted; }
            set
            {
                this.cardsCompleted = value;
                this.RaisePropertyChanged("CardsCompleted");
                this.RaisePropertyChanged("ProgressMessage");
            }
        }

        private int cardsCorrect;
        public int CardsCorrect
        {
            get { return this.cardsCorrect; }
            set
            {
                this.cardsCorrect = value;
                this.RaisePropertyChanged("CardsCorrect");
                this.RaisePropertyChanged("CorrectMessage");
            }
        }

        private int cardsIncorrect;

        public int CardsIncorrect
        {
            get { return this.cardsIncorrect; }
            set
            {
                this.cardsIncorrect = value;
                this.RaisePropertyChanged("CardsIncorrect");
                this.RaisePropertyChanged("IncorrectMessage");
            }
        }

        public string ProgressMessage
        {
            get { return string.Format("{0} of {1} cards completed", this.CardsCompleted, this.CardCount); }
        }

        public string CorrectMessage
        {
            get { return string.Format("{0} correct", this.CardsCorrect); }
        }

        public string IncorrectMessage
        {
            get { return string.Format("{0} incorrect", this.CardsIncorrect); }
        }

        public ObservableCollection<string> Items
        {
            get
            {
                return new ObservableCollection<string> { "One", "Two", "Three", "One", "Two", "Three", "One", "Two", "Three", "One", "Two", "Three", "One", "Two", "Three" };
            }
        }
    }
}