using Azure;
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
    public class UserService
    {
        private IUserRespository _userRespository;
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration,IUserRespository userRespository) 
        {
            _configuration = configuration;
            _userRespository = userRespository;
        }

        public APIResult<string>Register(userRequestModel userRequestModel)
        {
            APIResult<string> response=new APIResult<string>();
            try
            {
                userRequestModel.Email=Cryptography.Encrypt(userRequestModel.Email);
                userRequestModel.Password=Cryptography.Encrypt(userRequestModel.Password);

                var createData=_userRespository.Register(userRequestModel);
                if(createData.IsSuccess)
                {
                    response.IsSuccess = true;
                    response.Value = "User creared success";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Value = "Failed to user create";
                }
            }catch (Exception ex)
            {
                
                response.IsSuccess = false;
                response.Value = ex.Message;
            }
            return response;
        }

        public APIResult<List<userResponseModel>> GetAllUsers()
        {
            APIResult<List<userResponseModel>>response=new APIResult<List<userResponseModel>>();
            try
            {
                var data = _userRespository.GetAllUser();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        item.Email = Cryptography.Decrypt(item.Email);
                    }
                }
                response.Value = data;
                response.IsSuccess = true;
                return response;
            }catch(Exception ex) 
            { 
                response.IsSuccess = false;
                response.Value=null;
                response.ErrorMessageKey = "UnableToFetchUserData";
                response.ExceptionInfo = JsonConvert.SerializeObject(ex);
                return response;
            }
        }
    }
}
