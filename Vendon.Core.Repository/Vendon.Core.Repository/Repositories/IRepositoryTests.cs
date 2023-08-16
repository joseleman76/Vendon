using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendon.Core.Repository.Models;

namespace Vendon.Core.Repository.Repositories
{
    public interface IRepositoryTests
    {
        Task<IEnumerable<Test>> getAllTest();
        Task<IEnumerable<Test>> getTest(int testId);
        Task<IEnumerable<TestDefinition>> getAllTestDefinition();
    }
}
