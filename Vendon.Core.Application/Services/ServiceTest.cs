using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendon.Core.Application.Mappers;
using Vendon.Core.Application.Messages.Requests;
using Vendon.Core.Application.Messages.Responses;
using Vendon.Core.Repository.Models;
using Vendon.Core.Repository.Repositories;

namespace Vendon.Core.Application.Services
{
    public class ServiceTest : IServiceTest
    {
        private readonly IRepositoryTests _repositoryTests;
        private readonly IRepositoryResults _repositoryResults;
        private readonly IMapperTest _mapperTest;

        public ServiceTest(IRepositoryTests repositoryTests, IRepositoryResults repositoryResults, IMapperTest mapper)
        {
            _repositoryTests = repositoryTests;
            _repositoryResults = repositoryResults;
            _mapperTest= mapper;
        }

        public async Task<IEnumerable<Test>> getTest(int testId)
        {
            return await _repositoryTests.getTest(testId);
        }
        public async Task<IEnumerable<TestDefinition>> getAllTestDefinition()
        {
            return await _repositoryTests.getAllTestDefinition();
        }

        public async Task<RespResultTest> registerTest(ReqResultTests result)
        {
            var validation = _mapperTest.validateRequest(result);
            
            RespResultTest toReturn = new RespResultTest() { name = "Empty", numAnswers = 0, numResponsesOk = 0 };
            var test = await _repositoryTests.getTest(result.testId);
            if (test?.Count() > 0)
            {
                ResultTest resultTestToRegister = new ResultTest();
                resultTestToRegister.id = Guid.NewGuid().ToString();
                resultTestToRegister.name = result.name;
                resultTestToRegister.testId = result.testId;
                resultTestToRegister.reponsesOk = new List<int>();
                Test myTest = test.ElementAt(0);
                resultTestToRegister.answersCount = myTest?.answers?.Count();

                List<Answer>? myanswers = myTest?.answers;
                if (myanswers != null)
                {
                    for (int i = 0; i < myanswers?.Count(); i++)
                    {
                        if (myanswers?.Count() > i && result?.responses?.Count > i)
                        {
                            if (myanswers[i]?.correctResponse == result?.responses[i])
                            {
                                resultTestToRegister?.reponsesOk?.Add(i);
                            }
                        }
                    }
                }
                var responseToRet = await _repositoryResults.registerResult(resultTestToRegister);

                if (responseToRet == true)
                {
                    toReturn = new RespResultTest() { name = myTest?.name ?? "Empty", numAnswers = myTest?.answers?.Count() ?? 0, numResponsesOk = resultTestToRegister?.reponsesOk?.Count() ?? 0 };
                }
            }
            return toReturn;
        }


    }
}
