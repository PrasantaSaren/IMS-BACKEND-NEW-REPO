using Common_Helper.CommonHelper;
using Dapper;
using Inventory.AppCode;
using Inventory.Models.Request;
using Inventory.Models.Response;
using Inventory.Repository.DBContext;
using Inventory.Repository.IService;
using Microsoft.EntityFrameworkCore;
using System.Data;
// ReSharper disable All

namespace Inventory.Repository.Service
{
    public class AuthService : IAuthService
    {
        #region Declaration
        private readonly IDbConnection _dbConnection;
        private readonly IMSV2Context _dbContext;
        bool result = true;
        AuditLog.BeLogLevel _errorlevel = AuditLog.BeLogLevel.Information;
        AuditLog.BeLogType _errortype = AuditLog.BeLogType.Success;
        string? ExceptionMsg = "";
        string? METHODNAME = "";
        string? TableID = "";
       // CommonService _commonService = new CommonService();
        public AuthService(IConfiguration Configuration, IMSV2Context context)
        {
            ConnectionHelper connectionHelper = new ConnectionHelper(Configuration);
            _dbConnection = connectionHelper.GetDbConnection();
            _dbContext = context;
        }
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
        public async Task<LoginResponse?> GetLoginDetails(LoginRequest user, AuditLogHelper _auditLogHelper)
        {
            var LoginResponse = new LoginResponse();
            METHODNAME = "GetLoginDetails";
            TableID = user.UserName;
            ExceptionMsg = "Successful To Get Login Details";
            try
            {
                string SqlQuery = LoginSql();
                var parameters = new { UserName = user.UserName};
                LoginResponse = await _dbConnection.QuerySingleOrDefaultAsync<LoginResponse>(SqlQuery, parameters);
                dynamic? existingUser = null;
                if (LoginResponse is not null)
                {
                    existingUser = await _dbContext.Users
                        .FirstOrDefaultAsync(u => u.UserId == LoginResponse.UserId);
                }

                if (existingUser is not null)
                {
                    existingUser.LastOtp = "1234";
                    existingUser.LastOtpDate = DateTime.UtcNow;
                    _dbContext.Users.Update(existingUser);
                    _dbContext.SaveChanges();
                }
                else
                {
                    return null;
                }
                LoginResponse!.ExpiryTime = DateTimeOffset.UtcNow.AddMinutes(180).ToUnixTimeSeconds();
                LoginResponse!.Otp = existingUser.LastOtp;
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                ExceptionMsg = "Failed To Get Login Details : - " + ex.Message;
                _errorlevel = AuditLog.BeLogLevel.Error;
                _errortype = AuditLog.BeLogType.SQL;
            }
            //Audit Helper
            AuditLog.CallLog(_auditLogHelper, _errorlevel, _errortype, METHODNAME, TableID, ExceptionMsg);
            if (result)
            {
                return LoginResponse;
            }
            else
            {
                throw new ArgumentNullException(nameof(ExceptionMsg));
            }
        }

        private static string LoginSql()
        {
            const string query =
                "SELECT ISNULL(Login,'') AS LoginId, ISNULL(Email,'') AS EmailId, UserID AS UserId, UserName, UserType AS UserType " +
                "FROM [dbo].[Users] WHERE Login = @UserName AND InActive = 0";
            return query;
        }

        public async Task<bool> UpdateRefreshToken(TokenResponse tokenResponse, long? UserId, AuditLogHelper _auditLogHelper)
        {
            METHODNAME = "UpdateRefreshToken";
            TableID = UserId.ToString();
            ExceptionMsg = "Successfull To Update Refresh Token";
            try
            {
                //var user = await _dbContext.Users.Where(u => u.UserId == UserId).FirstAsync();
                // user.RefreshToken = tokenResponse.RefreshToken;
                // user.RefreshTokenExpiryDate = DateTime.Now.AddDays(7);
                // user.RefreshTokenStartDate = DateTime.Now;
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                ExceptionMsg = "Failed To Update Refresh Token : - " + ex.Message;
                _errorlevel = AuditLog.BeLogLevel.Error;
                _errortype = AuditLog.BeLogType.SQL;
            }
            //Audit Helper
            AuditLog.CallLog(_auditLogHelper, _errorlevel, _errortype, METHODNAME, TableID, ExceptionMsg);
            if (result)
            {
                return result;
            }
            else
            {
                throw new ArgumentNullException(nameof(ExceptionMsg));
            }
        }
        public async Task<Guid?> GetValidToken(string? UserName, string? refreshToken, AuditLogHelper _auditLogHelper)
        {
            METHODNAME = "GetValidToken";
            TableID = UserName;
            ExceptionMsg = "Successfull To Get Valid Token";
            Guid? UserId = null;
            try
            {
                string SqlQuery = ValidTokenSql();
                var parameters = new { UserName = UserName };
                var user = await _dbConnection.QuerySingleOrDefaultAsync(SqlQuery, parameters);
                if (user is not null || user?.RefreshToken != refreshToken || user?.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    UserId = user?.UserId;
                    result = true;
                }
                else
                {
                    result = false;
                    ExceptionMsg = "Faild To Get Valid Token";
                }
            }
            catch (Exception ex)
            {
                result = false;
                ExceptionMsg = "Faild To Get Valid Token : - " + ex.Message;
                _errorlevel = AuditLog.BeLogLevel.Error;
                _errortype = AuditLog.BeLogType.SQL;
            }
            //Audit Helper
            AuditLog.CallLog(_auditLogHelper, _errorlevel, _errortype, METHODNAME, TableID, ExceptionMsg);
            if (result)
            {
                return UserId;
            }
            else
            {
                throw new ArgumentNullException(nameof(ExceptionMsg));
            }
        }
        public static string ValidTokenSql()
        {
            string query = "";
            query = " SELECT UserId, ISNULL(RefreshToken,'')AS RefreshToken, RefreshTokenExpiryDate FROM [User] Where Email = @UserName ";
            return query;
        }
        public async Task<bool> RevokeToken(string? username, AuditLogHelper _auditLogHelper)
        {
            METHODNAME = "RevokeToken";
            TableID = username;
            ExceptionMsg = "Successfull To Revoke Token";
            try
            {
                var user = await _dbContext.Users.Where(u => u.Email == username).FirstAsync();
                //user.RefreshToken = null;
                _dbContext.Update(user);
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                ExceptionMsg = "Faild To Revoke Token : - " + ex.Message;
                _errorlevel = AuditLog.BeLogLevel.Error;
                _errortype = AuditLog.BeLogType.SQL;
            }
            //Audit Helper
            AuditLog.CallLog(_auditLogHelper, _errorlevel, _errortype, METHODNAME, TableID, ExceptionMsg);
            if (result)
            {
                return result;
            }
            else
            {
                throw new ArgumentNullException(nameof(ExceptionMsg));
            }
        }
    }
}
