using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.Domain.Tables
{
    public class tblComments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime createDate { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        [ForeignKey("UserId")]
        public tblEmployee User { get; set; }
        [ForeignKey("ReplayComment")]
        public tblComments CommentsById { get; set; }
        public int? ReplayComment { get; set; }
        public ICollection<tblReplayComment> Replay { get; } = new List<tblReplayComment>(); // Collection navigation containing dependents


    }
}
