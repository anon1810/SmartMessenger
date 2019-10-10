using Newtonsoft.Json;
using SmartMessenger.Data;
using SmartMessenger.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SmartMessenger
{
    /// <summary>
    /// Summary description for ChartService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    class item {
        public string x { get; set; }
        public string y { get; set; }

    }

    [System.Web.Script.Services.ScriptService]
    public class ChartService : System.Web.Services.WebService
    {

        [WebMethod]
        public string getChart()
        {
            List<item> Ldata = new List<item>();
            item i1 = new item();
            i1.x = "x1";
            i1.y = "y1";
            Ldata.Add(i1);

            item i2 = new item();
            i2.x = "x2";
            i2.y = "y2";
            Ldata.Add(i2);

            var result = JsonConvert.SerializeObject(Ldata);
            return result;
        }

        [WebMethod]
        public string HelloWorld(string username)
        {
            return "Hello World"+username;
        }


        [WebMethod]
        public string btn(string surname, string name)
        {
            return surname + " " + name;
        }
    }
}
