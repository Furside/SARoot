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
    public class Handset
    {
        #region Fields
        [Required]
        public int? ID { get; set; }

        [Display(Name = "Holder ID")]
        public int? HolderID { get; set; }

        [Display(Name = "Holder Type")]
        public int? HolderType { get; set; }

        public string Model { get; set; } = null;

        [Display(Name = "Duo No")]
        public string DuoNo { get; set; } = null;

        [Display(Name = "Encoded By")]
        public int? EncBy { get; set; }

        [Display(Name = "Date Encoded")]
        public DateTime? EncDate { get; set; }

        [Display(Name = "Modified By")]
        public int? ModifiedBy { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime? ModifiedDate { get; set; }
        #endregion Fields

        UserSessions session = new UserSessions();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Contact"].ConnectionString);

        #region Select
        public List<Handset> Select()
        {
            DBObject dbObj = new DBObject();
            DataTable dt = dbObj.Query(
                "Contact",
                "SELECT * from tbl_Handsets",
                new Dictionary<string, object>()
            );

            List<Handset> handsets = new List<Handset>();

            foreach (DataRow r in dt.Rows)
            {
                handsets.Add(
                    new Handset()
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),
                        HolderID = r.IsNull("HolderID") ? default(int) : Convert.ToInt32(r["HolderID"]),
                        HolderType = r.IsNull("HolderType") ? default(int) : Convert.ToInt32(r["HolderType"]),

                        EncBy = Convert.ToInt32(r["EncBy"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        ModifiedBy = Convert.ToInt32(r["ModifiedBy"]),
                        ModifiedDate = Convert.ToDateTime(r["ModifiedDate"]),
                    }
                );
            }

            return handsets;

        }

        #endregion Select

        #region Insert
        public void Insert(Handset handset)
        {
            //try
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "INSERT INTO tbl_Handsets (HolderID, HolderType, Status, StartDate, EndDate, EncBy, ModifiedBy) " +
                    "VALUES (@HolderID, @HolderType, @Status, @StartDate, @EndDate, @EncBy, @ModifiedBy)",
                    new Dictionary<string, object> {
                        {"@HolderID", handset.HolderID},
                        {"@HolderType", handset.HolderType },
                        {"@EndDate", handset.ID },

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
        public void Update(Handset handset)
        {
            try
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "UPDATE tbl_Handsets SET " +
                    "HolderID = @HolderID, HolderType = @HolderType, Status = @Status, StartDate = @StartDate, EndDate = @EndDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate ",
                    new Dictionary<string, object> {
                        {"@HolderID", handset.HolderID},
                        {"@HolderType", handset.HolderType },
                        {"@EndDate", handset.ID },

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
                    "DELETE FROM tbl_Handsets WHERE ID = @ID",
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
        public Handset Details(int? searchID = null)
        {
            DBObject dbObj = new DBObject();
            DataTable dt = dbObj.Query(
                "Contact",
                $"SELECT * from tbl_Handsets " + ((searchID == null) ? "" :
                $"WHERE ID = {searchID}"),
                new Dictionary<string, object>()
            );

            Handset handset = new Handset();
            foreach (DataRow r in dt.Rows)
            {
                if (r != null)
                {
                    handset = new Handset()
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),
                        HolderID = r.IsNull("HolderID") ? default(int) : Convert.ToInt32(r["HolderID"]),
                        HolderType = r.IsNull("HolderType") ? default(int) : Convert.ToInt32(r["HolderType"]),

                        EncBy = Convert.ToInt32(r["EncBy"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        ModifiedBy = Convert.ToInt32(r["ModifiedBy"]),
                        ModifiedDate = Convert.ToDateTime(r["ModifiedDate"]),
                    };
                }
            }

            return handset;

        }
        #endregion Details
    }
}