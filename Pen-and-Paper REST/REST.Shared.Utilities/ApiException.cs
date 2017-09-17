using System;

namespace REST.Shared.Utilities
{
    public class ApiException : Exception
    {
        /// <summary>
        /// The status of the Http Response.
        /// </summary>
        public int HttpStatus {
            get { return _HttpStatus; }
            set { _HttpStatus = value; }
        }
        private int _HttpStatus;

        /// <summary>
        /// The body of the Http Response.
        /// </summary>
        public string HttpBody {
            get { return _HttpBody; }
            set { _HttpBody = value; }
        }
        private string _HttpBody;

        /// <summary>
        /// Generates a new HttpException which will be catched and send to the client by the listener.
        /// </summary>
        /// <param name="body">Http Body</param>
        /// <param name="status">Http Status</param>
        public ApiException(string body, int status)
        {
            HttpBody = body;
            HttpStatus = status;
        }

        /// <summary>
        /// Generates a new HttpException which will be catched and send to the client by the listener.
        /// </summary>
        /// <param name="status">Http Status</param>
        public ApiException(int status)
        {
            HttpStatus = status;
        }
    }
}
