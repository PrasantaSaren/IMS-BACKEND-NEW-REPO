using Inventory.AppCode.Helper;
using Inventory.Models.Entity;
using Inventory.Models.Request;
using Inventory.Models.Response;
using Inventory.Repository.DBContext;
using Inventory.Repository.IService;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Data;
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
                var requisitionList = new List<GetRequisitionList>();
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
                        requisitionList = GlobalHelper.DataTableToList<GetRequisitionList>(ds.Tables[0]);
                        getRequisitionListResponse.RequisitionList = requisitionList;
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


    }
}
