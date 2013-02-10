using System.Collections.Generic;

namespace BrainThud.Core.Calendars
{
    public interface IQuizCalendar : IDictionary<int, int>
    {
        int GetQuizInterval(int cardLevel);
    }
}