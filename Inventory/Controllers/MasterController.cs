using Common_Helper.CommonHelper;
using Inventory.AppCode;
using Inventory.Models.Master;
using Inventory.Models.Request;
using Inventory.Models.Response;
using Inventory.Repository.IService;
using Inventory.Repository.Service;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MasterController : ControllerBase
{
    #region Declaration
    private readonly IConfiguration _configuration = null!;
    private readonly ITokenService _tokenService = null!;
    private readonly ISessionHelper _sessionHelper = null!;
    AuditLogHelper _auditLogHelper = new AuditLogHelper();
    private readonly GeneralUtilityService service = new();
#pragma warning disable IDE0290 // Use primary constructor
    public MasterController(IMasterRepository masterRepository, ITokenService tokenService, ISessionHelper sessionHelper, IConfiguration configuration)
#pragma warning restore IDE0290 // Use primary constructor
    {
        _configuration = configuration;
        _masterRepository = masterRepository;
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _sessionHelper = sessionHelper ?? throw new ArgumentNullException(nameof(sessionHelper));
    }
    #endregion
    private void InitialCall()
    {
        _sessionHelper.GetSessionClaim();
        _auditLogHelper.ClientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        _auditLogHelper.ClientEmail = _sessionHelper.Email;
    }
    private readonly IMasterRepository _masterRepository;

    #region UOM
    [HttpPost("SaveUOM")]
    public IActionResult SaveUOM([FromBody] Ims_M_UOM uom)
    {
        var result = _masterRepository.InsertOrUpdateUOM(uom);
        return result switch
        {
            > 0 => Ok("UOM saved successfully."),
            -7 => Conflict("UOM already exists."),
            _ => BadRequest("Failed to save UOM.")
        };
    }

    [HttpGet("GetUOMs")]
    public IActionResult GetUOMs()
    {
        var ds = _masterRepository.GetUOMs();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }

    [HttpGet("SearchUOM")]
    public IActionResult SearchUOM(string uomName)
    {
        var ds = _masterRepository.SearchUOM(uomName);
        if (ds.Tables[0].Rows.Count <= 0) return NotFound("No matching UOM records found.");
        var uomData = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(uomData);
    }
    #endregion

    #region Unit/SBU
    [HttpPost("PostAddUpdateUnitDetails")]
    public IActionResult AddUpdateUnitDetails([FromBody] UnitModel unit)
    {
        var result = _masterRepository.AddUpdateUnitDetails(unit);
        return result switch
        {
            > 0 => Ok("Unit Details saved successfully."),
            -2 => Ok("Unit Details updated successfully."),
            -7 => Conflict("Unit already exists."),
            _ => BadRequest("Failed to save Unit.")
        };
    }

    [HttpGet("GetUnitDetailsList")]
    public IActionResult GetUnitDetailsList()
    {
        var ds = _masterRepository.GetUnitDetailsList();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }
    #endregion

    #region Area/Department
    [HttpGet("GetAreaDepartmentDetailsList")]
    public IActionResult GetAreaDepartmentDetailsList()
    {
        var ds = _masterRepository.GetAreaDepartmentDetailsList();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }

    [HttpPost("PostAddUpdateAreaDepartmentDetails")]
    public IActionResult AddUpdateAreaDepartmentDetails([FromBody] AreaDepartmentModel area)
    {
        var result = _masterRepository.AddUpdateAreaDepartmentDetails(area);
        return result switch
        {
            > 0 => Ok("Area Details saved successfully."),
            -2 => Ok("Area Details updated successfully."),
            -7 => Conflict("Area already exists."),
            _ => BadRequest("Failed to save Area.")
        };
    }
    #endregion

    #region Unit Location
    [HttpGet("GetUnitLocationDetailsList")]
    public IActionResult GetUnitLocationDetailsList()
    {
        var ds = _masterRepository.GetUnitLocationDetailsList();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }

    [HttpPost("PostAddUpdateUnitLocationDetails")]
    public IActionResult AddUpdateUnitLocationDetails([FromBody] UnitLocationModel unitLocation)
    {
        var result = _masterRepository.AddUpdateUnitLocationDetails(unitLocation);
        return result switch
        {
            > 0 => Ok("Unit Location Details saved successfully."),
            -2 => Ok("Unit Location Details updated successfully."),
            -7 => Conflict("Unit Location already exists."),
            _ => BadRequest("Failed to save Unit Location.")
        };
    }
    #endregion

    #region Job Type Section
    [HttpGet("GetJobTypeDetailsList")]
    public IActionResult GetJobTypeDetailsList()
    {
        var ds = _masterRepository.GetJobTypeDetailsList();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }

    [HttpPost("PostAddUpdateJobTypeDetails")]
    public IActionResult AddUpdateJobTypeDetails([FromBody] JobTypeModel jobType)
    {
        var result = _masterRepository.AddUpdateJobTypeDetails(jobType);
        return result switch
        {
            > 0 => Ok("Job type details saved successfully."),
            -2 => Ok("Job type details updated successfully."),
            -7 => Conflict("Job type already exists."),
            _ => BadRequest("Failed to save Job Type.")
        };
    }
    #endregion

    #region Job Section
    [HttpGet("GetJobDetailsList")]
    public IActionResult GetJobDetailsList()
    {
        var ds = _masterRepository.GetJobDetailsList();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }

    [HttpPost("PostAddUpdateJobDetails")]
    public IActionResult AddUpdateJobDetails([FromBody] JobModel job)
    {
        var result = _masterRepository.AddUpdateJobDetails(job);
        return result switch
        {
            > 0 => Ok("Job Details saved successfully."),
            -2 => Ok("Job Details updated successfully."),
            -7 => Conflict("Job already exists."),
            _ => BadRequest("Failed to save Job.")
        };
    }
    #endregion

    #region Item Group Section
    [HttpGet("GetItemGroupDetailsList")]
    public IActionResult GetItemGroupDetailsList()
    {
        var ds = _masterRepository.GetItemGroupDetailsList();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }

    [HttpPost("PostAddUpdateItemGroupDetails")]
    public IActionResult AddUpdateItemGroupDetails([FromBody] ItemGroupModel itemGroup)
    {
        var result = _masterRepository.AddUpdateItemGroupDetails(itemGroup);
        return result switch
        {
            > 0 => Ok("Item Group details saved successfully."),
            -2 => Ok("Item Group details updated successfully."),
            -7 => Conflict("Item Group already exists."),
            _ => BadRequest("Failed to save Item Group.")
        };
    }
    #endregion

    #region Item Sub Group Section
    [HttpGet("GetItemSubGroupDetailsList")]
    public IActionResult GetItemSubGroupDetailsList()
    {
        var ds = _masterRepository.GetItemSubGroupDetailsList();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }

    [HttpPost("PostAddUpdateItemSubGroupDetails")]
    public IActionResult AddUpdateItemSubGroupDetails([FromBody] ItemSubGroupModel itemSubGroup)
    {
        var result = _masterRepository.AddUpdateItemSubGroupDetails(itemSubGroup);
        return result switch
        {
            > 0 => Ok("Item Sub Group details saved successfully."),
            -2 => Ok("Item Sub Group details updated successfully."),
            -7 => Conflict("Item Sub Group already exists."),
            _ => BadRequest("Failed to save Item Sub Group.")
        };
    }
    #endregion

    #region Bank Section
    [HttpGet("GetBankDetailsList")]
    public IActionResult GetBankDetailsList()
    {
        var ds = _masterRepository.GetBankDetailsList();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }

    [HttpPost("PostAddUpdateBankDetails")]
    public IActionResult AddUpdateBankDetails([FromBody] BankModel bank)
    {
        var result = _masterRepository.AddUpdateBankDetails(bank);
        return result switch
        {
            > 0 => Ok("Bank details saved successfully."),
            -2 => Ok("Bank details updated successfully."),
            -7 => Conflict("Bank already exists."),
            _ => BadRequest("Failed to save Bank.")
        };
    }
    #endregion

    #region Vendor Section
    [HttpGet("GetVendorDetailsList")]
    public IActionResult GetVendorDetailsList()
    {
        var ds = _masterRepository.GetVendorDetailsList();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }

    [HttpPost("PostAddUpdateVendorDetails")]
    public IActionResult AddUpdateVendorDetails([FromBody] VendorModel vendor)
    {
        var result = _masterRepository.AddUpdateVendorDetails(vendor);
        return result switch
        {
            > 0 => Ok("Vendor details saved successfully."),
            -2 => Ok("Vendor details updated successfully."),
            -7 => Conflict("Vendor mobile no already exists."),
            _ => BadRequest("Failed to save Vendor.")
        };
    }
    #endregion

    #region Item Section
    [HttpPost("PostAddUpdateItemEntryDetails")]
    public IActionResult AddUpdateItemEntryDetails([FromBody] ItemEntryModel itemEntryModel)
    {
        var result = _masterRepository.AddUpdateItemEntryDetails(itemEntryModel);
        return result switch
        {
            > 0 => Ok("Item details saved successfully."),
            -2 => Ok("Item details updated successfully."),
            -7 => Conflict("Item Name already exists."),
            _ => BadRequest("Failed to save Item.")
        };
    }

    [HttpPost("PostAddUpdateItemServiceDetails")]
    public IActionResult AddUpdateItemServiceDetails([FromBody] ItemServiceModel itemServiceModel)
    {
        var result = _masterRepository.AddUpdateItemServiceDetails(itemServiceModel);
        return result switch
        {
            > 0 => Ok("Item Service details saved successfully."),
            -2 => Ok("Item Service details updated successfully."),
            _ => BadRequest("Failed to save Item Service.")
        };
    }

    [HttpPost("PostUpdateItemAnnualMaintenanceDetails")]
    public IActionResult UpdateItemAnnualMaintenanceDetails([FromBody] ItemMaintenanceModel annualMaintenance)
    {
        var result = _masterRepository.UpdateItemAnnualMaintenanceDetails(annualMaintenance);
        return result switch
        {
            > 0 => Ok("Annual maintenance updated successfully."),
            _ => BadRequest("Failed to update Annual maintenance.")
        };
    }

    [HttpGet("GetItemEntryDetailsList")]
    public IActionResult GetItemEntryDetailsList()
    {
        var ds = _masterRepository.GetItemEntryDetailsList();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return Ok(new { message = "No records found.", data = new List<Dictionary<string, object>>() });
        }
        var output = GeneralUtilityService.ConvertDataTableToDictionaryList(ds.Tables[0]);
        return Ok(output);
    }
    #endregion

}
