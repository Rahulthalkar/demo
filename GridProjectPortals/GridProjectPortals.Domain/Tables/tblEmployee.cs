using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.Domain.Tables
{
    public class tblEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? FirstName{ get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set;}
        public string? Salt { get; set; }
        public bool IsActive { get; set; }
        public int? FailedLoginAttempts { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public tblEmployee? UserCreatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public tblEmployee? UserUpdatedBy { get; set; }
    }
}
