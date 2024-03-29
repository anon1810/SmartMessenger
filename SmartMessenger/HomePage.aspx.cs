﻿using System;
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
            id01.Style["display"] = "none";
            modelUplodeReport.Style["display"] = "none";
            waringModeEdit.Style["display"] = "none";

            if (Session["Username"] == null) {
                Response.Redirect("LoginPage.aspx");
            }

            if (Request.QueryString["LoadPage"] != null) {
                string result = Request.QueryString["LoadPage"];
                if (result == "Accept") {
                    isNotiAccept = true;
                } else if (result == "Close") {
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
                gvMessager.Columns[12].Visible = false;
                mesList = mesRes.GetMessagerList().Where(a => a.msg_close_status == "รอปล่อยงาน").OrderByDescending(a => a.msg_id).ToList();
            } else if (isNotiClose) {
                gvMessager.Columns[11].Visible = false;
                mesList = mesRes.GetMessagerList().Where(a => a.msg_close_status == "ดำเนินการ").OrderByDescending(a => a.msg_id).ToList();
            } else {
                gvMessager.Columns[11].Visible = false;
                gvMessager.Columns[12].Visible = false;
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

        public bool chkIsAdmin() {
            MessengerRepository mesRes = new MessengerRepository();
            return mesRes.isAdmin(Session["Username"].ToString());
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
                string strFileName = e.CommandArgument.ToString();
                string path = Server.MapPath("~/FileUpload//" + strFileName);
                if (File.Exists(path)) {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "filename=" + e.CommandArgument);
                    Response.TransmitFile(Server.MapPath("~/FileUpload/") + e.CommandArgument);
                    Response.End();
                } else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('ไม่พบไฟล์ที่ต้องการ กรุณาติดต่อผู้ดูแลระบบ')", true);
                }
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
                MessengerRepository mesRes = new MessengerRepository();
                var result = mesRes.GetMessagerByID(int.Parse(id));
                if (result.msg_accept_by != null && !mesRes.isAdmin(Session["Username"].ToString())) {
                    waringModeEdit.Style["display"] = "block";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('กรุณาติดต่อผู้ดูแลระบบ')", true);
                } else {
                    Response.Redirect("~/EditPage.aspx?id=" + id);
                }
            } else if (e.CommandName == "lnkCloseJob") { //อาจจะมีเช็คสิทธ์ปิด Job งาน
                string id = e.CommandArgument.ToString();
                modelUplodeReport.Style["display"] = "block";
                lblRefnumber.InnerText = id;
                //MessengerRepository mesRes = new MessengerRepository();
                //mesRes.UpdateCloseStatusMessenger(int.Parse(id), "เสร็จสิ้น", Session["Name"].ToString());
                //var master = Master as Site1;
                //if (master != null) {
                //    master.CheckClose();
                //}
                //Page.Response.Redirect(Page.Request.Url.ToString(), true);
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
                lblMapText.Text = "";
                lnkBtnloadMap.Text = "";
                if (result.msg_map == "แนบแผนที่" || result.msg_map == "แนบแผนทีี่") {
                    lblMapText.Text = result.msg_map;
                } else {
                    lnkBtnloadMap.Text = result.msg_map;
                }
                modaltd13.InnerText = result.msg_on_date.Value.ToString("dd/MM/yyyy");
                modaltd14.InnerText = result.msg_remark;
                modaltd15.InnerText = result.msg_msg_name;
                modaltd16.InnerText = result.msg_close_status;
                modaltd17.InnerText = result.msg_accept_by +" "+result.msg_accept_date;
                modaltd19.InnerText = result.msg_close_by + " " + result.msg_close_date;
                modaltd20.InnerText = result.msg_edit_by + " " + result.msg_edit_date;
                lnkBtnloadReport.Text = result.msg_report;
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

        protected void lnkBtnloadMap_Click(object sender, EventArgs e) {
            string strFileName = lnkBtnloadMap.Text;
            string path = Server.MapPath("~/FileUpload//" + lnkBtnloadMap.Text);
            if (File.Exists(path)) {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "filename=" + lnkBtnloadMap.Text);
                Response.TransmitFile(Server.MapPath("~/FileUpload/") + lnkBtnloadMap.Text);
                Response.End();
            } else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('ไม่พบไฟล์ที่ต้องการ กรุณาติดต่อผู้ดูแลระบบ')", true);
            }
        }

        public void UploadFile(FileUpload file, string filename) {
            if (file.HasFile) {
                file.SaveAs(Server.MapPath("~/FileUpload/") + filename);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            string id = lblRefnumber.InnerText;
            MessengerRepository mesRes = new MessengerRepository();
            string newFileName = "";
            if (FileUploadReport.HasFile) {
                newFileName = Path.Combine(Path.GetDirectoryName(FileUploadReport.PostedFile.FileName)
                            , string.Concat(Path.GetFileNameWithoutExtension(FileUploadReport.PostedFile.FileName)
                            , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                            , Path.GetExtension(FileUploadReport.PostedFile.FileName)));
            }
            string msg_report = newFileName;
            UploadFile(FileUploadReport, msg_report);
            mesRes.UpdateCloseStatusMessenger(int.Parse(id), "เสร็จสิ้น", Session["Name"].ToString(), msg_report);
            var master = Master as Site1;
            if (master != null) {
                master.CheckClose();
            }
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void lnkBtnloadReport_Click(object sender, EventArgs e) {
            string strFileName = lnkBtnloadReport.Text;
            string path = Server.MapPath("~/FileUpload//" + lnkBtnloadReport.Text);
            if (File.Exists(path)) {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "filename=" + lnkBtnloadReport.Text);
                Response.TransmitFile(Server.MapPath("~/FileUpload/") + lnkBtnloadReport.Text);
                Response.End();
            } else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSuccess", "alert('ไม่พบไฟล์ที่ต้องการ กรุณาติดต่อผู้ดูแลระบบ')", true);
            }
        }
    }
}