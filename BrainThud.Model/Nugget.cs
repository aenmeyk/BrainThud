using System;

namespace BrainThud.Model
{
    public class Nugget
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string SupplementalInformation { get; set; }
    }
}