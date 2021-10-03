using Framework.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserApi.Application.Users.Commands;
using UserApi.Application.Users.Queries;
using UserApi.ViewModels.Users;

namespace ApplicationApi.Controllers
{
    public class UsersController : 
        Utilities.ControllerBase
    {
        public UsersController
            (MediatR.IMediator mediator) : base(mediator: mediator)
        {
        }

        /// <summary>  
        /// Get all users paging
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>
        [HttpPost(template: "GetAll")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<ViewPagingDataResult<UserViewModel>>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<ViewPagingDataResult<UserViewModel>>> GetAll
            ([FromBody] GetAllUsersQuery request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Create new user
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
            ([FromBody] CreateUserCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Edit user
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
            ([FromBody] UpdateUserCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Get user by id
        /// </summary>  
        /// <param name="userid"></param>  
        /// <returns></returns>
        [HttpGet(template: "{userId?}")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result<UserViewModel>),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result<UserViewModel>> Get
            ([FromRoute] int? userId)
        {
            var request =
                new GetUserByIdQuery
                {
                    Id = userId,
                };

            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Add new group
        /// </summary>  
        /// <param name="request"></param>   
        /// <returns></returns>
        [HttpPost(template: "AssignToGroup")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result> AssignToGroup
            ([FromBody] AssignToGroupCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }

        /// <summary>  
        /// Remove group
        /// </summary>  
        /// <param name="request"></param>  
        /// <returns></returns>
        [HttpPost(template: "RemoveFromGroup")]

        //[Infrastructure.Attributes.Authorize
        //    (userType: Models.Enums.UserType.User)]

        [ProducesResponseType
            (type: typeof(Framework.Result),
            statusCode: StatusCodes.Status200OK)]

        public async Task<Framework.Result> RemoveGroup
            ([FromBody] RemoveFromGroupCommand request)
        {
            var result =
                await Mediator.Send(request);

            return result;
        }
    }
}
