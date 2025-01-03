using Inventory.AppCode.Helper;
using Inventory.Models.Entity;
using Inventory.Models.Request;
using Inventory.Models.Response;
using Inventory.Repository.DBContext;
using Inventory.Repository.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;

namespace Inventory.Repository.Service
{
    public class RequisitionService : IRequisitionService
    {
        private readonly string? _connectionString;
        private readonly IMSV2Context _context;

        public RequisitionService(IConfiguration configuration, IMSV2Context context)
        {
            _connectionString = configuration.GetConnectionString("ProjectConnection");
            _context = context;
        }
        public async Task<DataSet> GetRequisitionList(GetRequisitionListRequest getRequisitionListRequest)
        {

            try
            {
                //var requisitionList = new List<GetRequisition>();
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                //GetRequisitionListResponse getRequisitionListResponse = new GetRequisitionListResponse();
                try
                {
                    var SPName = "";
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    if (getRequisitionListRequest.CreatedUID == 138)
                    {
                        SPName = "Usp_RequisitionSearchListForMaintanance";
                    }
                    else if (getRequisitionListRequest.chkrejectHold == true)
                    {
                        SPName = "Usp_RequisitionSearchList_Reject";
                    }
                    else
                    {
                        SPName = "Usp_RequisitionSearchList";
                    }
                    SqlCommand command = new SqlCommand(SPName, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Startdate", getRequisitionListRequest.Startdate);
                    command.Parameters.AddWithValue("@Enddate", getRequisitionListRequest.Enddate);
                    command.Parameters.AddWithValue("@UnitID", getRequisitionListRequest.UnitID);
                    command.Parameters.AddWithValue("@ApprovalStatus", getRequisitionListRequest.ApprovalStatus);
                    command.Parameters.AddWithValue("@ReqNo", getRequisitionListRequest.ReqNo);
                    command.Parameters.AddWithValue("@CompanyID", getRequisitionListRequest.CompanyID);
                    command.Parameters.AddWithValue("@RequisitionType", getRequisitionListRequest.RequisitionType);
                    command.Parameters.AddWithValue("@SearchStat", getRequisitionListRequest.SearchStat);
                    command.Parameters.AddWithValue("@ReqID", getRequisitionListRequest.ReqID);
                    command.Parameters.AddWithValue("@SbuType", getRequisitionListRequest.SbuType);
                    command.Parameters.AddWithValue("@Layer", getRequisitionListRequest.Layer);
                    command.Parameters.AddWithValue("@CreatedUID", getRequisitionListRequest.CreatedUID);

                    await command.ExecuteNonQueryAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    requisitionList = GlobalHelper.DataTableToList<GetRequisition>(ds.Tables[0]);
                    //    getRequisitionListResponse.requisitionList = requisitionList;
                    //}
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

        public async Task<DataSet> PostRequisition(PostRequisitionRequest requistion)
        {
            try
            {
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("slno", typeof(long));
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("ItemID", typeof(long));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("uomn", typeof(long));
                dt.Columns.Add("uom", typeof(long));
                dt.Columns.Add("assetn", typeof(string));
                dt.Columns.Add("asset", typeof(string));
                dt.Columns.Add("Qty", typeof(decimal));
                dt.Columns.Add("Rate", typeof(decimal));
                dt.Columns.Add("GrossAmount", typeof(decimal));
                int i = 0;
                if (requistion.getReqDetailsList != null)
                {
                    foreach (var item in requistion.getReqDetailsList)
                    {
                        i++;
                        DataRow row = dt.NewRow();

                        row["slno"] = i;
                        row["ItemName"] = item.ItemName;
                        row["ItemID"] = item.ItemId;
                        row["Description"] = item.Description;
                        row["uomn"] = item.uomn;
                        row["uom"] = item.uom;
                        row["assetn"] = item.assetn;
                        row["asset"] = item.asset;
                        row["Qty"] = item.Qty;
                        row["Rate"] = item.Rate;
                        row["GrossAmount"] = item.GrossAmount;
                        dt.Rows.Add(row);
                    }
                }
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("Usp_SaveRequisitionIns", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@ReqItemJob", dt);
                    command.Parameters.AddWithValue("@ReqID", requistion.ReqID);
                    command.Parameters.AddWithValue("@RefReqNo", requistion.RefReqNo);
                    command.Parameters.AddWithValue("@ReqANMType", requistion.ReqANMType);
                    command.Parameters.AddWithValue("@ReqNo", requistion.ReqNo);
                    command.Parameters.AddWithValue("@ReqDate", requistion.ReqDate);
                    command.Parameters.AddWithValue("@ReqType", requistion.ReqType);
                    command.Parameters.AddWithValue("@Type", requistion.Type);
                    command.Parameters.AddWithValue("@WorkStatus", requistion.WorkStatus);
                    command.Parameters.AddWithValue("@IsRateContract", requistion.IsRateContract);
                    command.Parameters.AddWithValue("@UnitID", requistion.UnitID);
                    command.Parameters.AddWithValue("@LocationID", requistion.LocationID);
                    command.Parameters.AddWithValue("@AreaID", requistion.AreaID);
                    command.Parameters.AddWithValue("@Description", requistion.Description);
                    command.Parameters.AddWithValue("@Remarks", requistion.Remarks);
                    command.Parameters.AddWithValue("@Experiment", requistion.Experiment);
                    command.Parameters.AddWithValue("@RejectRemarks", requistion.RejectRemarks);
                    command.Parameters.AddWithValue("@CompanyID", requistion.CompanyID);
                    command.Parameters.AddWithValue("@CreatedUID", requistion.CreatedUID);
                    command.Parameters.AddWithValue("@ApprovalStatus", requistion.ApprovalStatus);
                    command.Parameters.AddWithValue("@ApprovedLevel", requistion.ApprovedLevel);
                    command.Parameters.AddWithValue("@ApprovedBy", requistion.ApprovedBy);
                    command.Parameters.AddWithValue("@SuppDocName1", requistion.SuppDocName1);
                    command.Parameters.AddWithValue("@SuppDoc1", requistion.SuppDoc1);
                    command.Parameters.AddWithValue("@SuppDocName2", requistion.SuppDocName2);
                    command.Parameters.AddWithValue("@SuppDoc2", requistion.SuppDoc2);
                    command.Parameters.AddWithValue("@SuppDocName3", requistion.SuppDocName3);
                    command.Parameters.AddWithValue("@SuppDoc3", requistion.SuppDoc3);
                    command.Parameters.AddWithValue("@Justification", requistion.Justification);
                    command.Parameters.AddWithValue("@ContactNo", requistion.ContactNo);
                    command.Parameters.AddWithValue("@InitBy", requistion.InitBy);
                    command.Parameters.AddWithValue("@FrdStatus", requistion.FrdStatus);
                    command.Parameters.AddWithValue("@ApproveDate", requistion.ApprovedDate);


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

        public async Task<DataSet> GetRequisitionItemDetails(long ReqID)
        {
            try
            {
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("USP_GETREQUISITIONITEMDTLS", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@ReqID", ReqID);

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

        public async Task<DataSet> GetItemOrJobDetails(long ReqID)
        {
            try
            {
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("Usp_GetDataForEdit", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@ReqID", ReqID);

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

        //public async Task<bool> UpdateRequisition(UpdateRequisitionRequest updateRequisitionRequest)
        //{
        //    try
        //    {
        //        bool result = false;
        //        try
        //        {
        //            var reqResponse = await _context.Reqs.FirstOrDefaultAsync(r => r.ReqId == updateRequisitionRequest.ReqId);
        //            if (reqResponse != null)
        //            {
        //                reqResponse.RefReqNo = updateRequisitionRequest.RefReqNo;
        //                reqResponse.ReqNo = updateRequisitionRequest.ReqNo;
        //                reqResponse.ReqDate = updateRequisitionRequest.ReqDate;
        //                reqResponse.ReqType = updateRequisitionRequest.ReqType;
        //                reqResponse.Type = updateRequisitionRequest.Type;
        //                reqResponse.ReqAnmtype = updateRequisitionRequest.ReqANMType;
        //                reqResponse.WorkStatus = updateRequisitionRequest.WorkStatus;
        //                reqResponse.UnitId = updateRequisitionRequest.UnitID;
        //                reqResponse.LocationId = updateRequisitionRequest.LocationID;
        //                reqResponse.AreaId = updateRequisitionRequest.AreaID;
        //                reqResponse.Description = updateRequisitionRequest.Description;
        //                reqResponse.Justification = updateRequisitionRequest.Justification;
        //                reqResponse.Remarks = updateRequisitionRequest.Remarks;
        //                reqResponse.Experiment = updateRequisitionRequest.Experiment;
        //                reqResponse.RejectRemarks = updateRequisitionRequest.RejectRemarks;
        //                reqResponse.ApprovedBy = updateRequisitionRequest.ApprovedBy;
        //                reqResponse.ApprovedDate = updateRequisitionRequest.ApprovedDate;
        //                reqResponse.CompanyId = updateRequisitionRequest.CompanyID;
        //                reqResponse.CreatedUid = updateRequisitionRequest.CreatedUID;
        //                reqResponse.ApprovalStatus = updateRequisitionRequest.ApprovalStatus;
        //                reqResponse.ApprovedLevel = updateRequisitionRequest.ApprovedLevel;
        //                reqResponse.SuppDocName1 = updateRequisitionRequest.SuppDocName1;
        //                reqResponse.SuppDoc1 = updateRequisitionRequest.SuppDoc1;
        //                reqResponse.SuppDocName2 = updateRequisitionRequest.SuppDocName2;
        //                reqResponse.SuppDoc2 = updateRequisitionRequest.SuppDoc2;
        //                reqResponse.SuppDocName3 = updateRequisitionRequest.SuppDocName3;
        //                reqResponse.SuppDoc3 = updateRequisitionRequest.SuppDoc3;
        //                reqResponse.ContactNo = updateRequisitionRequest.ContactNo;
        //                reqResponse.InitBy = updateRequisitionRequest.InitBy;
        //                _context.Reqs.Update(reqResponse);
        //                await _context.SaveChangesAsync();
        //            }
        //            if (updateRequisitionRequest.getReqDetailsList != null)
        //            {
        //                _ = await _context.ReqDetails.Where(r => r.ReqId == updateRequisitionRequest.ReqId).ExecuteDeleteAsync();

        //                foreach (var reqDetails in updateRequisitionRequest.getReqDetailsList)
        //                {
        //                    var reqDetail = new ReqDetail
        //                    {
        //                        ReqId = reqDetails.ReqId,
        //                        ItemId = reqDetails.ItemId,
        //                        ReqType = updateRequisitionRequest.ReqType,
        //                        Qty = reqDetails.Qty,
        //                        Rate = reqDetails.Rate,
        //                        GrossAmount = 507.50m,
        //                        Uomid = reqDetails.Uomid,
        //                        AssetId = reqDetails.AssetId,
        //                        UnitId = updateRequisitionRequest.UnitId,
        //                        CompanyId = updateRequisitionRequest.CompanyId,
        //                        CreatedUid = updateRequisitionRequest.CreatedUid,
        //                        CreateDate = DateTime.Now,
        //                        Description = updateRequisitionRequest.Description
        //                    };
        //                    _context.ReqDetails.AddRange(reqDetail);
        //                }
        //                await _context.SaveChangesAsync();
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("An error occurred while post data.", ex);
        //        }
        //        finally
        //        {

        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("An unexpected error occurred.", ex);
        //    }
        //}
    }
}
