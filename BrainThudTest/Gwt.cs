using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest
{
    [TestFixture]
    public abstract class Gwt
    {
        private readonly TestExceptionList testExceptionList = new TestExceptionList();

        [SetUp]
        public virtual void SubInitialize()
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

        [TearDown]
        public void SubCleanup()
        {
            this.ThrowUnhandledExceptions();
        }

        protected void ThrowUnhandledExceptions()
        {
            this.testExceptionList.ThrowUnhandled();
        }

        protected void ShouldThrowException<T>(string message)
        {
            var exception = this.testExceptionList.Single();
            exception.Should().BeOfType<T>();
            exception.Message.Should().Be(message);

            this.testExceptionList.HandleAll();
        }
    }
}
