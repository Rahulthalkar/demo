using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using GridProjectPortals.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.DAL
{
    public class UserRepository : IUserRespository
    {
        private readonly string connectionString;
        public UserRepository(IConfiguration configuration)
        {
            connectionString = Convert.ToString(configuration.GetSection("ConnectionStrings:GRIDDb").Value);
        }

        public List<userResponseModel> GetAllUser()
        {
            using(var dbcontext =new GridDbContext(connectionString))
            {
                var datalist=dbcontext.tblUsers.ToList();
                var result=datalist.Select(d=>new userResponseModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Email = d.Email,

                }).ToList();
                return result;
            }
        }

        public APIResult<string> Register(userRequestModel userRequestModel)
        {
            APIResult<string> result = new APIResult<string>();
            using(var dbcontext= new GridDbContext(connectionString))
            {
                try
                {
                    var data = new tblUser
                    {
                        Id = userRequestModel.Id,
                        Name = userRequestModel.Name,
                        Password = userRequestModel.Password,
                        Email = userRequestModel.Email,
                    };
                    dbcontext.tblUsers.Add(data);
                    dbcontext.SaveChanges();
                    result.IsSuccess=true;
                    result.Value = "UserCreateSuccess";
                }
                catch(Exception ex)
                {
                    result.IsSuccess=false;
                    result.Value = "Failed to user create";
                    result.ExceptionInfo = ex.Message;
                }
            }
            return result;
        }
    }
}
