using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.Exceptions
{
    public class EmptyDeckException : Exception
    {
        public EmptyDeckException() : base("No cards left in the deck.")
        {
        }
        public EmptyDeckException(string message) : base(message)
        {
        }
        public EmptyDeckException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
