using System.Data;

namespace Inventory.Models.Request
{
    public class GetPurchaseOrderListRequest
    {
        //public BrowseParam? browseParam { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? UnitId { get; set; }
        public long? CV_ID { get; set; }
        public string? PoNo { get; set; }
        public long? CompanyID { get; set; }
        public string? SearchStat { get; set; }
        public string? SbuType { get; set; }
        public long? UserId { get; set; }
    }
    public class PostPurchaseOrderRequest
    {
        public Int64 ATOPOID { get; set; }

        public Int64 POID { get; set; }
        public string? Type { get; set; }
        public string? PurchaseType { get; set; }
        public Int64 QuotationID { get; set; }
        public Int64 NoteSheetID { get; set; }
        public string? PONo { get; set; }
        public string? PCDays { get; set; }
        public string? RefPONo { get; set; }
        public DateTime PODate { get; set; }
        public Int64? CV_ID { get; set; }
        public Int64? UnitID { get; set; }
        public Int64 LocationID { get; set; }
        public Int64 AreaID { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalDiscountAmt { get; set; }
        public decimal TotalDiscountPer { get; set; }
        public decimal DeliveryCharges { get; set; }
        public decimal NetAmount { get; set; }
        public string? Description { get; set; }
        public string? Remarks { get; set; }
        public string? TermCond { get; set; }
        public string? TermCond1 { get; set; }
        public Int64 ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Int64 CompanyID { get; set; }
        public Int64 CreatedUID { get; set; }
        public DateTime CreateDate { get; set; }
        public Int64 UpdatedUID { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovedLevel { get; set; }
        public string? SuppDocName1 { get; set; }
        public string? SuppDocName2 { get; set; }
        public string? SuppDocName3 { get; set; }
        public byte[]? SuppDoc1 { get; set; }
        public byte[]? SuppDoc2 { get; set; }
        public byte[]? SuppDoc3 { get; set; }
        public string? Experiment { get; set; }
        public int DigitalSign { get; set; }
        public int Layer { get; set; }
        public string? POLabelType { get; set; }
        public string? POANMType { get; set; }
        public string? Justification { get; set; }
        public Int64 ReqID { get; set; }
        public string? FileNo { get; set; }
        public string? Source { get; set; }
        public decimal AdjustAmt { get; set; }
        public DateTime? StartDate { get; set; }
        public string? SearchStat { get; set; }
        public DateTime? EndDate { get; set; }
        public string? SBUTYPE { get; set; }

        public string? RDateFrom { get; set; }
        public string? RDateTo { get; set; }

        public bool FLAG { get; set; }

        public string? IsRateContract { get; set; }

        public Int64? BillinUnitID { get; set; }
        public DataTable? POItemJob { get; set; }

        //Add New On 24/08/2017
        public string? MonthwiseBillType { get; set; }
        //End Add New On 24/08/2017

        //Add New On 22/03/2018
        public string? POCancelDate { get; set; }
        public Int64? POCancelBy { get; set; }
        public string? POCancelReason { get; set; }
        public bool? IsPOCancel { get; set; }
        //Add New On 22/03/2018
        public string? ProformaInvoiceType { get; set; }
        public Nullable<decimal> ProformaAmount { get; set; }
        public long? UserID { get; set; }
        public List<PurchaseOrderDetails>? purchaseOrderDetailsList { get; set; }

    }
    public class PurchaseOrderDetails
    {
        public string? ItemName { get; set; }
        public string? ItemID { get; set; }
        public string? Description { get; set; }
        public string? AssetType { get; set; }
        public string? uom { get; set; }
        public string? Qty { get; set; }
        public string? Rate { get; set; }
        public string? GrossAmount { get; set; }
        public string? Vat { get; set; }
        public string? Stex { get; set; }
        public string? dis { get; set; }
        public string? cst { get; set; }
        public string? NetAmount { get; set; }
    }
    public class GetPurcOrdItemOrJobDetlsRequest
    {
        public BrowseParam? browseParam { get; set; }
    }
    public class UpdatePurchaseOrderRequest: PostPurchaseOrderRequest
    {
        public long? PoId { get; set; }
    }
}
