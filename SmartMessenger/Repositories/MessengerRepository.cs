using SmartMessenger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartMessenger.Repositories
{
    public class MessengerRepository
    {
        public List<msgctrlDev> GetMessagerList()
        {
            using (SFAEntities en = new SFAEntities()) {
                var result = en.msgctrlDevs.ToList();
                return result;
            }
        }

        public msgctrlDev GetMessagerByID(int ID)
        {
            using (SFAEntities en = new SFAEntities())
            {
                msgctrlDev result = en.msgctrlDevs.Single(a=>a.msg_id==ID);
                return result;
            }
        }

        public void InsertMessager(DateTime mDate, string mBy, string mSection, string mPhone, string mSend, string mRecieve, string mDocType, string isNormale, string isUrgent, string mContractName, string mAddress, string mTelephone, string mMAP, DateTime mOndate, string mMesName,string mRemark, string mStatus)
        {
            using (SFAEntities en = new SFAEntities())
            {
                msgctrlDev mes = new msgctrlDev();
                mes.msg_date = mDate;
                mes.msg_by = mBy;
                mes.msg_section = mSection;
                mes.msg_phone = mPhone;
                mes.msg_send = mSend;
                mes.msg_receive = mRecieve;
                mes.msg_doctype = mDocType;
                mes.msg_priority_normal = isNormale;
                mes.msg_priority_urgent = isUrgent;
                mes.msg_contact_name = mContractName;
                mes.msg_telephone = mTelephone;
                mes.msg_address = mAddress;
                mes.msg_on_date = mOndate;
                mes.msg_map = mMAP;
                mes.msg_msg_name = mMesName;
                mes.msg_remark = mRemark;
                mes.msg_close_status = mStatus;
                
                en.msgctrlDevs.Add(mes);
                en.SaveChanges();
            }

        }

        public void UpdateMessager(int id, string mBy, string mSection, string mPhone, string mSend, string mRecieve, string mDocType, string isNormale, string isUrgent, string mContractName, string mAddress, string mTelephone, string mMAP, DateTime mOndate, string mMesName, string mRemark, string mStatus)
        {
            using (SFAEntities en = new SFAEntities())
            {
                msgctrlDev mes = en.msgctrlDevs.Single(c=>c.msg_id==id);

                mes.msg_by = mBy;
                mes.msg_section = mSection;
                mes.msg_phone = mPhone;
                mes.msg_send = mSend;
                mes.msg_receive = mRecieve;
                mes.msg_doctype = mDocType;
                mes.msg_priority_normal = isNormale;
                mes.msg_priority_urgent = isUrgent;
                mes.msg_contact_name = mContractName;
                mes.msg_telephone = mTelephone;
                mes.msg_address = mAddress;
                mes.msg_on_date = mOndate;
                if (mMAP != "") {
                    mes.msg_map = mMAP;
                }
                mes.msg_msg_name = mMesName;
                mes.msg_remark = mRemark;             

                en.SaveChanges();
            }

        }
    }
}