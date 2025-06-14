using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.ComponentModel; // Required for TypeConverter

namespace NATO_OS_App_Installer
{
    public static class RewritePkg
    {
        // Helper to convert Color to ARGB hex string
        public static string ColorToHexString(Color color)
        {
            return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        // Helper to convert ARGB hex string to Color
        private static Color HexStringToColor(string hex)
        {
            if (string.IsNullOrEmpty(hex) || !hex.StartsWith("#") || hex.Length != 9)
            {
                return Color.Empty; // Or some default color
            }
            try
            {
                int a = int.Parse(hex.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                int r = int.Parse(hex.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                int g = int.Parse(hex.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
                int b = int.Parse(hex.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);
                return Color.FromArgb(a, r, g, b);
            }
            catch
            {
                return Color.Empty; // Handle parsing errors
            }
        }

        // Helper to convert Font to string
        public static string FontToString(Font font)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            if (font == null) return null;
            return converter.ConvertToString(font);
        }

        // Helper to convert string to Font
        private static Font StringToFont(string fontString)
        {
            if (string.IsNullOrEmpty(fontString)) return null;
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            try
            {
                return converter.ConvertFromString(fontString) as Font;
            }
            catch
            {
                return SystemFonts.DefaultFont; // Fallback to default font on error
            }
        }

        // Helper to convert Image to Base64 string
        public static string ImageToBase64(Image image)
        {
            if (image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                // Save image to stream in PNG format (lossless, good for UI elements)
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        // Helper to convert Base64 string to Image
        private static Image Base64ToImage(string base64String)
        {
            if (string.IsNullOrEmpty(base64String)) return null;
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch
            {
                return null; // Handle invalid Base64 strings
            }
        }


        // Saves an AppInfo object to a .npkg file
        public static void SaveAppInfoToPackage(AppInfo appInfo, string filePath)
        {
            AppPackage package = new AppPackage();
            package.Apps.Add(appInfo);

            string jsonString = JsonConvert.SerializeObject(package, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }

        // Loads an AppPackage from a .npkg file
        public static AppPackage LoadPackageFromFile(string filePath)
        {
            string jsonContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<AppPackage>(jsonContent);
        }

        // Creates a GroupBox representation from AppInfo for display/preview
        public static GroupBox CreateGroupBoxFromAppInfo(AppInfo app)
        {
            GroupBox appGroupBox = new GroupBox();
            appGroupBox.Text = app.AppName;
            appGroupBox.Margin = new Padding(10);
            appGroupBox.Cursor = Cursors.Hand;
            appGroupBox.Tag = app; // Store the AppInfo object

            // Set the GroupBox size from AppInfo properties if available
            if (app.GroupBoxWidth.HasValue && app.GroupBoxHeight.HasValue && app.GroupBoxWidth.Value > 0 && app.GroupBoxHeight.Value > 0)
            {
                appGroupBox.Size = new Size(app.GroupBoxWidth.Value, app.GroupBoxHeight.Value);
                appGroupBox.AutoSize = false; // Turn off auto-size if explicit size is set
            }
            else
            {
                appGroupBox.Size = new Size(300, 200); // Default reasonable size if no explicit size
                appGroupBox.AutoSize = false;
            }

            if (app.Controls == null || app.Controls.Count == 0)
            {
                Label lblNoControls = new Label();
                lblNoControls.Text = $"No detailed controls for '{app.AppName}'.\nVersion: {app.Version}\nDev: {app.Developer}\nDesc: {app.Description}";
                lblNoControls.Location = new Point(10, 20);
                lblNoControls.AutoSize = true;
                lblNoControls.MaximumSize = new Size(appGroupBox.Width - 20, 0);
                appGroupBox.Controls.Add(lblNoControls);
                // If no controls and no explicit size, adjust to fit this default label
                if (!app.GroupBoxWidth.HasValue || !app.GroupBoxHeight.HasValue)
                {
                    appGroupBox.Size = new Size(Math.Max(appGroupBox.Width, lblNoControls.Width + 20), Math.Max(appGroupBox.Height, lblNoControls.Height + 40));
                }
            }
            else
            {
                // Iterate through the ControlInfo list and create/configure actual controls
                foreach (ControlInfo ci in app.Controls)
                {
                    try
                    {
                        Type controlType = Type.GetType(ci.ControlType);
                        if (controlType == null || !typeof(Control).IsAssignableFrom(controlType))
                        {
                            Console.WriteLine($"Warning: Could not find or create control type '{ci.ControlType}'. Skipping.");
                            continue;
                        }

                        // Create an instance of the control
                        Control control = (Control)Activator.CreateInstance(controlType);

                        // Set common properties
                        control.Name = ci.Name;
                        control.Text = ci.Text;
                        control.Location = new Point(ci.X, ci.Y);
                        control.Size = new Size(ci.Width, ci.Height);
                        control.Enabled = ci.Enabled;
                        control.Visible = ci.Visible;

                        if (!string.IsNullOrEmpty(ci.BackColorHex))
                        {
                            control.BackColor = HexStringToColor(ci.BackColorHex);
                        }
                        if (!string.IsNullOrEmpty(ci.ForeColorHex))
                        {
                            control.ForeColor = HexStringToColor(ci.ForeColorHex);
                        }
                        if (!string.IsNullOrEmpty(ci.FontString))
                        {
                            control.Font = StringToFont(ci.FontString);
                        }

                        // Handle specific control properties using 'is' and casting
                        if (control is CheckBox checkBox && ci.Checked.HasValue)
                        {
                            checkBox.Checked = ci.Checked.Value;
                        }
                        else if (control is RadioButton radioButton && ci.Checked.HasValue)
                        {
                            radioButton.Checked = ci.Checked.Value;
                        }
                        else if (control is ProgressBar progressBar && ci.Value.HasValue)
                        {
                            progressBar.Minimum = (int)(ci.Minimum ?? 0);
                            progressBar.Maximum = (int)(ci.Maximum ?? 100);
                            progressBar.Value = Math.Max(progressBar.Minimum, Math.Min(progressBar.Maximum, (int)ci.Value.Value));
                        }
                        else if (control is NumericUpDown numericUpDown && ci.Value.HasValue)
                        {
                            numericUpDown.Minimum = ci.Minimum ?? 0;
                            numericUpDown.Maximum = ci.Maximum ?? 100;
                            numericUpDown.Value = Math.Max(numericUpDown.Minimum, Math.Min(numericUpDown.Maximum, ci.Value.Value));
                        }
                        else if (control is TrackBar trackBar && ci.Value.HasValue)
                        {
                            trackBar.Minimum = (int)(ci.Minimum ?? 0);
                            trackBar.Maximum = (int)(ci.Maximum ?? 100);
                            trackBar.Value = Math.Max(trackBar.Minimum, Math.Min(trackBar.Maximum, (int)ci.Value.Value));
                        }
                        else if (control is MaskedTextBox maskedTextBox && !string.IsNullOrEmpty(ci.Mask))
                        {
                            maskedTextBox.Mask = ci.Mask;
                        }
                        else if (control is PictureBox pictureBox && !string.IsNullOrEmpty(ci.ImageData))
                        {
                            pictureBox.Image = Base64ToImage(ci.ImageData);
                            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Or other mode as desired
                        }
                        // Note: For controls like ComboBox, ListBox, CheckedListBox, TabControl, DataGridView, etc.,
                        // their items/collections are not serialized here due to complexity.
                        // Nested controls within containers (Panel, GroupBox) are not recursively handled in this flat list.

                        // Handle EventAction for clickable controls (e.g., Buttons, LinkLabels, PictureBox)
                        if (!string.IsNullOrEmpty(ci.EventAction))
                        {
                            // Store the ControlInfo within the Tag of the newly created control
                            // This allows the generic event handler to access the action definition.
                            control.Tag = ci;

                            if (control is Button button)
                            {
                                button.Click += (s, eArgs) => HandleDynamicEventAction(s, eArgs, appGroupBox);
                            }
                            else if (control is LinkLabel linkLabel)
                            {
                                linkLabel.LinkClicked += (s, eArgs) => HandleDynamicEventAction(s, eArgs, appGroupBox);
                            }
                            else if (control is PictureBox clickablePictureBox)
                            {
                                clickablePictureBox.Click += (s, eArgs) => HandleDynamicEventAction(s, eArgs, appGroupBox);
                            }
                            // Add other clickable controls here as needed
                        }

                        appGroupBox.Controls.Add(control);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating/configuring control '{ci.Name}' of type '{ci.ControlType}': {ex.Message}");
                    }
                }
                appGroupBox.Padding = new Padding(10, 25, 10, 10);
            }

            return appGroupBox;
        }

        // Generic event handler for dynamic actions
        private static void HandleDynamicEventAction(object sender, EventArgs e, GroupBox parentGroupBox)
        {
            Control clickedControl = sender as Control;
            if (clickedControl == null) return;

            ControlInfo ci = clickedControl.Tag as ControlInfo;
            if (ci == null || string.IsNullOrEmpty(ci.EventAction)) return;

            string action = ci.EventAction.Trim();

            if (action.Equals("ShowMessageBox", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Action triggered by '{ci.Name}' ({ci.ControlType}).\nThis is a pre-defined action.", "Dynamic Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (action.StartsWith("ToggleVisibility:", StringComparison.OrdinalIgnoreCase))
            {
                string targetControlName = action.Substring("ToggleVisibility:".Length).Trim();
                // Find the target control within the same parent GroupBox
                Control[] foundControls = parentGroupBox.Controls.Find(targetControlName, true);
                if (foundControls.Length > 0)
                {
                    foundControls[0].Visible = !foundControls[0].Visible;
                    // Provide feedback
                    string visibility = foundControls[0].Visible ? "visible" : "hidden";
                    MessageBox.Show($"Control '{foundControls[0].Name}' visibility toggled to {visibility}.", "Visibility Toggle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Target control '{targetControlName}' not found for visibility toggle.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"Unknown dynamic action: '{action}' defined for '{ci.Name}'.", "Unknown Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}