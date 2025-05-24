using System;
using System.Windows.Forms;

namespace NATO_OS_7
{
    public class ScriptDialog : Form
    {
        // Property to hold the script text
        public string ScriptText { get; set; }

        // Constructor that takes initial script text
        public ScriptDialog(string initialScript)
        {
            ScriptText = initialScript; // Initialize the script text
            InitializeComponents();
        }

        // Method to initialize the form components (TextBox, etc.)
        private void InitializeComponents()
        {
            // Create the TextBox for script input
            TextBox scriptTextBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                Text = ScriptText
            };

            // Create the Submit button
            Button submitButton = new Button
            {
                Text = "Submit",
                Dock = DockStyle.Bottom
            };
            submitButton.Click += (sender, e) =>
            {
                ScriptText = scriptTextBox.Text; // Update the script text
            this.DialogResult = DialogResult.OK; // Close the form with OK
            this.Close();
            };

            // Add controls to the form
            this.Controls.Add(scriptTextBox);
            this.Controls.Add(submitButton);
        }
    }
}
