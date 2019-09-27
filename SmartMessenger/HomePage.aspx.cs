using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartMessenger.Data;
using System.Collections.Generic;
using SmartMessenger.Repositories;

namespace SmartMessenger
{
    public partial class HomePage : System.Web.UI.Page
    {
        List<msgctrl> mesList = new List<msgctrl>();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGridData();
        }

        public void LoadGridData() {
            MessengerRepository mesRes = new MessengerRepository();
            mesList = mesRes.GetMessagerList().OrderByDescending(a=>a.msg_id).ToList();
            if (txtSearch.Value!="") {
                string phone = txtSearch.Value;
                var result = mesList.Where(a=>a.msg_phone!=null).Where(a => a.msg_phone.Contains(phone)).ToList();
                gvMessager.DataSource = result;
                gvMessager.DataBind();
            } else {
                gvMessager.DataSource = mesList;
                gvMessager.DataBind();
            }
        }

        protected void gvMessager_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMessager.PageIndex = e.NewPageIndex;
            LoadGridData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void Create_ServerClick(object sender, EventArgs e)
        {
            ((Site1)Page.Master).MyText = "9";
        }
    }
}