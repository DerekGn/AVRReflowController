using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflowController
{

    [Serializable]
    public class ReflowControllerException : Exception
    {
        public ReflowControllerException() { }

        public ReflowControllerException(string message) : base(message) { }
        public ReflowControllerException(string message, Exception inner) : base(message, inner) { }
        protected ReflowControllerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
