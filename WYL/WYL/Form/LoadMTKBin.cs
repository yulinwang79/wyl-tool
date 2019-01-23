using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



namespace WYL
{
    public partial class LoadMTKBin : Form
    {
        MTKResourceClass m_mtkResource;

        public LoadMTKBin()
        {
            InitializeComponent();
        }

        private void tsOpen_Click(object sender, EventArgs e)
        {
            OpenBinForm frm = new OpenBinForm();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                m_mtkResource = new MTKResourceClass(frm.FileName);
            }
        }

        private void tsExportFonts_Click(object sender, EventArgs e)
        {
            if(m_mtkResource ==null ||m_mtkResource.g_langpack2ndJumpTbl== null ||m_mtkResource.g_langpack2ndJumpTbl.mtk_gLanguageArray ==null  )
            {
                tsOpen_Click(sender, e);
            }
            else
            {
                int index =0;
                for(int i=0; i< m_mtkResource.g_langpack2ndJumpTbl.fontfamilyList.Length; i++)
                {
                    for(int j=0; j< m_mtkResource.g_langpack2ndJumpTbl.fontfamilyList[i].nTotalFonts; j++)
                    {
                    
                        BdfClass bdf = new BdfClass();
                        bdf.LoadData(m_mtkResource.g_langpack2ndJumpTbl.fontfamilyList[i].DatafontData[j]);
                        bdf.SaveFile("cust" + index + ".bdf");
                        index ++;
                    }
                }
            }
        }
    }
}
