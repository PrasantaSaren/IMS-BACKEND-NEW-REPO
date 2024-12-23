namespace Inventory.Models.Master;

public class UnitLocationModel
{
    public long? LocationID { get; set; }
    public long? AreaID { get; set; }
    public string? LocationName { get; set; }
    public string? LocationCode { get; set; }
    public long? UnitID { get; set; }
    public long? CompanyID { get; set; }
    public long? CreatedUID { get; set; }
}