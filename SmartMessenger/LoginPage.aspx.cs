using SmartMessenger.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMessenger
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack){
              if (Request.Cookies["UserName"] != null) {
                  txtUsername.Value = Request.Cookies["UserName"].Value;
              }
              if (Request.Cookies["Password"] != null) {
                  txtPsw.Attributes["value"] = Request.Cookies["Password"].Value;
              }
              if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null) {
                  chkbRemem.Checked = true;
              }
            }
            Session["Username"] = null;
            Session["Name"] = null;
            Session["Department"] = null;
        }

        

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Value;
            string password = txtPsw.Text;
            MessengerRepository mesRes = new MessengerRepository();
            var result = mesRes.Login(username,password);
            if (result != null) {
                Session["Username"] = result.username;
                Session["Name"] = result.name;
                Session["Department"] = result.department;
                chkRememberme(result.username, result.password);
                Response.Redirect("HomePage.aspx");
            } else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('Error')", true);
            }
        }

        protected void chkRememberme(string username ,string password) {
            if (chkbRemem.Checked)
            {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
            } else {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

            }
            Response.Cookies["UserName"].Value = username;
            Response.Cookies["Password"].Value = password;
        }
    }
}