using Microsoft.Reporting.WebForms;
using PMSRedirect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePlan.Controllers
{
    public class HomeController : Controller
    {
        UserSessions session = new UserSessions();
        public string user { get; set; }
        public ActionResult Index()
        {
            InitAdmin();
            user = session.User.User;
            return RedirectToAction("Index", "Mobile");
        }

        public void InitAdmin()
        {
            //session = new UserSessions(@"SERVER=WIN-IIS\SQLEXPRESS;DATABASE=DBPMS;USER=SA;PWD=1234");
            //session = new UserSessions(@"SERVER=122.54.131.132;DATABASE=DBPMS;USER=SA;PWD=1234");
            session = new UserSessions(@"SERVER=192.168.0.101\sqlexpress;DATABASE=DBPMS;USER=SA;PWD=1234");
            session.InitializeAdmin(125);
        }

        public ActionResult Redirect()
        {
            session.Initialize(Company.General);
            return RedirectToAction("Index");
        }

        //public ActionResult Print(int ID)
        //{
        //    ReportViewer rv = new ReportViewer(); //EXCELOPENXML FOR EXCEL & application/vnd.ms-excel FOR contentType
        //    rv.LocalReport.ReportPath = Server.MapPath("~/Reports/Report.rdlc");
        //    var data = new object { };
        //    rv.LocalReport.DataSources.Add(new ReportDataSource("Report", data));
        //    var file = rv.LocalReport.Render("pdf");
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", $"inline;filename=Form.pdf");
        //    Response.Buffer = true;
        //    Response.Clear();
        //    Response.BinaryWrite(file);
        //    Response.End();
        //    return View(file);
        //}
    }
}