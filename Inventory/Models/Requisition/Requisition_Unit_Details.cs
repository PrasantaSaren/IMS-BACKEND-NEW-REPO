namespace Inventory.Models.Requisition
{
    public class Requisition_Unit_Details
    {
        public string? UnitName { get; set; }
        public long UnitID { get; set; }
        public long CompanyID { get; set; }
    }

    public class UnitList
    {
        public List<Requisition_Unit_Details>? UnitDetailsList { get; set; }
    }
}
