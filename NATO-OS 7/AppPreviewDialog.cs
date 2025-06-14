using System.Windows.Forms;
using System.Drawing;

namespace NATO_OS_App_Installer
{
    public partial class AppPreviewDialog : Form
    {
        public AppPreviewDialog(GroupBox appGroupBox)
        {
            InitializeComponent();
            this.Text = $"Preview: {appGroupBox.Text}";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Adjust dialog size to fit the groupbox plus some padding
            // Use the groupbox's actual AutoSize dimensions here
            this.ClientSize = new Size(appGroupBox.Width + 40, appGroupBox.Height + 60);

            // Position the groupbox in the center of the dialog
            appGroupBox.Location = new Point(20, 20);
            appGroupBox.Anchor = AnchorStyles.None;

            // Add the groupbox to the dialog's controls
            this.Controls.Add(appGroupBox);
        }
    }
}
