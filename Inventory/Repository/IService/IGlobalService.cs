using Inventory.Models.Request;
using System.Data;

namespace Inventory.Repository.IService
{
    public interface IGlobalService
    {
        Task<List<T>> GetGlobalDropdownList<T>(GlobalDropdownRequest globalDropdownRequest) where T : class, new();
        Task<DataTable> GetGlobalSelectTable(GlobalSelectTableRequest globalSelectTableRequest);
        Task<Dictionary<string, object>?> GetGlobalSelectTableRow(GlobalSelectTableRowRequest globalSelectTableRowRequest);
        Task<bool> GlobalLogicalDelete(GlobalLogicalDeleteRequest globalLogicalDeleteRequest);
    }
}
