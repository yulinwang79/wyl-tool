using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;

namespace WYL
{
    public partial class DrawForm : Form
    {

        public FontBitmap m_fontBmp;
        
        public System.Windows.Forms.PictureBox pbBitmap;
        private Point p1 = Point.Empty, p2 = Point.Empty;
        private Point p3 = Point.Empty;
        private bool isMouseDown = false, isMouseUp = false;
        string m_filename;
        ArrayList addArray = new ArrayList();
        ArrayList aaaArray = new ArrayList();
        public string shape;
        private Color color = Color.Black;
        //  private Pen pen;
        byte m_height;

        //      Pen pen=new Pen(col,2);

        public struct SharpType
        {
            public string type;
            public Point p1, p2;
            public Color foreColor, backColor;
            public Brush brush;
            public SharpType(string type, Point p1, Point p2, Color foreColor, Color backColor, Brush brush)
            {
                this.type = type;
                this.p1 = p1;
                this.p2 = p2;
                this.foreColor = foreColor;
                this.backColor = backColor;
                this.brush = brush;
            }
            //   public Point[]points=new Point[];

        }

        public DrawForm(byte height)
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();
            m_height = height;

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }


        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!isMouseUp)
            {
                this.isMouseDown = true;
                this.p1 = new Point(e.X, e.Y);
            }

        }

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Graphics g = this.pbBitmap.CreateGraphics();
            if (isMouseDown && p2 != Point.Empty)
            {
                if (this.shape == "DrawEllipse")
                {
                    g.DrawEllipse(Pens.White, p1.X, p1.Y, Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
                }
                if (this.shape == "DrawRectangle")
                {
                    g.DrawRectangle(Pens.White, p1.X, p1.Y, Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
                }
                if (this.shape == "DrawLine")
                {
                    g.DrawLine(Pens.White, this.p1, this.p2);
                }

            }

            if (isMouseDown && !isMouseUp)
            {
                p2 = new Point(e.X, e.Y);
                if (this.shape == "DrawEllipse")
                {

                    g.DrawEllipse(new Pen(color, 1), p1.X, p1.Y, Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
                }
                if (this.shape == "DrawRectangle")
                {
                    //     g.DrawRectangle( Pens.Black, p1.X, p1.Y, Math.Abs( p1.X - p2.X ), Math.Abs( p1.Y - p2.Y ) );
                    g.DrawRectangle(new Pen(color, 1), p1.X, p1.Y, Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
                }
                if (this.shape == "DrawLine")
                {
                    g.DrawLine(new Pen(color, 1), this.p1, this.p2);
                }
                if (this.shape == "DrawCurve")
                {

                    g.DrawLine(new Pen(color, 1), this.p1, this.p2);
                    addArray.Add(new SharpType("DrawCurve", p1, p2, color, Color.Empty, Brushes.Black));
                    this.p1 = this.p2;
                    this.p2 = this.p3;


                }


            }
            foreach (SharpType type in addArray)
            {
                if (type.type == "DrawEllipse")
                {
                    g.DrawEllipse(new Pen(type.foreColor, 1), type.p1.X, type.p1.Y, Math.Abs(type.p1.X - type.p2.X), Math.Abs(type.p1.Y - type.p2.Y));

                }
                if (type.type == "DrawRectangle")
                {
                    g.DrawRectangle(new Pen(type.foreColor, 1), type.p1.X, type.p1.Y, Math.Abs(type.p1.X - type.p2.X), Math.Abs(type.p1.Y - type.p2.Y));
                }
                if (type.type == "DrawLine")
                {
                    g.DrawLine(new Pen(type.foreColor, 1), type.p1, type.p2);
                }

                if (type.type == "DrawCurve")
                {

                    g.DrawLine(new Pen(type.foreColor, 1), type.p1, type.p2);

                }
            }
            g.Dispose();

        }

        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.isMouseDown = false;
            p2 = new Point(e.X, e.Y);
            Graphics g = this.pbBitmap.CreateGraphics();
            if (this.shape == "DrawEllipse")
            {
                g.DrawEllipse(new Pen(color, 1), p1.X, p1.Y, Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
                addArray.Add(new SharpType("DrawEllipse", p1, p2, color, Color.Empty, Brushes.Black));
            }
            if (this.shape == "DrawRectangle")
            {

                g.DrawRectangle(new Pen(color, 1), p1.X, p1.Y, Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
                //    addArray.Add( new SharpType("DrawRectangle", p1, p2, Color.Black, Color.Empty, Brushes.Black ) );
                addArray.Add(new SharpType("DrawRectangle", p1, p2, color, Color.Empty, Brushes.Black));
            }
            if (this.shape == "DrawLine")
            {
                g.DrawLine(new Pen(color, 1), this.p1, this.p2);
                addArray.Add(new SharpType("DrawLine", p1, p2, color, Color.Empty, Brushes.Black));
            }
            if (this.shape == "DrawCurve")
            {
                g.DrawLine(new Pen(color, 1), this.p1, this.p2);
                addArray.Add(new SharpType("DrawCurve", p1, p2, color, Color.Empty, Brushes.Black));
                //    this.p1=this.p2;
                //    this.p2=this.p3;

            }

            p1 = Point.Empty;
            p2 = Point.Empty;
            g.Dispose();

        }

        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            foreach (SharpType type in addArray)
            {
                if (type.type == "DrawEllipse")
                {
                    e.Graphics.DrawEllipse(new Pen(type.foreColor, 1), type.p1.X, type.p1.Y, Math.Abs(type.p1.X - type.p2.X), Math.Abs(type.p1.Y - type.p2.Y));

                }
                if (type.type == "DrawRectangle")
                {
                    e.Graphics.DrawRectangle(new Pen(type.foreColor, 1), type.p1.X, type.p1.Y, Math.Abs(type.p1.X - type.p2.X), Math.Abs(type.p1.Y - type.p2.Y));
                }
                if (type.type == "DrawLine")
                {
                    e.Graphics.DrawLine(new Pen(type.foreColor, 1), type.p1, type.p2);
                }


                if (type.type == "DrawCurve")
                {

                    e.Graphics.DrawLine(new Pen(type.foreColor, 1), type.p1, type.p2);

                }

            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            shape = "DrawRectangle";
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            shape = "DrawEllipse";
            //   this.color=Color.Black;
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            shape = "DrawLine";
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            ColorDialog ColorDialog1 = new ColorDialog();
            ColorDialog1.AllowFullOpen = true;
            ColorDialog1.FullOpen = true;
            //设定此颜色对话框存在"帮助"按钮，缺省是没有的
            ColorDialog1.ShowHelp = true;
            // 设定此颜色对话框的初始颜色，所以如果在对话框中选择"取消"，则此对话框会重新此颜色
            ColorDialog1.Color = Color.Black;
            if (ColorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                this.color = ColorDialog1.Color;
                //    showInfo ( ) ;
            }
            else
            {
                this.color = Color.Black;
            }
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            this.addArray.Clear();
            this.pbBitmap.Refresh();
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            shape = "DrawCurve";
        }

        private void pbBitmap_Paint(object sender, PaintEventArgs e)
        {
            drawGrids(e);
        }

        private void drawGrids(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = Pens.Blue;
            for (int i = 0; i < ClientRectangle.Width; i++)
            {
                g.DrawLine(myPen, new Point(i, 0), new Point(i, ClientRectangle.Bottom));
                i += 10;
            }

            for (int j = 0; j < ClientRectangle.Height; j++)
            {
                g.DrawLine(myPen, new Point(0, j), new Point(ClientRectangle.Right, j));
                j += 10;
            }
        }
        private void btLoad_Click(object sender, EventArgs e)
        {
            BdfClass s_bdf = new BdfClass();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
            openFileDialog1.Filter = "BMP files (*.bmp)|*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((m_filename = openFileDialog1.FileName) != null)
                {
                    pbBitmap.SizeMode = PictureBoxSizeMode.Zoom;
                    pbBitmap.Image = Image.FromFile(m_filename);
                    btInsert.Enabled = true;
                }
            }
        }

        private void btInsert_Click(object sender, EventArgs e)
        {
            Bitmap bmp= (Bitmap)pbBitmap.Image;
            if(bmp.Height != m_height)
            {
                MessageBox.Show("Different heights!");
                return;
            }
            string[] item = m_filename.Split('\\');
            m_fontBmp = new FontBitmap(UInt16.Parse(item[item.Length - 1].Substring(0, item[item.Length - 1].IndexOf('.'))), bmp);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

    }
     
}
