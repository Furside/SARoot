
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
using System.Web.Mvc;
//using NSDBObject;

namespace CIS.Models
{
    public class Contact
    {
        #region Fields
        UserSessions session = new UserSessions();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Contact"].ConnectionString);
        //-------------------------------------------------------------------------------
        public int? ID { get; set; }

        [Required]
        [Display(Name="Employee ID")]
        public int? EID { get; set; }

        [Display(Name = "Employee Name")]
        public tbl_user Obj_EIDHolder { get; set; } = new tbl_user();

        [Required]
        [Display(Name = "Mobile No.")]
        public string NetworkNo { get; set; } = null;

        [Required]
        [Display(Name = "Network Provider")]
        public int? NetworkTypeID { get; set; }

        [Display(Name = "Network Provider")]
        public string NetworkProvider { get; set; } = null;

        [Required]
        [Display(Name = "Bincard")]
        public string BinCard { get; set; } = null;

        public string Remarks { get; set; } = null;

        [Display(Name = "Encoded by")]
        public int? EncBy { get; set; }

        [Display(Name = "Encoded by")]
        public tbl_user Obj_EncBy { get; set; } = null;

        [Display(Name = "Encoded Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EncDate { get; set; }

        [Display(Name = "Last modified by")]
        public int? LastModifiedBy { get; set; }

        [Display(Name = "Last modified by")]
        public tbl_user Obj_LastModifiedBy { get; set; } = null;

        [Display(Name = "Last modified date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastModifiedDate { get; set; }

        public List<Contact> ContactList {
            get
            {
                DBObject dbObj = new DBObject();
                //string conStringSetting = ConfigurationManager.ConnectionStrings["Contact"].ConnectionString;
                DataTable dt = dbObj.Query(
                    "Contact",
                    @"SELECT * from tbl_Contact",
                    new Dictionary<string, object>()
                );

                List<Contact> contacts = new List<Contact>();
                var users = session.User.List();

                int id;
                tbl_user userData = new tbl_user();
                foreach (DataRow obj in dt.Rows)
                {
                    userData = users.Find(f => f.ID == Convert.ToInt32(obj["EID"]));
                    contacts.Add(new Contact() { ID = (int)obj["ID"], Obj_EIDHolder = users.Find(f => f.ID == Convert.ToInt32(obj["EID"])) ?? users.Find(f => f.ID == 1), EID = (int)obj["EID"], NetworkNo = obj["NetworkNo"].ToString(), NetworkTypeID = (int)obj["NetworkTypeID"], BinCard = obj["BinCard"].ToString(), Remarks = obj["Remarks"].ToString(), EncBy = ((int)obj["EncBy"]).Equals(default(int)) ? default(int) : (int)obj["EncBy"], Obj_EncBy = users.Find(f => f.ID == Convert.ToInt32(obj["EncBy"])) ?? users.Find(f => f.ID == 1), Obj_LastModifiedBy = users.Find(f => f.ID == Convert.ToInt32(obj["LastModifiedBy"])), LastModifiedBy = (int)obj["LastModifiedBy"], LastModifiedDate = Convert.ToDateTime(obj["LastModifiedDate"]), EncDate = Convert.ToDateTime(obj["EncDate"]) });
                }

                return contacts;
            }
            set
            { 
                ContactList = value;
            }
        }

        public Contact()
        {
            Obj_EIDHolder = new tbl_user();
        }

        public Contact InitializeData()
        {
            var item = new Contact();
            SqlCommand cm;
            //try
            {
                con.Open();
                cm = new SqlCommand("SELECT * FROM tbl_Contacts", con);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                var users = session.User.List();


                foreach (DataRow r in dt.Rows)
                {
                    item = new Contact
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),
                        EID = r.IsNull("EID") ? default(int) : Convert.ToInt32(r["EID"]),
                        NetworkNo = Convert.ToString(r["NetworkNo"]),
                        NetworkTypeID = r.IsNull("NetworkTypeID") ? default(int) : Convert.ToInt32(r["NetworkTypeID"]),
                        BinCard = Convert.ToString(r["BinCard"]),
                        Remarks = Convert.ToString(r["Remarks"]),
                        EncBy = r.IsNull("EncBy") ? default(int) : Convert.ToInt32(r["EncBy"]),
                        LastModifiedBy = r.IsNull("LastModifiedBy") ? default(int) : Convert.ToInt32(r["LastModifiedBy"]),
                        LastModifiedDate = Convert.ToDateTime(r["LastModifiedDate"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        Obj_EIDHolder = users.Find(f => f.ID == Convert.ToInt32(r["EID"])),
                        NetworkProvider = GetNetworkName(Convert.ToInt32(r["NetworkTypeID"]))
                    };
                }
                return item;
            }
        }

        string table = "tbl_Contacts";

        #endregion Fields

        public tbl_user UsersDB()
        {
            tbl_user users = new tbl_user();
            users = session.User;
            return users;
        }

        //list
        public List<Contact> Retrieve(string searchItem = "", int searchNetwork = default(int))
        {
            DBObject dbObj = new DBObject();
            //string conStringSetting = ConfigurationManager.ConnectionStrings["Contact"].ConnectionString;
            DataTable dt = dbObj.Query(
                "Contact",
                @"SELECT * from tbl_Contacts WHERE NetworkNo LIKE @searchItem", 
                new Dictionary<string, object> {
                    {"@searchItem", $"%{searchItem}%" }
                }
            );

            List<Contact> contacts = new List<Contact>();
            var users = session.User.List();

            foreach (DataRow obj in dt.Rows)
            {
                contacts.Add(new Contact() { ID = (int)obj["ID"], Obj_EIDHolder = users.Find(f => f.ID == Convert.ToInt32(obj["EID"])), EID = (int)obj["EID"], NetworkNo = obj["NetworkNo"].ToString(), NetworkTypeID = (int)obj["NetworkTypeID"], BinCard = obj["BinCard"].ToString(), Remarks = obj["Remarks"].ToString(), EncBy = ((int)obj["EncBy"]).Equals(default(int)) ? default(int) : (int)obj["EncBy"], Obj_EncBy = users.Find(f => f.ID == Convert.ToInt32(obj["EncBy"])), Obj_LastModifiedBy = users.Find(f => f.ID == Convert.ToInt32(obj["LastModifiedBy"])), LastModifiedBy = (int)obj["LastModifiedBy"], LastModifiedDate = Convert.ToDateTime(obj["LastModifiedDate"]), EncDate = Convert.ToDateTime(obj["EncDate"]) });
            }

            ContactList = contacts;

            return contacts;
        }

        public List<Contact> Retrieve(int? searchNetwork)
        {
            SqlCommand cm;
            SqlDataAdapter da;
            DataTable dt;
            var users = session.User.List();
            string cmString;
            
            cmString = $"SELECT * FROM {table} WHERE NetworkTypeID = @searchNetwork";

            try
            {
                con.Open();
                cm = new SqlCommand(cmString, con);

                cm.Parameters.AddWithValue("@searchNetwork", searchNetwork);

                da = new SqlDataAdapter(cm);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            List<Contact> contacts = new List<Contact>();

            foreach (DataRow obj in dt.Rows)
            {
                contacts.Add(new Contact() { ID = (int)obj["ID"], Obj_EIDHolder = users.Find(f => f.ID == Convert.ToInt32(obj["EID"])), EID = (int)obj["EID"], NetworkNo = obj["NetworkNo"].ToString(), NetworkTypeID = (int)obj["NetworkTypeID"], BinCard = obj["BinCard"].ToString(), Remarks = obj["Remarks"].ToString(), EncBy = ((int)obj["EncBy"]).Equals(default(int)) ? default(int) : (int)obj["EncBy"], Obj_EncBy = users.Find(f => f.ID == Convert.ToInt32(obj["EncBy"])), Obj_LastModifiedBy = users.Find(f => f.ID == Convert.ToInt32(obj["LastModifiedBy"])), LastModifiedBy = (int)obj["LastModifiedBy"], LastModifiedDate = Convert.ToDateTime(obj["LastModifiedDate"]), EncDate = Convert.ToDateTime(obj["EncDate"]) });
            }

            return contacts;
        }

        //adds a new contact
        public void Create(Contact contact)
        {
            try
            {
                con.Close();
                con.Open();
                SqlCommand cm = new SqlCommand("INSERT INTO tbl_Contacts (EID, NetworkNo, NetworkTypeID, BinCard, Remarks, EncBy, LastModifiedBy, LastModifiedDate) VALUES (@EID, @NetworkNo, @NetworkTypeID, @BinCard, @Remarks, @EncBy, @LastModifiedBy, @LastModifiedDate)", con);
                cm.Parameters.Add(new SqlParameter("@EID", contact.EID));
                cm.Parameters.Add(new SqlParameter("@NetworkNo", contact.NetworkNo));
                cm.Parameters.Add(new SqlParameter("@NetworkTypeID", contact.NetworkTypeID==default(int)?0:contact.NetworkTypeID));
                cm.Parameters.Add(new SqlParameter("@BinCard", string.IsNullOrWhiteSpace(contact.BinCard)?"": contact.BinCard));
                cm.Parameters.Add(new SqlParameter("@Remarks", string.IsNullOrWhiteSpace(contact.Remarks)?DBNull.Value.ToString():contact.Remarks));
                cm.Parameters.Add(new SqlParameter("@EncBy", session.User.ID));
                cm.Parameters.Add(new SqlParameter("@LastModifiedBy", session.User.ID));
                cm.Parameters.Add(new SqlParameter("@LastModifiedDate", DateTime.Now));
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

        public void Update(Contact contact)
        {
            //try
            {
                con.Open();
                SqlCommand cm = new SqlCommand("UPDATE tbl_Contacts SET EID=@EID, NetworkNo=@NetworkNo, NetworkTypeID=@NetworkTypeID, BinCard=@BinCard, Remarks=@Remarks, LastModifiedBy=@LastModifiedBy, LastModifiedDate=@LastModifiedDate WHERE ID=@ID", con);
                cm.Parameters.Add(new SqlParameter("@ID", contact.ID));
                cm.Parameters.Add(new SqlParameter("@EID", contact.EID));
                cm.Parameters.Add(new SqlParameter("@NetworkNo", contact.NetworkNo));
                cm.Parameters.Add(new SqlParameter("@NetworkTypeID", contact.NetworkTypeID));
                cm.Parameters.Add(new SqlParameter("@BinCard", contact.BinCard));
                cm.Parameters.Add(new SqlParameter("@Remarks", string.IsNullOrWhiteSpace(contact.Remarks) ? "" : contact.Remarks));
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

        //dropdown for list of employees
        public SelectList ListEmployee()
        {
            var list = new List<object>();
            session.User?.List().ForEach(r =>
            {
                if (!string.IsNullOrEmpty(r.Info?.Fullname))
                {
                    list.Add(new
                    {
                        ID = r.ID,
                        Fullname = r.Info?.Fullname
                    });
                }
            });
            var outputs = new SelectList(list, "ID", "Fullname");
            return outputs;
        }

        public List<Contact> List(string search = "")
        {
            var list = new List<Contact>();

            SqlCommand cm;

                try
                {
                    con.Open();
                    if (search == "")
                    {
                        cm = new SqlCommand("SELECT * FROM tbl_Contacts", con);
                    }
                    else
                    {
                        cm = new SqlCommand("SELECT * FROM tbl_Contacts WHERE EID=@EID", con);
                        cm.Parameters.Add(new SqlParameter("@EID", Convert.ToInt32(search)));
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    foreach (DataRow r in dt.Rows)
                    {
                        list.Add(new Contact
                        {

                            ID = Convert.ToInt32(r["ID"]),
                            EID = Convert.ToInt32(r["EID"]),
                            NetworkNo = Convert.ToString(r["NetworkNo"]),
                            NetworkTypeID = Convert.ToInt32(r["NetworkTypeID"]),
                            BinCard = Convert.ToString(r["BinCard"]),
                            Remarks = Convert.ToString(r["Remarks"]),
                            EncBy = r.IsNull("EncBy") ? default(int) : Convert.ToInt32(r["EncBy"]),
                            LastModifiedBy = Convert.ToInt32(r["LastModifiedBy"]),
                            LastModifiedDate = Convert.ToDateTime(r["LastModifiedDate"]),
                            EncDate = Convert.ToDateTime(r["EncDate"])
                        });
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

               return list;
        }

        //drop-down for list of networks
        public string GetNetworkName(int? ID) {
            NetworkType network = new NetworkType();
            return network.GetNetworkName(ID);
        }

        //retrieve a single set of details of a contact
        public Contact Find(int? iEID, int FID=0)
        {
            var item = new Contact();
            var users = session.User.List();
            SqlCommand cm;
            try
            {
                con.Open();
                if (FID == default(int))
                {
                    cm = new SqlCommand("SELECT * FROM tbl_Contacts WHERE EID=@EID", con);
                    cm.Parameters.Add(new SqlParameter("@EID", iEID));
                }
                else
                {
                    cm = new SqlCommand("SELECT * FROM tbl_Contacts WHERE ID=@FID", con);
                    cm.Parameters.Add(new SqlParameter("@FID", FID));
                }
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    UserSessions usersList = new UserSessions();
                    Contact contact = new Models.Contact() {
                        EID = session.User.ID,
                        NetworkNo = "",
                        NetworkTypeID = 0,
                        BinCard = "",
                        Remarks = "",
                    };

                    return contact;
                    //cm.Parameters.Add(new SqlParameter("@EID", contact.EID));
                    //cm.Parameters.Add(new SqlParameter("@NetworkNo", contact.NetworkNo));
                    //cm.Parameters.Add(new SqlParameter("@NetworkTypeID", contact.NetworkTypeID));
                    //cm.Parameters.Add(new SqlParameter("@BinCard", string.IsNullOrWhiteSpace(contact.BinCard) ? "" : contact.BinCard));
                    //cm.Parameters.Add(new SqlParameter("@Remarks", string.IsNullOrWhiteSpace(contact.Remarks) ? DBNull.Value.ToString() : contact.Remarks));
                    //cm.Parameters.Add(new SqlParameter("@EncBy", session.User.ID));
                    //cm.Parameters.Add(new SqlParameter("@LastModifiedBy", session.User.ID));
                    //cm.Parameters.Add(new SqlParameter("@LastModifiedDate", DateTime.Now));

                    //this.Create(contact);
                    //cm = new SqlCommand("SELECT * FROM tbl_Contacts WHERE EID=@EID", con);
                    //cm.Parameters.Add(new SqlParameter("@EID", iEID));
                    //da = new SqlDataAdapter(cm);
                    //dt = new DataTable();
                    //da.Fill(dt);
                }

                con.Close();


                foreach (DataRow r in dt.Rows)
                {
                    item = new Contact
                    {
                        ID = Convert.ToInt32(r["ID"]),
                        EID = Convert.ToInt32(r["EID"]),
                        NetworkNo = Convert.ToString(r["NetworkNo"]),
                        NetworkTypeID = Convert.ToInt32(r["NetworkTypeID"]),
                        BinCard = Convert.ToString(r["BinCard"]),
                        Remarks = Convert.ToString(r["Remarks"]),
                        EncBy = Convert.ToInt32(r["EncBy"]),
                        LastModifiedBy = Convert.ToInt32(r["LastModifiedBy"]),
                        LastModifiedDate = Convert.ToDateTime(r["LastModifiedDate"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        Obj_EIDHolder = users.Find(f => f.ID == Convert.ToInt32(r["EID"])),
                        NetworkProvider = GetNetworkName(Convert.ToInt32(r["NetworkTypeID"]))
                    };
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return item;
        }

        //hmmm
        public Contact Find(string ID = "", string FID = "")
        {
            var item = new Contact();
            SqlCommand cm;
            //try
            {
                con.Open();
                if (FID == "")
                {
                    cm = new SqlCommand("SELECT * FROM tbl_Contacts WHERE ID=@ID", con);
                    cm.Parameters.Add(new SqlParameter("@ID", ID == "" ? 1 : Convert.ToInt32(ID)));
                }
                else
                {
                    cm = new SqlCommand("SELECT * FROM tbl_Contacts WHERE ID=@FID", con);
                    cm.Parameters.Add(new SqlParameter("@FID", FID));
                }
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                var users = session.User.List();


                foreach (DataRow r in dt.Rows)
                {
                    item = new Contact
                    {
                        ID = r.IsNull("ID") ? default(int) : Convert.ToInt32(r["ID"]),
                        EID = r.IsNull("EID") ? default(int) : Convert.ToInt32(r["EID"]),
                        NetworkNo = Convert.ToString(r["NetworkNo"]),
                        NetworkTypeID = r.IsNull("NetworkTypeID") ? default(int) : Convert.ToInt32(r["NetworkTypeID"]),
                        BinCard = Convert.ToString(r["BinCard"]),
                        Remarks = Convert.ToString(r["Remarks"]),
                        EncBy = r.IsNull("EncBy") ? default(int) : Convert.ToInt32(r["EncBy"]),
                        LastModifiedBy = r.IsNull("LastModifiedBy") ? default(int) : Convert.ToInt32(r["LastModifiedBy"]),
                        LastModifiedDate = Convert.ToDateTime(r["LastModifiedDate"]),
                        EncDate = Convert.ToDateTime(r["EncDate"]),
                        NetworkProvider = GetNetworkName(Convert.ToInt32(r["NetworkTypeID"]))
                    };

                    item.Obj_EIDHolder = users.Find(f => f.ID == Convert.ToInt32(r["EID"])) ?? users.Find(f => f.ID == (1));
                }
            }
            //catch (Exception e)
            //{
            //    throw e;
            //}

            return (item.EID != 0) ? item : item.ContactList.ElementAt(0) ;
        }

        public void Delete(int ID)
        {
            try
            {
                con.Open();
                SqlCommand cm = new SqlCommand("DELETE FROM tbl_Contacts WHERE ID=@ID", con);
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

        public void Delete(Contact contact)
        {
            try
            {
                con.Open();
                SqlCommand cm = new SqlCommand("DELETE FROM tbl_Contacts WHERE ID=@ID", con);
                cm.Parameters.Add(new SqlParameter("@ID", contact.ID));
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetEncoderFullName(int? ID)
        {
            string userFullname = "";
            var users = session.User.List();

            userFullname = users.Find(f => f.ID == ID).Info.Fullname;

            return userFullname;
        }
    }
}
