using System.Data;
using Inventory.Models.Requisition;
using Inventory.Repository.DBContext;
using Inventory.Repository.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Inventory.Repository.Service;
public class RequisitionRepository : IRequisitionRepository
{
    private readonly string? _connectionString;
    private readonly IMSV2Context _context;
    public RequisitionRepository(IConfiguration configuration, IMSV2Context context)
    {
        _connectionString = configuration.GetConnectionString("ProjectConnection");
        _context = context;
    }
    public UnitList GetRequisitionUnitDetails(string type, long userID)
    {
        UnitList response = new(){ UnitDetailsList = [] };
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                using SqlCommand cmd = new("[dbo].[Usp_GetUnit12]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@uid", userID);
                SqlDataAdapter adapter = new(cmd);
                DataSet dataSet = new();
                connection.Open();
                adapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        response.UnitDetailsList.Add(new Requisition_Unit_Details {
                            UnitName = row["UnitName"].ToString(),
                            UnitID = Convert.ToInt64(row["UnitID"]),
                            CompanyID = Convert.ToInt64(row["CompanyID"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching data.", ex);
            }
        }
        return response;
    }

    public AreaList GetRequisitionUnitAreaDetails(long unitId, long companyId)
    {
        AreaList response = new()
        {
            Areas = []
        };
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                using SqlCommand cmd = new("[dbo].[Usp_getAria]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UnitID", unitId);
                cmd.Parameters.AddWithValue("@companyID", companyId);
                SqlDataAdapter adapter = new(cmd);
                DataSet dataSet = new();
                connection.Open();
                adapter.Fill(dataSet);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        response.Areas.Add(new Ims_Requisition_GetArea
                        {
                            AreaName = row["AreaName"].ToString(),
                            AreaID = Convert.ToInt64(row["AreaID"]),
                            AreaCode = row["AreaCode"].ToString()
                        });
                    }
                }
                if (dataSet.Tables.Count > 1 && dataSet.Tables[1].Rows.Count > 0)
                {
                    DataRow row = dataSet.Tables[1].Rows[0];
                    response.UnitInfo = new UnitInfo
                    {
                        SBUType = row["SBUType"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching data.", ex);
            }
        }
        return response;
    }

    public async Task<DataSet> GetRequisitionUnitAreaLocationDetails(long unitId, long areaId)
    {
        var ds = new DataSet();
        using var conn = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("[dbo].[Usp_GetUnitAreaLocation]", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@UnitID", unitId);
        cmd.Parameters.AddWithValue("@AreaID", areaId);
        var adapter = new SqlDataAdapter(cmd);
        await conn.OpenAsync();
        await Task.Run(() => adapter.Fill(ds));
        return ds;
    }

    public async Task<long> InsertRequisitionDetails(Ims_Requisition_Model _params)
    {
        long result = -1;
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                using SqlCommand cmd = new("[dbo].[Usp_SaveRequisitionIns]", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                var table = new DataTable();
                table.Columns.Add("slno", typeof(long));
                table.Columns.Add("ItemName", typeof(string));
                table.Columns.Add("ItemID", typeof(long));
                table.Columns.Add("Description", typeof(string));
                table.Columns.Add("uomn", typeof(string));
                table.Columns.Add("uom", typeof(long));
                table.Columns.Add("assetn", typeof(string));
                table.Columns.Add("asset", typeof(long));
                table.Columns.Add("Qty", typeof(decimal));
                table.Columns.Add("Rate", typeof(decimal));
                table.Columns.Add("GrossAmount", typeof(decimal));

                foreach (var item in _params.ReqItemJob)
                {
                    table.Rows.Add(item.slno, item.ItemName, item.ItemID, item.Description, item.uomn, item.uom, item.assetn, item.asset, item.Qty, item.Rate, item.GrossAmount);
                }

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@ReqItemJob",
                    //SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.ReqItemJobType06",
                    Value = table
                });


                //cmd.Parameters.AddWithValue("@ReqItemJob", _params.ReqItemJob);
                cmd.Parameters.AddWithValue("@ReqID", _params.ReqID);
                cmd.Parameters.AddWithValue("@RefReqNo", _params.RefReqNo);
                cmd.Parameters.AddWithValue("@ReqANMType", _params.ReqANMType);
                cmd.Parameters.AddWithValue("@ReqNo", _params.ReqNo);
                cmd.Parameters.AddWithValue("@ReqDate", _params.ReqDate);
                cmd.Parameters.AddWithValue("@ReqType", _params.ReqType);
                cmd.Parameters.AddWithValue("@Type", _params.Type);
                cmd.Parameters.AddWithValue("@IsRateContract", _params.IsRateContract);
                cmd.Parameters.AddWithValue("@WorkStatus", _params.WorkStatus);
                cmd.Parameters.AddWithValue("@UnitID", _params.UnitID);
                cmd.Parameters.AddWithValue("@LocationID", _params.LocationID);
                cmd.Parameters.AddWithValue("@AreaID", _params.AreaID);
                cmd.Parameters.AddWithValue("@Description", _params.Description);
                cmd.Parameters.AddWithValue("@Remarks", _params.Remarks);
                cmd.Parameters.AddWithValue("@Experiment", _params.Experiment);
                cmd.Parameters.AddWithValue("@RejectRemarks", _params.RejectRemarks);
                cmd.Parameters.AddWithValue("@Justification", _params.Justification);
                cmd.Parameters.AddWithValue("@ContactNo", _params.ContactNo);
                cmd.Parameters.AddWithValue("@InitBy", _params.InitBy);
                cmd.Parameters.AddWithValue("@CompanyID", _params.CompanyID);
                cmd.Parameters.AddWithValue("@CreatedUID", _params.CreatedUID);
                cmd.Parameters.AddWithValue("@ApprovedBy", _params.ApprovedBy);
                cmd.Parameters.AddWithValue("@ApprovalStatus", _params.ApprovalStatus);
                cmd.Parameters.AddWithValue("@FrdStatus", _params.FrdStatus);
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

    public async Task<DataSet> _GetRequisitionSearchList([FromBody] Ims_Requisition_ReqSearchList_Reponse _params)
    {
        var ds = new DataSet();
        try
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("[dbo].[Usp_RequisitionSearchList]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = _params.StartDate });
            cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = _params.EndDate });
            cmd.Parameters.AddWithValue("@UnitID", _params.UnitId);
            cmd.Parameters.AddWithValue("@ApprovalStatus", _params.ApprovalStatus);
            cmd.Parameters.AddWithValue("@ReqNo", _params.ReqNo);
            cmd.Parameters.AddWithValue("@CompanyID", _params.CompanyId);
            cmd.Parameters.Add(new SqlParameter("@RequisitionType", SqlDbType.VarChar) { Value = _params.ReqType });
            cmd.Parameters.AddWithValue("@SearchStat", _params.SearchStat);
            cmd.Parameters.AddWithValue("@ReqID", _params.ReqId);
            cmd.Parameters.AddWithValue("@SbuType", _params.SBUType);
            cmd.Parameters.AddWithValue("@Layer", _params.Layer);
            cmd.Parameters.AddWithValue("@CreatedUID", _params.CreatedUID);
            var adapter = new SqlDataAdapter(cmd);
            await conn.OpenAsync();
            await Task.Run(() => adapter.Fill(ds));
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("SQL error occurred while fetching the requisition search list.", sqlEx);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching the requisition search list.", ex);
        }
        return ds;
    }

