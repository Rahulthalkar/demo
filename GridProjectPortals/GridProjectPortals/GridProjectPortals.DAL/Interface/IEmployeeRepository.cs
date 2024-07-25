using GridProjectPortals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.DAL.Interface
{
    public interface IEmployeeRepository
    {
        APIResult<string> CreateEmployee(AddEmployeesModel addEmployeesModel);
        APIResult<string> UpdateEmployee(EmployeUpdateModel employeUpdateModel);
        APIResult<string> UpdatePassword(ChangePasswordModel changePasswordModal, string salt);
        public UserDetailResponseModel GetUserDetailById(int userId);
        string UserExists(string username);

        public string GetSalt(LoginRequestModel loginModel);
        public APIResult<LoginResponse> Login(LoginRequestModel loginModel);
        List<EmployeDetailModel> GetEmployeDetail();
        List<EmployeDetailModel> SearchEmployee(string searchCriteria);
        public void UpdateLoginAttemptCount(string email, string hashedPassword);
        public void ClearLoginAttemptCount(APIResult<LoginResponse> user);

        public APIResult<string> ValidateUser(string email, string password);

    }
}
