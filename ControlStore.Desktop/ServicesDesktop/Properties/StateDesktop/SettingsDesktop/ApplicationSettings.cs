using System;
using System.Collections.Generic;
using System.Text;

namespace ControlStore.Desktop.ServicesDesktop.Properties.StateDesktop.SettingsDesktop
{
    public class ApplicationSettings
    {
        public ApplicationSettings() { 
            StateApp = DesktopState.Start;
            Networking = string.Empty;
            IDU = string.Empty;
            OperationMode = OperationModeSetting.None;
        }

        public ApplicationSettings(DesktopState stateApp, string networking, string idu, OperationModeSetting operationMode)
        {
            StateApp = stateApp;
            Networking = networking;
            IDU = idu;
            OperationMode = operationMode;
        }

        public ApplicationSettings(ApplicationSettings other)
        {
            StateApp = other.StateApp;
            Networking = other.Networking;
            IDU = other.IDU;
            OperationMode = other.OperationMode;
        }

        public DesktopState StateApp { get; set; }
        public string Networking { get; set; }
        public string IDU { get; set; }
        public OperationModeSetting OperationMode { get; set; }
    }
}
 