using CIS._UtilClasses;
using PMSRedirect;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CIS.Models
{
    public class Account
    {
        #region Fields
        UserSessions session = new UserSessions();

        [Required]
        public int? ID { get; set; }

        [Display(Name ="Holder ID")]
        public int? HolderID { get; set; }

        [Display(Name = "Holder Type")]
        public int? HolderType { get; set; }

        enum HOLDERTYPE
        {
            COMPANY,
            DEPARTMENT,
            OFFICE,
            GROUP,
            INDIVIDUAL
        }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public int? Status { get; set; }

        [Display(Name = "Encoded By")]
        public int? EncBy { get; set; }

        [Display(Name = "Date Encoded")]
        public DateTime? EncDate { get; set; }

        [Display(Name = "Last Modified By")]
        public int? ModifiedBy { get; set; }

        [Display(Name = "Last Date Modified")]
        public DateTime? ModifiedDate { get; set; }

        


        #endregion Fields

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Contact"].ConnectionString);

        #region Select
        public List<Account> Select()
        {
            DBObject dbObj = new DBObject();
            DataTable dt = dbObj.Query(
                "Contact",
                "SELECT * from tbl_Accounts",
                new Dictionary<string, object>() 
            ) ;

            List<Account> accounts = new List<Account>();
            
            foreach (DataRow r in dt.Rows)
            {
                accounts.Add(
                    new Account()
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),
                        HolderID = r.IsNull("HolderID") ? default(int) : Convert.ToInt32(r["HolderID"]),
                        HolderType = r.IsNull("HolderType") ? default(int) : Convert.ToInt32(r["HolderType"]),
                        Status = r.IsNull("Status") ? default(int) : Convert.ToInt32(r["Status"]),
                        StartDate = r.IsNull("StartDate") ? default(DateTime) : Convert.ToDateTime(r["StartDate"]),
                        EndDate = r.IsNull("EndDate") ? default(DateTime) : Convert.ToDateTime(r["EndDate"]),

                        EncBy = Convert.ToInt32(r["EncBy"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        ModifiedBy = Convert.ToInt32(r["ModifiedBy"]),
                        ModifiedDate = Convert.ToDateTime(r["ModifiedDate"]),
                    }
                );
            }

            return accounts;

        }

        #endregion Select

        #region Insert
        public void Insert(Account account)
        {
            //try
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "INSERT INTO tbl_Accounts (HolderID, HolderType, Status, StartDate, EndDate, EncBy, ModifiedBy) " +
                    "VALUES (@HolderID, @HolderType, @Status, @StartDate, @EndDate, @EncBy, @ModifiedBy)",
                    new Dictionary<string, Tuple<object, int?>> {
                        {"@HolderID", new Tuple<object, int?>(account.HolderID, 0)},
                        {"@HolderType", new Tuple<object, int?>(account.HolderType, 0)},
                        {"@Status", new Tuple<object, int?>(account.Status, 0) },
                        {"@StartDate", new Tuple<object, int?>(account.StartDate, 0) },
                        {"@EndDate", new Tuple<object, int?>(account.EndDate, 0) },
                    
                        {"@EncBy", new Tuple<object, int?>(account.HolderType, 0) },
                        { "@ModifiedBy", new Tuple<object, int?>(account.HolderType, 0) }
                    }
                );
            }
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }
        #endregion Insert

        #region Update
        public void Update(Account account)
        {
            try
            {
                int? defaultSize = null;
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "UPDATE tbl_Accounts SET " +
                    "HolderID = @HolderID, HolderType = @HolderType, Status = @Status, StartDate = @StartDate, EndDate = @EndDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate ",
                    new Dictionary<string, Tuple<object, int?>> {
                        {"@HolderID", new Tuple<object, int?> (account.HolderID, defaultSize ) },
                        {"@HolderType", new Tuple<object, int?>(account.HolderType, defaultSize ) },
                        {"@Status", new Tuple<object, int?>(account.Status, defaultSize) },
                        {"@StartDate", new Tuple<object, int?>(account.StartDate, defaultSize) },
                        {"@EndDate", new Tuple<object, int?>(account.ID, defaultSize) },
                        
                        { "@ModifiedBy", new Tuple<object, int?>(session.User.ID, defaultSize) },
                        { "@ModifiedDate", new Tuple<object, int?>(DateTime.Now, defaultSize) }
                    }
                );
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion Update

        #region Delete
        public void Delete(int? ID)
        {
            //try
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "DELETE FROM tbl_Accounts WHERE ID = @ID",
                    new Dictionary<string, object> { {"@ID", ID} }
                );
            }
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }
        #endregion Delet

        #region Details
        public Account Details(int? searchID = null)
        {
            DBObject dbObj = new DBObject();
            DataTable dt = dbObj.Query(
                "Contact",
                $"SELECT * from tbl_Accounts " + ((searchID == null) ? "" :
                $"WHERE ID = {searchID}"),
                new Dictionary<string, object>()
            );

            Account account = new Account();
            foreach (DataRow r in dt.Rows)
            {
                if (r != null)
                {
                    account = new Account()
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),
                        HolderID = r.IsNull("HolderID") ? default(int) : Convert.ToInt32(r["HolderID"]),
                        HolderType = r.IsNull("HolderType") ? default(int) : Convert.ToInt32(r["HolderType"]),
                        Status = r.IsNull("Status") ? default(int) : Convert.ToInt32(r["Status"]),
                        StartDate = r.IsNull("StartDate") ? default(DateTime) : Convert.ToDateTime(r["StartDate"]),
                        EndDate = r.IsNull("EndDate") ? default(DateTime) : Convert.ToDateTime(r["EndDate"]),

                        EncBy = Convert.ToInt32(r["EncBy"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        ModifiedBy = Convert.ToInt32(r["ModifiedBy"]),
                        ModifiedDate = Convert.ToDateTime(r["ModifiedDate"]),
                    };
                }
            }

            return account;

        }
        #endregion Details

    }
}