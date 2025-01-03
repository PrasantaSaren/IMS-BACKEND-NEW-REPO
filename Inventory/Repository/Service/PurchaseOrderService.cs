using Inventory.AppCode.Helper;
using Inventory.Models.Entity;
using Inventory.Models.Request;
using Inventory.Models.Response;
using Inventory.Repository.DBContext;
using Inventory.Repository.IService;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Inventory.Repository.Service
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly string? _connectionString;
        private readonly IMSV2Context _context;

        public PurchaseOrderService(IConfiguration configuration, IMSV2Context context)
        {
            _connectionString = configuration.GetConnectionString("ProjectConnection");
            _context = context;
        }
        public async Task<DataSet> GetPurchaseOrderList(GetPurchaseOrderListRequest getPurchaseOrderListRequest)
        {
            try
            {
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("USP_PurchaseOrderLIST", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Startdate", getPurchaseOrderListRequest.StartDate);
                    command.Parameters.AddWithValue("@Enddate", getPurchaseOrderListRequest.EndDate);
                    command.Parameters.AddWithValue("@UnitID", getPurchaseOrderListRequest.UnitId);
                    command.Parameters.AddWithValue("@CV_ID", getPurchaseOrderListRequest.CV_ID);
                    command.Parameters.AddWithValue("@PONo", getPurchaseOrderListRequest.PoNo);
                    command.Parameters.AddWithValue("@CompanyID", getPurchaseOrderListRequest.CompanyID);
                    command.Parameters.AddWithValue("@SearchStat", getPurchaseOrderListRequest.SearchStat);
                    command.Parameters.AddWithValue("@SbuType", getPurchaseOrderListRequest.SbuType);
                    command.Parameters.AddWithValue("@UserID", getPurchaseOrderListRequest.UserId);

                    await command.ExecuteNonQueryAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred fetch data.", ex);
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<DataSet> PostPurchaseOrder(PostPurchaseOrderRequest PurchaseOrder)
        {
            try
            {
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "ReqItem";
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemID", typeof(string)));
                dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("AssetType", typeof(string)));
                dt.Columns.Add(new DataColumn("uom", typeof(string)));
                dt.Columns.Add(new DataColumn("Qty", typeof(string)));
                dt.Columns.Add(new DataColumn("Rate", typeof(string)));
                dt.Columns.Add(new DataColumn("GrossAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("Vat", typeof(string)));
                dt.Columns.Add(new DataColumn("Stex", typeof(string)));
                dt.Columns.Add(new DataColumn("dis", typeof(string)));
                dt.Columns.Add(new DataColumn("cst", typeof(string)));
                dt.Columns.Add(new DataColumn("NetAmount", typeof(string)));
                if (PurchaseOrder.purchaseOrderDetailsList != null)
                {
                    foreach (var item in PurchaseOrder.purchaseOrderDetailsList)
                    {
                        DataRow row = dt.NewRow();

                        row["ItemName"] = item.ItemName;
                        row["ItemID"] = item.ItemID;
                        row["Description"] = item.Description;
                        row["AssetType"] = item.AssetType;
                        row["uom"] = item.uom;
                        row["Qty"] = item.Qty;
                        row["Rate"] = item.Rate;
                        row["GrossAmount"] = item.GrossAmount;
                        row["Vat"] = item.Vat;
                        row["Stex"] = item.Stex;
                        row["dis"] = item.dis;
                        row["cst"] = item.cst;
                        row["NetAmount"] = item.NetAmount;
                        dt.Rows.Add(row);
                    }
                }
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("Usp_POInsertUpdate", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@POItemJobType", dt);
                    command.Parameters.AddWithValue("@POID", PurchaseOrder.POID);
                    command.Parameters.AddWithValue("@QuotationID", PurchaseOrder.QuotationID);
                    command.Parameters.AddWithValue("@NoteSheetID", PurchaseOrder.NoteSheetID);
                    command.Parameters.AddWithValue("@POANMType", PurchaseOrder.POANMType);
                    command.Parameters.AddWithValue("@ReqID", PurchaseOrder.ReqID);
                    command.Parameters.AddWithValue("@CV_ID", PurchaseOrder.CV_ID);
                    command.Parameters.AddWithValue("@PODate", PurchaseOrder.PODate);
                    command.Parameters.AddWithValue("@PurchaseType", PurchaseOrder.PurchaseType);
                    command.Parameters.AddWithValue("@Type", PurchaseOrder.Type);
                    command.Parameters.AddWithValue("@POLabelType", PurchaseOrder.POLabelType);
                    command.Parameters.AddWithValue("@IsRateContract", PurchaseOrder.IsRateContract);
                    command.Parameters.AddWithValue("@Experiment", PurchaseOrder.Experiment);
                    command.Parameters.AddWithValue("@UnitID", PurchaseOrder.UnitID);
                    command.Parameters.AddWithValue("@UnitBillingID", PurchaseOrder.BillinUnitID);
                    command.Parameters.AddWithValue("@LocationID", PurchaseOrder.LocationID);
                    command.Parameters.AddWithValue("@AreaID", PurchaseOrder.AreaID);
                    command.Parameters.AddWithValue("@Description", PurchaseOrder.Description);
                    command.Parameters.AddWithValue("@Remarks", PurchaseOrder.Remarks);
                    command.Parameters.AddWithValue("@TermCond", PurchaseOrder.TermCond);
                    command.Parameters.AddWithValue("@GrossAmount", PurchaseOrder.GrossAmount);
                    command.Parameters.AddWithValue("@DeliveryCharges", PurchaseOrder.DeliveryCharges);
                    command.Parameters.AddWithValue("@TotalDiscountAmt", PurchaseOrder.TotalDiscountAmt);
                    command.Parameters.AddWithValue("@TotalDiscountPer", PurchaseOrder.TotalDiscountPer);
                    command.Parameters.AddWithValue("@NetAmount", PurchaseOrder.NetAmount);
                    command.Parameters.AddWithValue("@Source", PurchaseOrder.Source);
                    command.Parameters.AddWithValue("@CompanyID", PurchaseOrder.CompanyID);
                    command.Parameters.AddWithValue("@CreatedUID", PurchaseOrder.CreatedUID);
                    command.Parameters.AddWithValue("@ApprovalStatus", PurchaseOrder.ApprovalStatus);
                    command.Parameters.AddWithValue("@ApprovedLevel", PurchaseOrder.ApprovedLevel);
                    command.Parameters.AddWithValue("@SuppDocName1", PurchaseOrder.SuppDocName1);
                    command.Parameters.AddWithValue("@SuppDoc1", PurchaseOrder.SuppDoc1);
                    command.Parameters.AddWithValue("@SuppDocName2", PurchaseOrder.SuppDocName2);
                    command.Parameters.AddWithValue("@SuppDoc2", PurchaseOrder.SuppDoc2);
                    command.Parameters.AddWithValue("@Justification", PurchaseOrder.Justification);
                    command.Parameters.AddWithValue("@PCDays", PurchaseOrder.PCDays);
                    command.Parameters.AddWithValue("@FNo", PurchaseOrder.FileNo);
                    command.Parameters.AddWithValue("@ApproveDate", PurchaseOrder.ApprovedDate);
                    command.Parameters.AddWithValue("@MonthwiseBillType", PurchaseOrder.MonthwiseBillType);
                    command.Parameters.AddWithValue("@SuppDocName3", PurchaseOrder.SuppDocName3);
                    command.Parameters.AddWithValue("@SuppDoc3", PurchaseOrder.SuppDoc3);
                    command.Parameters.AddWithValue("@ProformaInvoiceType", PurchaseOrder.ProformaInvoiceType);
                    command.Parameters.AddWithValue("@ProformaAmount", PurchaseOrder.ProformaAmount);

                    await command.ExecuteNonQueryAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred fetch data.", ex);
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<GetPurcOrdItemOrJobDetlsResponse> GetPurchaseOrderItemOrJobDetails(GetPurcOrdItemOrJobDetlsRequest getPurcOrdItemOrJobDetlsRequest)
        {
            try
            {
                var purcOrdItemOrJobDetlsList = new List<PurcOrdItemOrJobDetls>();
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                GetPurcOrdItemOrJobDetlsResponse getPurcOrdItemOrJobDetlsResponse = new GetPurcOrdItemOrJobDetlsResponse();
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("GetPurchaseOrderItemOrJobDetails_SP", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@SearchFor", string.IsNullOrEmpty(getPurcOrdItemOrJobDetlsRequest.browseParam?.SearchFor) ? (object)DBNull.Value : getPurcOrdItemOrJobDetlsRequest.browseParam.SearchFor);
                    command.Parameters.AddWithValue("@PageSort", string.IsNullOrEmpty(getPurcOrdItemOrJobDetlsRequest.browseParam?.PageSort) ? (object)DBNull.Value : getPurcOrdItemOrJobDetlsRequest.browseParam.PageSort);
                    command.Parameters.AddWithValue("@PageOrder", string.IsNullOrEmpty(getPurcOrdItemOrJobDetlsRequest.browseParam?.PageOrder) ? (object)DBNull.Value : getPurcOrdItemOrJobDetlsRequest.browseParam.PageOrder);
                    command.Parameters.AddWithValue("@StartRow", getPurcOrdItemOrJobDetlsRequest.browseParam?.StartRow);
                    command.Parameters.AddWithValue("@EndRow", getPurcOrdItemOrJobDetlsRequest.browseParam?.EndRow);

                    var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(outputIdParam);
                    await command.ExecuteNonQueryAsync();

                    var outputId = (long)outputIdParam.Value;
                    getPurcOrdItemOrJobDetlsResponse.TotalItem = (int)outputId;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        purcOrdItemOrJobDetlsList = GlobalHelper.DataTableToList<PurcOrdItemOrJobDetls>(ds.Tables[0]);
                        getPurcOrdItemOrJobDetlsResponse.purcOrdItemOrJobDetlsList = purcOrdItemOrJobDetlsList;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred fetch data.", ex);
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
                return getPurcOrdItemOrJobDetlsResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

    }
}
