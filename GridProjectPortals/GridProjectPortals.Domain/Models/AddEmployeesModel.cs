using GridProjectPortals.Domain.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public byte[]? Photo { get; set; }
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
        public string mobileNo { get; set; }
        public byte[]? Photo { get; set; }

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

    public class CommentsrRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime createDate { get; set; }
      


    }
    public class ReplayCommentsrRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime createDate { get; set; }
        public int ReplayComment { get; set; }


    }
    public class CommentsrResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime createDate { get; set; }
        public int ReplayComment { get; set; }

    }
    public class CommentsrReplayModel
    {
        public int Id { get; set; }
        public int commentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime createDate { get; set; }
        public int ReplayComment { get; set; }

    }
}
