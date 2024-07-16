using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectPortals.BLL
{
    public class GenericGridService
    {
        private IGridSearchRepository _gridSearchRepository;

        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configuration;
        public GenericGridService(IConfiguration configuration,IGridSearchRepository gridSearchRepository) 
        {
            _configuration = configuration;
            _gridSearchRepository = gridSearchRepository;
        }

        public APIResult<List<FilterOperators>> GetOperatorByType(string operatorType)
        {
            APIResult<List<FilterOperators>> response = new APIResult<List<FilterOperators>>();
            try
            {
                response = _gridSearchRepository.GetOperatorByType(operatorType);
                return response;
            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Value = null;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                return response;
            }
        }
        public APIResult<List<GridColumnDetailsResponseModel>> GetGridDetail(int gridId)
        {
            APIResult<List<GridColumnDetailsResponseModel>> response = new APIResult<List<GridColumnDetailsResponseModel>>();
            try
            {
                response = _gridSearchRepository.GetGridDetail(gridId);
                return response;
            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Value = null;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                return response;
            }
        }

        public APIResult<List<GridPageResponseModel>> GetGridPageDetail(string pageName)
        {
            APIResult<List<GridPageResponseModel>> response = new APIResult<List<GridPageResponseModel>>();
            try
            {
                response = _gridSearchRepository.GetGridPageDetail(pageName);
                return response;
            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Value = null;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                return response;
            }
        }
        public APIResult<List<GridColumnResponseModel>> GetGridColumns(int columnId)
        {
            APIResult<List<GridColumnResponseModel>> response = new APIResult<List<GridColumnResponseModel>>();
            try
            {
                response = _gridSearchRepository.GetGridColumns(columnId);
                return response;
            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Value = null;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                return response;
            }
        }

        /// <summary>
        /// update columns as per user preference.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// returns  successful message     
        ///</returns>         
        public APIResult<string> UpdateGridColumnDetails(UpdateColumnDetailRequestModel obj)
        {
            APIResult<string> response = new APIResult<string>();
            try
            {
                response = _gridSearchRepository.UpdateGridColumnDetails(obj);
                return response;
            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Value = "";
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                return response;
            }
        }
    }
}
