using System;

namespace Jucardi.Inject.Exceptions
{
    public class ResolveErrorException : InjectException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Exceptions.ResolveErrorException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ResolveErrorException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
