// ReSharper disable All
// ReSharper disable once StringLiteralTypo
using System.Data;
using System.Data.SqlClient;
using Inventory.Models.Master;
using Inventory.Models.Request;
using Inventory.Models.Response;
using Inventory.Repository.DBContext;
using Inventory.Repository.IService;
using Microsoft.EntityFrameworkCore;


namespace Inventory.Repository.Service
{
    public class MasterRepository : IMasterRepository
    {
        private readonly string? _connectionString;
        private readonly IMSV2Context _context;

        public MasterRepository(IConfiguration configuration, IMSV2Context context)
        {
            _connectionString = configuration.GetConnectionString("ProjectConnection");
            _context = context;
        }

        #region UOM
        public DataSet GetUOMs()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridBindUOM", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }

        public int InsertOrUpdateUOM(Ims_M_UOM uom)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_UOMInsertUpdate", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@UOMID", uom.UOMID);
            cmd.Parameters.AddWithValue("@UOMtype", uom.UOMtype);
            cmd.Parameters.AddWithValue("@UOMname", uom.UOMname);
            cmd.Parameters.AddWithValue("@CompanyID", uom.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", uom.CreatedUID);

            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }

        public DataSet SearchUOM(string uomName)
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridBindUOMSearch", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@UOMname", uomName);
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }
        #endregion

        #region Unit/SBU
        public int AddUpdateUnitDetails(UnitModel unit)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_UnitInsertUpdate", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@UnitID", unit.UnitID);
            cmd.Parameters.AddWithValue("@UnitName", unit.UnitName);
            cmd.Parameters.AddWithValue("@SBUcode", unit.SBUcode);
            cmd.Parameters.AddWithValue("@ShortCode", unit.ShortCode);
            cmd.Parameters.AddWithValue("@SBUType", unit.SBUType);
            cmd.Parameters.AddWithValue("@Address", unit.Address);
            cmd.Parameters.AddWithValue("@Phone", unit.Phone);
            cmd.Parameters.AddWithValue("@Email", unit.Email);
            cmd.Parameters.AddWithValue("@LogoUrl", unit.LogoUrl);
            cmd.Parameters.AddWithValue("@ImageData", unit.ImageData);
            cmd.Parameters.AddWithValue("@CompanyID", unit.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", unit.CreatedUID);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }

        public DataSet GetUnitDetailsList()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridBindUnit", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }
        #endregion

        #region Area/Department
        public DataSet GetAreaDepartmentDetailsList()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridBindArea", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }

        public int AddUpdateAreaDepartmentDetails(AreaDepartmentModel area)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_AreaInsertUpdate", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@AreaID", area.AreaID);
            cmd.Parameters.AddWithValue("@AreaName", area.AreaName);
            cmd.Parameters.AddWithValue("@AreaCode", area.AreaCode);
            cmd.Parameters.AddWithValue("@UnitID", area.UnitID);
            cmd.Parameters.AddWithValue("@CompanyID", area.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", area.CreatedUID);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }
        #endregion

        #region Unit Location Section
        public DataSet GetUnitLocationDetailsList()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridBindLocation", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }

        public int AddUpdateUnitLocationDetails(UnitLocationModel unitLocation)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_LocationInsertUpdate", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@LocationID", unitLocation.LocationID);
            cmd.Parameters.AddWithValue("@AreaID", unitLocation.AreaID);
            cmd.Parameters.AddWithValue("@LocationName", unitLocation.LocationName);
            cmd.Parameters.AddWithValue("@LocationCode", unitLocation.LocationCode);
            cmd.Parameters.AddWithValue("@UnitID", unitLocation.UnitID);
            cmd.Parameters.AddWithValue("@CompanyID", unitLocation.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", unitLocation.CreatedUID);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }
        #endregion

        #region Job Type Section
        public DataSet GetJobTypeDetailsList()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridJobType", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }

        public int AddUpdateJobTypeDetails(JobTypeModel jobType)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_JobTypeInsertUpdate", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@JobTypeID", jobType.JobTypeID);
            cmd.Parameters.AddWithValue("@JobTypeName", jobType.JobTypeName);
            cmd.Parameters.AddWithValue("@CompanyID", jobType.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", jobType.CreatedUID);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }
        #endregion

        #region Job Section
        public DataSet GetJobDetailsList()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridBindJob", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }

        public int AddUpdateJobDetails(JobModel job)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_JobInsertUpdate", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@JobTypeID", job.JobTypeID);
            cmd.Parameters.AddWithValue("@JobID", job.JobID);
            cmd.Parameters.AddWithValue("@JobName", job.JobName);
            cmd.Parameters.AddWithValue("@JobNmaedescr", job.JobNmaedescr);
            cmd.Parameters.AddWithValue("@CompanyID", job.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", job.CreatedUID);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }
        #endregion

        #region Item Group Section
        public DataSet GetItemGroupDetailsList()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridItemGroup", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }

        public int AddUpdateItemGroupDetails(ItemGroupModel itemGroup)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_ItemGroupInsertUpdate", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ItemGroupID", itemGroup.ItemGroupID);
            cmd.Parameters.AddWithValue("@ItemGroupName", itemGroup.ItemGroupName);
            cmd.Parameters.AddWithValue("@CompanyID", itemGroup.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", itemGroup.CreatedUID);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }
        #endregion

        #region Item Sub Group Section
        public DataSet GetItemSubGroupDetailsList()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridItemSubGroup", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }

        public int AddUpdateItemSubGroupDetails(ItemSubGroupModel itemSubGroup)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_ItemSubGroupInsertUpdate", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ItemGroupID", itemSubGroup.ItemGroupID);
            cmd.Parameters.AddWithValue("@ItemSubGroupID", itemSubGroup.ItemSubGroupID);
            cmd.Parameters.AddWithValue("@ItemSubGroupName", itemSubGroup.ItemSubGroupName);
            cmd.Parameters.AddWithValue("@SetSubGroupName", itemSubGroup.SetSubGroupName);
            cmd.Parameters.AddWithValue("@CompanyID", itemSubGroup.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", itemSubGroup.CreatedUID);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }
        #endregion

        #region Bank Section
        public DataSet GetBankDetailsList()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridBindBank", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }

        public int AddUpdateBankDetails(BankModel bank)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_BankInsertUpdate", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@BankID", bank.BankID);
            cmd.Parameters.AddWithValue("@BankName", bank.BankName);
            cmd.Parameters.AddWithValue("@BaranchName", bank.BaranchName);
            cmd.Parameters.AddWithValue("@Ifsc_Code", bank.Ifsc_Code);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }
        #endregion

        #region Vendor Section
        public DataSet GetVendorDetailsList()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_GridBindCustomerVendor", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }

        public int AddUpdateVendorDetails(VendorModel vendor)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_CustomerVendorInsertUpdate", conn) { CommandType = CommandType.StoredProcedure };

            //For Item
            var ItemIDListTable = new DataTable();
            ItemIDListTable.Columns.Add("ItemTypeID", typeof(long));
            foreach (var item in vendor.ItemIDList) { ItemIDListTable.Rows.Add(item.ItemTypeID); }

            //For Job
            var JobIDListTable = new DataTable();
            JobIDListTable.Columns.Add("jobTypeID", typeof(long));
            foreach (var item in vendor.JobIDList) { JobIDListTable.Rows.Add(item.jobTypeID); }

            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ItemIDList", TypeName = "dbo.VendorItemType", Value = ItemIDListTable });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@JobIDList", TypeName = "dbo.JobIDTYPE", Value = JobIDListTable });
            cmd.Parameters.AddWithValue("@CV_ID", vendor.CV_ID);
            cmd.Parameters.AddWithValue("@DescripTionJob", vendor.DescripTionJob);
            cmd.Parameters.AddWithValue("@DescripTion", vendor.DescripTion);
            cmd.Parameters.AddWithValue("@CV_Tag", vendor.CV_Tag);
            cmd.Parameters.AddWithValue("@DealsIn", vendor.DealsIn);
            cmd.Parameters.AddWithValue("@CV_Code", vendor.CV_Code);
            cmd.Parameters.AddWithValue("@CV_Name", vendor.CV_Name);
            cmd.Parameters.AddWithValue("@ContactPerson", vendor.ContactPerson);
            cmd.Parameters.AddWithValue("@RegNumber", vendor.RegNumber);
            cmd.Parameters.AddWithValue("@RegDate", vendor.RegDate);
            cmd.Parameters.AddWithValue("@ServiceTaxNumber", vendor.ServiceTaxNumber);
            cmd.Parameters.AddWithValue("@CV_Address", vendor.CV_Address);
            cmd.Parameters.AddWithValue("@CVDistrict", vendor.CVDistrict);
            cmd.Parameters.AddWithValue("@CV_PinCode", vendor.CV_PinCode);
            cmd.Parameters.AddWithValue("@CV_State", vendor.CV_State);
            cmd.Parameters.AddWithValue("@CV_Phone", vendor.CV_Phone);
            cmd.Parameters.AddWithValue("@CV_Mobile", vendor.CV_Mobile);
            cmd.Parameters.AddWithValue("@CV_Email", vendor.CV_Email);
            cmd.Parameters.AddWithValue("@CV_Website", vendor.CV_Website);
            cmd.Parameters.AddWithValue("@CV_PANCardNo", vendor.CV_PANCardNo);
            cmd.Parameters.AddWithValue("@CV_TAN", vendor.CV_TAN);
            cmd.Parameters.AddWithValue("@CV_WBST_NO", vendor.CV_WBST_NO);
            cmd.Parameters.AddWithValue("@CV_CST_NO", vendor.CV_CST_NO);
            cmd.Parameters.AddWithValue("@BankCode", vendor.BankCode);
            cmd.Parameters.AddWithValue("@Bankbranch", vendor.Bankbranch);
            cmd.Parameters.AddWithValue("@VAT", vendor.VAT);
            cmd.Parameters.AddWithValue("@IFSCCode", vendor.IFSCCode);
            cmd.Parameters.AddWithValue("@AccountNumber", vendor.AccountNumber);
            cmd.Parameters.AddWithValue("@AccType", vendor.AccType);
            cmd.Parameters.AddWithValue("@UnitID", vendor.UnitID);
            cmd.Parameters.AddWithValue("@Status", vendor.Status);
            cmd.Parameters.AddWithValue("@file", vendor.file);
            cmd.Parameters.AddWithValue("@Type", vendor.Type);
            cmd.Parameters.AddWithValue("@CompanyID", vendor.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", vendor.CreatedUID);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }
        #endregion

        #region Item Section
        public int AddUpdateItemEntryDetails(ItemEntryModel itemEntryModel)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_ItemInsertUpdate", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ItemSubGroupID", itemEntryModel.ItemSubGroupID);
            cmd.Parameters.AddWithValue("@HiddenfildID", itemEntryModel.HiddenfildID);
            cmd.Parameters.AddWithValue("@UnitID", itemEntryModel.UnitID);
            cmd.Parameters.AddWithValue("@LocationID", itemEntryModel.LocationID);
            cmd.Parameters.AddWithValue("@AreaID", itemEntryModel.AreaID);
            cmd.Parameters.AddWithValue("@UOMID", itemEntryModel.UOMID);
            cmd.Parameters.AddWithValue("@ROL", itemEntryModel.ROL);
            cmd.Parameters.AddWithValue("@RQTY", itemEntryModel.RQTY);
            cmd.Parameters.AddWithValue("@SOH", itemEntryModel.SOH);
            cmd.Parameters.AddWithValue("@StoreUOMID", itemEntryModel.StoreUOMID);
            cmd.Parameters.AddWithValue("@Rate", itemEntryModel.Rate);
            cmd.Parameters.AddWithValue("@VAT", itemEntryModel.VAT);
            cmd.Parameters.AddWithValue("@ItemName", itemEntryModel.ItemName);
            cmd.Parameters.AddWithValue("@Exprement", itemEntryModel.Exprement);
            cmd.Parameters.AddWithValue("@ItemType", itemEntryModel.ItemType);
            cmd.Parameters.AddWithValue("@CompanyID", itemEntryModel.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", itemEntryModel.CreatedUID);
            cmd.Parameters.AddWithValue("@ItemDescHTML", itemEntryModel.ItemDescHTML);
            cmd.Parameters.AddWithValue("@AssetTypeID", itemEntryModel.AssetTypeID);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }

        public int AddUpdateItemServiceDetails(ItemServiceModel itemServiceModel)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_ItemInsertUpdatedetails", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ItemID", itemServiceModel.ItemID);
            cmd.Parameters.AddWithValue("@HiddenfildID", itemServiceModel.HiddenfildID);
            cmd.Parameters.AddWithValue("@Make", itemServiceModel.Make);
            cmd.Parameters.AddWithValue("@Model", itemServiceModel.Model);
            cmd.Parameters.AddWithValue("@Serial", itemServiceModel.Serial);
            cmd.Parameters.AddWithValue("@CompanyID", itemServiceModel.CompanyID);
            cmd.Parameters.AddWithValue("@CreatedUID", itemServiceModel.CreatedUID);
            cmd.Parameters.AddWithValue("@PurchaseDate", itemServiceModel.PurchaseDate);
            cmd.Parameters.AddWithValue("@InstallationDate", itemServiceModel.InstallationDate);
            cmd.Parameters.AddWithValue("@WarrantyTerm", itemServiceModel.WarrantyTerm);
            cmd.Parameters.AddWithValue("@WarrantyDetail", itemServiceModel.WarrantyDetail);
            cmd.Parameters.AddWithValue("@LogBookSerial", itemServiceModel.LogBookSerial);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }

        public int UpdateItemAnnualMaintenanceDetails(ItemMaintenanceModel annualMaintenance)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_ItemInsertUpdateMain", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ItemID", annualMaintenance.ItemID);
            cmd.Parameters.AddWithValue("@AmcProviderID", annualMaintenance.AmcProviderID);
            cmd.Parameters.AddWithValue("@AmcValue", annualMaintenance.AmcValue);
            cmd.Parameters.AddWithValue("@AmcType", annualMaintenance.AmcType);
            cmd.Parameters.AddWithValue("@AmcStartDate", annualMaintenance.AmcStartDate);
            cmd.Parameters.AddWithValue("@AmcRenewDate", annualMaintenance.AmcRenewDate);
            cmd.Parameters.AddWithValue("@Status", annualMaintenance.Status);
            cmd.Parameters.AddWithValue("@ItemDetailID", annualMaintenance.ItemDetailID);
            cmd.Parameters.AddWithValue("@SupplierID", annualMaintenance.SupplierID);
            cmd.Parameters.AddWithValue("@SupplierContactNo", annualMaintenance.SupplierContactNo);
            var outputIdParam = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputIdParam);
            conn.Open();
            cmd.ExecuteNonQuery();
            var outputId = (long)outputIdParam.Value;
            return (int)outputId;
        }

        public DataSet GetItemEntryDetailsList()
        {
            var ds = new DataSet();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("Usp_bindgridviewItem", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }
        #endregion

    }
}

