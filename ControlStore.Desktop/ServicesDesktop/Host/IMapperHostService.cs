using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlStore.Desktop.ServicesDesktop.Host
{
    public interface IMapperHostService
    {
        public IHost GetHostMapper();
        public void EnsureAppSettingsExists();
    }
}
