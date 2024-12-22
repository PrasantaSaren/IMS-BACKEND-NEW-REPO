namespace Inventory.Models.Master;

public class JobModel
{
    public long? JobTypeID { get; set; }
    public long? JobID { get; set; }
    public string? JobName { get; set; }
    public string? JobNmaedescr { get; set; }
    public long? CompanyID { get; set; }
    public long? CreatedUID { get; set; }
}