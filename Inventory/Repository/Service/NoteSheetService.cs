

using System.Data;
using Inventory.Models.Entity;
using Inventory.Models.NoteSheet;
using Inventory.Repository.DBContext;
using Inventory.Repository.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Inventory.Repository.Service;
public class NoteSheetService : INoteSheetService
{ 
    private readonly string? _connectionString;
    private readonly IMSV2Context _context;
    public NoteSheetService(IConfiguration configuration, IMSV2Context context)
    {
        _connectionString = configuration.GetConnectionString("ProjectConnection");
        _context = context;
    }

    public async Task<long> AddUpdateNoteSheetDetails(NoteSheetModel _params)
    {
        long result = -1;
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                using SqlCommand cmd = new("[dbo].[Usp_NOTESHEETInsertUpdate]", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                var table = new DataTable();
                table.Columns.Add("ItemID", typeof(long));
                table.Columns.Add("ItemName", typeof(string));
                table.Columns.Add("Description", typeof(string));
                table.Columns.Add("AssetType", typeof(long));
                table.Columns.Add("uom", typeof(string));
                table.Columns.Add("Qty", typeof(decimal));
                table.Columns.Add("Rate", typeof(decimal));
                table.Columns.Add("GrossAmount", typeof(decimal));
                table.Columns.Add("Vat", typeof(decimal));
                table.Columns.Add("Stex", typeof(decimal));
                table.Columns.Add("dis", typeof(decimal));
                table.Columns.Add("cst", typeof(decimal));
                table.Columns.Add("NetAmount", typeof(decimal));

                foreach (var item in _params.NoteItemJob)
                {
                    table.Rows.Add(
                        item.ItemID,
                        item.ItemName,
                        item.Description,
                        item.AssetType,
                        item.uom,
                        item.Qty,
                        item.Rate,
                        item.GrossAmount,
                        item.Vat,
                        item.Stex,
                        item.cst,
                        item.NetAmount
                    );
                }

                cmd.Parameters.Add(new SqlParameter { ParameterName = "@NoteItemJobType", TypeName = "dbo.NoteItemJobType", Value = table});
                cmd.Parameters.AddWithValue("@NoteSheetID", _params.NoteSheetID);
                cmd.Parameters.AddWithValue("@QuotationID", _params.QuotationID);
                cmd.Parameters.AddWithValue("@NoteSheetANMtype", _params.NoteSheetANMtype);
                cmd.Parameters.AddWithValue("@ReqID", _params.ReqID);
                cmd.Parameters.AddWithValue("@CV_ID", _params.CV_ID);
                cmd.Parameters.AddWithValue("@NoteSheetDate", _params.NoteSheetDate);
                cmd.Parameters.AddWithValue("@PurchaseType", _params.PurchaseType);
                cmd.Parameters.AddWithValue("@Type", _params.Type);
                cmd.Parameters.AddWithValue("@IsRateContract", _params.IsRateContract);
                cmd.Parameters.AddWithValue("@WorkStatus", _params.WorkStatus);
                cmd.Parameters.AddWithValue("@UnitID", _params.UnitID);
                cmd.Parameters.AddWithValue("@LocationID", _params.LocationID);
                cmd.Parameters.AddWithValue("@AreaID", _params.AreaID);
                cmd.Parameters.AddWithValue("@Description", _params.Description);
                cmd.Parameters.AddWithValue("@Remarks", _params.Remarks);
                cmd.Parameters.AddWithValue("@Justification", _params.Justification);
                cmd.Parameters.AddWithValue("@TermCond", _params.TermCond);
                cmd.Parameters.AddWithValue("@GrossAmount", _params.GrossAmount);
                cmd.Parameters.AddWithValue("@DeliveryCharges", _params.DeliveryCharges);
                cmd.Parameters.AddWithValue("@TotalDiscountAmt", _params.TotalDiscountAmt);
                cmd.Parameters.AddWithValue("@TotalDiscountPer", _params.TotalDiscountPer);
                cmd.Parameters.AddWithValue("@NetAmount", _params.NetAmount);
                cmd.Parameters.AddWithValue("@CompanyID", _params.CompanyID);
                cmd.Parameters.AddWithValue("@CreatedUID", _params.CreatedUID);
                cmd.Parameters.AddWithValue("@ApprovalStatus", _params.ApprovalStatus);
                cmd.Parameters.AddWithValue("@ApprovedLevel", _params.ApprovedLevel);
                cmd.Parameters.AddWithValue("@SuppDocName1", _params.SuppDocName1);
                cmd.Parameters.AddWithValue("@SuppDoc1", _params.SuppDoc1);
                cmd.Parameters.AddWithValue("@SuppDocName2", _params.SuppDocName2);
                cmd.Parameters.AddWithValue("@SuppDoc2", _params.SuppDoc2);
                cmd.Parameters.AddWithValue("@SuppDocName3", _params.SuppDocName3);
                cmd.Parameters.AddWithValue("@SuppDoc3", _params.SuppDoc3);
                
                await connection.OpenAsync();
                object returnValue = await cmd.ExecuteScalarAsync();
                if (returnValue != null)
                {
                    result = Convert.ToInt64(returnValue);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting requisition details.", ex);
            }
        }
        return result;
    }

