using System;

namespace Jucardi.Inject.Exceptions
{
    public class InjectException : Exception
    {
        /// <summary>
        /// Base exception for jucardi.inject.
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Exceptions.InjectException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InjectException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
