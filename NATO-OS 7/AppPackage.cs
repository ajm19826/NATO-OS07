using System.Collections.Generic;

namespace NATO_OS_App_Installer
{
    // Represents the structure of an .npkg file, containing a list of AppInfo objects.
    public class AppPackage
    {
        public List<AppInfo> Apps { get; set; } = new List<AppInfo>();
    }
}
