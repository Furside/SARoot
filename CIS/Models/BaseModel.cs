using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CIS._UtilClasses;
using PMSRedirect;

namespace CIS.Models
{
    public class BaseModel : DBObject
    {
        public UserSessions session = new UserSessions();
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Contact"].ConnectionString);

        #region Select Datatable
        virtual public DataTable SelectDataTable(string tblName, string conStringName = "Contact")
        {
            DBObject dbObj = new DBObject();
            DataTable dt = dbObj.Query(
                conStringName,
                $"SELECT * from {tblName}",
                new Dictionary<string, object>()
            );

            dbObj = null;

            return dt;
        }
        #endregion Select

        #region Delete
        public void Delete(int? ID, string tblName = "", string conStringName = "Contact")
        {
            {
                DBObject dbObj = new DBObject();
                DataTable dt = dbObj.Query(
                    conStringName,
                    $"DELETE FROM {tblName} WHERE ID = @ID",
                    new Dictionary<string, object> { { "@ID", ID } }
                );
            }
        }
        #endregion Delete

        #region Details
        public DataTable DetailsDataTable(int? searchID, string tblName, string conStringName = "Contact")
        {
            DBObject dbObj = new DBObject();
            DataTable dt = dbObj.Query(
                "Contact",
                $"SELECT * from tbl_Signatories " + ((searchID == null) ? "" :
                $"WHERE ID = {searchID}"),
                new Dictionary<string, object>()
            );

            dbObj = null;

            return dt;
        }

        public virtual TModel Details<TModel>(int? searchID = null) where TModel : class, new()
            => new TModel();

        #endregion Details Datatable

        public virtual void Insert<TModel>(TModel model){}

        #region Update
        public virtual void Update<TModel>(TModel model){}
        #endregion Update
    }
}