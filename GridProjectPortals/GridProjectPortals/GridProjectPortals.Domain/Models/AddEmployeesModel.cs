using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.Domain.Models
{
    public class AddEmployeesModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Salt { get; set; }
        public bool IsActive { get; set; }
    }
    public class UserDetailResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
    public class LoginRequestModel
    {
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
    public class LoginResponse
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public double ExpiresIn { get; set; }
    }
}
