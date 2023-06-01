using CIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Controllers
{
    public class NetworkPlanController : Controller
    {
        #region Select
        [HttpGet]
        public ActionResult Select()
        {
            NetworkPlan networkPlan = new NetworkPlan();
            return PartialView("Select", networkPlan.Select());
        }
        #endregion Select

        #region Insert
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(NetworkPlan networkPlan)
        {
            networkPlan.Insert(networkPlan);
            return RedirectToAction("Select");
        }
        #endregion Insert

        #region Update
        [HttpGet]
        public ActionResult Update(int ID)
        {
            NetworkPlan networkPlan = new NetworkPlan();
            return PartialView("Update", networkPlan.Details(ID));
        }

        [HttpPost]
        public ActionResult Update(NetworkPlan networkPlan)
        {
            networkPlan.Update(networkPlan);
            return RedirectToAction("Select");
        }
        #endregion Update

        #region Delete
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            NetworkPlan networkPlan = new NetworkPlan();
            return PartialView("Delete", networkPlan.Details(ID));
        }

        [HttpPost]
        public ActionResult Delete(NetworkPlan networkPlan)
        {
            networkPlan.Delete(networkPlan.ID);
            return RedirectToAction("Select");
        }
        #endregion Delete

        #region Details
        [HttpGet]
        public ActionResult Details(int ID)
        {
            NetworkPlan networkPlan = new NetworkPlan();
            return PartialView(networkPlan.Details(ID));
        }
        #endregion Details
    }
}