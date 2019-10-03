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
            if (!IsPostBack)
            {
                if (Request.QueryString["SendAcc"] != null) {
                    string txt = Request.QueryString["SendAcc"];
                    isNotiAccept = true;              
                }
                LoadGridData();
            }

        }
        protected bool DisplayListOfDevelopers()
        {         
            return isNotiAccept;
        }

        public void LoadGridData() {
            MessengerRepository mesRes = new MessengerRepository();
            if (isNotiAccept) {
                //gvMessager.Columns[14].Visible = true;
                mesList = mesRes.GetMessagerList().Where(a=>a.msg_close_status== "รอปล่อยงาน").OrderByDescending(a => a.msg_id).ToList();
            } else {
                gvMessager.Columns[15].Visible = false;
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
                MessengerRepository mesRes = new MessengerRepository();
                mesRes.UpdateStatusMessenger(int.Parse(id), "ดำเนินการ");
                var master = Master as Site1;
                if (master != null) {
                    master.CheckAccept();
                }
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('Success')", true);
            } else if (e.CommandName == "lnkEdit") {
                string id = e.CommandArgument.ToString();
                Response.Redirect("~/EditPage.aspx?id=" + id);
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
                modaltd6.InnerText = result.msg_doctype;
                modaltd7.InnerText = result.msg_send + " รับ : " + result.msg_receive;
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
                modaltd20.InnerText = result.msg_edit_date.ToString();
                modaltd20.InnerText = result.msg_edit_by;
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