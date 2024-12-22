using System.ComponentModel;

namespace Inventory.Models.NoteSheet
{
    public class NoteSheetModel
    {
        [DefaultValue("")]
        public string? NoteSheetNo { get; set; }
        [DefaultValue(0)]
        public long? NoteSheetID { get; set; }
        [DefaultValue(0)]
        public long? QuotationID { get; set; }
        [DefaultValue("")]
        public string? NoteSheetANMtype { get; set; }
        [DefaultValue(0)]
        public long? ReqID { get; set; }
        [DefaultValue(0)]
        public long? CV_ID { get; set; }
        [DefaultValue(null)]
        public DateTime? NoteSheetDate { get; set; }
        [DefaultValue("")]
        public string? PurchaseType { get; set; }
        [DefaultValue("")]
        public string? Type { get; set; }
        [DefaultValue("")]
        public string? IsRateContract { get; set; }
        [DefaultValue("")]
        public string? WorkStatus { get; set; }
        [DefaultValue(0)]
        public long? UnitID { get; set; }
        [DefaultValue("")]
        public string? UnitName { get; set; }
        [DefaultValue(0)]
        public long? LocationID { get; set; }
        [DefaultValue(0)]
        public long? AreaID { get; set; }
        [DefaultValue("")]
        public string? AreaName { get; set; }
        [DefaultValue("")]
        public string? Description { get; set; }
        [DefaultValue("")]
        public string? Remarks { get; set; }
        [DefaultValue("")]
        public string? Justification { get; set; }
        [DefaultValue("")]
        public string? TermCond { get; set; }
        [DefaultValue(0.00)]
        public decimal GrossAmount { get; set; }
        [DefaultValue(0.00)]
        public decimal DeliveryCharges { get; set; }
        [DefaultValue(0.00)]
        public decimal TotalDiscountAmt { get; set; }
        [DefaultValue(0.00)]
        public decimal TotalDiscountPer { get; set; }
        [DefaultValue(0.00)]
        public decimal NetAmount { get; set; }
        [DefaultValue(0)]
        public long? CompanyID { get; set; }
        [DefaultValue(0)]
        public long? CreatedUID { get; set; }
        [DefaultValue("")]
        public string? ApprovalStatus { get; set; }
        [DefaultValue(0)]
        public long? ApprovedLevel { get; set; }
        [DefaultValue("")]
        public string? SuppDocName1 { get; set; }
        [DefaultValue(null)]
        public byte[]? SuppDoc1 { get; set; }
        [DefaultValue("")]
        public string? SuppDocName2 { get; set; }
        [DefaultValue(null)]
        public byte[]? SuppDoc2 { get; set; }
        [DefaultValue("")]
        public string? SuppDocName3 { get; set; }
        [DefaultValue(null)]
        public byte[]? SuppDoc3 { get; set; }
        [DefaultValue(null)]
        public DateTime? ApproveDate { get; set; }
        public List<NoteSheetItemJob> NoteItemJob { get; set; } = new List<NoteSheetItemJob>();
    }
    public class NoteSheetItemJob
    {
        [DefaultValue(0)]
        public long NoteSheetDetailID { get; set; }
        [DefaultValue(0)]
        public long NoteSheetID { get; set; }
        [DefaultValue(0)]
        public long ItemID { get; set; }
        [DefaultValue("")]
        public string ItemName { get; set; }
        [DefaultValue("")]
        public string Description { get; set; }
        [DefaultValue(0)]
        public long AssetType { get; set; }
        [DefaultValue("")]
        public string uom { get; set; }
        [DefaultValue("")]
        public string UoMName { get; set; }
        
        [DefaultValue(0.00)]
        public decimal Qty { get; set; }
        [DefaultValue(0.00)]
        public decimal Rate { get; set; }
        [DefaultValue(0.00)]
        public decimal GrossAmount { get; set; }
        [DefaultValue(0.00)]
        public decimal Vat { get; set; }
        [DefaultValue(0.00)]
        public decimal Stex { get; set; }
        [DefaultValue(0.00)]
        public decimal dis { get; set; }
        [DefaultValue(0.00)]
        public decimal cst { get; set; }
        [DefaultValue(0.00)]
        public decimal NetAmount { get; set; }

    }
}
