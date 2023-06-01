using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CIS.Models;
using PMSRedirect;

namespace CIS.Controllers
{
    public enum NETWORK_SMART
    {
        #region smart prefixes
        NETWORK_SMART_NO_001 = 813 ,
        NETWORK_SMART_NO_002 = 907 ,
        NETWORK_SMART_NO_003 = 908 ,
        NETWORK_SMART_NO_004 = 909 ,
        NETWORK_SMART_NO_005 = 910 ,
        NETWORK_SMART_NO_006 = 911 ,
        NETWORK_SMART_NO_007 = 912 ,
        NETWORK_SMART_NO_008 = 913 ,
        NETWORK_SMART_NO_009 = 914 ,
        NETWORK_SMART_NO_010 = 918 ,
        NETWORK_SMART_NO_011 = 919 ,
        NETWORK_SMART_NO_012 = 920 ,
        NETWORK_SMART_NO_013 = 921 ,
        NETWORK_SMART_NO_014 = 928 ,
        NETWORK_SMART_NO_015 = 929 ,
        NETWORK_SMART_NO_016 = 930 ,
        NETWORK_SMART_NO_017 = 938 ,
        NETWORK_SMART_NO_018 = 939 ,
        NETWORK_SMART_NO_019 = 946 ,
        NETWORK_SMART_NO_020 = 947 ,
        NETWORK_SMART_NO_021 = 948 ,
        NETWORK_SMART_NO_022 = 949 ,
        NETWORK_SMART_NO_023 = 950 ,
        NETWORK_SMART_NO_024 = 951 ,
        NETWORK_SMART_NO_025 = 961 ,
        NETWORK_SMART_NO_026 = 963 ,
        NETWORK_SMART_NO_027 = 968 ,
        NETWORK_SMART_NO_028 = 970 ,
        NETWORK_SMART_NO_029 = 981 ,
        NETWORK_SMART_NO_030 = 989 ,
        NETWORK_SMART_NO_031 = 992 ,
        NETWORK_SMART_NO_032 = 998 ,
        NETWORK_SMART_NO_033 = 999 , 
        #endregion
    }
    public enum NETWORK_GLOBE
    {

    }
    public class ContactController : Controller
    {
        Contact mod = new Contact();
        UserSessions session = new UserSessions();
        
        [HttpGet]
        public ActionResult Index(string searchItem = "")
        {
            Contact contact = new Contact();

            //return Profile(Convert.ToInt32(576));
            return View(contact);
        }

        //ok
        [HttpGet]
        public ActionResult Profile(int? ID)
        {
            Contact contact = new Contact();
            if (ID.HasValue)
            {
                return View(contact.Find(ID));
            }
            return View(contact);
        }

        //ok
        [HttpGet]
        public ActionResult Search(string item)
        {
            Contact contact = new Contact();
            
            return PartialView("List", contact );
            //return string.IsNullOrWhiteSpace(item) ? PartialView("List", contact.InitializeData()) : PartialView("List", contact.Find(item));

        }

        //Create------------------------------------------
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Create(contact);
                return RedirectToAction("Index", "Home");
            }

            return View(contact);
        }


        //ok
        //Update------------------------------------------
        [HttpGet]
        public ActionResult Update(int ID)
        {
            Contact contact = new Models.Contact();
            return View(contact.Find(ID, ID));
        }

        //ok
        [HttpPost]
        public ActionResult Update(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Update(contact);
                return RedirectToAction("Index", "Home");
            }

            return View(contact);
        }

        //Search------------------------------------------
        [HttpGet]
        public ActionResult List(string search = "")
        {
            Contact contact = new Contact();
            return View(contact);
        }

        [HttpGet]
        public ActionResult Find(int ID)
        {
            Contact contact = new Models.Contact();
            return View(contact.Find(ID));
        }


        //ok
        //Delete------------------------------------------
        [HttpGet]
        public ActionResult DeleteView(int ID)
        {
            Contact contact = new Contact();
            return View(contact.Find(ID.ToString()));
        }

        //ok
        [HttpPost]
        public ActionResult DeleteView(Contact contact)
        {
            //if (ModelState.IsValid)
            //{
                contact.Delete(contact);
                return RedirectToAction("Index", "Home", "");
            //}

            //return View(contact);
        }

        [HttpGet]
        public ActionResult DisplayAll(string search = "")
        {
            Contact contact = new Models.Contact();
            return View(contact.UsersDB());
        }
    }
}