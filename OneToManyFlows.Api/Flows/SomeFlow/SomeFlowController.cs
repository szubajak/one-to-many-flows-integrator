using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneToManyFlows.Api.Core;

namespace OneToManyFlows.Api.Flows;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SomeFlowController(IServiceProvider services) : ControllerBase
{
    [HttpPost(Name = nameof(SomeFlowController))]
    [ProducesResponseType(typeof(SomeFlowResponseDto), StatusCodes.Status200OK)]
    [EndpointDescription("Dummy endpoint to produce collection of random ids")]
    public async Task<IActionResult> Execute([FromBody] SomeFlowRequestDto requestDto, CancellationToken cancellationToken)
    {
        var handler = services.GetKeyedService<ISomeFlowHandler>(requestDto.Provider);

        return handler == null
            ? BadRequest(ErrorMessages.OperationNotFound(nameof(ISomeFlowHandler), requestDto.Provider))
            : Ok(await handler!.Handle(requestDto, cancellationToken));
    }
}