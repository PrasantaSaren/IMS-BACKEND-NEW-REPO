
using System.Data;
using Inventory.Models.Quotation;
using Inventory.Models.Requisition;
namespace Inventory.Repository.IService;
public interface IQuotationRepository
{
    Task<long> AddUpdateQuotationDetails(QuotationModel _params);
    Task<List<QuotationModel>> GetQuotationListData(string Item, long QuotationID);

}
