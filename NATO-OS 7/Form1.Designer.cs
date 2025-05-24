namespace NATO_OS_7
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.StartOSBTN = new System.Windows.Forms.Button();
            this.CloseOS = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.STARTOSBTN1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shutDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systeminfobox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.infook = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.systeminfobox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "NATO OS 7";
            // 
            // StartOSBTN
            // 
            this.StartOSBTN.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StartOSBTN.Font = new System.Drawing.Font("MS PGothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartOSBTN.Location = new System.Drawing.Point(0, 397);
            this.StartOSBTN.Name = "StartOSBTN";
            this.StartOSBTN.Size = new System.Drawing.Size(800, 53);
            this.StartOSBTN.TabIndex = 2;
            this.StartOSBTN.Text = "User";
            this.StartOSBTN.UseVisualStyleBackColor = true;
            this.StartOSBTN.Click += new System.EventHandler(this.StartOSBTN_Click);
            // 
            // CloseOS
            // 
            this.CloseOS.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseOS.Font = new System.Drawing.Font("MS PGothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseOS.Location = new System.Drawing.Point(697, 0);
            this.CloseOS.Name = "CloseOS";
            this.CloseOS.Size = new System.Drawing.Size(103, 397);
            this.CloseOS.TabIndex = 3;
            this.CloseOS.Text = "Shut Down";
            this.CloseOS.UseVisualStyleBackColor = true;
            this.CloseOS.Click += new System.EventHandler(this.CloseOS_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(316, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(381, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "DISPLAY RESOLUTION IS NOT PERFECT";
            // 
            // STARTOSBTN1
            // 
            this.STARTOSBTN1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.STARTOSBTN1.Font = new System.Drawing.Font("MS PGothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STARTOSBTN1.Location = new System.Drawing.Point(0, 344);
            this.STARTOSBTN1.Name = "STARTOSBTN1";
            this.STARTOSBTN1.Size = new System.Drawing.Size(697, 53);
            this.STARTOSBTN1.TabIndex = 6;
            this.STARTOSBTN1.Text = "Administrator";
            this.STARTOSBTN1.UseVisualStyleBackColor = true;
            this.STARTOSBTN1.Click += new System.EventHandler(this.STARTOSBTN1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.powerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(111, 48);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // powerToolStripMenuItem
            // 
            this.powerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shutDownToolStripMenuItem});
            this.powerToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.powerToolStripMenuItem.Name = "powerToolStripMenuItem";
            this.powerToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.powerToolStripMenuItem.Text = "Power";
            // 
            // shutDownToolStripMenuItem
            // 
            this.shutDownToolStripMenuItem.Name = "shutDownToolStripMenuItem";
            this.shutDownToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.shutDownToolStripMenuItem.Text = "Shut Down";
            this.shutDownToolStripMenuItem.Click += new System.EventHandler(this.shutDownToolStripMenuItem_Click);
            // 
            // systeminfobox
            // 
            this.systeminfobox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.systeminfobox.Controls.Add(this.label3);
            this.systeminfobox.Controls.Add(this.label9);
            this.systeminfobox.Controls.Add(this.label10);
            this.systeminfobox.Controls.Add(this.label11);
            this.systeminfobox.Controls.Add(this.label12);
            this.systeminfobox.Controls.Add(this.infook);
            this.systeminfobox.Location = new System.Drawing.Point(284, 156);
            this.systeminfobox.Name = "systeminfobox";
            this.systeminfobox.Size = new System.Drawing.Size(233, 138);
            this.systeminfobox.TabIndex = 19;
            this.systeminfobox.TabStop = false;
            this.systeminfobox.Text = "System Info";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS PGothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(49, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Display may not be perfect";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS PGothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "is protected by PWS";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS PGothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(219, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "The NATO OS Operating System\r\n";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS PGothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(163, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "©PostBook Dev - NATO";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS PGothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(175, 15);
            this.label12.TabIndex = 1;
            this.label12.Text = "NATO OS-7 Developer";
            // 
            // infook
            // 
            this.infook.Location = new System.Drawing.Point(6, 115);
            this.infook.Name = "infook";
            this.infook.Size = new System.Drawing.Size(44, 23);
            this.infook.TabIndex = 0;
            this.infook.Text = "Close";
            this.infook.UseVisualStyleBackColor = true;
            this.infook.Click += new System.EventHandler(this.infook_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.systeminfobox);
            this.Controls.Add(this.STARTOSBTN1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CloseOS);
            this.Controls.Add(this.StartOSBTN);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "NATO OS - Virtual Machine";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.systeminfobox.ResumeLayout(false);
            this.systeminfobox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartOSBTN;
        private System.Windows.Forms.Button CloseOS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button STARTOSBTN1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shutDownToolStripMenuItem;
        private System.Windows.Forms.GroupBox systeminfobox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button infook;
    }
}

