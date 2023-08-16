using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Configuration;
using Vendon.Core.Application.Mappers;
using Vendon.Core.Application.Messages.Requests;
using Vendon.Core.Application.Services;
using Vendon.Core.Repository.Repositories;

namespace Vendon.Core.ApplicationTest
{
    public class UnitTestApplicationWithBd
    {
        public IServiceTest _serviceTest;
        public IRepositoryTests _repositoryTest;
        public IRepositoryResults _repositoryResultsTest;
        public IMapperTest _mapper;

        [SetUp]
        public void Setup()
        {
            var configuration = new Mock<IConfiguration>();

            configuration.SetupGet(m => m[It.Is<string>(s => s == "Settings:DataBaseId")]).Returns("DataBasePruebas");
            configuration.SetupGet(m => m[It.Is<string>(s => s == "Settings:CollectionId")]).Returns("Tests");
            configuration.SetupGet(m => m[It.Is<string>(s => s == "Settings:CollectionResultsId")]).Returns("Results");

            _mapper = new MapperTest();

            var cosmosClient = new CosmosClient(
                        "https://cosmosjosele.documents.azure.com:443/",
                        "hCb6lLjzNJF0oTy6K20LUWlA99w0lzciiNTnyKZlCaXyxvfDfcU5K01pGLnaUIDjuyiPjujCGKFgACDbTRDW6A=="
                    );
            _repositoryTest = new RepositoryTests(cosmosClient, configuration.Object);
            _repositoryResultsTest = new RepositoryResults(cosmosClient, configuration.Object);
            _serviceTest = new ServiceTest(_repositoryTest, _repositoryResultsTest, _mapper);
        }

        [Test]
        public async Task TestOK_IsThereInfoOnBD()
        {
            var test = await _serviceTest.getAllTestDefinition();
            Assert.IsTrue(test?.Count() > 0);
        }

        [Test]
        public async Task TestOK_Test1Correct()
        {
            List<int> responses = new List<int>();
            responses.Add(1);
            responses.Add(2);

            ReqResultTests resultToSave = new ReqResultTests() { name = "José Ignacio", testId = 1, responses = responses };

            var resultRegister = await _serviceTest.registerTest(resultToSave);

            Assert.IsTrue(resultRegister?.numAnswers == resultRegister?.numResponsesOk);
        }

        [Test]
        public async Task TestKO_Test1InCorrect()
        {
            List<int> responses = new List<int>();
            responses.Add(0);
            responses.Add(0);
            ReqResultTests resultToSave = new ReqResultTests() { name = "José Ignacio", testId = 1, responses = responses };
            var resultRegister = await _serviceTest.registerTest(resultToSave);
            Assert.IsTrue(resultRegister?.numAnswers != resultRegister?.numResponsesOk);
        }

    }
}