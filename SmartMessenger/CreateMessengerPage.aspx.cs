﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartMessenger.Repositories;

namespace SmartMessenger
{
    public partial class CreateMessengerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            MessengerRepository mesRes = new MessengerRepository();
            DateTime msg_date = DateTime.Now;
            string msg_by = txtByCreateP.Value;
            string msg_section = txtSectionCreateP.Value;
            string msg_phone = txtByPhoneCreateP.Value;
            string msg_send="";
            string msg_receive="";
            string msg_doctype="";
            if (opSendCreateP.Value != "0") {
                msg_send = "Yes";
                msg_doctype += "ส่ง:" + opSendCreateP.Value + txtSendCreateP.Value+" ";
            }
            if (opReceiveCreateP.Value != "0") {
                msg_receive = "Yes";
                msg_doctype += "รับ:" + opReceiveCreateP.Value + txtReceiveCreateP.Value;
            }

            string msg_priority_normal="";
            string msg_priority_urgent="";
            if (nmCreateP.Checked) {
                msg_priority_normal = "Yes";
            } else {
                msg_priority_urgent = "Yes";
            }
               
            string msg_contact_name = txtContratNameCreateP.Value;
            string msg_telephone = txtContratPhoneCreateP.Value;
            string msg_address = txtContratAddrCreateP.Value;
            DateTime msg_on_date = DateTime.ParseExact(dateContratCreatePage.Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var msg_map = fileContratMapCreateP.Value;
            string msg_msg_name = txtMesNameCreatePage.Value;
            string msg_remark = txtRemarkCreatPage.Value;
            mesRes.InsertMessager(msg_date, msg_by, msg_section, msg_phone, msg_send, msg_receive,msg_doctype,msg_priority_normal,msg_priority_urgent,msg_contact_name,msg_address,msg_telephone,msg_map,msg_on_date,msg_msg_name,msg_remark);
        }
    }
}