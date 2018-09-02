﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpGL
{
    public partial class Texture
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        } // end sub

        /// <summary>
        /// Destruct instance of the class.
        /// </summary>
        ~Texture()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Backing field to track whether Dispose has been called.
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Dispose managed and unmanaged resources of this instance.
        /// </summary>
        /// <param name="disposing">If disposing equals true, managed and unmanaged resources can be disposed. If disposing equals false, only unmanaged resources can be disposed. </param>
        private void Dispose(bool disposing)
        {
            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                } // end if

                // Dispose unmanaged resources.
                {
                    IntPtr context = GL.Instance.GetCurrentContext();
                    if (context != IntPtr.Zero)
                    {
                        GL.Instance.DeleteTextures(this.ids.Length, this.ids);
                    }
                    this.ids[0] = 0;
                }
                {
                    var disp = this.Storage as IDisposable;
                    if (disp != null) { disp.Dispose(); }
                }
                // A sampler builder can be used in multiple textures.
                // Thus we shouldn't dispose it here.
                //{
                //    var disp = this.SamplerBuilder as IDisposable;
                //    if (disp != null) { disp.Dispose(); }
                //}
            } // end if

            this.disposedValue = true;
        } // end sub
    }
}
