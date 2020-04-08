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
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        [HttpGet("insertdata")]
        public async Task<IActionResult> InsertDataToDatabase()
        {
            var posts = await _postService.GetJsonDatasFromRemoteServer();

            if (posts == null)
            {
                return BadRequest("Error while receiving posts");
            }

            var saveResult = _postService.InsertDataToDatabase(posts);

            if (!saveResult.Result)
            {
                return BadRequest("Error while insert operation");
            }

            return Ok();
        }

        [HttpGet("getdata")]
        public async Task<IActionResult> GetDataFromPost()
        {
            var posts = await _postService.GetData();

            if (posts == null)
            {
                return BadRequest("Error while receiving posts");
            }

            return Ok(posts);
        }

        [HttpGet("getcount")]
        public async Task<IActionResult> GetPostTableCount()
        {
            var dataCount = await _postService.GetCount();

            return Ok(dataCount);
        }
    }
}
