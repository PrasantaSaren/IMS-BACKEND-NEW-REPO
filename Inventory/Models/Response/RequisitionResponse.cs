namespace Inventory.Models.Response
{
    public class GetRequisitionListResponse
    {
        public int TotalItem { get; set; }
        public List<GetRequisitionList>? RequisitionList { get; set; }
    }
    public class GetRequisitionList
    {
        public long ReqId { get; set; }
        public string? ReqNo { get; set; }
        public string? Description { get; set; }
        public DateTime? ReqDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? UnitName { get; set; }
    }
}
