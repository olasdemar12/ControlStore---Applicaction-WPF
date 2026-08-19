using System;
using System.Collections.Generic;
using System.Text;

namespace ControlStore.Desktop.State
{
    public enum DesktopState
    {
        None = 0,
        Start = 1,
        Configured = 2,
    }

    public enum OperationModeSetting
    {
        None = 0,
        Server = 1,
        Client = 2,
    }
}
