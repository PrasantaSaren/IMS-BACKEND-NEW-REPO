using Inventory.Models.Request;
using Inventory.Models.Requisition;
using Inventory.Models.Response;
using Inventory.Repository.IService;
using Inventory.Repository.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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
        [ProducesResponseType(200, Type = typeof(GlobalDropdownResponse))]
        [HttpPost("GetRequisitionList")]
        public async Task<IActionResult> GetRequisitionList(GetRequisitionListRequest getRequisitionListRequest)
        {
            try
            {
                var result = await _iRequisitionService.GetRequisitionList(getRequisitionListRequest);
                return Ok(result);
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
            try
            {
                var result = await _iRequisitionService.PostRequisition(PostRequisitionRequest);
                return Ok(result);
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
        public async Task<IActionResult> GetRequisitionItemDetails(GetReqItemDetailsRequest getReqItemDetailsRequest)
        {
            try
            {
                var result = await _iRequisitionService.GetRequisitionItemDetails(getReqItemDetailsRequest);
                return Ok(result);
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
        public async Task<IActionResult> GetItemOrJobDetails(GetItemOrJobDetailsRequest getItemOrJobDetailsRequest)
        {
            try
            {
                var result = await _iRequisitionService.GetItemOrJobDetails(getItemOrJobDetailsRequest);
                return Ok(result);
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
        [HttpPost("UpdateRequisition")]
        public async Task<IActionResult> UpdateRequisition(UpdateRequisitionRequest updateRequisitionRequest)
        {
            try
            {
                var result = await _iRequisitionService.UpdateRequisition(updateRequisitionRequest);
                return Ok(result);
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
    }
}
