using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCorePostgreSQL.Api.Models;
using DotnetCorePostgreSQL.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotnetCorePostgreSQL.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISampleService _sampleService;

        public HomeController(ILogger<HomeController> logger, ISampleService sampleService)
        {
            _logger = logger;
            _sampleService = sampleService;
        }

        [HttpGet("insertdata")]
        public async Task<IActionResult> InsertDataToDatabase()
        {
            var posts = await _sampleService.GetJsonDatasFromRemoteServer();

            if (posts == null)
            {
                return BadRequest("Error while receiving posts");
            }

            var saveResult = _sampleService.InsertDataToDatabase(posts);

            if (!saveResult.Result)
            {
                return BadRequest("Error while insert operation");
            }

            return Ok();
        }
    }
}
