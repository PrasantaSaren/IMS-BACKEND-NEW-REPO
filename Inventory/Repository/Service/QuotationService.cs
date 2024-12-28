

using System.Data;
using Inventory.Models.Entity;
using Inventory.Models.Quotation;
using Inventory.Models.Requisition;
using Inventory.Repository.DBContext;
using Inventory.Repository.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Inventory.Repository.Service;
public class QuotationService : IQuotationRepository
{
    private readonly string? _connectionString;
    private readonly IMSV2Context _context;
    public QuotationService(IConfiguration configuration, IMSV2Context context)
    {
        _connectionString = configuration.GetConnectionString("ProjectConnection");
        _context = context;
    }

    public async Task<long> AddUpdateQuotationDetails(QuotationModel _params)
    {
        long result = -1;
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                using SqlCommand cmd = new("[dbo].[Usp_QUOTATIONInsertUpdate]", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                var table = new DataTable();
                table.Columns.Add("ItemName", typeof(string));
                table.Columns.Add("ItemID", typeof(long));
                table.Columns.Add("Description", typeof(string));
                table.Columns.Add("AssetType", typeof(long));
                table.Columns.Add("uom", typeof(string));
                table.Columns.Add("Qty", typeof(decimal));
                table.Columns.Add("Rate", typeof(decimal));
                table.Columns.Add("GrossAmount", typeof(decimal));
                table.Columns.Add("Vat", typeof(decimal));
                table.Columns.Add("Stex", typeof(decimal));
                table.Columns.Add("igst", typeof(decimal));
                table.Columns.Add("NetAmount", typeof(decimal));

                foreach (var item in _params.QuoteItemJob)
                {
                    table.Rows.Add(
                        item.ItemName, 
                        item.ItemID, 
                        item.Description, 
                        item.AssetType, 
                        item.uom, 
                        item.Qty, 
                        item.Rate, 
                        item.GrossAmount,
                        item.Vat,
                        item.Stex,
                        item.igst,
                        item.NetAmount
                    );
                }

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@QuotationItemJob",
                    TypeName = "dbo.QuotationItemJobType04",
                    Value = table
                });
                cmd.Parameters.AddWithValue("@QuotationID", _params.QuotationID);
                cmd.Parameters.AddWithValue("@RefQuotationNo", _params.RefQuotationNo);
                cmd.Parameters.AddWithValue("@QuotationANMType", _params.QuotationANMType);
                cmd.Parameters.AddWithValue("@QuotationNo", _params.QuotationNo);
                cmd.Parameters.AddWithValue("@ReqNo", _params.ReqNo);
                cmd.Parameters.AddWithValue("@ReqID", _params.ReqID);
                cmd.Parameters.AddWithValue("@CV_ID", _params.CV_ID);
                cmd.Parameters.AddWithValue("@QuotationDate", _params.QuotationDate);
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
                cmd.Parameters.AddWithValue("@ApproveDate", _params.ApproveDate);
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
    public async Task<List<QuotationModel>> GetQuotationListData(string Item, long QuotationID)
    {
        var quotations = new List<QuotationModel>();

        try
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("[dbo].[Usp_QuotationList]", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Item", Item);
            cmd.Parameters.AddWithValue("@QuotationID", QuotationID);

            var ds = new DataSet();
            var adapter = new SqlDataAdapter(cmd);
            await conn.OpenAsync();
            await Task.Run(() => adapter.Fill(ds));

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return quotations; // Return empty list if no data

            // Handle ALL_VIEW (single table, multiple rows)
            if (Item == "ALL_VIEW")
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    quotations.Add(new QuotationModel
                    {
                        QuotationID = Convert.ToInt64(row["QuotationID"]),
                        ReqID = Convert.ToInt64(row["ReqID"]),
                        QuotationDate = Convert.ToDateTime(row["QuotationDate"]),
                        QuotationNo = row["QuotationNo"].ToString(),
                        ReqNo = row["ReqNo"].ToString(),
                        Description = row["Description"].ToString(),
                        AreaName = row["AreaName"].ToString(),
                        LocationName = row["LocationName"].ToString(),
                        UnitName = row["UnitName"].ToString(),
                        CV_Name = row["CV_Name"].ToString(),
                        AreaID = Convert.ToInt64(row["AreaID"]),
                        LocationID = Convert.ToInt64(row["LocationID"]),
                        UnitID = Convert.ToInt64(row["UnitID"]),
                        CV_ID = Convert.ToInt64(row["CV_ID"]),
                        ApprovalStatus = row["ApprovalStatus"].ToString()
                    });
                }
            }

            // Handle SPECIFIC_VIEW (two tables)
            if (Item == "SPECIFIC_VIEW" && ds.Tables.Count > 1)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var quotation = new QuotationModel
                    {
                        QuotationID = Convert.ToInt64(row["QuotationID"]),
                        ReqID = Convert.ToInt64(row["ReqID"]),
                        QuotationDate = Convert.ToDateTime(row["QuotationDate"]),
                        QuotationNo = row["QuotationNo"].ToString(),
                        ReqNo = row["ReqNo"].ToString(),
                        Description = row["Description"].ToString(),
                        AreaName = row["AreaName"].ToString(),
                        LocationName = row["LocationName"].ToString(),
                        UnitName = row["UnitName"].ToString(),
                        CV_Name = row["CV_Name"].ToString(),
                        AreaID = Convert.ToInt64(row["AreaID"]),
                        LocationID = Convert.ToInt64(row["LocationID"]),
                        UnitID = Convert.ToInt64(row["UnitID"]),
                        CV_ID = Convert.ToInt64(row["CV_ID"]),
                        ApprovalStatus = row["ApprovalStatus"].ToString()
                    };

                    // Add Quotation Item Details
                    foreach (DataRow itemRow in ds.Tables[1].Rows)
                    {
                        quotation.QuoteItemJob.Add(new QuotationItemJob
                        {
                            QuotationDetailID = Convert.ToInt64(itemRow["QuotationDetailID"]),
                            QuotationID = Convert.ToInt64(itemRow["QuotationID"]),
                            ItemID = Convert.ToInt64(itemRow["ItemID"]),
                            uom = itemRow["uom"].ToString(),
                            UOMName = itemRow["UOMName"].ToString(),
                            Description = itemRow["Description"].ToString(),
                            Qty = Convert.ToDecimal(itemRow["Qty"]),
                            Rate = Convert.ToDecimal(itemRow["Rate"]),
                            GrossAmount = Convert.ToDecimal(itemRow["GrossAmount"]),
                            Vat = Convert.ToDecimal(itemRow["Vat"]),
                            Stex = Convert.ToDecimal(itemRow["Stex"]),
                            igst = Convert.ToDecimal(itemRow["igst"]),
                            NetAmount = Convert.ToDecimal(itemRow["NetAmount"])
                        });
                    }

                    quotations.Add(quotation);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle errors and log
            throw new Exception("An error occurred while fetching quotation data.", ex);
        }

        return quotations;
    }


}

