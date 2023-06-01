using Microsoft.Reporting.WebForms;
using MobilePlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePlan.Controllers
{
    public class NetworkTypeController : Controller
    {
        // GET: NetworkType
        NetworkType mod = new NetworkType();
        public ActionResult Index(string Search = "")
        {
            var list = mod.List(Search);
            return View(list);
        }

        public ActionResult Action(string Type, int? ID = null)
        {
            switch (Type)
            {
                case "Detail":
                    return RedirectToAction("Detail", new { ID = ID });
                case "Add":
                    return RedirectToAction("Create");
                case "Edit":
                    return RedirectToAction("Edit", new { ID = ID });
                case "Delete":
                    return RedirectToAction("Delete", new { ID = ID });
                case "ItemView":
                    return RedirectToAction("ItemView", new { ID = ID });
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NetworkType m)
        {
            if (ModelState.IsValid)
            {
                if (mod.Create(m) != 0)
                {
                    ViewBag.Message = "Data successfully created!";
                }
                else
                {
                    ModelState.AddModelError("", "Data has duplicate value!");
                }
            }

            ViewBag.Execute = "ReloadPage()";
            return PartialView("Field", m);
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var item = mod.Find(ID);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(NetworkType m)
        {
            if (ModelState.IsValid)
            {
                mod.Update(m);
                ViewBag.Message = "Data successfully saved!";
                ViewBag.Execute = "ReloadPage()";
            }
            return PartialView("Field", m);
        }
        
        public ActionResult Detail(int ID)
        {
            var item = mod.Find(ID);
            return View(item);
        }
        
        public ActionResult Delete(int ID)
        {
            var item = mod.Find(ID);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(NetworkType m)
        {
            if (ModelState.IsValid)
            {
                mod.Delete(m);
                ViewBag.Message = "Data successfully saved!";
                ViewBag.Execute = "ReloadPage()";
            }
            return PartialView("Field", m);
        }


        [HttpGet]
        public ActionResult ItemView(int ID)
        {
            var item = mod.Find(ID);
            return PartialView(item);
        }

        [HttpGet]
        public ActionResult Report(int ID)
        {
            //var item = mod.Find(ID);
            return PartialView();
        }

        public ActionResult Print(DateTime? StartDate, DateTime? EndDate, int ID = 0, int Status = 0, Format Format = Format.PDF)
        {
            //if (StartDate == null)
            //{
            //    StartDate = DateTime.Parse("1753-01-01 00:00:00.000");
            //}
            //if (EndDate == null)
            //{
            //    EndDate = DateTime.Parse("1753-01-01 00:00:00.000");
            //}
            ReportViewer rv = new ReportViewer(); //EXCELOPENXML FOR EXCEL & application/vnd.ms-excel FOR contentType
            rv.LocalReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");
            rv.LocalReport.Refresh();

            var item = new Contact().List(StartDate, EndDate, ID, ( (Status==0) ? "All" : ((Status==1) ? "Active" : "Terminated")) );
            rv.LocalReport.DataSources.Add(new ReportDataSource("ds_Network", item));
            //Microsoft.Reporting.WebForms.ReportViewer.LocalReport
            //rv.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", new NetworkType().List()));

            var file = rv.LocalReport.Render(Format == Format.PDF ? "pdf" : "EXCELOPENXML");
            
            Response.ContentType = Format == Format.PDF ? "application/pdf" : "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", Format == Format.PDF ? $"inline;filename=ITSD {DateTime.Now:yyyyMMddhhmmss}.pdf" : $"inline;filename=ITSD {DateTime.Now:yyyyMMddhhmmss}.xlsx");
            Response.Buffer = true;
            Response.Clear();
            Response.BinaryWrite(file);
            Response.End();
            return View(file);
        }

        public enum Format
        {
            PDF,
            Excel
        }
    }
}