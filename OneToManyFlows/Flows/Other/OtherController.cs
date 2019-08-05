namespace OneToManyFlows.Flows.Other
{
    using System.Threading.Tasks;
    using Autofac.Features.Indexed;
    using Core;
    using Core.Abstractions;
    using Core.Enums;
    using Core.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Operations;

    [Route("[controller]")]
    public class OtherController : BaseController<OtherInputDto>
    {
        private readonly IIndex<Provider, IOtherOperation> _operations;

        public OtherController(IIndex<Provider, IOtherOperation> operations)
        {
            _operations = operations;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public override async Task<IActionResult> Execute([FromBody] OtherInputDto inputDto)
        {
            var baseResult = await base.Execute(inputDto);
            if (baseResult != null)
            {
                return baseResult;
            }

            if (!_operations.TryGetValue(HttpContext.GetProvider(), out var operation))
            {
                return BadRequest(Errors.OperationNotFound(nameof(IOtherOperation), HttpContext.GetProvider().ToString()));
            }

            var result = await operation.Execute(inputDto);
            if (result.IsError)
            {
                // Do something with exception: result.Error.Exception
                return BadRequest(result.Error.Message);
            }

            return Ok(result.Value);
        }
    }
}