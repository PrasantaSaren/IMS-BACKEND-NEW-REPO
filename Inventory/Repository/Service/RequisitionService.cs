using Inventory.AppCode.Helper;
using Inventory.Models.Entity;
using Inventory.Models.Request;
using Inventory.Models.Response;
using Inventory.Repository.DBContext;
using Inventory.Repository.IService;
using Microsoft.EntityFrameworkCore;
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
        public async Task<GetRequisitionListResponse> GetRequisitionList(GetRequisitionListRequest getRequisitionListRequest)
        {

            try
            {
                var requisitionList = new List<GetRequisition>();
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                GetRequisitionListResponse getRequisitionListResponse = new GetRequisitionListResponse();
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("GetRequisitionList_SP", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@AllRejectAndHold", getRequisitionListRequest.AllRejectAndHold);
                    command.Parameters.AddWithValue("@SearchFor", string.IsNullOrEmpty(getRequisitionListRequest.browseParam?.SearchFor) ? (object)DBNull.Value : getRequisitionListRequest.browseParam.SearchFor);
                    command.Parameters.AddWithValue("@PageSort", string.IsNullOrEmpty(getRequisitionListRequest.browseParam?.PageSort) ? (object)DBNull.Value : getRequisitionListRequest.browseParam.PageSort);
                    command.Parameters.AddWithValue("@PageOrder", string.IsNullOrEmpty(getRequisitionListRequest.browseParam?.PageOrder) ? (object)DBNull.Value : getRequisitionListRequest.browseParam.PageOrder);
                    command.Parameters.AddWithValue("@StartRow", getRequisitionListRequest.browseParam?.StartRow);
                    command.Parameters.AddWithValue("@EndRow", getRequisitionListRequest.browseParam?.EndRow);

                    var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(outputIdParam);
                    await command.ExecuteNonQueryAsync();

                    var outputId = (long)outputIdParam.Value;
                    getRequisitionListResponse.TotalItem = (int)outputId;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        requisitionList = GlobalHelper.DataTableToList<GetRequisition>(ds.Tables[0]);
                        getRequisitionListResponse.requisitionList = requisitionList;
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
                return getRequisitionListResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<bool> PostRequisition(PostRequisitionRequest postRequisitionRequest)
        {
            try
            {
                bool result = false;
                try
                {
                    var req = new Req()
                    {
                        RefReqNo = postRequisitionRequest.RefReqNo,
                        ReqNo = postRequisitionRequest.ReqNo,
                        ReqDate = postRequisitionRequest.ReqDate,
                        ReqType = postRequisitionRequest.ReqType,
                        Type = postRequisitionRequest.Type,
                        ReqAnmtype = postRequisitionRequest.ReqAnmtype,
                        WorkStatus = postRequisitionRequest.WorkStatus,
                        UnitId = postRequisitionRequest.UnitId,
                        LocationId = postRequisitionRequest.LocationId,
                        AreaId = postRequisitionRequest.AreaId,
                        Description = postRequisitionRequest.Description,
                        Justification = postRequisitionRequest.Justification,
                        Remarks = postRequisitionRequest.Remarks,
                        Experiment = postRequisitionRequest.Experiment,
                        RejectRemarks = postRequisitionRequest.RejectRemarks,
                        ApprovedBy = postRequisitionRequest.ApprovedBy,
                        ApprovedDate = postRequisitionRequest.ApprovedDate,
                        CompanyId = postRequisitionRequest.CompanyId,
                        CreatedUid = postRequisitionRequest.CreatedUid,
                        CreateDate = postRequisitionRequest.CreateDate ?? DateTime.Now,
                        UpdatedUid = postRequisitionRequest.UpdatedUid,
                        UpdatedDate = postRequisitionRequest.UpdatedDate,
                        ApprovalStatus = postRequisitionRequest.ApprovalStatus,
                        ApprovedLevel = postRequisitionRequest.ApprovedLevel,
                        SuppDocName1 = postRequisitionRequest.SuppDocName1,
                        SuppDoc1 = postRequisitionRequest.SuppDoc1,
                        SuppDocName2 = postRequisitionRequest.SuppDocName2,
                        SuppDoc2 = postRequisitionRequest.SuppDoc2,
                        SuppDocName3 = postRequisitionRequest.SuppDocName3,
                        SuppDoc3 = postRequisitionRequest.SuppDoc3,
                        Noofapproval = postRequisitionRequest.Noofapproval,
                        ContactNo = postRequisitionRequest.ContactNo,
                        InitBy = postRequisitionRequest.InitBy,
                        ForwordStatus = postRequisitionRequest.ForwordStatus,
                        ForwordId = postRequisitionRequest.ForwordId,
                        ForwordDate = postRequisitionRequest.ForwordDate,
                        ProcessRemarks = postRequisitionRequest.ProcessRemarks,
                        BackwardId = postRequisitionRequest.BackwardId,
                        BackwardDate = postRequisitionRequest.BackwardDate
                    };
                    _context.Reqs.Add(req);
                    await _context.SaveChangesAsync();
                    if (postRequisitionRequest.getReqDetailsList != null)
                    {
                        foreach (var reqDetails in postRequisitionRequest.getReqDetailsList)
                        {
                            var reqDetail = new ReqDetail
                            {
                                ReqId = req.ReqId,
                                ItemId = reqDetails.ItemId,
                                ReqType = postRequisitionRequest.ReqType,
                                Qty = reqDetails.Qty,
                                Rate = reqDetails.Rate,
                                GrossAmount = 507.50m,
                                Uomid = reqDetails.Uomid,
                                AssetId = reqDetails.AssetId,
                                UnitId = postRequisitionRequest.UnitId,
                                CompanyId = postRequisitionRequest.CompanyId,
                                CreatedUid = postRequisitionRequest.CreatedUid,
                                CreateDate = DateTime.Now,
                                Description = postRequisitionRequest.Description
                            };
                            _context.ReqDetails.AddRange(reqDetail);
                        }
                        await _context.SaveChangesAsync();
                    }
                   
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while post data.", ex);
                }
                finally
                {

                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        
        public async Task<GetReqItemDetailsResponse> GetRequisitionItemDetails(GetReqItemDetailsRequest getReqItemDetailsRequest)
        {
            try
            {
                var reqItemDetailsList = new List<ReqItemDetails>();
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                GetReqItemDetailsResponse getReqItemDetailsResponse = new GetReqItemDetailsResponse();
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("GetRequisitionItemDetails_SP", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@SearchFor", string.IsNullOrEmpty(getReqItemDetailsRequest.browseParam?.SearchFor) ? (object)DBNull.Value : getReqItemDetailsRequest.browseParam.SearchFor);
                    command.Parameters.AddWithValue("@PageSort", string.IsNullOrEmpty(getReqItemDetailsRequest.browseParam?.PageSort) ? (object)DBNull.Value : getReqItemDetailsRequest.browseParam.PageSort);
                    command.Parameters.AddWithValue("@PageOrder", string.IsNullOrEmpty(getReqItemDetailsRequest.browseParam?.PageOrder) ? (object)DBNull.Value : getReqItemDetailsRequest.browseParam.PageOrder);
                    command.Parameters.AddWithValue("@StartRow", getReqItemDetailsRequest.browseParam?.StartRow);
                    command.Parameters.AddWithValue("@EndRow", getReqItemDetailsRequest.browseParam?.EndRow);

                    var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(outputIdParam);
                    await command.ExecuteNonQueryAsync();

                    var outputId = (long)outputIdParam.Value;
                    getReqItemDetailsResponse.TotalItem = (int)outputId;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        reqItemDetailsList = GlobalHelper.DataTableToList<ReqItemDetails>(ds.Tables[0]);
                        getReqItemDetailsResponse.reqItemDetailsList = reqItemDetailsList;
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
                return getReqItemDetailsResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<GetItemOrJobDetailsResponse> GetItemOrJobDetails(GetItemOrJobDetailsRequest getItemOrJobDetailsRequest)
        {
            try
            {
                var itemOrJobDetailsList = new List<ItemOrJobDetails>();
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                GetItemOrJobDetailsResponse getItemOrJobDetailsResponse = new GetItemOrJobDetailsResponse();
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("GetItemOrJobDetails_SP", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@SearchFor", string.IsNullOrEmpty(getItemOrJobDetailsRequest.browseParam?.SearchFor) ? (object)DBNull.Value : getItemOrJobDetailsRequest.browseParam.SearchFor);
                    command.Parameters.AddWithValue("@PageSort", string.IsNullOrEmpty(getItemOrJobDetailsRequest.browseParam?.PageSort) ? (object)DBNull.Value : getItemOrJobDetailsRequest.browseParam.PageSort);
                    command.Parameters.AddWithValue("@PageOrder", string.IsNullOrEmpty(getItemOrJobDetailsRequest.browseParam?.PageOrder) ? (object)DBNull.Value : getItemOrJobDetailsRequest.browseParam.PageOrder);
                    command.Parameters.AddWithValue("@StartRow", getItemOrJobDetailsRequest.browseParam?.StartRow);
                    command.Parameters.AddWithValue("@EndRow", getItemOrJobDetailsRequest.browseParam?.EndRow);

                    var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(outputIdParam);
                    await command.ExecuteNonQueryAsync();

                    var outputId = (long)outputIdParam.Value;
                    getItemOrJobDetailsResponse.TotalItem = (int)outputId;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        itemOrJobDetailsList = GlobalHelper.DataTableToList<ItemOrJobDetails>(ds.Tables[0]);
                        getItemOrJobDetailsResponse.itemOrJobDetailslsList = itemOrJobDetailsList;
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
                return getItemOrJobDetailsResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<bool> UpdateRequisition(UpdateRequisitionRequest updateRequisitionRequest)
        {
            try
            {
                bool result = false;
                try
                {
                    var reqResponse = await _context.Reqs.FirstOrDefaultAsync(r => r.ReqId == updateRequisitionRequest.ReqId);
                    if (reqResponse != null)
                    {
                        reqResponse.RefReqNo = updateRequisitionRequest.RefReqNo;
                        reqResponse.ReqNo = updateRequisitionRequest.ReqNo;
                        reqResponse.ReqDate = updateRequisitionRequest.ReqDate;
                        reqResponse.ReqType = updateRequisitionRequest.ReqType;
                        reqResponse.Type = updateRequisitionRequest.Type;
                        reqResponse.ReqAnmtype = updateRequisitionRequest.ReqAnmtype;
                        reqResponse.WorkStatus = updateRequisitionRequest.WorkStatus;
                        reqResponse.UnitId = updateRequisitionRequest.UnitId;
                        reqResponse.LocationId = updateRequisitionRequest.LocationId;
                        reqResponse.AreaId = updateRequisitionRequest.AreaId;
                        reqResponse.Description = updateRequisitionRequest.Description;
                        reqResponse.Justification = updateRequisitionRequest.Justification;
                        reqResponse.Remarks = updateRequisitionRequest.Remarks;
                        reqResponse.Experiment = updateRequisitionRequest.Experiment;
                        reqResponse.RejectRemarks = updateRequisitionRequest.RejectRemarks;
                        reqResponse.ApprovedBy = updateRequisitionRequest.ApprovedBy;
                        reqResponse.ApprovedDate = updateRequisitionRequest.ApprovedDate;
                        reqResponse.CompanyId = updateRequisitionRequest.CompanyId;
                        reqResponse.CreatedUid = updateRequisitionRequest.CreatedUid;
                        reqResponse.CreateDate = updateRequisitionRequest.CreateDate ?? DateTime.Now;
                        reqResponse.UpdatedUid = updateRequisitionRequest.UpdatedUid;
                        reqResponse.UpdatedDate = updateRequisitionRequest.UpdatedDate;
                        reqResponse.ApprovalStatus = updateRequisitionRequest.ApprovalStatus;
                        reqResponse.ApprovedLevel = updateRequisitionRequest.ApprovedLevel;
                        reqResponse.SuppDocName1 = updateRequisitionRequest.SuppDocName1;
                        reqResponse.SuppDoc1 = updateRequisitionRequest.SuppDoc1;
                        reqResponse.SuppDocName2 = updateRequisitionRequest.SuppDocName2;
                        reqResponse.SuppDoc2 = updateRequisitionRequest.SuppDoc2;
                        reqResponse.SuppDocName3 = updateRequisitionRequest.SuppDocName3;
                        reqResponse.SuppDoc3 = updateRequisitionRequest.SuppDoc3;
                        reqResponse.Noofapproval = updateRequisitionRequest.Noofapproval;
                        reqResponse.ContactNo = updateRequisitionRequest.ContactNo;
                        reqResponse.InitBy = updateRequisitionRequest.InitBy;
                        reqResponse.ForwordStatus = updateRequisitionRequest.ForwordStatus;
                        reqResponse.ForwordId = updateRequisitionRequest.ForwordId;
                        reqResponse.ForwordDate = updateRequisitionRequest.ForwordDate;
                        reqResponse.ProcessRemarks = updateRequisitionRequest.ProcessRemarks;
                        reqResponse.BackwardId = updateRequisitionRequest.BackwardId;
                        reqResponse.BackwardDate = updateRequisitionRequest.BackwardDate;
                        _context.Reqs.Update(reqResponse);
                        await _context.SaveChangesAsync();
                    }
                    if (updateRequisitionRequest.getReqDetailsList != null)
                    {
                        _ = await _context.ReqDetails.Where(r => r.ReqId == updateRequisitionRequest.ReqId).ExecuteDeleteAsync();

                        foreach (var reqDetails in updateRequisitionRequest.getReqDetailsList)
                        {
                            var reqDetail = new ReqDetail
                            {
                                ReqId = reqDetails.ReqId,
                                ItemId = reqDetails.ItemId,
                                ReqType = updateRequisitionRequest.ReqType,
                                Qty = reqDetails.Qty,
                                Rate = reqDetails.Rate,
                                GrossAmount = 507.50m,
                                Uomid = reqDetails.Uomid,
                                AssetId = reqDetails.AssetId,
                                UnitId = updateRequisitionRequest.UnitId,
                                CompanyId = updateRequisitionRequest.CompanyId,
                                CreatedUid = updateRequisitionRequest.CreatedUid,
                                CreateDate = DateTime.Now,
                                Description = updateRequisitionRequest.Description
                            };
                            _context.ReqDetails.AddRange(reqDetail);
                        }
                        await _context.SaveChangesAsync();
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while post data.", ex);
                }
                finally
                {

                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
