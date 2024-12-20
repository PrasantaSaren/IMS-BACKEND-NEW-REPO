using System.Data;
using System.Reflection;

namespace Inventory.AppCode.Helper
{
    public static class GlobalHelper
    {
        public static List<T> DataTableToList<T>(DataTable dt) where T : class, new()
        {
            List<T> lstItems = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
                foreach (DataRow row in dt.Rows)
                    lstItems.Add(ConvertDataRowToGenericType<T>(row));
            //else
            //    lstItems = null;
            return lstItems;
        }
        private static T ConvertDataRowToGenericType<T>(DataRow row) where T : class, new()
        {
            Type entityType = typeof(T);
            T objEntity = new T();
            foreach (DataColumn column in row.Table.Columns)
            {
                object value = row[column.ColumnName];
                if (value == DBNull.Value) value = null;
                PropertyInfo property = entityType.GetProperty(column.ColumnName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);
                try
                {
                    if (property != null && property.CanWrite)
                        property.SetValue(objEntity, value, null);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objEntity;
        }
        public static object GetPropertyValue(this object T, string PropName)
        {
            return T.GetType().GetProperty(PropName) == null ? null : T.GetType().GetProperty(PropName).GetValue(T, null);
        }
    }
}
