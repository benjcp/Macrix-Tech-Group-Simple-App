using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleDesktopApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Try to load what's already in the XML file.
            LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Printing all entries");
            // Save the data into the XML file.

            List<User> lstUsers = new List<User>();
            foreach (DataGridViewRow row in dgvDataTable.Rows)
            {
                Console.WriteLine(row.DataBoundItem);
                if (row.DataBoundItem != null)
                {
                    lstUsers.Add(row.DataBoundItem as User);
                }
            }

            try
            {
                XMLHandler.SaveData(lstUsers);
                DisableControls();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Reload the data in the DataTable.
            DialogResult dr = MessageBox.Show("Are you sure you want to discard all changes?", "Discard all changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            switch (dr)
            {
                case DialogResult.Yes:
                    LoadData();
                    break;

                case DialogResult.No:
                    // Do nothing.
                    break;
            }
        }

        private void LoadData()
        {
            DisableControls();
            // Clear current DataGridView and load data from XML file.
            List<User> users = XMLHandler.LoadData();
            dgvDataTable.Rows.Clear();
            BindingSource bs = userBindingSource;

            foreach (User u in users)
            {
                bs.Add(u);
            }
        }

        private void DisableControls()
        {
            // Disables controls when there is no data to edit.
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void EnableControls()
        {
            // Enables controls when the user has edited data.
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void dgvDataTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            EnableControls();
        }

        private void dgvDataTable_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Validate all columns
            string headerText = dgvDataTable.Columns[e.ColumnIndex].HeaderText;

            switch (headerText)
            {
                case "FirstName":
                case "LastName":
                case "StreetName":
                case "Town":
                case "PostalCode":
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        dgvDataTable.Rows[e.RowIndex].ErrorText =
                            "Value must not be empty";
                        e.Cancel = true;
                    }
                    break;

                case "ApartmentNumber":
                    string number = e.FormattedValue.ToString();
                    if (int.TryParse(number, out int result))
                    {
                        if(result < 0 && result > 999999)
                        {
                            dgvDataTable.Rows[e.RowIndex].ErrorText =
                            "Value must not be less than 1 or more than 999999";
                            e.Cancel = true;
                        }
                    }
                    break;

                case "PhoneNumber":
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()) || e.FormattedValue.ToString().Length > 15)
                    {
                        dgvDataTable.Rows[e.RowIndex].ErrorText =
                            "Value must not be empty or more than 15 characters";
                        e.Cancel = true;
                    }
                    break;

                case "DateOfBirth":
                    if (DateTime.TryParse(e.FormattedValue.ToString(), out DateTime temp))
                    {
                        if(temp > DateTime.Today)
                        {
                            dgvDataTable.Rows[e.RowIndex].ErrorText =
                            "Please enter a date that is in the past.";
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        dgvDataTable.Rows[e.RowIndex].ErrorText =
                            "Please enter a valid DateTime.";
                        e.Cancel = true;
                    }

                    break;
            }
        }

        private void dgvDataTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row from error.
            dgvDataTable.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void dgvDataTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dgvDataTable.Rows[e.RowIndex].ErrorText =
                            "Please Enter a valid value.";
            e.Cancel = true;
        }

    }
}
