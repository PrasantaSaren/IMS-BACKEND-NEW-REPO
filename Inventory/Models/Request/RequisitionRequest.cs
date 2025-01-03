using Inventory.Models.Entity;
using Inventory.Models.Response;
using System.Data;

namespace Inventory.Models.Request
{
    public class GetRequisitionListRequest
    {
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public long? UnitID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ReqNo { get; set; }
        public long? CompanyID { get; set; }
        public string? RequisitionType { get; set; }
        public string? SearchStat { get; set; }
        public long? ReqID { get; set; }
        public string? SbuType { get; set; }
        public long? Layer { get; set; }
        public long? CreatedUID { get; set; }
        public bool? chkrejectHold { get; set; }
        
        //public BrowseParam? browseParam { get; set; }
        //public long? UnitId { get; set; }
        //public string? ReqNo { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        //public bool? AllRejectAndHold { get; set; }
    }
    public class PostRequisitionRequest
    {
        //public DataTable? ReqItemJob { get; set; }

        public Int64 ReqID { get; set; }

        public string? RefReqNo { get; set; }

        public string? ReqANMType { get; set; }

        public string? ReqNo { get; set; }

        public DateTime? ReqDate { get; set; }

        public string? ReqType { get; set; }

        public string? Type { get; set; }

        public string? WorkStatus { get; set; }
        public string? IsRateContract { get; set; }

        public long? UnitID { get; set; }

        public long? LocationID { get; set; }

        public long? AreaID { get; set; }

        public string? Description { get; set; }

        public string? Remarks { get; set; }

        public string? Experiment { get; set; }

        public string? RejectRemarks { get; set; }

        public long? CompanyID { get; set; }

        public long? CreatedUID { get; set; }

        public string? ApprovalStatus { get; set; }

        public byte? ApprovedLevel { get; set; }

        public long? ApprovedBy { get; set; }

        public string? SuppDocName1 { get; set; }

        public byte[]? SuppDoc1 { get; set; }

        public string? SuppDocName2 { get; set; }

        public byte[]? SuppDoc2 { get; set; }

        public string? SuppDocName3 { get; set; }

        public byte[]? SuppDoc3 { get; set; }

        public string? Justification { get; set; }

        public string? ContactNo { get; set; }

        public string? InitBy { get; set; }

        public string? FrdStatus { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public List<GetReqDetail>? getReqDetailsList { get; set; }

    }
    public class GetReqDetail
    {
        public string? ItemName { get; set; }

        public long? ItemId { get; set; }

        public string? Description { get; set; }
        public long? uomn { get; set; }

        public long? uom { get; set; }

        public string? assetn { get; set; }

        public string? asset { get; set; }

        public decimal? Qty { get; set; }

        public decimal? Rate { get; set; }

        public decimal? GrossAmount { get; set; }
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
