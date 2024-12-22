namespace Inventory.Models.Requisition
{
    public class Ims_Requisition_GetArea
    {
        public string? AreaName { get; set; }
        public long AreaID { get; set; }
        public string? AreaCode { get; set; }
    }
    public class UnitInfo
    {
        public string? SBUType { get; set; }
    }
    public class AreaList
    {
        public List<Ims_Requisition_GetArea>? Areas { get; set; }
        public UnitInfo? UnitInfo { get; set; }
    }
}
