using System.Data;
using Inventory.Models.Requisition;
namespace Inventory.Repository.IService;
public interface IRequisitionRepository
{
    UnitList GetRequisitionUnitDetails(string type, long userID);
    AreaList GetRequisitionUnitAreaDetails(long unitId, long companyId);
    Task<DataSet> GetRequisitionUnitAreaLocationDetails(long unitId, long areaId);
    Task<long> InsertRequisitionDetails(Ims_Requisition_Model _params);
    Task<DataSet> GetRequisitionSearchList(Ims_Requisition_ReqSearchList_Reponse _params);




    long InsertUpdateArea(Ims_Requisition_AreaRequest _params);
    
    Task<DataSet> GetRemarks(long reqId);
    
    Task<long> InsertOrUpdateUnitLOcation(Ims_Requisition_UnitLocation_Request _params);

    #region Requisition List
    Task<DataSet> GetRequisitionAllItem(long reqId);
    Task<long> InsertOrUpdateRequisitionApproval(Ims_Requisition_ReqApproval _params);
    #endregion
    
    Task<long> UpdateRequisitionDetails(Ims_Requisition_Model _params);
}
