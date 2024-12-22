namespace Inventory.Models.Response
{
    public class GetRequisitionListResponse
    {
        public int TotalItem { get; set; }
        public List<GetRequisition>? requisitionList { get; set; }
    }
    public class GetRequisition
    {
        public long ReqId { get; set; }
        public string? ReqNo { get; set; }
        public string? Description { get; set; }
        public DateTime? ReqDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? UnitName { get; set; }
    }
    public class GetReqItemDetailsResponse
    {
        public int TotalItem { get; set; }
        public List<ReqItemDetails>? reqItemDetailsList { get; set; }
    }
    public class ReqItemDetails
    {
        public string? UnitName { get; set; }
        public string? Description { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Rate { get; set; }

    }
    public class GetItemOrJobDetailsResponse
    {
        public int TotalItem { get; set; }
        public List<ItemOrJobDetails>? itemOrJobDetailslsList { get; set; }
    }
    public class ItemOrJobDetails
    {
        public string? JobName { get; set; }
        public string? Description { get; set; }
        public string? Uom { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Rate { get; set; }

    }
}
