using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationApi.Application.Applications.Commands;
using ApplicationApi.Application.Applications.Queries;
using ApplicationApi.ViewModels.Applications;

namespace ApplicationApi.Controllers
{
    public class ApplicationsController : Utilities.ControllerBase
    {
        public ApplicationsController
            (MediatR.IMediator mediator) : base(mediator: mediator)
        {
        }

        /// <summary>  
        /// Get all applications
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>
        [HttpPost(template: "GetAll")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<IList<ApplicationViewModel>>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<IList<ApplicationViewModel>>> GetAll
            ([FromBody] GetAllApplicationQuery request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Create new application
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
            ([FromBody] CreateApplicationCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Edit application
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
            ([FromBody] UpdateApplicationCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Get application by id
        /// </summary>  
        /// <param name="applicationId"></param>  
        /// <returns></returns>
        [HttpGet(template: "{applicationId?}")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<ApplicationViewModel>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<ApplicationViewModel>> Get
            ([FromRoute] int? applicationId)
        {
            var request =
                new GetApplicationByIdQuery
                {
                    Id = applicationId,
                };

            var result =
                await Mediator.Send(request);

            return result;
        }

    }
}
