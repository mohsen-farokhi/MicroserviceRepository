using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Utilities
{
	[Route("[controller]")]
	[ApiController]
	public abstract class ControllerBase : 
		Microsoft.AspNetCore.Mvc.ControllerBase
	{
		protected ControllerBase(MediatR.IMediator mediator)
		{
			Mediator = mediator;
		}

		protected MediatR.IMediator Mediator { get; }

		protected IActionResult
            FluentResult<T>(Framework.Result<T> result)
		{
			if (result.IsSuccess)
			{
				return Ok(value: result);
			}
			else
			{
				return BadRequest(error: result.ToResult());
			}
		}
	}
}
