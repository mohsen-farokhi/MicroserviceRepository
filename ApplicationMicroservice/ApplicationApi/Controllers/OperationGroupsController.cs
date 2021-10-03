using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationApi.Application.OperationGroups.Commands;
using ApplicationApi.Application.OperationGroups.Queries;
using ApplicationApi.ViewModels.OperationGroups;

namespace ApplicationApi.Controllers
{
    public class OperationGroupsController : Utilities.ControllerBase
    {
        public OperationGroupsController
            (MediatR.IMediator mediator) : base(mediator: mediator)
        {
        }

        /// <summary>  
        /// Get all operation groups
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>
        [HttpPost(template: "GetAll")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<IList<OperationGroupViewModel>>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<IList<OperationGroupViewModel>>> GetAll
            ([FromBody] GetAllOperationGroupQuery request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Create new operation group
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
            ([FromBody] CreateOperationGroupCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Edit operation group
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
            ([FromBody] UpdateOperationGroupCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Get operation group by id
        /// </summary>  
        /// <param name="operationGroupId"></param>  
        /// <returns></returns>
        [HttpGet(template: "{operationGroupId?}")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<OperationGroupViewModel>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<OperationGroupViewModel>> Get
            ([FromRoute] int? operationGroupId)
        {
            var request =
                new GetOperaiotnGroupByIdQuery
                {
                    Id = operationGroupId,
                };

            var result =
                await Mediator.Send(request);

            return result;
        }

    }
}
