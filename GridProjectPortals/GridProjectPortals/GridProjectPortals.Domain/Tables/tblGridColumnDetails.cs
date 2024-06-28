using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.Domain.Tables
{
    public class tblGridColumnDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? GridId { get; set; }
        public string? ColumnName { get; set; }
        public string? ColumnDataType { get; set; }
        public bool? isSearchable { get; set; }
        public bool? isVisible { get; set; }
        public int? Sequence { get; set; }
        public bool? isFix { get; set; }
    }
}
