using AkkonMassive.Domain.Helper;
using Azure;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using GridProjectPortals.Domain.Tables;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            connectionString = Convert.ToString(configuration.GetSection("ConnectionStrings:GRIDDb").Value);
        }
        public APIResult<string> CreateEmployee(AddEmployeesModel addEmployeesModel)
        {
            var response= new APIResult<string>();
            using(var dbcontext=new GridDbContext(connectionString))
            {
                try
                {
                    var empCreate = new tblEmployee
                    {
                        FirstName = addEmployeesModel.FirstName,
                        LastName = addEmployeesModel.LastName,
                        UserName = addEmployeesModel.UserName,
                        Phone = addEmployeesModel.Phone,
                        IsActive = addEmployeesModel.IsActive,
                        Email = addEmployeesModel.Email,
                        Password = addEmployeesModel.Password,
                        Salt = addEmployeesModel.Salt,

                    };
                    dbcontext.tblEmployees.Add(empCreate);
                    dbcontext.SaveChanges();

                    response.IsSuccess= true;
                    response.Value = "Employee created successfully";

                }catch (Exception ex)
                {
                    response.IsSuccess= false;
                    response.Value = string.Empty;
                    response.ErrorMessageKey = "Create Failure";
                }
            }
            return response;
        }

        public List<EmployeDetailModel> GetEmployeDetail()
        {
            using(var context=new GridDbContext(connectionString))
            {
                var data= context.tblEmployees.ToList();
                var empDetails = data.Select(e => new EmployeDetailModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    UserName = e.UserName,
                    Phone = e.Phone,
                }).ToList();
                return empDetails;

            }

        }

        public string GetSalt(LoginRequestModel loginModel)
        {
            APIResult<string> response = new APIResult<string>();
            using (var dbcontext = new GridDbContext(connectionString))
            {
                var user = dbcontext.tblEmployees.Where(x => x.Email == loginModel.Email).FirstOrDefault();
                string? salt;
                if (user != null)
                {
                    salt = user.Salt;
                }
                else
                {
                    salt = string.Empty;
                }
                if (salt != "" || salt != null)
                {
                    return salt;
                }
                else
                {
                    return salt = string.Empty;
                }
            }
        }

        public APIResult<LoginResponse> Login(LoginRequestModel loginModel)
        {
            using(var dbcontext=new GridDbContext(connectionString))
            {
                APIResult<LoginResponse> response=new APIResult<LoginResponse>();
                try
                {
                    var user=(from usr in dbcontext.tblEmployees 
                              where usr.Email==loginModel.Email && usr.Password==loginModel.Password select usr)
                              .FirstOrDefault();

                    if(user!=null)
                    {
                        if(user.IsActive)
                        {
                           LoginResponse loginResponse = new LoginResponse()
                           {
                               Id = user.Id,
                               FirstName =user.FirstName,
                               LastName=user.LastName,
                               Email=user.Email,
                               UserName=user.UserName,
                           };
                            response.Value = loginResponse;
                            response.IsSuccess = true;

                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.ErrorMessageKey = "UserIsNotActive";
                            return response;
                        }

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessageKey = "InvalidUserOrPassword";
                        return response;
                    }
                    return response;
                }
                catch(Exception ex)
                {
                    response.IsSuccess = false;
                    response.ExceptionInfo = JsonConvert.SerializeObject(ex);
                    return response;
                }
            }
        }

        public APIResult<string> UpdateEmployee(EmployeUpdateModel employeUpdateModel)
        {
            var response = new APIResult<string>();
            using(var  dbcontext=new GridDbContext(connectionString))
            {
                var dataUpdate=dbcontext.tblEmployees.Where(emp=>emp.Id==employeUpdateModel.Id).FirstOrDefault();
                if (dataUpdate!=null)
                {
                   // dataUpdate.Id = employeUpdateModel.Id;
                    dataUpdate.FirstName = employeUpdateModel.FirstName;
                    dataUpdate.LastName = employeUpdateModel.LastName;
                    dataUpdate.UserName = employeUpdateModel.UserName;
                    dataUpdate.Phone = employeUpdateModel.Phone;


                    try
                    {
                        dbcontext.SaveChanges();
                        response.IsSuccess = true;
                        response.Value = "Employeeupdatesuccessfully";
                        return response;
                    }catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.ErrorMessageKey="UnabletoUpdateEmployee";
                        response.ExceptionInfo= JsonConvert.SerializeObject(ex);
                        return response;
                    }
                }
                else
                {
                    response.IsSuccess=false;
                    response.ErrorMessageKey = "NoUserFound";
                    return response;
                }

            }
        }

        public string UserExists(string username)
        {
            using (var dbcontext = new GridDbContext(connectionString))
                {
                    var isUserAlreadyExists = (from lusr in dbcontext.tblEmployees
                                               where lusr.UserName == username
                                               select lusr).FirstOrDefault();
                    if (isUserAlreadyExists != null)
                    {
                        return "UserExists";
                    }
                    else
                    {
                        return "Validated";
                    }

            }
        }

        public void UpdateLoginAttemptCount(string email, string hashedPassword)
        {
            using (var dbcontext = new GridDbContext(connectionString))
            {
                try
                {
                    var isUserEmailPresent = (from usr in dbcontext.tblEmployees
                                              where usr.Email == email
                                              select usr).FirstOrDefault();
                    if (isUserEmailPresent != null)
                    {
                        if (isUserEmailPresent.FailedLoginAttempts == null)
                        {
                            isUserEmailPresent.FailedLoginAttempts = 1;
                        }
                        else
                        {
                            isUserEmailPresent.FailedLoginAttempts++;
                        }
                        dbcontext.SaveChanges();
                    }
                    return;
                }
                catch (Exception exception)
                {
                    throw;
                }

            }
        }

        /// <summary>
        /// Clear LoginAttemptCount to zero if user is valid
        /// </summary>
        /// <param name="user"></param>
        public void ClearLoginAttemptCount(APIResult<LoginResponse> user)
        {
            using (var dbcontext = new GridDbContext(connectionString))
            {
                try
                {
                    if (user.IsSuccess)
                    {
                        var userToUpdate = (from usr in dbcontext.tblEmployees
                                            where usr.Email == user.Value.Email
                                            select usr).FirstOrDefault();
                        if (userToUpdate != null)
                        {
                            userToUpdate.FailedLoginAttempts = 0;
                            dbcontext.SaveChanges();
                        }
                        return;
                    }
                }
                catch (Exception exception)
                {

                    throw;
                }
            }
        }

        public UserDetailResponseModel GetUserDetailById(int userId)
        {
            using(var dbcontext=new GridDbContext(connectionString))
            {
                var user=(from usr in dbcontext.tblEmployees
                          where usr.Id== userId select new UserDetailResponseModel
                          {
                              Id = usr.Id,
                              FirstName=usr.FirstName,
                              LastName=usr.LastName,
                              Email=usr.Email,
                              UserName=usr.UserName
                          }).FirstOrDefault();
                return user;
            }
        }

        public List<EmployeDetailModel> SearchEmployee(string searchCriteria)
        {
           using(var dbContext =new  GridDbContext(connectionString))
            {
                var userlist=(from db in dbContext.tblEmployees
                            where (db.FirstName.Contains(searchCriteria) 
                            || db.LastName.Contains(searchCriteria)
                            || db.UserName.Contains(searchCriteria))
                            select new EmployeDetailModel
                            {
                               
                                FirstName=db.FirstName,
                                LastName=db.LastName,
                                UserName=db.UserName,
                                Phone=db.Phone,

                            }).Distinct().ToList();
                return userlist;
            }
        }

        public APIResult<string> UpdatePassword(ChangePasswordModel changePasswordModal, string salt)
        {
            var response=new APIResult<string>();
            using(var dbcontext=new  GridDbContext(connectionString))
            {
                var dataToUpdate=(from usr in dbcontext.tblEmployees 
                                  where usr.Id==changePasswordModal.UserId   
                                  select usr).FirstOrDefault();
                if (dataToUpdate!=null)
                {
                    dataToUpdate.Password = changePasswordModal.Password;
                    dataToUpdate.Salt = salt;
                    dataToUpdate.UpdatedOn = DateTime.UtcNow;
                    dataToUpdate.UpdatedBy = changePasswordModal.UserId;
                    try
                    {
                        dbcontext.SaveChanges();
                        response.IsSuccess = true;
                        response.Value = "PasswordIsUpdatedSuccessfully";
                        return response;
                    }
                    catch (Exception exception)
                    {
                        response.IsSuccess = false;
                        response.ErrorMessageKey = "UnableToUpdatePassword";
                        response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                        return response;
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessageKey = "NoUserFound";
                    return response;
                }
            }
        }

        public APIResult<string> ValidateUser(string email, string password)
        {
            using (var dbcontext =new GridDbContext(connectionString))
            {
                APIResult<string> response = new APIResult<string>();

                var user = dbcontext.tblEmployees.FirstOrDefault(tusr => tusr.Email == email);

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Value = "NotValidUser";
                    return response;
                }
               
                if (!VerifyPassword(password, user.Password, user.Salt))
                {
                        response.IsSuccess = false;
                        response.Value = "InvalidPassword";
                        return response;
               }
                
                response.IsSuccess = true;
                response.Value = "ValidUser";
                return response;
            }
        }
        // Password verification method
        private bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            string hashedInputPassword = HashingUtility.HashString(password, salt);
            return hashedPassword == hashedInputPassword;
        }
    }
}
