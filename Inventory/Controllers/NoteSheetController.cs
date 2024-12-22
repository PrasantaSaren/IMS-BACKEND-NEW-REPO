

using Inventory.Models.NoteSheet;
using Inventory.Repository.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Inventory.Controllers
{
    [Route(V)]
    [ApiController]
    public class NoteSheetController : ControllerBase
    {
        private const string V = "api/[controller]";
        private readonly INoteSheetService? _repo;
        private readonly IConfiguration _config;
        public NoteSheetController(INoteSheetService repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }


        [HttpPost("AddUpdateNoteSheetDetails")]
        public async Task<IActionResult> AddUpdateNoteSheetDetails([FromBody] NoteSheetModel _bodyParams)
        {
            try
            {
                var result = await _repo!.AddUpdateNoteSheetDetails(_bodyParams);
                if (result > 0)
                {
                    return Ok(new { Id = result, Message = "Note Sheet Details saved successfully." });
                }
                else if (result == -2)
                {
                    return Ok(new { Id = _bodyParams.NoteSheetID, Message = "Note Sheet Details updated successfully." });
                }
                else
                {
                    return StatusCode(500, "An error occurred while saving the Note Sheet Details.");
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
        [Route("GetNoteSheetListData")]
        public async Task<IActionResult> GetNoteSheetListData(string Item, long NoteSheetID)
        {
            try
            {
                var noteSheet = await _repo.GetNoteSheetListData(Item, NoteSheetID);

                if (noteSheet == null || noteSheet.Count == 0)
                {
                    return Ok(new { message = "No records found.", data = new List<NoteSheetModel>() });
                }

                // Return result
                return Ok(new { message = "Success", data = noteSheet });
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

