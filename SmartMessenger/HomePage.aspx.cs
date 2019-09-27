using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartMessenger.Data;
using System.Collections.Generic;

namespace SmartMessenger
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGridData();
        }

        public void LoadGridData() {
            using (MessengerDataClassesDataContext en = new MessengerDataClassesDataContext())
            {
                var result = en.msgctrls.ToList();
                gvMessager.DataSource = result;
                gvMessager.DataBind();
            }
        }

        protected void gvMessager_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMessager.PageIndex = e.NewPageIndex;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (MessengerDataClassesDataContext en = new MessengerDataClassesDataContext())
            {
                var result = en.msgctrls.Where(a => a.msg_phone==txtSearch.Value).ToList();
                gvMessager.DataSource = result;
                gvMessager.DataBind();
            }
        }
    }
}