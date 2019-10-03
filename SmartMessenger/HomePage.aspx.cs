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
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (Request.QueryString["SendAcc"] != null) {
                string txt = Request.QueryString["SendAcc"];
                isNotiAccept = true;              
            }
            LoadGridData();
        }
        protected bool DisplayListOfDevelopers()
        {         
            return isNotiAccept;
        }

        public void LoadGridData() {
            MessengerRepository mesRes = new MessengerRepository();
            if (isNotiAccept) {
                gvMessager.Columns[14].Visible = true;
                mesList = mesRes.GetMessagerList().Where(a=>a.msg_close_status== "รอปล่อยงาน").OrderByDescending(a => a.msg_id).ToList();
            } else {
                gvMessager.Columns[14].Visible = false;
                mesList = mesRes.GetMessagerList().OrderByDescending(a => a.msg_id).ToList();
            }

            if (txtSearch.Value != "") {
                string data = txtSearch.Value;
                List<msgctrlDev> result = null;
                if (opSelectSearch.Value == "ID") {
                    result = mesList.Where(a => a.msg_id.ToString().Contains(data)).ToList();
                } else if (opSelectSearch.Value == "Date") {
                    result = mesList.Where(a => a.msg_date != null).Where(a => a.msg_date.Value.Date.ToString("dd/MM/yyyy").Contains(data)).ToList();
                } else if (opSelectSearch.Value == "By") {
                    result = mesList.Where(a => a.msg_by != null).Where(a => a.msg_by.Contains(data)).ToList();
                } else if (opSelectSearch.Value == "Section") {
                    result = mesList.Where(a => a.msg_section != null).Where(a => a.msg_section.Contains(data)).ToList();
                } else if (opSelectSearch.Value == "ContractName") {
                    result = mesList.Where(a => a.msg_contact_name != null).Where(a => a.msg_contact_name.Contains(data)).ToList();
                } else if (opSelectSearch.Value == "Address") {
                    result = mesList.Where(a => a.msg_address != null).Where(a => a.msg_address.Contains(data)).ToList();
                } else if (opSelectSearch.Value == "Telephone") {
                    result = mesList.Where(a => a.msg_telephone != null).Where(a => a.msg_telephone.Contains(data)).ToList();
                } else if (opSelectSearch.Value == "OnDate") {
                    result = mesList.Where(a => a.msg_on_date != null).Where(a => a.msg_on_date.Value.Date.ToString("dd/MM/yyyy").Contains(data)).ToList();
                } else if (opSelectSearch.Value == "MessengerName") {
                    result = mesList.Where(a => a.msg_msg_name != null).Where(a => a.msg_msg_name.Contains(data)).ToList();
                } else if (opSelectSearch.Value == "Status") {
                    result = mesList.Where(a => a.msg_close_status != null).Where(a => a.msg_close_status.Contains(data)).ToList();
                }
                                         
                gvMessager.DataSource = result;
                gvMessager.DataBind();

            } else {
                foreach (var data in mesList) { 
                    if (data.msg_send == "Yes" && data.msg_receive == "Yes") {
                        data.msg_send = "ส่ง/รับ";
                    } else if (data.msg_send == "Yes") {
                        data.msg_send = "ส่ง";
                    } else if (data.msg_receive == "Yes") {
                        data.msg_send = "รับ";
                    }
                    

                    if (data.msg_priority_normal == "Yes"){
                        data.msg_priority_normal = "ปกติ";
                    } else {
                        data.msg_priority_normal = "ด่วน";
                    }

                    if (data.msg_close_status == "Yes"){
                        data.msg_close_status = "เสร็จสิ้น";
                    } else if(data.msg_close_status == null){
                        data.msg_close_status = "ดำเนินการ(เก่า)";
                    }

                    if (data.msg_doctype != null) {
                        data.msg_doctype=data.msg_doctype.Replace("ส่ง|"," ส่ง:");
                        data.msg_doctype = data.msg_doctype.Replace("รับ|", " รับ:");
                        data.msg_doctype = data.msg_doctype.Replace("|", "");
                    }
                    
                }
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
            } else if (e.CommandName == "lnkOutJob") {
                string id = e.CommandArgument.ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('lnkOutJob')", true);
            } else if(e.CommandName == "lnkEdit") {
                string id = e.CommandArgument.ToString();
                Response.Redirect("~/EditPage.aspx?id=" + id);
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