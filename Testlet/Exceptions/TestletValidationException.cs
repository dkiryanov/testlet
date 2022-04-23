using System;

namespace Testlet.Exceptions
{
    public class TestletValidationException : Exception
    {
        public TestletValidationException()
        {
        }

        public TestletValidationException(string message): base(message)
        {
        }
    }
}