    public async Task<List<NoteSheetModel>> GetNoteSheetListData(string Item, long NoteSheetID)
    {
        var NoteSheets = new List<NoteSheetModel>();

        try
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("[dbo].[USP_NOTESHEETLIST]", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Item", Item);
            cmd.Parameters.AddWithValue("@NoteSheetID", NoteSheetID);

            var ds = new DataSet();
            var adapter = new SqlDataAdapter(cmd);
            await conn.OpenAsync();
            await Task.Run(() => adapter.Fill(ds));

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return NoteSheets; // Return empty list if no data

            // Handle ALL_VIEW (single table, multiple rows)
            if (Item == "ALL_VIEW")
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    NoteSheets.Add(new NoteSheetModel
                    {
                        NoteSheetID = Convert.ToInt64(row["NOTESHEETID"]),
                        NoteSheetDate = Convert.ToDateTime(row["NOTESHEETDATE"]),
                        NoteSheetNo = row["NOTESHEETNO"].ToString(),
                        Description = row["DESCRIPTION"].ToString(),
                        AreaName = row["AREANAME"].ToString(),
                        AreaID = Convert.ToInt64(row["AREAID"]),
                        //ApproveDate = Convert.ToDateTime(row["APPROVEDDATE"]),
                        UnitName = row["UNITNAME"].ToString(),
                        UnitID = Convert.ToInt64(row["UNITID"]),
                        LocationID = Convert.ToInt64(row["LOCATIONID"]),
                        ApprovalStatus = row["APPROVALSTATUS"].ToString()
                    });
                }
            }

            // Handle SPECIFIC_VIEW (two tables)
            if (Item == "SPECIFIC_VIEW" && ds.Tables.Count > 1)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var NoteSheet = new NoteSheetModel
                    {
                        NoteSheetID = Convert.ToInt64(row["NOTESHEETID"]),
                        NoteSheetDate = Convert.ToDateTime(row["NOTESHEETDATE"]),
                        NoteSheetNo = row["NOTESHEETNO"].ToString(),
                        Description = row["DESCRIPTION"].ToString(),
                        AreaName = row["AREANAME"].ToString(),
                        AreaID = Convert.ToInt64(row["AREAID"]),
                        //ApproveDate = Convert.ToDateTime(row["APPROVEDDATE"]),
                        UnitName = row["UNITNAME"].ToString(),
                        UnitID = Convert.ToInt64(row["UNITID"]),
                        LocationID = Convert.ToInt64(row["LOCATIONID"]),
                        ApprovalStatus = row["APPROVALSTATUS"].ToString()
                    };

                    foreach (DataRow itemRow in ds.Tables[1].Rows)
                    {
                        NoteSheet.NoteItemJob.Add(new NoteSheetItemJob
                        {
                            NoteSheetDetailID = Convert.ToInt64(itemRow["NoteSheetDetailID"]),
                            NoteSheetID = Convert.ToInt64(itemRow["NoteSheetID"]),
                            ItemID = Convert.ToInt64(itemRow["ItemID"]),
                            uom = itemRow["UOMID"].ToString(),
                            UoMName = itemRow["UoMName"].ToString(),
                            Description = itemRow["Description"].ToString(),
                            Qty = Convert.ToDecimal(itemRow["Qty"]),
                            Rate = Convert.ToDecimal(itemRow["Rate"]),
                            GrossAmount = Convert.ToDecimal(itemRow["GrossAmount"]),
                            dis = Convert.ToDecimal(itemRow["Discount"]),
                            Vat = Convert.ToDecimal(itemRow["VAT"]),
                            Stex = Convert.ToDecimal(itemRow["ServiceTax"]),
                            cst = Convert.ToDecimal(itemRow["CST"]),
                            NetAmount = Convert.ToDecimal(itemRow["NetAmount"])
                        });
                    }

                    NoteSheets.Add(NoteSheet);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle errors and log
            throw new Exception("An error occurred while fetching quotation data.", ex);
        }

        return NoteSheets;
    }


}

