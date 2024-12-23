namespace Inventory.Models.Master;

public class UnitModel
{
    public long? UnitID { get; set; }
    public string? UnitName { get; set; }
    public string? SBUcode { get; set; }
    public string? ShortCode { get; set; }
    public string? SBUType { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? LogoUrl { get; set; }
    public byte[]? ImageData { get; set; }
    public long? CompanyID { get; set; }
    public long? CreatedUID { get; set; }
}