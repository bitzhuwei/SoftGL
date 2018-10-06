﻿using System.Windows.Forms;

namespace SoftGL.Windows
{
    public partial class FormPropertyGrid : Form
    {
        public FormPropertyGrid(object obj)
        {
            InitializeComponent();

            if (obj != null)
            {
                DisplayObject(obj);
            }
        }

        public void DisplayObject(object obj)
        {
            if (!this.IsDisposed)
            {
                this.propertyGrid1.SelectedObject = obj;
                this.Text = string.Format("{0} - {1}", obj, obj != null ? obj.GetType().FullName : "");
            }
        }
    }
}