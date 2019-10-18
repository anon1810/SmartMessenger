using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartMessenger.Data;
using System.Collections.Generic;
using SmartMessenger.Repositories;
using System.Data.SqlClient;
using System.IO;

namespace SmartMessenger
{
    public partial class HomePage : System.Web.UI.Page
    {
        List<msgctrlDev> mesList = new List<msgctrlDev>();
        bool isNotiAccept = false;
        bool isNotiClose = false;
        protected void Page_Load(object sender, EventArgs e)
        {              
            if (Session["Username"] == null) {
                Response.Redirect("LoginPage.aspx");
            }

            if (Request.QueryString["LoadPage"] != null) {
                string result = Request.QueryString["LoadPage"];
                if (result == "Accept") {
                    isNotiAccept = true;
                } else if (result== "Close") {
                    isNotiClose = true;
                }            
             }

            if (!IsPostBack)
            {
                LoadGridData();
            }

        }
        protected bool ChkShowAcceptBtn()
        {         
            return isNotiAccept;
        }

        protected bool ChkShowCloseBtn()
        {
            return isNotiClose;
        }

        public void LoadGridData() {
            MessengerRepository mesRes = new MessengerRepository();
            if (isNotiAccept) {
                gvMessager.Columns[16].Visible = false;
                mesList = mesRes.GetMessagerList().Where(a => a.msg_close_status == "รอปล่อยงาน").OrderByDescending(a => a.msg_id).ToList();
            } else if (isNotiClose) {
                gvMessager.Columns[15].Visible = false;
                mesList = mesRes.GetMessagerList().Where(a => a.msg_close_status == "ดำเนินการ").OrderByDescending(a => a.msg_id).ToList();
            } else {
                gvMessager.Columns[15].Visible = false;
                gvMessager.Columns[16].Visible = false;
                mesList = mesRes.GetMessagerList().OrderByDescending(a => a.msg_id).ToList();
            }

            foreach (var data in mesList) {
                if (data.msg_send == "Yes" && data.msg_receive == "Yes") {
                    data.msg_send = "ส่ง/รับ";
                } else if (data.msg_send == "Yes") {
                    data.msg_send = "ส่ง";
                } else if (data.msg_receive == "Yes") {
                    data.msg_send = "รับ";
                }


                if (data.msg_priority_normal == "Yes") {
                    data.msg_priority_normal = "ปกติ";
                } else {
                    data.msg_priority_normal = "ด่วน";
                }

                if (data.msg_close_status == "Yes") {
                    data.msg_close_status = "เสร็จสิ้น";
                } else if (data.msg_close_status == null) {
                    data.msg_close_status = "ดำเนินการ(เก่า)";
                }

                if (data.msg_doctype != null) {
                    data.msg_doctype = data.msg_doctype.Replace("ส่ง|", " ส่ง:");
                    data.msg_doctype = data.msg_doctype.Replace("รับ|", " รับ:");
                    data.msg_doctype = data.msg_doctype.Replace("|", "");
                }
            }

            if (txtSearch.Value != "") {
                string data = txtSearch.Value.ToLower();
                List<msgctrlDev> result = null;
                if (opSelectSearch.Value == "ID") {
                    result = mesList.Where(a => a.msg_id.ToString().ToLower().Contains(data)).ToList();
                } else if (opSelectSearch.Value == "Date") {
                    result = mesList.Where(a => a.msg_date != null).Where(a => a.msg_date.Value.Date.ToString("dd/MM/yyyy").Contains(data)).ToList();
                } else if (opSelectSearch.Value == "By") {
                    result = mesList.Where(a => a.msg_by != null).Where(a => a.msg_by.ToLower().Contains(data)).ToList();
                } else if (opSelectSearch.Value == "Section") {
                    result = mesList.Where(a => a.msg_section != null).Where(a => a.msg_section.ToLower().Contains(data)).ToList();
                } else if (opSelectSearch.Value == "ContractName") {
                    result = mesList.Where(a => a.msg_contact_name != null).Where(a => a.msg_contact_name.ToLower().Contains(data)).ToList();
                } else if (opSelectSearch.Value == "Address") {
                    result = mesList.Where(a => a.msg_address != null).Where(a => a.msg_address.ToLower().Contains(data)).ToList();
                } else if (opSelectSearch.Value == "Telephone") {
                    result = mesList.Where(a => a.msg_telephone != null).Where(a => a.msg_telephone.ToLower().Contains(data)).ToList();
                } else if (opSelectSearch.Value == "OnDate") {
                    result = mesList.Where(a => a.msg_on_date != null).Where(a => a.msg_on_date.Value.Date.ToString("dd/MM/yyyy").Contains(data)).ToList();
                } else if (opSelectSearch.Value == "MessengerName") {
                    result = mesList.Where(a => a.msg_msg_name != null).Where(a => a.msg_msg_name.ToLower().Contains(data)).ToList();
                } else if (opSelectSearch.Value == "Status") {
                    result = mesList.Where(a => a.msg_close_status != null).Where(a => a.msg_close_status.ToLower().Contains(data)).ToList();
                }
                                         
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



        protected void Create_ServerClick(object sender, EventArgs e)
        {
            //((Site1)Page.Master).MyText = "9";
            Response.Redirect("CreateMessengerPage.aspx");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gvMessager_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkDownload") {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("~/FileUpload/") + e.CommandArgument);
                Response.End();
            } else if (e.CommandName == "lnkOutJob") { //อาจจะมีเช็คสิทธ์การกดปุ่มปล่อยงาน
                string id = e.CommandArgument.ToString();
                MessengerRepository mesRes = new MessengerRepository();
                mesRes.UpdateAcceptStatusMessenger(int.Parse(id), "ดำเนินการ", Session["Name"].ToString());
                var master = Master as Site1;
                if (master != null) {
                    master.CheckAccept();
                }
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('Success')", true);
            } else if (e.CommandName == "lnkEdit") {
                string id = e.CommandArgument.ToString();
                Response.Redirect("~/EditPage.aspx?id=" + id);
            } else if (e.CommandName == "lnkCloseJob") { //อาจจะมีเช็คสิทธ์ปิด Job งาน
                string id = e.CommandArgument.ToString();
                MessengerRepository mesRes = new MessengerRepository();
                mesRes.UpdateCloseStatusMessenger(int.Parse(id), "เสร็จสิ้น", Session["Name"].ToString());
                var master = Master as Site1;
                if (master != null) {
                    master.CheckClose();
                }
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            } else if (e.CommandName == "lnkView") {
                id01.Style["display"] = "block";
                string id = e.CommandArgument.ToString();
                MessengerRepository mes = new MessengerRepository();
                var result = mes.GetMessagerByID(int.Parse(id));
                modaltd1.InnerText = result.msg_id.ToString();
                modaltd2.InnerText = result.msg_date.ToString();
                modaltd3.InnerText = result.msg_by;
                modaltd4.InnerText = result.msg_section;
                modaltd5.InnerText = result.msg_phone;

                string doctype = result.msg_doctype;
                doctype = doctype.Replace("ส่ง|", " ส่ง:");
                doctype = doctype.Replace("รับ|", " รับ:");
                doctype = doctype.Replace("|", "");

                modaltd6.InnerText = doctype;

                string sendreceive = "";
                if (result.msg_send == "Yes" && result.msg_receive == "Yes") {
                    sendreceive = "ส่ง/รับ";
                } else if (result.msg_send == "Yes") {
                    sendreceive = "ส่ง";
                } else if (result.msg_receive == "Yes") {
                    sendreceive = "รับ";
                }
                modaltd7.InnerText = sendreceive;
                modaltd8.InnerText = result.msg_priority_normal == "Yes" ? "ปกติ" : "ด่วน";
                modaltd9.InnerText = result.msg_contact_name;
                modaltd10.InnerText = result.msg_address;
                modaltd11.InnerText = result.msg_telephone;
                modaltd12.InnerText = result.msg_map;
                modaltd13.InnerText = result.msg_on_date.ToString();
                modaltd14.InnerText = result.msg_remark;
                modaltd15.InnerText = result.msg_msg_name;
                modaltd16.InnerText = result.msg_close_status;
                modaltd17.InnerText = result.msg_accept_by;
                modaltd18.InnerText = result.msg_accept_date.ToString();
                modaltd19.InnerText = result.msg_close_date.ToString();
                modaltd20.InnerText = result.msg_edit_by;
                modaltd21.InnerText = result.msg_edit_date.ToString();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var btnDelete = (Button)sender;
                var row = (GridViewRow)btnDelete.NamingContainer;
                //var img = (Image)row.FindControl("img");
                //string pathImg = Server.MapPath("~") + img.ImageUrl;
                //if (File.Exists(pathImg))
                //{
                //    File.Delete(pathImg);
                //}
                int id = int.Parse(row.Cells[0].Text);
                string x = "";
                //movieRepo.deleteMovie(id);
                //bindDataMovie();
                //showAlertSuccess("alertDelSuccess", "Delete success");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('Success')", true);
            }
            catch (SqlException sqlEx) {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('Success')", true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSqlErr", "alert('"+ sqlEx.Message + "')", true);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('Success')", true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertErr", "alert('" + ex.Message + "')", true);
            }
        }

    }
}