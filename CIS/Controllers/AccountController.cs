
using CIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Controllers
{
    public class AccountController : Controller
    {
        #region Select
        [HttpGet]
        public ActionResult Select()
        {
            Account account = new Account();
            return PartialView("Select", account.Select());
        }
        #endregion Select

        #region Insert
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Account account)
        {
            account.Insert(account);
            return RedirectToAction("Select");
        }
        #endregion Insert

        #region Update
        [HttpGet]
        public ActionResult Update(int ID)
        {
            Account account = new Account();
            return PartialView("Update", account.Details(ID));
        }

        [HttpPost]
        public ActionResult Update(Account account)
        {
            account.Update(account);
            return RedirectToAction("Select");
        }
        #endregion Update

        #region Delete
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Account account = new Account();
            return PartialView("Delete", account.Details(ID));
        }

        [HttpPost]
        public ActionResult Delete(Account account)
        {
            account.Delete(account.ID);
            return RedirectToAction("Select");
        }
        #endregion Delete

        #region Details
        [HttpGet]
        public ActionResult Details(int ID)
        {
            Account account = new Account();
            return PartialView(account.Details(ID));
        }
        #endregion Details
    }
}