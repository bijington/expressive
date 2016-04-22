using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Exceptions
{
    public class ParameterCountMismatchException : Exception
    {
        public ParameterCountMismatchException(string message)
            : base(message)
        {

        }
    }
}
