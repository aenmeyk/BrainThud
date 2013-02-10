using System.Collections.Generic;
using System.Linq;

namespace BrainThud.Core.Calendars
{
    public class QuizCalendar : Dictionary<int, int>, IQuizCalendar
    {
        public int GetQuizInterval(int cardLevel)
        {
            if (cardLevel < 0) return this[0];
            if (cardLevel >= this.Count) return this.Last().Value;
            return this[cardLevel];
        }
    }
}