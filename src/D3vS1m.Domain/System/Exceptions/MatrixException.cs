using System;
using System.Runtime.Serialization;

namespace D3vS1m.Domain.System.Exceptions
{
    /// <summary>
    /// The specific Exception type for operations with the Matrix class.
    /// </summary>
    [Serializable]
    public class MatrixException : Exception
    {
        public MatrixException()
        {
        }

        public MatrixException(string message) : base(message)
        {
        }

        public MatrixException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MatrixException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
