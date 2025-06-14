// AppInfo.cs
using System;
using System.Collections.Generic;

namespace NATO_OS_App_Installer
{
    // Represents the data for a single application within an .npkg package.
    public class AppInfo
    {
        public string AppId { get; set; }
        public string AppName { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string LaunchCommand { get; set; }
        public DateTime InstallationDate { get; set; }

        // Properties to store the dimensions of the main app GroupBox
        public int? GroupBoxWidth { get; set; } // Nullable int allows for backward compatibility
        public int? GroupBoxHeight { get; set; } // Nullable int

        // List to hold information about the controls within this app's GroupBox
        public List<ControlInfo> Controls { get; set; } = new List<ControlInfo>();

        public AppInfo()
        {
            AppId = Guid.NewGuid().ToString();
            InstallationDate = DateTime.Now;
        }
    }
}