    public async Task<DataSet> GetRequisitionSearchList([FromBody] Ims_Requisition_ReqSearchList_Reponse _params)
    {
        var ds = new DataSet();
        try
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("[dbo].[Usp_RequisitionSearchList]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Handling DateTime? for StartDate and EndDate
            cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.VarChar)
            {
                Value = _params.StartDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value
            });

            cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.VarChar)
            {
                Value = _params.EndDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value
            });

            // Handling nullable long for UnitID
            cmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.BigInt)
            {
                Value = _params.UnitId ?? (object)DBNull.Value
            });

            // Handling string-based parameters
            cmd.Parameters.Add(new SqlParameter("@ApprovalStatus", SqlDbType.VarChar)
            {
                Value = string.IsNullOrEmpty(_params.ApprovalStatus) ? DBNull.Value : _params.ApprovalStatus
            });

            cmd.Parameters.Add(new SqlParameter("@ReqNo", SqlDbType.VarChar)
            {
                Value = string.IsNullOrEmpty(_params.ReqNo) ? DBNull.Value : _params.ReqNo
            });

            // Handling nullable long for CompanyID
            cmd.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.BigInt)
            {
                Value = _params.CompanyId == 0 ? DBNull.Value : (object)_params.CompanyId
            });

            // Handling string-based RequisitionType
            cmd.Parameters.Add(new SqlParameter("@RequisitionType", SqlDbType.VarChar)
            {
                Value = string.IsNullOrEmpty(_params.ReqType) ? DBNull.Value : _params.ReqType
            });

            // Handling string-based SearchStat
            cmd.Parameters.Add(new SqlParameter("@SearchStat", SqlDbType.VarChar)
            {
                Value = string.IsNullOrEmpty(_params.SearchStat) ? DBNull.Value : _params.SearchStat
            });

            // Handling nullable long for ReqID
            cmd.Parameters.Add(new SqlParameter("@ReqID", SqlDbType.BigInt)
            {
                Value = _params.ReqId ?? (object)DBNull.Value
            });

            // Handling string-based SbuType
            cmd.Parameters.Add(new SqlParameter("@SbuType", SqlDbType.VarChar)
            {
                Value = string.IsNullOrEmpty(_params.SBUType) ? DBNull.Value : _params.SBUType
            });

            // Handling nullable int for Layer
            cmd.Parameters.Add(new SqlParameter("@Layer", SqlDbType.Int)
            {
                Value = _params.Layer ?? (object)DBNull.Value
            });

            // Handling nullable long for CreatedUID
            cmd.Parameters.Add(new SqlParameter("@CreatedUID", SqlDbType.BigInt)
            {
                Value = _params.CreatedUID == 0 ? DBNull.Value : (object)_params.CreatedUID
            });

            var adapter = new SqlDataAdapter(cmd);
            await conn.OpenAsync();
            await Task.Run(() => adapter.Fill(ds));
        }
        catch (SqlException sqlEx)
        {
            // Log detailed SQL error for debugging
            Console.Error.WriteLine($"SQL Error: {sqlEx.Message}");
            throw new Exception("SQL error occurred while fetching the requisition search list.", sqlEx);
        }
        catch (Exception ex)
        {
            // Log general errors
            Console.Error.WriteLine($"General Error: {ex.Message}");
            throw new Exception("An error occurred while fetching the requisition search list.", ex);
        }
        return ds;
    }








    public async Task<DataSet> GetRemarks(long reqId)
    {
        var ds = new DataSet();
        using var conn = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("[dbo].[Usp_GetRemarks]", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@ReqID", reqId);
        var adapter = new SqlDataAdapter(cmd);
        await conn.OpenAsync();
        await Task.Run(() => adapter.Fill(ds));
        return ds;
    }

    

    

    public async Task<long> InsertOrUpdateUnitLOcation(Ims_Requisition_UnitLocation_Request _params)
    {
        long result = 0;
        try
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("Usp_UnitLocationInsUpd", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LocationID", _params.LocationId);
            cmd.Parameters.AddWithValue("@LocationName", _params.LocationName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@AreaID", _params.AreaId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@UnitID", _params.UnitId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyID", _params.CompanyId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedUID", _params.CreatedUid ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@entrypoint", _params.EntryPoint ?? (object)DBNull.Value);
            await conn.OpenAsync();
            result = Convert.ToInt64(await cmd.ExecuteScalarAsync());
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("An SQL error occurred while inserting or updating the unit location.", sqlEx);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while inserting or updating the unit location.", ex);
        }
        return result;
    }

    public long InsertUpdateArea(Ims_Requisition_AreaRequest _params)
    {
        long result = -1;
        var connection = new SqlConnection(_connectionString);
        try
        {
            using SqlCommand cmd = new("[dbo].[Usp_AreaInsupd]", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AreaID", _params.AreaId);
            cmd.Parameters.AddWithValue("@AreaName", _params.AreaName);
            cmd.Parameters.AddWithValue("@UnitId", _params.UnitId);
            cmd.Parameters.AddWithValue("@CompanyID", _params.CompanyId);
            cmd.Parameters.AddWithValue("@CreatedUID", _params.CreatedUid);
            cmd.Parameters.AddWithValue("@entrypoint", _params.EntryPoint);
            connection.Open();
            object returnValue = cmd.ExecuteScalar();
            if (returnValue != null)
            {
                result = Convert.ToInt64(returnValue);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while inserting/updating the area.", ex);
        }
        return result;
    }
    #region Requisition List
    public async Task<DataSet> GetRequisitionAllItem(long reqId)
    {
        var ds = new DataSet();
        using var conn = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("[dbo].[USP_GETREQUISITIONITEMDTLS]", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@ReqID", reqId);
        var adapter = new SqlDataAdapter(cmd);
        await conn.OpenAsync();
        await Task.Run(() => adapter.Fill(ds));
        return ds;
    }
    public async Task<long> InsertOrUpdateRequisitionApproval(Ims_Requisition_ReqApproval _params)
    {
        long result = -1;
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                using SqlCommand cmd = new("[dbo].[Usp_RequisitionApproved]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", _params.ReqId);
                cmd.Parameters.AddWithValue("@STA", _params.STA);
                cmd.Parameters.AddWithValue("@uid", _params.Uid);
                await connection.OpenAsync();
                object returnValue = cmd.ExecuteScalarAsync();
                if (returnValue != null)
                {
                    result = Convert.ToInt64(returnValue);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting/updating the area.", ex);
            }
        }
        return result;
    }
    #endregion

    

    public async Task<long> UpdateRequisitionDetails(Ims_Requisition_Model _params)
    {
        long result = -1;
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                using SqlCommand cmd = new("[dbo].[Usp_SaveReqUpd]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqID", _params.ReqID);
                cmd.Parameters.AddWithValue("@RefReqNo", _params.RefReqNo);
                cmd.Parameters.AddWithValue("@ReqANMType", _params.ReqANMType);
                cmd.Parameters.AddWithValue("@ReqNo", _params.ReqNo);
                cmd.Parameters.AddWithValue("@ReqDate", _params.ReqDate);
                cmd.Parameters.AddWithValue("@ReqType", _params.ReqType);
                cmd.Parameters.AddWithValue("@Type", _params.Type);
                cmd.Parameters.AddWithValue("@IsRateContract", _params.IsRateContract);
                cmd.Parameters.AddWithValue("@WorkStatus", _params.WorkStatus);
                cmd.Parameters.AddWithValue("@UnitID", _params.UnitID);
                cmd.Parameters.AddWithValue("@LocationID", _params.LocationID);
                cmd.Parameters.AddWithValue("@AreaID", _params.AreaID);
                cmd.Parameters.AddWithValue("@Description", _params.Description);
                cmd.Parameters.AddWithValue("@Remarks", _params.Remarks);
                cmd.Parameters.AddWithValue("@Experiment", _params.Experiment);
                cmd.Parameters.AddWithValue("@RejectRemarks", _params.RejectRemarks);
                cmd.Parameters.AddWithValue("@Justification", _params.Justification);
                cmd.Parameters.AddWithValue("@ContactNo", _params.ContactNo);
                cmd.Parameters.AddWithValue("@InitBy", _params.InitBy);
                cmd.Parameters.AddWithValue("@CompanyID", _params.CompanyID);
                cmd.Parameters.AddWithValue("@CreatedUID", _params.CreatedUID);
                cmd.Parameters.AddWithValue("@ApprovedBy", _params.ApprovedBy);
                cmd.Parameters.AddWithValue("@ApprovalStatus", _params.ApprovalStatus);
                cmd.Parameters.AddWithValue("@FrdStatus", _params.FrdStatus);
                cmd.Parameters.AddWithValue("@ApprovedLevel", _params.ApprovedLevel);
                cmd.Parameters.AddWithValue("@SuppDocName1", _params.SuppDocName1);
                cmd.Parameters.AddWithValue("@SuppDoc1", _params.SuppDoc1);
                cmd.Parameters.AddWithValue("@SuppDocName2", _params.SuppDocName2);
                cmd.Parameters.AddWithValue("@SuppDoc2", _params.SuppDoc2);
                cmd.Parameters.AddWithValue("@SuppDocName3", _params.SuppDocName3);
                cmd.Parameters.AddWithValue("@SuppDoc3", _params.SuppDoc3);
                cmd.Parameters.AddWithValue("@ApproveDate", _params.ApproveDate);
                await connection.OpenAsync();
                object returnValue = cmd.ExecuteScalarAsync();
                if (returnValue != null)
                {
                    result = Convert.ToInt64(returnValue);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating requisition details.", ex);
            }
        }
        return result;
    }
}
