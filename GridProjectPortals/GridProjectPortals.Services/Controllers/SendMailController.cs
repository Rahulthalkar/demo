using GridProjectPortals.BLL;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using log4net;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace GridProjectPortals.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {

        private readonly IMailServices _mailServices;
        public readonly EmployeeService _employeeService;


        public SendMailController(IConfiguration configuration,IMailServices mailServices, IEmployeeRepository employeeRepository)
        {
            _mailServices = mailServices;
            _employeeService = new EmployeeService(configuration, employeeRepository);
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] sendMail request)
        {
            try
            {
                await _mailServices.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(AddEmployeesModel addEmployeesModel)
        {
           // log.Info($"{nameof(CreateUser)} method called with userdata: {addEmployeesModel}");
            APIResult<string> response = new APIResult<string>();

            if (addEmployeesModel != null)
            {
                response = _employeeService.CreateUser(addEmployeesModel);
                if (response != null)
                {
                    var sendMail = new sendMail
                    {
                        MailTo = addEmployeesModel.Email, // User's email
                        Subject = "Welcome to Our Service",
                        FirstName = addEmployeesModel.FirstName,
                        Body = $"<p>Thank you  {addEmployeesModel.FirstName} for creating an account with us!</p>"
                    };
                    _mailServices.SendEmailAsync(sendMail);
                }
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
