using GridProjectPortals.BLL;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using log4net;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GridProjectPortals.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {

        private readonly IMailServices _mailServices;

        public SendMailController(IMailServices mailServices)
        {
            _mailServices = mailServices;
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

    }
}
