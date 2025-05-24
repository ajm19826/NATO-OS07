using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace NATO_OS_7
{
    public partial class Form1 : Form
    {
        private System.Drawing.Point dragStartPoint;
        private List<Image> images; // Stores the 4K images for the slideshow
        private int currentImageIndex = 0; // Current index in the image list
        private Timer imageSwitchTimer; // Timer to switch images
        private Timer fadeTimer; // Timer to handle fade effect
        private double fadeStep = 0.05; // Fade increment per tick
        private PictureBox pictureBox1, pictureBox2; // Two PictureBoxes for crossfading
        private bool isFadingIn = true; // Track fade direction
        private bool isDragging = false;
        public Form1()
        {
            InitializeComponent();

            systeminfobox.Hide();
            // Initialize image list with high-resolution images (at least 3840x2160)
            images = new List<Image>
        {
            Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\New.SystemIcons\\Lock\\1.jpg"), // Add your image paths
            Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\New.SystemIcons\\Lock\\2.jpeg"),
            Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\New.SystemIcons\\Lock\\3.jpg"),
            Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\New.SystemIcons\\Lock\\4.jpeg")
        };

            // Set form properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // Initialize two PictureBoxes for crossfade effect
            pictureBox1 = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom, // Keeps aspect ratio
                Image = images[0], // Start with first image
                BackColor = Color.Black
            };

            pictureBox2 = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = images[1], // Set next image in advance
                BackColor = Color.Black,
                Visible = false // Initially hidden
            };

            this.Controls.Add(pictureBox2);
            this.Controls.Add(pictureBox1);

            // Timer to switch images every 5 seconds
            imageSwitchTimer = new Timer();
            imageSwitchTimer.Interval = 5000;
            imageSwitchTimer.Tick += ImageSwitchTimer_Tick;
            imageSwitchTimer.Start();

            // Timer to fade images smoothly
            fadeTimer = new Timer();
            fadeTimer.Interval = 30; // Smooth fade every 30ms
            fadeTimer.Tick += FadeTimer_Tick;
            
            
                systeminfobox.BringToFront();

            
        }
        private void ImageSwitchTimer_Tick(object sender, EventArgs e)
        {
            systeminfobox.BringToFront();

            // Set the next image
            currentImageIndex = (currentImageIndex + 1) % images.Count;

            // Swap images between PictureBoxes for a smooth transition
            if (isFadingIn)
            {
                pictureBox2.Image = images[currentImageIndex];
                pictureBox2.Visible = true;
                pictureBox2.BringToFront();
            }
            else
            {
                pictureBox1.Image = images[currentImageIndex];
                pictureBox1.Visible = true;
                pictureBox1.BringToFront();
            }

            isFadingIn = !isFadingIn; // Swap which PictureBox fades in next
            fadeTimer.Start(); // Start fade effect
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            systeminfobox.BringToFront();

            try
            {
                if (isFadingIn)
                {
                    pictureBox2.Visible = true;
                    pictureBox2.BringToFront();
                    pictureBox2.Image = images[currentImageIndex];

                    // Reduce opacity effect (manual simulation)
                    pictureBox1.BackColor = Color.FromArgb((int)(pictureBox1.BackColor.A - (fadeStep * 255)), 0, 0, 0);

                    if (pictureBox1.BackColor.A <= 0)
                    {
                        pictureBox1.Visible = false;
                        fadeTimer.Stop();
                    }
                }
                else
                {
                    try
                    {
                        pictureBox1.Visible = true;
                        pictureBox1.BringToFront();
                        pictureBox1.Image = images[currentImageIndex];

                        pictureBox2.BackColor = Color.FromArgb((int)(pictureBox2.BackColor.A - (fadeStep * 255)), 0, 0, 0);
                        if (pictureBox2.BackColor.A <= 0)
                        {
                            pictureBox2.Visible = false;
                            fadeTimer.Stop();
                        }
                    }

                    catch (Exception ex)
                    {
                        ListBox exList = new ListBox { };
                        exList.Items.Add(ex);
                        exList.Visible = false;

                        
                    }
                }
            }
            catch (Exception ex)
            {
                ListBox exList = new ListBox { };
                exList.Items.Add(ex);
                exList.Visible = false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            systeminfobox.BringToFront();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void StartOSBTN_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(); // Create an instance of Form2
            form2.Show(); // Show Form2

            this.Hide(); // Hide Form1 (you can also use this.Close() if you want to close it completely)

        }

        private async void CloseOS_Click(object sender, EventArgs e)
        {
            this.Hide();
            MessageBox.Show("NATO OS-7 (Pilot) Exited with a code 0", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            this.Hide();
            player.URL = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/eff/START2.wav";
            player.controls.play();
            await Task.Delay(4000);
            Application.Exit();

        }

        private void STARTOSBTN1_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2(); // Create an instance of Form2
            form2.Show(); // Show Form2

            this.Hide(); // Hide Form1 (you can also use this.Close() if you want to close it completely)
        }

        private void shutDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            MessageBox.Show("NATO OS-7 (Pilot) Exited with a code 0", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        private void infook_Click(object sender, EventArgs e)
        {
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            systeminfobox.Show();
        }
        private void s_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }
        private void s_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void infook_Click_1(object sender, EventArgs e)
        {
            systeminfobox.Hide();

        }

        private void s_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                systeminfobox.Left += e.X - dragStartPoint.X;
                systeminfobox.Top += e.Y - dragStartPoint.Y;
            }
        }
    }



    }




