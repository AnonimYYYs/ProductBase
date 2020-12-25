using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void SetBaseName(string name)
        {
            label_base_name.Text = $"Current base is: /{name}/";
        }

        public void SetData(List<List<string>> data)
        {
            data_grid_view.Rows.Clear();
            data_grid_view.Columns.Clear();
            data_grid_view.Refresh();
            data_grid_view.AllowUserToAddRows = false;

            for(int i = 0; i < data[0].Count; i++)
            {
                var column = new DataGridViewColumn();
                column.HeaderText = data[0][i];
                column.ReadOnly = true;
                column.Frozen = false;
                column.Width = 100;
                column.CellTemplate = new DataGridViewTextBoxCell();
                data_grid_view.Columns.Add(column);
            }

            for(int i = 1; i < data.Count; i++)
            {
                data_grid_view.Rows.Add(data[i].ToArray());
            }

        }

        private void button_action_Click(object sender, EventArgs e)
        {

            bool r = UserLogic.query(out List<List<string>> dBase, out string name, out string result, textbox_input.Text);
            if (r)
            {
                SetBaseName(name);
                if(dBase != null)
                {
                    SetData(dBase);
                }
            }

            label_result.Text = result;
           //textbox_input.Text = "";
        }
    }
}
