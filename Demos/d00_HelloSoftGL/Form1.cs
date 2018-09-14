using CSharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace d00_HelloSoftGL
{
    public partial class Form1 : Form
    {
        private static readonly vec4 clearColor = Color.SkyBlue.ToVec4();
        public Form1()
        {
            InitializeComponent();

            this.winSoftGLCanvas1.OpenGLDraw += winSoftGLCanvas1_OpenGLDraw;
        }

        void winSoftGLCanvas1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            GL gl = GL.Instance;
            if (gl != null)
            {
                gl.ClearColor(clearColor.x, clearColor.y, clearColor.z, clearColor.w);
                gl.Clear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);
                //this.assist.Render(this.RenderTrigger == RenderTrigger.TimerBased, this.Height, this.FPS, this);
            }
        }
    }
}
