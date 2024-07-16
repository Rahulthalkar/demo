using GridProjectPortals.DAL;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace GridProjectPortals.BLL
{
    public class MailService
    {

        private ISendIMailRepository _sendIMailRepository;

        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration, ISendIMailRepository sendIMailRepository)
        {
            _configuration = configuration;
            _sendIMailRepository = sendIMailRepository;
        }
        public APIResult<string> SendMail(sendMail sendMail)
        {
            APIResult<string> apiResult = new APIResult<string>();
            try
            {
               
                 var result =_sendIMailRepository.SendEmailAsync(sendMail);
                    if (result.IsCompletedSuccessfully)
                    {
                        apiResult.IsSuccess = true;
                        apiResult.Value = "User-CreateMessage";
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.Value = "User-FailureMessage";
                    }
                

                return apiResult;
            }
            catch (Exception ex)
            {
                apiResult.IsSuccess = false;
                apiResult.ErrorMessageKey = $"An error occurred: {ex.Message}";
            }
            return apiResult;
        }


    }
}
