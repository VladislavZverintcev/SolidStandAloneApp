using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using SolidWorks.Interop.sldworks;

namespace SolidStandAloneApp.Helpers
{
    public class SolidConnector
    {
        public static SldWorks GetSW()
        {
            try
            {
                return (SldWorks)Marshal2.GetActiveObject("SldWorks.Application");
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                return null;
            }
        }
    }
}
