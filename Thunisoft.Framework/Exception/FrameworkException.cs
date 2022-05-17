using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunisoft.Framework
{
    public class FrameworkException : Exception
    {
        public ExceptionLevel ExceptionLevel
        {
            get;
            set;
        }
        public FrameworkException(string aMessage, ExceptionLevel anExceptionLevel = ExceptionLevel.Slight):base(aMessage)
        {
            ExceptionLevel = anExceptionLevel;
        }
        public FrameworkException(string aMessage, Exception anInnerException, ExceptionLevel anExceptionLevel = ExceptionLevel.Slight) : base(aMessage, anInnerException)
        {
            ExceptionLevel = anExceptionLevel;
        }
    }
}
