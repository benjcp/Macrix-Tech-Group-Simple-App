using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void EnableControls()
        {
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void dgvDataTable_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Validate the all columns

        }

        private void dgvDataTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvDataTable.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void dgvDataTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            EnableControls();
        }
    }
}
