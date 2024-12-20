using Inventory.AppCode.Helper;
using Inventory.Models.Request;
using Inventory.Repository.DBContext;
using Inventory.Repository.IService;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.Repository.Service
{
    public class GlobalService : IGlobalService
    {
        private readonly string? _connectionString;
        private readonly Imsv2Context _context;

        public GlobalService(IConfiguration configuration, Imsv2Context context)
        {
            _connectionString = configuration.GetConnectionString("ProjectConnection");
            _context = context;
        }
        public async Task<List<T>> GetGlobalDropdownList<T>(GlobalDropdownRequest request) where T : class, new()
        {
            try
            {
                var result = new List<T>();
                SqlConnection? connection = null;
                DataSet ds = new DataSet();
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("GetGlobalDropdownSelect_SP", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@TableName", request.TableName);
                    command.Parameters.AddWithValue("@IdFieldName", request.IdFieldName);
                    command.Parameters.AddWithValue("@NameFieldName", request.NameFieldName);
                    command.Parameters.AddWithValue("@WhereFieldName", string.IsNullOrEmpty(request.WhereFieldName) ? (object)DBNull.Value : request.WhereFieldName);
                    command.Parameters.AddWithValue("@WhereFieldValue", string.IsNullOrEmpty(request.WhereFieldValue) ? (object)DBNull.Value : request.WhereFieldValue);
                    command.Parameters.AddWithValue("@WhereField2Name", string.IsNullOrEmpty(request.WhereField2Name) ? (object)DBNull.Value : request.WhereField2Name);
                    command.Parameters.AddWithValue("@WhereField2Value", string.IsNullOrEmpty(request.WhereField2Value) ? (object)DBNull.Value : request.WhereField2Value);
                    var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        result = GlobalHelper.DataTableToList<T>(ds.Tables[0]);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving data.", ex);
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<DataTable> GetGlobalSelectTable(GlobalSelectTableRequest globalSelectTableRequest)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dataTable = new DataTable();
                SqlConnection? connection = null;
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("GetGlobalSelectTable_SP", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@TableName", globalSelectTableRequest.TableName);
                    command.Parameters.AddWithValue("@WhereFieldName", string.IsNullOrEmpty(globalSelectTableRequest.WhereFieldName) ? (object)DBNull.Value : globalSelectTableRequest.WhereFieldName);
                    command.Parameters.AddWithValue("@WhereFieldValue", string.IsNullOrEmpty(globalSelectTableRequest.WhereFieldValue) ? (object)DBNull.Value : globalSelectTableRequest.WhereFieldValue);
                    command.Parameters.AddWithValue("@WhereField2Name", string.IsNullOrEmpty(globalSelectTableRequest.WhereField2Name) ? (object)DBNull.Value : globalSelectTableRequest.WhereField2Name);
                    command.Parameters.AddWithValue("@WhereField2Value", string.IsNullOrEmpty(globalSelectTableRequest.WhereField2Value) ? (object)DBNull.Value : globalSelectTableRequest.WhereField2Value);
                    var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dataTable = ds.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving data.", ex);
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<Dictionary<string, object>?> GetGlobalSelectTableRow(GlobalSelectTableRowRequest globalSelectTableRowRequest)
        {
            try
            {
                DataSet ds = new DataSet();
                Dictionary<string, object>? result = null;
                SqlConnection? connection = null;
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("GetGlobalSelectTableRow_SP", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@TableName", globalSelectTableRowRequest.TableName);
                    command.Parameters.AddWithValue("@WhereFieldName", string.IsNullOrEmpty(globalSelectTableRowRequest.WhereFieldName) ? (object)DBNull.Value : globalSelectTableRowRequest.WhereFieldName);
                    command.Parameters.AddWithValue("@WhereFieldValue", string.IsNullOrEmpty(globalSelectTableRowRequest.WhereFieldValue) ? (object)DBNull.Value : globalSelectTableRowRequest.WhereFieldValue);
                    command.Parameters.AddWithValue("@WhereField2Name", string.IsNullOrEmpty(globalSelectTableRowRequest.WhereField2Name) ? (object)DBNull.Value : globalSelectTableRowRequest.WhereField2Name);
                    command.Parameters.AddWithValue("@WhereField2Value", string.IsNullOrEmpty(globalSelectTableRowRequest.WhereField2Value) ? (object)DBNull.Value : globalSelectTableRowRequest.WhereField2Value);
                    var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dataRow = ds.Tables[0].Rows[0];
                        result = dataRow.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dataRow[col]);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving data.", ex);
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<bool> GlobalLogicalDelete(GlobalLogicalDeleteRequest globalLogicalDeleteRequest)
        {
            try
            {
                bool result = false;
                SqlConnection? connection = null;
                try
                {
                    connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("GlobalLogicalDelete_SP", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@TableName", globalLogicalDeleteRequest.TableName);
                    command.Parameters.AddWithValue("@WhereFieldName", string.IsNullOrEmpty(globalLogicalDeleteRequest.WhereFieldName) ? (object)DBNull.Value : globalLogicalDeleteRequest.WhereFieldName);
                    command.Parameters.AddWithValue("@WhereFieldValue", string.IsNullOrEmpty(globalLogicalDeleteRequest.WhereFieldValue) ? (object)DBNull.Value : globalLogicalDeleteRequest.WhereFieldValue);
                    command.Parameters.AddWithValue("@IsActiveFieldValue", string.IsNullOrEmpty(globalLogicalDeleteRequest.IsActiveFieldValue) ? (object)DBNull.Value : globalLogicalDeleteRequest.IsActiveFieldValue);
                    command.Parameters.AddWithValue("@IsActiveFieldName", string.IsNullOrEmpty(globalLogicalDeleteRequest.IsActiveFieldName) ? (object)DBNull.Value : globalLogicalDeleteRequest.IsActiveFieldName);
                    var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    var outputId = (long)outputIdParam.Value;
                    if (outputId == 1)
                    {
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while delete data.", ex);
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
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
