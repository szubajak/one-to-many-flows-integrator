using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OneToManyFlows.Api.Core;

namespace OneToManyFlows.Api.Flows.TestFlow;

[ApiController]
[Route("[controller]")]
public class TestFlowController(IOptions<EntraIdOptions> options) : ControllerBase
{
    readonly EntraIdOptions _entraIdOptions = options.Value;

    [HttpGet(Name = nameof(TestFlowController))]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [EndpointDescription("Dummy endpoint for testing purpouses")]
    public string Execute() => _entraIdOptions.Test ?? "Not Found";
}
