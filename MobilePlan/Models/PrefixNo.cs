using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobilePlan.Classes;
using PMSRedirect;
using System.Web.Mvc;

namespace MobilePlan.Models
{
    public class PrefixNo
    {
        dbcontrol s = new dbcontrol();
        public int Prefix { get; set; }
        public string Network { get; set; }
        public int NetworkProviderID { get; set; }

        public PrefixNo() { }

        public static SelectList prefixNos {
            get
            {
                List<string> list = new List<string>();
                foreach (var x in new NetworkType().ListNetwork() )
                {
                    list.Add(x.Value);
                }
                return new NetworkType().ListNetwork();
            }
        }

        public List<PrefixNo> PrefixNoList(string Search = "")
        {
            return s.Query<PrefixNo>(@"SELECT * FROM NetworkPrefix WHERE CONCAT([Prefix],[Network],[NetworkProviderID]) LIKE @Search", p => p.Add("@Search", $"%{Search}%"))
                .Select(r => { return r; }).ToList();
        }

        public List<PrefixNo> PrefixNoListItem(string Search = "")
        {
            return s.Query<PrefixNo>("SELECT * FROM [tbl_SimNetworkPrefixes] WHERE [Prefix] LIKE @Search", p => p.Add("@Search", $"%{Search}%"))
                .Select(r => { return r; }).ToList();
        }

        //public static List<string> Network()
        //{
        //    List<string> list = new List<string>();
        //    foreach ( var x in new NetworkType().ListPrefix() )
        //    {
        //        list.Add(x.Value);
        //    }
        //    return list;
        //}

        public static SelectList networks {
            get
            {
                return new NetworkType().ListNetwork();
            }
        }
    }
}