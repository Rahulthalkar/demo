using GridProjectPortals.BLL;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace GridProjectPortals.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly EmployeeService _employeeService;

        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));
        public EmployeeController(IConfiguration configuration, IEmployeeRepository employeeRepository)
        {
            _employeeService = new EmployeeService(configuration, employeeRepository);
        }
        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(AddEmployeesModel addEmployeesModel)
        {
            log.Info($"{nameof(CreateUser)} method called with userdata: {addEmployeesModel}");
            APIResult<string> response = new APIResult<string>();
            if (ModelState.IsValid)
            {
                if (addEmployeesModel != null)
                {
                    response = _employeeService.CreateUser(addEmployeesModel);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest(response);

           
        }
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(APIResult<LoginResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequestModel loginModel)
        {
            log.Info("Post Login method called");
            APIResult<LoginResponse> loginResponse = new APIResult<LoginResponse>();
            if (loginModel != null)
            {
                loginModel.Email = HttpUtility.UrlDecode(loginModel.Email);
                loginModel.Password = HttpUtility.UrlDecode(loginModel.Password);
                loginResponse = _employeeService.Login(loginModel);
            }
            else
            {
                loginResponse.IsSuccess = false;
            }
            return Ok(loginResponse);
        }

        [HttpGet]
        [Route("GetEmployeeList")]
        public async Task<IActionResult> GetEmployeeList()
        {
            APIResult<List<EmployeDetailModel>> lstemployee=new APIResult<List<EmployeDetailModel>>();
            lstemployee=_employeeService.GetEmployees();
            if (lstemployee != null)
            {
                return Ok(lstemployee);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("UpdateEmployees")]
        public async Task<IActionResult>UpdateEmployees(EmployeUpdateModel employeUpdateModel)
        {
            var response=new APIResult<string>();
            
            response=_employeeService.UpdateEmployee(employeUpdateModel);
            if(response != null)
            {
                return Ok(response);
            }
            else 
            { 
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("GetUserDetailById")]
        public async Task<IActionResult> GetUserDetailById(int userId)
        {
            if(userId == 0)
            {
                return BadRequest();
            }
            var userResponseDetails=_employeeService.GetUserDetailById(userId);
            if (userResponseDetails.IsSuccess)
            {
                return Ok(userResponseDetails);
            }
            else
            {
                return BadRequest(userResponseDetails);
            }
        }

        [HttpPost]
        [Route("SearchEmployee")]
        public async Task<IActionResult> SearchEmployee(string searchCriteria)
        {
            APIResult<List<EmployeDetailModel>> lstemployee = new APIResult<List<EmployeDetailModel>>();
            lstemployee = _employeeService.SearchEmployee(searchCriteria);
            if (lstemployee != null)
            {
                return Ok(lstemployee);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("UpdatePassword")]
        [ProducesResponseType(typeof(APIResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePassword(ChangePasswordModel changePasswordModal)
        {
            log.Info("Customer Service - UpdatePassword method called");

            APIResult<string> response = new APIResult<string>();

            if (changePasswordModal != null)
            {
                response = _employeeService.UpdatePassword(changePasswordModal);
                if (response != null && response.IsSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("ValidateUser")]
        [ProducesResponseType(typeof(APIResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ValidateUser(string email, string password)
        {
            log.Info("CreateUser method called");
            APIResult<string> objResponse = new APIResult<string>();
            objResponse =_employeeService.ValidateUser(email, password);
            if (objResponse != null)
            {
                return Ok(objResponse);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
