using System;
using System.Runtime.InteropServices;


namespace SolidStandAloneApp.Helpers
{
    internal static class NativeMethodHelper
    {
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);
    }
}
