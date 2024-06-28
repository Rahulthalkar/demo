using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.Domain.Models
{
    public class GenericGridResponseModel
    {
    }
    public class GridPageResponseModel
    {
        public int Id { get; set; }
        public int GridId { get; set; }
        public string PageName { get; set; } = string.Empty;
        public string GridName { get; set; } = string.Empty;
    }

    public class GridColumnDetailsResponseModel
    {
        public int Id { get; set; }
        public int? GridId { get; set; }
        public string? ColumnName { get; set; }
        public string? ColumnDataType { get; set; }
        public bool? isSearchable { get; set; }
        public bool? isVisible { get; set; }
        public int? Sequence { get; set; }
        public bool? isFix { get; set; }
    }

    public class GridColumnResponseModel
    {
        public int Id { get; set; }
        public string? ColumnName { get; set; }
        public string? ColumnDataType { get; set; }
        public bool? isVisible { get; set; }
    }

}
