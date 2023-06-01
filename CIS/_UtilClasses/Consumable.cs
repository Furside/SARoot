using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CIS._UtilClasses
{
    interface IConsumable
    {
        
    }

    class Consumable : IConsumable
    {
        int? Unit { get; set; }
        int? Quantity { get; set; }
        int? Multiplier { get; set; }
        DateTime? EffectivityStart { get; set; }
        DateTime? EffectivityEnd { get; set; }
        TimeSpan? lifeSpan;
        public TimeSpan? LifeSpan {
            get {
                return lifeSpan ?? ( EffectivityEnd - EffectivityStart );
            }
            set {
                EffectivityEnd = EffectivityStart + ( lifeSpan ?? TimeSpan.Zero) ;
                lifeSpan = value;
            }
        }

    }

    class DataBaseObject
    {
        protected int? ID { get; set; }
        protected string Name { get; set; } = null;
        protected int? EncodedBy { get; set; }
        protected DateTime? DateEncoded { get; set; }
        protected int? ModifiedBy { get; set; }
        protected DateTime? DateModified { get; set; }
        protected List<PropertyInfo> PropertyInfoList { get; set; }
        protected void SetPropertyInfoList(ref object dbObj)
        {
            PropertyInfoList.AddRange(dbObj.GetType().GetProperties());
        }
        public void CreateTable()
        {
            
        }
        public void CreateTable(ref string ConnectionStringName, Dictionary<string, object> Parameters = null)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString);
            connection.Open();
            List<string> tbl_Fields = new List<string>();
            DataBaseObject dbObj = new DataBaseObject();
            string commandString = "use dbsample " +
                "CREATE TABLE tbl_" + Name + " (";
            foreach ( PropertyInfo info in PropertyInfoList )
            {
                tbl_Fields.Add(info.Name + " " + info.PropertyType.ToString());
                SqlDbType sqlDbType = GetSqlDbType(info.PropertyType);
            }
            commandString += string.Join(", ", tbl_Fields) + ")";

            SqlCommand cm = new SqlCommand(commandString, connection);
            if (Parameters != null)
            {
                foreach (KeyValuePair<string, object> d in Parameters)
                {
                    cm.Parameters.Add(new SqlParameter(d.Key, (d.Value == null) ? DBNull.Value : d.Value));
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(cm);
        }

        private static SqlDbType GetSqlDbType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int64:
                    return SqlDbType.BigInt;
                case TypeCode.Object:
                    return SqlDbType.Binary;    // Image | Binary | UniqeuIdentifier | Timestamp | VarBinary | Variant
                case TypeCode.Boolean:
                    return SqlDbType.Bit;
                case TypeCode.String:
                    return SqlDbType.Char;      // NChar | NText | NVarChar | Text | VarChar
                case TypeCode.DateTime:
                    return SqlDbType.DateTime;  // SmallDateTime | DateTime2 | DateTimeOffset
                case TypeCode.Decimal:
                    return SqlDbType.Decimal;   // Decimal | Money | SmallMoney
                case TypeCode.Double:
                    return SqlDbType.Float;
                case TypeCode.Int32:
                    return SqlDbType.Int;
                case TypeCode.Single:
                    return SqlDbType.Real;
                case TypeCode.Int16:
                    return SqlDbType.SmallInt;
                case TypeCode.Byte:
                    return SqlDbType.TinyInt;

                //case XML:
                //    return SqlDbType.Xml;
                //case UDT:
                //    return SqlDbType.Udt;
                //case structuredData:
                //    return SqlDbType.Structured;
                //case Date:
                //    return SqlDbType.Date;
                //case Time:
                //    return SqlDbType.Time;
                default:
                    throw new ArgumentException("Unsupported data type: " + type.FullName);
            }
        }

        public DataTable Query(string connectionStringName, string command, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString))
            {
                cn.Open();
                SqlCommand cm = new SqlCommand(command, cn);
                if (parameters != null)
                {
                    AddParameters(ref cm, ref parameters);
                }
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt;
                da.Fill(dt = new DataTable());
                cn.Close();

                return dt;
            }
        }


        public void AddParameters(ref SqlCommand command, ref Dictionary<string, object> parameters)
        {
            foreach (KeyValuePair<string, object> d in parameters)
            {
                command.Parameters.Add(new SqlParameter(d.Key, (d.Value == null) ? DBNull.Value : d.Value));
            }
        }
        public void Dispose(ref object[] objects)
        {

        }
    }

    class Contact : DataBaseObject
    {
        public void Method()
        {

        }
    }

    enum UNIT
    {
        LENGTH,
        TIME,
        MONEY,
    }

    interface IDuration
    {
        int ey { get; set; }
        TimeSpan GetDuration(ref DateTime startDate, ref DateTime endDate);
    }

    class Duration : IDuration
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        TimeSpan IDuration.GetDuration(ref DateTime startDate, ref DateTime endDate)
        {
            TimeSpan ts = endDate - startDate;
            return ts;
        }
        int IDuration.ey
        {
            get; set;
        }
    }
}
