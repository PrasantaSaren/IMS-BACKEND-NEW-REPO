namespace Inventory.Models.Master;
using System.ComponentModel;
public class ItemMaintenanceModel
{
    [DefaultValue(0)]
    public long? ItemID { get; set; }
    [DefaultValue(0)]
    public long? AmcProviderID { get; set; }
    [DefaultValue(0)]
    public long? AmcValue { get; set; }
    [DefaultValue("")]
    public string? AmcType { get; set; }
    [DefaultValue(null)]
    public DateTime? AmcStartDate { get; set; }
    [DefaultValue(null)]
    public DateTime? AmcRenewDate { get; set; }
    [DefaultValue("")]
    public string? Status { get; set; }
    [DefaultValue(0)]
    public long? ItemDetailID { get; set; }
    [DefaultValue(0)]
    public long? SupplierID { get; set; }
    [DefaultValue("")]
    public string? SupplierContactNo { get; set; }
}