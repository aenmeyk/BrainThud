using BrainThudTest.Extensions;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.ModelTest.NuggetTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_Nugget
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_Id_should_get_and_set_a_Guid()
        {
            this.Nugget.CanGetSetValue(x => x.Id, TestValues.GUID);
        }

        [Test]
        public void Then_Question_should_get_and_set_a_string()
        {
            this.Nugget.CanGetSetValue(x => x.Question, TestValues.STRING);
        }

        [Test]
        public void Then_Answer_should_get_and_set_a_string()
        {
            this.Nugget.CanGetSetValue(x => x.Answer, TestValues.STRING);
        }

        [Test]
        public void Then_SupplementalInformation_should_get_and_set_a_string()
        {
            this.Nugget.CanGetSetValue(x => x.SupplementalInformation, TestValues.STRING );
        }
    }
}