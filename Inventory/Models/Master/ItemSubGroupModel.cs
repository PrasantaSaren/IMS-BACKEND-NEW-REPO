namespace Inventory.Models.Master;

public class ItemSubGroupModel
{
    public long? ItemGroupID { get; set; }
    public long? ItemSubGroupID { get; set; }
    public string? ItemSubGroupName { get; set; }
    public string? SetSubGroupName { get; set; }
    public long? CompanyID { get; set; }
    public long? CreatedUID { get; set; }
}