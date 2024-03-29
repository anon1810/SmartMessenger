﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartMessenger.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SFA")]
	public partial class MessengerDataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertmsgctrl(msgctrl instance);
    partial void Updatemsgctrl(msgctrl instance);
    partial void Deletemsgctrl(msgctrl instance);
    #endregion
		
		public MessengerDataClassesDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SFAConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MessengerDataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MessengerDataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MessengerDataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MessengerDataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<msgctrl> msgctrls
		{
			get
			{
				return this.GetTable<msgctrl>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.msgctrl")]
	public partial class msgctrl : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private decimal _msg_id;
		
		private System.Nullable<System.DateTime> _msg_entry;
		
		private System.Nullable<System.DateTime> _msg_date;
		
		private string _msg_by;
		
		private string _msg_section;
		
		private string _msg_phone;
		
		private string _msg_contact_name;
		
		private string _msg_company;
		
		private string _msg_address;
		
		private string _msg_telephone;
		
		private string _msg_map;
		
		private string _msg_send;
		
		private string _msg_receive;
		
		private string _msg_doctype;
		
		private string _msg_priority_normal;
		
		private string _msg_priority_urgent;
		
		private System.Nullable<System.DateTime> _msg_on_date;
		
		private string _msg_msg_name;
		
		private string _msg_close_status;
		
		private System.Nullable<System.DateTime> _msg_close_date;
		
		private System.Nullable<System.DateTime> _msg_close_time;
		
		private string _msg_remark;
		
		private string _msg_print_label;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onmsg_idChanging(decimal value);
    partial void Onmsg_idChanged();
    partial void Onmsg_entryChanging(System.Nullable<System.DateTime> value);
    partial void Onmsg_entryChanged();
    partial void Onmsg_dateChanging(System.Nullable<System.DateTime> value);
    partial void Onmsg_dateChanged();
    partial void Onmsg_byChanging(string value);
    partial void Onmsg_byChanged();
    partial void Onmsg_sectionChanging(string value);
    partial void Onmsg_sectionChanged();
    partial void Onmsg_phoneChanging(string value);
    partial void Onmsg_phoneChanged();
    partial void Onmsg_contact_nameChanging(string value);
    partial void Onmsg_contact_nameChanged();
    partial void Onmsg_companyChanging(string value);
    partial void Onmsg_companyChanged();
    partial void Onmsg_addressChanging(string value);
    partial void Onmsg_addressChanged();
    partial void Onmsg_telephoneChanging(string value);
    partial void Onmsg_telephoneChanged();
    partial void Onmsg_mapChanging(string value);
    partial void Onmsg_mapChanged();
    partial void Onmsg_sendChanging(string value);
    partial void Onmsg_sendChanged();
    partial void Onmsg_receiveChanging(string value);
    partial void Onmsg_receiveChanged();
    partial void Onmsg_doctypeChanging(string value);
    partial void Onmsg_doctypeChanged();
    partial void Onmsg_priority_normalChanging(string value);
    partial void Onmsg_priority_normalChanged();
    partial void Onmsg_priority_urgentChanging(string value);
    partial void Onmsg_priority_urgentChanged();
    partial void Onmsg_on_dateChanging(System.Nullable<System.DateTime> value);
    partial void Onmsg_on_dateChanged();
    partial void Onmsg_msg_nameChanging(string value);
    partial void Onmsg_msg_nameChanged();
    partial void Onmsg_close_statusChanging(string value);
    partial void Onmsg_close_statusChanged();
    partial void Onmsg_close_dateChanging(System.Nullable<System.DateTime> value);
    partial void Onmsg_close_dateChanged();
    partial void Onmsg_close_timeChanging(System.Nullable<System.DateTime> value);
    partial void Onmsg_close_timeChanged();
    partial void Onmsg_remarkChanging(string value);
    partial void Onmsg_remarkChanged();
    partial void Onmsg_print_labelChanging(string value);
    partial void Onmsg_print_labelChanged();
    #endregion
		
		public msgctrl()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_id", AutoSync=AutoSync.OnInsert, DbType="Decimal(18,0) NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public decimal msg_id
		{
			get
			{
				return this._msg_id;
			}
			set
			{
				if ((this._msg_id != value))
				{
					this.Onmsg_idChanging(value);
					this.SendPropertyChanging();
					this._msg_id = value;
					this.SendPropertyChanged("msg_id");
					this.Onmsg_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_entry", DbType="DateTime")]
		public System.Nullable<System.DateTime> msg_entry
		{
			get
			{
				return this._msg_entry;
			}
			set
			{
				if ((this._msg_entry != value))
				{
					this.Onmsg_entryChanging(value);
					this.SendPropertyChanging();
					this._msg_entry = value;
					this.SendPropertyChanged("msg_entry");
					this.Onmsg_entryChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_date", DbType="DateTime")]
		public System.Nullable<System.DateTime> msg_date
		{
			get
			{
				return this._msg_date;
			}
			set
			{
				if ((this._msg_date != value))
				{
					this.Onmsg_dateChanging(value);
					this.SendPropertyChanging();
					this._msg_date = value;
					this.SendPropertyChanged("msg_date");
					this.Onmsg_dateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_by", DbType="VarChar(10)")]
		public string msg_by
		{
			get
			{
				return this._msg_by;
			}
			set
			{
				if ((this._msg_by != value))
				{
					this.Onmsg_byChanging(value);
					this.SendPropertyChanging();
					this._msg_by = value;
					this.SendPropertyChanged("msg_by");
					this.Onmsg_byChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_section", DbType="VarChar(20)")]
		public string msg_section
		{
			get
			{
				return this._msg_section;
			}
			set
			{
				if ((this._msg_section != value))
				{
					this.Onmsg_sectionChanging(value);
					this.SendPropertyChanging();
					this._msg_section = value;
					this.SendPropertyChanged("msg_section");
					this.Onmsg_sectionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_phone", DbType="VarChar(50)")]
		public string msg_phone
		{
			get
			{
				return this._msg_phone;
			}
			set
			{
				if ((this._msg_phone != value))
				{
					this.Onmsg_phoneChanging(value);
					this.SendPropertyChanging();
					this._msg_phone = value;
					this.SendPropertyChanged("msg_phone");
					this.Onmsg_phoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_contact_name", DbType="VarChar(200)")]
		public string msg_contact_name
		{
			get
			{
				return this._msg_contact_name;
			}
			set
			{
				if ((this._msg_contact_name != value))
				{
					this.Onmsg_contact_nameChanging(value);
					this.SendPropertyChanging();
					this._msg_contact_name = value;
					this.SendPropertyChanged("msg_contact_name");
					this.Onmsg_contact_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_company", DbType="VarChar(200)")]
		public string msg_company
		{
			get
			{
				return this._msg_company;
			}
			set
			{
				if ((this._msg_company != value))
				{
					this.Onmsg_companyChanging(value);
					this.SendPropertyChanging();
					this._msg_company = value;
					this.SendPropertyChanged("msg_company");
					this.Onmsg_companyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_address", DbType="VarChar(200)")]
		public string msg_address
		{
			get
			{
				return this._msg_address;
			}
			set
			{
				if ((this._msg_address != value))
				{
					this.Onmsg_addressChanging(value);
					this.SendPropertyChanging();
					this._msg_address = value;
					this.SendPropertyChanged("msg_address");
					this.Onmsg_addressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_telephone", DbType="VarChar(50)")]
		public string msg_telephone
		{
			get
			{
				return this._msg_telephone;
			}
			set
			{
				if ((this._msg_telephone != value))
				{
					this.Onmsg_telephoneChanging(value);
					this.SendPropertyChanging();
					this._msg_telephone = value;
					this.SendPropertyChanged("msg_telephone");
					this.Onmsg_telephoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_map", DbType="VarChar(50)")]
		public string msg_map
		{
			get
			{
				return this._msg_map;
			}
			set
			{
				if ((this._msg_map != value))
				{
					this.Onmsg_mapChanging(value);
					this.SendPropertyChanging();
					this._msg_map = value;
					this.SendPropertyChanged("msg_map");
					this.Onmsg_mapChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_send", DbType="VarChar(3)")]
		public string msg_send
		{
			get
			{
				return this._msg_send;
			}
			set
			{
				if ((this._msg_send != value))
				{
					this.Onmsg_sendChanging(value);
					this.SendPropertyChanging();
					this._msg_send = value;
					this.SendPropertyChanged("msg_send");
					this.Onmsg_sendChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_receive", DbType="VarChar(3)")]
		public string msg_receive
		{
			get
			{
				return this._msg_receive;
			}
			set
			{
				if ((this._msg_receive != value))
				{
					this.Onmsg_receiveChanging(value);
					this.SendPropertyChanging();
					this._msg_receive = value;
					this.SendPropertyChanged("msg_receive");
					this.Onmsg_receiveChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_doctype", DbType="VarChar(200)")]
		public string msg_doctype
		{
			get
			{
				return this._msg_doctype;
			}
			set
			{
				if ((this._msg_doctype != value))
				{
					this.Onmsg_doctypeChanging(value);
					this.SendPropertyChanging();
					this._msg_doctype = value;
					this.SendPropertyChanged("msg_doctype");
					this.Onmsg_doctypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_priority_normal", DbType="VarChar(10)")]
		public string msg_priority_normal
		{
			get
			{
				return this._msg_priority_normal;
			}
			set
			{
				if ((this._msg_priority_normal != value))
				{
					this.Onmsg_priority_normalChanging(value);
					this.SendPropertyChanging();
					this._msg_priority_normal = value;
					this.SendPropertyChanged("msg_priority_normal");
					this.Onmsg_priority_normalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_priority_urgent", DbType="VarChar(10)")]
		public string msg_priority_urgent
		{
			get
			{
				return this._msg_priority_urgent;
			}
			set
			{
				if ((this._msg_priority_urgent != value))
				{
					this.Onmsg_priority_urgentChanging(value);
					this.SendPropertyChanging();
					this._msg_priority_urgent = value;
					this.SendPropertyChanged("msg_priority_urgent");
					this.Onmsg_priority_urgentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_on_date", DbType="DateTime")]
		public System.Nullable<System.DateTime> msg_on_date
		{
			get
			{
				return this._msg_on_date;
			}
			set
			{
				if ((this._msg_on_date != value))
				{
					this.Onmsg_on_dateChanging(value);
					this.SendPropertyChanging();
					this._msg_on_date = value;
					this.SendPropertyChanged("msg_on_date");
					this.Onmsg_on_dateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_msg_name", DbType="VarChar(50)")]
		public string msg_msg_name
		{
			get
			{
				return this._msg_msg_name;
			}
			set
			{
				if ((this._msg_msg_name != value))
				{
					this.Onmsg_msg_nameChanging(value);
					this.SendPropertyChanging();
					this._msg_msg_name = value;
					this.SendPropertyChanged("msg_msg_name");
					this.Onmsg_msg_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_close_status", DbType="VarChar(50)")]
		public string msg_close_status
		{
			get
			{
				return this._msg_close_status;
			}
			set
			{
				if ((this._msg_close_status != value))
				{
					this.Onmsg_close_statusChanging(value);
					this.SendPropertyChanging();
					this._msg_close_status = value;
					this.SendPropertyChanged("msg_close_status");
					this.Onmsg_close_statusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_close_date", DbType="DateTime")]
		public System.Nullable<System.DateTime> msg_close_date
		{
			get
			{
				return this._msg_close_date;
			}
			set
			{
				if ((this._msg_close_date != value))
				{
					this.Onmsg_close_dateChanging(value);
					this.SendPropertyChanging();
					this._msg_close_date = value;
					this.SendPropertyChanged("msg_close_date");
					this.Onmsg_close_dateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_close_time", DbType="DateTime")]
		public System.Nullable<System.DateTime> msg_close_time
		{
			get
			{
				return this._msg_close_time;
			}
			set
			{
				if ((this._msg_close_time != value))
				{
					this.Onmsg_close_timeChanging(value);
					this.SendPropertyChanging();
					this._msg_close_time = value;
					this.SendPropertyChanged("msg_close_time");
					this.Onmsg_close_timeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_remark", DbType="VarChar(255)")]
		public string msg_remark
		{
			get
			{
				return this._msg_remark;
			}
			set
			{
				if ((this._msg_remark != value))
				{
					this.Onmsg_remarkChanging(value);
					this.SendPropertyChanging();
					this._msg_remark = value;
					this.SendPropertyChanged("msg_remark");
					this.Onmsg_remarkChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_msg_print_label", DbType="VarChar(1)")]
		public string msg_print_label
		{
			get
			{
				return this._msg_print_label;
			}
			set
			{
				if ((this._msg_print_label != value))
				{
					this.Onmsg_print_labelChanging(value);
					this.SendPropertyChanging();
					this._msg_print_label = value;
					this.SendPropertyChanged("msg_print_label");
					this.Onmsg_print_labelChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
