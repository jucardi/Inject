using System;

namespace jucardi.inject.Exceptions
{
    public class InjectStartupException : InjectException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Exceptions.InjectStartupException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InjectStartupException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
