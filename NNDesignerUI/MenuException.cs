using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NNDesignerUI
{
    class MenuException : Exception
    {
        public MenuException()
        {
        }

        public MenuException(string message) : base(message)
        {
        }

        public MenuException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MenuException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
