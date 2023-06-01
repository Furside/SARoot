using CIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Controllers
{
    public class HandsetController : Controller
    {
        #region Select
        [HttpGet]
        public ActionResult Select()
        {
            Handset handset = new Handset();
            return PartialView("Select", handset.Select());
        }
        #endregion Select

        #region Insert
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Handset handset)
        {
            handset.Insert(handset);
            return RedirectToAction("Select");
        }
        #endregion Insert

        #region Update
        [HttpGet]
        public ActionResult Update(int ID)
        {
            Handset handset = new Handset();
            return PartialView("Update", handset.Details(ID));
        }

        [HttpPost]
        public ActionResult Update(Handset handset)
        {
            handset.Update(handset);
            return RedirectToAction("Select");
        }
        #endregion Update

        #region Delete
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Handset handset = new Handset();
            return PartialView("Delete", handset.Details(ID));
        }

        [HttpPost]
        public ActionResult Delete(Handset handset)
        {
            handset.Delete(handset.ID);
            return RedirectToAction("Select");
        }
        #endregion Delete

        #region Details
        [HttpGet]
        public ActionResult Details(int ID)
        {
            Handset handset = new Handset();
            return PartialView(handset.Details(ID));
        }
        #endregion Details
    }
}