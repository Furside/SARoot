using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CIS._UtilClasses
{
    public class DBObject
    {
        public int ID { get; set; }
        virtual public string Name { get { return Name??""; } set { Name = value.Trim(); } }
        public int? EncBy { get; set; }
        public DateTime? EncDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<PropertyInfo> PropertyInfoList { get; set; }
        public string GetTableName { get; set; }

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

        public DataTable Query(string connectionStringName, string command, Dictionary<string, Tuple<object, int?>> parameters = null)
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

        public bool CredentialCheck(string connectionStringName, string command, Dictionary<string, object> parameters)
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

                if (dt.Columns.Count == 0)
                {
                    return false;
                }

                return true;
            }


        }

        public void AddParameters(ref SqlCommand command, ref Dictionary<string, Tuple<object, int?>> parameters)
        {
            foreach (KeyValuePair<string, Tuple<object, int?>> d in parameters)
            {
                if (Type.GetTypeCode(d.Value.GetType()) == TypeCode.String)
                {
                    command.Parameters.Add(new SqlParameter(d.Key, (string.IsNullOrWhiteSpace(d.Value.Item1.ToString())) ? "NULL" : d.Value.Item1.ToString().Trim()));
                    continue;
                }
                command.Parameters.Add(new SqlParameter(d.Key, (d.Value.Item1 == null) ? DBNull.Value : d.Value.Item1));
            }
        }

        public void AddParameters(ref SqlCommand command, ref Dictionary<string, object> parameters)
        {
            foreach (KeyValuePair<string, object> d in parameters)
            {
                if (Type.GetTypeCode(d.Value.GetType()) == TypeCode.String)
                {
                    command.Parameters.Add(new SqlParameter(d.Key, (string.IsNullOrWhiteSpace(d.Value.ToString())) ? "NULL" : d.Value.ToString().Trim()));
                    continue;
                }
                command.Parameters.Add(new SqlParameter(d.Key, (d.Value == null) ? DBNull.Value : d.Value));
            }
        }

        //gonna make this back after project
        public void CreateTable(string tblName, Dictionary<string, Tuple<object, int?>> Parameters = null)
        {
            //Tuple<int, int> asd = new Tuple<int, int>(1, 2);
            //SqlConnection connection = new SqlConnection(@"SERVER=(local)\sqlExpress;DATABASE=dbsample;USER=SA;PWD=1234");
            //connection.Open();
            //List<string> tbl_Fields = new List<string>();
            //DataBaseObject dbObj = new DataBaseObject();
            //string commandString = $"use dbsample CREATE TABLE tbl_{tblName} (";
            //foreach (PropertyInfo info in PropertyInfoList)
            //{
            //    if (GetSqlDbType(info.PropertyType.Name.ToString()) != "")
            //    {
            //        //Console.WriteLine(info.Name + " " + GetSqlDbType(info.PropertyType.ToString()));
            //        tbl_Fields.Add(info.Name + " " + GetSqlDbType(info.PropertyType.ToString()));
            //    }
            //}
            //commandString += string.Join(", ", tbl_Fields) + ")";

            ////Console.WriteLine("\n\n"+commandString);
            ////Console.ReadLine();

            //SqlCommand cm = new SqlCommand(commandString, connection);
            //SqlDataAdapter da = new SqlDataAdapter(cm);

            //DataTable dt;
            //try
            //{
            //    da.Fill(dt = new DataTable());
            //}
            //catch (Exception e)
            //{
            //    //Console.WriteLine(e);
            //    //Console.ReadLine();
            //}
            //connection.Close();
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

        private static string GetSqlDbType(string type, int? len)
        {
            switch (type)
            {
                case "System.Int64":
                    return "BigInt";
                case "System.Object":
                    return "Binary";    // Image | Binary | UniqeuIdentifier | Timestamp | VarBinary | Variant
                case "System.Boolean":
                    return "Bit";
                case "System.String":
                    if (len != null)
                    {
                        return $"VarChar({len})";
                    }
                    return "VarChar(MAX)";      // NChar | NText | NVarChar | Text | VarChar | Char
                case "System.DateTime":
                    return "DateTime";  // SmallDateTime | DateTime2 | DateTimeOffset | DateTime
                case "System.Decimal":
                    return "Decimal";   // Decimal | Money | SmallMoney
                case "System.Double":
                    return "Float";
                case "System.Int32":
                    return "Int";
                case "System.Single":
                    return "Real";
                case "System.Int16":
                    return "SmallInt";
                case "System.Byte":
                    return "TinyInt";

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
                    return "";
            }
        }

        //public List<T> DBObjects<T>(Type objectType, string connectionStringName, string tableName = "")
        //{
        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString))
        //    {
        //        cn.Open();
        //        SqlCommand cm = new SqlCommand($"Select * from tbl_{objectType}", cn);

        //        SqlDataAdapter da = new SqlDataAdapter(cm);
        //        DataTable dt;
        //        da.Fill(dt = new DataTable());
        //        cn.Close();

        //        List<T> list = new List<T>();

        //        return list;
        //    }

        //}


        //public void CreateTable(string name, Dictionary<string, object> AdditionalColumns, string user = "576")
        //{
        //    if (Exists(name))
        //    {

        //    }
        //    Name = name;
        //    EncBy = user;
        //    EncDate = DateTime.Now;
        //}

        //public bool Exists(string name)
        //{
        //    return false;
        //}

        //public void DeleteRecord<T>( string tableName, int ID )
        //{
        //    //Query("Contact", "Select * from tbl_Contacts where ID = @ID", new Dictionary<string, object>
        //    //{
        //    //    {"@ID", null}
        //    //});
        //}


    }


}