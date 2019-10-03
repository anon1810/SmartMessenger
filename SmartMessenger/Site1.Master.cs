using SmartMessenger.Data;
using SmartMessenger.Repositories;
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
        public string notiAccept { get { return notiAcceptPage.InnerText; } set { notiAcceptPage.InnerText = value; } }
        public string notiClose { get { return notiClosePage.InnerText; } set { notiClosePage.InnerText = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckAccept();
                CheckClose();
            }
        }

        public void CheckAccept() {
            MessengerRepository mes = new MessengerRepository();
            var result = mes.GetMessagerList().Where(a=>a.msg_close_status== "รอปล่อยงาน").ToList();
            notiAccept = result.Count.ToString();
        }
        public void CheckClose()
        {
            MessengerRepository mes = new MessengerRepository();
            var result = mes.GetMessagerList().Where(a => a.msg_close_status == "ดำเนินการ").ToList();
            notiClose = result.Count.ToString();
        }
    }
}