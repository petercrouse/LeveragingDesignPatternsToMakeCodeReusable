using System;
using System.Runtime.Serialization;

namespace App.Common.Exceptions
{
    [Serializable]
    internal class InvalidStateModelException : Exception
    {
        public InvalidStateModelException()
        {
        }

        public InvalidStateModelException(string stateModel) 
            : base($"The statemodel is not of type {stateModel}")
        {
        }

        public InvalidStateModelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidStateModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}