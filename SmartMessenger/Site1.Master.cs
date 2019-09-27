using SmartMessenger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMessenger
{

    public partial class Site1 : System.Web.UI.MasterPage
    {
        public string MyText { get { return notiAcceptPage.InnerText; } set { notiAcceptPage.InnerText = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}