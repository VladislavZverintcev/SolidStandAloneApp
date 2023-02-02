using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;
using Support.Templates.ViewModel.Base;
using Support.Templates.ViewModel.Commands;
using SolidStandAloneApp.Helpers;
using SolidStandAloneApp.Views;
using System.Windows.Controls;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using System.Timers;

namespace SolidStandAloneApp.ViewModels
{
    public class AppMainWindowMV : ViewModel
    {

        #region Fields

        #region Private
        SldWorks? swApp = null;
        private System.Timers.Timer checkTimer = new Timer();
        private System.Timers.Timer connectTimer = new Timer();
        private bool isConnected = false;
        #endregion Private

        #region Public

        #endregion Public

        #endregion Fields

        #region Properties

        #region Private

        #endregion Private

        #region Public
        public bool IsConnected
        {
            get => isConnected;
            set => Set(ref isConnected, value);
        }
        #endregion Public

        #endregion Properties

        #region Constructors

        #region Private

        #endregion Private

        #region Public
        public AppMainWindowMV()
        {
            SetCheckConnectTimer();
            SetConnectTimer();
        }
        #endregion Public

        #endregion Constructors

        #region Methods

        #region Private
        private void SetCheckConnectTimer()
        {
            checkTimer = new System.Timers.Timer(100);
            checkTimer.Elapsed += (s,e) => 
            { 
                if(swApp != null) 
                {
                    if(!IsConnected)
                    {
                        IsConnected = true;
                    }
                }
                else
                { 
                    if(IsConnected)
                    {
                        IsConnected = false;
                    }
                }
            };
            checkTimer.Enabled = true;
        }
        private void SetConnectTimer()
        {
            connectTimer = new System.Timers.Timer(1000);
            connectTimer.Elapsed += (s, e) =>
            {
                if (!isConnected)
                {
                    swApp = Helpers.SolidConnector.GetSW();
                    if(swApp != null) 
                    {
                        swApp.DestroyNotify += () => { swApp = null; IsConnected = false; return 0; };
                    }
                }
            };
            connectTimer.Enabled = true;
        }
        #endregion Private

        #region Public

        #endregion Public

        #endregion Methods

        #region Commands

        #region Private

        #endregion Private

        #region Public

        //public ICommand OpenWindowAboutProgramComm
        //{
        //    get
        //    {
        //        return new VMCommands((obj) =>
        //        {
        //            var aboutProgrammWindow = new AboutProgrammWindow();
        //            aboutProgrammWindow.ShowDialog();
        //        }, (obj) => true);
        //    }
        //}
        #endregion Public

        #endregion Commands
    }
}
