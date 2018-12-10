using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangfireCoreExample.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangfireCoreExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        public IJobCreator JobCreator { get; }

        public JobsController(IJobCreator jobCreator)
        {
            JobCreator = jobCreator ?? throw new ArgumentNullException(nameof(jobCreator));
        }

        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            var jobId = JobCreator.NewFireAndForget(message);
            return Ok(jobId);
        }
    }
}