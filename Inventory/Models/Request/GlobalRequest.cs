using System.ComponentModel;

namespace Inventory.Models.Request
{
    public class GlobalDropdownRequest
    {
        [DefaultValue("Bank")]
        public string? TableName { get; set; }
        [DefaultValue("BankID")]
        public string? IdFieldName { get; set; }
        [DefaultValue("BankName")]
        public string? NameFieldName { get; set; }
        [DefaultValue("")]
        public string? WhereFieldName { get; set; }
        [DefaultValue("")]
        public string? WhereFieldValue { get; set; }
        [DefaultValue("")]
        public string? WhereField2Name { get; set; }
        [DefaultValue("")]
        public string? WhereField2Value { get; set; }
    }
    public class GlobalSelectTableRequest
    {
        [DefaultValue("Bank")]
        public string? TableName { get; set; }
        [DefaultValue("")]
        public string? WhereFieldName { get; set; }
        [DefaultValue("")]
        public string? WhereFieldValue { get; set; }
        [DefaultValue("")]
        public string? WhereField2Name { get; set; }
        [DefaultValue("")]
        public string? WhereField2Value { get; set; }
    }
    public class GlobalSelectTableRowRequest
    {
        [DefaultValue("Bank")]
        public string? TableName { get; set; }
        [DefaultValue("BankID")]
        public string? WhereFieldName { get; set; }
        [DefaultValue("2")]
        public string? WhereFieldValue { get; set; }
        [DefaultValue("")]
        public string? WhereField2Name { get; set; }
        [DefaultValue("")]
        public string? WhereField2Value { get; set; }
    }
    public class GlobalLogicalDeleteRequest
    {
        [DefaultValue("Bank")]
        public string? TableName { get; set; }
        [DefaultValue("IsActive")]
        public string? IsActiveFieldName { get; set; }
        [DefaultValue("0")]
        public string? IsActiveFieldValue { get; set; }
        [DefaultValue("")]
        public string? WhereFieldName { get; set; }
        [DefaultValue("")]
        public string? WhereFieldValue { get; set; }
    }
}
