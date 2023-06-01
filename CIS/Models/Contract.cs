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
    public class Contract
    {
        #region Fields
        [Required]
        public int? ID { get; set; }

        [Display(Name = "Contract No.")]
        public string ContractNo { get; set; } = null;

        [Display(Name = "Holder ID")]
        public int? HolderID { get; set; }

        [Display(Name = "Holder Type")]
        public int? HolderType { get; set; }

        public string Status { get; set; } = null;

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Description")]
        public string Contract_Description { get; set; } = null;

        [Display(Name = "Status")]
        public string Contract_Status { get; set; } = null;

        [Display(Name = "Offer")]
        public string Contract_Offer { get; set; } = null;

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
        public List<Contract> Select()
        {
            DBObject dbObj = new DBObject();
            DataTable dt = dbObj.Query(
                "Contact",
                "SELECT * from tbl_Contracts",
                new Dictionary<string, object>()
            );

            List<Contract> contracts = new List<Contract>();

            foreach (DataRow r in dt.Rows)
            {
                contracts.Add(
                    new Contract()
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),
                        HolderID = r.IsNull("HolderID") ? default(int) : Convert.ToInt32(r["HolderID"]),
                        HolderType = r.IsNull("HolderType") ? default(int) : Convert.ToInt32(r["HolderType"]),
                        Status = r.IsNull("Status") ? default(string) : Convert.ToString(r["Status"]),
                        StartDate = r.IsNull("StartDate") ? default(DateTime) : Convert.ToDateTime(r["StartDate"]),
                        EndDate = r.IsNull("EndDate") ? default(DateTime) : Convert.ToDateTime(r["EndDate"]),

                        EncBy = r.IsNull("EncBy") ? default(int) : Convert.ToInt32(r["EncBy"]),
                        EncDate = r.IsNull("EncDate") ? default(DateTime) : Convert.ToDateTime(r["EncDate"]),
                        ModifiedBy = r.IsNull("ModifiedBy") ? default(int) : Convert.ToInt32(r["ModifiedBy"]),
                        ModifiedDate = r.IsNull("ModifiedDate") ? default(DateTime) : Convert.ToDateTime(r["ModifiedDate"]),
                    }
                );
            }

            return contracts;

        }

        #endregion Select

        #region Insert
        public void Insert(Contract contract)
        {
            //try
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "INSERT INTO tbl_Contracts (HolderID, HolderType, Status, StartDate, EndDate, EncBy, ModifiedBy) " +
                    "VALUES (@HolderID, @HolderType, @Status, @StartDate, @EndDate, @EncBy, @ModifiedBy)",
                    new Dictionary<string, object> {
                        {"@HolderID", contract.HolderID},
                        {"@HolderType", contract.HolderType },
                        {"@Status", contract.Status },
                        {"@StartDate", contract.StartDate },
                        {"@EndDate", contract.ID },

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
        public void Update(Contract contract)
        {
            try
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    "Contact",
                    "UPDATE tbl_Contracts SET " +
                    "HolderID = @HolderID, HolderType = @HolderType, Status = @Status, StartDate = @StartDate, EndDate = @EndDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate ",
                    new Dictionary<string, object> {
                        {"@HolderID", contract.HolderID},
                        {"@HolderType", contract.HolderType },
                        {"@Status", contract.Status },
                        {"@StartDate", contract.StartDate },
                        {"@EndDate", contract.ID },

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
                    "DELETE FROM tbl_Contracts WHERE ID = @ID",
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
        public Contract Details(int? searchID = null)
        {
            DBObject dbObj = new DBObject();
            DataTable dt = dbObj.Query(
                "Contact",
                $"SELECT * from tbl_Contracts " + ((searchID == null) ? "" :
                $"WHERE ID = {searchID}"),
                new Dictionary<string, object>()
            );

            Contract contract = new Contract();
            foreach (DataRow r in dt.Rows)
            {
                if (r != null)
                {
                    contract = new Contract()
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),
                        HolderID = r.IsNull("HolderID") ? default(int) : Convert.ToInt32(r["HolderID"]),
                        HolderType = r.IsNull("HolderType") ? default(int) : Convert.ToInt32(r["HolderType"]),
                        Status = r.IsNull("Status") ? default(string) : Convert.ToString(r["Status"]),
                        StartDate = r.IsNull("StartDate") ? default(DateTime) : Convert.ToDateTime(r["StartDate"]),
                        EndDate = r.IsNull("EndDate") ? default(DateTime) : Convert.ToDateTime(r["EndDate"]),

                        EncBy = Convert.ToInt32(r["EncBy"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        ModifiedBy = Convert.ToInt32(r["ModifiedBy"]),
                        ModifiedDate = Convert.ToDateTime(r["ModifiedDate"]),
                    };
                }
            }

            return contract;

        }
        #endregion Details
    }
}