using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exception = System.Exception;
using System.Runtime.Serialization;

namespace SGM.Common.Exception
{
    public class BaseException:System.ApplicationException
    {        
        
		/// <summary>
		/// Default constructor
		/// </summary>
		public BaseException() : base() 
		{            
		}

		/// <summary>
		/// Initializes with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public BaseException(string message) : base(message) 
		{
		}

		/// <summary>
		/// Initializes with a specified error 
		/// message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.
		/// </param>
		/// <param name="exception">The exception that is the cause of the current exception. 
		/// If the innerException parameter is not a null reference, the current exception 
		/// is raised in a catch block that handles the inner exception.
		/// </param>
		public BaseException(string message, System.Exception exception) : 
			base(message, exception) 
		{
		}

		/// <summary>
		/// Initializes with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.
		/// </param>
        protected BaseException(SerializationInfo info, StreamingContext context) :
			base(info, context) 
		{
		} 
    }
}
