using Inventory.Models.Request;
using Inventory.Models.Response;
using Inventory.Repository.IService;
using Inventory.Repository.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        #region Declaration
        private readonly IPurchaseOrderService _iPurchaseOrderService;
        public PurchaseOrderController(IPurchaseOrderService iPurchaseOrderService)
        {
            _iPurchaseOrderService = iPurchaseOrderService;
        }
        #endregion
        [ProducesResponseType(200, Type = typeof(GlobalDropdownResponse))]
        [HttpPost("GetPurchaseOrderList")]
        public async Task<IActionResult> GetPurchaseOrderList(GetPurchaseOrderListRequest getPurchaseOrderListRequest)
        {
            try
            {
                var result = await _iPurchaseOrderService.GetPurchaseOrderList(getPurchaseOrderListRequest);
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
        [HttpPost("PostPurchaseOrder")]
        public async Task<IActionResult> PostPurchaseOrder(PostPurchaseOrderRequest postPurchaseOrderRequest)
        {
            try
            {
                var response = "";
                var result = await _iPurchaseOrderService.PostPurchaseOrder(postPurchaseOrderRequest);
                if (result != null && result.Tables[0] != null)
                {
                    if (result.Tables[0].Rows.Count > 0)
                    {
                        var POID = postPurchaseOrderRequest.POID;
                        if (Convert.ToInt64(result.Tables[0].Rows[0]["@retval"].ToString()) == POID)
                        {
                            response = "Update Successfully!";
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
        [HttpPost("GetPurchaseOrderItemOrJobDetails")]
        public async Task<IActionResult> GetPurchaseOrderItemOrJobDetails(GetPurcOrdItemOrJobDetlsRequest getPurcOrdItemOrJobDetlsRequest)
        {
            try
            {
                var result = await _iPurchaseOrderService.GetPurchaseOrderItemOrJobDetails(getPurcOrdItemOrJobDetlsRequest);
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
    }
}
