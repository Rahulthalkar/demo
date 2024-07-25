using AkkonMassive.Domain.Helper;
using Azure;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.BLL
{
    public class EmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        private readonly IConfiguration _configuration;

        public EmployeeService(IConfiguration configuration,IEmployeeRepository employeeRepository)
        {
            _configuration = configuration;
            _employeeRepository = employeeRepository;
        }

        public APIResult<string> CreateUser(AddEmployeesModel addEmployeesModel)
        {
            APIResult<string> apiResult = new APIResult<string>();
            try
            {
                var existResponse = _employeeRepository.UserExists(addEmployeesModel.UserName);
                if (existResponse == "Validated")
                {
                   /* if (addEmployeesModell.LoginTypeId == (int)LoginTypes.MassivePortal)
                    {*/
                        addEmployeesModel.Salt = HashingUtility.GenerateSalt();
                        addEmployeesModel.Password = HashingUtility.HashString(addEmployeesModel.Password, addEmployeesModel.Salt);
                    /*}
                    else
                    {
                        addUserModel.Salt = null;
                        addUserModel.Password = null;
                    }*/
                    var result = _employeeRepository.CreateEmployee(addEmployeesModel);

                    if (result.IsSuccess)
                    {
                        apiResult.IsSuccess = true;
                        apiResult.Value = "User-CreateMessage";
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.Value = "User-FailureMessage";
                    }
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.Value = "UserExists";
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


        public APIResult<List<EmployeDetailModel>> GetEmployees()
        {
            APIResult<List<EmployeDetailModel>> response=new APIResult<List<EmployeDetailModel>>();
            try
            {
                var empdata=_employeeRepository.GetEmployeDetail();
                response.IsSuccess = true;
                response.Value = empdata;
                return response;

            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(ex);
                return response;
            }
            
        }
        public APIResult<LoginResponse> Login(LoginRequestModel loginModel)
        {
            APIResult<LoginResponse> loginResponse=new APIResult<LoginResponse>();
            try
            {
                string salt = _employeeRepository.GetSalt(loginModel);
                if (string.IsNullOrEmpty(salt))
                {
                    loginResponse.IsSuccess = false;
                    loginResponse.ErrorMessageKey = "InvalidUserOrPassword";
                    return loginResponse;
                }
                var hashedPassword = HashingUtility.HashString(loginModel.Password, salt);
                loginModel.Password = hashedPassword;
                loginResponse = _employeeRepository.Login(loginModel);

                if (loginResponse.ErrorMessageKey == "InvalidUserOrPassword")
                {
                    _employeeRepository.UpdateLoginAttemptCount(loginModel.Email, hashedPassword);
                }

                if (loginResponse.IsSuccess)
                {
                    _employeeRepository.ClearLoginAttemptCount(loginResponse);
                    // _employeeRepository.UpdateResetPassword(null, loginResponse.Value.Id);
                }
                return loginResponse;

            }
            catch (Exception ex)
            {
                loginResponse.IsSuccess = false;
                loginResponse.Value = null;
                loginResponse.ErrorMessageKey = "ConnectionFailed";
                loginResponse.ExceptionInfo = JsonConvert.SerializeObject(ex);
                return loginResponse;
            }
        }
     
        public APIResult<string> UpdateEmployee(EmployeUpdateModel employeUpdateModel)
        {
            APIResult<string> apiResult = new APIResult<string>();
            try
            {
                var empData = _employeeRepository.UpdateEmployee(employeUpdateModel);
                return empData;
            }
            catch (Exception ex)
            {
                apiResult.IsSuccess = false;
                apiResult.Value = "";
                apiResult.ErrorMessageKey = "ConnectionFailed";
                apiResult.ExceptionInfo = JsonConvert.SerializeObject(ex);
                return apiResult;
            }
           

        }
        public APIResult<UserDetailResponseModel> GetUserDetailById(int userId)
        {
            APIResult<UserDetailResponseModel> result = new APIResult<UserDetailResponseModel>();
            try
            {
                result.Value = _employeeRepository.GetUserDetailById(userId);
                result.IsSuccess = true;
            }
            catch (Exception exception)
            {
                result.ExceptionInfo = exception.Message;            
                result.IsSuccess = false;
            }
            return result;
        }
        private string GenerateToken(string name, string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public APIResult<List<EmployeDetailModel>> SearchEmployee(string searchCriteria)
        {
            APIResult<List<EmployeDetailModel>> response = new APIResult<List<EmployeDetailModel>>();
            try
            {
                var empdata = _employeeRepository.SearchEmployee(searchCriteria);
                response.IsSuccess = true;
                response.Value = empdata;
                return response;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(ex);
                return response;
            }

        }

        public APIResult<string> UpdatePassword(ChangePasswordModel changePasswordModal)
        {
            APIResult<string> response = new APIResult<string>();
            var validMinutes = Convert.ToInt32(_configuration.GetSection("ResetLinkValidUpto").Value);
            try
            {
                if (changePasswordModal != null)
                {
                    var salt = HashingUtility.GenerateSalt();
                    var hashedPassword = HashingUtility.HashString(changePasswordModal.Password, salt);
                    changePasswordModal.Password = hashedPassword;
                    response = _employeeRepository.UpdatePassword(changePasswordModal, salt);
                    if (response.IsSuccess)
                    {
                        response.IsSuccess = response.IsSuccess;
                        response.Value = response.Value;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessageKey = "UnableToUpdatePassword";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessageKey = "UnableToUpdatePassword";
                }
                return response;
            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.ErrorMessageKey = "UnableToUpdatePassword";
                response.ExceptionInfo = JsonConvert.SerializeObject(response.ExceptionInfo + exception);
                return response;
            }
        }

        public APIResult<string> ValidateUser(string email, string password)
        {
            APIResult<string> response = new APIResult<string>();
            response = _employeeRepository.ValidateUser(email, password);
            return response;
        }

        public APIResult<string> Comments(CommentsrRequestModel commentsrRequestModel)
        {
            APIResult<string> response = new APIResult<string>();
            try
            {
                var empdata = _employeeRepository.Comments(commentsrRequestModel);
                if (empdata != null)
                {
                    response.IsSuccess = true;
                    response.Value = "success comment";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Value = "failed to comment";
                }
                return response;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(ex);
                return response;
            }

        }

        public APIResult<string> ReplayComments(ReplayCommentsrRequestModel replayCommentsrRequest)
        {
            APIResult<string> response = new APIResult<string>();
            try
            {
                var empdata = _employeeRepository.ReplayComments(replayCommentsrRequest);
                if (empdata != null)
                {
                    response.IsSuccess = true;
                    response.Value = "success comment";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Value = "failed to comment";
                }
                return response;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(ex);
                return response;
            }

        }

        public APIResult<List<CommentsrResponseModel>> GetCommentById(int id)
        {
            APIResult<List<CommentsrResponseModel>> result=new APIResult<List<CommentsrResponseModel>>();
            try
            {
                var dataResult = _employeeRepository.GetCommentsByUserId(id);
                result.IsSuccess = true;
                result.Value = dataResult;
                return result;
            }catch(Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessageKey = "ConnectionFailed";
                result.ExceptionInfo= JsonConvert.SerializeObject(ex);
                return result;
            }

        }
        public APIResult<List<CommentsrResponseModel>> GetCommentByCommentId(int id,int commentId)
        {
            APIResult<List<CommentsrResponseModel>> result=new APIResult<List<CommentsrResponseModel>>();
            try
            {
                var dataResult = _employeeRepository.GetCommentByCommentId(id,commentId);
                result.IsSuccess = true;
                result.Value = dataResult;
                return result;
            }catch(Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessageKey = "ConnectionFailed";
                result.ExceptionInfo= JsonConvert.SerializeObject(ex);
                return result;
            }

        }
    }
}
