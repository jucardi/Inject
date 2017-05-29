using System;

namespace Jucardi.Inject.Exceptions
{
    public class BeanNotFoundException : InjectException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Exceptions.BeanNotFoundException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public BeanNotFoundException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
