using Microsoft.AspNetCore.Mvc;
using OneToManyFlows.Api.Core;

namespace OneToManyFlows.Api.Flows;

[ApiController]
[Route("[controller]")]
public class OtherFlowController(IServiceProvider services) : ControllerBase
{
    [HttpPost(Name = nameof(OtherFlowController))]
    [ProducesResponseType(typeof(OtherFlowResponseDto), StatusCodes.Status200OK)]
    [EndpointDescription("Dummy endpoint to produce random string")]
    public async Task<IActionResult> Execute([FromBody] OtherFlowRequestDto requestDto, CancellationToken cancellationToken)
    {
        var handler = services.GetKeyedService<IOtherFlowHandler>(requestDto.Provider);

        return handler == null
            ? BadRequest(ErrorMessages.OperationNotFound(nameof(ISomeFlowHandler), requestDto.Provider))
            : Ok(await handler!.Handle(requestDto, cancellationToken));
    }
}