using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendon.Core.Application.Messages.Requests;

namespace Vendon.Core.Application.Mappers
{
    public interface IMapperTest
    {
        bool validateRequest(ReqResultTests results);
    }
}
