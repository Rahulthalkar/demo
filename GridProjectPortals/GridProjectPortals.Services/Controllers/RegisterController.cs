using GridProjectPortals.BLL;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GridProjectPortals.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserService _userService;
        public RegisterController(IConfiguration configuration,IUserRespository userRespository)
        {
            _userService = new UserService(configuration, userRespository);
        }
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(userRequestModel userRequestModel) 
        {
            APIResult<string> response = new APIResult<string>();
            if (ModelState.IsValid)
            {
                if (userRequestModel != null)
                {
                    var createUser = _userService.Register(userRequestModel);

                    return Ok(createUser);
                }
                else
                {
                    return BadRequest();
                }
            }
                return BadRequest(response);
        }
        [HttpGet]
        [Route("GetAllUsers")]
       public async Task<IActionResult> GetAllUsers()
       {
            APIResult<List<userResponseModel>> response = _userService.GetAllUsers();
            response =_userService.GetAllUsers();
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

       }
    }
}