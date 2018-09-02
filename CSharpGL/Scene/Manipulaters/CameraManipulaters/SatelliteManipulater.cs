﻿using System;
using System.Drawing;

using System.Windows.Forms;

namespace CSharpGL
{
    /// <summary>
    /// Rotates a camera on a sphere, whose center is camera's Target.
    /// <para>Just like a satellite moves around a fixed star.</para>
    /// </summary>
    public class SatelliteManipulater : Manipulater, IMouseHandler
    {
        private vec3 back;
        private GUISize bound = new GUISize();
        private ICamera camera;
        private IGLCanvas canvas;

        private GLMouseButtons lastBindingMouseButtons;
        private ivec2 lastPosition = new ivec2();
        private GLEventHandler<GLMouseEventArgs> mouseDownEvent;
        private bool mouseDownFlag = false;
        private GLEventHandler<GLMouseEventArgs> mouseMoveEvent;
        private GLEventHandler<GLMouseEventArgs> mouseUpEvent;
        private GLEventHandler<GLMouseEventArgs> mouseWheelEvent;
        private vec3 right;
        private vec3 up;

        /// <summary>
        ///
        /// </summary>
        public SatelliteManipulater(GLMouseButtons bindingMouseButtons = GLMouseButtons.Right)
        {
            this.HorizontalRotationFactor = 4;
            this.VerticalRotationFactor = 4;
            this.BindingMouseButtons = bindingMouseButtons;
            this.mouseDownEvent = (((IMouseHandler)this).canvas_MouseDown);
            this.mouseMoveEvent = (((IMouseHandler)this).canvas_MouseMove);
            this.mouseUpEvent = (((IMouseHandler)this).canvas_MouseUp);
            this.mouseWheelEvent = (((IMouseHandler)this).canvas_MouseWheel);
        }

        /// <summary>
        ///
        /// </summary>
        public GLMouseButtons BindingMouseButtons { get; set; }

        /// <summary>
        ///
        /// </summary>
        public float HorizontalRotationFactor { get; set; }

        /// <summary>
        ///
        /// </summary>
        public float VerticalRotationFactor { get; set; }

        /// <summary>
        ///
        /// </summary>
        public override void Bind(ICamera camera, IGLCanvas canvas)
        {
            if (camera == null || canvas == null) { throw new ArgumentNullException(); }

            this.camera = camera;
            this.canvas = canvas;

            canvas.MouseDown += this.mouseDownEvent;
            canvas.MouseMove += this.mouseMoveEvent;
            canvas.MouseUp += this.mouseUpEvent;
            canvas.MouseWheel += this.mouseWheelEvent;
        }

        void IMouseHandler.canvas_MouseDown(object sender, GLMouseEventArgs e)
        {
            this.lastBindingMouseButtons = this.BindingMouseButtons;
            if ((e.Button & this.lastBindingMouseButtons) != GLMouseButtons.None)
            {
                this.lastPosition = e.Location;
                var control = sender as Control;
                this.SetBounds(control.Width, control.Height);
                this.mouseDownFlag = true;
                PrepareCamera();
            }
        }

        void IMouseHandler.canvas_MouseMove(object sender, GLMouseEventArgs e)
        {
            if (this.mouseDownFlag
                && ((e.Button & this.lastBindingMouseButtons) != GLMouseButtons.None)
                && (e.X != lastPosition.x || e.Y != lastPosition.y))
            {
                IViewCamera camera = this.camera;
                if (camera == null) { return; }

                vec3 back = this.back;
                vec3 right = this.right;
                vec3 up = this.up;
                GUISize bound = this.bound;
                ivec2 downPosition = this.lastPosition;
                {
                    float deltaX = -this.HorizontalRotationFactor * (e.X - downPosition.x) / bound.Width;
                    float cos = (float)Math.Cos(deltaX);
                    float sin = (float)Math.Sin(deltaX);
                    vec3 newBack = new vec3(
                        back.x * cos + right.x * sin,
                        back.y * cos + right.y * sin,
                        back.z * cos + right.z * sin);
                    back = newBack;
                    right = up.cross(back);
                    back = back.normalize();
                    right = right.normalize();
                }
                {
                    float deltaY = this.VerticalRotationFactor * (e.Y - downPosition.y) / bound.Height;
                    float cos = (float)Math.Cos(deltaY);
                    float sin = (float)Math.Sin(deltaY);
                    vec3 newBack = new vec3(
                        back.x * cos + up.x * sin,
                        back.y * cos + up.y * sin,
                        back.z * cos + up.z * sin);
                    back = newBack;
                    up = back.cross(right);
                    back = back.normalize();
                    up = up.normalize();
                }

                camera.Position = camera.Target +
                    back * (float)((camera.Position - camera.Target).length());
                camera.UpVector = up;
                this.back = back;
                this.right = right;
                this.up = up;
                this.lastPosition = e.Location;

                IGLCanvas canvas = this.canvas;
                if (canvas != null && canvas.RenderTrigger == RenderTrigger.Manual)
                {
                    canvas.Repaint();
                }
            }
        }

        void IMouseHandler.canvas_MouseUp(object sender, GLMouseEventArgs e)
        {
            if ((e.Button & this.lastBindingMouseButtons) != GLMouseButtons.None)
            {
                this.mouseDownFlag = false;
            }
        }

        void IMouseHandler.canvas_MouseWheel(object sender, GLMouseEventArgs e)
        {
            this.camera.MouseWheel(e.Delta);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("back:{0}|{3:0.00},up:{1}|{4:0.00},right:{2}|{5:0.00}",
                back, up, right, back.length(), up.length(), right.length());
        }

        /// <summary>
        ///
        /// </summary>
        public override void Unbind()
        {
            if (this.canvas != null && (!this.canvas.IsDisposed))
            {
                this.canvas.MouseDown -= this.mouseDownEvent;
                this.canvas.MouseMove -= this.mouseMoveEvent;
                this.canvas.MouseUp -= this.mouseUpEvent;
                this.canvas.MouseWheel -= this.mouseWheelEvent;
                this.canvas = null;
                this.camera = null;
            }
        }

        private void PrepareCamera()
        {
            var camera = this.camera;
            if (camera != null)
            {
                vec3 back = camera.Position - camera.Target;
                vec3 right = camera.UpVector.cross(back);
                vec3 up = back.cross(right);

                this.back = back.normalize();
                this.right = right.normalize();
                this.up = up.normalize();
            }
        }

        private void SetBounds(int width, int height)
        {
            this.bound.Width = width;
            this.bound.Height = height;
        }
    }
}