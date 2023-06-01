using CIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Controllers
{
    public class TController<TModel> : Controller where TModel : BaseModel, new()
    {
        #region Modal Redirect
        public ActionResult ModalRedirect(int id, string controllerAction) 
            => RedirectToAction(controllerAction, new { ID = id });
        #endregion Modal Redirect



        #region Select
        [HttpGet]
        public ActionResult Select() 
            => PartialView("Select", new TModel());
        #endregion Select



        #region Insert
        [HttpGet]
        public ActionResult Insert() 
            => View();

        [HttpPost]
        virtual public ActionResult Insert(TModel model)
        {
            model.Insert<TModel>(model);
            return RedirectToAction("Select");
        }
        #endregion Insert



        #region Update
        [HttpGet]
        public ActionResult Update(int ID) 
            => PartialView("Update", new TModel().Details<TModel>(ID));

        [HttpPost]
        virtual public ActionResult Update(TModel model)
        {
            model.Update<TModel>(model);
            return RedirectToAction("Select");
        }
        #endregion Update



        #region Delete
        [HttpGet]
        public ActionResult Delete(int ID)
            => PartialView("Delete", new TModel().Details<TModel>(ID));

        [HttpPost]
        virtual public ActionResult Delete(TModel model)
        {
            model.Delete(model.ID);
            return RedirectToAction("Select");
        }
        #endregion Delete



        #region Details
        [HttpGet]
        public ActionResult Details(int ID)
            => PartialView(new TModel().Details<TModel>(ID));
        #endregion Details
    }
}