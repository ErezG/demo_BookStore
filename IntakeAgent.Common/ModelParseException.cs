using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntakeAgent.Common
{
    public class ModelParseException : Exception
    {
        public ModelParseException() : base()
        { }
        public ModelParseException(string? message) : base(message)
        { }
    }
}
