using SmartMessenger.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMessenger
{
    public partial class EditPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null) {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack) {
                setData();
            }
        }
        void setData() {
            string idRequest = Request["id"];
            MessengerRepository mes = new MessengerRepository();
            var result = mes.GetMessagerByID(int.Parse(idRequest));

            txtByCreateP.Value = result.msg_by;
            txtSectionCreateP.Value = result.msg_section;
            txtByPhoneCreateP.Value = result.msg_phone;
   
            if (result.msg_doctype.Split('|')[1] != "") {
                opSendCreateP.Value = result.msg_doctype.Split('|')[1];
            }
            if (result.msg_doctype.Split('|')[2] != "") {
                txtSendCreateP.Value = result.msg_doctype.Split('|')[2];
                txtSendCreateP.Disabled = false;
            }
            if (result.msg_doctype.Split('|')[4] != "") {
                opReceiveCreateP.Value = result.msg_doctype.Split('|')[4];
            }
            if (result.msg_doctype.Split('|')[5] != "") {
                txtReceiveCreateP.Value = result.msg_doctype.Split('|')[5];
                txtReceiveCreateP.Disabled = false;
            }

            if (result.msg_priority_normal=="Yes") {
                nmCreateP.Checked = true;
            } else {
                urCreateP.Checked = true;
            }

            txtContratNameCreateP.Value = result.msg_contact_name;
            txtContratPhoneCreateP.Value = result.msg_telephone;
            txtContratAddrCreateP.Value = result.msg_address;
            dateContratCreatePage.Value = result.msg_on_date.Value.Date.ToString("yyyy-MM-dd").Replace(' ', 'T');
            txtOldfile.InnerText = "ไฟล์ปัจจุบัน : -";
            if (result.msg_map != "") {
                txtOldfile.InnerText = "ไฟล์ปัจจุบัน : "+ result.msg_map;
            }

            txtMesNameCreatePage.Value = result.msg_msg_name;
            txtRemarkCreatPage.Value = result.msg_remark;

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int idRequest = int.Parse(Request["id"]);
            MessengerRepository mesRes = new MessengerRepository();
            DateTime msg_date = DateTime.Now;
            string msg_by = txtByCreateP.Value;
            string msg_section = txtSectionCreateP.Value;
            string msg_phone = txtByPhoneCreateP.Value;
            string msg_send = "No";
            string msg_receive = "No";
            string msg_doctype = "";
            if (opSendCreateP.Value != "0" && opReceiveCreateP.Value != "0")
            {
                msg_send = "Yes";
                msg_doctype += "ส่ง|" + opSendCreateP.Value + "|" + txtSendCreateP.Value + "|";
                msg_receive = "Yes";
                msg_doctype += "รับ|" + opReceiveCreateP.Value + "|" + txtReceiveCreateP.Value + "|";
            }
            else if (opSendCreateP.Value != "0")
            {
                msg_send = "Yes";
                msg_doctype += "ส่ง|" + opSendCreateP.Value + "|" + txtSendCreateP.Value + "||||";
            }
            else if (opSendCreateP.Value != "0")
            {
                msg_receive = "Yes";
                msg_doctype += "|||รับ|" + opReceiveCreateP.Value + "|" + txtReceiveCreateP.Value + "|";
            }

            string msg_priority_normal = "-";
            string msg_priority_urgent = "-";
            if (nmCreateP.Checked)
            {
                msg_priority_normal = "Yes";
            }
            else
            {
                msg_priority_urgent = "Yes";
            }

            string msg_contact_name = txtContratNameCreateP.Value;
            string msg_telephone = txtContratPhoneCreateP.Value;
            string msg_address = txtContratAddrCreateP.Value;
            DateTime msg_on_date = DateTime.ParseExact(dateContratCreatePage.Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string msg_map = "";
            string msg_msg_name = txtMesNameCreatePage.Value;
            string msg_remark = txtRemarkCreatPage.Value;
            string msg_status = "";

            if (FileUploadMap.PostedFile.FileName != "") {
                msg_map = FileUploadMap.PostedFile.FileName;
                UploadFile(FileUploadMap);
            }

            string msg_edit_by = Session["Name"].ToString();


            mesRes.UpdateMessenger(idRequest, msg_by, msg_section, msg_phone, msg_send, msg_receive, msg_doctype, msg_priority_normal, msg_priority_urgent, msg_contact_name, msg_address, msg_telephone, msg_map, msg_on_date, msg_msg_name, msg_remark, msg_status, msg_edit_by);
            Response.Redirect("HomePage.aspx");
        }
        public void UploadFile(FileUpload file)
        {
            if (file.HasFile)
            {
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("~/FileUpload/") + filename);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            id01.Style["display"] = "block";
        }

        protected void btnCancelRemark_Click(object sender, EventArgs e)
        {
            string remark = Textarea1.Value;
            int idRequest = int.Parse(Request["id"]);
            MessengerRepository mesRes = new MessengerRepository();
            mesRes.UpdateCancelStatusMessenger(idRequest, "ยกเลิก", Session["Name"].ToString(), remark);
            Response.Redirect("HomePage.aspx");
        }

    }
}