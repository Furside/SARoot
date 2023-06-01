using CIS._UtilClasses;
using PMSRedirect;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CIS.Models
{
    public class NetworkPlan
    {
        #region Fields
        [Required]
        public int? ID { get; set; }

        [Display(Name = "Network ID")]
        public int? NetworkID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; } = null;

        [Display(Name = "Combo")]
        public string Combo { get; set; } = null;

        [Display(Name = "Booster")]
        public string Booster { get; set; } = null;

        [Display(Name = "Duration")]
        public DateTime? Duration { get; set; }

        [Display(Name = "Credit Limit")]
        public int? CreditLimit { get; set; }

        [Display(Name = "SpendingLimit")]
        public int? SpendingLimit { get; set; }

        [Display(Name = "Billing Cycle")]
        public string BillingCycle { get; set; } = null;

        [Display(Name = "Encoded By")]
        public int? EncBy { get; set; }

        [Display(Name = "Date Encoded")]
        public DateTime? EncDate { get; set; }

        [Display(Name = "Modified By")]
        public int? ModifiedBy { get; set; }
        #endregion Fields

        UserSessions session = new UserSessions();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Contact"].ConnectionString);

        #region Select
        public List<NetworkPlan> Select()
        {
            DBObject dbObj = new DBObject();
            DataTable dt = dbObj.Query(
                "Contact",
                "SELECT * from tbl_NetworkPlans",
                new Dictionary<string, object>()
            );

            List<NetworkPlan> networkPlans = new List<NetworkPlan>();

            foreach (DataRow r in dt.Rows)
            {
                networkPlans.Add(
                    new NetworkPlan()
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),

                        EncBy = Convert.ToInt32(r["EncBy"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        ModifiedBy = Convert.ToInt32(r["ModifiedBy"]),
                    }
                );
            }

            return networkPlans;

        }

        #endregion Select

        #region Insert
        public void Insert(NetworkPlan networkPlan)
        {
            //try
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "INSERT INTO tbl_NetworkPlans (HolderID, HolderType, Status, StartDate, EndDate, EncBy, ModifiedBy) " +
                    "VALUES (@HolderID, @HolderType, @Status, @StartDate, @EndDate, @EncBy, @ModifiedBy)",
                    new Dictionary<string, object> {
                        {"@EndDate", networkPlan.ID },

                        {"@EncBy", session.User.ID },
                        { "@ModifiedBy", session.User.ID }
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
        public void Update(NetworkPlan networkPlan)
        {
            try
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "UPDATE tbl_NetworkPlans SET " +
                    "HolderID = @HolderID, HolderType = @HolderType, Status = @Status, StartDate = @StartDate, EndDate = @EndDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate ",
                    new Dictionary<string, object> {
                        {"@EndDate", networkPlan.ID },

                        { "@ModifiedBy", session.User.ID },
                        { "@ModifiedDate", DateTime.Now }
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
                    "DELETE FROM tbl_NetworkPlans WHERE ID = @ID",
                    new Dictionary<string, object> { { "@ID", ID } }
                );
            }
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }
        #endregion Delet

        #region Details
        public NetworkPlan Details(int? searchID = null)
        {
            DBObject dbObj = new DBObject();
            DataTable dt = dbObj.Query(
                "Contact",
                $"SELECT * from tbl_NetworkPlans " + ((searchID == null) ? "" :
                $"WHERE ID = {searchID}"),
                new Dictionary<string, object>()
            );

            NetworkPlan networkPlan = new NetworkPlan();
            foreach (DataRow r in dt.Rows)
            {
                if (r != null)
                {
                    networkPlan = new NetworkPlan()
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),

                        EncBy = Convert.ToInt32(r["EncBy"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        ModifiedBy = Convert.ToInt32(r["ModifiedBy"]),
                    };
                }
            }

            return networkPlan;

        }
        #endregion Details
    }
}