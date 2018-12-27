using MyLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection;

namespace NslDbInfo
{
    public class ContactDO : BaseDO
    {
        public const string TableName = "Contact";

        public ContactDO()
        {
            this.DateOfJoining = DateTime.Today;
            this.DateOfBirth = DateTime.Today;
        }

        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfJoining { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public string NidNumber { get; set; }
        public string PassPortNumber { get; set; }
        public string CurrentAllocation { get; set; }
        public string PhoneNumber { get; set; }
        public Image Photo { get; set; }
        public Image NidScan { get; set; }
        public Image PassportScan { get; set; }

        protected override ReadOnlyCollection<PropertyInfo> GetOrderedPropertiesInternal()
        {
            List<PropertyInfo> list = new List<PropertyInfo>();

            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.IdNumber), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.Name), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.Designation), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.DateOfJoining), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.DateOfBirth), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.BloodGroup), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.NidNumber), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.PassPortNumber), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.CurrentAllocation), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.PhoneNumber), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.Photo), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.NidScan), list);
            DomainObjectHelper.AddProperty<ContactDO>(nameof(this.PassportScan), list);

            return new ReadOnlyCollection<PropertyInfo>(list);
        }
    }
}