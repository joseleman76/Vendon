using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendon.Core.Application.Exceptions
{
    public class ValidationException:Exception
    {
        public ValidationException()
        {
        }
        public ValidationException(string message)
        : base(message)
        {
        }
    }
}
