using System.Data;
using System.Reflection;

namespace XBoot.Composables
{
    public static class XBootExtensions
    {
        /// <summary>
        ///  将dt对象转换成 实体模型
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T>? ConvertToModel<T>(DataTable? dt) where T : new()
        {
            IList<T>? ts = new List<T>();// 定义集合
            if (dt == null)
                return ts.ToList();
            foreach (DataRow dr in dt.Rows)
            {
                T t = new();
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    string tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {

                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            if (pi.PropertyType.Name == "Boolean")
                                value = Convert.ToBoolean(value);
                            if (pi.PropertyType.Name == "Decimal")
                                value = Convert.ToDecimal(value);
                            if (pi.PropertyType.Name == "DateTime")
                                value = Convert.ToDateTime(value);
                            if (pi.PropertyType.Name == "Date")
                                value = Convert.ToDateTime(value);
                            if (pi.PropertyType.Name == "Double")
                                value = Convert.ToDouble(value);
                            if (pi.PropertyType.Name == "Int32")
                                value = Convert.ToInt32(value);
                            if (pi.PropertyType.Name == "Int64")
                                value = Convert.ToInt64(value);

                            pi.SetValue(t, value, null);
                        }
                    }
                }
                ts.Add(t);
            }
            return ts.ToList();
        }

    }
}
