using Inventory.Models.Request;
using Inventory.Models.Response;
using Inventory.Repository.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalController : ControllerBase
    {
        #region Declaration
        private readonly IGlobalService _IGlobalService;
        public GlobalController(IGlobalService iGlobalService)
        {
            _IGlobalService = iGlobalService;
        }
        #endregion
        [ProducesResponseType(200, Type = typeof(GlobalDropdownResponse))]
        [HttpPost("GetGlobalDropdownList")]
        public async Task<IActionResult> GetGlobalDropdownList(GlobalDropdownRequest globalDropdownRequest)
        {
            if (string.IsNullOrEmpty(globalDropdownRequest.TableName)) return BadRequest(new { message = "TableName field is required." });
            if (string.IsNullOrEmpty(globalDropdownRequest.NameFieldName)) return BadRequest(new { message = "NameFieldName field is required." });
            if (string.IsNullOrEmpty(globalDropdownRequest.IdFieldName)) return BadRequest(new { message = "IdFieldName field is required." });
            try
            {
                var result = await _IGlobalService.GetGlobalDropdownList<GlobalDropdownResponse>(globalDropdownRequest);
                if (result == null || !result.Any()) return NotFound(new { message = "No data found for the provided request." });
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

        [ProducesResponseType(200, Type = typeof(JsonConvert))]
        [HttpPost("GetGlobalSelectTable")]
        public async Task<IActionResult> GetGlobalSelectTable(GlobalSelectTableRequest globalSelectTableRequest)
        {
            if (string.IsNullOrEmpty(globalSelectTableRequest.TableName)) return BadRequest(new { message = "TableName field is required." });
            try
            {
                var result = await _IGlobalService.GetGlobalSelectTable(globalSelectTableRequest);
                var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                return Ok(json);
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

        [ProducesResponseType(200, Type = typeof(string))]
        [HttpPost("GetGlobalSelectTableRow")]
        public async Task<IActionResult> GetGlobalSelectTableRow(GlobalSelectTableRowRequest globalSelectTableRowRequest)
        {
            if (string.IsNullOrEmpty(globalSelectTableRowRequest.TableName)) return BadRequest(new { message = "TableName field is required." });
            if (string.IsNullOrEmpty(globalSelectTableRowRequest.WhereFieldName)) return BadRequest(new { message = "WhereFieldName field is required." });
            if (string.IsNullOrEmpty(globalSelectTableRowRequest.WhereFieldValue)) return BadRequest(new { message = "WhereFieldValue field is required." });
            try
            {
                var result = await _IGlobalService.GetGlobalSelectTableRow(globalSelectTableRowRequest);
                //var json = JsonConvert.SerializeObject(result, Formatting.Indented);
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

        [ProducesResponseType(200, Type = typeof(string))]
        [HttpPost("GlobalLogicalDelete")]
        public async Task<IActionResult> GlobalLogicalDelete(GlobalLogicalDeleteRequest globalLogicalDeleteRequest)
        {
            if (string.IsNullOrEmpty(globalLogicalDeleteRequest.TableName)) return BadRequest(new { message = "TableName field is required." });
            if (string.IsNullOrEmpty(globalLogicalDeleteRequest.IsActiveFieldName)) return BadRequest(new { message = "IsActiveFieldName field is required." });
            if (string.IsNullOrEmpty(globalLogicalDeleteRequest.IsActiveFieldValue)) return BadRequest(new { message = "IsActiveFieldValue field is required." });
            if (string.IsNullOrEmpty(globalLogicalDeleteRequest.WhereFieldName)) return BadRequest(new { message = "WhereFieldName field is required." });
            if (string.IsNullOrEmpty(globalLogicalDeleteRequest.WhereFieldValue)) return BadRequest(new { message = "WhereFieldValue field is required." });
            try
            {
                var result = await _IGlobalService.GlobalLogicalDelete(globalLogicalDeleteRequest);
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
