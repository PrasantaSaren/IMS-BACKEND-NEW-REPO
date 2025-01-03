using Inventory.Models.Request;
using Inventory.Models.Response;
using System.Data;

namespace Inventory.Repository.IService
{
    public interface IRequisitionService
    {
        Task<DataSet> GetRequisitionList(GetRequisitionListRequest getRequisitionListRequest);
        Task<DataSet> PostRequisition(PostRequisitionRequest PostRequisitionRequest);
        Task<DataSet> GetRequisitionItemDetails(long ReqID);
        Task<DataSet> GetItemOrJobDetails(long ReqID);
        //Task<bool> UpdateRequisition(UpdateRequisitionRequest updateRequisitionRequest);

    }
}
