namespace Inventory.Models.Master;
using System.ComponentModel;
public class ItemServiceModel
{
    [DefaultValue(0)]
    public long? ItemID { get; set; }
    [DefaultValue(0)]
    public long? HiddenfildID { get; set; }
    [DefaultValue("")]
    public string? Make { get; set; }
    [DefaultValue("")]
    public string? Model { get; set; }
    [DefaultValue("")]
    public string? Serial { get; set; }
    [DefaultValue(0)]
    public long? CompanyID { get; set; }
    [DefaultValue(0)]
    public long? CreatedUID { get; set; }
    [DefaultValue(null)]
    public DateTime? PurchaseDate { get; set; }
    [DefaultValue(null)]
    public DateTime? InstallationDate { get; set; }
    [DefaultValue("")]
    public string? WarrantyTerm { get; set; }
    [DefaultValue("")]
    public string? WarrantyDetail { get; set; }
    [DefaultValue("")]
    public string? LogBookSerial { get; set; }
}