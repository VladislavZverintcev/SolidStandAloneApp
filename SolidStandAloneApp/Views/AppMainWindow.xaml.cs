using System;
using System.Windows;
using Support.Helpers;

namespace SolidStandAloneApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AppMainWindow : Window
    {
        public AppMainWindow()
        {
            InitializeComponent();
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            WindowScaling.WindowInitialized(this, Convert.ToInt32(Height), Convert.ToInt32(Width));
        }
    }
}
