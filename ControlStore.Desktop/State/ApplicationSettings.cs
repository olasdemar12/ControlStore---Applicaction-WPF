using System;
using System.Collections.Generic;
using System.Text;

namespace ControlStore.Desktop.State
{
    public class ApplicationSettings
    {
        public ApplicationSettings() { 
            StateApp = DesktopState.Start;
            OperationMode = OperationModeSetting.None;
        }

        public ApplicationSettings(DesktopState stateApp, OperationModeSetting operationMode)
        {
            StateApp = stateApp;
            OperationMode = operationMode;
        }

        public ApplicationSettings(ApplicationSettings other)
        {
            StateApp = other.StateApp;
            OperationMode = other.OperationMode;
        }

        public DesktopState StateApp { get; set; }
        public OperationModeSetting OperationMode { get; set; }
    }
}
 