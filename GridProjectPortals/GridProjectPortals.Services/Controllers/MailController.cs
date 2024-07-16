
using Azure;
using GridProjectPortals.BLL;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using GridProjectPortals.Services.Controllers;
using log4net;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;


namespace GridProjectPortals.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        public readonly BLL.MailService _mailService;

        private static readonly ILog log = LogManager.GetLogger(typeof(SendMailController));
        public MailController(IConfiguration configuration, ISendIMailRepository sendIMailRepository)
        {
            _mailService = new BLL.MailService(configuration, sendIMailRepository);
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] sendMail request)
        {
            if (request != null)
            {
                var response = _mailService.SendMail(request);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
