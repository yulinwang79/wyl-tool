using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WYL
{
    public partial class FontView : Form
    {
        BdfClass m_bdfl;
        BdfClass m_bdfr;

        public FontView()
        {
            InitializeComponent();
        }

        public void LoadFont(bool isLeft, BdfClass bdf)
        {
            DataGridView dgv;
            if(isLeft)
            {
                dgv = dgvFontL;
                m_bdfl = bdf;
            }
            else
            {
                dgv = dgvFontR;
                m_bdfr = bdf;
            }
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            for (int i = 0; i < 16; i++)
            {
                //DataColumn dtCol = new DataColumn();
                DataGridViewImageColumn dtCol = new DataGridViewImageColumn();
                dtCol.HeaderText = i.ToString("X2");
                dtCol.Width = bdf.m_width+4;
                dgv.Columns.Add(dtCol);
            }


            Bitmap bmp = new Bitmap((int)bdf.m_width, (int)bdf.m_height);
            Bitmap[] BmpRows = new Bitmap[16]{bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp};
            if (bdf.isExistFont())
            {
                dgv.RowTemplate.Height = bdf.m_height;
                //dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
                dgv.RowHeadersWidth = 60;
                for (int i = 0; i < BdfClass.UICODE_TOTAL; i++)
                {
                    if(bdf.m_FontArray[i]!=null)
                    {
                        if (dgv.Rows.Count == 0 || (int)dgv.Rows[dgv.Rows.Count - 1].HeaderCell.Tag != ((FontBitmap)(bdf.m_FontArray[i])).m_ch >>4)
                        {
                            dgv.Rows.Add(BmpRows);
                            for (int j = 0; j < 16; j++)
                            {
                                dgv[((FontBitmap)(bdf.m_FontArray[i])).m_ch % 16, dgv.Rows.Count - 1].Tag = 0;                           
                            }
                            dgv.Rows[dgv.Rows.Count - 1].HeaderCell.Tag = ((FontBitmap)(bdf.m_FontArray[i])).m_ch >> 4;
                            dgv.Rows[dgv.Rows.Count - 1].HeaderCell.Value = ((((FontBitmap)(bdf.m_FontArray[i])).m_ch >>4)<<4).ToString("X4");
                        }
                        dgv[((FontBitmap)(bdf.m_FontArray[i])).m_ch % 16, dgv.Rows.Count - 1].Value = ((FontBitmap)(bdf.m_FontArray[i])).ToBitmap();
                        dgv[((FontBitmap)(bdf.m_FontArray[i])).m_ch % 16, dgv.Rows.Count - 1].Tag = ((FontBitmap)(bdf.m_FontArray[i])).m_ch;
                    }
                }
            }
            if(isLeft)
            {
                tsSaveL.Enabled = true;
                 if (tsSaveR.Enabled == true)
                 {
                     tsMerge.Enabled = true;
                     tsMergeR.Enabled = true;
                 }
            }
            else
            {
                tsSaveR.Enabled = true;
                if (tsSaveL.Enabled == true)
                {
                    tsMerge.Enabled = true;
                    tsMergeR.Enabled = true;
                }
            }

        }

        public void InsertFont(bool isLeft, FontBitmap fontBmp)
        {
            DataGridView dgv;
            if(isLeft)
            {
                dgv = dgvFontL;
            }
            else
            {
                dgv = dgvFontR;
            }

            {
                Bitmap bmp = new Bitmap((int)fontBmp.m_width, (int)fontBmp.m_height);
                Bitmap[] BmpRows = new Bitmap[16]{bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp,bmp};
                for (int i = 0; i< dgv.Rows.Count; i++)
                {
                    if((int)dgv.Rows[i].HeaderCell.Tag == fontBmp.m_ch >>4)
                    {
                        dgv[fontBmp.m_ch % 16, i].Value = fontBmp.ToBitmap();
                        dgv[fontBmp.m_ch % 16, i].Tag = fontBmp.m_ch;  
                        break;
                    }
                    else if((int)dgv.Rows[i].HeaderCell.Tag > fontBmp.m_ch >>4 ||  i== dgv.Rows.Count-1)
                    {
                        if( (int)dgv.Rows[i].HeaderCell.Tag > fontBmp.m_ch >>4 )
                        {
                            dgv.Rows.Insert(i,BmpRows);
                        }
                        else
                        {
                            dgv.Rows.Add(BmpRows);     
                            i++;
                        }
                            
                        for (int j = 0; j < 16; j++)
                        {
                            dgv[fontBmp.m_ch % 16, dgv.Rows.Count - 1].Tag = 0;                           
                        }
                        dgv.Rows[dgv.Rows.Count - 1].HeaderCell.Tag = fontBmp.m_ch >> 4;
                        dgv.Rows[dgv.Rows.Count - 1].HeaderCell.Value = ((fontBmp.m_ch >>4)<<4).ToString("X4");
                        
                        dgv[fontBmp.m_ch % 16, i].Value = fontBmp.ToBitmap();
                        dgv[fontBmp.m_ch % 16, i].Tag = fontBmp.m_ch;  
                        
                        break;
                    }

                }
            }

        }

        private void tsLoadData_Click(object sender, EventArgs e)
        {
            LoadDataForm frm = new LoadDataForm();
            try
            {
                 if (frm.ShowDialog() == DialogResult.OK)
                 {
                     LoadFont(true,frm.m_bdf);
                 }
 
            }
            finally
            {
                frm.Dispose();
            }
        }

        private void tsSaveL_Click(object sender, EventArgs e)
        {
            if (m_bdfl != null)
            {
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

                SaveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
                SaveFileDialog1.Filter = "BDF files (*.bdf)|*.bdf";
                SaveFileDialog1.FilterIndex = 1;
                SaveFileDialog1.RestoreDirectory = true;
                SaveFileDialog1.Title = "Save file";

                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    m_bdfl.SaveFile(SaveFileDialog1.FileName);
                }
            }
        }

        private void tsSaveR_Click(object sender, EventArgs e)
        {
            if (m_bdfr != null)
            {
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

                SaveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
                SaveFileDialog1.Filter = "BDF files (*.bdf)|*.bdf";
                SaveFileDialog1.FilterIndex = 1;
                SaveFileDialog1.RestoreDirectory = true;
                SaveFileDialog1.Title = "Save file";

                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    m_bdfr.SaveFile(SaveFileDialog1.FileName);
                }
            }

        }

        private void tsLoadBdfL_Click(object sender, EventArgs e)
        {
            BdfClass s_bdf = new BdfClass();
            string fileName;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
            openFileDialog1.Filter = "BDF files (*.bdf)|*.bdf";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((fileName = openFileDialog1.FileName) != null)
                {
                    if (s_bdf.LoadData(fileName))
                    {
                        LoadFont(true, s_bdf);
                    }
                }
            }

        }

        private void tsLoadBdfR_Click(object sender, EventArgs e)
        {
            BdfClass s_bdf = new BdfClass();
            string fileName;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
            openFileDialog1.Filter = "BDF files (*.bdf)|*.bdf";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((fileName = openFileDialog1.FileName) != null)
                {
                    if (s_bdf.LoadData(fileName))
                    {
                        LoadFont(false, s_bdf);
                    }
                }
            }
        }

        private void tsLoadDataR_Click(object sender, EventArgs e)
        {
            LoadDataForm frm = new LoadDataForm();
            try
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadFont(false, frm.m_bdf);
                }

            }
            finally
            {
                frm.Dispose();
            }
        }

        private void tsMergeL_Click(object sender, EventArgs e)
        {
            m_bdfl.Merge(m_bdfr);
            LoadFont(true,m_bdfl);
        }

        private void tsMergeR_Click(object sender, EventArgs e)
        {
            m_bdfr.Merge(m_bdfl);
            LoadFont(false,m_bdfr);
        }

        private void tsInsertBitmap_Click(object sender, EventArgs e)
        {
            DrawForm frm = new DrawForm(m_bdfl.m_height);
            try
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    m_bdfl.Insert(frm.m_fontBmp);
                }


            }
            finally
            {
                frm.Dispose();
            }
        }

        private void btLoadBmp_Click(object sender, EventArgs e)
        {
            BdfClass s_bdf = new BdfClass();
            string filename;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
            openFileDialog1.Filter = "BMP files (*.bmp)|*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((filename = openFileDialog1.FileName) != null)
                {
                    string[] item = filename.Split('\\');
                    pbBitmap.SizeMode = PictureBoxSizeMode.Zoom;
                    pbBitmap.Image = Image.FromFile(filename);
                    tbCode.Text = item[item.Length - 1].Substring(0, item[item.Length - 1].IndexOf('.'));
                    if(m_bdfl!=null)
                        btInsertL.Enabled = true;

                    if (m_bdfr != null)
                        btInsertR.Enabled = true;

                }
            }

        }

        private void btInsertL_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)pbBitmap.Image;
            if (bmp.Height != m_bdfl.m_height)
            {
                MessageBox.Show("Different heights!");
                return;
            }
            ;
            FontBitmap fontBmp = new FontBitmap(Convert.ToUInt16(tbCode.Text, 16), bmp);
            if(m_bdfl.Insert(fontBmp))
            {
                InsertFont(true,fontBmp);
            }
        }

        private void btInsertR_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)pbBitmap.Image;
            if (bmp.Height != m_bdfr.m_height)
            {
                MessageBox.Show("Different heights!");
                return;
            }

            FontBitmap fontBmp = new FontBitmap(Convert.ToUInt16(tbCode.Text, 16), bmp);
            if(m_bdfr.Insert(fontBmp))
            {
                InsertFont(false,fontBmp);
            }
        }

        private void dgvFontL_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            pbBitmap.Image = (Bitmap)dgvFontL[e.ColumnIndex, e.RowIndex].Value;
            if (dgvFontL[e.ColumnIndex, e.RowIndex].Tag != null)
            {
                UInt16 ch = (UInt16)(dgvFontL[e.ColumnIndex, e.RowIndex].Tag);
                tbCode.Text = ch.ToString("X");
            }
            else
                tbCode.Text = "0";

        }

        private void dgvFontR_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            pbBitmap.Image = (Bitmap)dgvFontR[e.ColumnIndex, e.RowIndex].Value;
            if (dgvFontR[e.ColumnIndex, e.RowIndex].Tag != null)
            {
                UInt16 ch = (UInt16)(dgvFontR[e.ColumnIndex, e.RowIndex].Tag);
                tbCode.Text = ch.ToString("X");
            }
            else
                tbCode.Text = "0";
        }
    }
}
