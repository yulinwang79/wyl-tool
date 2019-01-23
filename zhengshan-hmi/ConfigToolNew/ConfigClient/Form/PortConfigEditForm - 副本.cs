using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Config
{

    // Create a DataSet with two tables and populate it.
    public partial class PortConfigEditForm : Form
    {
            
        private PortConfig m_portConfig;
        private PortConfig m_portConfig_default;
        private DataTable m_datatable;
        private DataTable m_datatable_default;

        public PortConfigEditForm(PortConfig[] portConfig )
        {
            InitializeComponent();
            //m_xmlDoc = new XmlDocument();
            //m_xmlDoc.LoadXml(Config.Resource.Description);
            //string nodeString = "PortConfig";
            //XmlNodeList nodeList;
            m_portConfig = portConfig[0];
            m_portConfig_default = portConfig[1];
            m_datatable = m_portConfig.CreateDataTable();
            m_datatable_default = portConfig[1].CreateDataTable();
            this.Text = m_portConfig.GetName();
            tbComment.Text = m_portConfig.m_comment;
            if(m_datatable != null)
            {
	            DataSet dataset = new DataSet();
	            dataset.Tables.Add(m_datatable);
	            dgvPortConfig.DataSource = dataset;
	            dgvPortConfig.DataMember = "PortConfig";
                //tbComment.Text = dgvPortConfig.CurrentRow.Cells[2].Value;
                //dgvPortConfig.Columns[dgvPortConfig.Columns.Count - 1].Visible = false;
            }
            /*
            switch(m_portConfig.m_type)
            {
                case Config.EditType.EDIT_PORT_CONFIG:
                    nodeString = "PortConfig";
                    break;
                case Config.EditType.EDIT_PORT_COMMAND:
                    nodeString = "PortCommands";
                    break;
                case Config.EditType.EDIT_PCPORT_CONFIG:
                    nodeString = "PCPortConfig";
                    break;
                case Config.EditType.EDIT_PCPORT_COMMAND:
                    nodeString = "PCPortCommands";
                    break;
                case Config.EditType.EDIT_ETHERNET_CONFIG:
                    nodeString = "EthernetConfig";
                    break;
                case Config.EditType.EDIT_ETHERNET_CLIENT_CONFIG:
                    nodeString = "EthernetClientConfig";
                    break;
                case Config.EditType.EDIT_ETHERNET_CLIENT_COMMAND:
                    nodeString = "EthernetClientCommands";
                    break;
                case Config.EditType.EDIT_ETHERNET_SERVER_CONFIG:
                    nodeString = "EthernetServerConfig";
                    break;
                case Config.EditType.EDIT_DATA_REMAP:
                    nodeString = "DataRemap";
                    break;
                default:
                    break;
            }
            nodeList = m_xmlDoc.SelectSingleNode("modbus").ChildNodes;
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                if (xe.Name == nodeString)//如果name属性值为tbCfgName.Text
                {
                    m_nodeList = xe.ChildNodes;
                    break;
                }
            }
*/
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvPortConfig_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgvPortConfig.Rows[dgvPortConfig.CurrentCell.RowIndex].Selected = true;

         }

        private void cbCfgValue_SelectedIndexChanged(object sender, EventArgs e)
        {
 //           dgvPortConfig.CurrentRow.Cells[1].Value = cbCfgValue.Text;
        }

        private void cbCfgValue_SelectedValueChanged(object sender, EventArgs e)
        {
            dgvPortConfig.CurrentRow.Cells[1].Value = cbCfgValue.Text;

        }
        
        private void tbCfgValue_TextChanged(object sender, EventArgs e)
        {
            dgvPortConfig.CurrentRow.Cells[1].Value = tbCfgValue.Text;

        }

        private void dgvPortConfig_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            string contentText = null;
            tbCfgName.Text = (string)dgvPortConfig.CurrentRow.Cells[0].Value;
            if (m_portConfig.m_nodeList != null)
            {
                foreach (XmlNode xn in m_portConfig.m_nodeList)
                {
                    XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                    if (xe.GetAttribute("name") == tbCfgName.Text)//如果name属性值为tbCfgName.Text
                    {
                        tbDescription.Text = xe.GetAttribute("description");//xe.InnerText;
                        contentText = xe.InnerText;
                        break;
                    }
                }
            }
            if (contentText != null)
            {
            
                string[] stringSeparators = new string[] { "/"  };
                string[] item_sel = contentText.Trim().Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                if(item_sel.Length>=2)
                {
                    tbCfgValue.Visible = false;
                    cbCfgValue.Visible = true;
                    cbCfgValue.Items.Clear();
                    for(int i=0; i<item_sel.Length; i++)
                    {
                        cbCfgValue.Items.Add(item_sel[i]);
                    }
                    cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                }
                else
                {
                    tbCfgValue.Visible = true;
                    cbCfgValue.Visible = false;
                    tbCfgValue.Text = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                }
            }
            /*
            if (contentText !=null && (m_portConfig.m_type == Config.EditType.EDIT_PORT_CONFIG
                ||m_portConfig.m_type == Config.EditType.EDIT_PCPORT_CONFIG
                ||m_portConfig.m_type == Config.EditType.EDIT_ETHERNET_CONFIG
                ||m_portConfig.m_type == Config.EditType.EDIT_ETHERNET_CLIENT_CONFIG
                ||m_portConfig.m_type == Config.EditType.EDIT_ETHERNET_SERVER_CONFIG
                ||m_portConfig.m_type == Config.EditType.EDIT_DATA_REMAP)
            )
            {
 

                switch (tbCfgName.Text)
                {
                    case "Module Name":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("A");
                        cbCfgValue.Items.Add("B");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        break;
                    case "Enable":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("Enable");
                        cbCfgValue.Items.Add("Disable");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;//m_portConfig.m_item[dgvPortConfig.CurrentCell.RowIndex, 1];
                        break;
                    case "Type":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("Master");
                        cbCfgValue.Items.Add("Slave");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        break;
                    case "Protocol":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("RTU");
                        cbCfgValue.Items.Add("ASCII");
                        //cbCfgValue.Items.Add("TCP");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        break;
                        
                    case "Ring Number":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("0");
                        cbCfgValue.Items.Add("1");
                        cbCfgValue.Items.Add("2");
                        cbCfgValue.Items.Add("3");
                        cbCfgValue.Items.Add("4");
                        cbCfgValue.Items.Add("5");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        break;
                    
                    case "Data Bits":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("7");
                        cbCfgValue.Items.Add("8");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        break;
                    
                    case "Stop Bits":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("1");
                        cbCfgValue.Items.Add("2");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        break;

                    case "Parity":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("O");
                        cbCfgValue.Items.Add("N");
                        cbCfgValue.Items.Add("E");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        break;


                    case "Baud Rate":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("3800");
                        cbCfgValue.Items.Add("9600");
                        cbCfgValue.Items.Add("19200");
                        cbCfgValue.Items.Add("48400");
                        cbCfgValue.Items.Add("57600");
                        cbCfgValue.Items.Add("115200");
                        //cbCfgValue.Items.Add("230400");
                        //cbCfgValue.Items.Add("460800");
                        //cbCfgValue.Items.Add("921600");
                        //cbCfgValue.Items.Add("115200");

                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        break;
                    default:
                        tbCfgValue.Visible = true;
                        cbCfgValue.Visible = false;
                        tbCfgValue.Text = (string)dgvPortConfig.CurrentRow.Cells[1].Value;

                        break;

                }
            }
            else
            {
                switch (tbCfgName.Text)
                {
                    case "Enable":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("0");
                        cbCfgValue.Items.Add("1");
                        cbCfgValue.Items.Add("2");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;//m_portConfig.m_item[dgvPortConfig.CurrentCell.RowIndex, 1];
                        break;
                    case "Func Code":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("1");
                        cbCfgValue.Items.Add("2");
                        cbCfgValue.Items.Add("3");
                        cbCfgValue.Items.Add("4");
                        cbCfgValue.Items.Add("5");
                        cbCfgValue.Items.Add("6");
                        cbCfgValue.Items.Add("15");
                        cbCfgValue.Items.Add("16");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        break;
                    case "Swap":
                        tbCfgValue.Visible = false;
                        cbCfgValue.Visible = true;
                        cbCfgValue.Items.Clear();
                        cbCfgValue.Items.Add("0");
                        cbCfgValue.Items.Add("1");
                        cbCfgValue.SelectedItem = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        break;
                    default:
                        tbCfgValue.Visible = true;
                        cbCfgValue.Visible = false;
                        tbCfgValue.Text = (string)dgvPortConfig.CurrentRow.Cells[1].Value;
                        
                        break;

                    }
                }
             * */
        }
        
        private void btOk_Click(object sender, EventArgs e)
        {
            if (m_datatable != null)
            {
                for(int i=0; i<dgvPortConfig.Rows.Count; i++)
                {
                    if (dgvPortConfig.Rows[i].Cells[1].Value.ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Can not be empty!");
                        return;
                    }
                }
                m_portConfig.m_comment = tbComment.Text;
                m_portConfig.Update(m_datatable);
            }
            Close();
        }

        private void btResetTag_Click(object sender, EventArgs e)
        {
        	if(m_portConfig!=null)
        	{
                    m_datatable = m_portConfig.CreateDataTable();
                    if (m_datatable != null)
                    {
                        DataSet dataset = (DataSet)(dgvPortConfig.DataSource);
                        if (dataset != null)
                            dataset.Dispose();
                        dataset = new DataSet();
                        dataset.Tables.Add(m_datatable);
                        dgvPortConfig.DataSource = dataset;
                        dgvPortConfig.DataMember = "PortConfig";
                    }
            }
        }

        private void btResetDefault_Click(object sender, EventArgs e)
        {
        	if(m_portConfig_default!=null)
        	{
        	
                    m_datatable.Rows.Clear();
                    for (int i = 0; i < m_datatable_default.Rows.Count; i++)
                    {
                        m_datatable.Rows.Add(m_datatable_default.Rows[i].ItemArray);
                    }
        	/*
                {
                    m_datatable = ((PortConfig)m_portConfig_default).CreateDataTable();
                    if (m_datatable != null)
                    {
                        DataSet dataset = (DataSet)(dgvPortConfig.DataSource);
                        if (dataset != null)
                            dataset.Dispose();
                        dataset = new DataSet();
                        dataset.Tables.Add(m_datatable);
                        dgvPortConfig.DataSource = dataset;
                        dgvPortConfig.DataMember = "PortConfig";
                    }
                }*/
            }
            
        }

        private void dgvPortConfig_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPortConfig.Rows[e.RowIndex].Cells[1].Value.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Can not be empty!");
            }
            dgvPortConfig.Rows[e.RowIndex].Selected = true;

        }


    
    }
}
