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

        public string txtProfile { get { return lblProfile.InnerText; } set { lblProfile.InnerText = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            waringModel.Style["display"] = "none";

            if (Session["Username"] == null) {
                Response.Redirect("LoginPage.aspx");
            } else {
                txtProfile = "  " + Session["Name"].ToString() + " ";
            }

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


        protected void aAcc_ServerClick(object sender, EventArgs e) {
            MessengerRepository mes = new MessengerRepository();
            if (mes.isAdmin(Session["Username"].ToString())) {
                Response.Redirect("HomePage.aspx?LoadPage=Accept");
            } else {
                waringModel.Style["display"] = "block";
            }
        }

        protected void aClose_ServerClick(object sender, EventArgs e) {
            MessengerRepository mes = new MessengerRepository();
            if (mes.isAdmin(Session["Username"].ToString())) {
                Response.Redirect("HomePage.aspx?LoadPage=Close");
            } else {
                waringModel.Style["display"] = "block";
            }
        }
    }
}