namespace Inventory.Models.Master;
using System.ComponentModel;
public class ItemEntryModel
{
    [DefaultValue(0)]
    public long? ItemSubGroupID { get; set; }
    [DefaultValue(0)]
    public long? HiddenfildID { get; set; }
    [DefaultValue(0)]
    public long? UnitID { get; set; }
    [DefaultValue(0)]
    public long? LocationID { get; set; }
    [DefaultValue(0)]
    public long? AreaID { get; set; }
    [DefaultValue(0)]
    public long? UOMID { get; set; }
    [DefaultValue(0)]
    public long? ROL { get; set; }
    [DefaultValue(0)]
    public long? RQTY { get; set; }
    [DefaultValue(0)]
    public long? SOH { get; set; }
    [DefaultValue(0)]
    public long? StoreUOMID { get; set; }
    [DefaultValue(0.0)]
    public decimal? Rate { get; set; }
    [DefaultValue(0.0)]
    public decimal? VAT { get; set; }
    [DefaultValue("")]
    public string? ItemName { get; set; }
    [DefaultValue("")]
    public string? Exprement { get; set; }
    [DefaultValue("")]
    public string? ItemType { get; set; }
    [DefaultValue(0)]
    public long? CompanyID { get; set; }
    [DefaultValue(0)]
    public long? CreatedUID { get; set; }
    [DefaultValue("")]
    public string? ItemDescHTML { get; set; }
    [DefaultValue("")]
    public string? AssetTypeID { get; set; }
}