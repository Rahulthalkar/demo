using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.Domain.Models
{
    public class GenericGridRequestModel
    {
    }
    public class GridColumnDetailModel
    {
        public int Id { get; set; }
        public bool? isVisible { get; set; }
    }
    public class UpdateColumnDetailRequestModel
    {
        public List<GridColumnDetailModel> GridColumnDetails { get; set; }
    }
}