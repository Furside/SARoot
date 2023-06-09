﻿using MobilePlan.Classes;
using PMSRedirect;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
namespace MobilePlan.Models
{
    public class NetworkType
    {
        dbcontrol s = new dbcontrol();
        UserSessions session = new UserSessions();
        [Display(Name = "ID")]
        [ScaffoldColumn(false)]
        public Int32 ID { get; set; }

        [Display(Name = "Network")]
        public String Network { get; set; }

        [Display(Name = "Encoded By")]
        [ScaffoldColumn(false)]
        public Int32 encBy { get; set; }

        [Display(Name = "Encoded Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM dd, yyyy hh:mm tt}")]
        [DataType(DataType.Date)]
        public DateTime? encDate { get; set; }

        public NetworkType()
        {
        }
        public List<NetworkType> List(string Search = "")
        {
            return s.Query<NetworkType>("SELECT * FROM [tbl_NetworkType] WHERE CONCAT([ID],[Network],[encBy],[encDate]) LIKE @Search", p => p.Add("@Search", $"%{Search}%"))
            .Select(r =>
            {
                return r;
            }).ToList();
        }

        public SelectList ListNetwork()
        {
            var list = new SelectList(List(), "ID", "Network");
            return list;
        }

        public NetworkType Find(int ID)
        {

            return s.Query<NetworkType>("SELECT * FROM tbl_NetworkType where ID = @ID", p => p.Add("@ID", ID))
            .Select(r =>
            {

                return r;
            }).SingleOrDefault();
        }

        public int Create(NetworkType obj)
        {
            foreach(var item in List())
            {
                if (item.Network.ToLower().Trim() == obj.Network.ToLower().Trim())
                {
                    return 0;
                }
            }
            var ID = s.Insert("[tbl_NetworkType]", p =>
            {
                p.Add("Network", obj.Network);
                p.Add("encBy", session.User.ID);
            });
            return ID;
        }

        public void Update(NetworkType obj)
        {
            s.Update("[tbl_NetworkType]", obj.ID, p =>
            {
                p.Add("Network", obj.Network);

            });
        }
        public void Delete(NetworkType obj)
        {
            s.Query("DELETE FROM [tbl_NetworkType] WHERE ID = @ID", p =>
            {
                p.Add("@ID", obj.ID);
            });
        }

        public SelectList ListPrefix(string Search="")
        {
            var list = new SelectList(PrefixNoList(), "Prefix", "NetworkProviderID");
            return list;
        }

        public List<PrefixNo> PrefixNoList(string Search = "")
        {
            return s.Query<PrefixNo>("SELECT * FROM [tbl_SimNetworkPrefixes] WHERE CONCAT([Prefix],[NetworkProviderID]) LIKE @Search", p => p.Add("@Search", $"%{Search}%"))
                .Select(r =>{ return r; }).ToList();
        }

        public List<PrefixNo> PrefixNoListItem(string Search = "")
        {
            return s.Query<PrefixNo>("SELECT * FROM [tbl_SimNetworkPrefixes] WHERE [Prefix] LIKE @Search", p => p.Add("@Search", $"%{Search}%"))
                .Select(r => { return r; }).ToList();
        }

        public static List<Contact> NetworkContactList(string Search = "")
        {
            return new Contact().List(Search);
        }
    }


}