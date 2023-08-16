using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Vendon.Core.Application.Mappers;
using Vendon.Core.Application.Messages.Requests;
using Vendon.Core.Application.Services;
using Vendon.Core.Repository.Models;
using Vendon.Core.Repository.Repositories;

namespace Vendon.Core.Test.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TestsController : ControllerBase
    {
       
        private readonly ILogger<TestsController> _logger;
        private readonly IRepositoryTests _repositoryTests;
        private readonly IServiceTest _serviceTest;
        private readonly IMapperTest _mapperTest;

        public TestsController(ILogger<TestsController> logger, IRepositoryTests repositoryTests, IServiceTest serviceTest, IMapperTest mapperTest)
        {
            _logger = logger;
            _repositoryTests = repositoryTests;
            _serviceTest = serviceTest;
            _mapperTest = mapperTest;

        }

        /// <summary>
        /// Get Names and ids of tests to fill the the initial page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllTestsDefinition")]
        public async Task<IActionResult> GetAllTestsDefinition()
        {
            try
            {

                var result = await _serviceTest.getAllTestDefinition();
                if (result?.Count() > 0)
                {
                    return Ok(await _serviceTest.getAllTestDefinition());
                }
                else
                {
                    return NotFound(new Object() { });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500); 
            }
        }

        /// <summary>
        /// Get Answers and possible responses froma determinated Test
        /// </summary>
        /// <param name="testId">Defines de testId to recover.</param>
        /// <returns> If test not exits response is 404</returns>
        [HttpGet]
        [Route("GetTest")]
        public async Task<IActionResult> GetTest(int testId)
        {
            try
            {
                var result = await _serviceTest.getTest(testId);
                if (result?.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }

        }
        /// <summary>
        /// Controller used to Register 
        /// </summary>
        /// <param name="req"> Get Object with name and responses sent by FrontEnd</param>
        /// <returns>Returns a object with the evaluation of tests</returns>
        [HttpPost]
        [Route("RegisterTest")]
        public async Task<IActionResult> GetTest(ReqResultTests req)
        {
            try
            {
                var response = await _serviceTest.registerTest(req);
                if (response == null)
                {
                    return BadRequest();
                }
                return Ok(response);
            }
            catch (ValidationException e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }
        }
    }
}