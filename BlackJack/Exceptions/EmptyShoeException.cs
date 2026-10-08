using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.Exceptions
{
    public class EmptyShoeException : Exception
    {
        public EmptyShoeException() : base("The shoe is empty. No more cards can be drawn.")
        {
        }
        public EmptyShoeException(string message) : base(message)
        {
        }
        public EmptyShoeException(string message, Exception innerException) : base(message, innerException)
        {
        }
}
