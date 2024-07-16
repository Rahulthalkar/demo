using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.Domain.Tables
{
    public class tblGridColumnDef
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? GridName { get; set; }

        public int? UserId { get; set; }

        public int? PageId { get; set; }
    }
}
