/*
// AppInstaller.cs
// Change this file to inherit from UserControl instead of Form.
// Adjust constructor and Initialize methods accordingly.

using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing; // Make sure System.Drawing is included for Color

namespace PostBookOneMedia.Servers.NATO.Host.AppPkgr
{
    // IMPORTANT: Change from Form to UserControl
    public partial class AppInstallerUserControl : UserControl
    {
        // Private fields for internal UI elements of the installer
        private GroupBox savePanel;
        private Label selectedGroupBoxLabel;
        private Button savePackageBtn;

        // Publicly accessible property to track the currently selected app GroupBox
        // This is what Form2.cs will interact with for saving/exporting.
        public GroupBox CurrentlySelectedAppGroupBox
        {
            get { return currentlySelectedGroupBox; }
        }
        private GroupBox currentlySelectedGroupBox; // Internal backing field

        // Internal panels to hold the loaded apps and the save controls
        private FlowLayoutPanel appsFlowPanel;
        private GroupBox lastSelectedGroupBoxVisual; // To manage visual feedback

        public AppInstallerUserControl()
        {
            InitializeUserControlUI();
            InitializeAppsArea(); // Initialize the panel where apps will be displayed
            InitializeSavePanel(); // Initialize the save panel

            // Adjust the layout of the panels within the UserControl
            // This ensures they are positioned correctly when the UserControl is added to Form2.cs
            appsFlowPanel.Location = new Point(10, 10); // Example positioning
            appsFlowPanel.Width = this.Width - 20 - (savePanel != null ? savePanel.Width : 0);
            appsFlowPanel.Height = this.Height - 20;
            appsFlowPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;

            if (savePanel != null)
            {
                savePanel.Location = new Point(this.Width - savePanel.Width - 10, 10);
                savePanel.Height = this.Height - 20;
                savePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            }

            // Add the panels to the UserControl's controls
            this.Controls.Add(appsFlowPanel);
            this.Controls.Add(savePanel);

            // Ensure the savePanel is on top if it overlaps
            savePanel.BringToFront();
        }

        // Renamed from InitializeInstallerUI as it's no longer a Form
        private void InitializeUserControlUI()
        {
            // Set basic properties for the UserControl itself
            this.AutoScroll = true;
            this.Size = new System.Drawing.Size(750, 550); // Give it a default size
            this.BackColor = System.Drawing.Color.WhiteSmoke; // Just for visibility
        }

        private void InitializeSavePanel()
        {
            savePanel = new GroupBox()
            {
                Text = "Save Package Panel",
                Width = 250,
                // Height will be set in constructor based on UserControl height
                Location = new System.Drawing.Point(0, 0), // Initial relative position, adjusted in constructor
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom // Anchor to the UserControl's right edge
            };

            selectedGroupBoxLabel = new Label()
            {
                Text = "Selected: None",
                Location = new System.Drawing.Point(20, 80),
                AutoSize = true
            };

            savePackageBtn = new Button()
            {
                Text = "Save Selected Package",
                Width = 200,
                Height = 40,
                Location = new System.Drawing.Point(20, 30)
            };
            savePackageBtn.Click += SavePackageBtn_Click; // Hook up the click event

            savePanel.Controls.Add(savePackageBtn);
            savePanel.Controls.Add(selectedGroupBoxLabel);
        }

        private void InitializeAppsArea()
        {
            appsFlowPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false,
                // Location and Size will be adjusted in constructor relative to UserControl
                Location = new Point(0, 0),
                Width = 480, // Example initial width
                Height = 500, // Example initial height
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = System.Drawing.Color.LightGray
            };
        }

        private void SavePackageBtn_Click(object sender, EventArgs e)
        {
            // This method can remain internal to the UserControl, or you can expose
            // an event for Form2.cs to handle the save if you prefer.
            if (currentlySelectedGroupBox != null)
            {
                TriggerSave(currentlySelectedGroupBox);
            }
            else
            {
                MessageBox.Show("Please Select A Groupbox to save.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private ContextMenuStrip BuildPackageContextMenu(GroupBox groupBox)
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripMenuItem saveOption = new ToolStripMenuItem("Save Package (Ctrl+Shift+Alt+P)");
            saveOption.Click += (s, e) => TriggerSave(groupBox);
            cms.Items.Add(saveOption);
            return cms;
        }

        private void TriggerSave(GroupBox groupBox)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "NATO Package Files|*.npkg",
                Title = "Save NATO OS Package",
                FileName = groupBox.Text.Replace(" ", "_") + ".npkg"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    RewritePkg.SaveGroupBoxToPackage(groupBox, sfd.FileName);
                    MessageBox.Show("Package saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving package: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Public method to be called from Form2.cs's Install button
        public void PerformInstallPackage()
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "NATO Package Files|*.npkg",
                Title = "Select a NATO OS Package"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GroupBox loadedApp = RewritePkg.LoadGroupBoxFromPackage(ofd.FileName);
                    if (loadedApp != null)
                    {
                        loadedApp.ContextMenuStrip = BuildPackageContextMenu(loadedApp);
                        AddAppGroupBox(loadedApp); // Add to internal FlowLayoutPanel & hook selection
                        MessageBox.Show($"Package '{loadedApp.Text}' loaded successfully!", "Load Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to load package. The file might be corrupted or not a valid NATO package.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading package: {ex.Message}\n\nDetails: {ex.StackTrace}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Public method to be called from Form2.cs's Retrieve button (if it's different from install)
        // Assuming "retrieve" also means loading a package
        public void PerformRetrievePackage()
        {
            PerformInstallPackage(); // For now, assuming retrieve is similar to install
            // If retrieve has different logic, implement it here
        }


        // Method to add the loaded GroupBox to the UserControl's FlowLayoutPanel
        private void AddAppGroupBox(GroupBox newGroupBox)
        {
            newGroupBox.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            newGroupBox.Margin = new Padding(5);

            newGroupBox.BackColor = SystemColors.Control; // Default background

            // Add event handlers for selection
            newGroupBox.Click += GroupBox_SelectionClick;
            foreach (Control control in newGroupBox.Controls)
            {
                control.Click += GroupBox_SelectionClick;
            }

            appsFlowPanel.Controls.Add(newGroupBox);
            newGroupBox.Show();
            appsFlowPanel.Show();
            appsFlowPanel.Visible = true;
        }

        private void GroupBox_SelectionClick(object sender, EventArgs e)
        {
            GroupBox clickedGroupBox = null;

            if (sender is GroupBox gb)
            {
                clickedGroupBox = gb;
            }
            else if (sender is Control ctrl && ctrl.Parent is GroupBox parentGb)
            {
                clickedGroupBox = parentGb;
            }

            if (clickedGroupBox != null)
            {
                if (lastSelectedGroupBoxVisual != null && lastSelectedGroupBoxVisual != clickedGroupBox)
                {
                    lastSelectedGroupBoxVisual.BackColor = SystemColors.Control;
                    lastSelectedGroupBoxVisual.Invalidate();
                }

                currentlySelectedGroupBox = clickedGroupBox;
                currentlySelectedGroupBox.BackColor = System.Drawing.Color.LightBlue; // Highlight selected
                currentlySelectedGroupBox.Invalidate();

                selectedGroupBoxLabel.Text = $"Selected: {currentlySelectedGroupBox.Text}";
            }
        }
    }
}
*/