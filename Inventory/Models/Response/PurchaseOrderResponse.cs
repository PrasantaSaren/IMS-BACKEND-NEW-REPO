namespace Inventory.Models.Response
{
    public class GetPurchaseOrderListResponse
    {
        public int TotalItem { get; set; }
        public List<PurchaseOrderList>? purchaseOrderList { get; set; }
    }
    public class PurchaseOrderList
    {

    }
    public class PostPurchaseOrderResponse
    {
        public ReturnResponse? returnResponse { get; set; }
    }
    public class GetPurcOrdItemOrJobDetlsResponse
    {
        public int TotalItem { get; set; }
        public List<PurcOrdItemOrJobDetls>? purcOrdItemOrJobDetlsList { get; set; }
    }
    public class PurcOrdItemOrJobDetls
    {

    }
    public class UpdatePurchaseOrderResponse
    {
        public ReturnResponse? returnResponse { get; set; }
    }
}
