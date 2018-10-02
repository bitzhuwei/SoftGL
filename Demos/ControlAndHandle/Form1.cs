using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ControlAndHandle
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr WindowFromPoint(Point Point);
        [DllImport("user32.dll")]
        internal static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("user32.dll")]
        public static extern bool CloseWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.lblInfo.Text = string.Empty;
            var builder = new StringBuilder();
            {
                Point p;
                GetCursorPos(out p);
                IntPtr handle = WindowFromPoint(p);
                builder.AppendLine(string.Format("WindowFromPoint({0}) => {1}", p, handle));
                builder.AppendLine(string.Format("{0}.Handle({1})", this, this.Handle));
            }
            foreach (var item in this.Controls)
            {
                var control = item as Control;
                if (control != null)
                {
                    builder.AppendLine(string.Format("{0}.Handle({1})", control, control.Handle));
                }
                else
                {
                    builder.AppendLine(string.Format("{0}", item));
                }
            }

            string result = builder.ToString();
            this.textBox1.Text = result;
        }
    }
}
