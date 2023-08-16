using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Configuration;
using Vendon.Core.Application.Mappers;
using Vendon.Core.Application.Messages.Requests;
using Vendon.Core.Application.Services;
using Vendon.Core.Repository.Models;
using Vendon.Core.Repository.Repositories;

namespace Vendon.Core.ApplicationTest
{
    public class UnitTestApplicationWithoutBd
    {
        public IServiceTest _serviceTest;
        public IRepositoryTests _repositoryTest;
        public IRepositoryResults _repositoryResultsTest;
        public IMapperTest _mapper;

        [SetUp]
        public void Setup()
        {

            var repositoryTests = new Mock<IRepositoryTests>();
            var repositoryResultsTests = new Mock<IRepositoryResults>();

            List<TestDefinition> testsdefinition = new List<TestDefinition>();
            testsdefinition.Add(new TestDefinition() { name = "Test1", testId = 1 });
            testsdefinition.Add(new TestDefinition() { name = "Test2", testId = 2 });
            IEnumerable<TestDefinition> testsDefinitionsSetUp = testsdefinition;

            repositoryTests.Setup(x => x.getAllTestDefinition()).Returns(Task.FromResult<IEnumerable<TestDefinition>>(testsDefinitionsSetUp));

            _repositoryTest = (IRepositoryTests)repositoryTests.Object;
            _repositoryResultsTest = (IRepositoryResults)repositoryResultsTests.Object;
            _mapper = new MapperTest();
            _serviceTest = new ServiceTest(_repositoryTest, _repositoryResultsTest, _mapper);
        }

        [Test]
        public async Task TestOK_IsThereInfoOnBD()
        {
            var test = await _serviceTest.getAllTestDefinition();
            Assert.IsTrue(test?.Count() > 0);
        }
    }
}