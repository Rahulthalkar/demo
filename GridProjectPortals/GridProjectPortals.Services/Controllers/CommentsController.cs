using GridProjectPortals.BLL;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GridProjectPortals.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        public readonly EmployeeService _employeeService;
        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));

        public CommentsController(IConfiguration configuration,IEmployeeRepository employeeRepository) {
            _employeeService=new EmployeeService(configuration, employeeRepository);
        }
        [Route("Comments")]
        [HttpPost]
        public async Task<IActionResult> Comments(CommentsrRequestModel requestModel)
        {
            log.Info($"{nameof(Comments)} method called with userdata: {requestModel}");
            APIResult<string> response = new APIResult<string>();
            if(ModelState.IsValid)
            {
                if (requestModel != null&& requestModel.UserId!=0)
                {
                    response = _employeeService.Comments(requestModel);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest(response);
           
        }
        [Route("ReplayComments")]
        [HttpPost]
        public async Task<IActionResult> ReplayComments(ReplayCommentsrRequestModel requestModel)
        {
            log.Info($"{nameof(ReplayComments)} method called with userdata: {requestModel}");
            APIResult<string> response = new APIResult<string>();
            if(ModelState.IsValid)
            {
                if (requestModel != null && requestModel.ReplayComment!=0)
                {
                    response = _employeeService.ReplayComments(requestModel);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
           
        }
        [Route("GetCommentById")]
        [HttpPost]
        public async Task<IActionResult> GetCommentById(int userId)
        {
            APIResult<List<CommentsrResponseModel>> response = new APIResult<List<CommentsrResponseModel>>();
            response = _employeeService.GetCommentById(userId);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [Route("GetCommentByCommentId")]
        [HttpPost]
        public async Task<IActionResult> GetCommentByCommentId(int userId,int commentId)
        {
            APIResult<List<CommentsrResponseModel>> response = new APIResult<List<CommentsrResponseModel>>();
            response = _employeeService.GetCommentByCommentId(userId,commentId);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
