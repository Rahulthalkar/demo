using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.Domain.Tables
{
    public class tblReplayComment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReplayComments { get; set; }
        public int? CommentsId { get; set; }
        [ForeignKey("CommentsId")]
        public tblComments CommentsTbl { get; set; }
    }
}
