using MyLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace NslDbInfo
{
    public partial class NslDbForm : Form
    {
        private BindingSource contactsBindingSource = new BindingSource();
        private ReadOnlyCollection<PropertyInfo> orderedPropertiesContacts = new ContactDO().GetOrderedProperties();

        public NslDbForm()
        {
            this.InitializeComponent();

            this.grvContacts.DataSource = contactsBindingSource;
            this.grvContacts.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular);
        }

        private void NslDbFormLoad(object sender, EventArgs e)
        {
            this.ReloadContacts();
        }

        private void ReloadContacts()
        {
            string error = string.Empty;
            DataTable data = DataBaseHelper.SelectAll(Program.ConnectionString, ContactDO.TableName, ref error);

            if (string.IsNullOrEmpty(error))
            {
                contactsBindingSource.DataSource = data;
            }
            else
            {
                if (contactsBindingSource.DataSource is DataTable)
                {
                    (contactsBindingSource.DataSource as DataTable).Clear();
                }

                MessageBox.Show("Error: " + error);
            }

            foreach (DataGridViewColumn column in this.grvContacts.Columns)
            {
                if (column.Name == nameof(BaseDO.Id))
                {
                    column.Visible = false;
                }
            }
        }

        private void NewMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                new ContactDetailView().ShowDialog();
                this.ReloadContacts();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ReloadMenuItemClick(object sender, EventArgs e)
        {
            this.ReloadContacts();
        }

        private void ContactsDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void ContactsUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = false;

            string error = string.Empty;

            int id = (int)e.Row.Cells[nameof(BaseDO.Id)].Value;

            try
            {
                DataBaseHelper.Delete<ContactDO>(Program.ConnectionString, ContactDO.TableName, id, ref error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while trying to delete a record: " + ex.Message);
                e.Cancel = true;
            }

            if (!string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(error);
                e.Cancel = true;
            }
        }

        private void ImportClick(object sender, EventArgs e)
        {
            try
            {
                if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string file = this.openFileDialog1.FileName;

                    string error = string.Empty;

                    ReadOnlyCollection<PropertyInfo> collection = new ContactDO().GetOrderedProperties();

                    DataTable data = ExcelHelper.Import(file, collection, ref error);

                    this.ShowErrorIfNotEmpty(error);

                    error = string.Empty;
                    List<ContactDO> contacts = DomainObjectHelper.GetDomainObjectFromDataTable<ContactDO>(data, ref error);

                    this.ShowErrorIfNotEmpty(error);

                    foreach (ContactDO record in contacts)
                    {
                        error = string.Empty;
                        DataBaseHelper.Insert(Program.ConnectionString, ContactDO.TableName, record, ref error);
                        this.ShowErrorIfNotEmpty(error);
                    }

                    this.ReloadContacts();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while trying to import data: " + ex.Message);
            }
        }

        private void ExportClick(object sender, EventArgs e)
        {
            try
            {
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    List<ContactDO> records = new List<ContactDO>();

                    string fileName = this.saveFileDialog1.FileName;

                    DataGridViewSelectedRowCollection rows = this.grvContacts.SelectedRows;

                    string error = string.Empty;

                    foreach (DataGridViewRow item in rows)
                    {
                        error = string.Empty;

                        ContactDO contact = DataBaseHelper.ConvertDataRow<ContactDO>(item, ref error);

                        this.ShowErrorIfNotEmpty(error);

                        if (string.IsNullOrWhiteSpace(error))
                        {
                            records.Add(contact);
                        }
                    }

                    ExcelHelper.Export<ContactDO>(records, fileName, ref error);
                    this.ShowErrorIfNotEmpty(error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while trying to export data: " + ex.Message);
            }
        }

        private void SearchClick(object sender, EventArgs e)
        {
            string searchString = this.txtSearch.Text;

            DataGridViewRowCollection rows = this.grvContacts.Rows;

            foreach (DataGridViewRow row in rows)
            {
                string rowString = string.Empty;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    Type type = cell.GetType();

                    if (cell.ValueType != typeof(Image) || cell.ValueType != typeof(byte[]))
                    {
                        rowString += cell.Value.ToString();
                    }
                }

                CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[this.grvContacts.DataSource];
                currencyManager1.SuspendBinding();
                row.Visible = rowString.Contains(searchString);
                currencyManager1.ResumeBinding();
            }
        }

        private void ContactsMouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.grvContacts.SelectedRows.Count == 1)
                {
                    string error = string.Empty;
                    ContactDO contact = DataBaseHelper.ConvertDataRow<ContactDO>(this.grvContacts.SelectedRows[0], ref error);

                    if (string.IsNullOrWhiteSpace(error))
                    {
                        new ContactDetailView(contact).ShowDialog();
                        this.ReloadContacts();
                    }
                    else
                    {
                        MessageBox.Show(error);
                    }
                }
                else if (this.grvContacts.SelectedRows.Count > 0)
                {
                    MessageBox.Show("More than one row selected for edit");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error happened while editing row: " + ex.Message);
            }
        }

        private void ShowErrorIfNotEmpty(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(error);
            }
        }
    }
}