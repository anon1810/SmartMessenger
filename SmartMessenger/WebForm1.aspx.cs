using SmartMessenger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMessenger
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGridData();
        }

        public void LoadGridData()
        {
            using (MessengerDataClassesDataContext en = new MessengerDataClassesDataContext())
            {
                var result2 = en.msgctrls.ToList();
                searchGridView.DataSource = result2;
                searchGridView.DataBind();
            }
        }

        protected void gvMessager_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            searchGridView.PageIndex = e.NewPageIndex;
            LoadGridData();
        }
    }
}