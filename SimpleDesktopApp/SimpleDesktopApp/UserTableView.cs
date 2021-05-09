using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimpleDesktopApp
{
    public partial class UserTableView : Form
    {
        List<User> lstUsers = new List<User>();

        public UserTableView()
        {
            InitializeComponent();
        }

        private void User_Table_View_Load(object sender, EventArgs e)
        {
            // Try to load what's already in the XML file.
            LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Printing all entries");
            // Save the data into the XML file.

            foreach (DataGridViewRow row in dgvDataTable.Rows)
            {
                if (row.DataBoundItem != null)
                {
                    lstUsers.Add(row.DataBoundItem as User);
                }
            }
            SaveData();
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
                    break;
            }
        }

        private void LoadData()
        {
            // Clear current DataGridView and load data from XML file.
            lstUsers = XMLHandler.LoadData();
            dgvDataTable.Rows.Clear();
            BindingSource bs = userBindingSource;

            lstUsers.ForEach(u => bs.Add(u));

            DisableControls();
        }

        private void SaveData()
        {
            XMLHandler.SaveData(lstUsers);
            DisableControls();
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

            string errorMessage = UserController.ValidateField(headerText, e.FormattedValue.ToString());
            
            if(errorMessage != null)
            {
                dgvDataTable.Rows[e.RowIndex].ErrorText = errorMessage;
            }
        }

        private void dgvDataTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row from error.
            dgvDataTable.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void dgvDataTable_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            EnableControls();
        }

        private void dgvDataTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(dgvDataTable.Rows[e.RowIndex].ErrorText, $"{dgvDataTable.Columns[e.ColumnIndex].HeaderText} is invalid");

        }
    }
}
