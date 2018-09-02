using System;

namespace CSharpGL
{
    partial class DIBSection
    {
        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        ~DIBSection()
        {
            this.Dispose(false);
        }

        private bool disposedValue = false;

        private void Dispose(bool disposing)
        {
            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Dispose unmanaged resources.
                this.DestroyBitmap();

                IntPtr dc = this.MemoryDeviceContext;
                if (dc != IntPtr.Zero)
                {
                    this.MemoryDeviceContext = IntPtr.Zero;
                    //Win32.DeleteDC(dc);
                }
            }

            this.disposedValue = true;
        }
    }
}