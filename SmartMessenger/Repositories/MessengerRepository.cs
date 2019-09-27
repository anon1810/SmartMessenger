using SmartMessenger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartMessenger.Repositories
{
    public class MessengerRepository
    {
        public List<msgctrl> GetMessagerList()
        {
            using (MessengerDataClassesDataContext en = new MessengerDataClassesDataContext())
            {
                var result = en.msgctrls.ToList();
                return result;
            }
        }

        public void InsertMessager(string mID, string mDate, string mBy, string mSection, string mPhone, string mContractName, string mCompany, string mAddress, string mTelephone, string mSend, string mDocType)
        {
            using (MessengerDataClassesDataContext en = new MessengerDataClassesDataContext())
            {
                //msgctrl mes = new msgctrl();
                //mes.msg_id = mID;
                //mes.msg_date = DateTime.ParseExact(mDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //mes.mBy = mBy;
                //mes.mSection = mSection;
                //mes.mPhone = mPhone;
                //mes.mContractName = mContractName;
                //mes.mCompany = mCompany;
                //mes.mAddress = mAddress;
                //mes.mTelephone = mTelephone;
                //mes.mSend = mSend == "Yes" ? true : false;
                //mes.mDocType = mDocType;

                //en.Messengers.AddObject(mes);
                //en.SaveChanges();
            }

        }
    }
}