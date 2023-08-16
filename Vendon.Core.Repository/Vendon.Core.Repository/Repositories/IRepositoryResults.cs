using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendon.Core.Repository.Models;

namespace Vendon.Core.Repository.Repositories
{
    public  interface IRepositoryResults
    {
        public Task<bool> registerResult(ResultTest? resultTest);
    }
}
