using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationApi.Application.Operations.Commands;
using ApplicationApi.Application.Operations.Queries;
using ApplicationApi.ViewModels.Operations;

namespace ApplicationApi.Controllers
{
    public class OperationsController : Utilities.ControllerBase
    {
        public OperationsController
            (MediatR.IMediator mediator) : base(mediator: mediator)
        {
        }

        /// <summary>  
        /// Get all operations
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>
        [HttpPost(template: "GetAll")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<IList<OperationViewModel>>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<IList<OperationViewModel>>> GetAll
            ([FromBody] GetAllOperationQuery request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Create new operation
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>
        [HttpPost]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<int>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<int>> Create
            ([FromBody] CreateOperationCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Edit operation
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>
        [HttpPut]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result> Edit
            ([FromBody] UpdateOperationCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Get operation group by id
        /// </summary>  
        /// <param name="operationId"></param>  
        /// <returns></returns>
        [HttpGet(template: "{operationId?}")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<OperationViewModel>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<OperationViewModel>> Get
            ([FromRoute] int? operationId)
        {
            var request =
                new GetOperaiotnByIdQuery
                {
                    Id = operationId,
                };

            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Assign to operation group
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>
        [HttpPost(template: "AssignToOperationGroup")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result> AssignToOperationGroup
            ([FromBody] AssignToOperationGroupCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Remove from operation group
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>
        [HttpPost(template: "RemoveFromOperationGroup")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result> RemoveFromOperationGroup
            ([FromBody] RemoveFromOperationGroupCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }
    }
}
