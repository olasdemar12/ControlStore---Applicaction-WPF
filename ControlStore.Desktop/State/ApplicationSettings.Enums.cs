using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
