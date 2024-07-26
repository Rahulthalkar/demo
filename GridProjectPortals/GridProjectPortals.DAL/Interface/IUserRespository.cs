using GridProjectPortals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.DAL.Interface
{
    public interface IUserRespository
    {
        public APIResult<string> Register(userRequestModel userRequestModel);
        List<userResponseModel> GetAllUser();
    }
}
