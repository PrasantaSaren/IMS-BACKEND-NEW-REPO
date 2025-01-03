using Inventory.Models.Entity;
using Inventory.Models.Request;
using Inventory.Models.Requisition;
using Inventory.Models.Response;
using Inventory.Repository.IService;
using Inventory.Repository.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitionController : ControllerBase
    {
        #region Declaration
        private readonly IRequisitionService _iRequisitionService;
        public RequisitionController(IRequisitionService iRequisitionService)
        {
            _iRequisitionService = iRequisitionService;
        }
        #endregion
        [ProducesResponseType(200, Type = typeof(JsonConvert))]
        [HttpPost("GetRequisitionList")]
        public async Task<IActionResult> GetRequisitionList(GetRequisitionListRequest getRequisitionListRequest)
        {
            try
            {
                var result = await _iRequisitionService.GetRequisitionList(getRequisitionListRequest);
                var jsonData = JsonConvert.SerializeObject(result, Formatting.Indented);
                return Ok(jsonData);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // LogError
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request. " + ex.Message });
            }
        }
        [ProducesResponseType(200, Type = typeof(GlobalDropdownResponse))]
        [HttpPost("PostRequisition")]
        public async Task<IActionResult> PostRequisition(PostRequisitionRequest PostRequisitionRequest)
        {
            string response = "";
            try
            {
                var result = await _iRequisitionService.PostRequisition(PostRequisitionRequest);
                if (result.Tables[0].Rows.Count > 0)
                {
                    long ReqId = PostRequisitionRequest.ReqID;
                    if (Convert.ToInt64(result.Tables[0].Rows[0]["@retval"].ToString()) == ReqId)
                    {
                        response = "Upadte Successfully!";
                    }
                    else if (Convert.ToInt64(result.Tables[0].Rows[0]["@retval"].ToString()) > 0)
                    {
                        response = "Save Successfully!";
                    }
                    else if (Convert.ToInt64(result.Tables[0].Rows[0]["@retval"].ToString()) == -1)
                    {
                        response = "Not Save Requisition!";
                    }
                }
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // LogError
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request. " + ex.Message });
            }
        }
        [HttpPost("GetRequisitionItemDetails")]
        public async Task<IActionResult> GetRequisitionItemDetails(long ReqID)
        {  
            try
            {
                var result = await _iRequisitionService.GetRequisitionItemDetails(ReqID);
                var jsonData = JsonConvert.SerializeObject(result, Formatting.Indented);
                return Ok(jsonData);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // LogError
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request. " + ex.Message });
            }
        }
        [HttpPost("GetItemOrJobDetails")]
        public async Task<IActionResult> GetItemOrJobDetails(long ReqID)
        {
            try
            {
                var result = await _iRequisitionService.GetItemOrJobDetails(ReqID);
                var jsonData = JsonConvert.SerializeObject(result, Formatting.Indented);
                return Ok(jsonData);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // LogError
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request. " + ex.Message });
            }
        }
        //[HttpPost("UpdateRequisition")]
        //public async Task<IActionResult> UpdateRequisition(UpdateRequisitionRequest updateRequisitionRequest)
        //{
        //    try
        //    {
        //        var result = await _iRequisitionService.UpdateRequisition(updateRequisitionRequest);
        //        return Ok(result);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        // LogError
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request. " + ex.Message });
        //    }
        //}
    }
}
