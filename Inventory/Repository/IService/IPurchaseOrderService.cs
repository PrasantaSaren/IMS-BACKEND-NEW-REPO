using Inventory.Models.Request;
using Inventory.Models.Response;
using System.Data;

namespace Inventory.Repository.IService
{
    public interface IPurchaseOrderService
    {
        Task<DataSet> GetPurchaseOrderList(GetPurchaseOrderListRequest getPurchaseOrderListRequest);
        Task<DataSet> PostPurchaseOrder(PostPurchaseOrderRequest postPurchaseOrderRequest);
        Task<DataSet> GetPurchaseOrderItemOrJobDetails(GetPurcOrdItemOrJobDetlsRequest getPurcOrdItemOrJobDetlsRequest);
    }
}
