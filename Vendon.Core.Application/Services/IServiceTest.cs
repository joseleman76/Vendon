using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendon.Core.Application.Messages.Requests;
using Vendon.Core.Application.Messages.Responses;
using Vendon.Core.Repository.Models;

namespace Vendon.Core.Application.Services
{
    public interface IServiceTest
    {
        Task<IEnumerable<Test>> getTest(int testId);
        Task<IEnumerable<TestDefinition>> getAllTestDefinition();
        Task<RespResultTest> registerTest(ReqResultTests result);

    }
}
