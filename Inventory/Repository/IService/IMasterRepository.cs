using System.Data;
using Inventory.Models.Master;
using Inventory.Models.Request;
using Inventory.Models.Response;

namespace Inventory.Repository.IService;

public interface IMasterRepository
{
    #region UOM
    int InsertOrUpdateUOM(Ims_M_UOM uom);
    DataSet GetUOMs();
    DataSet SearchUOM(string uomName);
    #endregion

    #region Unit/SBU
    int AddUpdateUnitDetails(UnitModel unit);
    DataSet GetUnitDetailsList();
    #endregion

    #region Area/Department
    DataSet GetAreaDepartmentDetailsList();
    int AddUpdateAreaDepartmentDetails(AreaDepartmentModel area);
    #endregion

    #region Unit Location
    DataSet GetUnitLocationDetailsList();
    int AddUpdateUnitLocationDetails(UnitLocationModel unitLocation);
    #endregion

    #region Job Type Section
    DataSet GetJobTypeDetailsList();
    int AddUpdateJobTypeDetails(JobTypeModel jobType);
    #endregion

    #region Job Section
    DataSet GetJobDetailsList();
    int AddUpdateJobDetails(JobModel job);
    #endregion

    #region Item Group Section
    DataSet GetItemGroupDetailsList();
    int AddUpdateItemGroupDetails(ItemGroupModel itemGroup);
    #endregion

    #region Item Sub Group Section
    DataSet GetItemSubGroupDetailsList();
    int AddUpdateItemSubGroupDetails(ItemSubGroupModel itemSubGroup);
    #endregion

    #region Bank Section
    DataSet GetBankDetailsList();
    int AddUpdateBankDetails(BankModel bank);
    #endregion

    #region Vendor Section
    DataSet GetVendorDetailsList();
    int AddUpdateVendorDetails(VendorModel vendor);
    #endregion

    #region Item  Section
    int AddUpdateItemEntryDetails(ItemEntryModel itemEntryModel);
    int AddUpdateItemServiceDetails(ItemServiceModel itemServiceModel);
    int UpdateItemAnnualMaintenanceDetails(ItemMaintenanceModel annualMaintenance);
    DataSet GetItemEntryDetailsList();
    #endregion

}
