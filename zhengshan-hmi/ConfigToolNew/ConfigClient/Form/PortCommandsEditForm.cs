using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Config
{
    public partial class PortCommandsEditForm : Form
    {
        PortCommands m_portCommands;
        PortCommands m_portCommands_default;
        DataTable m_datatable;
        DataTable m_datatable_default;
        object[] m_RowItemArray;

        public PortCommandsEditForm(PortCommands[] portCommands)
        {
            InitializeComponent();
            m_portCommands = portCommands[0];
            m_portCommands_default = portCommands[1];
            m_datatable = m_portCommands.CreateDataTable();
            m_datatable_default = portCommands[1].CreateDataTable();
            if (m_datatable != null)
            {
                DataSet dataset = (DataSet)(dgvPortCommands.DataSource);
                if (dataset != null)
                    dataset.Dispose();
                dataset = new DataSet();
                dataset.Tables.Add(m_datatable);
                dgvPortCommands.DataSource = dataset;
                dgvPortCommands.DataMember = "PortCommands";
            }
            dgvPortCommands.Columns[dgvPortCommands.Columns.Count - 1].Visible = false;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (m_datatable != null)
                m_portCommands.Update(m_datatable);
            Close();
        }
        
        private void btAddRow_Click(object sender, EventArgs e)
        {
            m_datatable.Rows.Add(m_datatable_default.Rows[0].ItemArray);
        }
        
        private void btInsertRow_Click(object sender, EventArgs e)
        {
            if (dgvPortCommands.CurrentCell != null)
            {
                DataRow dataRow = m_datatable.NewRow();
                dataRow.ItemArray = m_datatable_default.Rows[0].ItemArray;;
                m_datatable.Rows.InsertAt(dataRow, dgvPortCommands.CurrentCell.RowIndex);
            }
            else
            {
                m_datatable.Rows.Add(m_datatable_default.Rows[0].ItemArray);
            }
        }

        private void btDeleteRow_Click(object sender, EventArgs e)
        {
            if (dgvPortCommands.CurrentCell != null && dgvPortCommands.CurrentCell.RowIndex >= 0)
            {
                m_datatable.Rows.RemoveAt(dgvPortCommands.CurrentCell.RowIndex);
                if (dgvPortCommands.CurrentCell != null && dgvPortCommands.CurrentCell.RowIndex >= 0)
                {
                    dgvPortCommands.Rows[dgvPortCommands.CurrentCell.RowIndex].Selected = true;
                }
            }
        }

        private void btCopyRow_Click(object sender, EventArgs e)
        {
            if (dgvPortCommands.CurrentCell != null && dgvPortCommands.CurrentCell.RowIndex >= 0)
            {
                m_RowItemArray = m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex].ItemArray;
                dgvPortCommands.Rows[dgvPortCommands.CurrentCell.RowIndex].Selected = true;
            }
        }

        private void btPasteRow_Click(object sender, EventArgs e)
        {
            if (m_RowItemArray != null && dgvPortCommands.CurrentCell!=null)
            {
                DataRow dataRow = m_datatable.NewRow();
                dataRow.ItemArray = m_RowItemArray;

                m_datatable.Rows.InsertAt(dataRow, dgvPortCommands.CurrentCell.RowIndex);
                dgvPortCommands.Rows[dgvPortCommands.CurrentCell.RowIndex].Selected = true;
                dgvPortCommands.CurrentCell = dgvPortCommands.Rows[dgvPortCommands.CurrentCell.RowIndex].Cells[0];
            }
        }
 
        private void btResetDefault_Click(object sender, EventArgs e)
        {
            if (m_portCommands_default != null)
            {
                m_datatable.Rows.Clear();
                for (int i = 0; i < m_datatable_default.Rows.Count; i++)
                {
                    m_datatable.Rows.Add(m_datatable_default.Rows[i].ItemArray);
                }
                /*
                m_datatable = m_portCommands_default.CreateDataTable();
                if (m_datatable != null)
                {
                    DataSet dataset = (DataSet)(dgvPortCommands.DataSource);
                    if (dataset != null)
                        dataset.Dispose();
                    dataset.Tables.Clear();
                    dataset.Tables.Add(m_datatable);
                    dgvPortCommands.DataSource = dataset;
                    dgvPortCommands.DataMember = "PortCommands";
                }
                dgvPortCommands.Show();
                 * */
            }

        }

        private void btMoveUp_Click(object sender, EventArgs e)
        {
            if (dgvPortCommands.CurrentCell != null && dgvPortCommands.CurrentCell.RowIndex > 0)
            {
                object[] _rowData = m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex].ItemArray;

                m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex].ItemArray = m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex - 1].ItemArray;

                m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex - 1].ItemArray = _rowData;
                dgvPortCommands.Rows[dgvPortCommands.CurrentCell.RowIndex - 1].Selected = true;
                dgvPortCommands.CurrentCell = dgvPortCommands.Rows[dgvPortCommands.CurrentCell.RowIndex - 1].Cells[dgvPortCommands.CurrentCell.ColumnIndex];
            }
        }

        private void btMoveDown_Click(object sender, EventArgs e)
        {
            if (dgvPortCommands.CurrentCell != null && dgvPortCommands.CurrentCell.RowIndex < m_datatable.Rows.Count - 1)
            {
                object[] _rowData = m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex].ItemArray;

                m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex].ItemArray = m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex + 1].ItemArray;

                m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex + 1].ItemArray = _rowData;
                dgvPortCommands.Rows[dgvPortCommands.CurrentCell.RowIndex + 1].Selected = true;
                dgvPortCommands.CurrentCell = dgvPortCommands.Rows[dgvPortCommands.CurrentCell.RowIndex + 1].Cells[dgvPortCommands.CurrentCell.ColumnIndex];
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvPortCommands.CurrentCell != null && dgvPortCommands.CurrentCell.RowIndex >=00)
            {
                int j;
                string field_name = "[" + m_portCommands.GetName() + " Column: " + dgvPortCommands.CurrentCell.RowIndex + "]";
                PortConfig portConfig = null;// = (PortConfig)m_portCommands.m_ItemArray[dgvPortCommands.CurrentCell.RowIndex];
                string temp = "    ";
                string[] item_value = new string[m_datatable.Columns.Count];
                for (int i = 0; i < m_datatable.Columns.Count; i++)
                {
                    temp += m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex].ItemArray[i].ToString() + "  ";
                }
                portConfig = new PortConfig(field_name, temp, m_portCommands.m_type, m_portCommands.m_nodeList);

                PortConfig[] tmpPortConfig = new PortConfig[]{portConfig,
                                                                ((PortConfig)m_portCommands_default.m_ItemArray[0])};

                PortConfigEditForm frm = new PortConfigEditForm(tmpPortConfig);

                try
                {
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        //DataTable dataTable = portConfig.CreateDataTable();

                        for (j = 0; j < portConfig.m_value.Length/*dataTable.Rows.Count*/; j++)
                        {
                            m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex][j] = portConfig.m_value[j];//dataTable.Rows[j][1];
                        }
                        if (portConfig.m_comment.Length > 0)
                        {
                            m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex][j] = '#' + portConfig.m_comment;
                        }
                        else
                        {
                            m_datatable.Rows[dgvPortCommands.CurrentCell.RowIndex][j] = "";
                        }
                    }

                }
                finally
                {
                    frm.Dispose();
                }

            }
        }

        private void dgvPortCommands_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPortCommands[e.ColumnIndex, e.RowIndex].Value!=null && dgvPortCommands[e.ColumnIndex, e.RowIndex].Value.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Can not be empty!");
            }
        }

        private void dgvPortCommands_DoubleClick(object sender, EventArgs e)
        {
            btEdit_Click(sender, e);
        }

        private void dgvPortCommands_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
