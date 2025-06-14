using System.Drawing;
using System.ComponentModel; // Required for TypeConverter for Font, Color

namespace NATO_OS_App_Installer
{
    // Represents serializable information for a single Windows Forms control
    public class ControlInfo
    {
        public string ControlType { get; set; } // Full type name, e.g., "System.Windows.Forms.Button"
        public string Name { get; set; }
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string BackColorHex { get; set; } // ARGB hex string, e.g., "#FF000000" (Black)
        public string ForeColorHex { get; set; } // ARGB hex string
        public string FontString { get; set; }   // String representation from FontConverter, e.g., "Arial, 10pt, style=Bold"
        public bool Enabled { get; set; }
        public bool Visible { get; set; }

        // Common properties for specific control types
        public bool? Checked { get; set; }     // For CheckBox, RadioButton
        public decimal? Value { get; set; }    // For NumericUpDown (uses decimal), ProgressBar, TrackBar (uses int)
        public decimal? Minimum { get; set; }  // For NumericUpDown (uses decimal), TrackBar (uses int)
        public decimal? Maximum { get; set; }  // For NumericUpDown (uses decimal), TrackBar (uses int)
        public string Mask { get; set; }       // For MaskedTextBox

        // For PictureBox - Base64 encoded image data
        public string ImageData { get; set; }

        // For Event Handlers - A string representing a predefined action
        // Example values: "ShowMessageBox", "ToggleVisibility:[ControlName]"
        public string EventAction { get; set; }
    }
}

