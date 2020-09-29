using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Dotnet.Template.IntegrationTests.Fake
{
    [ApiController]
    [Route("api/errors")]
    public class FakeErrorController : Controller
    {
        [HttpGet("400")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("401")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("404")]
        public IActionResult GetNotfound()
        {
            return NotFound();
        }

        [HttpGet("500")]
        public IActionResult GetInternalServerError()
        {
            throw new Exception();
        }
    }
}
