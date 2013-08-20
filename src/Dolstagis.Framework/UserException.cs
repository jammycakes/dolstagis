using System;
using System.Runtime.Serialization;

namespace Dolstagis.Framework
{
    /// <summary>
    ///  Indicates an error condition that can conclusively be identified as
    ///  resulting from bad user input.
    /// </summary>

    [Serializable]
    public class UserException : Exception
    {
        /// <summary>
        ///  Creates a new instance of the <see cref="UserException"/>.
        /// </summary>
        public UserException()
            : base()
        { }

        /// <summary>
        ///  Creates a new instance of the <see cref="UserException"/> with a given message.
        /// </summary>
        /// <param name="message">
        ///  The exception message. This will be displayed to the user, so don't include
        ///  anything that could compromise security.
        /// </param>
        public UserException(string message)
            : base(message)
        { }

        /// <summary>
        ///  Creates a new instance of the <see cref="UserException"/> with a given message
        ///  and inner exception.
        /// </summary>
        /// <param name="message">
        ///  The exception message. This will be displayed to the user, so don't include
        ///  anything that could compromise security.
        /// </param>
        /// <param name="innerException">
        ///  The exception that is the cause of the current exception, or null if no inner
        ///  exception is specified.
        /// </param>

        public UserException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        ///  Creates a new instance of the <see cref="UserException"/> from serialization data.
        /// </summary>
        /// <param name="info">
        ///  The <see cref="SerializationInfo"/> instance that holds the data about the exception
        ///  being serialized.
        /// </param>
        /// <param name="context">
        ///  The <see cref="StreamingContext"/> instance that contains contextual information
        ///  about the source or destination.
        /// </param>

        protected UserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
