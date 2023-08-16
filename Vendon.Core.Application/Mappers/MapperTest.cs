using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendon.Core.Application.Exceptions;
using Vendon.Core.Application.Messages.Requests;

namespace Vendon.Core.Application.Mappers
{
    public class MapperTest:IMapperTest
    {
        public bool validateRequest(ReqResultTests results)
        {
            if (string.IsNullOrEmpty(results?.name) || results.testId<1 || results?.responses?.Count==0) throw new ValidationException("Error validating request");
            return true;
        }
    }
}
