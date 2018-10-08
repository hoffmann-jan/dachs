using System;
using System.Net.Http;

namespace dachs
{
    class Program
    {
        string __EVENTVADILATION = "%2FwEdAAT4a%2F0VhriQbIVD9%2FbvKbrtDlSBEqzYFlt%2BcOr2YiDfOlyVpOm4%2BiOnL8lQyfdNrJtLXopOGbu3LzPW1UTL9bhKS%2BMv%2F%2By7F6VC1D3JVQ3sXhGOuJWb0beY%2BmRTiGE9bws%3D";
        string __VIEWSTAT = "%2FwEPDwUKMTA1MzQ4MjU4NQ9kFgJmD2QWAgIBD2QWBAIFDxBkZBYAZAITDxBkZBYAZGQ8SBK%2FYB%2BUaXwCWdo0khKeMVwn0MkW9YllXaUUgCdq5Q%3D%3D";
        string __VIEWSTATEGENERATOR = "90059987";
        string btnFindStreet = "Suche+starten";
        string txtHnr = "";
        string txtStreet = "";

        string cookie = "wt_cdbeid=1; wt3_sid=%3B498716889887543; wt_geid=815296688990039019590195; wt_rla=498716889887543%2C4%2C1538654274799; wt3_eid=%3B498716889887543%7C2152966889852904860%232153865497300881676";

        static void Main(string[] args)
        {
            var client = new HttpClient();
            var response = client.GetAsync("https://leipzigde01.webtrekk.net/498716889887543/hm?p=435,adressen_leipzig_de.,319,375&tz=2&eid=2152966889852904860&one=0&fns=0&geid=815296688990039019590195&eor=1&hm_ts=1538652452113").Result;


        }
    }
}
