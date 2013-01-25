using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Model
{
    public class UserConfiguration : AzureTableEntity 
    {
        public int UserId { get; set; }
        public int QuizInterval0 { get; set; }
        public int QuizInterval1 { get; set; }
        public int QuizInterval2 { get; set; }
        public int QuizInterval3 { get; set; }
        public int QuizInterval4 { get; set; }
        public int QuizInterval5 { get; set; }
    }
}