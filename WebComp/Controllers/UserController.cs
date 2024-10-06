using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebComp.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("getadmins")]
        public async Task<IActionResult> GetAdmins() => Ok(await userService.GetAllAdminsAsync());

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers() => Ok(await userService.GetAllUsersAsync());

        [HttpGet("get/${id}")]
        public async Task<IActionResult> GetUser([FromRoute]long id) => Ok(await userService.GetByIdAsync(id));

        [HttpPost("createadmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] TelegramUserDto user) => Ok(await userService.CreateUserAsync(user,true));

        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] TelegramUserDto user) => Ok(await userService.CreateUserAsync(user));

    }
}
