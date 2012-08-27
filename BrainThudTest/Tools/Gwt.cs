using System;
using NUnit.Framework;

namespace BrainThudTest.Tools
{
    [TestFixture]
    public abstract class Gwt
    {
        private readonly TestExceptionList testExceptionList = new TestExceptionList();

        [TestFixtureSetUp]
        public void SubInitialize()
        {
            try
            {
                this.Given();
                this.When();
            }
            catch (Exception exception)
            {
                this.testExceptionList.Add(exception);
            }
        }

        public abstract void Given();
        public abstract void When();

        [TestFixtureTearDown]
        public void SubCleanup()
        {
            this.ThrowUnhandledExceptions();
        }

        protected void ThrowUnhandledExceptions()
        {
            this.testExceptionList.ThrowUnhandled();
        }
    }
}
