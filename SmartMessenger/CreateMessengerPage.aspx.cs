using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartMessenger.Repositories;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;

namespace SmartMessenger
{
    public partial class CreateMessengerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                if (Session["Username"].ToString() == "SPP" || Session["Username"].ToString() == "SDR" || Session["Username"].ToString() == "TYK" || Session["Username"].ToString() == "ANO") {
                    txtByCreateP.Enabled = true;
                    //txtSectionCreateP.Disabled = true;
                } else {
                    txtByCreateP.Text = Session["Name"].ToString();
                    txtSectionCreateP.Value = Session["Department"].ToString();
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> AutoComBy(string pre) {
            List<string> allCompanyName = new List<string>();
            MessengerRepository mesRes = new MessengerRepository();
            //allCompanyName = mesRes.GetMessagerList().Where(a=>a.msg_by!=null&&a.msg_by!="").GroupBy(a => a.msg_by).Select(a => a.Key).Where(a => a.ToLower().Contains(pre.ToLower())).ToList();         
            allCompanyName = mesRes.GetUserList().Select(a => a.name).Where(a => a.ToLower().Contains(pre.ToLower())).ToList();        
            return allCompanyName;
        }

        public void UploadFile(FileUpload file,string filename) {
            if (file.HasFile) {
                //string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("~/FileUpload/") + filename);
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            MessengerRepository mesRes = new MessengerRepository();
            DateTime msg_date = DateTime.Now;
            string msg_by = txtByCreateP.Text;
            string msg_section = txtSectionCreateP.Value;
            string msg_phone = txtByPhoneCreateP.Value;
            string msg_send="No";
            string msg_receive="No";
            string msg_doctype="";
            if (opSendCreateP.Value != "0" && opReceiveCreateP.Value != "0") {
                msg_send = "Yes";
                msg_doctype += "ส่ง|" + opSendCreateP.Value + "|" + txtSendCreateP.Value + "|";
                msg_receive = "Yes";
                msg_doctype += "รับ|" + opReceiveCreateP.Value + "|" + txtReceiveCreateP.Value + "|";
            } else if (opSendCreateP.Value != "0") {
                msg_send = "Yes";
                msg_doctype += "ส่ง|" + opSendCreateP.Value + "|" + txtSendCreateP.Value + "||||";
            } else if (opReceiveCreateP.Value != "0") {
                msg_receive = "Yes";
                msg_doctype += "|||รับ|" + opReceiveCreateP.Value + "|" + txtReceiveCreateP.Value + "|";
            }

            string msg_priority_normal="-";
            string msg_priority_urgent="-";
            if (nmCreateP.Checked) {
                msg_priority_normal = "Yes";
            } else {
                msg_priority_urgent = "Yes";
            }
               
            string msg_contact_name = txtContratNameCreateP.Value;
            string msg_telephone = txtContratPhoneCreateP.Value;
            string msg_address = txtContratAddrCreateP.Value;
            DateTime msg_on_date = DateTime.ParseExact(dateContratCreatePage.Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string newFileName = opMapSelect.Value == "แนบไปพร้อมกับเอกสารแล้ว"?"แนบแผนที่":"";
            if (FileUploadMap.HasFile) {
               newFileName = Path.Combine(Path.GetDirectoryName(FileUploadMap.PostedFile.FileName)
                           , string.Concat(Path.GetFileNameWithoutExtension(FileUploadMap.PostedFile.FileName)
                           , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                           , Path.GetExtension(FileUploadMap.PostedFile.FileName)));
            }

            string msg_map = newFileName;

            //string msg_msg_name = txtMesNameCreatePage.Value;
            string msg_msg_name = sleMes.Value;
            string msg_remark = txtRemarkCreatPage.Value;
            string msg_status = "รอปล่อยงาน";

            UploadFile(FileUploadMap, msg_map);
            mesRes.InsertMessager(msg_date, msg_by, msg_section, msg_phone, msg_send, msg_receive,msg_doctype,msg_priority_normal,msg_priority_urgent,msg_contact_name,msg_address,msg_telephone,msg_map,msg_on_date,msg_msg_name,msg_remark, msg_status);
            Response.Redirect("HomePage.aspx");
        }

        protected void txtByCreateP_TextChanged(object sender, EventArgs e) {      
            MessengerRepository mesRes = new MessengerRepository();
            var dept = mesRes.GetUserList().SingleOrDefault(c => c.name == txtByCreateP.Text);        
            if (dept!=null) {
                txtSectionCreateP.Value = dept.department;
            } else {
                txtSectionCreateP.Value = "";
            }
        }
    }
}