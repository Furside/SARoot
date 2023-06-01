using CIS._UtilClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CIS.Models
{
    public class HasContact : DBObject
    {
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class LoginCredential : DBObject
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        //Credential Test v1
        //Preferably do this on sql. If table.rows.count < 1, deny(return false), else(return true)
        public bool CredentialChk(ref Tuple<string, string, int> userNamePass,ref DataTable table)
        {
            foreach (DataRow item in table.Rows)
            {
                if (item["UserName"].ToString() != userNamePass.Item1)
                    continue;

                if (item["Password"].ToString() != userNamePass.Item2)
                    continue;

                if (Convert.ToInt32(item["ID"]) != userNamePass.Item3)
                    continue;

                return true;
            }

            return false;
        }

        public bool CredentialChk(string connectionStringName, string command, Dictionary<string, Tuple<object, int?>> parameters = null)
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
    }

    public class Firm : HasContact
    {
        public int ContactEID { get; set; }
    }

    public class Aging
    {

    }

    //smart sets
    //globe isotech

    public class Person : HasContact
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SuffixName { get; set; }
        public DateTime BirthDate { get; set; }

        public void SetFullName()
        {
            Name = $"{FirstName}{((string.IsNullOrWhiteSpace(MiddleName) )?"":" "+MiddleName) } {LastName}{(string.IsNullOrWhiteSpace(SuffixName) ? "" : " " + SuffixName) }";
        }
    }

    public class Product : DBObject
    {
        public string ProductLine { get; set; }     //product group defined by producer
        public string Category { get; set; }
        public int ProducerID { get; set; }         //
        public int SellerID { get; set; }
        public int TableID { get; set; }
        public Specification Specification { get; set; }
        public List<Product> Products { get; set; }
    }

    public class PhysicalComponent : Product
    {

    }

    public class Specification
    {
        public Dimension Dimension { get; set; }   //x, y, z
        public COLOR_TYPE colorType { get; set; } = COLOR_TYPE.RGB;
        public string Color { get; set; }

        public Specification()
        {
            Dimension.Size = new Tuple<int, int, int>(1,1,1);
        }
    }

    public enum COLOR_TYPE
    {
        RGB,
        CMYK
    }

    public enum UNIT
    {
        LENGTH,
        TIME,
        CURRENCY,
        STORAGE,
        DATASPEED,
        CYCLE,
        WEIGHT
    }

    public enum LENGTH_UNIT
    {
        METRIC,     //meter
        IMPERIAL,   //yard
        MARINE,     //fathom, nautical mile
        AVIATION,   //feet, nautical mile
        SURVEYING,  //chain, rod
        ASTRONOMY,  //
        PHYSICS,
        ARCHAIC,
        INFORMAL,
        OTHERS
    }

    public enum TIME_UNIT
    {
        SECOND,
        MINUTE,
        HOUR,
        DAY,
        MONTH,
        YEAR
    }

    public enum CURRENCY_UNIT
    {
        PESO,
        DOLLAR,
        YUAN
    }

    public enum STORAGE_UNIT
    {
        BYTE
    }

    public enum DATASPEED_UNIT
    {
        BIT
    }

    public enum WEIGHT_UNIT
    {
        KG,
        CUP,
    }

    public interface IRGB
    {
        int r { get; set; }
        int g { get; set; }
        int b { get; set; }
    }

    public interface ICMYK
    {
        int c { get; set; }
        int m { get; set; }
        int y { get; set; }
        int k { get; set; }
    }

    public class Dimension
    {
        public Tuple<int, int, int> Size { get; set; }
        public UNIT Unit { get; set; }
        public Dimension()
        {

        }

        public Dimension(ref int x, ref int y, ref int z, UNIT unit = UNIT.LENGTH )
        {
            Size = new Tuple<int, int, int>(x, y, z);
            Unit = unit;
        }

    }

    public class Package
    {
        List<KeyValuePair<Product, Specification>> Products { get; set; }
    }

    public class Sizeable
    {

    }

    public class Address
    {
        int x { get; set; }
        int y { get; set; }

    }

    public class Producer : HasContact
    {
        List<Tuple<int, int, int>> Products { get; set; }     //KeyValuePair<tableID, productID>
    }

    public class Seller<T> : HasContact
    {
        List<Tuple<int, int, int>> Products { get; set; }
    }

    public class Buyer<T> : HasContact
    {

    }

    public class Employee : Person
    {
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int CompanyID { get; set; }
        LoginCredential Credential { get; set; }
    }

    public class Company : HasContact
    {
        
    }

    public class Factory : Producer
    {

    }

    public class Manufacturer : Producer
    {
        
    }

    public class Chipset : DBObject
    {
        public string Model { get; set; }
        public string Unit { get; set; }
        public string ProductLine { get; set; }

        public int ManufacturerID { get; set; }
    }

    public class NetworkOperator : HasContact
    {

    }

    public class NetworkProvider : DBObject
    {

    }

    public class OperatingSystem : DBObject
    {

    }

    public class BinCard : BaseModel
    {
        public string Company { get; set; } = "ISOTECH";
        public string NumberSeries { get; set; } = "000000-000";
    }

    public class SIM : BaseModel
    {
        public int? ID { get; set; }
        public int PhoneNumber { get; set; }
        public int CountryCode { get; set; }
        public BinCard BinCard { get; set; }
        public string FormatStandard { get; set; }
    }

    

    public class Plan : BaseModel
    {
        public enum PlanStatus
        {
            Qualified,
            WithinContract,
            Recontracted
        }

        public bool HasSim { get; set; } = true;
        public bool HasHandset { get; set; } = true;

        public SIM Sim { get; set; } = null;
        public Handset Handset { get; set; } = null;

        public string Description { get; set; } = "";
        public DateTime StartDate { get; set; } = default(DateTime);
        public DateTime EndDate { get; set; } = default(DateTime);
        public PlanStatus Status { get; set; } = PlanStatus.Qualified;
        public Person EndUser { get; set; }
        public float OverdueBalance { get; set; }
        public float EarlyRenewalFee { get; set; }

    }

    public class Store : DBObject
    {

    }
    public class Screen
    {
        Dimension Dimension { get; set; }
    }
}