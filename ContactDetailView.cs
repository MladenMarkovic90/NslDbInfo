using MyLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NslDbInfo
{
    public partial class ContactDetailView : Form
    {
        private ContactDO record;

        public ContactDetailView(ContactDO record = null)
        {
            this.InitializeComponent();
            this.record = record ?? new ContactDO();
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            string error = string.Empty;

            record.IdNumber = this.txtIdNumber.Text;
            record.Name = this.txtName.Text;
            record.Designation = this.txtDesignation.Text;
            record.DateOfJoining = this.datDateOfJoining.Value;
            record.DateOfBirth = this.datDateOfBirth.Value;
            record.BloodGroup = this.txtBloodGroup.Text;
            record.NidNumber = this.txtNidNumber.Text;
            record.PassPortNumber = this.txtPassPortNumber.Text;
            record.CurrentAllocation = this.txtCurrentAllocation.Text;
            record.PhoneNumber = this.txtPhoneNumber.Text;
            record.Photo = this.picPhoto.Image;
            record.NidScan = this.picNIDScan.Image;
            record.PassportScan = this.picPassportScan.Image;

            DataBaseHelper.Save(Program.ConnectionString, ContactDO.TableName, record, ref error);

            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show("Error: " + error);
            }

            this.Close();
        }

        private void ContactDetailView_Load(object sender, EventArgs e)
        {
            this.txtIdNumber.Text = record.IdNumber;
            this.txtName.Text = record.Name;
            this.txtDesignation.Text = record.Designation;
            this.datDateOfJoining.Value = record.DateOfJoining;
            this.datDateOfBirth.Value = record.DateOfBirth;
            this.txtBloodGroup.Text = record.BloodGroup;
            this.txtNidNumber.Text = record.NidNumber;
            this.txtPassPortNumber.Text = record.PassPortNumber;
            this.txtCurrentAllocation.Text = record.CurrentAllocation;
            this.txtPhoneNumber.Text = record.PhoneNumber;
            this.picPhoto.Image = record.Photo;
            this.picNIDScan.Image = record.NidScan;
            this.picPassportScan.Image = record.PassportScan;
        }

        private void ChangePictureMenuItemClick(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(this.openFileDialog1.FileName);

                if (this.tabControl1.SelectedTab.Contains(this.picPhoto))
                {
                    this.picPhoto.Image = image;
                }
                else if (this.tabControl1.SelectedTab.Contains(this.picNIDScan))
                {
                    this.picNIDScan.Image = image;
                }
                else if (this.tabControl1.SelectedTab.Contains(this.picPassportScan))
                {
                    this.picPassportScan.Image = image;
                }
            }
        }

        private void RemovePictureMenuItemClick(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Contains(this.picPhoto))
            {
                this.picPhoto.Image = null;
            }
            else if (this.tabControl1.SelectedTab.Contains(this.picNIDScan))
            {
                this.picNIDScan.Image = null;
            }
            else if (this.tabControl1.SelectedTab.Contains(this.picPassportScan))
            {
                this.picPassportScan.Image = null;
            }
        }
    }
}