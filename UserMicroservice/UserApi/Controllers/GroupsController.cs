using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Application.Groups.Commands;
using UserApi.Application.Groups.Queries;
using UserApi.ViewModels.Groups;

namespace ApplicationApi.Controllers
{
    public class GroupsController : Utilities.ControllerBase
    {
        public GroupsController
            (MediatR.IMediator mediator) : base(mediator: mediator)
        {
        }

        /// <summary>  
        /// Get all groups
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>
        [HttpPost(template: "GetAll")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<IList<GroupViewModel>>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<IList<GroupViewModel>>> GetAll()
        {
            var request =
                new GetAllGroupsQuery();

            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Create new group
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
            ([FromBody] CreateGroupCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Edit group
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
            ([FromBody] UpdateGroupCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Get group by id
        /// </summary>  
        /// <param name="groupId"></param>  
        /// <returns></returns>
        [HttpGet(template: "{groupId?}")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<GroupViewModel>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<GroupViewModel>> Get
            ([FromRoute] int? groupId)
        {
            var request =
                new GetGroupByIdQuery
                {
                    Id = groupId,
                };

            var result =
                await Mediator.Send(request);

            return result;
        }
    }
}
