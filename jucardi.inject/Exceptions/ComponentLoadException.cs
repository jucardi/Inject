using System;
namespace Jucardi.Inject.Exceptions
{
    public class ComponentLoadException : InjectException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Exceptions.ComponentLoadException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ComponentLoadException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
