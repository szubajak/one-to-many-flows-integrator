namespace OneToManyFlows.Core.Abstractions
{
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public abstract class BaseController<T> : ControllerBase
        where T : BaseInputDto
    {
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Execute([FromBody] T inputDto)
        {
            if (!HttpContext.TrySetProvider(inputDto?.ProviderId))
            {
                return await Task.FromResult(BadRequest(Errors.ProviderNotFound(inputDto?.ProviderId.ToString())));
            }

            return null;
        }
    }
}