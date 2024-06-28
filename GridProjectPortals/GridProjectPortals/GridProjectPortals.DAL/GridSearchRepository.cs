using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.DAL
{
    public class GridSearchRepository : IGridSearchRepository
    {
        private readonly string connectionString;
        public GridSearchRepository(IConfiguration configuration)
        {
            connectionString = Convert.ToString(configuration.GetSection("ConnectionStrings:GRIDDb").Value);
        }
        public APIResult<List<GridColumnResponseModel>> GetGridColumns(int columnId)
        {
            var response = new APIResult<List<GridColumnResponseModel>>();

            using (var _appDbContext = new GridDbContext(connectionString))
            {
                var lstResponse = (from obj in _appDbContext.tblGridColumnDetails
                                   orderby obj.Sequence
                                   where obj.Id == columnId
                                   select new GridColumnResponseModel
                                   {
                                       Id = obj.Id,
                                       ColumnName = obj.ColumnName,
                                       ColumnDataType = obj.ColumnDataType,
                                       isVisible = obj.isVisible
                                   }).ToList();
                response.Value = lstResponse;
                response.IsSuccess = true;
            }
            return response;
        }

        public APIResult<List<GridColumnDetailsResponseModel>> GetGridDetail(int gridId)
        {
            var response = new APIResult<List<GridColumnDetailsResponseModel>>();

            using (var _appDbContext = new GridDbContext(connectionString))
            {
                var lstResponse = (from obj in _appDbContext.tblGridColumnDetails
                                   where obj.GridId == gridId
                                   select new GridColumnDetailsResponseModel
                                   {
                                       Id = obj.Id,
                                       ColumnName = obj.ColumnName,
                                       ColumnDataType = obj.ColumnDataType,
                                       GridId = obj.GridId,
                                       isSearchable = obj.isSearchable,
                                       isVisible = obj.isVisible,
                                       Sequence = obj.Sequence,
                                       isFix = obj.isFix
                                   }).OrderBy(x => x.Sequence).ToList();
                response.Value = lstResponse;
                response.IsSuccess = true;
            }
            return response;
        }

        public APIResult<List<GridPageResponseModel>> GetGridPageDetail(string pageName)
        {
            var response = new APIResult<List<GridPageResponseModel>>();

            using (var _appDbContext = new GridDbContext(connectionString))
            {
                var lstResponse = (from obj in _appDbContext.tblGridPages
                                   join grd in _appDbContext.tblGridColumnDefs
                                   on obj.Id equals grd.PageId
                                   where obj.Page == pageName
                                   select new GridPageResponseModel
                                   {
                                       Id = obj.Id,
                                       PageName = obj.Page,
                                       GridId = grd.Id,
                                       GridName = grd.GridName,
                                   }).ToList();

               
                response.Value = lstResponse;
                response.IsSuccess = true;
            }
            return response;
        }

        public APIResult<FilterOperators> GetOperatorById(int operatorId)
        {
            var response = new APIResult<FilterOperators>();

            using (var _appDbContext = new GridDbContext(connectionString))
            {
                var lstResponse = (from obj in _appDbContext.tblGridDataTypeOperators
                                   where obj.Id == operatorId
                                   select new FilterOperators
                                   {
                                       OperatorId = obj.Id,
                                       DataType = obj.DataType,
                                       OperatorInWords = obj.OperatorInWords,
                                       Operator = obj.Operator
                                   }).FirstOrDefault();
                response.Value = lstResponse;
                response.IsSuccess = true;
            }
            return response;
        }

        public APIResult<List<FilterOperators>> GetOperatorByType(string operatorType)
        {
            var response = new APIResult<List<FilterOperators>>();

            using (var _appDbContext = new GridDbContext(connectionString))
            {
                var lstResponse = (from obj in _appDbContext.tblGridDataTypeOperators
                                   where obj.DataType == operatorType
                                   select new FilterOperators
                                   {
                                       OperatorId = obj.Id,
                                       DataType = obj.DataType,
                                       OperatorInWords = obj.OperatorInWords,
                                       Operator = obj.Operator
                                   }).ToList();
                response.Value = lstResponse;
                response.IsSuccess = true;
            }
            return response;
        }

        public APIResult<string> UpdateGridColumnDetails(UpdateColumnDetailRequestModel obj)
        {
            var response = new APIResult<string>();

            using (var _appDbContext = new GridDbContext(connectionString))
            {
                int cnt = 1;
                foreach (var gridColumnDetail in obj.GridColumnDetails)
                {

                    var detailToUpdate = _appDbContext.tblGridColumnDetails.FirstOrDefault(detail => detail.Id == gridColumnDetail.Id);
                    if (detailToUpdate != null)
                    {
                        detailToUpdate.Sequence = cnt;
                        detailToUpdate.isVisible = gridColumnDetail.isVisible;
                    }
                    cnt++;
                }
                try
                {
                    _appDbContext.SaveChanges();
                    response.IsSuccess = true;
                    response.Value = "GridColumnsUpdated";
                    return response;
                }
                catch (Exception exception)
                {
                    response.IsSuccess = false;
                    response.Value = "UnableToUpdate";
                    response.ErrorMessageKey = "UnableToUpdate";
                    response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                    return response;
                }
            }
        }
    }
}
