using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Exceptions
{
    public class NoInternetConnectionException : System.Exception
    {
        public NoInternetConnectionException() : base() { }
        public NoInternetConnectionException(string message) : base(message) { }
        public NoInternetConnectionException(string message, System.Exception inner) : base(message, inner) { }
        
        protected NoInternetConnectionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
