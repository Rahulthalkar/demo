using GridProjectPortals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.DAL.Interface
{
    public interface IGridSearchRepository
    {
        public APIResult<List<GridColumnDetailsResponseModel>> GetGridDetail(int gridId);
        public APIResult<List<GridColumnResponseModel>> GetGridColumns(int columnId);
        public APIResult<List<GridPageResponseModel>> GetGridPageDetail(string pageName);

        /// <summary>
        /// update columns as per user preference.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// returns  successful message     
        ///</returns>       
        public APIResult<string> UpdateGridColumnDetails(UpdateColumnDetailRequestModel obj);
        public APIResult<FilterOperators> GetOperatorById(int operatorId);
        public APIResult<List<FilterOperators>> GetOperatorByType(string operatorType);
    }
}
