

using System.Data;
using Inventory.Models.NoteSheet;
namespace Inventory.Repository.IService;
public interface INoteSheetService
{
    Task<long> AddUpdateNoteSheetDetails(NoteSheetModel _params);
    Task<List<NoteSheetModel>> GetNoteSheetListData(string Item, long NoteSheetID);

}

