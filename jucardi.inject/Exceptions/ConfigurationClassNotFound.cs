using System;

namespace jucardi.inject.Exceptions
{
    public class ConfigurationClassNotFound : InjectException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Exceptions.ConfigurationClassNotFound"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ConfigurationClassNotFound(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
