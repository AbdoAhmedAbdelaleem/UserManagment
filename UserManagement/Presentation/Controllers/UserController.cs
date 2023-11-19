using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Application;
using UserManagement.Application.Commands;
using UserManagement.Entities;
using UserManagement.Infrastructure.DB;

namespace UserManagement.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommand createUserCommand)
        {
            var createdUserResult = await _userRepository.CreateUser(createUserCommand);
            if (createdUserResult.StrtausCode == HttpStatusCode.OK)
                return Ok(createdUserResult.Data);
            else
                return BadRequest(createdUserResult.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id, [FromQuery] string accessToken)
        {
            var userResult = await _userRepository.GetUser(new Application.Queries.GetUserQuery() { Id = id, AccessToken = accessToken});

            if (userResult.StrtausCode == HttpStatusCode.OK)
                return Ok(userResult.Data);
            else
                return BadRequest(userResult.Message);
        }
    }
}
