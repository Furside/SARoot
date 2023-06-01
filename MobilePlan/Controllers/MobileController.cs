using MobilePlan.Models;
using PMSRedirect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePlan.Controllers
{
    public class MobileController : Controller
    {
        // GET: Mobile
        Contact mod = new Contact();
        public ActionResult Index(string Search = "")
        {
            var list = mod.List(Search);
            return View(list);
        }

        public ActionResult Action(string Type, int? ID = null)
        {
            switch (Type)
            {
                case "Add":
                    return RedirectToAction("Create");
                case "Edit":
                    return RedirectToAction("Edit", new { ID = ID });
                case "Detail":
                    return RedirectToAction("Detail", new { ID = ID });
                case "Delete":
                    return RedirectToAction("Delete", new { ID = ID });
                case "Index":
                    return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Create()
        {
            return View(new Contact());
        }

        [HttpPost]
        public ActionResult Create(Contact m)
        {
            if (ModelState.IsValid)
            {
                mod.Create(m);
                ViewBag.Message = "Data successfully created!";
                ViewBag.Execute = "ReloadPage()";
            }
            return PartialView("Field", m);
        }

        public ActionResult Edit(int ID)
        {
            var item = mod.Find(ID);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Contact m)
        {
            if (ModelState.IsValid)
            {
                mod.Update(m);
                ViewBag.Message = "Data successfully Save!";
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
        public ActionResult Delete(Contact m)
        {
            if (ModelState.IsValid)
            {
                m.Delete(m);
                ViewBag.Message = "Data successfully deleted!";
                ViewBag.Execute = "ReloadPage()";
            }
            return PartialView("Field", m);
        }

        
    }
}