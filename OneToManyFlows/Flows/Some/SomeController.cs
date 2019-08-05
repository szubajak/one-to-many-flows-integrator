namespace OneToManyFlows.Flows.Some
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
    public class SomeController : BaseController<SomeInputDto>
    {
        private readonly IIndex<Provider, ISomeOperation> _operations;

        public SomeController(IIndex<Provider, ISomeOperation> operations)
        {
            _operations = operations;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public override async Task<IActionResult> Execute([FromBody] SomeInputDto inputDto)
        {
            var baseResult = await base.Execute(inputDto);
            if (baseResult != null)
            {
                return baseResult;
            }

            if (!_operations.TryGetValue(HttpContext.GetProvider(), out var operation))
            {
                return BadRequest(Errors.OperationNotFound(nameof(ISomeOperation), HttpContext.GetProvider().ToString()));
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