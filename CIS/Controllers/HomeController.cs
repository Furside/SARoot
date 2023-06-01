using PMSRedirect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CIS.Models;

namespace CIS.Controllers
{
    public class HomeController : Controller
    {
        //UserSessions session = new UserSessions(@"SERVER=LAPTOP-HAK1P0TS;DATABASE=dbPMS;INTEGRATED SECURITY=TRUE");
        UserSessions session = new UserSessions(@"SERVER=192.168.0.101\sqlExpress;DATABASE=dbPMS;USER=SA;PWD=1234");

        [HttpGet]
        public ActionResult Index()
        {
            Contact contact = new Contact();
            InitializeAdmin();
            return View(contact);
            //return RedirectToAction("Search", "Contact", "");
        }

        public void InitializeAdmin()
        {
            session.InitializeAdmin(576);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region test region
        private void FuncTest()
        {
            UserSessions session;
                #region session members
                session = new UserSessions(@"SERVER=192.168.0.101\sqlExpress;DATABASE=dbPMS;USER=SA;PWD=1234");
                session = new UserSessions();
                session.Initialize();
                session.Initialize(PMSRedirect.Company.General); // General | ISOTech | MayoFoods | MHCI (PMS)| Sets
                session.DestroySession();
                bool samp = session.HasSession;
                session.InitializeAdmin();
                session.InitializeAdmin(576, 7);
                int? sessionlevel = session.Level; 

                tbl_user user = session.User;
                    #region user members
                    string userName = user.User;
                    string phone = user.Phone;
                    string pass = user.Pass;
                    List<tbl_user> usersList = user.List();
                    bool? isAdmin = user.isAdmin;
                    bool? isActive = user.isActive;
                    int ID = user.ID;
                    user.Find(ID);
                    DateTime? encDate = user.encDate;
                    string email = user.Email;

                    tbl_userInfo userInfo = user.Info;
                        #region userInfo members
            
                        string autoNo = userInfo.AutoNo;
                        DateTime? bday = userInfo.bday;
                        string company = userInfo.Company;
                        DateTime? dateHired = userInfo.DateHired;
                        string userInfodepartment = userInfo.department;
                        string userInfoemploymentStatus = userInfo.employmentstatus;
                        string userInfofname = userInfo.fname;
                        string userInfofullname = userInfo.Fullname;
                        string userInfogender = userInfo.gender;
                        int userInfoID = userInfo.ID;
                        string userInfoIDNo = userInfo.IDNo;
                        List<tbl_userInfo> userInfoList = userInfo.List();
                        userInfoList = userInfo.List("");
                        string userInfoLname = userInfo.lname;
                        string userInfoLocation = userInfo.Location;
                        string userInfoMn = userInfo.mn;
                        string userInfoPosition = userInfo.position;
                        int userInfoUserID = userInfo.userID;
                        #endregion
                    #endregion

                #endregion session
        }
        #endregion test region
    }
}
