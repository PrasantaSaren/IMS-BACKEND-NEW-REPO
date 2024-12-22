using Inventory.Models.Quotation;
using Inventory.Models.Requisition;
using Inventory.Repository.IService;
using Inventory.Repository.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Inventory.Controllers
{
    [Route(V)]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private const string V = "api/[controller]";
        private readonly IQuotationRepository? _repo;
        private readonly IConfiguration _config;
        public QuotationController(IQuotationRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }
        

        [HttpPost("AddUpdateQuotationDetails")]
        public async Task<IActionResult> AddUpdateQuotationDetails([FromBody] QuotationModel _bodyParams)
        {
            try
            {
                var result = await _repo!.AddUpdateQuotationDetails(_bodyParams);
                if (result > 0)
                {
                    return Ok(new { Id = result, Message = "Quotation Details saved successfully." });
                }
                else if(result == -2)
                {
                    return Ok(new { Id = _bodyParams.QuotationID, Message = "Quotation Details updated successfully." });
                }
                else
                {
                    return StatusCode(500, "An error occurred while saving the Quotation Details.");
                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, new { message = "A database error occurred.", error = sqlEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetQuotationListData")]
        public async Task<IActionResult> GetQuotationListData(string Item, long QuotationID)
        {
            try
            {
                var quotations = await _repo.GetQuotationListData(Item, QuotationID);

                if (quotations == null || quotations.Count == 0)
                {
                    return Ok(new { message = "No records found.", data = new List<QuotationModel>() });
                }

                // Return result
                return Ok(new { message = "Success", data = quotations });
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, new { message = "A database error occurred.", error = sqlEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

    }
}
