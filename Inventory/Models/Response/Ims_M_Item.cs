// ReSharper disable IdentifierTypo
namespace Inventory.Models.Response;

public class Ims_M_Item
{
    public long ItemGroupID { get; set; }
    public string? ItemGroupName { get; set; }
    public long ItemSubGroupID { get; set; }
    public long UnitID { get; set; }
    public long LocationID { get; set; }
    public long AreaID { get; set; }
    public long UOMID { get; set; }
    public long SOH { get; set; }
    public long StoreUOMID { get; set; }
    public string? ItemType { get; set; }
    public string? ROL { get; set; }
    public long CompanyID { get; set; }
    public long CreatedUID { get; set; }
    public string? RQTY { get; set; }
    public string? AssetTypeID { get; set; }
    public string? Exprement { get; set; }
    public string? ItemName { get; set; }
    public string? ItemDescHTML { get; set; }
    public decimal VAT { get; set; }
    public decimal Rate { get; set; }
    public long ItemID { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? Serial { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime InstallationDate { get; set; }
    public string? WarrantyTerm { get; set; }
    public string? WarrantyDetail { get; set; }
    public string? LogBookSerial { get; set; }

    public long AmcProviderID { get; set; }
    public decimal AmcValue { get; set; }

    public string? AmcType { get; set; }

    public string? Status { get; set; }

    public DateTime AmcStartDate { get; set; }
    public DateTime AmcRenewDate { get; set; }

    public long SupplierID { get; set; }

    public string? SupplierContactNo { get; set; }
    public long itemdetilsID { get; set; }
    public long HiddenfildID { get; set; }
    public string? ItemSubGroupName { get; set; }
    public string? setsubgroupname { get; set; }
    public string? setgroupname { get; set; }
}
