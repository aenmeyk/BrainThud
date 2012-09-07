using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BrainThudTest.Tools
{
    public class TestExceptionList : IEnumerable<Exception>
    {
        private readonly IList<TestException> testExceptions = new List<TestException>();

        public void Add(Exception exception)
        {
            this.testExceptions.Add(new TestException(exception));
        }

        public void ThrowUnhandled()
        {
            foreach (var testException in this.testExceptions.Where(a => !a.IsHandled))
            {
                testException.IsHandled = true;
                throw (testException.Exception);
            }
        }

        public void HandleAll()
        {
            foreach (var testException in this.testExceptions.Where(a => !a.IsHandled))
            {
                testException.IsHandled = true;
            }
        }
        
        public IEnumerator<Exception> GetEnumerator()
        {
//            yield return this.testExceptions.GetEnumerator().Current.Exception;

            foreach(var testException in testExceptions)
            {
                yield return testException.Exception;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class TestException
        {
            public TestException(Exception exception)
            {
                this.Exception = exception;
            }

            public Exception Exception { get; private set; }
            public bool IsHandled { get; set; }
        }
    }
}