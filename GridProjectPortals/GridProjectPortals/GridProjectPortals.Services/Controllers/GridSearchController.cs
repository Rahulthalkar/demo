using GridProjectPortals.BLL;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GridProjectPortals.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GridSearchController : ControllerBase
    {
        private readonly GenericGridService _genericGridService;
        private readonly IGridSearchRepository _gridSearchRepository;

        private static readonly ILog log = LogManager.GetLogger(typeof(GridSearchController));

        public GridSearchController(IConfiguration configuration,IGridSearchRepository gridSearchRepository)
        {
            _gridSearchRepository = gridSearchRepository;
            _genericGridService=new GenericGridService(configuration,gridSearchRepository);
        }
        [HttpGet]
        [Route("GetGridDetail")]
        [ProducesResponseType(typeof(APIResult<GridColumnDetailsResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetGridDetail(int gridId)
        {
            APIResult<List<GridColumnDetailsResponseModel>> response = new APIResult<List<GridColumnDetailsResponseModel>>();
            try
            {
                response = _genericGridService.GetGridDetail(gridId);
                return Ok(response);

            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Value = null;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                return Ok(response);
            }
        }

        /// <summary>
        ///  Get grid detail list.
        /// </summary>
        /// <param name="pageName">Please provide detail to get grid page details.</param>
        /// <returns cref="GridPageResponseModel">Returns grid detail model</returns>
        /// 
        [HttpGet]
        [Route("GetGridPageDetail")]
        [ProducesResponseType(typeof(APIResult<GridPageResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetGridPageDetail(string pageName)
        {
            APIResult<List<GridPageResponseModel>> response = new APIResult<List<GridPageResponseModel>>();
            try
            {
                response = _genericGridService.GetGridPageDetail(pageName);
                return Ok(response);

            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Value = null;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                return Ok(response);
            }
        }
        [HttpGet]
        [Route("GetGridColumns")]
        [ProducesResponseType(typeof(APIResult<GridColumnResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetGridColumns(int columnId)
        {
            APIResult<List<GridColumnResponseModel>> response = new APIResult<List<GridColumnResponseModel>>();
            try
            {
                response = _genericGridService.GetGridColumns(columnId);
                return Ok(response);

            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Value = null;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                return Ok(response);
            }
        }

        /// <summary>
        /// make api call to user service in BLL to invite users
        /// </summary>
        /// <returns>successful message</returns>
        /// <response code="200">Returns successfully added message</response>
        [HttpPost]
        [Route("UpdateGridColumnDetails")]
        [ProducesResponseType(typeof(APIResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateGridColumnDetails(UpdateColumnDetailRequestModel obj)
        {
            log.Info("UpdateGridColumnDetails method called");
            APIResult<string> objResponse = new APIResult<string>();
            objResponse = _genericGridService.UpdateGridColumnDetails(obj);
            if (objResponse != null)
            {
                return Ok(objResponse);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetOperatorByType")]
        [ProducesResponseType(typeof(APIResult<FilterOperators>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetOperatorByType(string operatorType)
        {
            APIResult<List<FilterOperators>> response = new APIResult<List<FilterOperators>>();
            try
            {
                response =_genericGridService.GetOperatorByType(operatorType);
                return Ok(response);

            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Value = null;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                return Ok(response);
            }
        }
    }
}
