using System;
using Jucardi.Inject.Exceptions;
namespace jucardi.inject.Exceptions
{
    public class BeanCreationException : InjectException
    {
        public BeanCreationException(string msg, Exception innerException = null) : base(msg, innerException)
        {
        }
    }
}
