using System;
using System.Runtime.Serialization;

namespace AttendanceSystem.StudentAttendance.Exceptions
{
    [Serializable]
    internal class DuplicateValueException : Exception
    {
        public DuplicateValueException()
        {
        }

        public DuplicateValueException(string message) : base(message)
        {
        }

        public DuplicateValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}