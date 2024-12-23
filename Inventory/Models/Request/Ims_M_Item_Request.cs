namespace Inventory.Models.Request;

public class Ims_M_Item_Request
{
    public long ItemID { get; set; }
    public string? ItemName { get; set; }
    public long? ItemCode { get; set; }
    public string? ItemDescHTML { get; set; }
    public int? UnitID { get; set; }
    public int? LocationID { get; set; }
    public int? AreaID { get; set; }
    public string? Exprement { get; set; }
    public decimal? Rate { get; set; }
    public decimal? ROL { get; set; }
    public decimal? RQTY { get; set; }
    public long? UOMID { get; set; }
    public int? StoreUOMID { get; set; }
    public decimal? SOH { get; set; }
    public long? ItemSubGroupID { get; set; }
    public decimal? VAT { get; set; }
    public decimal? ServiceTax { get; set; }
    public byte? AssetTypeID { get; set; }
    public char? ItemType { get; set; }
    public bool? IsActive { get; set; }
    public long CompanyID { get; set; }
    public long CreatedUID { get; set; }
    public DateTime CreatedDate { get; set; }
    public long? UpdatedUID { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? ItemShortCode { get; set; }
}