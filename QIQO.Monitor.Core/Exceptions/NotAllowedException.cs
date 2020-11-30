using System;
using System.Runtime.Serialization;

namespace QIQO.Monitor.Core
{
    [Serializable]
    public class NotAllowedException : Exception
    {
        public NotAllowedException()
        {
        }

        public NotAllowedException(string message) : base(message)
        {
        }

        public NotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}