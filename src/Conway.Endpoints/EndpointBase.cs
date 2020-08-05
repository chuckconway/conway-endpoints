using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Conway.Endpoints
{
    /// <summary>
    /// Use when we know the request type and response type and don't need flexibility in the response type.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    [ApiController]
    public abstract class EndpointBase<TRequest, TResponse> : ControllerBase
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(TRequest request);
    }

    /// <summary>
    /// Use when we don't have a request payload.
    /// </summary>
    [ApiController]
    public abstract class EndpointBase: ControllerBase
    {
        public abstract Task<IActionResult> HandleAsync();
    }
    
    /// <summary>
    /// Use when we know we know the request type, but need flexibility on the response type.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    [ApiController]
    public abstract class EndpointBase<TRequest> : ControllerBase
    {
        public abstract Task<IActionResult> HandleAsync(TRequest request);
    }
}