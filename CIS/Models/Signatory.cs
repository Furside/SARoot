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
    public class Signatory : BaseModel
    {
        #region Fields
        [Display(Name = "Employee ID")]
        public int? EID { get; set; }
        #endregion Fields

        public Signatory SelectedItem { get; set;  }
        public List<Signatory> List { get; set; }
        public tbl_user Employee { get; set; }

        public Signatory() { }

        #region Select
        public List<Signatory> Select()
        {
            List = new List<Signatory>();

            foreach (DataRow r in SelectDataTable("tbl_Signatory").Rows)
            {
                List.Add(
                    new Signatory()
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),
                        EID = r.IsNull("EID") ? default(int) : Convert.ToInt32(r["EID"]),
                        Employee = session.User.List().Find(f=>f.ID == Convert.ToInt32(r["EID"])),
                        EncBy = r.IsNull("EncBy") ? default(int) : Convert.ToInt32(r["EncBy"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        ModifiedBy = r.IsNull("ModifiedBy") ? default(int) : Convert.ToInt32(r["ModifiedBy"]),
                    }
                );
            }
            return List;
        }

        #endregion Select

        #region Insert
        public void Insert(Signatory signatory)
        {
            //try
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "INSERT INTO tbl_Signatory (EID, EncBy, ModifiedBy) " +
                    "VALUES (@EID, @EncBy, @ModifiedBy)",
                    new Dictionary<string, object> {
                        {"@EndDate", signatory.ID },

                        {"@EncBy", session.User.ID },
                        { "@ModifiedBy", session.User.ID }
                    }
                );

                dbObj = null;
            }
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }
        #endregion Insert

        #region Update
        public void Update(Signatory signatory)
        {
            try
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "UPDATE tbl_Signatory SET " +
                    "EndDate = @EndDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate ",
                    new Dictionary<string, object> {
                        {"@EndDate", signatory.ID },
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
            Delete(ID, "tbl_Signatory");
        }
        #endregion Delete

        #region Details
        public override TModel Details<TModel>(int? searchID = null)
        {
            foreach (DataRow r in SelectDataTable("tbl_Signatory").Rows)
            {
                if (r != null)
                {
                    SelectedItem = new Signatory()
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),
                        EID = r.IsNull("EID") ? default(int) : Convert.ToInt32(r["EID"]),
                        Employee = session.User.List().Find(f => f.ID == Convert.ToInt32(r["EID"])) ,
                        EncBy = r.IsNull("EncBy") ? default(int) : Convert.ToInt32(r["EncBy"]),
                        EncDate = r.IsNull("EncDate") ? default(DateTime) : Convert.ToDateTime(r["EncDate"]),
                        ModifiedBy = r.IsNull("ModifiedBy") ? default(int) : Convert.ToInt32(r["ModifiedBy"]),
                        ModifiedDate = r.IsNull("ModifiedDate") ? default(DateTime) : Convert.ToDateTime(r["ModifiedDate"]),
                    };
                    return SelectedItem as TModel;
                }
            }

            return new TModel();

        }
        #endregion Details
    }
}