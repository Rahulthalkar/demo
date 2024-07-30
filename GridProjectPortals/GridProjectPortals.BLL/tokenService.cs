using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.BLL
{
    public class tokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenRespository _tokenRespository;
        public tokenService(IConfiguration configuration,ITokenRespository tokenRespository)
        {
            _configuration = configuration;
            _tokenRespository=tokenRespository;
        }

        public APIResult<bool>GenerateToken(string username,string password)
        {
            var response=new APIResult<bool>();
            try
            {
                response.Value = _tokenRespository.GenerateToken(username, password);
                response.IsSuccess = true;
                return response;
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessageKey = "GeneratingToken";
                response.ExceptionInfo = JsonConvert.SerializeObject(ex.Message);
                return response;
            }
        }
    }
}
