namespace Inventory.Models.Master;

public class VendorModel
{
    public long? CV_ID { get; set; }
    public string? DescripTionJob { get; set; }
    public string? DescripTion { get; set; }
    public string? CV_Tag { get; set; }
    public string? DealsIn { get; set; }
    public string? CV_Code { get; set; }
    public string? CV_Name { get; set; }
    public string? ContactPerson { get; set; }
    public string? RegNumber { get; set; }
    public string? RegDate { get; set; }
    public string? ServiceTaxNumber { get; set; }
    public string? CV_Address { get; set; }
    public string? CVDistrict { get; set; }
    public string? CV_PinCode { get; set; }
    public string? CV_State { get; set; }
    public string? CV_Phone { get; set; }
    public string? CV_Mobile { get; set; }
    public string? CV_Email { get; set; }
    public string? CV_Website { get; set; }
    public string? CV_PANCardNo { get; set; }
    public string? CV_TAN { get; set; }
    public string? CV_WBST_NO { get; set; }
    public string? CV_CST_NO { get; set; }
    public string? BankCode { get; set; }
    public string? Bankbranch { get; set; }
    public string? VAT { get; set; }
    public string? IFSCCode { get; set; }
    public string? AccountNumber { get; set; }
    public string? AccType { get; set; }
    public string? UnitID { get; set; }
    public string? Status { get; set; }
    public string? file { get; set; }
    public string? Type { get; set; }
    public long? CompanyID { get; set; }
    public long? CreatedUID { get; set; }
    public List<ItemIDListModel> ItemIDList { get; set; } = new List<ItemIDListModel>();
    public List<JobIDListModel> JobIDList { get; set; } = new List<JobIDListModel>();
}
public class ItemIDListModel
{
    public long ItemTypeID { get; set; }
}

public class JobIDListModel
{
    public long jobTypeID { get; set; }
}