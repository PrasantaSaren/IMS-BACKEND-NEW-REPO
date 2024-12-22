using Inventory.Models.Request;
using Inventory.Models.Response;

namespace Inventory.Repository.IService
{
    public interface IRequisitionService
    {
        Task<GetRequisitionListResponse> GetRequisitionList(GetRequisitionListRequest getRequisitionListRequest);
        Task<bool> PostRequisition(PostRequisitionRequest PostRequisitionRequest);

    }
}
