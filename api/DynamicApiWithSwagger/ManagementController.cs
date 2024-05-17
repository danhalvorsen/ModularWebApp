using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NSDynamicApiManager;
using System.Collections.Generic;

namespace DynamicApiWithSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly DynamicApiManager _apiManager;

        public ManagementController(DynamicApiManager apiManager)
        {
            _apiManager = apiManager;
        }

        [HttpPost("add-api")]
        public IActionResult AddApi([FromQuery] string name, [FromBody] ApiDefinition apiDefinition)
        {
            var implementation = new Func<IDictionary<string, object>, object>(parameters =>
            {
                return new { Message = $"Executed dynamic API '{name}'", Parameters = parameters };
            });

            var operation = new OpenApiOperation
            {
                Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Dynamic" } },
                Summary = apiDefinition.Summary,
                Description = apiDefinition.Description,
                OperationId = $"Dynamic_{name}",
                Responses = new OpenApiResponses
                {
                    { "200", new OpenApiResponse { Description = "Success" } }
                }
            };

            _apiManager.AddApi(name, implementation, operation);
            return Ok(new { Message = $"API '{name}' added successfully." });
        }

        [HttpGet("get-apis")]
        public IActionResult GetApis()
        {
            return Ok(_apiManager.GetAllApis().Keys);
        }
    }

    public class ApiDefinition
    {
        public required string Summary { get; set; }
        public required string Description { get; set; }
    }
}

