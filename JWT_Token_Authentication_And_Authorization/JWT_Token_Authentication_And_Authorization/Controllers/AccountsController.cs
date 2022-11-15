using JWT_Token_Authentication_And_Authorization.DAL;
using JWT_Token_Authentication_And_Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Token_Authentication_And_Authorization.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountDAL _accountDAL;

        public AccountsController(IAccountDAL accountDAL)
        {
            _accountDAL = accountDAL;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(Users user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request");
            }

            var jwtToken = _accountDAL.AuthenticateAndGenerateToken(user);

            if(jwtToken == null)
            {
                return Unauthorized();
            }

            return Ok(jwtToken);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUserList()
        {
            var userList = new List<string>
            {
                "Vimal Gohil",
                "Rajesh Panchal",
                "Suresh Makwana"
            };

             return Ok(userList);
        }
    }
}
