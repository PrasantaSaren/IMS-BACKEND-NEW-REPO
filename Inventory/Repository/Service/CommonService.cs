using Inventory.Repository.DBContext;
using Microsoft.EntityFrameworkCore;
using Inventory.AppCode;
using System.Data;
using Dapper;

namespace Inventory.Repository.Service
{
    public class CommonService
    {
        #region Declaration
        private readonly IDbConnection _dbConnection;
        private readonly IMSV2Context _dbContext;
        bool result = true;
        #endregion
        public CommonService(IConfiguration Configuration)
        {
            ConnectionHelper connectionHelper = new ConnectionHelper(Configuration);
            _dbConnection = connectionHelper.GetDbConnection();
            _dbContext = connectionHelper.GetDbContext();
        }

        // public CommonService()
        // {
        //     throw new NotImplementedException();
        // }

        #region Connection
        private void connectionOpen()
        {
            _dbConnection.Open();
            _dbConnection.BeginTransaction();
        }
        private void connectionClose()
        {
            _dbConnection.Dispose();
            _dbConnection.Close();
        }
        #endregion
        public async Task<string> AddUpdate()
        {
            string ReturnOutPut = string.Empty;
            try
            {
                connectionOpen();
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@Column", "Data");
                ObjParm.Add("@ReturnOutPut", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                _ = await _dbConnection.ExecuteAsync("StoreProcedureName", ObjParm, commandType: CommandType.StoredProcedure);
                //Getting the out parameter value of stored procedure  
                ReturnOutPut = ObjParm.Get<string>("@ReturnOutPut");
            }
            catch (Exception)
            {
                connectionClose();
                result = false;
            }
            //Audit Helper
            if (result)
            {
                return ReturnOutPut;
            }
            else
            {
                return ReturnOutPut;
            }

        }
        public async Task<DataTable> GetTable(string TableName)
        {
            DataTable dt = new();
            try
            {
                connectionOpen();
                var ObjParm = new { TableName };
                dt.Load(await _dbConnection.ExecuteReaderAsync("storedProcedureName", ObjParm, commandType: CommandType.StoredProcedure));
            }
            catch (Exception)
            {
                connectionClose();
                result = false;
            }
            //Audit Helper
            if (result)
            {
                return dt;
            }
            else
            {
                return dt;
            }
        }
        public DateTime GetIndianDatetime()
        {
            var indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var indianDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);
            return indianDateTime;
        }
        
    }
}
