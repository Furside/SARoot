using PMSRedirect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Models
{
    public class NetworkType
    {
        UserSessions session = new UserSessions();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Contact"].ConnectionString);
        //-------------------------------------------------------------------------------
        [Display(Name = "Network Type ID")]
        public int? ID { get; set; }

        [Required]
        [Display(Name="Network Name")]
        public string Name { get; set; }

        [Display(Name="Encoder ID")]
        public int EncBy { get; set; }

        [Display(Name="Encoded Date")]
        [DisplayFormat(DataFormatString = "{f}")]
        public DateTime? EncDate { get; set; }

        [Display(Name="Last Modifier ID")]
        public int LastModifiedBy { get; set; }

        [Display(Name="Last Modified Date")]
        [DisplayFormat(DataFormatString = "f")]
        public DateTime? LastModifiedDate { get; set; }

        [Display(Name = "Network Name")]
        public string NetworkName { get; set; }

        public NetworkType()
        {

        }

        public void Create(NetworkType networkType)
        {
            try
            {
                con.Open();
                SqlCommand cm = new SqlCommand("INSERT INTO tbl_NetworkProvider ([Name], [EncBy], [LastModifiedBy]) VALUES (@Name,@EncBy,@LastModifiedBy);", con);
                cm.Parameters.Add(new SqlParameter("@Name", networkType.Name));
                cm.Parameters.Add(new SqlParameter("@EncBy", session.User.ID));
                cm.Parameters.Add(new SqlParameter("@LastModifiedBy", session.User.ID));
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(NetworkType networkType)
        {
            //try
            {
                con.Open();
                SqlCommand cm = new SqlCommand("UPDATE tbl_NetworkProvider SET [Name]=@Name, [LastModifiedBy]=@LastModifiedBy, [LastModifiedDate]=@LastModifiedDate WHERE [ID]=@ID ", con);
                cm.Parameters.Add(new SqlParameter("@ID", networkType.ID));
                cm.Parameters.Add(new SqlParameter("@Name", networkType.Name));
                cm.Parameters.Add(new SqlParameter("@LastModifiedBy", session.User.ID));
                cm.Parameters.Add(new SqlParameter("@LastModifiedDate", DateTime.Now));
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
            }
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

        public List<NetworkType> List(string search = "")
        {
            var list = new List<NetworkType>();

            try
            {
                con.Open();
                SqlCommand cm = new SqlCommand("SELECT * FROM tbl_NetworkProvider WHERE Name LIKE @search;", con);
                cm.Parameters.Add(new SqlParameter("@search", $"%{search}%"));
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                
                foreach (DataRow r in dt.Rows)
                {
                    list.Add(new NetworkType
                    {
                        ID = Convert.ToInt32(r["ID"]),
                        Name = Convert.ToString(r["Name"]),
                        EncBy = Convert.ToInt32(r["EncBy"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        LastModifiedBy = r["LastModifiedBy"].Equals(DBNull.Value) ? default(int) : Convert.ToInt32( r["LastModifiedBy"] ),
                        LastModifiedDate = r["LastModifiedDate"].Equals(DBNull.Value) ? default(DateTime) : Convert.ToDateTime( r["LastModifiedDate"] )
                    });
                }
                con.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public SelectList NetworkTypes()
        {
            {
                var list = new List<NetworkType>();
                NetworkType networks = new NetworkType();

                list = networks.List();

                //session.User?.List().ForEach(r =>
                //{
                //    if (!string.IsNullOrEmpty(r.Info?.Fullname))
                //    {
                //        list.Add(new
                //        {
                //            ID = r.ID,
                //            Fullname = r.Info?.Fullname
                //        });
                //    }
                //});
                var outputs = new SelectList(list, "ID", "Name");
                return outputs;
            }
            
        }

        public NetworkType Find(int ID)
        {
            var item = new NetworkType();

            try
            {
                con.Open();
                SqlCommand cm = new SqlCommand("SELECT * FROM tbl_NetworkProvider WHERE ID=@ID;", con);
                cm.Parameters.Add(new SqlParameter("@ID", ID));
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                foreach (DataRow r in dt.Rows)
                {
                    item = new NetworkType
                    {
                        ID = Convert.ToInt32(r["ID"]),
                        Name = Convert.ToString(r["Name"]),
                        EncBy = Convert.ToInt32(r["EncBy"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        LastModifiedBy = Convert.ToInt32(r["EncBy"]),
                        LastModifiedDate = Convert.ToDateTime(r["EncDate"])
                    };
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return item;
        }

        public string GetNetworkName(int? ID)
        {
            NetworkType item = new NetworkType();
            try
            {
                con.Open();
                SqlCommand cm;
                string query = "SELECT * FROM tbl_NetworkProvider";
                if (ID!= null)
                {
                    query += " WHERE ID = @ID";
                }
                cm = new SqlCommand(query, con);
                if(ID!=null) cm.Parameters.Add(new SqlParameter("@ID", ID));
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (ID != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        item = new NetworkType
                        {
                            ID = Convert.ToInt32(r["ID"]),
                            Name = Convert.ToString(r["Name"]),
                            EncBy = Convert.ToInt32(r["EncBy"]),
                            EncDate = Convert.ToDateTime(r["EncDate"]),
                            LastModifiedBy = Convert.ToInt32(r["EncBy"]),
                            LastModifiedDate = Convert.ToDateTime(r["EncDate"])
                        };
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return item.Name==default(string)?"": item.Name;
        }

        public string GetEncoderFullName(int? ID)
        {
            string userFullname = "";
            var users = session.User.List();
            
            userFullname = users.Find(f => f.ID == ID).Info.Fullname ;

            return userFullname;
        }

        public void Delete(int? ID)
        {
            try
            {
                con.Open();
                SqlCommand cm = new SqlCommand("DELETE FROM tbl_NetworkProvider WHERE ID=@ID", con);
                cm.Parameters.Add(new SqlParameter("@ID", ID));
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Contact> ListContacts(int ID=default(int))
        {
            Contact contact = new Contact();
            return contact.Retrieve("", ID);
        }

    }
}