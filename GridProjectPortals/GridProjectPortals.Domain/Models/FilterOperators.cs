using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.Domain.Models
{
    public class FilterOperators
    {
        public int OperatorId { get; set; }
        public string? DataType { get; set; }
        public string? OperatorInWords { get; set; }
        public string? Operator { get; set; }
    }
    public class FilterCriteriaModel
    {
        public string OperatorName { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }
    }
}
