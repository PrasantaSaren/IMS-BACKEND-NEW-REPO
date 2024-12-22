using Inventory.Models.Entity;
using Inventory.Models.Response;

namespace Inventory.Models.Request
{
    public class GetRequisitionListRequest
    {
        public BrowseParam? browseParam { get; set; }
        public long? UnitId { get; set; }
        public string? ReqNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? AllRejectAndHold { get; set; }
    }
    public class PostRequisitionRequest
    {
        public string? RefReqNo { get; set; }

        public string? ReqNo { get; set; }

        public DateTime? ReqDate { get; set; }

        public string? ReqType { get; set; }

        public string? Type { get; set; }

        public string? ReqAnmtype { get; set; }

        public string? WorkStatus { get; set; }

        public long? UnitId { get; set; }

        public long? LocationId { get; set; }

        public long? AreaId { get; set; }

        public string? Description { get; set; }

        public string? Justification { get; set; }

        public string? Remarks { get; set; }

        public string? Experiment { get; set; }

        public string? RejectRemarks { get; set; }

        public long? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public long? CompanyId { get; set; }

        public long? CreatedUid { get; set; }

        public DateTime? CreateDate { get; set; }

        public long? UpdatedUid { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? ApprovalStatus { get; set; }

        public byte? ApprovedLevel { get; set; }

        public string? SuppDocName1 { get; set; }

        public byte[]? SuppDoc1 { get; set; }

        public string? SuppDocName2 { get; set; }

        public byte[]? SuppDoc2 { get; set; }

        public string? SuppDocName3 { get; set; }

        public byte[]? SuppDoc3 { get; set; }

        public string? Noofapproval { get; set; }

        public string? ContactNo { get; set; }

        public string? InitBy { get; set; }

        public string? ForwordStatus { get; set; }

        public int? ForwordId { get; set; }

        public DateTime? ForwordDate { get; set; }

        public string? ProcessRemarks { get; set; }

        public int? BackwardId { get; set; }

        public DateTime? BackwardDate { get; set; }
        public List<GetReqDetail>? getReqDetailsList { get; set; }

    }
    public class GetReqDetail
    {
        public long ReqDetailId { get; set; }

        public long? ReqId { get; set; }

        public long? ItemId { get; set; }

        public string? ReqType { get; set; }

        public decimal? Qty { get; set; }

        public decimal? Rate { get; set; }

        public decimal? GrossAmount { get; set; }

        public long? Uomid { get; set; }

        public long? AssetId { get; set; }

        public long? UnitId { get; set; }

        public long? CompanyId { get; set; }

        public long? CreatedUid { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? Description { get; set; }

        //public virtual Company? Company { get; set; }
    }
    public class GetReqItemDetailsRequest
    {
        public BrowseParam? browseParam { get; set; }
    }
    public class GetItemOrJobDetailsRequest
    {
        public BrowseParam? browseParam { get; set; }
    }
    public class UpdateRequisitionRequest: PostRequisitionRequest
    {
        public long ReqId { get; set; }
    }
}
