using CIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Controllers
{
    public class ContractController : Controller
    {
        #region Select
        [HttpGet]
        public ActionResult Select()
        {
            Contract contract = new Contract();
            return PartialView("Select", contract.Select());
        }
        #endregion Select

        #region Insert
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Contract contract)
        {
            contract.Insert(contract);
            return RedirectToAction("Select");
        }
        #endregion Insert

        #region Update
        [HttpGet]
        public ActionResult Update(int ID)
        {
            Contract contract = new Contract();
            return PartialView("Update", contract.Details(ID));
        }

        [HttpPost]
        public ActionResult Update(Contract contract)
        {
            contract.Update(contract);
            return RedirectToAction("Select");
        }
        #endregion Update

        #region Delete
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Contract contract = new Contract();
            return PartialView("Delete", contract.Details(ID));
        }

        [HttpPost]
        public ActionResult Delete(Contract contract)
        {
            contract.Delete(contract.ID);
            return RedirectToAction("Select");
        }
        #endregion Delete

        #region Details
        [HttpGet]
        public ActionResult Details(int ID)
        {
            Contract contract = new Contract();
            return PartialView(contract.Details(ID));
        }
        #endregion Details
    }
}