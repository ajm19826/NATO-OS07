//NATO OS-7 CHECKED: 10-28-2025
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Add this at the top to use VBScript methods
using System.Net.Mail;
using System.Net;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Net.Http;
using System.ServiceModel.Syndication;
using Google.Apis.Translate.v2;
using Google.Apis.Services;
using Google.Apis.Translate.v2.Data;
using System.Media;
using WMPLib;
using AxSHDocVw;
using AxWMPLib;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using SharpGL;
using SharpGL.Enumerations;
using Point = System.Drawing.Point;
using Google;
using ManagedBass;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Xml;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using NATO_OS_7;
using Timer = System.Windows.Forms.Timer;
using Blender;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;
using System.Security.Cryptography;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using NAudio.Wave;
using WaveFileWriter = NAudio.Wave.WaveFileWriter;
using WaveFormat = NAudio.Wave.WaveFormat;
using NAudio.CoreAudioApi;
using NAudio.Wave.SampleProviders;
using Blender.Audio;
using Microsoft.Win32;
using System.Data.SqlClient;
using PostBookOneMedia.AuthLoginForm.Scope.FormTemplate.three;
using PostBookOneMedia.Servers.NATO.Build.DesignerCode.DesignerCodeUIAgent;
using PostBookOneMedia.Servers.NATO.Build.DesignerCode.GeminiAgentResponce;
using NATO_OS_App_Installer; // Ensure this matches your project's root namespace if different
using System.Text.Json;
using Newtonsoft.Json;


//using PostBookOneMedia.Servers.NATO.Host.AppPkgr;
namespace NATO_OS_7
{
    public partial class Form2 : Form

    {
        //App Retrieve
        private GroupBox appInstallerGroupBox;
        private TextBox txtAppUrl;
        private FlowLayoutPanel flowLayoutPanelApps;
        private AppInfo _selectedAppInfo; // This will now hold the full AppInfo including ControlInfo
        private string _lastLoadedPackagePath = string.Empty;
        private string _lastLoadedPackageUrl = string.Empty;
        //System Desktop Icons

        //BangLab

        private ToolStrip BeatLabtoolStrip;
        private ToolStripButton BeatLabrecordButton, BeatLabstopButton, BeatLabplayButton, BeatLabtrimButton, BeatLabvolumeButton, BeatLabexportButton;
        private Panel BeatLabAudioPanel;
        private HScrollBar BeatLabvScrollBar;
        private WaveFileWriter BeatLabwriter;
        private WasapiLoopbackCapture BeatLabcapture;
        private string BeatLaboutputFile;
        private MemoryStream BeatLabwaveData;
        private float BeatLabvolumeLevel = 1.0f;
        private int BeatLabtrimStart = 0, BeatLabtrimEnd = 0;
        //BeatLab Piano
        private Panel BeatLabPianoPanel;
        private Panel BeatLabWaveformPanel;
        private Button BeatLabEchoButton, BeatLabRecordButton, BeatLabSaveButton;
        private Dictionary<Rectangle, string> BeatLabWhiteKeys = new Dictionary<Rectangle, string>();
        private Dictionary<Rectangle, string> BeatLabBlackKeys = new Dictionary<Rectangle, string>();
        private List<float> BeatLabRecordedSamples = new List<float>();
        private bool BeatLabIsRecording = false;
        private List<ISampleProvider> BeatLabPlayedNotes = new List<ISampleProvider>();
        private bool BeatLabEchoEnabled = false;
        private Rectangle? BeatLabPressedKey = null;

        // Container for the ToolStrip
        //BangLab
        private string whenNATOStartup;
        private bool isSpeechEnabled = true; // Flag to track speech recognition state

        private bool isDesignerCodeInstalled = false;

        private SpeechRecognitionEngine NATOVoiceRecognizer;
        private SpeechSynthesizer NATOVoiceSynthesizer;
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int KEYEVENTF_KEYUP = 0x0002;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSE_MOVE_DISTANCE = 100;

        //IMPORTANT!!!
        private string sessionID = Guid.NewGuid().ToString(); // New ID every session

        //IMPORTANT!!!
        //Restore Points
        private string tempDir = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\FILES\\System Active\\$TempFiles";

        //Restore Points
        //Debug Mode
        private bool isDebugModeAvalible;
        private Dictionary<string, Point> controlPositions = new Dictionary<string, Point>();
        private Queue<Keys> keySequence = new Queue<Keys>();
        private Keys[] konamiCode = { Keys.ControlKey, Keys.Right, Keys.Left, Keys.Down };

        //Debug Mode
        private Panel sdfpanel;
        private bool isSandboxEnabled = false;

        private bool sandboxEnabled = false;
        private Dictionary<string, string> objectScripts = new Dictionary<string, string>(); // Store scripts

        //3D Egg
        //FPS COUNT
        private int frameCount = 0;
        private int fps = 0;
        private DateTime lastTime;
        //Tetris Code
        private AxWindowsMediaPlayer tetrisMusic;
        private Timer gameTimer;
        private int blockX = 4, blockY = 0;
        private const int TETRISgridSize = 30;
        private const int GridWidth = 10, GridHeight = 20;
        private int score = 0;
        private Color blockColor;
        private Random TETRISrandom;
        private GroupBox TetrisGameBox;
        private Button moveLeftButton, moveRightButton;
        private Label scoreLabel;
        private Panel gamePanel;
        private List<Point> placedBlocks = new List<Point>();
        private Dictionary<Point, Color> blockColors = new Dictionary<Point, Color>();
        private List<Point> currentShape;
        //Tetris Code
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetFocus(IntPtr hWnd);

        private const int SW_SHOW = 5;
        private const int SW_RESTORE = 9;

        private Process _runningProcess = null;
        int isPlanet = 1;
        private bool allowisPlanet;
        private bool isPlayerTurn = true; // Player starts first
        private Button[,] buttons = new Button[3, 3];
        private int streamHandle = 0;
        private Stopwatch UpTimeWindowsFormsPlayerBoxTimer = new Stopwatch(); // Timer for tracking visibility
        private Timer uiUpdateWindowsFormsPlayerTimer = new Timer();
        private int gridSize = 3; // 3x3 puzzle
        private int tileSize = 100; // Size of each tile
        private List<PictureBox> tiles = new List<PictureBox>();
        private Bitmap originalImage;
        private PictureBox emptyTile;
        private ListBox listBoxFeeds;
        private Timer _timer;
        private Random _random;
        public Point current = new Point();
        public Point old = new Point();
        public Graphics g;

        public Graphics graph;
        public Pen pyen = new Pen(Color.Black, 5);
        private Label lblMonthYear;
        private Button btnPreviousMonth;
        private Button btnNextMonth;
        private FlowLayoutPanel dayPanel;
        private DateTime currentDate;
        Bitmap surface;
        // Create a list to store image paths
        // Create a list to store image paths
        private List<string> imagePaths;
        private int currentImageIndex = 0;
        private Timer slideshowTimer;
        Socket sck;
        EndPoint epLocal, epRemote;
        byte[] buffer;

        VideoCaptureDevice frame;
        FilterInfoCollection Devices;

        private Process _ffmpegProcess;
        private Bitmap NATOPaintCanvas;
        private Graphics NATOPaintGraphics;
        private Pen NATOPaintPen = new Pen(Color.Black, 5);
        private bool NATOPaintDrawing = false;
        private Point NATOPaintLastPoint;
        private PictureBox NATOPaintPictureBox;
        private Button NATOPaintColorButton;
        private Button NATOPaintSaveButton;
        private Button NATOPaintZoomInButton;
        private Button NATOPaintZoomOutButton;
        private Button NATOPaintSelectButton;
        private Button NATOPaintShapesButton;
        private Panel NATOPaintShapePanel;
        private Label NATOPaintStatusLabel;
        private Rectangle NATOPaintSelectionBox;
        private bool NATOPaintSelecting = false;
        private float NATOPaintZoomFactor = 1.0f;
        private string NATOPaintSelectedTool = "None";

        //Drag
        Random random = new Random();
        //Windows Forms Player
        private int WFlinkLabelCount = 0; // Counter to keep track of added LinkLabels
        private int WFGroupBoxCount = 0; // Counter to keep track of added LinkLabels
        private int WFButtonCount = 0; // Counter to keep track of added LinkLabels
        private int WFpictureBoxCount = 0; // Counter to keep track of added PictureBoxes
        private int WFmediaPlayerCount = 0; // Counter to keep track of added players
        private int WtextBoxCount = 0; // Counter to keep track of added players
        static int WFformscount = 0;
        private bool isDragging = false;
        private Point startPoint;
        private Rectangle selectionRectangle = Rectangle.Empty;
        DateTime datetimevmwareregistered = DateTime.Now;
        private Point dragStartPoint;
        //Windows Forms Player Tool Context Menu Strip
        //Unit Converter Widget
        private ComboBox UnitConvertercmbFromUnit;
        private ComboBox UnitConvertercmbToUnit;
        private TextBox UnitConvertertxtInput;
        private Label UnitConverterlblResult;
        //Unit Converter Widget
        private VScrollBar NATOcmdvScrollBar = new VScrollBar();

        private Random Backrandom = new Random();
        private bool redrawBackground = false;
        private Bitmap backgroundBitmap; // To store the generated background
        private System.Timers.Timer TickbeepTimer;

        public Form2()
        {

            InitializeComponent();
            InitializeAppInstallerUI(); // Call the new method to set up the App Installer GroupBox

            //designer copilot
            designercopilotboxdesignercode.Hide();

            /*
            if (this.DebugWindowBrowserDesignerCode != null)
            {
                this.DebugWindowBrowserDesignerCode.Dock = System.Windows.Forms.DockStyle.Fill; // Fills the available space
                this.DebugWindowBrowserDesignerCode.ScriptErrorsSuppressed = true; // Suppress JavaScript errors
            }

            // Configure the TextBox for Send Message (CodeAreaDesignerCode)
            // Assuming CodeAreaDesignerCode is already defined in InitializeComponent().
            if (this.CodeAreaDesignerCode != null)
            {
                this.CodeAreaDesignerCode.Dock = System.Windows.Forms.DockStyle.Bottom; // Docks to the bottom
                this.CodeAreaDesignerCode.Multiline = true; // Allow multi-line input
                this.CodeAreaDesignerCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical; // Add vertical scrollbar if text exceeds height
                this.CodeAreaDesignerCode.Height = 100; // Set a fixed height for the input box (adjust as needed)
            }
            string htmlContent = Markdig.Markdown.ToHtml(copilotMarkdownAnswer);

            // Display the HTML in the WebBrowser control
            if (this.DebugWindowBrowserDesignerCode != null)
            {
                this.DebugWindowBrowserDesignerCode.DocumentText = htmlContent;
            }
            */
            //System Desktop Icons

            //System Desktop Icons
            // Initialize UI Components
            BeatLabtoolStrip = new ToolStrip();
            BeatLabrecordButton = new ToolStripButton("Record BeatLabwmp");
            BeatLabstopButton = new ToolStripButton("Stop");
            BeatLabplayButton = new ToolStripButton("Play");
            BeatLabtrimButton = new ToolStripButton("Trim");
            BeatLabvolumeButton = new ToolStripButton("Adjust Volume");
            BeatLabexportButton = new ToolStripButton("Export");

            BeatLabtoolStrip.Items.AddRange(new ToolStripItem[] { BeatLabrecordButton, BeatLabstopButton, BeatLabplayButton, BeatLabtrimButton, BeatLabvolumeButton, BeatLabexportButton });
            // Initialize the horizontal scrollbar
            HScrollBar BeatLabhScrollBar = new HScrollBar
            {
                Dock = DockStyle.Bottom
            };



            // Add both scrollbars to the BeatLabAudioPanel

            // Initialize Audio Panel
            BeatLabAudioPanel = new Panel
            {
                Location = new Point(10, 40), // Inside BandLabSettings
                Size = new Size(206, 300), // Adjusted to fit inside GroupBox
                AutoScroll = true,
                BackColor = Color.Black
            };

            // Initialize ScrollBar
            BeatLabvScrollBar = new HScrollBar
            {
                Dock = DockStyle.Bottom // Keep it inside Audio Panel
            };

            // Add controls inside BandLabSettings
            bandlabsettings.Controls.Add(BeatLabtoolStrip);
            bandlabsettings.Controls.Add(BeatLabAudioPanel);
            bandlabsettings.Controls.Add(BeatLabvScrollBar);

            // Set toolstrip docking
            BeatLabtoolStrip.Dock = DockStyle.Top;

            // Add BandLabSettings to the main form

            // Event Handlers
            BeatLabrecordButton.Click += StartRecording;
            BeatLabstopButton.Click += StopRecording;
            BeatLabplayButton.Click += PlayAudio;
            BeatLabtrimButton.Click += TrimAudio;
            BeatLabvolumeButton.Click += AdjustVolume;
            BeatLabexportButton.Click += ExportAudio;
            BeatLabAudioPanel.Controls.Add(BeatLabhScrollBar);
            //BeatLab Piano
            BeatLabPianoPanel = new Panel { Size = new Size(636, 200), Location = new Point(10, 20), BackColor = Color.Gray };
            BeatLabPianoPanel.MouseDown += BeatLabPianoMouseDown;
            BeatLabPianoPanel.MouseUp += BeatLabPianoMouseUp;
            BeatLabPianoPanel.Paint += BeatLabPianoPaint;
            BeatLabPianoBox.Controls.Add(BeatLabPianoPanel);

            BeatLabWaveformPanel = new Panel { Size = new Size(636, 100), Location = new Point(10, 230), BackColor = Color.Black };
            BeatLabWaveformPanel.Paint += BeatLabDrawWaveform;
            BeatLabPianoBox.Controls.Add(BeatLabWaveformPanel);

            BeatLabEchoButton = new Button { Text = "Echo", Location = new Point(10, 340), Size = new Size(100, 30) };
            BeatLabEchoButton.Click += (s, e) => BeatLabEchoEnabled = !BeatLabEchoEnabled;
            BeatLabPianoBox.Controls.Add(BeatLabEchoButton);

            BeatLabRecordButton = new Button { Text = "Record", Location = new Point(120, 340), Size = new Size(100, 30) };
            BeatLabRecordButton.Click += BeatLabToggleRecording;
            BeatLabPianoBox.Controls.Add(BeatLabRecordButton);

            BeatLabSaveButton = new Button { Text = "Save", Location = new Point(230, 340), Size = new Size(100, 30) };
            BeatLabSaveButton.Click += BeatLabSaveRecording;
            BeatLabPianoBox.Controls.Add(BeatLabSaveButton);

            BeatLabDefinePianoKeys();
            //BeatLab
            TickbeepTimer = new System.Timers.Timer(1000);
            TickbeepTimer.Elapsed += (s, e) => Console.Beep();
            whenNATOStartup = "" + DateTime.Now;
            Directory.CreateDirectory(tempDir);

            InitializeCustomComponents();
            sneakydeakyfeaturesselectionbox.Hide();
            EnableSandboxing(this);  // Pass the form (or any container) as the parameter
            WebBrowser1.ScriptErrorsSuppressed = false;
            // Initialize the timer
            fpsTimer.Interval = 1000; // Update every second
            fpsTimer.Tick += FpsTimer_Tick;
            fpsTimer.Start();

            // Initialize frame count and last time
            lastTime = DateTime.Now;
            NATOcmdvScrollBar.Maximum = natocmd.Lines.Length;
            NATOcmdvScrollBar.Value = NATOcmdvScrollBar.Maximum;
            tutorialsbox.Hide();
            this.KeyPreview = true; // Allows the form to capture key events before controls

            //Unit Converter Code
            Label lblTitle = new Label() { Text = "Enter value:", AutoSize = true, Top = 20, Left = 10 };
            UnitConvertertxtInput = new TextBox() { Top = 40, Left = 10, Width = 100 };

            UnitConvertercmbFromUnit = new ComboBox() { Top = 70, Left = 10, Width = 100 };
            UnitConvertercmbFromUnit.Items.AddRange(new string[] { "km", "miles", "feet", "inches", "yards", "centimeters", "millimeters", "nautical miles", "light years", "parsecs" });
            UnitConvertercmbFromUnit.SelectedIndex = 0;

            UnitConvertercmbToUnit = new ComboBox() { Top = 70, Left = 120, Width = 75 };
            UnitConvertercmbToUnit.Items.AddRange(new string[] { "km", "miles", "feet", "inches", "yards", "centimeters", "millimeters", "nautical miles", "light years", "parsecs" });
            UnitConvertercmbToUnit.SelectedIndex = 0;

            Button btnConvert = new Button() { Text = "Convert", Top = 100, Left = 10, Width = 170 };
            UnitConverterlblResult = new Label() { Text = "Result: ", AutoSize = true, Top = 130, Left = 10 };
            btnConvert.Click += ConvertUnits;
            natocmd.KeyDown += natocmd_KeyDown;
            RadioWidgetNATO.Controls.Add(lblTitle);
            RadioWidgetNATO.Controls.Add(UnitConvertertxtInput);
            RadioWidgetNATO.Controls.Add(UnitConvertercmbFromUnit);
            RadioWidgetNATO.Controls.Add(UnitConvertercmbToUnit);
            RadioWidgetNATO.Controls.Add(btnConvert);
            RadioWidgetNATO.Controls.Add(UnitConverterlblResult);
            //Unit Converter Code
            //NATO PAINT CODE
            NATOPAINTBOX.Text = "NATOPaint";
            NATOPAINTBOX.Size = new Size(1000, 700);
            NATOPaintCanvas = new Bitmap(1000, 700);
            NATOPaintGraphics = Graphics.FromImage(NATOPaintCanvas);
            NATOPaintGraphics.Clear(Color.White);
            NATOPaintPictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = NATOPaintCanvas,
                Cursor = Cursors.Cross
            };
            NATOPaintPictureBox.MouseDown += NATOPaintMouseDown;
            NATOPaintPictureBox.MouseMove += NATOPaintMouseMove;
            NATOPaintPictureBox.MouseUp += NATOPaintMouseUp;
            NATOPaintPictureBox.Paint += NATOPaintPictureBoxPaint;
            NATOPaintPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            NATOPAINTBOX.Controls.Add(NATOPaintPictureBox);

            FlowLayoutPanel topPanel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
            NATOPAINTBOX.Controls.Add(topPanel);

            NATOPaintColorButton = new Button { Text = "Change Color" };
            NATOPaintColorButton.Click += NATOPaintChangeColor;
            topPanel.Controls.Add(NATOPaintColorButton);

            NATOPaintSaveButton = new Button { Text = "Save Image" };
            NATOPaintSaveButton.Click += NATOPaintSaveImage;
            topPanel.Controls.Add(NATOPaintSaveButton);

            NATOPaintZoomInButton = new Button { Text = "Zoom In" };
            NATOPaintZoomInButton.Click += NATOPaintZoomIn;
            topPanel.Controls.Add(NATOPaintZoomInButton);

            NATOPaintZoomOutButton = new Button { Text = "Zoom Out" };
            NATOPaintZoomOutButton.Click += NATOPaintZoomOut;
            topPanel.Controls.Add(NATOPaintZoomOutButton);

            NATOPaintSelectButton = new Button { Text = "Select" };
            NATOPaintSelectButton.Click += NATOPaintStartSelection;
            topPanel.Controls.Add(NATOPaintSelectButton);

            NATOPaintShapesButton = new Button { Text = "Shapes" };
            NATOPaintShapesButton.Click += NATOPaintShowShapesPanel;
            topPanel.Controls.Add(NATOPaintShapesButton);

            NATOPaintShapePanel = new Panel { Dock = DockStyle.Top, Height = 50, Visible = false };
            NATOPAINTBOX.Controls.Add(NATOPaintShapePanel);
            string[] shapes = { "Square", "Triangle", "Circle", "Line", "Star" };
            foreach (var shape in shapes)
            {
                Button shapeButton = new Button { Text = shape };
                shapeButton.Click += (s, e) => NATOPaintDrawShape(shape);
                NATOPaintShapePanel.Controls.Add(shapeButton);
            }

            NATOPaintStatusLabel = new Label { Dock = DockStyle.Bottom, Text = "Ready", Height = 30, TextAlign = ContentAlignment.MiddleLeft };
            NATOPAINTBOX.Controls.Add(NATOPaintStatusLabel);
            //NATO PAINT CODE
            sdfpanel = new Panel() { Text = "", Height = 9999, Width = 99999 };
            this.Controls.Add(sdfpanel);

            sdfpanel.Hide();
            //TETRIS CODE



            TetrisGameBox = new GroupBox();
            TetrisGameBox.Text = "TetrisGameBox";
            TetrisGameBox.Size = new Size(300, 650);
            TetrisGameBox.Location = new Point(10, 40);
            this.Controls.Add(TetrisGameBox);

            TetrisGameBox.BackColor = Color.White;
            scoreLabel = new Label();
            scoreLabel.Text = "Score: 0";
            scoreLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            scoreLabel.Location = new Point(10, 10);
            scoreLabel.AutoSize = true;
            scoreLabel.BackColor = Color.White;
            scoreLabel.ForeColor = Color.Black;
            TetrisGameBox.Controls.Add(scoreLabel);
            gamePanel = new Panel();
            gamePanel.Size = new Size(300, 550);
            gamePanel.Location = new Point(0, 20);
            gamePanel.Paint += GamePanel_Paint;
            TetrisGameBox.Controls.Add(gamePanel);

            moveLeftButton = new Button();
            moveLeftButton.Text = "Move Left";
            moveLeftButton.Location = new Point(50, 580);
            moveLeftButton.Click += MoveLeft;
            TetrisGameBox.Controls.Add(moveLeftButton);

            moveRightButton = new Button();
            moveRightButton.Text = "Move Right";
            moveRightButton.Location = new Point(180, 580);
            moveRightButton.Click += MoveRight;
            TetrisGameBox.Controls.Add(moveRightButton);

            gameTimer = new Timer();
            gameTimer.Interval = 500;
            gameTimer.Tick += GameLoop;

            TETRISrandom = new Random();
            blockColor = GetRandomColor();
            GenerateNewShape();
            ContextMenuStrip tetrisMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem tetrisHideMenuStrip = new ToolStripMenuItem("Close Tetris");
            tetrisMenuStrip.Items.Add(tetrisHideMenuStrip);
            TetrisGameBox.ContextMenuStrip = tetrisMenuStrip;
            tetrisHideMenuStrip.Click += (s, args) =>
            {
                try
                {
                    TetrisGameBox.Visible = false;
                    tetrisMusic.Ctlcontrols.stop();
                    gameTimer.Stop();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            };
            TetrisGameBox.Visible = false;

            //TETRIS CODE
            syslabel.BackColor = Color.Transparent;
            currentDate = DateTime.Now;

            InitializeRSSWidget();
            uiUpdateWindowsFormsPlayerTimer.Interval = 100; // Update every 100 milliseconds
            uiUpdateWindowsFormsPlayerTimer.Tick += UiUpdateWindowsFormsPlayerTimer_Tick;
            // Initialize image list

            imagePaths = new List<string>
        {
            @"C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/New.SystemIcons/Widgets/gwSlideshow/slide1.jpg",
            @"C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/New.SystemIcons/Widgets/gwSlideshow/slide2.jpg",
            @"C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/New.SystemIcons/Widgets/gwSlideshow/slide3.jpg",
            @"C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/New.SystemIcons/Widgets/gwSlideshow/slide4.jpg",
            @"C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/New.SystemIcons/Widgets/gwSlideshow/slide5.jpg",
            // Add paths to your images
        };
            SubscribeMouseEvents(blender3dgamebox);
            SubscribeMouseEvents(TetrisGameBox);
            // Set up PictureBox

            PictureBox pictureBox = new PictureBox();
            pictureBox.Parent = ImageSlideshowWidgetNATO;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Name = "slideshowPictureBox";

            // Set up Timer for slideshow
            slideshowTimer = new Timer();
            slideshowTimer.Interval = 2000; // Change image every 2 seconds
            slideshowTimer.Tick += SlideshowTimer_Tick;
            slideshowTimer.Start();
            mypointsfishgame.Hide();
            aipointsfishgame.Hide();
            GameWidgetNATO.Hide();
            ContextMenuStrip CompcontextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem changeCompName = new ToolStripMenuItem("Change Computer Name");
            CompcontextMenuStrip.Items.Add(changeCompName);

            changeCompName.Click += (s, args) =>
            {
                using (Form ComppasswordForm = new Form())
                {
                    ComppasswordForm.Text = "Change Computer Name";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Name:", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        syslabel.Text = compDialogtextBox.Text;
                    }
                }
            };
            syslabel.ContextMenuStrip = CompcontextMenuStrip;
            PuzzlePictureWidgetNATO.Text = "Daily Puzzle Picture";
            // Enable dragging for the GroupBox
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.URL = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/eff/START1.wav";
            player.controls.play();
            ClockWidgetNATO.MouseDown += ClockWidgetNATO_MouseDown;
            ClockWidgetNATO.MouseMove += ClockWidgetNATO_MouseMove;
            ClockWidgetNATO.MouseUp += ClockWidgetNATO_MouseUp;
            ImageSlideshowWidgetNATO.MouseDown += ImageSlideshowWidget_MouseDown;
            ImageSlideshowWidgetNATO.MouseMove += ImageSlideshowWidget_MouseMove;
            ImageSlideshowWidgetNATO.MouseUp += ImageSlideshowWidget_MouseUp;
            CodeSnippetWidgetNATO.MouseDown += CodeSnippetNATO_MouseDown;
            CodeSnippetWidgetNATO.MouseMove += CodeSnippetNATO_MouseMove;
            CodeSnippetWidgetNATO.MouseUp += CodeSnippetNATO_MouseUp;
            GameWidgetNATO.MouseDown += GameWidgetNATO_MouseDown;
            GameWidgetNATO.MouseMove += GameWidgetNATO_MouseMove;
            GameWidgetNATO.MouseUp += GameWidgetNATO_MouseUp;
            RSSWidgetNATO.MouseDown += RSSWidgetNATO_MouseDown;
            RSSWidgetNATO.MouseMove += RSSWidgetNATO_MouseMove;
            RSSWidgetNATO.MouseUp += RSSWidgetNATO_MouseUp;
            MediaWidgetNATO.MouseDown += MediaWidgetNATO_MouseDown;
            MediaWidgetNATO.MouseMove += MediaWidgetNATO_MouseMove;
            MediaWidgetNATO.MouseUp += MediaWidgetNATO_MouseUp;
            PuzzlePictureWidgetNATO.MouseDown += PuzzlePictureWidgetNATO_MouseDown;
            PuzzlePictureWidgetNATO.MouseMove += PuzzlePictureWidgetNATO_MouseMove;
            PuzzlePictureWidgetNATO.MouseUp += PuzzlePictureWidgetNATO_MouseUp;
            stickynotewidgetnato.MouseDown += stickynotewidgetnato_MouseDown;
            stickynotewidgetnato.MouseMove += stickynotewidgetnato_MouseMove;
            stickynotewidgetnato.MouseUp += stickynotewidgetnato_MouseUp;

            NATODesignerComboBox.MouseDown += NATODesignerComboBox_MouseDown;
            NATODesignerComboBox.MouseMove += NATODesignerComboBox_MouseMove;
            NATODesignerComboBox.MouseUp += NATODesignerComboBox_MouseUp;
            CalendarWidgetNATO.MouseDown += CalendarWidgetNATO_MouseDown;
            CalendarWidgetNATO.MouseMove += CalendarWidgetNATO_MouseMove;
            CalendarWidgetNATO.MouseUp += CalendarWidgetNATO_MouseUp;
            //Calculator Drag
            Calculator.MouseDown += Calculator_MouseDown;
            Calculator.MouseMove += Calculator_MouseMove;
            Calculator.MouseUp += Calculator_MouseUp;

            this.DoubleBuffered = true; // Reduce flickering
            timer1.Interval = 1000;
            windowsformsDesignerCode.Enabled = true;

            timer1.Tick += timer1_Tick;
            timer1.Start();
            ClockWidgetNATO.Paint += ClockWidgetNATO_Paint;
            CalendarWidgetNATO.Hide();
            ClockWidgetNATO.Hide();
            ImageSlideshowWidgetNATO.Hide();
            CodeSnippetWidgetNATO.Hide();
            CodeSnippetTxt.Show();
            RSSWidgetNATO.Hide();
            DesignerCodeWindowsFormsPlayer.Hide();
            PuzzlePictureWidgetNATO.Hide();
            MediaWidgetNATO.Hide();
            stickynotewidgetnato.Hide();
            // Label for Month/Year
            lblMonthYear = new Label();
            lblMonthYear.Font = new Font("Arial", 10);
            lblMonthYear.TextAlign = ContentAlignment.MiddleCenter;
            lblMonthYear.Dock = DockStyle.Top;
            lblMonthYear.Height = 30;
            CalendarWidgetNATO.Controls.Add(lblMonthYear);

            // Previous Month Button
            btnPreviousMonth = new Button();
            btnPreviousMonth.Text = "<";
            btnPreviousMonth.Dock = DockStyle.Left;
            btnPreviousMonth.Width = 30;
            btnPreviousMonth.Click += BtnPreviousMonth_Click;
            CalendarWidgetNATO.Controls.Add(btnPreviousMonth);
            DebugWindowsFormsPlayerBox.Hide();
            // Next Month Button
            btnNextMonth = new Button();
            btnNextMonth.Text = ">";
            btnNextMonth.Dock = DockStyle.Right;
            btnNextMonth.Width = 30;
            btnNextMonth.Click += BtnNextMonth_Click;
            CalendarWidgetNATO.Controls.Add(btnNextMonth);

            // FlowLayoutPanel to hold the days of the month
            dayPanel = new FlowLayoutPanel();
            dayPanel.Dock = DockStyle.Fill;
            dayPanel.WrapContents = false;
            CalendarWidgetNATO.Controls.Add(dayPanel);

            // Add calendar to the form
            Controls.Add(CalendarWidgetNATO);

            // Load the current month
            LoadCalendar();
            // Initialize components
            _random = new Random();
            _timer = new Timer
            {
                Interval = 10000 // 10 seconds
            };

            _timer.Start();

            UpdateWeather();
            this.ContextMenuStrip = RightClickMenuNATO;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true; // Reduce flickering
            this.MouseDown += OnMouseDown;
            this.MouseMove += OnMouseMove;
            this.MouseUp += OnMouseUp;
            this.Paint += OnPaint;
            this.KeyDown += OnThisKeyDown;
            this.KeyUp += OnThisKeyUp;
            this.MouseClick += OnThisMouseClick;
            GameWidgetNATO.BackgroundImageLayout = ImageLayout.Stretch;
            RegisteredTimeForVmwareTXT.Text = "" + datetimevmwareregistered;
            GoogleMapsMapApp.ScriptErrorsSuppressed = true;
            widgetsboxselect.Hide();
            createarestorepointrestoreorresetbtn.Enabled = false;
            VisualStudioLinkBrowser.Navigate("https://vscode.dev");
            VisualStudioLinkBrowser.ScriptErrorsSuppressed = true;
            IE_Browser.ScriptErrorsSuppressed = true;
            //New ContextMenuStrips
            //Chart
            // Create ContextMenuStrip
            ContextMenuStrip chartmenu = new ContextMenuStrip();
            // Create menu items
            ToolStripMenuItem chartmenuitem1 = new ToolStripMenuItem("Create Point");
            chartmenuitem1.Enabled = false;

            // Add event handlers
            chartmenuitem1.Click += (s, e) => MessageBox.Show("Error", "Create Point", MessageBoxButtons.OK, MessageBoxIcon.Error);


            // Add items to the context menu
            chartmenu.Items.Add(chartmenuitem1);


            // Assign ContextMenuStrip to a control
            chart1.ContextMenuStrip = chartmenu;
            //Blender API
            //End Blender API
            restoreorresetbox.Hide();
            pass.PasswordChar = '*';
            powerpointbox.Hide();
            createNewFileToolStripMenuItem.Enabled = true;
            g = canvasPanel.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pyen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            surface = new Bitmap(canvasPanel.Height, canvasPanel.Height);
            graph = Graphics.FromImage(surface);
            canvasPanel.BackgroundImage = surface;
            canvasPanel.BackgroundImageLayout = ImageLayout.None;
            pyen.Width = (float)numericUpDown1.Value;
            findtextapp.Hide();
            PasswordBoxNatoDesigner1.PasswordChar = '*';
            NatoDesignerTXT1.Hide();
            NatoDesignerSetStatusOnline.Hide();
            NatoDesignerSignInMain.Hide();
            WelcomeStartupBox.Show();
            NatoDesignerSignInBox.Hide();
            RecordBox.Hide();

            //Items Hide
            BeatLabProjectBox.Hide();
            fboostoptimizer.Hide();
            groupBox1.Hide();
            systeminfobox.Hide();
            browserBox.Hide();
            editorbox.Hide();
            groupBox2.Hide(); //NOTEPAD
            powerbox.Hide();
            passwordmanagerbox.Hide();
            filesbox.Hide();
            customapp.Hide();
            settingsbox.Hide();
            cachecomplete.Hide();
            taskmgr.Text = "Task Manager";
            rdpbox.Hide();
            shopapp.Hide();
            LabelVarShared.Hide();
            LabelVarShared.Text = "undefined";
            AuthenticPlanner.Hide();
            registryeditorbox.Hide();
            //Apps Hide
            //Premium Player
            premiumplayerimg.Hide();
            premiumplayertxt.Hide();
            uninstallpremiumplayer.Hide();
            openpremiumplayerbtn.Hide();
            //File PDF Reader
            filepdfreaderimg.Hide();
            filepdfreadertxt.Hide();
            uninstallfilepdfreader.Hide();
            openfilepdfreaderbtn.Hide();
            //Browser+
            browserplusimg.Hide();
            browserplustxt.Hide();
            uninstallbrowserplus.Hide();
            openbrowserplusbtn.Hide();
            //All Apps Box
            AllAppsBox.Hide();
            //YourMail
            yourmailimg.Hide();
            yourmailtxt.Hide();
            uninstallyourmail.Hide();
            openyourmail.Hide();
            //StylusPad
            styluspadimg.Hide();
            styluspadtxt.Hide();
            uninstallstyluspad.Hide();
            openstyluspad.Hide();
            //Amhst Paint
            amhstpaintimg.Hide();
            amhstpainttxt.Hide();
            uninstallamhstpaint.Hide();
            openamhstpaint.Hide();
            AmhstVideoPlayerandCamcorderPlusBox.Hide();
            //Amhst video player
            amhstvideoplayer.Hide();
            amhstvideoplayertxt.Hide();
            uninstallamhstvideoplayer.Hide();
            openamhstvideoplayer.Hide();
            //Camcorder plus
            camcorderplusimg.Hide();
            camcorderplustxt.Hide();
            uninstallcamcorderplus.Hide();
            opencamcorderplus.Hide();
            //iMailling
            imailingimg.Hide();
            imailingtxt.Hide();
            uninstallimailling.Hide();
            openimailling.Hide();
            //NoteNote
            notenoteimg.Hide();
            notenotetxt.Hide();
            uninstallnotenote.Hide();
            opennotenote.Hide();
            NoteNoteandImaillingBox.Hide();
            //Page Index Apps
            InstallAppPageIndex.Text = "Page 1/2";
            page2library.Hide();
            bottomhalfNotSafeForScripting.Hide();
            chromiumbox.Hide();
            spotifybox.Hide();
            backgroundchangebox.Hide();
            sketchplusbox.Hide();
            application.Hide();

            //Chromium (App)
            ChromiumBookmarksBar.Hide();
            ChromiumMenu.Hide();
            ChromiumFavoriteIcon.Hide();
            ChromiumFowardBtn.Hide();
            ChromiumWebBrowser.Hide();
            ChromiumReloadBtn.Hide();
            ChromiumBackBtn.Hide();
            ChromiumSearchBarIMG.Hide();
            ChromiumSettingsBtn.Hide();
            ChromiumSearchBarLookupBtn.Hide();
            ChromiumAppSearchBar1.Hide();
            notificationsboxNATO.Hide();
            DownloadNatoDesigner.Hide();
            //Sketch Plus
            sketchplustxt.Hide();

            //Spotify
            SpotifyIMG.Hide();
            SpotifyLabel1.Hide();
            //Mail
            MailBox.Hide();
            NatoDesignerSetupWizard.Hide();
            NATODesignerComboBox.Hide();
            NATODesigner.Hide();
            AuthenticMessengerApp.Hide();
            AuthenticNotePad.Hide();
            AuthenticMovieViewer.Hide();
            BugsListNATO.Hide();
            SetStatusNatoDesignerSignIn.Enabled = false;
            AuthenticPhoneLinkBox.Hide();
            //Phone Link
            CallNatoDesigner.Enabled = false;
            PhoneBookNatoDesigner.Enabled = false;
            MissedCallsNatoDesigner.Enabled = false;
            InfoNatoDesigner.Enabled = false;
            NumberListPhoneLink.Enabled = false;
            TestCallBtnPhoneLink.Enabled = false;
            //Designer Code
            DesignerCode.Hide();
            DebugWindowDesignerCode.Hide();
            Console.Write("OS: NATO OS-7 Sucessfully started running, see app menu.");
            //NATO Developers
            NATODevelopersApp.Hide();
            //Other Apps
            AODesignerBox.Hide();
            appstatisticsbox.Hide();
            backuprestorebox.Hide();
            cameraapp.Hide();
            Calculator.Hide();
            designertextbox.Hide();
            listBoxEventViewer.Items.Add("NATO OS-7 Started with 0 exceptions");
            eventviewerbox.Hide();
            EINKPAD.Hide();
            GamesBox.Hide();
            InternetExplorerBox.Hide();
            ixviewbox.Hide();
            ixviewString1.Text = "Hidden Network";
            ixviewString2.Text = "Fully Connected";
            disconnectedbtnixview.Enabled = true;
            MediaPlayerApp.Hide();
            diagnosenetworkixview.Enabled = false;
            MagnifierBox.Hide();
            PrintToPDFBox.Hide();
            blenderbox.Hide();
            blenderloadingassetsbox.Hide();
            blendercityviewdialogbox.Hide();
            textBox2.Text = "NATO OS-7 NOTEPAD";
            excelbox.Hide();
            microsoftwordbox.Hide();
            microsoftpublisherapp.Hide();
            VisualStudioCodeBoxMicrosoft.Hide();
            CallVideoPhoneLink.BackgroundImageLayout = ImageLayout.Zoom;
            //Usage Chart
            usagechartbox.Hide();
            chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
            enable2DToolStripMenuItem.Enabled = false;
            enable3DToolStripMenuItem.Enabled = true;
            gameBoardPlayAGame.Hide();
            OpenSlateBox.Hide();
            vmwarebox.Hide();
            weatherBox.Hide();
            NATOPAINTBOX.Hide();
            BeatLabPianoBox.Hide();
            //Fish Game
            GameWidgetNATO.Click += GameWidgetNATO_Click;

            GameWidgetNATO.BackgroundImage = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/StartScreen.png");
            RadioWidgetNATO.Hide();
            //SubscribeMouseEvents(appInstallerGroupBox);
            SubscribeMouseEvents(groupBox2);
            SubscribeMouseEvents(DesignerCodeWindowsFormsPlayer);
            SubscribeMouseEvents(AllAppsBox);
            SubscribeMouseEvents(amhstpaintratingbox);
            SubscribeMouseEvents(YourMailandstylusPadBox);
            SubscribeMouseEvents(AmhstVideoPlayerandCamcorderPlusBox);
            SubscribeMouseEvents(AODesignerBox);
            SubscribeMouseEvents(application);
            SubscribeMouseEvents(AppPremiumPlayer_BackgroundChooserBOX);
            SubscribeMouseEvents(AuthenticMessengerApp);
            SubscribeMouseEvents(AuthenticMovieViewer);
            SubscribeMouseEvents(shopapp);
            SubscribeMouseEvents(AuthenticNotePad);
            SubscribeMouseEvents(AuthenticPhoneLinkBox);
            SubscribeMouseEvents(AuthenticPlanner);
            SubscribeMouseEvents(backgroundchangebox);
            SubscribeMouseEvents(blenderbox);
            SubscribeMouseEvents(backuprestorebox);
            SubscribeMouseEvents(blenderloadingassetsbox);
            SubscribeMouseEvents(browserBox);
            SubscribeMouseEvents(BugsListNATO);
            SubscribeMouseEvents(cameraapp);
            SubscribeMouseEvents(customapp);
            SubscribeMouseEvents(DebugWindowDesignerCode);
            SubscribeMouseEvents(DesignerCode);
            SubscribeMouseEvents(NATOPAINTBOX);
            SubscribeMouseEvents(NATODevelopersApp);
            SubscribeMouseEvents(designertextbox);
            SubscribeMouseEvents(editorbox);
            SubscribeMouseEvents(EINKPAD);
            SubscribeMouseEvents(eventviewerbox);
            SubscribeMouseEvents(excelbox);
            SubscribeMouseEvents(fboostoptimizer);
            SubscribeMouseEvents(filesbox);
            SubscribeMouseEvents(findtextapp);
            SubscribeMouseEvents(tutorialsbox);
            SubscribeMouseEvents(widgetsboxselect);
            SubscribeMouseEvents(GamesBox);
            SubscribeMouseEvents(MagnifierBox);
            SubscribeMouseEvents(InternetExplorerBox);
            SubscribeMouseEvents(ixviewbox);
            SubscribeMouseEvents(RadioWidgetNATO);
            SubscribeMouseEvents(MailBox);
            SubscribeMouseEvents(MediaPlayerApp);
            SubscribeMouseEvents(MediaPlayerBox);
            SubscribeMouseEvents(microsoftpublisherapp);
            SubscribeMouseEvents(microsoftwordbox);
            SubscribeMouseEvents(NatoDesignerSignInBox);
            SubscribeMouseEvents(NoteNoteandImaillingBox);
            SubscribeMouseEvents(OpenFileandPdfReaderandBrowserPlusBox);
            SubscribeMouseEvents(OpenSlateBox);
            SubscribeMouseEvents(passwordmanagerbox);
            SubscribeMouseEvents(powerbox);
            SubscribeMouseEvents(powerpointbox);
            SubscribeMouseEvents(PrintToPDFBox);
            //SubscribeMouseEvents(rdpbox); Accidental 3-11-2025
            SubscribeMouseEvents(RecordBox);
            SubscribeMouseEvents(registryeditorbox);
            SubscribeMouseEvents(restoreorresetbox);
            SubscribeMouseEvents(sketchplusbox);
            SubscribeMouseEvents(spotifybox);
            SubscribeMouseEvents(systeminfobox);
            SubscribeMouseEvents(taskmgrbox);
            SubscribeMouseEvents(usagechartbox);
            SubscribeMouseEvents(vmwarebox);
            SubscribeMouseEvents(weatherBox);
            SubscribeMouseEvents(WelcomeStartupBox);
            SubscribeMouseEvents(VisualStudioCodeBoxMicrosoft);
            SubscribeMouseEvents(blendercityviewdialogbox);
            SubscribeMouseEvents(DebugWindowsFormsPlayerBox);
            SubscribeMouseEvents(gameBoardPlayAGame);
            SubscribeMouseEvents(DownloadNatoDesigner);
            SubscribeMouseEvents(blender3dgameplayerloadingassetsbox);
            SubscribeMouseEvents(appstatisticsbox);
            SubscribeMouseEvents(phoneDialerBox);
            SubscribeMouseEvents(sneakydeakyfeaturesselectionbox);
            SubscribeMouseEvents(natospeechrecogbox);
            SubscribeMouseEvents(BeatLabBox);
            SubscribeMouseEvents(postbookonemediabox);
            postbookonemediabox.Hide();

            DebugWindowsFormsPlayerBox.Hide();
            blender3dgameplayerloadingassetsbox.Hide();
            blender3dgamebox.Hide();
            phoneDialerBox.Hide();
            natospeechrecogbox.Hide();
            BeatLabBox.Hide();
            generateBackgroundToolStripMenuItem.Click += (s, e) =>
            {
                redrawBackground = true;
                this.Invalidate(); // Redraw on button click
            };


            //System Desktop Icons 
            

        }
        //App Retriever
        private void InitializeAppInstallerUI()
        {
            appInstallerGroupBox = new GroupBox();
            appInstallerGroupBox.Text = "App Installer";
            appInstallerGroupBox.Size = new Size(650, 600);
            appInstallerGroupBox.Location = new Point(this.ClientSize.Width / 2 - appInstallerGroupBox.Width / 2, this.ClientSize.Height / 2 - appInstallerGroupBox.Height / 2);
            appInstallerGroupBox.Anchor = AnchorStyles.None;
            appInstallerGroupBox.BackColor = System.Drawing.Color.White;
            appInstallerGroupBox.Hide();

            Label lblHowItWorks = new Label();
            lblHowItWorks.Text = "This installer allows you to load application packages from a URL or upload a local .npkg file. Click on an app groupbox, then press Ctrl+Shift+Alt+P to save its data to a new .npkg file.";
            lblHowItWorks.AutoSize = true;
            lblHowItWorks.Location = new Point(20, 30);
            lblHowItWorks.MaximumSize = new Size(appInstallerGroupBox.Width - 40, 0);
            lblHowItWorks.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            appInstallerGroupBox.Controls.Add(lblHowItWorks);

            Label lblAppUrl = new Label();
            lblAppUrl.Text = "Enter App URL:";
            lblAppUrl.AutoSize = true;
            lblAppUrl.Location = new Point(20, lblHowItWorks.Bottom + 20);
            appInstallerGroupBox.Controls.Add(lblAppUrl);

            txtAppUrl = new TextBox();
            txtAppUrl.Name = "txtAppUrl";
            txtAppUrl.Size = new Size(400, 25);
            txtAppUrl.Location = new Point(20, lblAppUrl.Bottom + 5);
            appInstallerGroupBox.Controls.Add(txtAppUrl);

            Button btnLoadUrl = new Button();
            btnLoadUrl.Text = "Load from URL";
            btnLoadUrl.Size = new Size(150, 30);
            btnLoadUrl.Location = new Point(txtAppUrl.Right + 10, txtAppUrl.Top);
            btnLoadUrl.Click += BtnLoadUrl_Click;
            appInstallerGroupBox.Controls.Add(btnLoadUrl);

            Button btnUploadPackage = new Button();
            btnUploadPackage.Text = "Upload Package";
            btnUploadPackage.Size = new Size(150, 30);
            btnUploadPackage.Location = new Point(btnLoadUrl.Left, btnLoadUrl.Bottom + 10);
            btnUploadPackage.Click += BtnUploadPackage_Click;
            appInstallerGroupBox.Controls.Add(btnUploadPackage);

            // NEW: Preview App Button
            Button btnPreviewApp = new Button();
            btnPreviewApp.Text = "Preview First App";
            btnPreviewApp.Size = new Size(150, 30);
            btnPreviewApp.Location = new Point(btnUploadPackage.Left, btnUploadPackage.Bottom + 10);
            btnPreviewApp.Click += BtnPreviewApp_Click;
            appInstallerGroupBox.Controls.Add(btnPreviewApp);


            flowLayoutPanelApps = new FlowLayoutPanel();
            flowLayoutPanelApps.Name = "flowLayoutPanelApps";
            flowLayoutPanelApps.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelApps.AutoScroll = true;
            flowLayoutPanelApps.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanelApps.BackColor = Color.LightGray;
            flowLayoutPanelApps.Location = new Point(20, btnPreviewApp.Bottom + 20);
            flowLayoutPanelApps.Size = new Size(appInstallerGroupBox.Width - 40, appInstallerGroupBox.Height - btnPreviewApp.Bottom - 40);
            appInstallerGroupBox.Controls.Add(flowLayoutPanelApps);

            Button btnCloseInstaller = new Button();
            btnCloseInstaller.Text = "X";
            btnCloseInstaller.Size = new Size(25, 25);
            btnCloseInstaller.Location = new Point(appInstallerGroupBox.Width - btnCloseInstaller.Width - 5, 5);
            btnCloseInstaller.FlatStyle = FlatStyle.Flat;
            btnCloseInstaller.BackColor = Color.Red;
            btnCloseInstaller.ForeColor = Color.White;
            btnCloseInstaller.Click += (s, e) => appInstallerGroupBox.Hide();
            appInstallerGroupBox.Controls.Add(btnCloseInstaller);

            this.Controls.Add(appInstallerGroupBox);
            appInstallerGroupBox.BringToFront();
        }

        private void OpenAppInstallerGroupBox(object sender, EventArgs e)
        {
            appInstallerGroupBox.Show();
        }

        private async void BtnLoadUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppUrl.Text))
            {
                MessageBox.Show("Please enter a valid URL.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string url = txtAppUrl.Text;
            _lastLoadedPackageUrl = url;
            _lastLoadedPackagePath = string.Empty;
            await LoadPackageFromUrl(url);
        }

        private async Task LoadPackageFromUrl(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

                    // Add a timeout for the HTTP request
                    client.Timeout = TimeSpan.FromSeconds(10);

                    string jsonContent = await client.GetStringAsync(url);
                    AppPackage package = JsonConvert.DeserializeObject<AppPackage>(jsonContent);

                    DisplayApps(package);
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error downloading package: {httpEx.Message}\nMake sure the URL is correct and accessible.", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TaskCanceledException) // Catches timeouts
            {
                MessageBox.Show("The request to download the package timed out. Please check your internet connection or the URL.", "Timeout Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (JsonSerializationException jsonEx)
            {
                MessageBox.Show($"Error parsing package content: {jsonEx.Message}\nEnsure the file is a valid JSON .npkg format.", "Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnUploadPackage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "NATO OS Package Files (*.npkg)|*.npkg|All Files (*.*)|*.*";
                openFileDialog.Title = "Select NATO OS Package File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    _lastLoadedPackagePath = filePath;
                    _lastLoadedPackageUrl = string.Empty;
                    await LoadPackageFromFile(filePath);
                }
            }
        }

        private async Task LoadPackageFromFile(string filePath)
        {
            try
            {
                AppPackage package = RewritePkg.LoadPackageFromFile(filePath);
                DisplayApps(package);
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"Error reading file: {ioEx.Message}\nCheck file permissions or if the file is open elsewhere.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (JsonSerializationException jsonEx)
            {
                MessageBox.Show($"Error parsing file content: {jsonEx.Message}\nEnsure the file is a valid JSON .npkg format.", "Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnPreviewApp_Click(object sender, EventArgs e)
        {
            AppPackage package = null;

            if (!string.IsNullOrWhiteSpace(_lastLoadedPackageUrl))
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                        client.Timeout = TimeSpan.FromSeconds(10);
                        string jsonContent = await client.GetStringAsync(_lastLoadedPackageUrl);
                        package = JsonConvert.DeserializeObject<AppPackage>(jsonContent);
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    MessageBox.Show($"Error downloading URL for preview: {httpEx.Message}\nMake sure the URL is correct and accessible.", "Preview Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (TaskCanceledException) // Catches timeouts
                {
                    MessageBox.Show("The request to download the package timed out. Please check your internet connection or the URL.", "Preview Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                catch (JsonSerializationException jsonEx)
                {
                    MessageBox.Show($"Error parsing URL content for preview: {jsonEx.Message}\nEnsure the URL content is a valid JSON .npkg format.", "Preview Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred while loading URL for preview: {ex.Message}", "Preview Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (!string.IsNullOrWhiteSpace(_lastLoadedPackagePath))
            {
                try
                {
                    package = RewritePkg.LoadPackageFromFile(_lastLoadedPackagePath);
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show($"Error reading file for preview: {ioEx.Message}\nCheck file permissions or if the file is open elsewhere.", "Preview File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (JsonSerializationException jsonEx)
                {
                    MessageBox.Show($"Error parsing file content for preview: {jsonEx.Message}\nEnsure the file is a valid JSON .npkg format.", "Preview Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred while loading file for preview: {ex.Message}", "Preview Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please load a package from URL or file first to preview an app.", "No Package Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (package != null && package.Apps != null && package.Apps.Count > 0)
            {
                // Take the first app for preview
                AppInfo appToPreview = package.Apps[0];

                // Create a NEW GroupBox instance for the preview dialog
                GroupBox previewGroupBox = RewritePkg.CreateGroupBoxFromAppInfo(appToPreview);

                // Create and show the preview dialog
                using (AppPreviewDialog previewDialog = new AppPreviewDialog(previewGroupBox))
                {
                    previewDialog.ShowDialog(this);
                }
            }
            else
            {
                MessageBox.Show("No apps found in the loaded package to preview.", "Preview Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void DisplayApps(AppPackage package)
        {
            flowLayoutPanelApps.Controls.Clear();
            _selectedAppInfo = null;

            if (package == null || package.Apps == null || package.Apps.Count == 0)
            {
                Label noAppsLabel = new Label();
                noAppsLabel.Text = "No apps found in this package.";
                noAppsLabel.AutoSize = true;
                noAppsLabel.Font = new Font("Segoe UI", 10F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
                noAppsLabel.ForeColor = Color.DarkRed;
                flowLayoutPanelApps.Controls.Add(noAppsLabel);
                return;
            }

            foreach (AppInfo app in package.Apps)
            {
                // Create a new GroupBox instance for display in the FlowLayoutPanel
                GroupBox appGroupBox = RewritePkg.CreateGroupBoxFromAppInfo(app);

                // Re-add click handlers for the new groupbox and its controls
                appGroupBox.Click += AppGroupBox_Click;
                foreach (Control control in appGroupBox.Controls)
                {
                    control.Click += AppGroupBox_Click;
                }

                flowLayoutPanelApps.Controls.Add(appGroupBox);
            }
        }

        private void AppGroupBox_Click(object sender, EventArgs e)
        {
            foreach (Control control in flowLayoutPanelApps.Controls)
            {
                if (control is GroupBox gb)
                {
                    gb.BackColor = SystemColors.Control;
                    gb.ForeColor = SystemColors.ControlText;
                }
            }

            GroupBox clickedGroupBox = sender as GroupBox;
            if (clickedGroupBox == null)
            {
                Control clickedControl = sender as Control;
                clickedGroupBox = clickedControl?.Parent as GroupBox;
            }

            if (clickedGroupBox != null)
            {
                clickedGroupBox.BackColor = Color.DodgerBlue;
                clickedGroupBox.ForeColor = Color.White;
                // Store the original AppInfo (which now includes ControlInfo)
                _selectedAppInfo = clickedGroupBox.Tag as AppInfo;
                this.Focus();
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.Alt && e.KeyCode == Keys.P)
            {
                SaveSelectedAppAsPackage();
                e.Handled = true;
            }
            // Add your existing KeyDown logic here if any
        }

        // MODIFIED: SaveSelectedAppAsPackage to extract ControlInfo and GroupBox size
        private async void SaveSelectedAppAsPackage()
        {
            if (_selectedAppInfo == null)
            {
                MessageBox.Show("Please select an app groupbox first to save.", "No App Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "NATO OS Package Files (*.npkg)|*.npkg";
                saveFileDialog.Title = $"Save {_selectedAppInfo.AppName} Package";
                saveFileDialog.FileName = $"{_selectedAppInfo.AppName.Replace(" ", "")}_{_selectedAppInfo.Version.Replace(".", "")}.npkg";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Get the currently selected GroupBox from flowLayoutPanelApps
                        GroupBox sourceGroupBox = null;
                        foreach (Control control in flowLayoutPanelApps.Controls)
                        {
                            if (control is GroupBox gb && gb.Tag == _selectedAppInfo)
                            {
                                sourceGroupBox = gb;
                                break;
                            }
                        }

                        if (sourceGroupBox == null)
                        {
                            MessageBox.Show("Could not find the selected app's groupbox to save its controls.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Capture the GroupBox's own size
                        _selectedAppInfo.GroupBoxWidth = sourceGroupBox.Width;
                        _selectedAppInfo.GroupBoxHeight = sourceGroupBox.Height;

                        // Clear existing controls info in case of previous saves
                        _selectedAppInfo.Controls.Clear();

                        // Populate ControlInfo for each child control within the source GroupBox
                        foreach (Control childControl in sourceGroupBox.Controls)
                        {
                            // Initialize ControlInfo with common properties
                            ControlInfo ci = new ControlInfo
                            {
                                ControlType = childControl.GetType().FullName,
                                Name = childControl.Name,
                                Text = childControl.Text,
                                X = childControl.Location.X,
                                Y = childControl.Location.Y,
                                Width = childControl.Size.Width,
                                Height = childControl.Size.Height,
                                Enabled = childControl.Enabled,
                                Visible = childControl.Visible,
                                BackColorHex = RewritePkg.ColorToHexString(childControl.BackColor),
                                ForeColorHex = RewritePkg.ColorToHexString(childControl.ForeColor),
                                FontString = RewritePkg.FontToString(childControl.Font)
                            };

                            // Add specific properties for certain control types during serialization
                            if (childControl is CheckBox checkBox)
                            {
                                ci.Checked = checkBox.Checked;
                            }
                            else if (childControl is RadioButton radioButton)
                            {
                                ci.Checked = radioButton.Checked;
                            }
                            else if (childControl is ProgressBar progressBar)
                            {
                                ci.Value = progressBar.Value;
                                ci.Minimum = progressBar.Minimum;
                                ci.Maximum = progressBar.Maximum;
                            }
                            else if (childControl is NumericUpDown numericUpDown)
                            {
                                ci.Value = numericUpDown.Value; // NumericUpDown.Value is decimal
                                ci.Minimum = numericUpDown.Minimum;
                                ci.Maximum = numericUpDown.Maximum;
                            }
                            else if (childControl is TrackBar trackBar)
                            {
                                ci.Value = trackBar.Value;
                                ci.Minimum = trackBar.Minimum;
                                ci.Maximum = trackBar.Maximum;
                            }
                            else if (childControl is MaskedTextBox maskedTextBox)
                            {
                                ci.Mask = maskedTextBox.Mask;
                            }
                            else if (childControl is PictureBox pictureBox)
                            {
                                // Convert PictureBox image to Base64 string for serialization
                                ci.ImageData = RewritePkg.ImageToBase64(pictureBox.Image);
                            }
                            // Add a placeholder for EventAction when saving.
                            // In a real scenario, you'd have a UI for defining these actions,
                            // or you'd manually add them to the .npkg file.
                            // For this example, let's pre-define some example actions for buttons
                            if (childControl is Button button)
                            {
                                // Example: If a button is named "btnToggleLabel", give it a toggle action
                                if (button.Name == "btnToggleLabel")
                                { // Assuming a label named "lblTogglable" in your test groupbox
                                    ci.EventAction = "ToggleVisibility:lblTogglable";
                                }
                                else
                                {
                                    ci.EventAction = "ShowMessageBox"; // Default action for other buttons
                                }
                            }
                            else if (childControl is LinkLabel linkLabel)
                            {
                                ci.EventAction = "ShowMessageBox"; // Default action for LinkLabels
                            }
                            else if (childControl is PictureBox clickablePictureBox)
                            {
                                // If a PictureBox is intended to be clickable, assign it an action
                                if (clickablePictureBox.Name == "pbSampleImage")
                                {
                                    ci.EventAction = "ShowMessageBox";
                                }
                            }

                            _selectedAppInfo.Controls.Add(ci);
                        }

                        // Now save the updated AppInfo
                        RewritePkg.SaveAppInfoToPackage(_selectedAppInfo, saveFileDialog.FileName);

                        MessageBox.Show($"App '{_selectedAppInfo.AppName}' saved successfully to:\n{saveFileDialog.FileName}", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving app package: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        //App Retriever
        //System Desktop Icons

        //System Desktop Icons
        //BeatLab
        //BeatLab Piano
        private void BeatLabDefinePianoKeys()
        {
            int keyWidth = 40, blackKeyWidth = 25;
            string[] whiteKeyNames = { "key01", "key03", "key05", "key07", "key09", "key11", "key13", "key15", "key17", "key19", "key21", "key23", "key24", "KICK" };
            string[] blackKeyNames = { "key02", "key04", null, "key06", "key08", "key10", null, "key12", "key14", null, "key16", "key18", "key20", null };

            for (int i = 0; i < whiteKeyNames.Length; i++)
            {
                BeatLabWhiteKeys.Add(new Rectangle(i * keyWidth, 0, keyWidth, 200), $"C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\{whiteKeyNames[i]}.mp3");

                if (blackKeyNames[i] != null)
                    BeatLabBlackKeys.Add(new Rectangle(i * keyWidth + 30, 0, blackKeyWidth, 120), $"C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\{blackKeyNames[i]}.mp3");
            }
        }
        private void BeatLabPianoPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var key in BeatLabWhiteKeys.Keys)
            {
                g.FillRectangle(BeatLabPressedKey == key ? Brushes.LightGray : Brushes.White, key);
                g.DrawRectangle(Pens.Black, key);
            }

            foreach (var key in BeatLabBlackKeys.Keys)
            {
                g.FillRectangle(BeatLabPressedKey == key ? Brushes.DarkGray : Brushes.Black, key);
                g.DrawRectangle(Pens.Black, key);
            }
        }

        private void BeatLabPianoMouseDown(object sender, MouseEventArgs e)
        {
            Rectangle key = BeatLabBlackKeys.Keys.FirstOrDefault(k => k.Contains(e.Location));

            if (key.IsEmpty)
                key = BeatLabWhiteKeys.Keys.FirstOrDefault(k => k.Contains(e.Location));


            if (!key.IsEmpty)
            {
                BeatLabPressedKey = key;
                BeatLabPianoPanel.Invalidate();
                BeatLabPlaySound(BeatLabWhiteKeys.ContainsKey(key) ? BeatLabWhiteKeys[key] : BeatLabBlackKeys[key]);
            }
        }

        private void BeatLabPianoMouseUp(object sender, MouseEventArgs e)
        {
            BeatLabPressedKey = null;
            BeatLabPianoPanel.Invalidate();
        }

        private void BeatLabPlaySound(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var reader = new AudioFileReader(filePath);
            ISampleProvider provider = BeatLabEchoEnabled ? new DelayProvider(reader.ToSampleProvider(), 500) : reader.ToSampleProvider();

            if (BeatLabIsRecording)
            {
                float[] buffer = new float[reader.WaveFormat.SampleRate]; // 1 second of audio
                int samplesRead;
                while ((samplesRead = reader.ToSampleProvider().Read(buffer, 0, buffer.Length)) > 0)
                {
                    BeatLabRecordedSamples.AddRange(buffer.Take(samplesRead));
                }
            }

            var waveOut = new WaveOutEvent();
            waveOut.Init(provider);
            waveOut.Play();
        }

        private void BeatLabToggleRecording(object sender, EventArgs e)
        {
            BeatLabIsRecording = !BeatLabIsRecording;
        }

        private void BeatLabSaveRecording(object sender, EventArgs e)
        {
            string filePath = $"Recorded_{DateTime.Now:yyyyMMdd_HHmmss}.wav";
            using (var writer = new WaveFileWriter(filePath, new WaveFormat(44100, 1)))
            {
                foreach (var sample in BeatLabRecordedSamples)
                    writer.WriteSample(sample);
            }
            MessageBox.Show($"Saved to {filePath}");
        }

        private void BeatLabDrawWaveform(object sender, PaintEventArgs e)
        {
            if (BeatLabRecordedSamples.Count == 0) return;
            var g = e.Graphics;
            int midY = BeatLabWaveformPanel.Height / 2;
            for (int i = 0; i < BeatLabRecordedSamples.Count - 1; i++)
            {
                int x1 = i * BeatLabWaveformPanel.Width / BeatLabRecordedSamples.Count;
                int x2 = (i + 1) * BeatLabWaveformPanel.Width / BeatLabRecordedSamples.Count;
                int y1 = midY + (int)(BeatLabRecordedSamples[i] * midY);
                int y2 = midY + (int)(BeatLabRecordedSamples[i + 1] * midY);
                g.DrawLine(Pens.Green, x1, y1, x2, y2);
            }
        }
        //BeatLab Piano
        private void StartRecording(object sender, EventArgs e)
        {


            BeatLaboutputFile = $"BeatLabRecordedAudio_{DateTime.Now:yyyyMMdd_HHmmss}.mp3";
            BeatLabcapture = new WasapiLoopbackCapture(); // Captures system sound (drums)
            BeatLabwriter = new WaveFileWriter(BeatLaboutputFile, BeatLabcapture.WaveFormat);
            BeatLabwaveData = new MemoryStream();

            BeatLabcapture.DataAvailable += (s, a) =>
            {
                BeatLabwriter.Write(a.Buffer, 0, a.BytesRecorded);
                BeatLabwaveData.Write(a.Buffer, 0, a.BytesRecorded);
            };



            BeatLabcapture.RecordingStopped += (s, a) =>
            {
                BeatLabwriter.Dispose();
                DrawWaveform();

            };

            BeatLabcapture.StartRecording();
        }



        private void StopRecording(object sender, EventArgs e)
        {
            BeatLabcapture?.StopRecording();
        }

        private void PlayAudio(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(BeatLaboutputFile) || !File.Exists(BeatLaboutputFile))
                {
                    MessageBox.Show("No valid audio file found.");
                    return;
                }

                string tempWavFile = Path.Combine(Path.GetTempPath(), "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\FILES\\System Active\\$TempFiles\\BeatLab_Temp.wav");

                using (var reader = new MediaFoundationReader(BeatLaboutputFile)) // More flexible than Mp3FileReader
                using (var resampler = new MediaFoundationResampler(reader, new WaveFormat(44100, reader.WaveFormat.Channels)))
                {
                    resampler.ResamplerQuality = 60; // High-quality resampling
                    WaveFileWriter.CreateWaveFile(tempWavFile, resampler);
                }

                using (var player = new WaveOutEvent())
                using (var audioFile = new AudioFileReader(tempWavFile))
                {
                    player.Init(audioFile);
                    player.Play();

                    while (player.PlaybackState == NAudio.Wave.PlaybackState.Playing)
                    {
                        Application.DoEvents();
                    }
                }

                File.Delete(tempWavFile); // Cleanup
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Playback error: {ex.Message}");
            }
        }


        private void TrimAudio(object sender, EventArgs e)
        {
            using (var form = new Form())
            {
                form.Text = "Trim Audio";
                var startLabel = new Label { Text = "Start (ms):", Left = 10, Top = 10 };
                var startBox = new TextBox { Left = 80, Top = 10, Width = 100, Text = BeatLabtrimStart.ToString() };
                var endLabel = new Label { Text = "End (ms):", Left = 10, Top = 40 };
                var endBox = new TextBox { Left = 80, Top = 40, Width = 100, Text = BeatLabtrimEnd.ToString() };
                var button = new Button { Text = "Apply", Left = 10, Top = 70 };
                button.Click += (s, args) =>
                {
                    if (int.TryParse(startBox.Text, out int start) && int.TryParse(endBox.Text, out int end))
                    {
                        BeatLabtrimStart = start;
                        BeatLabtrimEnd = end;
                        MessageBox.Show($"Trimmed from {BeatLabtrimStart / 1000}s to {BeatLabtrimEnd / 1000}s");
                        form.Close();
                    }
                };
                form.Controls.AddRange(new Control[] { startLabel, startBox, endLabel, endBox, button });
                form.ShowDialog();
            }
        }

        private void AdjustVolume(object sender, EventArgs e)
        {
            using (var form = new Form())
            {
                form.Text = "Adjust Volume";
                var label = new Label { Text = "Volume (%):", Left = 10, Top = 10 };
                var trackBar = new TrackBar { Left = 80, Top = 10, Width = 200, Minimum = 0, Maximum = 200, Value = (int)(BeatLabvolumeLevel * 100) };
                var button = new Button { Text = "Apply", Left = 10, Top = 50 };
                button.Click += (s, args) =>
                {
                    BeatLabvolumeLevel = trackBar.Value / 100f;
                    MessageBox.Show($"Volume set to {trackBar.Value}%");
                    form.Close();
                };
                form.Controls.AddRange(new Control[] { label, trackBar, button });
                form.ShowDialog();
            }
        }

        private void ExportAudio(object sender, EventArgs e)
        {
            string exportPath = Path.ChangeExtension(BeatLaboutputFile, ".wav");
            File.Copy(BeatLaboutputFile, exportPath, true);
            MessageBox.Show($"Audio exported to {exportPath}");
        }

        private void DrawWaveform()
        {
            if (BeatLabwaveData == null || BeatLabwaveData.Length == 0) return;

            Bitmap waveformBitmap = new Bitmap(BeatLabAudioPanel.Width, BeatLabAudioPanel.Height);
            using (Graphics g = Graphics.FromImage(waveformBitmap))
            {
                g.Clear(Color.Black);
                Pen waveformPen = new Pen(Color.Green, 1);
                byte[] buffer = BeatLabwaveData.ToArray();
                int step = Math.Max(1, buffer.Length / waveformBitmap.Width);
                for (int i = 0; i < waveformBitmap.Width; i++)
                {
                    int value = buffer[i * step] - 128;
                    int y = BeatLabAudioPanel.Height / 2 - (value * BeatLabAudioPanel.Height / 256);
                    g.DrawLine(waveformPen, i, BeatLabAudioPanel.Height / 2, i, y);
                }
            }
            BeatLabAudioPanel.BackgroundImage = waveformBitmap;
        }

        //BeatLab
        private void PlayTetrisMelody()
        {
            int[] Tetrismelody = {
            659, 494, 523, 587, 523, 494, 440, 440, 523, 659, 587, 523, 494, 523, 587, 659, 523, 440, 440,
            523, 659, 587, 523, 494, 523, 587, 659, 523, 523, 587, 494, 523, 440, 440, 349, 392, 523, 440,
            349, 392, 523, 440, 349, 392, 523, 440, 349, 392, 523, 440, 349, 392, 523, 440, 494, 523, 587, 659,
            784, 659, 698, 740, 659, 523, 587, 494, 523, 659, 784, 698, 740, 659, 523, 587, 494, 523, 659, 523, 440,
            392, 440, 494, 523, 440, 349, 392, 440, 349
        };

            int[] Tetrisbeats = {
            300, 150, 300, 300, 150, 150, 300, 150, 150, 300, 150, 150, 300, 150, 150, 300, 150, 300, 300,
            300, 150, 300, 300, 150, 150, 300, 150, 300, 150, 300, 300, 150, 150, 300, 300, 300, 300,
            200, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, 300, 300, 300, 300,
            400, 300, 300, 300, 300, 300, 300, 300, 300, 300, 400, 300, 300, 300, 300, 300, 300, 300, 400, 400, 400,
            300, 300, 300, 300, 300, 300, 300, 300, 400
        };

            for (int i = 0; i < Tetrismelody.Length; i++)
            {
                try
                {
                    Console.Beep(Tetrismelody[i], Tetrisbeats[i]); // Play each note
                    Thread.Sleep(50); // Small delay between notes
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
        }
        private bool showCross = false;  // Flag to control whether to show the cross

        private void ConvertUnits(object sender, EventArgs e)
        {
            if (double.TryParse(UnitConvertertxtInput.Text, out double inputValue))
            {
                try
                {
                    string fromUnit = UnitConvertercmbFromUnit.SelectedItem.ToString();

                    string toUnit = UnitConvertercmbToUnit.SelectedItem.ToString();

                    double result = ConvertUnitsValue(inputValue, fromUnit, toUnit);
                    UnitConverterlblResult.Text = $"Result: {result} {toUnit}";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    UnitConverterlblResult.Text = "Invalid input, " + ex;

                }
            }
            else
            {
                UnitConverterlblResult.Text = "Invalid input!";
            }
        }

        private double ConvertUnitsValue(double value, string fromUnit, string toUnit)
        {
            double metersValue = ConvertToMeters(value, fromUnit);
            return ConvertFromMeters(metersValue, toUnit);
        }

        private double ConvertToMeters(double value, string fromUnit)
        {
            switch (fromUnit)
            {
                case "km": return value * 1000;
                case "miles": return value / 0.000621371;
                case "feet": return value / 3.28084;
                case "inches": return value / 39.3701;
                case "yards": return value / 1.09361;
                case "centimeters": return value / 100;
                case "millimeters": return value / 1000;
                case "nautical miles": return value / 0.000539957;
                case "light years": return value / 1.057e-16;
                case "parsecs": return value / 3.24078e-17;
                default: return value;
            }
        }

        private double ConvertFromMeters(double value, string toUnit)
        {
            switch (toUnit)
            {
                case "km": return value / 1000;
                case "miles": return value * 0.000621371;
                case "feet": return value * 3.28084;
                case "inches": return value * 39.3701;
                case "yards": return value * 1.09361;
                case "centimeters": return value * 100;
                case "millimeters": return value * 1000;
                case "nautical miles": return value * 0.000539957;
                case "light years": return value * 1.057e-16;
                case "parsecs": return value * 3.24078e-17;
                default: return value;
            }
        }
        //Blender 3D

        bool isDebuggingWindowsFormsPlayer;
        int resetPuzzlePicture = 0;
        //Fish Game

        int myfishgamepoints;
        int aifishgamepoints;
        int fishgamesetpoint = 0;
        //Password Manager API
        string passwordManagerLogon = "start";
        string beforePassword;
        //Designer Code / Windows Forms Player


        GroupBox designercodeplayergroupBox = new GroupBox
        {
            Text = "Form" + WFformscount + 1 + ".cs",
            Size = new Size(200, 150),
            Location = new Point(50, 50),
            BackColor = Color.LightGray
        };
        //TETRIS CODE

        private void MoveLeft(object sender, EventArgs e)
        {
            if (!CollisionCheck(-1, 0))
                blockX--;
            gamePanel.Invalidate();
        }

        private void MoveRight(object sender, EventArgs e)
        {
            if (!CollisionCheck(1, 0))
                blockX++;
            gamePanel.Invalidate();
        }

        private void GenerateNewShape()
        {
            blockX = 4;
            blockY = 0;
            blockColor = GetRandomColor();
            int shapeType = TETRISrandom.Next(3);
            currentShape = new List<Point>();

            if (shapeType == 0) // Square
            {
                currentShape.Add(new Point(0, 0));
                currentShape.Add(new Point(1, 0));
                currentShape.Add(new Point(0, 1));
                currentShape.Add(new Point(1, 1));
            }
            else if (shapeType == 1) // Rectangle
            {
                currentShape.Add(new Point(0, 0));
                currentShape.Add(new Point(1, 0));
                currentShape.Add(new Point(2, 0));
                currentShape.Add(new Point(3, 0));
            }
            else if (shapeType == 2) // L-Shape
            {
                currentShape.Add(new Point(0, 0));
                currentShape.Add(new Point(0, 1));
                currentShape.Add(new Point(0, 2));
                currentShape.Add(new Point(1, 2));
            }
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (CollisionCheck(0, 1))
            {
                foreach (var point in currentShape)
                {
                    Point newPoint = new Point(blockX + point.X, blockY + point.Y);
                    placedBlocks.Add(newPoint);
                    blockColors[newPoint] = blockColor;
                }
                score += 10;
                scoreLabel.Text = "Score: " + score;
                GenerateNewShape();
            }
            else
            {
                blockY++;
            }
            gamePanel.Invalidate();
        }

        private bool CollisionCheck(int dx, int dy)
        {
            foreach (var point in currentShape)
            {
                int newX = blockX + point.X + dx;
                int newY = blockY + point.Y + dy;
                if (newX < 0 || newX >= GridWidth || newY >= GridHeight || placedBlocks.Contains(new Point(newX, newY)))
                {
                    return true;
                }
            }
            return false;
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush brush = new SolidBrush(blockColor);

            foreach (var point in currentShape)
            {
                g.FillRectangle(brush, (blockX + point.X) * TETRISgridSize, (blockY + point.Y) * TETRISgridSize, TETRISgridSize, TETRISgridSize);
                g.DrawRectangle(Pens.Black, (blockX + point.X) * TETRISgridSize, (blockY + point.Y) * TETRISgridSize, TETRISgridSize, TETRISgridSize);
            }

            foreach (var point in placedBlocks)
            {
                Brush placedBrush = new SolidBrush(blockColors[point]);
                g.FillRectangle(placedBrush, point.X * TETRISgridSize, point.Y * TETRISgridSize, TETRISgridSize, TETRISgridSize);
                g.DrawRectangle(Pens.Black, point.X * TETRISgridSize, point.Y * TETRISgridSize, TETRISgridSize, TETRISgridSize);
            }
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(TETRISrandom.Next(256), TETRISrandom.Next(256), TETRISrandom.Next(256));
        }

        //TETRIS CODE
        private void InitializeCustomComponents()
        {

            debugWindowsFormsPlayer.Click += (sender, e) => CompileAndRunCode(CodeAreaDesignerCode.Text);

        }

        private void CompileAndRunCode(string code)
        {
            try
            {
                // Find the DebugWindowsFormsPlayerBox in the form
                GroupBox debugBox = this.Controls["DebugWindowsFormsPlayerBox"] as GroupBox;

                // Clear existing controls in the debugBox (if found)
                debugBox?.Controls.Clear();

                // Preprocess the user's code: Replace "this" with "debugBox" when applicable
                string processedCode = debugBox != null ? code.Replace("this", "debugBox") : code;

                // Create the syntax tree for the processed code
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(processedCode);

                // References needed for compilation
                MetadataReference[] references = new MetadataReference[]
                {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Form).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
                };

                // Create a compilation
                CSharpCompilation compilation = CSharpCompilation.Create(
                    "DynamicCode",
                    new[] { syntaxTree },
                    references,
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                using (var ms = new MemoryStream())
                {
                    // Emit the compiled assembly
                    EmitResult result = compilation.Emit(ms);

                    if (!result.Success)
                    {
                        // Handle compilation errors
                        string errors = string.Join(Environment.NewLine, result.Diagnostics
                            .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
                            .Select(diagnostic => diagnostic.ToString()));
                        MessageBox.Show("Compilation failed:\n" + errors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Load the compiled assembly
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    // Find the user-defined type
                    Type type = assembly.GetType("MyWindowsCode.MyDynamicCode");

                    if (type == null)
                    {
                        MessageBox.Show("Class 'MyDynamicCode' could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Try finding the method Run(GroupBox) or Run()
                    MethodInfo method = type.GetMethod("Run", new[] { typeof(GroupBox) })
                        ?? type.GetMethod("Run", Type.EmptyTypes);

                    if (method == null)
                    {
                        MessageBox.Show("No suitable 'Run' method found. Please define 'Run()' or 'Run(GroupBox)'.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        // Invoke the method dynamically
                        if (method.GetParameters().Length == 1)
                        {
                            method.Invoke(null, new object[] { debugBox });  // Pass GroupBox
                        }
                        else
                        {
                            method.Invoke(null, null);  // Call Run() without parameters
                        }
                    }
                    catch (TargetInvocationException ex)
                    {
                        MessageBox.Show($"An error occurred during execution:\n{ex.InnerException?.Message}",
                            "Runtime Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred:\n{ex.Message}",
                            "Runtime Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateWeather();
        }
        private void SubscribeMouseEvents(Control control)
        {
            control.MouseDown += (sender, e) => Control_MouseDown(sender, e, control);
            control.MouseMove += Control_MouseMove;
            control.MouseUp += Control_MouseUp;
        }

        private void Control_MouseDown(object sender, MouseEventArgs e, Control control)
        {

            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Control control = sender as Control;

                if (control != null)
                {
                    // Calculate new position
                    control.Left += e.X - dragStartPoint.X;
                    control.Top += e.Y - dragStartPoint.Y;
                }
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
            //Sneaky Deaky Features

            if (!sandboxEnabled || e.Button != MouseButtons.Right) return;

            Control clickedControl = sender as Control;
            string objectName = clickedControl.Name;

            if (!objectScripts.ContainsKey(objectName))
                objectScripts[objectName] = $"sender.Text = \"Hello from {objectName}\";";

            string originalScript = objectScripts[objectName];

            // Use 'using' for script dialog
            using (ScriptDialog scriptForm = new ScriptDialog(objectScripts[objectName]))  // Pass initial script here
            {
                // Initialize and configure the script dialog
                using (ScriptDialog scriptDialog = new ScriptDialog(objectScripts[objectName]))
                {
                    scriptForm.Text = "Enter Code";
                    scriptForm.Width = 400;
                    scriptForm.Height = 250;
                    scriptForm.FormBorderStyle = FormBorderStyle.FixedDialog;

                    Label lblPrompt = new Label();
                    lblPrompt.Text = "Enter Code:";
                    lblPrompt.Dock = DockStyle.Top;

                    TextBox txtScript = new TextBox();
                    txtScript.Multiline = true;
                    txtScript.Dock = DockStyle.Fill;
                    txtScript.Text = objectScripts[objectName];

                    Button btnSubmit = new Button();
                    btnSubmit.Text = "Submit";
                    btnSubmit.Dock = DockStyle.Bottom;
                    btnSubmit.Click += (s, args) =>
                    {
                        scriptDialog.ScriptText = txtScript.Text;
                        scriptForm.DialogResult = DialogResult.OK;
                        scriptForm.Close();
                    };

                    scriptForm.Controls.Add(txtScript);
                    scriptForm.Controls.Add(btnSubmit);
                    scriptForm.Controls.Add(lblPrompt);

                    // Show the script form
                    if (scriptForm.ShowDialog() == DialogResult.OK)
                    {
                        objectScripts[objectName] = scriptDialog.ScriptText;

                        // Execute the entered script
                        using (var ms = new System.IO.MemoryStream())
                        {
                            string fullCode = $@"
                    using System;
                    using System.Windows.Forms;

                    public class ScriptClass {{
                        public void Run(Control sender) {{
                            {scriptDialog.ScriptText}
                        }}
                    }}";

                            var syntaxTree = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(fullCode);
                            var references = new[]
                            {
                        Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                        Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(typeof(Control).Assembly.Location),
                        Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(System.Reflection.Assembly.GetExecutingAssembly().Location)
                    };

                            var compilation = Microsoft.CodeAnalysis.CSharp.CSharpCompilation.Create("ScriptAssembly")
                                .WithOptions(new Microsoft.CodeAnalysis.CSharp.CSharpCompilationOptions(Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary))
                                .AddReferences(references)
                                .AddSyntaxTrees(syntaxTree);

                            var result = compilation.Emit(ms);
                            if (!result.Success)
                            {
                                string errors = string.Join("\n", result.Diagnostics
                                    .Where(d => d.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error)
                                    .Select(d => d.GetMessage()));

                                MessageBox.Show($"Error Detected: {errors}, Rolling Back...", "Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                objectScripts[objectName] = originalScript;
                            }
                            else
                            {
                                ms.Seek(0, System.IO.SeekOrigin.Begin);
                                var assembly = System.Reflection.Assembly.Load(ms.ToArray());
                                var instance = assembly.CreateInstance("ScriptClass");
                                var method = instance.GetType().GetMethod("Run");
                                method.Invoke(instance, new object[] { clickedControl });
                            }
                        }
                    }
                }
            }
        }

        private void EnableDragging()
        {
            foreach (Control ctrl in this.Controls)
            {
                SubscribeMouseEvents(ctrl);
            }
        }
        private void BtnPreviousMonth_Click(object sender, EventArgs e)
        {
            currentDate = currentDate.AddMonths(-1);
            LoadCalendar();

        }

        private void BtnNextMonth_Click(object sender, EventArgs e)
        {
            currentDate = currentDate.AddMonths(1);
            LoadCalendar();
        }
        private void MoveMouse(int deltaX, int deltaY)
        {
            Cursor.Position = new System.Drawing.Point(Cursor.Position.X + deltaX, Cursor.Position.Y + deltaY);
        }
        private void InitSpeech()
        {
            NATOVoiceRecognizer = new SpeechRecognitionEngine();
            NATOVoiceSynthesizer = new SpeechSynthesizer();

            Choices NATOVoiceCommands = new Choices();
            NATOVoiceCommands.Add(new string[] { "open external paint", "open external browser", "self destruct", "open games", "open internet access view", "open map", "open mail", "open nato media player", "open nato video player",
                "open microsoft publisher", "open microsoft word", "open designer apps", "open designer sign in", "open developers", "open open slate", "open password manager", "open power options", "open powerpoint", "open print to p d f", "open remote desktop connection", "record pc",
                "open registry editor", "open restore and reset", "open app shop", "open system info", "open task manager", "open usage chart", "open v m ware", "open weather", "open welcome menu", "open notepad", "open all apps box", "open a o designer", "open authentic messenger", "open authentic movie viewer",
                "open authentic notepad", "open authentic phone link", "open authentic planner", "open blender", "open browser", "open bugs list", "open calculator", "open camera", "open designer code debug window", "open designer code", "open designer", "open code editor", "open e ink pad", "open event viewer",
                "open microsoft excel", "open f boost", "open files", "open find text", "open widgets box", "random background", "change p c background", "type period","type comma","type space","type enter","type backspace", "annotate", "voice about", "open speech recognition", "start listening", "stop listening", "open s d f", "activity coin flip",
                "activity pick random number", "open beatlab", "open postbook one media", "is middle school fun"

            });
            for (char c = 'a'; c <= 'z'; c++) NATOVoiceCommands.Add($"type {c}");
            for (char c = '0'; c <= '9'; c++) NATOVoiceCommands.Add($"type {c}");
            string[] symbols = { "period", "comma", "space", "enter", "backspace", "arrow up", "arrow down", "arrow left", "arrow right", "arrow tab", "mouse move right", "mouse move left", "mouse move up", "mouse move down", "mouse move click left", "mouse move click right" };
            NATOVoiceCommands.Add(symbols);
            Grammar NATOVoiceGrammar = new Grammar(new GrammarBuilder(NATOVoiceCommands));
            NATOVoiceRecognizer.LoadGrammar(NATOVoiceGrammar);
            NATOVoiceRecognizer.SetInputToDefaultAudioDevice();
            NATOVoiceRecognizer.SpeechRecognitionRejected += NATOVoiceRecognizer_SpeechRecognitionRejected;
            NATOVoiceRecognizer.SpeechRecognized += NATOVoiceRecognizer_SpeechRecognized;
            NATOVoiceRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }
        PaintEventArgs NATOSPEECHf;
        private void NATOVoiceRecognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            EnableSpeechBtn.Image = Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\SystemSpeech\\idle.png");
            defaultNATOSpeechTXT.Text = "What was that?";
            try
            {
                NATOVoiceRecognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private async void NATOVoiceRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            if (!isSpeechEnabled) return; // Do nothing if speech is disabled
            EnableSpeechBtn.Image = Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\SystemSpeech\\active.png");

            string NATOVoiceCommand = e.Result.Text.ToLower();
            NATOVoiceSynthesizer.SpeakAsync($"You said {NATOVoiceCommand}");
            defaultNATOSpeechTXT.Text = $"You said {NATOVoiceCommand}";
            if (NATOVoiceCommand.StartsWith("type "))
            {
                string key = NATOVoiceCommand.Substring(5);
                SimulateKeyPress(key);
                return;
            }

            switch (NATOVoiceCommand)
            {
                case "is middle school fun":
                    NATOVoiceSynthesizer.SpeakAsync($"No, middle school is not fun.");

                    break;
                case "open postbook one media":
                    postbookonemediabox.Show();
                    break;
                case "open beatlab":
                    BeatLabBox.Show();
                    break;
                case "activity pick random number":
                    int randomNumber = random.Next(0, 100000000);
                    NATOVoiceSynthesizer.SpeakAsync("Random Number: " + randomNumber + "");

                    break;
                case "activity coin flip":
                    int coinAmount = random.Next(1, 2);

                    if (coinAmount == 1)
                    {
                        NATOVoiceSynthesizer.SpeakAsync($"Heads");

                    }
                    if (coinAmount == 2)

                    {
                        NATOVoiceSynthesizer.SpeakAsync($"Tails");

                    }

                    break;
                case "open s d f":
                    sneakydeakyfeaturesselectionbox.Show();
                    break;
                case "stop listening":
                    EnableSpeechBtn.Image = Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\SystemSpeech\\off.png");
                    NATOVoiceRecognizer.RecognizeAsyncCancel();
                    NATOVoiceSynthesizer.SpeakAsync("Stopped listening");

                    break;
                case "start listening":
                    EnableSpeechBtn.Image = Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\SystemSpeech\\active.png");
                    try
                    {
                        NATOVoiceRecognizer.RecognizeAsync(RecognizeMode.Multiple);

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                    }
                    NATOVoiceSynthesizer.SpeakAsync("listening");
                    break;
                case "open speech recognition":
                    natospeechrecogbox.Show();
                    break;
                case "annotate":
                    using (Form ComppasswordForm = new Form())
                    {
                        ComppasswordForm.Text = "Annotate...";
                        ComppasswordForm.Width = 300;
                        ComppasswordForm.Height = 150;
                        ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                        Label compDialoglabel = new Label { Text = "Type text to annotate:\nMust enable speech first", Left = 10, Top = 10, AutoSize = true };
                        TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                        Button compsubmitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, };
                        Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                        ComppasswordForm.Controls.Add(compDialoglabel);
                        ComppasswordForm.Controls.Add(compDialogtextBox);
                        ComppasswordForm.Controls.Add(compsubmitButton);
                        ComppasswordForm.Controls.Add(compcancelButton);
                        compsubmitButton.Click += (s, args) =>
                        {



                            NATOVoiceSynthesizer.SpeakAsync(compDialogtextBox.Text);




                        };

                        ComppasswordForm.CancelButton = compcancelButton;

                        if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                    break;
                case "open external paint":
                    Process.Start("mspaint");
                    break;
                case "open external browser":
                    Process.Start("chrome");
                    break;
                case "mouse move right":
                    MoveMouse(MOUSE_MOVE_DISTANCE, 0);
                    break;
                case "mouse move left":
                    MoveMouse(-MOUSE_MOVE_DISTANCE, 0);
                    break;
                case "mouse move up":
                    MoveMouse(0, -MOUSE_MOVE_DISTANCE);
                    break;
                case "mouse move down":
                    MoveMouse(0, MOUSE_MOVE_DISTANCE);
                    break;
                case "mouse move click right":
                    SimulateMouseClick("right");
                    break;
                case "mouse move click left":
                    SimulateMouseClick("left");
                    break;
                case "voice about":
                    NATOVoiceSynthesizer.SpeakAsync("About NATO VOICE:");
                    NATOVoiceSynthesizer.SpeakAsync("Info recieved on," + whenNATOStartup);
                    MessageBox.Show("About NATO Voice\nNATO VOICE V. 1.1.6 BETA\nAuthors:\n*The PostBook ServerSynthesizer Group\nMicrosoft (System.Speech)\nAvalible commands:\nopen [app name]\nvoice about\nmouse move [direction]\narrow [direction]\nrandom background\nchange pc background\nenter\nspace\type [letter / number]\nQuestions? Reach out to: ServerSynthesizer@postbook.net\nor call 800-400-8080\n ©2025 PostBook Server Synthesizer", "About NATO Voice", MessageBoxButtons.OK, MessageBoxIcon.Question);

                    break;
                case "change p c background":
                    openFileDialog1.ShowDialog();
                    try
                    {
                        this.BackgroundImage = Image.FromFile(openFileDialog1.FileName);

                    }
                    catch (Exception ex)
                    {
                        NATOVoiceSynthesizer.SpeakAsync($"Error, could not load background. Error reason: {ex}");

                    }
                    break;
                case "random background":
                    // Custom background drawing logic (only on button click)
                    if (backgroundBitmap != null)
                    {
                        try
                        {
                            // If we have a pre-generated background, we draw it
                            NATOSPEECHf.Graphics.DrawImage(backgroundBitmap, 0, 0);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                    }

                    // Your SELECT BOX code remains here (unchanged)
                    if (!selectionRectangle.IsEmpty)
                    {
                        using (Pen pen = new Pen(Color.Blue, 2))
                        {
                            NATOSPEECHf.Graphics.DrawRectangle(pen, selectionRectangle);
                            this.ContextMenuStrip = selectBoxMenuNATO;
                        }
                    }

                    // Custom background generation logic
                    if (redrawBackground)
                    {
                        // Create a new Bitmap to store the generated background
                        backgroundBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                        using (Graphics g = Graphics.FromImage(backgroundBitmap))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;

                            // Fill background with random gradient
                            using (LinearGradientBrush brush = new LinearGradientBrush(
                                this.ClientRectangle,
                                GetRandomColor(), GetRandomColor(),
                                random.Next(360)))
                            {
                                g.FillRectangle(brush, this.ClientRectangle);
                            }

                            // Draw random shapes
                            for (int i = 0; i < 8; i++)
                            {
                                int x = random.Next(50, this.Width - 100);
                                int y = random.Next(50, this.Height - 100);
                                int size = random.Next(40, 120);

                                int shapeType = random.Next(4);
                                using (Pen pen = new Pen(GetRandomColor(), 3))
                                using (Brush brush = new SolidBrush(GetRandomColor()))
                                {
                                    switch (shapeType)
                                    {
                                        case 0: DrawStar(g, pen, brush, x, y, size, 5); break;
                                        case 1: DrawZigzag(g, pen, x, y, size); break;
                                        case 2: DrawPolygon(g, pen, brush, x, y, size, random.Next(3, 7)); break;
                                        case 3: Draw3DCube(g, pen, brush, x, y, size); break;
                                    }
                                }
                            }
                        }

                        // Set flag to false after generating the background
                        redrawBackground = false;
                        this.Invalidate(); // Trigger a repaint to show the new background

                    }

                    break;
                case "self destruct":
                    NATOVoiceSynthesizer.SpeakAsync($"OS Closing, Make sure to save everything within 10 seconds, or it will autosave");
                    NATOVoiceSynthesizer.SpeakAsync($"10");
                    NATOVoiceSynthesizer.SpeakAsync($"9");
                    NATOVoiceSynthesizer.SpeakAsync($"8");
                    NATOVoiceSynthesizer.SpeakAsync($"7");
                    NATOVoiceSynthesizer.SpeakAsync($"6");
                    NATOVoiceSynthesizer.SpeakAsync($"5");
                    NATOVoiceSynthesizer.SpeakAsync($"4");
                    NATOVoiceSynthesizer.SpeakAsync($"3");
                    NATOVoiceSynthesizer.SpeakAsync($"2");
                    NATOVoiceSynthesizer.SpeakAsync($"1");
                    Application.Exit();
                    this.Close();

                    break;
                //Apps Reco
                case "open paint":
                    NATOPAINTBOX.Show();
                    break;
                case "open games":
                    GamesBox.Show();


                    break;
                case "open internet explorer":
                    InternetExplorerBox.Show();
                    break;
                case "open internet access view":
                    ixviewbox.Show();
                    break;
                case "open map":
                    MagnifierBox.Show();
                    break;
                case "open mail":
                    MagnifierBox.Show();
                    break;
                case "open nato media player":
                    MediaPlayerApp.Show();
                    break;
                case "open nato video player":
                    MediaPlayerBox.Show();
                    break;
                case "open microsoft publisher":
                    microsoftpublisherapp.Show();
                    break;
                case "open microsoft word":
                    microsoftwordbox.Show();
                    break;
                case "open designer apps":
                    if (isDesignerCodeInstalled == true)
                    {
                        NATODesignerComboBox.Show();
                    }
                    break;
                case "open designer sign in":
                    NatoDesignerSignInBox.Show();
                    break;
                case "open developers":
                    NATODevelopersApp.Show();
                    break;
                case "open open slate":
                    OpenSlateBox.Show();
                    break;
                case "open password manager":
                    changepasswordboxpasswordmanager.Hide();
                    // Create a custom message box with password input
                    if (passwordManagerLogon == "")
                    {


                    }
                    else
                    {
                        using (Form passwordForm = new Form())
                        {
                            passwordForm.Text = "Enter Password";
                            passwordForm.Width = 300;
                            passwordForm.Height = 150;
                            passwordForm.StartPosition = FormStartPosition.CenterParent;

                            Label label = new Label { Text = "Password:", Left = 10, Top = 10, AutoSize = true };
                            TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260, PasswordChar = '*' };
                            Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                            Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                            passwordForm.Controls.Add(label);
                            passwordForm.Controls.Add(textBox);
                            passwordForm.Controls.Add(submitButton);
                            passwordForm.Controls.Add(cancelButton);

                            passwordForm.AcceptButton = submitButton;
                            passwordForm.CancelButton = cancelButton;

                            if (passwordForm.ShowDialog() == DialogResult.OK)
                            {
                                string enteredPassword = textBox.Text;
                                if (enteredPassword == passwordManagerLogon)
                                {
                                    passwordmanagerbox.Visible = true;
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    break;
                case "open power options":
                    powerbox.Show();
                    break;
                case "open power point":
                    powerpointbox.Show();
                    break;
                case "open print to p d f":
                    PrintToPDFBox.Show();
                    break;
                case "open remote desktop connection":
                    recentapp.Text = "Remote Desktop Connection";

                    customapp.Show();
                    rdpbox.Show();
                    connectlabelrdp.Hide();
                    moreoptionboxrdp.Hide();
                    form3.Hide();
                    filesboxrdp.Hide();
                    notepadboxrdp.Hide();

                    //RDP Loading
                    menurdp.Hide();
                    disconnectrdp.Hide();
                    browserboxrdp.Hide();
                    settingsboxrdp.Hide();
                    backgrounddisplayboxrdp.Hide();
                    Remotedesktoprdp.Hide();
                    break;
                case "record pc":
                    RecordBox.Show();
                    break;
                case "open registry editor":
                    registryeditorbox.Show();
                    break;
                case "open restore and reset":
                    restoreorresetbox.Show();
                    break;
                case "open app shop":
                    DownloadNatoDesigner.Show();
                    recentapp.Text = "Store";
                    librarybox.Hide();
                    SpotifyBtn.Hide();
                    DownloadStats.ForeColor = Color.Black;
                    DownloadStats.Text = "Downloading: 0%";
                    recentapp.Text = "App Shop";
                    shopapp.Show();
                    HomeAPP1.Enabled = false;
                    appbox1.Hide();
                    ShowApps1.Show();
                    //Download Buttons
                    ChromiumDownloadBTN.Hide();
                    //Download Stats
                    DownloadStats1.Hide();
                    DownloadStats1.Value = 0;
                    DownloadStats2.Hide();
                    DownloadStats2.Value = 0;
                    DownloadStats3.Hide();
                    DownloadStats3.Value = 0;
                    moreappsboxstore.Hide();
                    libraryclose.Enabled = true;
                    shopappclose.Enabled = true;
                    searchappbtn.Enabled = true;
                    HomeAPP1.Enabled = true;
                    Account1.Enabled = true;
                    Library1.Enabled = true;
                    closelibrary.Hide();
                    OpenFileandPdfReaderandBrowserPlusBox.Hide();
                    YourMailandstylusPadBox.Hide();
                    break;
                case "open system info":
                    systeminfobox.Show();
                    break;
                case "open task manager":
                    recentapp.Text = "Task Manager";
                    taskmgrbox.Show();
                    break;
                case "open usage chart":
                    usagechartbox.Show();
                    break;
                case "open v m ware":
                    vmwarebox.Show();
                    break;
                case "open weather":
                    weatherBox.Show();
                    break;
                case "open welcome menu":
                    WelcomeStartupBox.Show();
                    break;
                case "open notepad":
                    groupBox2.Show();
                    break;
                case "open all apps box":
                    AllAppsBox.Show();
                    break;
                case "open a o designer":
                    AODesignerBox.Show();
                    break;
                case "open authentic messenger":
                    if (isDesignerCodeInstalled == true)
                    {
                        AuthenticMessengerApp.Show();
                    }
                    break;
                case "open authentic movie viewer":
                    if (isDesignerCodeInstalled == true)
                    {
                        AuthenticMovieViewer.Show();
                    }
                    break;
                case "open authentic notepad":
                    if (isDesignerCodeInstalled == true)
                    {
                        AuthenticNotePad.Show();
                    }
                    break;
                case "open authentic phone link":
                    if (isDesignerCodeInstalled == true)
                    {
                        AuthenticPhoneLinkBox.Show();
                    }
                    break;
                case "open authentic planner":
                    if (isDesignerCodeInstalled == true)
                    {
                        AuthenticPlanner.Show();
                    }
                    break;
                case "open blender":
                    SpaceViewConsole.BackColor = Color.White;
                    spaceviewblenderbox.Hide();
                    selectoptionblender.Hide();
                    blenderbox.Hide();
                    blenderloadingassetsbox.Show();
                    BlenderDialogExtractingAssets.Text = "none avalible";
                    blenderloadingpayloadbar.Value = 0;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "BlenderPackage.KB100.NewPause";
                    blenderloadingpayloadbar.Value = 0;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "BlenderPackage.KB100.Animation";
                    blenderloadingpayloadbar.Value = 0;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "C# Animation";
                    blenderloadingpayloadbar.Value = 0;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "using; Blender.[attribution]";
                    blenderloadingpayloadbar.Value = 0;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "Blender.Click.KB381";
                    blenderloadingpayloadbar.Value = 10;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "BlenderPackage.BlenderSpace";
                    blenderloadingpayloadbar.Value = 0;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "LiveUpdates";
                    blenderloadingpayloadbar.Value = 0;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "Blender.GoogleEarthLibrary";
                    blenderloadingpayloadbar.Value = 0;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "Blender.VisualBasic";
                    blenderloadingpayloadbar.Value = 0;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "Blender.SoundEffects";
                    blenderloadingpayloadbar.Value = 20;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "BlenderToolbar";
                    blenderloadingpayloadbar.Value = 25;
                    await Task.Delay(100);
                    BlenderDialogExtractingAssets.Text = "Blender.PackageBind";
                    blenderloadingpayloadbar.Value = 45;
                    await Task.Delay(1000);
                    blenderloadingpayloadbar.Value = 65;
                    await Task.Delay(800);
                    blenderloadingpayloadbar.Value = 85;
                    await Task.Delay(900);
                    blenderloadingpayloadbar.Value = 85;
                    await Task.Delay(900);
                    blenderloadingpayloadbar.Value = 100;
                    BlenderDialogExtractingAssets.Text = "none avalible";
                    await Task.Delay(1000);
                    blenderloadingassetsbox.Hide();
                    blenderbox.Show();

                    break;
                case "open browser":
                    browserBox.Show();
                    break;
                case "open bugs list":
                    BugsListNATO.Show();
                    break;
                case "open calculator":
                    Calculator.Show();
                    break;
                case "open camera":
                    Start_cam();
                    cameraapp.Show();
                    break;
                case "open designer code debug window":
                    if (isDesignerCodeInstalled == true)
                    {
                        DebugWindowDesignerCode.Show();
                        DebugWindowBrowserDesignerCode.DocumentText = "<HTML><CENTER><H1>Launch Designer Code to start debugging</H1><hr><p>Debugger is unavalible untill Designer Code is launched.</p></CENTER></HTML>";
                    }
                    else
                    {

                    }
                    break;
                case "open designer code":
                    if (isDesignerCodeInstalled == true)
                    {
                        DesignerCode.Show();
                        CodeAreaDesignerCode.Hide();
                        RunFileDesignerCode.Enabled = false;
                        ConsoleDesignerCode.Enabled = false;
                        LogsDesignerCode.Enabled = false;
                        FileNameDesignerCode.Enabled = false;
                        ShareDesignerCode.Enabled = false;
                        SaveDesignerCode.Enabled = false;
                        NewBlankFileDesignerCode.Enabled = true;
                        ProgrammingLanguageDesignerCodeBox.Enabled = false;
                        ProgrammingLanguageBoxDesignerCode.Hide();
                        ProgrammingLanguageTypeDesignerCode.Text = "---";
                        CodeAreaDesignerCode.Enabled = true;
                        ExtentionBoxDesignerCode.Hide();
                        ConsoleAndShareBoxDesignerCode.Hide();
                        LogsListBoxDesignerBox.Items.Add("New instance today, end other instances:");
                    }
                    else
                    {

                    }
                    break;
                case "open designer":
                    designertextbox.Show();
                    break;
                case "open code editor":
                    editorbox.Show();
                    break;
                case "open e ink pad":
                    EINKPAD.Show();
                    break;
                case "open event viewer":
                    eventviewerbox.Show();
                    break;
                case "open microsoft excel":
                    excelbox.Show();
                    break;
                case "open f boost":
                    fboostoptimizer.Show();
                    break;
                case "open files":
                    filesbox.Show();
                    break;
                case "open find text":
                    findtextapp.Show();
                    break;
                case "open widgets box":
                    widgetsboxselect.Show();
                    break;
                default:
                    SimulateKeyPress(NATOVoiceCommand);
                    break;



            }
        }
        private void SimulateKeyPress(string key)
        {
            Keys keyEnum;
            switch (key)
            {
                case "period": keyEnum = Keys.OemPeriod; break;
                case "comma": keyEnum = Keys.Oemcomma; break;
                case "space": keyEnum = Keys.Space; break;
                case "enter": keyEnum = Keys.Enter; break;
                case "backspace": keyEnum = Keys.Back; break;
                case "arrow up": keyEnum = Keys.Up; break;
                case "arrow down": keyEnum = Keys.Down; break;
                case "arrow left": keyEnum = Keys.Left; break;
                case "arrow right": keyEnum = Keys.Right; break;
                case "arrow tab": keyEnum = Keys.Tab; break;
                default:
                    if (key.Length == 1)
                    {
                        if (char.IsDigit(key[0]))
                        {
                            keyEnum = (Keys)Enum.Parse(typeof(Keys), "D" + key);
                        }
                        else if (Enum.TryParse(key.ToUpper(), out keyEnum))
                        {
                            // Letter key
                        }
                        else return; // Invalid key, ignore
                    }
                    else return; // Invalid key, ignore
                    break;
            }

            // Simulate key press (down and up events)
            keybd_event((byte)keyEnum, 0, 0, 0);
            keybd_event((byte)keyEnum, 0, KEYEVENTF_KEYUP, 0);
        }



        private void SimulateMouseClick(string button)
        {
            if (button == "left")
            {
                // Simulate left mouse button click
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }
            else if (button == "right")
            {
                // Simulate right mouse button click
                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            }
        }

        private void LoadCalendar()
        {
            // Update the Month/Year label
            lblMonthYear.Text = currentDate.ToString("MMMM yyyy");

            // Clear any existing buttons for the previous month
            dayPanel.Controls.Clear();

            // Get the first day of the current month
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            // Find the day of the week the month starts on (0 = Sunday, 1 = Monday, etc.)
            int firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            // Calculate how many days in this month
            int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

            // Add empty labels for the days before the first day of the month
            for (int i = 0; i < firstDayOfWeek; i++)
            {
                dayPanel.Controls.Add(new Label() { Width = 24, Height = 24 });
            }

            // Add buttons for the days of the month
            for (int day = 1; day <= daysInMonth; day++)
            {
                Button dayButton = new Button();
                dayButton.Text = day.ToString();
                dayButton.Width = 24;
                dayButton.Height = 24;
                dayButton.Click += DayButton_Click;
                dayPanel.Controls.Add(dayButton);
            }
        }

        private void DayButton_Click(object sender, EventArgs e)
        {
            Button dayButton = sender as Button;
            MessageBox.Show("Selected Day: " + dayButton.Text);
        }

        private void SlideshowTimer_Tick(object sender, EventArgs e)
        {
            // Get PictureBox to update the image
            PictureBox pictureBox = (PictureBox)ImageSlideshowWidgetNATO.Controls["slideshowPictureBox"];

            // Load the next image in the list
            if (imagePaths.Count > 0)
            {
                try
                {
                    pictureBox.Image = Image.FromFile(imagePaths[currentImageIndex]);
                    currentImageIndex = (currentImageIndex + 1) % imagePaths.Count; // Loop through images
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
        }
        private void InitializeRSSWidget()
        {


            // Add a ListBox to display feed items
            listBoxFeeds = new ListBox
            {
                Dock = DockStyle.Top,
                Height = 192,
                Width = 147
            };
            RSSWidgetNATO.Controls.Add(listBoxFeeds);

            // Add a Refresh button

        }

        private void UpdateWeather()
        {
            // Simulate weather data
            string[] conditions = { "Sunny", "Cloudy", "Rainy", "Stormy", "Snowy" };
            int temperature = _random.Next(-10, 35); // Random temperature
            string condition = conditions[_random.Next(conditions.Length)];

            // Display weather data
            weatherLabel.Text = $"Condition: {condition}\nTemperature: {temperature}°C\nUpdated at: {DateTime.Now:HH:mm:ss}";
        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                startPoint = e.Location;
                selectionRectangle = new Rectangle(e.X, e.Y, 0, 0);
                this.ContextMenuStrip = RightClickMenuNATO;

                Invalidate(); // Redraw the form
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            cursorPosition = e.Location;
            this.Invalidate();  // Redraw the form to update the cross position
            NATOLABELMOUSEPOSITION.Text = $"POSITION:: X: {e.X}, Y: {e.Y}";

            if (isDragging)
            {
                // Calculate the new rectangle
                selectionRectangle = new Rectangle(
                    Math.Min(startPoint.X, e.X),
                    Math.Min(startPoint.Y, e.Y),
                    Math.Abs(startPoint.X - e.X),
                    Math.Abs(startPoint.Y - e.Y)
                );
                Invalidate(); // Redraw the form
                this.ContextMenuStrip = selectBoxMenuNATO;

            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isDragging)
            {
                isDragging = false;
                selectionRectangle = Rectangle.Empty; // Clear the rectangle
                Invalidate(); // Redraw the form
                this.ContextMenuStrip = RightClickMenuNATO;

            }
        }
        private Point cursorPosition;  // To store the cursor position

        private void OnPaint(object sender, PaintEventArgs e)
        {

            // Your painting logic here...

            // Count the frame
            IncrementFrameCount();
            // Custom background drawing logic (only on button click)
            if (backgroundBitmap != null)
            {
                // If we have a pre-generated background, we draw it first
                e.Graphics.DrawImage(backgroundBitmap, 0, 0);
            }

            // Debug Mode for Grid (only if the grid is enabled)
            if (showGrid)
            {
                Graphics g = e.Graphics;
                Color backgroundColor = this.BackColor; // Get the current background color

                // Calculate the inverted color of the background
                Color invertedColor = Color.FromArgb(
                    255 - backgroundColor.R,
                    255 - backgroundColor.G,
                    255 - backgroundColor.B
                );

                Pen gridPen = new Pen(invertedColor);  // Use the inverted color for grid lines
                Brush textBrush = new SolidBrush(invertedColor); // Use inverted color for text

                int gridSize = 80; // Size of grid squares (80x80)

                // Draw vertical grid lines
                for (int x = 0; x < this.ClientSize.Width; x += gridSize)
                {
                    g.DrawLine(gridPen, x, 0, x, this.ClientSize.Height);
                }

                // Draw horizontal grid lines
                for (int y = 0; y < this.ClientSize.Height; y += gridSize)
                {
                    g.DrawLine(gridPen, 0, y, this.ClientSize.Width, y);
                }

                // Draw X and Y coordinates at the grid intersections
                for (int x = 0; x < this.ClientSize.Width; x += gridSize)
                {
                    for (int y = 0; y < this.ClientSize.Height; y += gridSize)
                    {
                        string coordinates = $"({x}, {y})";
                        g.DrawString(coordinates, this.Font, textBrush, new PointF(x + 5, y + 5));
                    }
                }
            }

            // Your SELECT BOX code remains here (unchanged)
            // For example, drawing a selection rectangle
            if (!selectionRectangle.IsEmpty)
            {
                using (Pen pen = new Pen(Color.Blue, 2))
                {
                    e.Graphics.DrawRectangle(pen, selectionRectangle);
                    this.ContextMenuStrip = selectBoxMenuNATO;
                }
            }

            // If the cross is enabled, draw the perpendicular yellow cross
            if (showGrid && showCross)  // Check if both grid and cross are enabled
            {
                Graphics g = e.Graphics;
                Pen crossPen = new Pen(Color.Yellow, 2); // Yellow color for the cross

                // Draw vertical line (from top to bottom)
                g.DrawLine(crossPen, 0, cursorPosition.Y, this.ClientSize.Width, cursorPosition.Y);

                // Draw horizontal line (from left to right)
                g.DrawLine(crossPen, cursorPosition.X, 0, cursorPosition.X, this.ClientSize.Height);
            }

            // Custom background generation logic (if redrawBackground is true)
            if (redrawBackground)
            {
                // Create a new Bitmap to store the generated background
                backgroundBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                using (Graphics g = Graphics.FromImage(backgroundBitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    // Fill background with random gradient
                    using (LinearGradientBrush brush = new LinearGradientBrush(
                        this.ClientRectangle,
                        GetRandomColor(), GetRandomColor(),
                        random.Next(360)))
                    {
                        g.FillRectangle(brush, this.ClientRectangle);
                    }

                    // Draw random shapes
                    for (int i = 0; i < 8; i++)
                    {
                        int x = random.Next(50, this.Width - 100);
                        int y = random.Next(50, this.Height - 100);
                        int size = random.Next(40, 120);

                        int shapeType = random.Next(4);
                        using (Pen pen = new Pen(GetRandomColor(), 3))
                        using (Brush brush = new SolidBrush(GetRandomColor()))
                        {
                            switch (shapeType)
                            {
                                case 0: DrawStar(g, pen, brush, x, y, size, 5); break;
                                case 1: DrawZigzag(g, pen, x, y, size); break;
                                case 2: DrawPolygon(g, pen, brush, x, y, size, random.Next(3, 7)); break;
                                case 3: Draw3DCube(g, pen, brush, x, y, size); break;
                            }
                        }
                    }
                }

                // Set flag to false after generating the background
                redrawBackground = false;
                this.Invalidate(); // Trigger a repaint to show the new background
            }
        }



        //RANDOM BACKGROUND CODE




        private void DrawStar(Graphics g, Pen pen, Brush brush, int x, int y, int size, int points)
        {
            PointF[] star = new PointF[points * 2];
            double angle = Math.PI / points;

            for (int i = 0; i < star.Length; i++)
            {
                double radius = (i % 2 == 0) ? size / 2 : size;
                star[i] = new PointF(
                    x + (float)(radius * Math.Cos(i * angle * 2)),
                    y + (float)(radius * Math.Sin(i * angle * 2))
                );
            }

            g.FillPolygon(brush, star);
            g.DrawPolygon(pen, star);
        }

        private void DrawZigzag(Graphics g, Pen pen, int x, int y, int width)
        {
            Point[] points = new Point[6];
            int step = width / (points.Length - 1);
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Point(x + i * step, y + (i % 2 == 0 ? 0 : step));
            }
            g.DrawLines(pen, points);
        }

        private void DrawPolygon(Graphics g, Pen pen, Brush brush, int x, int y, int size, int sides)
        {
            PointF[] points = new PointF[sides];
            double angle = 2 * Math.PI / sides;

            for (int i = 0; i < sides; i++)
            {
                points[i] = new PointF(
                    x + (float)(size * Math.Cos(i * angle)),
                    y + (float)(size * Math.Sin(i * angle))
                );
            }

            g.FillPolygon(brush, points);
            g.DrawPolygon(pen, points);
        }

        private void Draw3DCube(Graphics g, Pen pen, Brush brush, int x, int y, int size)
        {
            PointF[] frontFace =
            {
            new PointF(x, y),
            new PointF(x + size, y),
            new PointF(x + size, y + size),
            new PointF(x, y + size)
        };

            PointF[] backFace =
            {
            new PointF(x + size / 2, y - size / 2),
            new PointF(x + size + size / 2, y - size / 2),
            new PointF(x + size + size / 2, y + size - size / 2),
            new PointF(x + size / 2, y + size - size / 2)
        };

            g.FillPolygon(brush, frontFace);
            g.DrawPolygon(pen, frontFace);
            g.FillPolygon(brush, backFace);
            g.DrawPolygon(pen, backFace);

            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(pen, frontFace[i], backFace[i]);
            }
        }
        //RANDOM BACKGROUND CODE
        //string CalTotal;
        int num1;
        int num2;
        string option;
        int result;
        int powerpointslide;
        //PowerPoint to NATO API
        //Slide 1
        string slide1header;
        string slide1sub;
        Image slide1image;
        //Slide 2
        string slide2header;
        string slide2sub;
        Image slide2image;

        //Slide 3
        string slide3header;
        string slide3sub;
        Image slide3image;

        //End API
        //Avoid override screenshots
        int newscrnshot;
        void Start_cam()
        {
            Devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            frame = new VideoCaptureDevice(Devices[0].MonikerString);
            frame.NewFrame += new NewFrameEventHandler(NewFrame_event);
            frame.Start();
        }
        string output;
        void NewFrame_event(object send, NewFrameEventArgs e)
        {
            try
            {
                cameraVideo.Image = (Image)e.Frame.Clone();
                CallVideoPhoneLink.Image = (Image)e.Frame.Clone();
                cameraappcamera.Image = (Image)e.Frame.Clone();


            }
            catch (Exception)
            {

            }
        }

        public virtual string PlaceholderText { get; set; }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Debug Mode
            foreach (Control ctrl in this.Controls)
            {
                controlPositions[ctrl.Name] = ctrl.Location;
            }

            // Example: Print stored positions
            foreach (var item in controlPositions)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            //Debug Mode
            rulerPublisher.Paint += rulerPublisher_Paint;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            recentapp.Text = "NATO Homescreen";
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            TextLocalIP.Text = GetLocalIP();
            TextRemoteIP.Text = GetLocalIP();
            //Usage Chart
            //Memory Series
            // Clear existing series
            chart1.Series.Clear();

            // Create a new series

            var memseries = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Memory Usage",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column
            };
            chart1.Series.Add(memseries);

            // Add data points
            memseries.Points.AddXY(1, 50);
            memseries.Points.AddXY(2, 70);
            memseries.Points.AddXY(3, 30);
            //Other Usage (image files & other files)
            var otherseries = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Other Usage",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column
            };
            chart1.Series.Add(otherseries);

            // Add data points
            otherseries.Points.AddXY(1, 25);
            otherseries.Points.AddXY(2, 30);
            otherseries.Points.AddXY(3, 55);
            //Data Usage
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Data Usage",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column
            };
            chart1.Series.Add(series);

            // Add data points
            series.Points.AddXY(1, 50);
            series.Points.AddXY(2, 70);
            series.Points.AddXY(3, 30);
        }

        private string GetLocalIP()
        {
            return "127.0.0.1";



        }
        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Hide();
        }
        // Menu Links
        private void filelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            recentapp.Text = "Files";

            filesbox.Show();
            filesdir.Navigate(@"file:///C:\Users\Alex\source\repos\NATO-OS%207\NATO-OS%207\OS");
        }

        private void notepadlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            groupBox2.Show();
            recentapp.Text = "NotePad";

        }

        private void browserlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DownloadNatoDesigner.Show();
            browserBox.Show();
            recentapp.Text = "Browser";

        }

        private void settingslink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            customapp.Show();
            natocmd.Hide();
            settingsbox.Show();
            backgrounddisplaybox.Hide();
            discstoragebox.Hide();
            helpsupportbox.Hide();
            uninstallappsgroup.Hide();
            privacygroup.Hide();
            taskmgrbox.Hide();
            DownloadNatoDesigner.Show();



        }

        private void codelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            editorbox.Show();
            recentapp.Text = "Code";

        }

        private void appshoplink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DownloadNatoDesigner.Show();
            recentapp.Text = "Store";
            librarybox.Hide();
            SpotifyBtn.Hide();
            DownloadStats.ForeColor = Color.Black;
            DownloadStats.Text = "Downloading: 0%";
            recentapp.Text = "App Shop";
            shopapp.Show();
            HomeAPP1.Enabled = false;
            appbox1.Hide();
            ShowApps1.Show();
            //Download Buttons
            ChromiumDownloadBTN.Hide();
            //Download Stats
            DownloadStats1.Hide();
            DownloadStats1.Value = 0;
            DownloadStats2.Hide();
            DownloadStats2.Value = 0;
            DownloadStats3.Hide();
            DownloadStats3.Value = 0;
            moreappsboxstore.Hide();
            libraryclose.Enabled = true;
            shopappclose.Enabled = true;
            searchappbtn.Enabled = true;
            HomeAPP1.Enabled = true;
            Account1.Enabled = true;
            Library1.Enabled = true;
            closelibrary.Hide();
            OpenFileandPdfReaderandBrowserPlusBox.Hide();
            YourMailandstylusPadBox.Hide();


        }

        private void devlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            recentapp.Text = "NATO Developers";
            NATODevelopersApp.Show();
            SearchResultsForTextNatoDevelopersTXT.Hide();
            TextNatoDevelopersInfo.Hide();
            SharedItemsNatoForDevelopers.Hide();

        }
        //Power
        private void button6_Click(object sender, EventArgs e)
        {
            powerbox.Show();
        }
        // System Info
        private void infolink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            systeminfobox.Show();
            recentapp.Text = "System Info";

        }

        private void systeminfobox_Enter(object sender, EventArgs e)
        {

        }

        private void infook_Click(object sender, EventArgs e)
        {
            systeminfobox.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        //Web Browser APP - NATO COPYRIGHT
        private void browserBox_Enter(object sender, EventArgs e)
        {

        }

        private void closebrowser_Click(object sender, EventArgs e)
        {
            browserBox.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WebBrowser1.Navigate(browsersearch.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WebBrowser1.GoBack();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            WebBrowser1.GoForward();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            WebBrowser1.GoBack();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            WebBrowser1.Refresh();
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // CODE APP
            editorbox.Show();
            recentapp.Text = "Code";


        }

        private void editorbox_Enter(object sender, EventArgs e)
        {

        }

        private void codeeditor_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void closecode_Click(object sender, EventArgs e)
        {
            editorbox.Hide();
        }

        private void savecode_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //NOTEPAD APP
            recentapp.Text = "NotePad";

            groupBox2.Show();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            groupBox2.Hide();
        }

        private void powerclose_Click(object sender, EventArgs e)
        {
            powerbox.Hide();
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            listBoxEventViewer.Items.Add("App terminated: NATO.APP (0001)");
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            this.Hide();
            player.URL = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/eff/START2.wav";
            player.controls.play();
            await Task.Delay(4000);
            Application.Exit();

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(); // Create an instance of Form2
            form1.Show(); // Show Form1

            this.Hide(); // Hide Form2 (you can also use this.Close() if you want to close it completely)
            recentapp.Text = "Sign-in Screen";
            listBoxEventViewer.Items.Add("App started: UILogonScreen.APP");
            listBoxEventViewer.Items.Add("User signed out.");



        }

        private void filesbox_Enter(object sender, EventArgs e)
        {

        }

        private void fileclose_Click(object sender, EventArgs e)
        {
            filesbox.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            browserBox.Show();
            recentapp.Text = "Browser";

        }

        private void filesdir_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void commandlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            customapp.Show();
            natocmd.Show();
            natocmd.AppendText("NATO-OS 7 Implemented Code\r\nVersion 12.7\r\n\r\nNATO OS-2000 Implemented.\n");
            recentapp.Text = "Command Terminal";
            NATOcmdvScrollBar.Dock = DockStyle.Right;
            NATOcmdvScrollBar.Scroll += (s, ev) => natocmd.SelectionStart = natocmd.Text.Length;
            natocmd.Controls.Add(NATOcmdvScrollBar);
            UpdateScrollBar();

        }
        private void UpdateScrollBar()
        {
            NATOcmdvScrollBar.Maximum = natocmd.Lines.Length;
            NATOcmdvScrollBar.Value = NATOcmdvScrollBar.Maximum;
        }
        private void customapp_Enter(object sender, EventArgs e)
        {

        }

        private void CloseApp_Click(object sender, EventArgs e)
        {
            //Custom App programmed to run unnessesary apps.
            customapp.Hide();
            settingsbox.Hide();
            natocmd.Hide();
            rdpbox.Hide();

        }

        private void natocmd_TextChanged(object sender, EventArgs e)
        {

        }
        //settings buttons
        private void settingsdisplaybtn_Click(object sender, EventArgs e)
        {
            backgrounddisplaybox.Show();
        }

        private void settingsstoragebtn_Click(object sender, EventArgs e)
        {
            discstoragebox.Show();
            cleangroup.Hide();
            storagebar.Value = 18;
            storage1.Value = 18;
            storage2.Value = 17;
            storage3.Value = 19;
            storage4.Value = 4;
            storage5.Value = 6;
            storage6.Value = 23;
            storage7.Value = 6;
            storage8.Value = 10;
            storage9.Value = 100;

        }

        private void helpbtnsettings_Click(object sender, EventArgs e)
        {
            helpsupportbox.Show();
            QAlabel1.Hide();
            QAlabel2A.Hide();
            QAlabel2B.Hide();
            QAlabel3.Hide();

        }

        private void settingsappsbtn_Click(object sender, EventArgs e)
        {
            uninstallappsgroup.Show();
            appsbar1.Value = 20;
            appsbar2.Value = 7;
            moreappsbox.Hide();
        }

        private void othersettingsbtn_Click(object sender, EventArgs e)
        {

        }

        private void settingsprivacybtn_Click(object sender, EventArgs e)
        {
            privacygroup.Show();
            privacysecuritymainbar.Value = 99;
            protectionlearnmore.Hide();
        }

        private void devtoolssettings_Click(object sender, EventArgs e)
        {

        }

        private void taskmgr_Click(object sender, EventArgs e)
        {
            recentapp.Text = "Task Manager";
            taskmgrbox.Show();
        }

        private void networkbtnsettings_Click(object sender, EventArgs e)
        {

        }

        private void settingsbox_Enter(object sender, EventArgs e)
        {

        }

        private void backgroundbackbtn_Click(object sender, EventArgs e)
        {
            backgrounddisplaybox.Hide();
        }

        private void storagebar_Click(object sender, EventArgs e)
        {







        }

        private void cleanstoragebtn_Click(object sender, EventArgs e)
        {
            cleangroup.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            cleangroup.Hide();
        }

        private void discstoragebox_Enter(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            cleangroup.Hide();
            storagebar.Value = 15;
            //storage

        }

        private void button16_Click(object sender, EventArgs e)
        {
            discstoragebox.Hide();
        }

        private void storage1_Click(object sender, EventArgs e)
        {

        }

        private void storage2_Click(object sender, EventArgs e)
        {

        }

        private void storage3_Click(object sender, EventArgs e)
        {

        }

        private void storage4_Click(object sender, EventArgs e)
        {

        }

        private void storage5_Click(object sender, EventArgs e)
        {

        }

        private void storage6_Click(object sender, EventArgs e)
        {

        }

        private void storage7_Click(object sender, EventArgs e)
        {

        }

        private void storage8_Click(object sender, EventArgs e)
        {

        }

        private void storage9_Click(object sender, EventArgs e)
        {

        }

        private void closehelpbtn_Click(object sender, EventArgs e)
        {
            helpsupportbox.Hide();
        }

        private void submitQA_Click(object sender, EventArgs e)
        {
            QAtextbox.Clear();
            MessageBox.Show("Question Submitted, thank you!", "Question", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void QAbtn1_Click(object sender, EventArgs e)
        {
            QAlabel1.Show();
        }

        private void QAbtn2_Click(object sender, EventArgs e)
        {
            QAlabel2A.Show();
            QAlabel2B.Show();
        }

        private void QAbtn3_Click(object sender, EventArgs e)
        {
            QAlabel3.Show();
        }

        private void uninstallappsX_Click(object sender, EventArgs e)
        {
            uninstallappsgroup.Hide();
        }

        private void morebtnapps_Click(object sender, EventArgs e)
        {
            moreappsbox.Show();
        }

        private void moreappsboxX_Click(object sender, EventArgs e)
        {
            moreappsbox.Hide();
        }
        //Uninstall Apps Menu
        private void appuninstall1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Uninstalling file...", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            applbl1.Hide();
            appuninstall1.Hide();
            tetrislink.Hide();
        }

        private void appuninstall2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Uninstalling file...", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            applbl2.Hide();
            browserlink.Hide();
            button2.Hide();
            appuninstall2.Hide();
        }

        private void appuninstall3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Uninstalling file...", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            applbl3.Hide();
            appuninstall3.Hide();
            settingslink.Hide();
            MessageBox.Show("OS/SYSTEM FILES/SETTINGS.APP: Is not found, could this file be deleted?", "File not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            settingsbox.Hide();
            customapp.Hide();

        }

        private void appuninstall4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Uninstalling file...", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            appuninstall4.Hide();
            applbl4.Hide();
            codelink.Hide();
            button4.Hide();
        }

        private void appuninstall5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Uninstalling file...", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            appuninstall5.Hide();
            applbl5.Hide();
            notepadlink.Hide();
            button3.Hide();
        }

        private void appuninstall6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Uninstalling file...", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            appuninstall6.Hide();
            applbl6.Hide();
            commandlink.Hide();
            MessageBox.Show("Unable to run: BackgroundProcessor.DLL", "COMMAND.APP", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);

        }

        private void appuninstall7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Uninstalling file...", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            appuninstall7.Hide();
            applbl7.Hide();
            devlink.Hide();
        }

        private void appuninstall8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Uninstalling file...", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            appuninstall8.Hide();
            applbl8.Hide();
            infolink.Hide();
            syslabel.Hide();
        }

        private void appuninstall9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Unable to install: SYSTEM EXPLORER / The administrator of the system permits 'Administrator' to uninstall this file.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show("Unable to install: SYSTEM EXPLORER / This file is in use by: NATO.APP, NATO.DLL, SYSGO.DLL, User-Administrator", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void uninstallappsgroup_Enter(object sender, EventArgs e)
        {

        }

        private void privacysecurityclose_Click(object sender, EventArgs e)
        {
            privacygroup.Hide();
            cachecomplete.Hide();
        }

        private void tetrislink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            recentapp.Text = "Games - Tetris";
            try
            {
                Task.Run(() =>
                {
                    PlayTetrisMelody();
                });
                TetrisGameBox.Visible = true;
                gameTimer.Start();

                string tetrisMusicURL = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/Packages (x86)/Tetris codec/media/main.mp3";
                tetrisMusic.URL = tetrisMusicURL;
                tetrisMusic.Ctlcontrols.play();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }



        private void cachereset1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to Cache Reset your NATO OS?", "System Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (DialogResult == DialogResult.Yes)
            {
                cachecomplete.Show();
            }
            else if (DialogResult == DialogResult.No)
            {
                //Aborted
            }
        }

        private void learnprivacybtn_Click(object sender, EventArgs e)
        {
            protectionlearnmore.Show();
        }

        private void btnprotectionlearnmore_Click(object sender, EventArgs e)
        {
            protectionlearnmore.Hide();
        }

        private void keepmonitorbtn_Click(object sender, EventArgs e)
        {
            keepmonitorbtn.Hide();
        }

        private void taskmgrUI_ItemCheck(object sender, EventArgs e)
        {

        }

        private void taskmgrclose_Click(object sender, EventArgs e)
        {
            taskmgrbox.Hide();
        }

        private void EndTask_Click(object sender, EventArgs e)
        {

            if (taskmgrUI.SelectedIndex >= 0)

            {

                taskmgrUI.Items.RemoveAt(taskmgrUI.SelectedIndex);

            }

        }

        private void rdpexit_Click(object sender, EventArgs e)
        {
            rdpbox.Hide();
        }

        private void morefiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rdplink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            recentapp.Text = "Remote Desktop Connection";

            customapp.Show();
            rdpbox.Show();
            connectlabelrdp.Hide();
            moreoptionboxrdp.Hide();
            form3.Hide();
            filesboxrdp.Hide();
            notepadboxrdp.Hide();

            //RDP Loading
            menurdp.Hide();
            disconnectrdp.Hide();
            browserboxrdp.Hide();
            settingsboxrdp.Hide();
            backgrounddisplayboxrdp.Hide();
            Remotedesktoprdp.Hide();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rdpusername.Text))
            {
                MessageBox.Show("Please enter a username to connect", "Unable to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (string.IsNullOrEmpty(rdppassword.Text))
                {
                    MessageBox.Show("Please enter a password to connect", "Unable to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    connectlabelrdp.Show();

                }

            }
            if (string.IsNullOrEmpty(rdppassword.Text))
            {
                MessageBox.Show("Please enter a password to connect", "Unable to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                connectlabelrdp.Show();

            }
            MessageBox.Show("Connect?", "Confirm Connection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            form3.Show();
        }

        private void moreoptionsrdpboxhide_Click(object sender, EventArgs e)
        {
            moreoptionboxrdp.Show();
        }

        private void moreoptionsrdp_Click(object sender, EventArgs e)
        {
            moreoptionboxrdp.Show();

        }

        private void saverdpfile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("File recording: Recording Interface when ready.", "Saved File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            saverdpfile.Text = "Stop Recording";

        }

        private void rdpconnecthide_Click(object sender, EventArgs e)
        {
            form3.Hide();
        }
        //Remote Desktop
        private void form3_Load(object sender, EventArgs e)
        {
        }

        private void rdpmenu_Click(object sender, EventArgs e)
        {
            menurdp.Show();


        }

        private void button21_Click(object sender, EventArgs e)
        {
            menurdp.Hide();
        }

        private void rdpshutdown_Click(object sender, EventArgs e)
        {
            form3.Hide();
        }

        private void logoffrdp_Click(object sender, EventArgs e)
        {
            form3.Hide();
        }

        private void rdpdisconnecthide_Click(object sender, EventArgs e)
        {
            disconnectrdp.Hide();
        }

        private void sysinfordp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            notepadboxrdp.Show();
            notepadboxrdp.Text = "System Info";
            notepadUIrdp.Text = "NATO -7 COMMAND TERMINAL, ALL RIGHTS RESERVED";
        }

        private void tetrisrdp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void cmdrdp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            notepadboxrdp.Show();
            notepadboxrdp.Text = "Command Terminal";
            notepadUIrdp.Text = "NATO -7 COMMAND TERMINAL, ALL RIGHTS RESERVED";


        }

        private void filesrdp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            filesboxrdp.Show();
            filesdirrdp.Navigate(@"file:///C:\Users\Alex\source\repos\NATO-OS%207\NATO-OS%207\OS");

        }

        private void notepadrdp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            notepadboxrdp.Show();
        }

        private void browserrdp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            browserboxrdp.Show();
        }

        private void settingsrdp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            settingsboxrdp.Show();
            storageboxrdp.Hide();
        }

        private void coderdp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            notepadboxrdp.Show();
            notepadUIrdp.Text = "CODE EDITOR - NATO OS7";
            notepadboxrdp.Text = "Code Editor";
        }

        private void rdpweb_Click(object sender, EventArgs e)
        {
            browserboxrdp.Show();
        }

        private void browserhiderdp_Click(object sender, EventArgs e)
        {
            browserboxrdp.Hide();
        }

        private void browsergordp_Click(object sender, EventArgs e)
        {
            webbrowserrdp.Navigate(browserbarrdp.Text);
        }

        private void browserrefreshrdp_Click(object sender, EventArgs e)
        {
            webbrowserrdp.Refresh();
        }

        private void closefilesrdp_Click(object sender, EventArgs e)
        {
            filesboxrdp.Hide();
        }

        private void filesdirrdp_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void hidenotepadrdp_Click(object sender, EventArgs e)
        {
            notepadboxrdp.Hide();
        }

        private void rdpnotepad_Click(object sender, EventArgs e)
        {
            notepadboxrdp.Show();
            notepadboxrdp.Text = "NotePad";
            notepadUIrdp.Text = "";

        }

        private void button20_Click(object sender, EventArgs e)
        {
            disconnectrdp.Show();
        }

        private void moreoptionboxrdp_Enter(object sender, EventArgs e)
        {

        }

        private void rdpcode_Click(object sender, EventArgs e)
        {
            notepadboxrdp.Show();
            notepadUIrdp.Text = "CODE EDITOR - NATO OS7";
            notepadboxrdp.Text = "Code Editor";
        }

        private void settingsboxclose_Click(object sender, EventArgs e)
        {
            settingsboxrdp.Hide();
        }

        private void backgroundchange1_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;
        }
        private void backgroundchange2_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;
        }
        private void backgroundchange3_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Blue;
        }
        private void backgroundchange4_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Purple;
        }
        private void backgroundchange5_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
        }

        private void backgroundchange6_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;


        }
        private void backgroundchange7_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;

        }

        private void displaybtnhiderdp_Click(object sender, EventArgs e)
        {
            backgrounddisplayboxrdp.Hide();
        }

        private void backgrounddisplaybtnrdp_Click(object sender, EventArgs e)
        {
            backgrounddisplayboxrdp.Show();
        }

        private void storagebtnrdp_Click(object sender, EventArgs e)
        {
            storageboxrdp.Show();
            storagebarrdp.Value = 19;
            appsbarrdp.Value = 30;
            systembarrdp.Value = 28;
            filesbarrdp.Value = 20;
            otherbarrdp.Value = 10;
        }

        private void form3red_Click(object sender, EventArgs e)
        {
            form3.BackColor = Color.Red;
        }

        private void form3green_Click(object sender, EventArgs e)
        {
            form3.BackColor = Color.Green;

        }

        private void form3blue_Click(object sender, EventArgs e)
        {
            form3.BackColor = Color.Blue;

        }

        private void form3black_Click(object sender, EventArgs e)
        {
            form3.BackColor = Color.Black;

        }

        private void form3white_Click(object sender, EventArgs e)
        {
            form3.BackColor = Color.White;

        }

        private void form3gray_Click(object sender, EventArgs e)
        {
            form3.BackColor = Color.Gray;

        }

        private void hidestoragerdp_Click(object sender, EventArgs e)
        {
            storageboxrdp.Hide();
        }

        private void storagebarrdp_Click(object sender, EventArgs e)
        {

        }

        private void settingsboxrdp_Enter(object sender, EventArgs e)
        {

        }

        private void cleartempfilesrdp_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Clear .TEMP files? \n (Temororary Files)", "System Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (DialogResult == DialogResult.Yes)
            {
                otherbarrdp.Value = 3;
            }
            else if (DialogResult == DialogResult.No)
            {
                //Aborted
            }
        }

        private void rdpconnectlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Useless
        }

        private void RdpHide_Click(object sender, EventArgs e)
        {
            Remotedesktoprdp.Hide();
        }

        private void rdpconnectlink_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //Useless
        }

        private void rdpconnectlink_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Remotedesktoprdp.Show();
            rdpform4.Hide();
            MessageBox.Show("Memory Usage: VERY HIGH, please 'or/terminate' %rdpclient.exe", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);

        }

        private void connectrdpbtn_Click(object sender, EventArgs e)
        {
            rdpform4.Show();
            menurdp1.Hide();
            browserboxrdp1.Hide();
            rdpbox1.Hide();
        }

        private void menubtnrdp1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Undefined Action, Line 592105 RW940 (getNewline.(void)webactions.write;) 'void' is undefined.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            menurdp1.Show();
        }

        private void webbtnrdp1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Undefined Action, Line 790111 RW1562 (if action.(params)void (webInto.var1004) = 'newParams'.over()) 'newParams' is undefined.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            browserboxrdp1.Show();
        }

        private void menurdp1hide_Click(object sender, EventArgs e)
        {
            menurdp1.Hide();

        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void navigate_Click(object sender, EventArgs e)
        {
            webBrowser2.Navigate(textBox3.Text);
        }

        private void webboxclose_Click(object sender, EventArgs e)
        {
            browserboxrdp1.Hide();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            rdpbox1.Hide();
        }

        private void rdplink1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rdpbox1.Show();
            form5.Hide();
        }

        private void connectbtnrdp_Click(object sender, EventArgs e)
        {
            form5.Show();
            rdpmenu1.Hide();
            webbox.Hide();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            rdpmenu1.Show();
        }

        private void rdpmenu1hide_Click(object sender, EventArgs e)
        {
            rdpmenu1.Hide();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            webbox.Show();
        }

        private void webhide_Click(object sender, EventArgs e)
        {
            webbox.Hide();
        }

        private void webgo_Click(object sender, EventArgs e)
        {
            web1.Navigate("https://google.com");
        }
        //End Rdp Section Finished 11-13-2024 at 8:07PM
        private void shopappclose_Click(object sender, EventArgs e)
        {
            shopapp.Hide();

        }

        private void HomeAPP1_Click(object sender, EventArgs e)
        {

        }

        private void Account1_Click(object sender, EventArgs e)
        {
            HomeAPP1.Enabled = true;
            MessageBox.Show("Computer ID: 0771852462 \n Username: (see in developers app) \n App Uploads: 0", "Account Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void NATOapps1_Click(object sender, EventArgs e)
        {
            HomeAPP1.Enabled = true;
        }

        private void Library1_Click(object sender, EventArgs e)
        {
            InstallAppPageIndexBack.Enabled = false;

            HomeAPP1.Enabled = true;
            librarybox.Show();
        }

        private void ShowApps1_Click(object sender, EventArgs e)
        {
            HomeAPP1.Enabled = true;
            moreappsboxstore.Show();
        }

        private void form3_Enter(object sender, EventArgs e)
        {

        }

        private void closeappbox_Click(object sender, EventArgs e)
        {
            appbox1.Hide();
            ShowApps1.Show();
            DownloadStats.Text = "Download Incomplete!";
            DownloadStats.ForeColor = Color.Red;
            ChromiumDownloadBTN.Hide();
            SpotifyBtn.Hide();
            SketchplusBtn.Hide();
            backgroundchangerBtn.Hide();
        }

        private void buyappbtn1_Click(object sender, EventArgs e)
        {
            //Background Changer
            appbox1.Show();
            ShowApps1.Hide();
            apppicturebox1.Image = System.Drawing.Image.FromFile(@"C:\Users\Alex\source\repos\NATO-OS 7\NATO-OS 7\OS\%App%\App Shop\IMG-BackgChoser\IMG1.jpg");
            applabel.Text = "Background Chooser";
            appmodel.Text = "Version 1.25B fpr NATO OS";
            LabelVarShared.Text = "1";
            DownloadStats4.Hide();


        }

        private void buyappbtn2_Click(object sender, EventArgs e)
        {
            //Sketch+
            appbox1.Show();
            ShowApps1.Hide();
            apppicturebox1.Image = System.Drawing.Image.FromFile(@"C:\Users\Alex\source\repos\NATO-OS 7\NATO-OS 7\OS\%App%\App Shop\IMG-Sketch%15\IMG1.jfif");
            applabel.Text = "Sketch+";
            appmodel.Text = "Version 2.0 Using CSHARP";
            LabelVarShared.Text = "2";
            SketchplusBtn.Show();


        }

        private void buyappbtn3_Click(object sender, EventArgs e)
        {
            //Spotify for NATO
            appbox1.Show();
            ShowApps1.Hide();
            apppicturebox1.Image = System.Drawing.Image.FromFile(@"C:\Users\Alex\source\repos\NATO-OS 7\NATO-OS 7\OS\%App%\App Shop\SPOTIFY\IMG1.jpg");
            applabel.Text = "Spotify for NATO OS-7";
            appmodel.Text = "Version ? for NATO OS-6 + 7";
            LabelVarShared.Text = "3";
            SpotifyBtn.Show();


        }

        private void buyappbtn4_Click(object sender, EventArgs e)
        {
            //Chromium
            appbox1.Show();
            ShowApps1.Hide();
            apppicturebox1.Image = System.Drawing.Image.FromFile(@"C:\Users\Alex\source\repos\NATO-OS 7\NATO-OS 7\OS\%App%\App Shop\IMG-CHROME\IMG1.png");
            applabel.Text = "Google Chrome for NATO OS-7";
            appmodel.Text = "Google Chrome for NATO OS-5 + 7";
            LabelVarShared.Text = "4";
            ChromiumDownloadBTN.Show();



        }

        private void downloadappbtn_Click(object sender, EventArgs e)
        {






        }

        private void ChromiumDownloadBTN_Enter(object sender, EventArgs e)
        {

        }

        private async void DownloadChromiumBtN_Click(object sender, EventArgs e)
        {
            DownloadStats1.Show();
            DownloadStats1.Value = 10;
            await Task.Delay(5000);

            DownloadStats1.Value = 50;
            await Task.Delay(5000);

            DownloadStats1.Value = 100;
            MessageBox.Show("Downloading Chrome for NATO, please do not exit the app while downloading.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            shopappclose.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Chrome for Nato has been installed. OS/%APPS%/Chrome/chrome.APP", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            shopappclose.Enabled = true;
            chromiumbox.Show();

        }

        private async void SpotifyBtnDownload_Click(object sender, EventArgs e)
        {
            DownloadStats2.Show();
            DownloadStats2.Value = 10;
            await Task.Delay(5000);

            DownloadStats2.Value = 50;
            await Task.Delay(5000);

            DownloadStats2.Value = 100;
            MessageBox.Show("Downloading Spotify for NATO, please do not exit the app while downloading.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            shopappclose.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Spotify for Nato has been installed. OS/%APPS%/SpotifyMusic/spotify.APP", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            shopappclose.Enabled = true;
            spotifybox.Show();


        }

        private async void sketch_Click(object sender, EventArgs e)
        {
            DownloadStats3.Show();
            DownloadStats3.Value = 10;
            await Task.Delay(5000);

            DownloadStats3.Value = 50;
            await Task.Delay(5000);

            DownloadStats3.Value = 100;
            MessageBox.Show("Downloading Sketch+, please do not exit the app while downloading.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            shopappclose.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Sketch+ has been installed. OS/%APPS%/sketchplus/sketchp.APP", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            shopappclose.Enabled = true;
            sketchplusbox.Show();
        }

        private async void backchangerbtn_Click(object sender, EventArgs e)
        {
            DownloadStats4.Show();
            DownloadStats4.Value = 10;
            await Task.Delay(5000);

            DownloadStats4.Value = 50;
            await Task.Delay(5000);

            DownloadStats4.Value = 100;
            MessageBox.Show("Downloading Background Changer, please do not exit the app while downloading.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            shopappclose.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Background Changer has been installed. OS/%APPS%/backchanger/backchanger.APP", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            shopappclose.Enabled = true;
            backgroundchangebox.Show();
        }

        private void moreappsX_Click(object sender, EventArgs e)
        {
            moreappsboxstore.Hide();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }
        //  App Section
        private async void download1_Click(object sender, EventArgs e)
        {// Premium Player

            download1.Enabled = false;
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Download Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            moreappsX.Enabled = true;
            download1.Enabled = true;

            premiumplayerimg.Show();
            premiumplayertxt.Show();
            uninstallpremiumplayer.Show();
            openpremiumplayerbtn.Show();
        }

        private async void download2_Click(object sender, EventArgs e)
        {//File + PDF Reader
            download2.Enabled = false;
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Download Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            moreappsX.Enabled = true;
            download2.Enabled = true;
            filepdfreaderimg.Show();
            filepdfreadertxt.Show();
            uninstallfilepdfreader.Show();
            openfilepdfreaderbtn.Show();
        }

        private async void download3_Click(object sender, EventArgs e)
        {// Browser+
            download3.Enabled = false;
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Download Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            moreappsX.Enabled = true;
            download3.Enabled = true;
            browserplusimg.Show();
            browserplustxt.Show();
            uninstallbrowserplus.Show();
            openbrowserplusbtn.Show();
        }

        private async void download4_Click(object sender, EventArgs e)
        {//YourMail
            download4.Enabled = false;
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Download Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            moreappsX.Enabled = true;
            download4.Enabled = true;
            yourmailimg.Show();
            yourmailtxt.Show();
            uninstallyourmail.Show();
            openyourmail.Show();
        }

        private async void download5_Click(object sender, EventArgs e)
        {//stylusPad
            download5.Enabled = false;
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Download Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            moreappsX.Enabled = true;
            download5.Enabled = true;
            styluspadimg.Show();
            styluspadtxt.Show();
            uninstallstyluspad.Show();
            openstyluspad.Show();
        }

        private async void download6_Click(object sender, EventArgs e)
        {//Ahmst Paint
            download6.Enabled = false;
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Download Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            moreappsX.Enabled = true;
            download6.Enabled = true;
            amhstpaintimg.Show();
            amhstpainttxt.Show();
            uninstallamhstpaint.Show();
            openamhstpaint.Show();
        }

        private async void download7_Click(object sender, EventArgs e)
        {//Amhst Video Player
            download7.Enabled = false;
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Download Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            moreappsX.Enabled = true;
            download7.Enabled = true;
            amhstvideoplayer.Show();
            amhstvideoplayertxt.Show();
            uninstallamhstvideoplayer.Show();
            openamhstvideoplayer.Show();
        }

        private async void download8_Click(object sender, EventArgs e)
        {//Camcorder +
            download8.Enabled = false;
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Download Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            moreappsX.Enabled = true;
            download8.Enabled = true;
            camcorderplusimg.Show();
            camcorderplustxt.Show();
            uninstallcamcorderplus.Show();
            opencamcorderplus.Show();
        }

        private async void download9_Click(object sender, EventArgs e)
        {//iMailling
            download9.Enabled = false;
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Download Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            moreappsX.Enabled = true;
            download9.Enabled = true;
            imailingimg.Show();
            imailingtxt.Show();
            uninstallimailling.Show();
            openimailling.Show();
        }

        private async void download10_Click(object sender, EventArgs e)
        {//NoteNote
            download10.Enabled = false;
            await Task.Delay(500);

            DownloadStats.Text = "Downloading: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 29%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 39%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 48%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 59%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 68%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 74%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 87%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 98%";

            await Task.Delay(500);
            DownloadStats.Text = "Downloading: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Download Complete";
            MessageBox.Show("Download Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Perform actions after the delay

            // Re-enable the button
            closeappbox.Enabled = true;
            moreappsX.Enabled = true;
            download10.Enabled = true;
            notenoteimg.Show();
            notenotetxt.Show();
            uninstallnotenote.Show();
            opennotenote.Show();
        }

        private void libraryclose_Click(object sender, EventArgs e)
        {
            librarybox.Hide();
        }

        private void otherappslink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            recentapp.Text = "App Library";

            shopapp.Show();
            librarybox.Show();
            closelibrary.Show();
            libraryclose.Enabled = false;
            shopappclose.Enabled = false;
            searchappbtn.Enabled = false;
            HomeAPP1.Enabled = false;
            Account1.Enabled = false;
            Library1.Enabled = false;
            InstallAppPageIndexBack.Enabled = false;
            OpenFileandPdfReaderandBrowserPlusBox.Hide();


        }

        private void closelibrary_Click(object sender, EventArgs e)
        {
            librarybox.Hide();
            shopapp.Hide();
        }

        private void tetrisuninstall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Unable to uninstall /TETRIS.APP \n This file is in use by GameHost. \n Please close all uses of this app and try again.", "Uninstall incomplete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            tetrisuninstall.Enabled = false;
        }
        //Uninstall Apps
        private async void uninstallpremiumplayer_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";

            premiumplayerimg.Hide();
            premiumplayertxt.Hide();
            uninstallpremiumplayer.Hide();
            openpremiumplayerbtn.Hide();
        }

        private async void uninstallfilepdfreader_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            filepdfreaderimg.Hide();
            filepdfreadertxt.Hide();
            uninstallfilepdfreader.Hide();
            openfilepdfreaderbtn.Hide();
        }

        private async void uninstallbrowserplus_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            browserplusimg.Hide();
            browserplustxt.Hide();
            uninstallbrowserplus.Hide();
            openbrowserplusbtn.Hide();
        }

        private async void uninstallyourmail_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            yourmailimg.Hide();
            yourmailtxt.Hide();
            uninstallyourmail.Hide();
            openyourmail.Hide();
        }

        private async void uninstallstyluspad_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            styluspadimg.Hide();
            styluspadtxt.Hide();
            uninstallstyluspad.Hide();
            openstyluspad.Hide();

        }

        private async void uninstallamhstpaint_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            amhstpaintimg.Hide();
            amhstpainttxt.Hide();
            uninstallamhstpaint.Hide();
            openamhstpaint.Hide();
        }

        private async void uninstallamhstvideoplayer_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            amhstvideoplayer.Hide();
            amhstvideoplayertxt.Hide();
            uninstallamhstvideoplayer.Hide();
            openamhstvideoplayer.Hide();
        }

        private async void uninstallcamcorderplus_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            camcorderplusimg.Hide();
            camcorderplustxt.Hide();
            uninstallcamcorderplus.Hide();
            opencamcorderplus.Hide();
        }

        private async void uninstallimailling_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            imailingimg.Hide();
            imailingtxt.Hide();
            uninstallimailling.Hide();
            openimailling.Hide();
        }

        private async void uninstallnotenote_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            notenoteimg.Hide();
            notenotetxt.Hide();
            uninstallnotenote.Hide();
            opennotenote.Hide();
        }
        //Open Other Apps/ downloaded apps

        private void opentetrisbtn_Click(object sender, EventArgs e)
        {//For Later
         //noscript
            try
            {
                Task.Run(() =>
                {
                    PlayTetrisMelody();
                });
                TetrisGameBox.Visible = true;
                gameTimer.Start();

                string tetrisMusicURL = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/Packages (x86)/Tetris codec/media/main.mp3";
                tetrisMusic.URL = tetrisMusicURL;
                tetrisMusic.Ctlcontrols.play();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void openpremiumplayerbtn_Click(object sender, EventArgs e)
        {
            application.Show();
            AppPremiumPlayer_BackgroundChooserBOX.Show();
            AppPremiumPlayer_BackgroundChooserBOX.Text = "Premium Player";
            axWindowsMediaPlayer1.Hide();
            showvideolistpremiumplayerbtn.Hide();
            VideoNotesPremiumPlayer.Hide();
            VideoNotesPremiumPlayerTXT.Hide();
            PremiumPlayerTXT1.Show();
            videolistpremiumplayer.Show();
            //Background Changer
            BackgroundChangerTXT1.Hide();
            DefaultImageSelectorBox.Hide();
            ResetImageBackgroundChangerBTN.Hide();
            SolidColorsBTN.Hide();
            solidcolorsbox.Hide();

        }

        private void openfilepdfreaderbtn_Click(object sender, EventArgs e)
        {
            application.Show();
            OpenFileandPdfReaderandBrowserPlusBox.Show();
            OpenFileandPdfReaderandBrowserPlusBox.Text = "File & Pdf Reader";
            pdftext.Show();
            PDFUpload.Show();
            browserplusbrowser.Hide();
            BrowserPlus.Hide();
            SearchBrowserPlus.Hide();
            FowardBrowserPlus.Hide();
            RefreshBrowserPlus.Hide();
            BackwardBrowserPlus.Hide();
        }

        private void openbrowserplusbtn_Click(object sender, EventArgs e)
        {
            application.Show();
            OpenFileandPdfReaderandBrowserPlusBox.Show();
            OpenFileandPdfReaderandBrowserPlusBox.Text = "Browser+";
            pdftext.Hide();
            PDFUpload.Hide();
            //Paste
            browserplusbrowser.Show();
            browserplusbrowser.Navigate("file:///C:/Users/Alex/source/repos/NATO-OS%207/NATO-OS%207/OS/%25App%25/App%20Shop/other/INDEX.htm");
            BrowserPlus.Show();
            SearchBrowserPlus.Show();
            FowardBrowserPlus.Show();
            RefreshBrowserPlus.Show();
            BackwardBrowserPlus.Show();

        }

        private void openyourmail_Click(object sender, EventArgs e)
        {
            application.Show();
            YourMailandstylusPadBox.Show();
            YourMailandstylusPadBox.Text = "YourMail";
            from.Show();
            to.Show();
            content.Show();
            subj.Show();
            pass.Show();
            YourMail.Show();
            send.Show();
            label101.Show();
            label102.Show();
            label103.Show();
            label104.Show();
            label105.Show();
            //StylusPad
            styluspadheader.Hide();
            styluspad.Hide();
            axWebBrowser1.Hide();
            amhstpaintbox.Hide();
            amhstpaint.Hide();
            rateamhstpaint.Hide();
        }

        private void openstyluspad_Click(object sender, EventArgs e)
        {
            application.Show();
            YourMailandstylusPadBox.Show();
            YourMailandstylusPadBox.Text = "StylusPad";
            from.Hide();
            to.Hide();
            content.Hide();
            subj.Hide();
            pass.Hide();
            YourMail.Hide();
            send.Hide();
            label101.Hide();
            label102.Hide();
            label103.Hide();
            label104.Hide();
            label105.Hide();
            styluspadheader.Show();
            styluspad.Show();
            axWebBrowser1.Show();
            axWebBrowser1.Navigate("https://jspaint.app");
            amhstpaintbox.Hide();
            amhstpaint.Hide();
            rateamhstpaint.Hide();
        }

        private void openamhstpaint_Click(object sender, EventArgs e)
        {
            application.Show();
            YourMailandstylusPadBox.Show();
            YourMailandstylusPadBox.Text = "Amhst Paint";
            axWebBrowser1.Show();
            axWebBrowser1.Navigate("https://jspaint.app");
            from.Hide();
            to.Hide();
            content.Hide();
            subj.Hide();
            pass.Hide();
            YourMail.Hide();
            send.Hide();
            label101.Hide();
            label102.Hide();
            label103.Hide();
            label104.Hide();
            label105.Hide();
            styluspadheader.Hide();
            styluspad.Hide();
            amhstpaintbox.Show();
            amhstpaint.Show();
            rateamhstpaint.Show();
        }

        private void openamhstvideoplayer_Click(object sender, EventArgs e)
        {
            application.Show();
            AmhstVideoPlayerandCamcorderPlusBox.Show();
            camcorderimg.Hide();
            camcordertxt1.Hide();
            camcordertxt2.Hide();
            CamcorderPlusTooltip.Hide();
            // noscript = cameraVideo.Show();
            AmhstVideoPlayerandCamcorderPlusBox.Text = "Amhst Video Player";
            outputfoldertxtcamcorderplus.Hide();
            textBox7.Hide();
            startbtncamcorder.Hide();
            stopbtncamcorder.Hide();
            Capturebtncamcorderplus.Hide();
            BrowseBtnCamcorderplus.Hide();
            //Video Player
            MenuBoxAmhstVideoPlayer.Hide();
            AmhstVideoPlayerIcon.Show();
            AmhstVideoPlayerTXT1.Show();
            AmhstVideoPlayerTXT2.Show();
            axWindowsMediaPlayer2.Show();
            RateUsBTNAmhstVideoPlayer.Show();
        }

        private void opencamcorderplus_Click(object sender, EventArgs e)
        {
            application.Show();
            AmhstVideoPlayerandCamcorderPlusBox.Show();
            camcorderimg.Show();
            camcordertxt1.Show();
            camcordertxt2.Show();
            CamcorderPlusTooltip.Show();
            // noscript = cameraVideo.Show();

            AmhstVideoPlayerandCamcorderPlusBox.Text = "Camcorder+";
            outputfoldertxtcamcorderplus.Show();
            textBox7.Show();
            startbtncamcorder.Show();
            stopbtncamcorder.Show();
            Capturebtncamcorderplus.Show();
            BrowseBtnCamcorderplus.Show();
            //Video Player
            MenuBoxAmhstVideoPlayer.Hide();
            AmhstVideoPlayerIcon.Hide();
            AmhstVideoPlayerTXT1.Hide();
            AmhstVideoPlayerTXT2.Hide();
            axWindowsMediaPlayer2.Hide();
            RateUsBTNAmhstVideoPlayer.Hide();
        }

        private void openimailling_Click(object sender, EventArgs e)
        {
            application.Show();
            NoteNoteandImaillingBox.Show();
            NoteNoteandImaillingBox.Text = "Imailling";
            label110.Show();
            label111.Show();
            label112.Show();
            label113.Show();
            label114.Show();
            label115.Show();
            textBox8.Show();
            textBox9.Show();
            textBox10.Show();
            textBox11.Show();
            textBox12.Show();
            pictureBox23.Show();
            button30.Show();
            NoteNote.Hide();
            savenotenote.Hide();

        }

        private void opennotenote_Click(object sender, EventArgs e)
        {
            application.Show();
            NoteNoteandImaillingBox.Show();
            NoteNoteandImaillingBox.Text = "NoteNote";
            NoteNote.Show();
            savenotenote.Show();
            label110.Hide();
            label111.Hide();
            label112.Hide();
            label113.Hide();
            label114.Hide();
            label115.Hide();
            textBox8.Hide();
            textBox9.Hide();
            textBox10.Hide();
            textBox11.Hide();
            textBox12.Hide();
            pictureBox23.Hide();
            button30.Hide();
        }

        private void InstallAppPageIndexFoward_Click(object sender, EventArgs e)
        {
            InstallAppPageIndex.Text = "Page 2/2";
            page2library.Show();
            bottomhalfNotSafeForScripting.Show();
            InstallAppPageIndexBack.Enabled = true;
            InstallAppPageIndexFoward.Enabled = false;

        }

        private void InstallAppPageIndexBack_Click(object sender, EventArgs e)
        {
            InstallAppPageIndex.Text = "Page 1/2";
            page2library.Hide();
            bottomhalfNotSafeForScripting.Hide();
            InstallAppPageIndexBack.Enabled = false;
            InstallAppPageIndexFoward.Enabled = true;



        }

        private async void uninstallchromium_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            chromiumbox.Hide();
        }

        private async void uninstallsketchplus_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            sketchplusbox.Hide();
        }

        private async void uninstallbackgroundchanger_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            backgroundchangebox.Hide();
        }

        private async void uninstallspotify_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);

            DownloadStats.Text = "Uninstalling: 3%";
            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 10%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 19%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 30%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 41%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 44%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 56%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 69%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 79%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 88%";

            await Task.Delay(500);
            DownloadStats.Text = "Uninstalling: 99%";

            closeappbox.Enabled = false;
            moreappsX.Enabled = false;
            // Wait for 10 seconds (10000 milliseconds)
            await Task.Delay(7000);
            DownloadStats.Text = "Uninstalling Complete";

            MessageBox.Show("Uninstalling Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DownloadStats.Text = "Downloading: 0%";
            spotifybox.Hide();
        }

        private void openchromium_Click(object sender, EventArgs e)
        {
            application.Show();
            application.Text = "Google Chrome (NATO OS-7)";
            ChromiumBookmarksBar.Hide();
            ChromiumMenu.Hide();
            ChromiumFavoriteIcon.Show();
            ChromiumFowardBtn.Show();
            ChromiumWebBrowser.Show();
            ChromiumReloadBtn.Show();
            ChromiumBackBtn.Show();
            ChromiumSearchBarIMG.Show();
            ChromiumSettingsBtn.Show();
            ChromiumSearchBarLookupBtn.Show();
            ChromiumAppSearchBar1.Show();
            ChromiumWebBrowser.Navigate("https://google.com");
        }

        private void opensketchplus_Click(object sender, EventArgs e)
        {
            application.Show();
            application.Text = "Sketch Plus";
            ChromiumWebBrowser.Navigate("https://canvas.apps.chrome/");
            ChromiumWebBrowser.Show();
            sketchplustxt.Show();
            application.BackColor = SystemColors.ButtonFace;
            application.ForeColor = Color.Black;
        }

        private void openbackgroundchanger_Click(object sender, EventArgs e)
        {
            application.Show();

            AppPremiumPlayer_BackgroundChooserBOX.Hide();
            axWindowsMediaPlayer1.Hide();
            showvideolistpremiumplayerbtn.Hide();
            VideoNotesPremiumPlayer.Hide();
            VideoNotesPremiumPlayerTXT.Hide();
            PremiumPlayerTXT1.Hide();
            videolistpremiumplayer.Hide();
            //Background Changer
            BackgroundChangerTXT1.Show();
            AppPremiumPlayer_BackgroundChooserBOX.Show();
            DefaultImageSelectorBox.Show();
            AppPremiumPlayer_BackgroundChooserBOX.Text = "Background Changer";
            application.ForeColor = Color.Black;
            AppPremiumPlayer_BackgroundChooserBOX.BackColor = SystemColors.ButtonFace;
            //Thancopy
            ResetImageBackgroundChangerBTN.Show();
            SolidColorsBTN.Show();
            solidcolorsbox.Hide();




        }

        private void openspotify_Click(object sender, EventArgs e)
        {
            application.Show();
            SpotifyIMG.Show();
            SpotifyLabel1.Show();
            ChromiumWebBrowser.Show();
            ChromiumWebBrowser.Navigate("https://spotify.com");

        }

        private void closeapplication_Click(object sender, EventArgs e)
        {
            application.Hide();
            //Chrome
            ChromiumBookmarksBar.Hide();
            ChromiumMenu.Hide();
            ChromiumFavoriteIcon.Hide();
            ChromiumFowardBtn.Hide();
            ChromiumWebBrowser.Hide();
            ChromiumReloadBtn.Hide();
            ChromiumBackBtn.Hide();
            ChromiumSearchBarIMG.Hide();
            ChromiumSettingsBtn.Hide();
            ChromiumSearchBarLookupBtn.Hide();
            ChromiumAppSearchBar1.Hide();
            //Sketch+
            sketchplustxt.Hide();
            application.BackColor = SystemColors.ButtonFace;
            application.ForeColor = Color.Black;
            //Spotify
            SpotifyIMG.Hide();
            SpotifyLabel1.Hide();
            //Double-App Intregation
            AppPremiumPlayer_BackgroundChooserBOX.Hide();
            OpenFileandPdfReaderandBrowserPlusBox.Hide();
            //Background Changer

            AppPremiumPlayer_BackgroundChooserBOX.Hide();

            axWindowsMediaPlayer1.Hide();
            showvideolistpremiumplayerbtn.Hide();
            VideoNotesPremiumPlayer.Hide();
            VideoNotesPremiumPlayerTXT.Hide();
            PremiumPlayerTXT1.Hide();
            videolistpremiumplayer.Hide();
            //Background Changer
            BackgroundChangerTXT1.Hide();
            DefaultImageSelectorBox.Hide();
            ResetImageBackgroundChangerBTN.Hide();
            ResetImageBackgroundChangerBTN.Hide();
            SolidColorsBTN.Hide();
            solidcolorsbox.Hide();
            //FilePDFReader
            PDFUpload.Hide();
            pdftext.Hide();
            //Browser+
            browserplusbrowser.Hide();
            browserplusbrowser.Navigate("");
            BrowserPlus.Hide();
            SearchBrowserPlus.Hide();
            FowardBrowserPlus.Hide();
            RefreshBrowserPlus.Hide();
            BackwardBrowserPlus.Hide();
        }

        private void ChromiumShowBookmarkBarBtn_Click(object sender, EventArgs e)
        {
            ChromiumBookmarksBar.Show();
        }

        private void ChromiumSettingsBtn_Click(object sender, EventArgs e)
        {
            ChromiumMenu.Show();
        }

        private void ChromiumSearchBarLookupBtn_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser.Navigate(ChromiumAppSearchBar1.Text);
        }

        private void ChromiumFowardBtn_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser.GoForward();
        }

        private void ChromiumReloadBtn_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser.Refresh();
        }

        private void ChromiumBackBtn_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser.GoBack();
        }

        private void ChromiumMenuHide_Click(object sender, EventArgs e)
        {
            ChromiumMenu.Hide();
        }
        //linktree
        private void KSIMAIN_Click(object sender, EventArgs e)
        {

        }

        private void SMAIN_Click(object sender, EventArgs e)
        {

        }

        private void AppPremiumPlayer_BackgroundChooserBOX_Enter(object sender, EventArgs e)
        {

        }

        private void myTree_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void examplevideo1_Click(object sender, EventArgs e)
        {
            string f = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/Untitled_Video1.mp4";
            axWindowsMediaPlayer1.URL = f;
            axWindowsMediaPlayer1.Show();
            videolistpremiumplayer.Hide();
            showvideolistpremiumplayerbtn.Show();
            VideoNotesPremiumPlayer.Show();
        }

        private void showvideolistpremiumplayerbtn_Click(object sender, EventArgs e)
        {
            videolistpremiumplayer.Show();
        }

        private void videolistpremiumplayer_Enter(object sender, EventArgs e)
        {

        }

        private void MyVideoLabelPremiumPlayer1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string f = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/divideby0.mp4";
            axWindowsMediaPlayer1.URL = f;
            axWindowsMediaPlayer1.Show();
            videolistpremiumplayer.Hide();
            showvideolistpremiumplayerbtn.Show();
            VideoNotesPremiumPlayer.Show();
            VideoNotesPremiumPlayerTXT.Show();
        }

        private void MyVideoLabelPremiumPlayer2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string f = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/coins.mp4";
            axWindowsMediaPlayer1.URL = f;
            axWindowsMediaPlayer1.Show();
            videolistpremiumplayer.Hide();
            showvideolistpremiumplayerbtn.Show();
            VideoNotesPremiumPlayer.Show();
            VideoNotesPremiumPlayerTXT.Show();
        }

        private void MyVideoLabelPremiumPlayer3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string f = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/america.mp4";
            axWindowsMediaPlayer1.URL = f;
            axWindowsMediaPlayer1.Show();
            videolistpremiumplayer.Hide();
            showvideolistpremiumplayerbtn.Show();
            VideoNotesPremiumPlayer.Show();
            VideoNotesPremiumPlayerTXT.Show();
        }

        private void MyVideoPlayerPremiumPlayer4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string f = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/bigcar.mp4";
            axWindowsMediaPlayer1.URL = f;
            axWindowsMediaPlayer1.Show();
            videolistpremiumplayer.Hide();
            showvideolistpremiumplayerbtn.Show();
            VideoNotesPremiumPlayerTXT.Show();
            VideoNotesPremiumPlayer.Show();
        }

        private void DefaultImage1_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/%App%/BackgroundChanger/DefaultImage1.jpg");
        }

        private void DefaultImage2_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/%App%/BackgroundChanger/DefaultImage2.jpeg");
        }

        private void DefaultImage3_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/%App%/BackgroundChanger/DefaultImage3.jpg");
        }

        private void DefaultImage4_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/%App%/BackgroundChanger/DefaultImage4.jpeg");

        }

        private void DefaultImage5_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/%App%/BackgroundChanger/DefaultImage5.jpg");

        }

        private void ResetImageBackgroundChangerBTN_Click(object sender, EventArgs e)
        {


            this.BackColor = Color.Black;
        }

        private void SolidColorsBTN_Click(object sender, EventArgs e)
        {

            solidcolorsbox.Show();
        }

        private void CloseSolidColorBox_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("");

            solidcolorsbox.Hide();
        }

        private void redcolorbtn_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("");

            this.BackColor = Color.Red;
        }

        private void orangecolorbtn_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("");

            this.BackColor = Color.Orange;
        }

        private void yellowcolorbtn_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("");

            this.BackColor = Color.Yellow;
        }

        private void greencolorbtn_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("");

            this.BackColor = Color.Green;
        }

        private void bluecolorbtn_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("");

            this.BackColor = Color.Blue;

        }

        private void purplecolorbtn_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("");

            this.BackColor = Color.Purple;
        }

        private void PDFUpload_Click(object sender, EventArgs e)
        {
            PDFOpenerDialog.DefaultExt = ".pdf"; // Required file extension 

            PDFOpenerDialog.ShowDialog();
        }

        private void BackwardBrowserPlus_Click(object sender, EventArgs e)
        {
            browserplusbrowser.GoBack();
        }

        private void RefreshBrowserPlus_Click(object sender, EventArgs e)
        {
            browserplusbrowser.Refresh();
        }

        private void FowardBrowserPlus_Click(object sender, EventArgs e)
        {
            browserplusbrowser.GoForward();

        }

        private void SearchBrowserPlus_Click(object sender, EventArgs e)
        {
            browserplusbrowser.Navigate(BrowserPlus.Text);
        }

        private void CloseYourMailandStyluspadbtn_Click(object sender, EventArgs e)
        {
            application.Hide();
            YourMailandstylusPadBox.Hide();
            from.Hide();
            to.Hide();
            content.Hide();
            subj.Hide();
            pass.Hide();
            YourMail.Hide();
            send.Hide();
            label101.Hide();
            label102.Hide();
            label103.Hide();
            label104.Hide();
            label105.Hide();
        }

        private void send_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mm = new MailMessage();
                SmtpClient sc = new SmtpClient("smtp.gmail.com");
                mm.From = new MailAddress(from.Text);
                mm.To.Add(to.Text);
                mm.Subject = subj.Text;
                mm.Body = content.Text;
                sc.Port = 587;
                sc.Credentials = new NetworkCredential(from.Text, pass.Text);
                sc.EnableSsl = true;
                sc.Send(mm);
                MessageBox.Show("Sent Email!", "YourMail Client");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RememberMeCredentialsYourMailBtn_Click(object sender, EventArgs e)
        {

        }

        private void AutofillcredentialsYourMail_Click(object sender, EventArgs e)
        {
        }

        private void YourMail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks for using YourMail!", "YourMail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void NotificationsNATO_Click(object sender, EventArgs e)
        {
            notificationsboxNATO.Show();
        }

        private void CloseNotificationsBoxNATO_Click(object sender, EventArgs e)
        {
            notificationsboxNATO.Hide();
        }

        private void amhstratingimg_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Writing a review will help us alot!", "Amhst Paint", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void submitamhstpaintreview_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Thank you for writing a review! \n\n Your review will be looked at shortly.", "Amhst Paint", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void rateamhstpaint_Click(object sender, EventArgs e)
        {
            amhstpaintratingbox.Show();
        }

        private void closeamhstpaintratingbox_Click(object sender, EventArgs e)
        {
            amhstpaintratingbox.Hide();
        }

        private void startbtncamcorder_Click(object sender, EventArgs e)
        {
            Start_cam();
        }

        private void stopbtncamcorder_Click(object sender, EventArgs e)
        {
            frame.Stop();
        }

        private void BrowseBtnCamcorderplus_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            output = folderBrowserDialog1.SelectedPath;
        }

        private void Capturebtncamcorderplus_Click(object sender, EventArgs e)
        {
            if (output != "")
            {
                cameraVideo.Image.Save(output + "Untitled-1.png");
            }
        }

        private void HideCamcorderBtn_Click(object sender, EventArgs e)
        {
            AmhstVideoPlayerandCamcorderPlusBox.Hide();
            application.Hide();
        }

        private void AmhstVideoPlayerTXT1_Click(object sender, EventArgs e)
        {

        }

        private void MenuBtnAmhstVideoPlayer_Click(object sender, EventArgs e)
        {
            MenuBoxAmhstVideoPlayer.Show();
        }

        private void HideMenuAmhstVideoPlayer_Click(object sender, EventArgs e)
        {
            MenuBoxAmhstVideoPlayer.Hide();
            application.Hide();
        }

        private void BrowseFilesAmhstVideoPlayer_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void SampleVideoAmhstVideoPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string r = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/Untitled_Video1.mp4";
            axWindowsMediaPlayer2.URL = r;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            button28.Enabled = false;
        }

        private void MyVideo1Amhst_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string r = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/bigcar.mp4";
            axWindowsMediaPlayer2.URL = r;

        }

        private void MyVideo2Amhst_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string r = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/america.mp4";
            axWindowsMediaPlayer2.URL = r;

        }

        private void MyVideo3Amhst_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string r = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/coins.mp4";
            axWindowsMediaPlayer2.URL = r;

        }

        private void MyVideo4Amhst_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string r = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/divideby0.mp4";
            axWindowsMediaPlayer2.URL = r;

        }

        private void savenotenote_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            NoteNoteandImaillingBox.Hide();
            application.Hide();
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TIP: When sending an email, please make sure that both email addresses work.", "Imailling", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mm = new MailMessage();
                SmtpClient sc = new SmtpClient("smtp.gmail.com");
                mm.From = new MailAddress(textBox8.Text);
                mm.To.Add(textBox9.Text);
                mm.Subject = textBox10.Text;
                mm.Body = textBox11.Text;
                sc.Port = 587;
                sc.Credentials = new NetworkCredential(textBox8.Text, textBox12.Text);
                sc.EnableSsl = true;
                sc.Send(mm);
                MessageBox.Show("Sent Email!", "YourMail Client");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HideMailBTN_Click(object sender, EventArgs e)
        {
            MailBox.Hide();
        }

        private void internetlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MailBox.Show();
            HtmlEditorBox.Hide();
            MailBoxMainBox.Hide();
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            MailBox.Hide();
            browserBox.Show();

        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {
            HtmlEditorBox.Show();
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            //Mail
            MailBoxMainBox.Show();
            DownloadNatoDesigner.Show();

        }

        private void HideHTMLEditor_Click(object sender, EventArgs e)
        {
            HtmlEditorBox.Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            webBrowser3.DocumentText = richTextBox1.Text;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            editorbox.Show();
        }

        private void MailBoxHide_Click(object sender, EventArgs e)
        {
            MailBoxMainBox.Hide();
        }

        private void CloseNotificationNatoDesigner_Click(object sender, EventArgs e)
        {
            DownloadNatoDesigner.Hide();
            notificationline1.Text = "Download Nato Designer";
        }

        private void button35_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mm = new MailMessage();
                SmtpClient sc = new SmtpClient("smtp.gmail.com");
                mm.From = new MailAddress(frombox.Text);
                mm.To.Add(tobox.Text);
                mm.Subject = subjectbox.Text;
                mm.Body = contentbox.Text;
                sc.Port = 587;
                sc.Credentials = new NetworkCredential(frombox.Text, passwordbox.Text);
                sc.EnableSsl = true;
                sc.Send(mm);
                MessageBox.Show("Sent Email!", "Nato");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        private void LearnMoreNatoDesigner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("NATO Designer is a program that offers authentic app like:\n*Authentic Messenger\n*Authentic NotePad\n*Toolbar for Browser\nAuthentic News\n*Authentic Programmer\n*Authentic Photo Gallery\nThese apps are all free!*\n\n\nMUST REQUIRE A NATO DEVELOPER ACCOUNT CREATION", "Learn More");

        }

        private void HideSetupWizard1_Click(object sender, EventArgs e)
        {
            NatoDesignerSetupWizard.Hide();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            DownloadingNatoDesigner.Hide();
            DownloadBTNNatoDesigner.Enabled = false;
            NatoDesignerSetupWizard.Show();
            listBoxEventViewer.Items.Add("Attempted to download: NATO-DesignerSetupInstaller.APP");

        }

        private void IhavereadthetermsandconditionsNatoDesignerSetupWizard_CheckedChanged(object sender, EventArgs e)
        {
            if (IhavereadthetermsandconditionsNatoDesignerSetupWizard.Checked == true)
            {
                DownloadBTNNatoDesigner.Enabled = true;



            }
            else
            {
                DownloadBTNNatoDesigner.Enabled = false;

            }
        }

        private async void DownloadBTNNatoDesigner_Click(object sender, EventArgs e)
        {
            DownloadingNatoDesigner.Show();
            isDesignerCodeInstalled = true;
            DownloadingNatoDesigner.Value = 0;
            await Task.Delay(750);
            DownloadingNatoDesigner.Value = 10;
            await Task.Delay(750);
            DownloadingNatoDesigner.Value = 20;

            await Task.Delay(750);
            DownloadingNatoDesigner.Value = 30;

            await Task.Delay(750);
            DownloadingNatoDesigner.Value = 40;
            await Task.Delay(750);
            DownloadingNatoDesigner.Value = 50;
            await Task.Delay(750);
            DownloadingNatoDesigner.Value = 60;
            await Task.Delay(750);
            DownloadingNatoDesigner.Value = 70;
            await Task.Delay(750);
            DownloadingNatoDesigner.Value = 80;
            await Task.Delay(750);
            DownloadingNatoDesigner.Value = 90;
            await Task.Delay(750);
            DownloadingNatoDesigner.Value = 100;
            await Task.Delay(3000);
            MessageBox.Show("Download Complete!", "NATO Designer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NatoDesignerSetupWizard.Hide();
            NATODesigner.Show();
            NatoDesignerTXT1.Show();
            NatoDesignerSetStatusOnline.Show();
            NatoDesignerSignInMain.Show();

        }

        private void NATODesignerComboBoxHide_Click(object sender, EventArgs e)
        {
            NATODesignerComboBox.Hide();
        }

        private void NATODesigner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NATODesignerComboBox.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            browserBox.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void AuthenticMessengerIMG_Click(object sender, EventArgs e)
        {
            AuthenticMessengerApp.Show();
        }

        private void AuthenticNotepadIMG_Click(object sender, EventArgs e)
        {
            AuthenticNotePad.Show();
            NotePadBox.Hide();

        }

        private void pictureBox33_Click(object sender, EventArgs e)
        {
            //Movie Viewer
            AuthenticMovieViewer.Show();
            MediaPlayerBox.Hide();
        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            //Planner
            AuthenticPlanner.Show();
            ExampleCalendar1.Enabled = false;
            CalenFile.Hide();
        }

        private void pictureBox35_Click(object sender, EventArgs e)
        {
            //PhoneLink
            AuthenticPhoneLinkBox.Show();
            CallVideoPhoneLink.Hide();
            CallVideoPhoneLink.Hide();
            YouLabelPhoneLink.Hide();
            OtherUserNoCameraIconPhoneLink.Hide();
            OtherUserPhoneLink.Hide();
            OtherLabelPhoneLink.Hide();
            HangUpPhoneLink.Hide();
            MuteBtnPhoneLink.Hide();
            PhoneLinkPhoneBook.Hide();


        }

        private void pictureBox34_Click(object sender, EventArgs e)
        {
            //Designer Code
            DesignerCode.Show();
            CodeAreaDesignerCode.Hide();
            RunFileDesignerCode.Enabled = false;
            ConsoleDesignerCode.Enabled = false;
            LogsDesignerCode.Enabled = false;
            FileNameDesignerCode.Enabled = false;
            ShareDesignerCode.Enabled = false;
            SaveDesignerCode.Enabled = false;
            NewBlankFileDesignerCode.Enabled = true;
            ProgrammingLanguageDesignerCodeBox.Enabled = false;
            ProgrammingLanguageBoxDesignerCode.Hide();
            ProgrammingLanguageTypeDesignerCode.Text = "---";
            CodeAreaDesignerCode.Enabled = true;
            ExtentionBoxDesignerCode.Hide();
            ConsoleAndShareBoxDesignerCode.Hide();
            LogsListBoxDesignerBox.Items.Add("New instance today, end other instances:");



        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            epLocal = new IPEndPoint(IPAddress.Parse(TextLocalIP.Text), Convert.ToInt32(TextLocalPort.Text));
            sck.Bind(epLocal);

            epRemote = new IPEndPoint(IPAddress.Parse(TextRemoteIP.Text), Convert.ToInt32(TextRemotePort.Text));
            sck.Connect(epRemote);

            buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            sendingMessage = aEncoding.GetBytes(textMessage.Text);

            sck.Send(sendingMessage);
            listMessage.Items.Add("Me: " + textMessage.Text);
            textMessage.Text = "";
        }

        private void HideAuthenticMessager_Click(object sender, EventArgs e)
        {
            AuthenticMessengerApp.Hide();
        }

        private void NewFileAuthenticNotePad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NotePadBox.Show();
            TemplateCalendar1.Hide();
            TextBoxCalendarTemplate1.Hide();
            TextAreaCalendarTemplate1.Hide();
            ReopenLastFileNotePad.Hide();
            HeaderForm.Hide();
            String1Form.Hide();
            String2Form.Hide();
            String3Form.Hide();
            String4Form.Hide();
            String5Form.Hide();
            String6Form.Hide();
            CheckListBoxForm.Hide();
            ReportTemplateImage.Hide();
            ImagePlaceholderReplaceNotePad.Hide();
            ReportString1.Hide();
            ReportString2.Hide();
            ReportString3.Hide();
            LetterString1.Hide();
            LetterString2.Hide();
            LetterString3.Hide();
            LetterString4.Hide();
            PlainTextNotePad.Show();
        }

        private void OpenFileAuthenticNotePad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void SettingsAuthenticNotePad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void SearchOnlineTemplateAuthenticNotePadBTN_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No results found", "Search Template", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void CalendarNotePadTemplates_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The following template(s) will be downloaded onto this computer:\n\n\nCalendarTemplate.templ", "Download Template", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NotePadBox.Show();
            TemplateCalendar1.Show();
            TextBoxCalendarTemplate1.Show();
            TextAreaCalendarTemplate1.Show();
            ReopenLastFileNotePad.Hide();
            HeaderForm.Hide();
            String1Form.Hide();
            String2Form.Hide();
            String3Form.Hide();
            String4Form.Hide();
            String5Form.Hide();
            String6Form.Hide();
            CheckListBoxForm.Hide();
            ReportTemplateImage.Hide();
            ImagePlaceholderReplaceNotePad.Hide();
            ReportString1.Hide();
            ReportString2.Hide();
            ReportString3.Hide();
            LetterString1.Hide();
            LetterString2.Hide();
            LetterString3.Hide();
            LetterString4.Hide();
            SendLetterNotePad.Hide();
        }

        private void FormNotePadTemplates_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The following template(s) will be downloaded onto this computer:\n\n\nFormTemplate.templ", "Download Template", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NotePadBox.Show();
            ReportTemplateImage.Hide();
            ImagePlaceholderReplaceNotePad.Hide();
            ReportString1.Hide();
            ReportString2.Hide();
            ReportString3.Hide();
            ReopenLastFileNotePad.Hide();
            TemplateCalendar1.Hide();
            TextBoxCalendarTemplate1.Hide();
            TextAreaCalendarTemplate1.Hide();
            //Items
            HeaderForm.Show();
            String1Form.Show();
            String2Form.Show();
            String3Form.Show();
            String4Form.Show();
            String5Form.Show();
            String6Form.Show();
            CheckListBoxForm.Show();
            LetterString1.Hide();
            LetterString2.Hide();
            LetterString3.Hide();
            LetterString4.Hide();
            SendLetterNotePad.Hide();
            PlainTextNotePad.Hide();

        }

        private void ReportNotePadTemplates_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The following template(s) will be downloaded onto this computer:\n\n\nReportTemplate.templ", "Download Template", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NotePadBox.Show();

            ReopenLastFileNotePad.Hide();
            TemplateCalendar1.Hide();
            TextBoxCalendarTemplate1.Hide();
            TextAreaCalendarTemplate1.Hide();
            HeaderForm.Hide();
            String1Form.Hide();
            String2Form.Hide();
            String3Form.Hide();
            String4Form.Hide();
            String5Form.Hide();
            String6Form.Hide();
            CheckListBoxForm.Hide();

            ReportTemplateImage.Show();
            ImagePlaceholderReplaceNotePad.Show();
            ReportString1.Show();
            ReportString2.Show();
            ReportString3.Show();
            LetterString1.Hide();
            LetterString2.Hide();
            LetterString3.Hide();
            LetterString4.Hide();
            SendLetterNotePad.Hide();
            PlainTextNotePad.Hide();

        }

        private void LetterNotePadTemplates_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The following template(s) will be downloaded onto this computer:\n\n\nLetterBoxTemplate.templ", "Download Template", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NotePadBox.Show();
            ReportTemplateImage.Hide();
            ImagePlaceholderReplaceNotePad.Hide();
            ReportString1.Hide();
            ReportString2.Hide();
            ReportString3.Hide();
            ReopenLastFileNotePad.Hide();
            TemplateCalendar1.Hide();
            TextBoxCalendarTemplate1.Hide();
            TextAreaCalendarTemplate1.Hide();
            HeaderForm.Hide();
            String1Form.Hide();
            String2Form.Hide();
            String3Form.Hide();
            String4Form.Hide();
            String5Form.Hide();
            String6Form.Hide();
            CheckListBoxForm.Hide();
            LetterString1.Show();
            LetterString2.Show();
            LetterString3.Show();
            LetterString4.Show();
            SendLetterNotePad.Show();
            PlainTextNotePad.Hide();

        }

        private void SaveTXTDocumentBtn_Click(object sender, EventArgs e)
        {
            NotePadBox.Hide();
            MessageBox.Show("File Saved!", "Authentic NotePad", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReopenLastFileNotePad.Show();

        }

        private void ReopenLastFileNotePad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NotePadBox.Show();
            MessageBox.Show("Reopened Last File!", "Authentic NotePad", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button36_Click(object sender, EventArgs e)
        {
            AuthenticNotePad.Hide();
        }

        private void ImagePlaceholderReplaceNotePad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImagePlaceholderReplace.ShowDialog();
            ReportTemplateImage.Image = System.Drawing.Image.FromFile(ImagePlaceholderReplace.FileName);
        }

        private void SendLetterNotePad_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Send Letter?", "NOTEPAD-TransferEmailDirective.NATODesigner.VBS", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (DialogResult == DialogResult.Yes)
            {
                MailBoxMainBox.Show();
                MessageBox.Show("Unable to display Letter", "Error", MessageBoxButtons.RetryCancel);

            }
            else if (DialogResult == DialogResult.No)
            {
                //Aborted
            }
        }

        private void AuthenticMovieViewersHide_Click(object sender, EventArgs e)
        {
            AuthenticMovieViewer.Hide();
        }

        private void UploadVideoMovieViewer_Click(object sender, EventArgs e)
        {
            OpenVideoAuthenticMovieViewer.ShowDialog();
            if (OpenVideoAuthenticMovieViewer.ShowDialog() == DialogResult.OK)

            {
                MediaPlayerBox.Show();
                string videoPath = OpenVideoAuthenticMovieViewer.FileName;

                axWindowsMediaPlayer3.URL = videoPath;  // Set the video URL to play

            }
        }

        private void MovieViewer1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MediaPlayerBox.Show();
            string r = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/bigcar.mp4";
            axWindowsMediaPlayer3.URL = r;
        }

        private void MovieViewer2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MediaPlayerBox.Show();

            string r = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/america.mp4";
            axWindowsMediaPlayer3.URL = r;
        }

        private void MovieViewer3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MediaPlayerBox.Show();

            string r = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/coins.mp4";
            axWindowsMediaPlayer3.URL = r;
        }

        private void MovieViewer4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MediaPlayerBox.Show();

            string r = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/divideby0.mp4";
            axWindowsMediaPlayer3.URL = r;
        }

        private void ExampleVideo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void HideMediaPlayer_Click(object sender, EventArgs e)
        {
            MediaPlayerBox.Hide();
        }

        private void CloseAuthenticPlanner_Click(object sender, EventArgs e)
        {
            AuthenticPlanner.Hide();

        }

        private void CalendarDocumentBtn_Click(object sender, EventArgs e)
        {
            CalenFile.Show();
            string1NormalCalendar.Hide();
            string2NormalCalendar.Hide();
            string1ParagraphCalendar.Hide();
            string2ParagraphCalendar.Hide();
            string3ParagraphCalendar.Hide();
            string1CalendarDocument.Show();
            string2CalendarDocument.Show();
            ParagraphCalendar1.Hide();

        }

        private void CalendarParagraphBtn_Click(object sender, EventArgs e)
        {
            CalenFile.Show();
            string1NormalCalendar.Hide();
            string2NormalCalendar.Hide();
            ParagraphCalendar1.Show();
            string1ParagraphCalendar.Show();
            string2ParagraphCalendar.Show();
            string3ParagraphCalendar.Show();
            string1CalendarDocument.Hide();
            string2CalendarDocument.Hide();
        }

        private void CreateNewBtn_Click(object sender, EventArgs e)
        {
            CalenFile.Show();
            string1NormalCalendar.Show();
            string2NormalCalendar.Show();
            string1ParagraphCalendar.Hide();
            string2ParagraphCalendar.Hide();
            string3ParagraphCalendar.Hide();
            string1CalendarDocument.Hide();
            string2CalendarDocument.Hide();
            ParagraphCalendar1.Hide();

        }

        private void UploadCalenfiles_Click(object sender, EventArgs e)
        {
            CalenFile.Show();
        }

        private void HideCalenFile_Click(object sender, EventArgs e)
        {
            CalenFile.Hide();
            MessageBox.Show("File Saved!", "Authentic Planner", MessageBoxButtons.OK);

        }

        private void string2NormalCalendar_TextChanged(object sender, EventArgs e)
        {

        }
        private void OnThisMouseClick(object sender, MouseEventArgs e)
        {
            string clickInfo = $"New Click @ X: {e.X}, Y: {e.Y}, Button: {e.Button}";
            appstatisticsgroup.Items.Add(clickInfo);
        }
        private void HideWelcomeStartupBox_Click(object sender, EventArgs e)
        {
            WelcomeStartupBox.Hide();
        }

        private void WelcomeCenterOpenSettings_Click(object sender, EventArgs e)
        {
            customapp.Show();
            natocmd.Hide();
            settingsbox.Show();
            backgrounddisplaybox.Hide();
            discstoragebox.Hide();
            helpsupportbox.Hide();
            uninstallappsgroup.Hide();
            privacygroup.Hide();
            taskmgrbox.Hide();
            DownloadNatoDesigner.Show();
        }

        private void DownloadNatoDesignerBTN_Click(object sender, EventArgs e)
        {
            DownloadingNatoDesigner.Hide();
            DownloadBTNNatoDesigner.Enabled = false;
            NatoDesignerSetupWizard.Show();
        }

        private void DownloadAppsBtnNATOStartup_Click(object sender, EventArgs e)
        {
            DownloadNatoDesigner.Show();
            recentapp.Text = "Store";
            librarybox.Hide();
            SpotifyBtn.Hide();
            DownloadStats.ForeColor = Color.Black;
            DownloadStats.Text = "Downloading: 0%";
            recentapp.Text = "App Shop";
            shopapp.Show();
            HomeAPP1.Enabled = false;
            appbox1.Hide();
            ShowApps1.Show();
            //Download Buttons
            ChromiumDownloadBTN.Hide();
            //Download Stats
            DownloadStats1.Hide();
            DownloadStats1.Value = 0;
            DownloadStats2.Hide();
            DownloadStats2.Value = 0;
            DownloadStats3.Hide();
            DownloadStats3.Value = 0;
            moreappsboxstore.Hide();
            libraryclose.Enabled = true;
            shopappclose.Enabled = true;
            searchappbtn.Enabled = true;
            HomeAPP1.Enabled = true;
            Account1.Enabled = true;
            Library1.Enabled = true;
            closelibrary.Hide();
            OpenFileandPdfReaderandBrowserPlusBox.Hide();
            YourMailandstylusPadBox.Hide();

        }

        private void HideBugsList_Click(object sender, EventArgs e)
        {
            BugsListNATO.Hide();
        }

        private void FixedBugsListNATO_Click(object sender, EventArgs e)
        {
            BugsListNATO.Show();
        }

        private void NatoDesignerSignInMain_Click(object sender, EventArgs e)
        {
            //Sign into Nato Designer main button
            NatoDesignerSignInBox.Show();
            CallNatoDesigner.Enabled = true;
            PhoneBookNatoDesigner.Enabled = true;
            MissedCallsNatoDesigner.Enabled = true;
            InfoNatoDesigner.Enabled = true;
            NumberListPhoneLink.Enabled = true;
            TestCallBtnPhoneLink.Enabled = true;


        }

        private void HideNatoDesignerSignInBtn_Click(object sender, EventArgs e)
        {
            NatoDesignerSignInBox.Hide();
        }

        private void MainSignInBtnNatoDesignerWelcomeCenterSignInScreenButton_Click(object sender, EventArgs e)
        {

            if (UsernameBoxNatoDesigner1.Text != "")
            {
                SetStatusNatoDesignerSignIn.Enabled = true;
                NatoDesignerSignInBox.Hide();
            }

            if (PasswordBoxNatoDesigner1.Text != "")
            {
                SetStatusNatoDesignerSignIn.Enabled = true;
                NatoDesignerSignInBox.Hide();

            }


        }

        private void welcomemsglink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WelcomeStartupBox.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UsernameBoxNatoDesigner1.Text = "";
            PasswordBoxNatoDesigner1.Text = "";
        }

        private void HideAuthenticPhoneLinkBox_Click(object sender, EventArgs e)
        {
            AuthenticPhoneLinkBox.Hide();
        }

        private void SignOutBtnNatoDesignerPhoneLink_Click(object sender, EventArgs e)
        {
            CallNatoDesigner.Enabled = false;
            PhoneBookNatoDesigner.Enabled = false;
            MissedCallsNatoDesigner.Enabled = false;
            InfoNatoDesigner.Enabled = false;
            NumberListPhoneLink.Enabled = false;
            TestCallBtnPhoneLink.Enabled = false;
        }

        private void CallNatoDesigner_Click(object sender, EventArgs e)
        {
            if (DialBoxPhoneLink.Text != "")
            {
                //Show Camera
                Start_cam();
                CallVideoPhoneLink.Show();
                YouLabelPhoneLink.Show();
                OtherUserNoCameraIconPhoneLink.Show();
                OtherUserPhoneLink.Show();
                OtherLabelPhoneLink.Show();
                HangUpPhoneLink.Show();
                MuteBtnPhoneLink.Show();
                PhoneBookPhoneLink.Items.Add(DialBoxPhoneLink.Text);

            }
            else
            {
                MessageBox.Show("Unable to call user, no number specified", "Phone Link");
            }
        }

        private void PhoneBookNatoDesigner_Click(object sender, EventArgs e)
        {
            PhoneLinkPhoneBook.Show();
            MissedCallsBoxPhoneLink.Hide();
            PhoneBookPhoneLink.Show();
        }

        private void MissedCallsNatoDesigner_Click(object sender, EventArgs e)
        {
            PhoneLinkPhoneBook.Show();
            MissedCallsBoxPhoneLink.Show();
            PhoneBookPhoneLink.Hide();
        }

        private void InfoNatoDesigner_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About Phone Link:\n\n Phone Link Version 2.0\nPhone Link Registered for Administrator account\n for NATO OS-7 (This PC)\n\nMy Number: NATO Designer Account, ---------@nato.com\nFor More Information, visit: https://designer.nato.com/phonelink", "Phone Link");

        }

        private void CallingListNatoDesigner_Click(object sender, EventArgs e)
        {

        }

        private void label174_Click(object sender, EventArgs e)
        {

        }

        private void HangUpPhoneLink_Click(object sender, EventArgs e)
        {
            CallVideoPhoneLink.Hide();
            YouLabelPhoneLink.Hide();
            OtherUserNoCameraIconPhoneLink.Hide();
            OtherUserPhoneLink.Hide();
            OtherLabelPhoneLink.Hide();
            DialBoxPhoneLink.Text = "";
            HangUpPhoneLink.Hide();
            MuteBtnPhoneLink.Hide();

        }

        private void TestCallBtnPhoneLink_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Test Call? \n User phoned: /://00TEST (Nato Designer Account)", "Call User", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (DialogResult == DialogResult.Yes)
            {
                Start_cam();
                CallVideoPhoneLink.Show();
                YouLabelPhoneLink.Show();
                OtherUserNoCameraIconPhoneLink.Show();
                OtherUserPhoneLink.Show();
                OtherLabelPhoneLink.Show();
                HangUpPhoneLink.Show();
                MuteBtnPhoneLink.Show();
                DialBoxPhoneLink.Text = "/://00TEST";
                PhoneBookPhoneLink.Items.Add("Test Call");

            }
            else if (DialogResult == DialogResult.No)
            {
                //Aborted
            }
        }

        private void HidePhoneBookPhoneLink_Click(object sender, EventArgs e)
        {
            PhoneLinkPhoneBook.Hide();
        }
        //Designer Code

        private void HideDesignerCode_Click(object sender, EventArgs e)
        {
            createNewFileToolStripMenuItem.Enabled = true;

            DesignerCode.Hide();
            LogsListBoxDesignerBox.Items.Add("Closed all instances. File autosaved: " + FileNameDesignerCode.Text + "." + ProgrammingLanguageTypeDesignerCode.Text);


        }

        private void NewBlankFileDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Code Debug Area
            CodeAreaDesignerCode.Show();
            NewBlankFileDesignerCode.Enabled = false;
            RunFileDesignerCode.Enabled = true;
            ConsoleDesignerCode.Enabled = true;
            LogsDesignerCode.Enabled = true;
            FileNameDesignerCode.Enabled = true;
            ShareDesignerCode.Enabled = true;
            SaveDesignerCode.Enabled = true;
            ProgrammingLanguageDesignerCodeBox.Enabled = true;
            ProgrammingLanguageTypeDesignerCode.Text = "Plain Text";
            LogsListBoxDesignerBox.Items.Add("New Project was created");
            createNewFileToolStripMenuItem.Enabled = false;
            CodeAreaDesignerCode.Text = "";

        }

        private void UploadFileDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void UploadFolderDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
        }

        private void SharedFilesDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("No shared files, unable to preview area", "Designer Code");

        }

        private void ExtentionsLinkDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ExtentionBoxDesignerCode.Show();
        }

        private void BuildYourFirstAppInFiveMinutesButtonNATODesignerCodeButton_Click(object sender, EventArgs e)
        {
            browserBox.Show();
            WebBrowser1.Navigate("https://goFwd.pws.xyz/NATOPG1?RefId=buildyourfirstappinfiveminutes?type=html");
        }

        private void DevelopMobileAppsButtonNATODesignerCodeBtn_Click(object sender, EventArgs e)
        {
            browserBox.Show();
            WebBrowser1.Navigate("https://goFwd.pws.xyz/NATOPG5?RefId=DevelopeMobileAppsForIOSAndroid?type=html");

        }

        private void OpenLastFileDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CodeAreaDesignerCode.Show();
            MessageBox.Show("Repoened Last File", "Designer Code");
            NewBlankFileDesignerCode.Enabled = false;
            RunFileDesignerCode.Enabled = true;
            ConsoleDesignerCode.Enabled = true;
            LogsDesignerCode.Enabled = true;
            FileNameDesignerCode.Enabled = true;
            ShareDesignerCode.Enabled = true;
            SaveDesignerCode.Enabled = true;
            ProgrammingLanguageDesignerCodeBox.Enabled = true;
            LogsListBoxDesignerBox.Items.Add("Repoened File: " + FileNameDesignerCode.Text + "." + ProgrammingLanguageTypeDesignerCode.Text);


        }



        private void ConsoleDesignerCode_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cannot open console, website denied console entry", "Console", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DesignerCode_Enter(object sender, EventArgs e)
        {

        }

        private void LogsDesignerCode_Click(object sender, EventArgs e)
        {
            ConsoleAndShareBoxDesignerCode.Show();
            ConsoleAndShareBoxDesignerCode.Text = "Logs";
            LogsListBoxDesignerBox.Show();
            ContactsTextDesignerCode.Hide();
            checkedListBox2.Hide();
        }

        private void ShareDesignerCode_Click(object sender, EventArgs e)
        {
            ConsoleAndShareBoxDesignerCode.Show();
            ConsoleAndShareBoxDesignerCode.Text = "Share";
            LogsListBoxDesignerBox.Hide();
            ContactsTextDesignerCode.Show();
            checkedListBox2.Show();

        }

        private void SaveDesignerCode_Click(object sender, EventArgs e)
        {
            createNewFileToolStripMenuItem.Enabled = true;

            NewBlankFileDesignerCode.Enabled = true;
            RunFileDesignerCode.Enabled = false;
            ConsoleDesignerCode.Enabled = false;
            LogsDesignerCode.Enabled = false;
            FileNameDesignerCode.Enabled = false;
            ShareDesignerCode.Enabled = false;
            SaveDesignerCode.Enabled = false;
            ProgrammingLanguageDesignerCodeBox.Enabled = false;
            CodeAreaDesignerCode.Hide();
        }

        private void CodeAreaDesignerCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void HideProgrammingLanguageListDesignerCodeBtn_Click(object sender, EventArgs e)
        {
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void ProgrammingLanguageDesignerCodeBox_Click(object sender, EventArgs e)
        {
            ProgrammingLanguageBoxDesignerCode.Show();

        }
        private void RunFileDesignerCode_Click(object sender, EventArgs e)
        {
            LogsListBoxDesignerBox.Items.Add("File deployed: " + FileNameDesignerCode.Text + "." + ProgrammingLanguageTypeDesignerCode.Text);
            RunFileDesignerCode.Enabled = false;
            CodeAreaDesignerCode.Enabled = false;
            DebugWindowDesignerCode.Show();
            DebugWindowBrowserDesignerCode.DocumentText = CodeAreaDesignerCode.Text;
            DebugWindowDesignerCode.Text = FileNameDesignerCode.Text;
        }
        private void BatchDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "Batch";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void CDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "C";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void CSharpDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "C#";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void CPlusPlusDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "C++";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void ClojureDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "Clojure";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void CoffeeScriptDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "CoffeeScript";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void CSSDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "CSS";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void CUDACplusplusDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "CUDA C++";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void GoDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "Go";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void GroovyDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "Groovy";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void HTMLDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "HTML";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void JavaDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "Java";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void JavascriptDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "JavaScript";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void JSONDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "JSON";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void PythonDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "Python";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void PHPDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "PHP";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void XMLDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProgrammingLanguageTypeDesignerCode.Text = "XML";
            ProgrammingLanguageBoxDesignerCode.Hide();

        }

        private void HideDebugWindowDesignerCode_Click(object sender, EventArgs e)
        {
            LogsListBoxDesignerBox.Items.Add("File closed: " + FileNameDesignerCode.Text + "." + ProgrammingLanguageTypeDesignerCode.Text);
            LogsListBoxDesignerBox.Items.Add("Debug was succesfull");

            DebugWindowDesignerCode.Hide();
            CodeAreaDesignerCode.Enabled = true;
            RunFileDesignerCode.Enabled = true;


        }

        private void RefreshDebugWindowDesigner_Click(object sender, EventArgs e)
        {
            DebugWindowBrowserDesignerCode.Refresh();
        }

        private void FowardDebugWindowDesignerCode_Click(object sender, EventArgs e)
        {
            DebugWindowBrowserDesignerCode.GoForward();
        }

        private void BackDebugWindowDesignerCode_Click(object sender, EventArgs e)
        {
            DebugWindowBrowserDesignerCode.GoBack();
        }

        private void HideExtentionBox_Click(object sender, EventArgs e)
        {
            ExtentionBoxDesignerCode.Hide();
        }

        private void AdditionalExtentionsBoxDesigner_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Download Extention?", "Designer Code", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (DialogResult == DialogResult.Yes)
            {

                MessageBox.Show("Download Complete!", "Designer Code");
                LogsListBoxDesignerBox.Items.Add("New extention downloaded for file: " + FileNameDesignerCode.Text + "." + ProgrammingLanguageTypeDesignerCode.Text);

            }
            else if (DialogResult == DialogResult.No)
            {
                //Aborted
            }
        }

        private void SearchExtentionsDesignerCode_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No results for: " + FindExtentionsDesignerCode.Text, "Designer Code Extention Search");

        }
        //Context Menu Strip
        private void syslabel_Click(object sender, EventArgs e)
        {
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            systeminfobox.Show();
        }

        private void downloadNATODesignerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadNatoDesigner.Show();
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;

        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;

        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;

        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Blue;

        }

        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Purple;

        }

        private void greyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;

        }

        private void webBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browserBox.Show();
        }

        private void codeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorbox.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customapp.Show();
            natocmd.Hide();
            settingsbox.Show();
            backgrounddisplaybox.Hide();
            discstoragebox.Hide();
            helpsupportbox.Hide();
            uninstallappsgroup.Hide();
            privacygroup.Hide();
            taskmgrbox.Hide();
            DownloadNatoDesigner.Show();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void HideConsoleAndShareDesignerCodeBox_Click(object sender, EventArgs e)
        {
            ConsoleAndShareBoxDesignerCode.Hide();
        }

        private void openPublicLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleAndShareBoxDesignerCode.Show();
            ConsoleAndShareBoxDesignerCode.Text = "Logs";
            LogsListBoxDesignerBox.Show();
        }

        private void whiteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DesignerCode.BackColor = Color.White;
            DesignerCode.ForeColor = Color.Black;

        }

        private void defualtGreyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesignerCode.BackColor = Color.FromArgb(64, 64, 64);
            DesignerCode.BackColor = Color.White;

        }

        private void createNewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Code Debug Area
            CodeAreaDesignerCode.Show();
            NewBlankFileDesignerCode.Enabled = false;
            RunFileDesignerCode.Enabled = true;
            ConsoleDesignerCode.Enabled = true;
            LogsDesignerCode.Enabled = true;
            FileNameDesignerCode.Enabled = true;
            ShareDesignerCode.Enabled = true;
            SaveDesignerCode.Enabled = true;
            ProgrammingLanguageDesignerCodeBox.Enabled = true;
            ProgrammingLanguageTypeDesignerCode.Text = "Plain Text";
            LogsListBoxDesignerBox.Items.Add("New Project was created");
            createNewFileToolStripMenuItem.Enabled = false;

        }

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createNewFileToolStripMenuItem.Enabled = true;

            DesignerCode.Hide();
            LogsListBoxDesignerBox.Items.Add("Closed all instances. File autosaved: " + FileNameDesignerCode.Text + "." + ProgrammingLanguageTypeDesignerCode.Text);
        }

        private void shutDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Shut down NATO OS-7?", "System Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (DialogResult == DialogResult.Yes)
            {
                Application.Exit();

            }
            else if (DialogResult == DialogResult.No)
            {
                //Aborted
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This file is in use by RightClickMenu-FileRestartToolStripMenuItem,\nPlease exit all instances and try again.", "File in use", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Log out of system?", "System Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (DialogResult == DialogResult.Yes)
            {

                Form1 form1 = new Form1(); // Create an instance of Form2
                form1.Show(); // Show Form2

                this.Hide(); // Hide Form1 (you can also use this.Close() if you want to close it completely)

            }
            else if (DialogResult == DialogResult.No)
            {
                //Aborted
            }
        }

        private void moreAppsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moreappsboxstore.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shopapp.Hide();
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shopapp.BackColor = SystemColors.ButtonFace;
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shopapp.BackColor = Color.Gray;

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Send to this user?", "Nato Authentic Contacts", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (DialogResult == DialogResult.Yes)
            {

                ConsoleAndShareBoxDesignerCode.Hide();

            }
            else if (DialogResult == DialogResult.No)
            {
                //Aborted
            }
        }

        private void XNATODevelopersAppBtn_Click(object sender, EventArgs e)
        {
            NATODevelopersApp.Hide();
        }

        private void NatoDevelopersSearchBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void SearchResultsForTextNatoDevelopersInfoButtonSearch_Click(object sender, EventArgs e)
        {
            if (NatoDevelopersSearchBar.Text == "")
            {
                MessageBox.Show("Please enter a search term", "Nato Developers", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SearchResultsForTextNatoDevelopersTXT.Show();
                TextNatoDevelopersInfo.Show();
                TextNatoDevelopersInfo.Text = NatoDevelopersSearchBar.Text;
            }

        }

        private void csbuttonnatofordevelopers_Click(object sender, EventArgs e)
        {
            SharedItemsNatoForDevelopers.Show();
            MainPageNatoForDevelopers.Show();
            //Mine
            HEADERCSNATOFORDEV.Show();
            String1CSNATOFORDEVELOPERS.Show();
            String2CSNATOFORDEV.Show();
            string2imgCSNATOFORDEV.Show();
            uppercaseletterCSNATOFORDEV.Show();
            upperdescCSNATOFORDEV.Show();
            lowerdescCSNATOFORDEV.Show();
            lowercaseletterCSNATOFORDEV.Show();
            label190.Show();
            noticeupperlowerCSNATOFORDEV.Show();
            //Others

            label189.Hide();
            guideNATODESIGNERSERVERSCRIPT.Hide();
            guidetxtNATODESIGNERSERVERSCRIPT.Hide();
            DESCRIPTIONNATODESIGNERSERVERSCRIPT.Hide();
            headerNATODESIGNERSERVERSCRIPT.Hide();
            favimgNATODESIGNERSERVERSCRIPT.Hide();
            HeaderNATO67.Hide();
            ContextMenuStripNATO67ForDevelopers.Hide();
            appslistNATO67ForDevelopers.Hide();
            taskmgrNATO67ForDevelopers.Hide();
            settingsNATO67ForDevelopers.Hide();
            DescriptionNato67NatoForDevelopers.Hide();
            NATO67ImageBox.Hide();
            TutorialsLabelMailNatoDevelopers.Hide();
            IntegrateTextBoxMailNatoForDevelopers.Hide();
            NatoForDevelopersMailTXTHeader.Hide();
            NATOMailTutorialNatoDevelopers.Hide();
            TextBoxMailNatoForDevelopers.Hide();
            DownloadNatoDesignerBtnNatoForDevelopers1.Hide();
            Label2NatoForDevelopersNatoDesigner.Hide();
            ChatGPTDescriptionNatoForDevelopers.Hide();
            ChatGPTInfoTXTNatoForDevelopers.Hide();
            ChatGPTLabelNatoForDevelopers.Hide();
            ChatGPTMainImageNatoForDevelopers.Hide();
            ExplainLikeIamfiveyearsoldChatGPTNatoForDevelopers.Hide();
            GenerateMeSampleHTMLCodeChatGPTNatoForDevelopers.Hide();
            generateimagesChatGPTNatoForDevelopers.Hide();
            HeaderChatGptTutorialNatoDevelopers.Hide();
            GetStartedChatGPTNatoForDevelopers.Hide();
            NatoForDevelopersDesignerBox1.Hide();
            NatoForDevelopersDesignerBox2.Hide();
            NatoForDevelopersDesignerBox3.Hide();
            NatoForDevelopersDesignerBox4.Hide();
            NatoForDevelopersDesignerBox5.Hide();
            NatoForDevelopersDesignerBox6.Hide();
            LabelText1NatoForDevelopersNatoDesigner.Hide();
            TXT2ServerscriptNatoForDevelopersNatoDesignerPage.Hide();
            NatoForDevelopersNatoDesignerImage.Hide();
            NatoWebTutorialHeaderNatoForDevelopers.Hide();
            ButtonClickDescriptionVBNETNATOFORDEVELOPERS.Hide();
            buttonclickVBNETNATOFORDEVELOPERS.Hide();
            datetimeVBNETNATOFORDEVELOPERS.Hide();
            debugVBNETNATOFORDEVELOPERS.Hide();
            formatform1VBNETNATOFORDEVELOPERS.Hide();
            formatimagesVBNETNATOFORDEVELOPERS.Hide();
            getstartedVBNETNATOFORDEVELOPERS.Hide();
            groupboxesVBNETNATOFORDEVELOPERS.Hide();
            HEADERVBNETNATOFORDEVELOPERS.Hide();
            progressbarVBNETNATOFORDEVELOPERS.Hide();
            VBNETBOXIMGNATOFORDEVELOPERS.Hide();
            VBNETDESCRIPTIONNATOFORDEVELOPERS.Hide();
            VBNETLOGONATOFORDEVELOPERS.Hide();
            headerHTMLNATOFORDEVELOPERS.Hide();

            VBNETClickabuttontogetstartedNATOFORDEVELOPERS.Hide();
            writetextButtonWebTutorialNatoForDevelopers.Hide();
            buildgraphButtonWebTutorialNatoForDevelopers.Hide();
            displayimageButtonWebTutorialNatoForDevelopers.Hide();
            changebackgroundcolorButtonWebTutorialNatoForDevelopers.Hide();
            mathButtonWebTutorialNatoForDevelopers.Hide();
            styleButtonWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Hide();
            RecommendedStartingPointNatoForDevelopersNatoWebTutorialTXT.Hide();
            ServerscriptCodeForNatoDevelopersNatoDesigner.Hide();
            ThisCodeSupportscsTXTNatoForDevelopers.Hide();
            aAttributeHTMLNATOFORDEVELOPERS.Hide();
            basicHTMLNATOFORDEVELOPERS.Hide();
            jslinkHTMLNATOFORDEVELOPERS.Hide();
            csslinkHTMLNATOFORDEVELOPERS.Hide();
            iframeHTMLNATOFORDEVELOPERS.Hide();
            buttonHTMLNATOFORDEVELOPERS.Hide();
            imgHTMLNATOFORDEVELOPERS.Hide();
            compilerdataHTMLNATOFORDEVELOPERS.Hide();
            compilerHTMLNATOFORDEVELOPERS.Hide();
            formatsHTMLNATOFORDEVELOPERS.Hide();
            compilecodeHTMLNATOFORDEVELOPERS.Hide();
            descriptionHTMLNATOFORDEVELOPERS.Hide();
        }

        private void htmlbuttonfordevelopers_Click(object sender, EventArgs e)
        {
            SharedItemsNatoForDevelopers.Show();
            MainPageNatoForDevelopers.Show();
            compilerHTMLNATOFORDEVELOPERS.DocumentText = compilerdataHTMLNATOFORDEVELOPERS.Text;
            //Mine
            aAttributeHTMLNATOFORDEVELOPERS.Show();
            basicHTMLNATOFORDEVELOPERS.Show();
            jslinkHTMLNATOFORDEVELOPERS.Show();
            csslinkHTMLNATOFORDEVELOPERS.Show();
            iframeHTMLNATOFORDEVELOPERS.Show();
            buttonHTMLNATOFORDEVELOPERS.Show();
            imgHTMLNATOFORDEVELOPERS.Show();
            compilerdataHTMLNATOFORDEVELOPERS.Show();
            compilerHTMLNATOFORDEVELOPERS.Show();
            formatsHTMLNATOFORDEVELOPERS.Show();
            compilecodeHTMLNATOFORDEVELOPERS.Show();
            descriptionHTMLNATOFORDEVELOPERS.Show();
            headerHTMLNATOFORDEVELOPERS.Show();

            //Others
            HEADERCSNATOFORDEV.Hide();
            String1CSNATOFORDEVELOPERS.Hide();
            String2CSNATOFORDEV.Hide();
            string2imgCSNATOFORDEV.Hide();
            uppercaseletterCSNATOFORDEV.Hide();
            upperdescCSNATOFORDEV.Hide();
            lowerdescCSNATOFORDEV.Hide();
            lowercaseletterCSNATOFORDEV.Hide();
            label190.Hide();
            noticeupperlowerCSNATOFORDEV.Hide();
            label189.Hide();
            guideNATODESIGNERSERVERSCRIPT.Hide();
            guidetxtNATODESIGNERSERVERSCRIPT.Hide();
            DESCRIPTIONNATODESIGNERSERVERSCRIPT.Hide();
            headerNATODESIGNERSERVERSCRIPT.Hide();
            favimgNATODESIGNERSERVERSCRIPT.Hide();
            HeaderNATO67.Hide();
            ContextMenuStripNATO67ForDevelopers.Hide();
            appslistNATO67ForDevelopers.Hide();
            taskmgrNATO67ForDevelopers.Hide();
            settingsNATO67ForDevelopers.Hide();
            DescriptionNato67NatoForDevelopers.Hide();
            NATO67ImageBox.Hide();
            TutorialsLabelMailNatoDevelopers.Hide();
            VBNETClickabuttontogetstartedNATOFORDEVELOPERS.Hide();

            IntegrateTextBoxMailNatoForDevelopers.Hide();
            GetStartedChatGPTNatoForDevelopers.Hide();
            ButtonClickDescriptionVBNETNATOFORDEVELOPERS.Hide();
            buttonclickVBNETNATOFORDEVELOPERS.Hide();
            datetimeVBNETNATOFORDEVELOPERS.Hide();
            debugVBNETNATOFORDEVELOPERS.Hide();
            formatform1VBNETNATOFORDEVELOPERS.Hide();
            formatimagesVBNETNATOFORDEVELOPERS.Hide();
            getstartedVBNETNATOFORDEVELOPERS.Hide();
            groupboxesVBNETNATOFORDEVELOPERS.Hide();
            HEADERVBNETNATOFORDEVELOPERS.Hide();
            progressbarVBNETNATOFORDEVELOPERS.Hide();
            VBNETBOXIMGNATOFORDEVELOPERS.Hide();
            VBNETDESCRIPTIONNATOFORDEVELOPERS.Hide();
            VBNETLOGONATOFORDEVELOPERS.Hide();
            NatoForDevelopersMailTXTHeader.Hide();
            NATOMailTutorialNatoDevelopers.Hide();
            TextBoxMailNatoForDevelopers.Hide();
            ThisCodeSupportscsTXTNatoForDevelopers.Hide();
            DownloadNatoDesignerBtnNatoForDevelopers1.Hide();
            Label2NatoForDevelopersNatoDesigner.Hide();
            ChatGPTDescriptionNatoForDevelopers.Hide();
            ChatGPTInfoTXTNatoForDevelopers.Hide();
            ChatGPTLabelNatoForDevelopers.Hide();
            ChatGPTMainImageNatoForDevelopers.Hide();
            ExplainLikeIamfiveyearsoldChatGPTNatoForDevelopers.Hide();
            GenerateMeSampleHTMLCodeChatGPTNatoForDevelopers.Hide();
            generateimagesChatGPTNatoForDevelopers.Hide();
            HeaderChatGptTutorialNatoDevelopers.Hide();
            NatoForDevelopersDesignerBox1.Hide();
            NatoForDevelopersDesignerBox2.Hide();
            NatoForDevelopersDesignerBox3.Hide();
            NatoForDevelopersDesignerBox4.Hide();
            NatoForDevelopersDesignerBox5.Hide();
            NatoForDevelopersDesignerBox6.Hide();
            LabelText1NatoForDevelopersNatoDesigner.Hide();
            TXT2ServerscriptNatoForDevelopersNatoDesignerPage.Hide();
            NatoForDevelopersNatoDesignerImage.Hide();
            NatoWebTutorialHeaderNatoForDevelopers.Hide();
            writetextButtonWebTutorialNatoForDevelopers.Hide();
            buildgraphButtonWebTutorialNatoForDevelopers.Hide();
            displayimageButtonWebTutorialNatoForDevelopers.Hide();
            changebackgroundcolorButtonWebTutorialNatoForDevelopers.Hide();
            mathButtonWebTutorialNatoForDevelopers.Hide();
            styleButtonWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Hide();
            RecommendedStartingPointNatoForDevelopersNatoWebTutorialTXT.Hide();
            ServerscriptCodeForNatoDevelopersNatoDesigner.Hide();
        }

        private void serverscriptbuttonfordevelopers_Click(object sender, EventArgs e)
        {
            SharedItemsNatoForDevelopers.Show();
            MainPageNatoForDevelopers.Show();
            //Mine
            guideNATODESIGNERSERVERSCRIPT.Show();
            guidetxtNATODESIGNERSERVERSCRIPT.Show();
            DESCRIPTIONNATODESIGNERSERVERSCRIPT.Show();
            headerNATODESIGNERSERVERSCRIPT.Show();
            favimgNATODESIGNERSERVERSCRIPT.Show();
            //Others
            HEADERCSNATOFORDEV.Hide();
            String1CSNATOFORDEVELOPERS.Hide();
            String2CSNATOFORDEV.Hide();
            string2imgCSNATOFORDEV.Hide();
            uppercaseletterCSNATOFORDEV.Hide();
            upperdescCSNATOFORDEV.Hide();
            lowerdescCSNATOFORDEV.Hide();
            lowercaseletterCSNATOFORDEV.Hide();
            label190.Hide();
            noticeupperlowerCSNATOFORDEV.Hide();
            label189.Hide();
            aAttributeHTMLNATOFORDEVELOPERS.Hide();
            basicHTMLNATOFORDEVELOPERS.Hide();
            jslinkHTMLNATOFORDEVELOPERS.Hide();
            headerHTMLNATOFORDEVELOPERS.Hide();

            csslinkHTMLNATOFORDEVELOPERS.Hide();
            iframeHTMLNATOFORDEVELOPERS.Hide();
            buttonHTMLNATOFORDEVELOPERS.Hide();
            imgHTMLNATOFORDEVELOPERS.Hide();
            compilerdataHTMLNATOFORDEVELOPERS.Hide();
            compilerHTMLNATOFORDEVELOPERS.Hide();
            formatsHTMLNATOFORDEVELOPERS.Hide();
            compilecodeHTMLNATOFORDEVELOPERS.Hide();
            descriptionHTMLNATOFORDEVELOPERS.Hide();
            HeaderNATO67.Hide();
            ContextMenuStripNATO67ForDevelopers.Hide();
            appslistNATO67ForDevelopers.Hide();
            taskmgrNATO67ForDevelopers.Hide();
            settingsNATO67ForDevelopers.Hide();
            VBNETClickabuttontogetstartedNATOFORDEVELOPERS.Hide();

            DescriptionNato67NatoForDevelopers.Hide();
            NATO67ImageBox.Hide();
            TutorialsLabelMailNatoDevelopers.Hide();
            IntegrateTextBoxMailNatoForDevelopers.Hide();
            NatoForDevelopersMailTXTHeader.Hide();
            NATOMailTutorialNatoDevelopers.Hide();
            GetStartedChatGPTNatoForDevelopers.Hide();
            TextBoxMailNatoForDevelopers.Hide();
            DownloadNatoDesignerBtnNatoForDevelopers1.Hide();
            Label2NatoForDevelopersNatoDesigner.Hide();
            NatoForDevelopersDesignerBox1.Hide();
            NatoForDevelopersDesignerBox2.Hide();
            NatoForDevelopersDesignerBox3.Hide();
            NatoForDevelopersDesignerBox4.Hide();
            ButtonClickDescriptionVBNETNATOFORDEVELOPERS.Hide();
            buttonclickVBNETNATOFORDEVELOPERS.Hide();
            datetimeVBNETNATOFORDEVELOPERS.Hide();
            debugVBNETNATOFORDEVELOPERS.Hide();
            formatform1VBNETNATOFORDEVELOPERS.Hide();
            formatimagesVBNETNATOFORDEVELOPERS.Hide();
            getstartedVBNETNATOFORDEVELOPERS.Hide();
            groupboxesVBNETNATOFORDEVELOPERS.Hide();
            HEADERVBNETNATOFORDEVELOPERS.Hide();
            progressbarVBNETNATOFORDEVELOPERS.Hide();
            VBNETBOXIMGNATOFORDEVELOPERS.Hide();
            VBNETDESCRIPTIONNATOFORDEVELOPERS.Hide();
            VBNETLOGONATOFORDEVELOPERS.Hide();
            NatoForDevelopersDesignerBox5.Hide();
            ChatGPTDescriptionNatoForDevelopers.Hide();
            ChatGPTInfoTXTNatoForDevelopers.Hide();
            ChatGPTLabelNatoForDevelopers.Hide();
            ChatGPTMainImageNatoForDevelopers.Hide();
            ExplainLikeIamfiveyearsoldChatGPTNatoForDevelopers.Hide();
            GenerateMeSampleHTMLCodeChatGPTNatoForDevelopers.Hide();
            generateimagesChatGPTNatoForDevelopers.Hide();
            HeaderChatGptTutorialNatoDevelopers.Hide();
            NatoForDevelopersDesignerBox6.Hide();
            LabelText1NatoForDevelopersNatoDesigner.Hide();
            TXT2ServerscriptNatoForDevelopersNatoDesignerPage.Hide();
            NatoForDevelopersNatoDesignerImage.Hide();
            ServerscriptCodeForNatoDevelopersNatoDesigner.Hide();
            ThisCodeSupportscsTXTNatoForDevelopers.Hide();
            NatoWebTutorialHeaderNatoForDevelopers.Hide();
            writetextButtonWebTutorialNatoForDevelopers.Hide();
            buildgraphButtonWebTutorialNatoForDevelopers.Hide();
            displayimageButtonWebTutorialNatoForDevelopers.Hide();
            changebackgroundcolorButtonWebTutorialNatoForDevelopers.Hide();
            mathButtonWebTutorialNatoForDevelopers.Hide();
            styleButtonWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Hide();
            RecommendedStartingPointNatoForDevelopersNatoWebTutorialTXT.Hide();
        }

        private void vbdotnetbuttonfordevelopers_Click(object sender, EventArgs e)
        {
            SharedItemsNatoForDevelopers.Show();
            MainPageNatoForDevelopers.Show();
            //Mine
            ButtonClickDescriptionVBNETNATOFORDEVELOPERS.Show();
            buttonclickVBNETNATOFORDEVELOPERS.Show();
            datetimeVBNETNATOFORDEVELOPERS.Show();
            VBNETClickabuttontogetstartedNATOFORDEVELOPERS.Show();

            debugVBNETNATOFORDEVELOPERS.Show();
            formatform1VBNETNATOFORDEVELOPERS.Show();
            formatimagesVBNETNATOFORDEVELOPERS.Show();
            getstartedVBNETNATOFORDEVELOPERS.Show();
            groupboxesVBNETNATOFORDEVELOPERS.Show();
            HEADERVBNETNATOFORDEVELOPERS.Show();
            progressbarVBNETNATOFORDEVELOPERS.Show();
            VBNETBOXIMGNATOFORDEVELOPERS.Show();
            VBNETDESCRIPTIONNATOFORDEVELOPERS.Show();
            VBNETLOGONATOFORDEVELOPERS.Show();
            //Others
            label189.Hide();
            HEADERCSNATOFORDEV.Hide();
            String1CSNATOFORDEVELOPERS.Hide();
            String2CSNATOFORDEV.Hide();
            string2imgCSNATOFORDEV.Hide();
            uppercaseletterCSNATOFORDEV.Hide();
            upperdescCSNATOFORDEV.Hide();
            lowerdescCSNATOFORDEV.Hide();
            lowercaseletterCSNATOFORDEV.Hide();
            label190.Hide();
            noticeupperlowerCSNATOFORDEV.Hide();
            guideNATODESIGNERSERVERSCRIPT.Hide();
            guidetxtNATODESIGNERSERVERSCRIPT.Hide();
            DESCRIPTIONNATODESIGNERSERVERSCRIPT.Hide();
            headerNATODESIGNERSERVERSCRIPT.Hide();
            favimgNATODESIGNERSERVERSCRIPT.Hide();
            HeaderNATO67.Hide();
            ContextMenuStripNATO67ForDevelopers.Hide();
            appslistNATO67ForDevelopers.Hide();
            taskmgrNATO67ForDevelopers.Hide();
            settingsNATO67ForDevelopers.Hide();
            DescriptionNato67NatoForDevelopers.Hide();
            NATO67ImageBox.Hide();
            TutorialsLabelMailNatoDevelopers.Hide();
            IntegrateTextBoxMailNatoForDevelopers.Hide();
            NatoForDevelopersMailTXTHeader.Hide();
            NATOMailTutorialNatoDevelopers.Hide();
            TextBoxMailNatoForDevelopers.Hide();
            DownloadNatoDesignerBtnNatoForDevelopers1.Hide();
            Label2NatoForDevelopersNatoDesigner.Hide();
            NatoForDevelopersDesignerBox1.Hide();
            NatoForDevelopersDesignerBox2.Hide();
            NatoForDevelopersDesignerBox3.Hide();
            NatoForDevelopersDesignerBox4.Hide();
            NatoForDevelopersDesignerBox5.Hide();
            NatoForDevelopersDesignerBox6.Hide();
            LabelText1NatoForDevelopersNatoDesigner.Hide();
            TXT2ServerscriptNatoForDevelopersNatoDesignerPage.Hide();
            headerHTMLNATOFORDEVELOPERS.Hide();

            NatoForDevelopersNatoDesignerImage.Hide();
            ServerscriptCodeForNatoDevelopersNatoDesigner.Hide();
            ThisCodeSupportscsTXTNatoForDevelopers.Hide();
            NatoWebTutorialHeaderNatoForDevelopers.Hide();
            writetextButtonWebTutorialNatoForDevelopers.Hide();
            buildgraphButtonWebTutorialNatoForDevelopers.Hide();
            displayimageButtonWebTutorialNatoForDevelopers.Hide();
            changebackgroundcolorButtonWebTutorialNatoForDevelopers.Hide();
            mathButtonWebTutorialNatoForDevelopers.Hide();
            styleButtonWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopers.Hide();
            ChatGPTDescriptionNatoForDevelopers.Hide();
            ChatGPTInfoTXTNatoForDevelopers.Hide();
            ChatGPTLabelNatoForDevelopers.Hide();
            ChatGPTMainImageNatoForDevelopers.Hide();
            ExplainLikeIamfiveyearsoldChatGPTNatoForDevelopers.Hide();
            GenerateMeSampleHTMLCodeChatGPTNatoForDevelopers.Hide();
            generateimagesChatGPTNatoForDevelopers.Hide();
            HeaderChatGptTutorialNatoDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Hide();
            RecommendedStartingPointNatoForDevelopersNatoWebTutorialTXT.Hide();
            GetStartedChatGPTNatoForDevelopers.Hide();
            aAttributeHTMLNATOFORDEVELOPERS.Hide();
            basicHTMLNATOFORDEVELOPERS.Hide();
            jslinkHTMLNATOFORDEVELOPERS.Hide();
            csslinkHTMLNATOFORDEVELOPERS.Hide();
            iframeHTMLNATOFORDEVELOPERS.Hide();
            buttonHTMLNATOFORDEVELOPERS.Hide();
            HEADERCSNATOFORDEV.Hide();
            String1CSNATOFORDEVELOPERS.Hide();
            String2CSNATOFORDEV.Hide();
            string2imgCSNATOFORDEV.Hide();
            uppercaseletterCSNATOFORDEV.Hide();
            upperdescCSNATOFORDEV.Hide();
            lowerdescCSNATOFORDEV.Hide();
            lowercaseletterCSNATOFORDEV.Hide();
            label190.Hide();
            noticeupperlowerCSNATOFORDEV.Hide();
            imgHTMLNATOFORDEVELOPERS.Hide();
            compilerdataHTMLNATOFORDEVELOPERS.Hide();
            compilerHTMLNATOFORDEVELOPERS.Hide();
            formatsHTMLNATOFORDEVELOPERS.Hide();
            compilecodeHTMLNATOFORDEVELOPERS.Hide();
            descriptionHTMLNATOFORDEVELOPERS.Hide();
        }

        private void natosixandsevenfordevelopers_Click(object sender, EventArgs e)
        {
            //Mine
            label189.Show();

            HeaderNATO67.Show();
            ContextMenuStripNATO67ForDevelopers.Show();
            appslistNATO67ForDevelopers.Show();
            taskmgrNATO67ForDevelopers.Show();
            settingsNATO67ForDevelopers.Show();
            DescriptionNato67NatoForDevelopers.Show();
            NATO67ImageBox.Show();

            MainPageNatoForDevelopers.Show();
            SharedItemsNatoForDevelopers.Show();
            //Others
            TutorialsLabelMailNatoDevelopers.Hide();
            IntegrateTextBoxMailNatoForDevelopers.Hide();
            ButtonClickDescriptionVBNETNATOFORDEVELOPERS.Hide();
            VBNETClickabuttontogetstartedNATOFORDEVELOPERS.Hide();
            guideNATODESIGNERSERVERSCRIPT.Hide();
            guidetxtNATODESIGNERSERVERSCRIPT.Hide();
            DESCRIPTIONNATODESIGNERSERVERSCRIPT.Hide();
            headerNATODESIGNERSERVERSCRIPT.Hide();
            HEADERCSNATOFORDEV.Hide();
            String1CSNATOFORDEVELOPERS.Hide();
            String2CSNATOFORDEV.Hide();
            string2imgCSNATOFORDEV.Hide();
            uppercaseletterCSNATOFORDEV.Hide();
            upperdescCSNATOFORDEV.Hide();
            lowerdescCSNATOFORDEV.Hide();
            lowercaseletterCSNATOFORDEV.Hide();
            label190.Hide();
            noticeupperlowerCSNATOFORDEV.Hide();
            favimgNATODESIGNERSERVERSCRIPT.Hide();
            buttonclickVBNETNATOFORDEVELOPERS.Hide();
            datetimeVBNETNATOFORDEVELOPERS.Hide();
            debugVBNETNATOFORDEVELOPERS.Hide();
            formatform1VBNETNATOFORDEVELOPERS.Hide();
            formatimagesVBNETNATOFORDEVELOPERS.Hide();
            getstartedVBNETNATOFORDEVELOPERS.Hide();
            groupboxesVBNETNATOFORDEVELOPERS.Hide();
            HEADERVBNETNATOFORDEVELOPERS.Hide();
            progressbarVBNETNATOFORDEVELOPERS.Hide();
            VBNETBOXIMGNATOFORDEVELOPERS.Hide();
            VBNETDESCRIPTIONNATOFORDEVELOPERS.Hide();
            VBNETLOGONATOFORDEVELOPERS.Hide();
            NatoForDevelopersMailTXTHeader.Hide();
            NATOMailTutorialNatoDevelopers.Hide();
            TextBoxMailNatoForDevelopers.Hide();
            ThisCodeSupportscsTXTNatoForDevelopers.Hide();
            DownloadNatoDesignerBtnNatoForDevelopers1.Hide();
            Label2NatoForDevelopersNatoDesigner.Hide();
            NatoForDevelopersDesignerBox1.Hide();
            NatoForDevelopersDesignerBox2.Hide();
            NatoForDevelopersDesignerBox3.Hide();
            NatoForDevelopersDesignerBox4.Hide();
            NatoForDevelopersDesignerBox5.Hide();
            NatoForDevelopersDesignerBox6.Hide();
            LabelText1NatoForDevelopersNatoDesigner.Hide();
            TXT2ServerscriptNatoForDevelopersNatoDesignerPage.Hide();
            NatoForDevelopersNatoDesignerImage.Hide();
            ServerscriptCodeForNatoDevelopersNatoDesigner.Hide();
            NatoWebTutorialHeaderNatoForDevelopers.Hide();
            writetextButtonWebTutorialNatoForDevelopers.Hide();
            buildgraphButtonWebTutorialNatoForDevelopers.Hide();
            displayimageButtonWebTutorialNatoForDevelopers.Hide();
            changebackgroundcolorButtonWebTutorialNatoForDevelopers.Hide();
            mathButtonWebTutorialNatoForDevelopers.Hide();
            styleButtonWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopers.Hide();
            headerHTMLNATOFORDEVELOPERS.Hide();

            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Hide();
            RecommendedStartingPointNatoForDevelopersNatoWebTutorialTXT.Hide();
            ChatGPTDescriptionNatoForDevelopers.Hide();
            ChatGPTInfoTXTNatoForDevelopers.Hide();
            ChatGPTLabelNatoForDevelopers.Hide();
            ChatGPTMainImageNatoForDevelopers.Hide();
            ExplainLikeIamfiveyearsoldChatGPTNatoForDevelopers.Hide();
            GenerateMeSampleHTMLCodeChatGPTNatoForDevelopers.Hide();
            generateimagesChatGPTNatoForDevelopers.Hide();
            HeaderChatGptTutorialNatoDevelopers.Hide();
            GetStartedChatGPTNatoForDevelopers.Hide();
            aAttributeHTMLNATOFORDEVELOPERS.Hide();
            basicHTMLNATOFORDEVELOPERS.Hide();
            jslinkHTMLNATOFORDEVELOPERS.Hide();
            csslinkHTMLNATOFORDEVELOPERS.Hide();
            iframeHTMLNATOFORDEVELOPERS.Hide();
            buttonHTMLNATOFORDEVELOPERS.Hide();
            imgHTMLNATOFORDEVELOPERS.Hide();
            compilerdataHTMLNATOFORDEVELOPERS.Hide();
            compilerHTMLNATOFORDEVELOPERS.Hide();
            formatsHTMLNATOFORDEVELOPERS.Hide();
            compilecodeHTMLNATOFORDEVELOPERS.Hide();
            descriptionHTMLNATOFORDEVELOPERS.Hide();
        }

        private void chatgptfordevelopers_Click(object sender, EventArgs e)
        {
            SharedItemsNatoForDevelopers.Show();
            MainPageNatoForDevelopers.Show();
            //Others
            label189.Hide();
            guideNATODESIGNERSERVERSCRIPT.Hide();
            guidetxtNATODESIGNERSERVERSCRIPT.Hide();
            DESCRIPTIONNATODESIGNERSERVERSCRIPT.Hide();
            headerNATODESIGNERSERVERSCRIPT.Hide();
            HEADERCSNATOFORDEV.Hide();
            String1CSNATOFORDEVELOPERS.Hide();
            String2CSNATOFORDEV.Hide();
            string2imgCSNATOFORDEV.Hide();
            uppercaseletterCSNATOFORDEV.Hide();
            upperdescCSNATOFORDEV.Hide();
            lowerdescCSNATOFORDEV.Hide();
            lowercaseletterCSNATOFORDEV.Hide();
            label190.Hide();
            noticeupperlowerCSNATOFORDEV.Hide();
            favimgNATODESIGNERSERVERSCRIPT.Hide();
            HeaderNATO67.Hide();
            ContextMenuStripNATO67ForDevelopers.Hide();
            appslistNATO67ForDevelopers.Hide();
            taskmgrNATO67ForDevelopers.Hide();
            settingsNATO67ForDevelopers.Hide();
            DescriptionNato67NatoForDevelopers.Hide();
            NATO67ImageBox.Hide();
            TutorialsLabelMailNatoDevelopers.Hide();
            IntegrateTextBoxMailNatoForDevelopers.Hide();
            NatoForDevelopersMailTXTHeader.Hide();
            NATOMailTutorialNatoDevelopers.Hide();
            ButtonClickDescriptionVBNETNATOFORDEVELOPERS.Hide();
            buttonclickVBNETNATOFORDEVELOPERS.Hide();
            datetimeVBNETNATOFORDEVELOPERS.Hide();
            debugVBNETNATOFORDEVELOPERS.Hide();
            formatform1VBNETNATOFORDEVELOPERS.Hide();
            formatimagesVBNETNATOFORDEVELOPERS.Hide();
            VBNETClickabuttontogetstartedNATOFORDEVELOPERS.Hide();
            aAttributeHTMLNATOFORDEVELOPERS.Hide();
            basicHTMLNATOFORDEVELOPERS.Hide();
            jslinkHTMLNATOFORDEVELOPERS.Hide();
            csslinkHTMLNATOFORDEVELOPERS.Hide();
            iframeHTMLNATOFORDEVELOPERS.Hide();
            buttonHTMLNATOFORDEVELOPERS.Hide();
            imgHTMLNATOFORDEVELOPERS.Hide();
            compilerdataHTMLNATOFORDEVELOPERS.Hide();
            compilerHTMLNATOFORDEVELOPERS.Hide();
            formatsHTMLNATOFORDEVELOPERS.Hide();
            compilecodeHTMLNATOFORDEVELOPERS.Hide();
            descriptionHTMLNATOFORDEVELOPERS.Hide();
            getstartedVBNETNATOFORDEVELOPERS.Hide();
            groupboxesVBNETNATOFORDEVELOPERS.Hide();
            HEADERVBNETNATOFORDEVELOPERS.Hide();
            progressbarVBNETNATOFORDEVELOPERS.Hide();
            VBNETBOXIMGNATOFORDEVELOPERS.Hide();
            VBNETDESCRIPTIONNATOFORDEVELOPERS.Hide();
            VBNETLOGONATOFORDEVELOPERS.Hide();
            TextBoxMailNatoForDevelopers.Hide();
            ThisCodeSupportscsTXTNatoForDevelopers.Hide();
            DownloadNatoDesignerBtnNatoForDevelopers1.Hide();
            Label2NatoForDevelopersNatoDesigner.Hide();
            NatoForDevelopersDesignerBox1.Hide();
            NatoForDevelopersDesignerBox2.Hide();
            NatoForDevelopersDesignerBox3.Hide();
            NatoForDevelopersDesignerBox4.Hide();
            NatoForDevelopersDesignerBox5.Hide();
            NatoForDevelopersDesignerBox6.Hide();
            LabelText1NatoForDevelopersNatoDesigner.Hide();
            TXT2ServerscriptNatoForDevelopersNatoDesignerPage.Hide();
            NatoForDevelopersNatoDesignerImage.Hide();
            ServerscriptCodeForNatoDevelopersNatoDesigner.Hide();
            NatoWebTutorialHeaderNatoForDevelopers.Hide();
            writetextButtonWebTutorialNatoForDevelopers.Hide();
            headerHTMLNATOFORDEVELOPERS.Hide();

            buildgraphButtonWebTutorialNatoForDevelopers.Hide();
            displayimageButtonWebTutorialNatoForDevelopers.Hide();
            changebackgroundcolorButtonWebTutorialNatoForDevelopers.Hide();
            mathButtonWebTutorialNatoForDevelopers.Hide();
            styleButtonWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Hide();
            RecommendedStartingPointNatoForDevelopersNatoWebTutorialTXT.Hide();
            //Mine
            GetStartedChatGPTNatoForDevelopers.Show();

            ChatGPTDescriptionNatoForDevelopers.Show();
            ChatGPTInfoTXTNatoForDevelopers.Show();
            ChatGPTLabelNatoForDevelopers.Show();
            ChatGPTMainImageNatoForDevelopers.Show();
            ExplainLikeIamfiveyearsoldChatGPTNatoForDevelopers.Show();
            GenerateMeSampleHTMLCodeChatGPTNatoForDevelopers.Show();
            generateimagesChatGPTNatoForDevelopers.Show();
            HeaderChatGptTutorialNatoDevelopers.Show();

        }

        private void webbuttonfordevelopers_Click(object sender, EventArgs e)
        {
            MainPageNatoForDevelopers.Show();
            SharedItemsNatoForDevelopers.Show();
            //Others
            label189.Hide();
            guideNATODESIGNERSERVERSCRIPT.Hide();
            guidetxtNATODESIGNERSERVERSCRIPT.Hide();
            DESCRIPTIONNATODESIGNERSERVERSCRIPT.Hide();
            headerNATODESIGNERSERVERSCRIPT.Hide();
            favimgNATODESIGNERSERVERSCRIPT.Hide();
            HeaderNATO67.Hide();
            ContextMenuStripNATO67ForDevelopers.Hide();
            appslistNATO67ForDevelopers.Hide();
            taskmgrNATO67ForDevelopers.Hide();
            settingsNATO67ForDevelopers.Hide();
            DescriptionNato67NatoForDevelopers.Hide();
            NATO67ImageBox.Hide();
            GetStartedChatGPTNatoForDevelopers.Hide();
            aAttributeHTMLNATOFORDEVELOPERS.Hide();
            basicHTMLNATOFORDEVELOPERS.Hide();
            HEADERCSNATOFORDEV.Hide();
            String1CSNATOFORDEVELOPERS.Hide();
            String2CSNATOFORDEV.Hide();
            string2imgCSNATOFORDEV.Hide();
            uppercaseletterCSNATOFORDEV.Hide();
            upperdescCSNATOFORDEV.Hide();
            lowerdescCSNATOFORDEV.Hide();
            lowercaseletterCSNATOFORDEV.Hide();
            label190.Hide();
            noticeupperlowerCSNATOFORDEV.Hide();
            jslinkHTMLNATOFORDEVELOPERS.Hide();
            csslinkHTMLNATOFORDEVELOPERS.Hide();
            iframeHTMLNATOFORDEVELOPERS.Hide();
            buttonHTMLNATOFORDEVELOPERS.Hide();
            imgHTMLNATOFORDEVELOPERS.Hide();
            compilerdataHTMLNATOFORDEVELOPERS.Hide();
            compilerHTMLNATOFORDEVELOPERS.Hide();
            formatsHTMLNATOFORDEVELOPERS.Hide();
            compilecodeHTMLNATOFORDEVELOPERS.Hide();
            descriptionHTMLNATOFORDEVELOPERS.Hide();
            ChatGPTDescriptionNatoForDevelopers.Hide();
            ChatGPTInfoTXTNatoForDevelopers.Hide();
            ChatGPTLabelNatoForDevelopers.Hide();
            VBNETClickabuttontogetstartedNATOFORDEVELOPERS.Hide();

            ChatGPTMainImageNatoForDevelopers.Hide();
            ExplainLikeIamfiveyearsoldChatGPTNatoForDevelopers.Hide();
            GenerateMeSampleHTMLCodeChatGPTNatoForDevelopers.Hide();
            generateimagesChatGPTNatoForDevelopers.Hide();
            HeaderChatGptTutorialNatoDevelopers.Hide();
            TutorialsLabelMailNatoDevelopers.Hide();
            IntegrateTextBoxMailNatoForDevelopers.Hide();
            NatoForDevelopersMailTXTHeader.Hide();
            NATOMailTutorialNatoDevelopers.Hide();
            TextBoxMailNatoForDevelopers.Hide();
            ThisCodeSupportscsTXTNatoForDevelopers.Hide();
            DownloadNatoDesignerBtnNatoForDevelopers1.Hide();
            Label2NatoForDevelopersNatoDesigner.Hide();
            NatoForDevelopersDesignerBox1.Hide();
            NatoForDevelopersDesignerBox2.Hide();
            NatoForDevelopersDesignerBox3.Hide();
            NatoForDevelopersDesignerBox4.Hide();
            NatoForDevelopersDesignerBox5.Hide();
            headerHTMLNATOFORDEVELOPERS.Hide();

            NatoForDevelopersDesignerBox6.Hide();
            LabelText1NatoForDevelopersNatoDesigner.Hide();
            TXT2ServerscriptNatoForDevelopersNatoDesignerPage.Hide();
            NatoForDevelopersNatoDesignerImage.Hide();
            ServerscriptCodeForNatoDevelopersNatoDesigner.Hide();
            NatoWebTutorialHeaderNatoForDevelopers.Hide();
            writetextButtonWebTutorialNatoForDevelopers.Hide();
            buildgraphButtonWebTutorialNatoForDevelopers.Hide();
            displayimageButtonWebTutorialNatoForDevelopers.Hide();
            changebackgroundcolorButtonWebTutorialNatoForDevelopers.Hide();
            mathButtonWebTutorialNatoForDevelopers.Hide();
            styleButtonWebTutorialNatoForDevelopers.Hide();
            ButtonClickDescriptionVBNETNATOFORDEVELOPERS.Hide();
            buttonclickVBNETNATOFORDEVELOPERS.Hide();
            datetimeVBNETNATOFORDEVELOPERS.Hide();
            debugVBNETNATOFORDEVELOPERS.Hide();
            formatform1VBNETNATOFORDEVELOPERS.Hide();
            formatimagesVBNETNATOFORDEVELOPERS.Hide();
            getstartedVBNETNATOFORDEVELOPERS.Hide();
            groupboxesVBNETNATOFORDEVELOPERS.Hide();
            HEADERVBNETNATOFORDEVELOPERS.Hide();
            progressbarVBNETNATOFORDEVELOPERS.Hide();
            VBNETBOXIMGNATOFORDEVELOPERS.Hide();
            VBNETDESCRIPTIONNATOFORDEVELOPERS.Hide();
            VBNETLOGONATOFORDEVELOPERS.Hide();
            ImageTopicsWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Hide();
            RecommendedStartingPointNatoForDevelopersNatoWebTutorialTXT.Hide();
            //Mine
            NatoWebTutorialHeaderNatoForDevelopers.Show();
            writetextButtonWebTutorialNatoForDevelopers.Show();
            buildgraphButtonWebTutorialNatoForDevelopers.Show();
            displayimageButtonWebTutorialNatoForDevelopers.Show();
            changebackgroundcolorButtonWebTutorialNatoForDevelopers.Show();
            mathButtonWebTutorialNatoForDevelopers.Show();
            styleButtonWebTutorialNatoForDevelopers.Show();
            ImageTopicsWebTutorialNatoForDevelopers.Show();
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Show();
            RecommendedStartingPointNatoForDevelopersNatoWebTutorialTXT.Show();
        }

        private void LearnMoreBtnNatoForDevelopersMail_Click(object sender, EventArgs e)
        {
            SharedItemsNatoForDevelopers.Show();
            MainPageNatoForDevelopers.Show();
            //Others
            label189.Hide();
            guideNATODESIGNERSERVERSCRIPT.Hide();
            guidetxtNATODESIGNERSERVERSCRIPT.Hide();
            DESCRIPTIONNATODESIGNERSERVERSCRIPT.Hide();
            headerNATODESIGNERSERVERSCRIPT.Hide();
            favimgNATODESIGNERSERVERSCRIPT.Hide();
            HeaderNATO67.Hide();
            ContextMenuStripNATO67ForDevelopers.Hide();
            appslistNATO67ForDevelopers.Hide();
            taskmgrNATO67ForDevelopers.Hide();
            settingsNATO67ForDevelopers.Hide();
            DescriptionNato67NatoForDevelopers.Hide();
            NATO67ImageBox.Hide();
            HEADERCSNATOFORDEV.Hide();
            String1CSNATOFORDEVELOPERS.Hide();
            String2CSNATOFORDEV.Hide();
            string2imgCSNATOFORDEV.Hide();
            uppercaseletterCSNATOFORDEV.Hide();
            upperdescCSNATOFORDEV.Hide();
            lowerdescCSNATOFORDEV.Hide();
            lowercaseletterCSNATOFORDEV.Hide();
            label190.Hide();
            noticeupperlowerCSNATOFORDEV.Hide();
            GetStartedChatGPTNatoForDevelopers.Hide();
            aAttributeHTMLNATOFORDEVELOPERS.Hide();
            basicHTMLNATOFORDEVELOPERS.Hide();
            jslinkHTMLNATOFORDEVELOPERS.Hide();
            csslinkHTMLNATOFORDEVELOPERS.Hide();
            iframeHTMLNATOFORDEVELOPERS.Hide();
            buttonHTMLNATOFORDEVELOPERS.Hide();
            imgHTMLNATOFORDEVELOPERS.Hide();
            compilerdataHTMLNATOFORDEVELOPERS.Hide();
            compilerHTMLNATOFORDEVELOPERS.Hide();
            formatsHTMLNATOFORDEVELOPERS.Hide();
            compilecodeHTMLNATOFORDEVELOPERS.Hide();
            descriptionHTMLNATOFORDEVELOPERS.Hide();
            DownloadNatoDesignerBtnNatoForDevelopers1.Hide();
            Label2NatoForDevelopersNatoDesigner.Hide();
            NatoForDevelopersDesignerBox1.Hide();
            NatoForDevelopersDesignerBox2.Hide();
            VBNETClickabuttontogetstartedNATOFORDEVELOPERS.Hide();

            NatoForDevelopersDesignerBox3.Hide();
            NatoForDevelopersDesignerBox4.Hide();
            NatoForDevelopersDesignerBox5.Hide();
            NatoForDevelopersDesignerBox6.Hide();
            LabelText1NatoForDevelopersNatoDesigner.Hide();
            TXT2ServerscriptNatoForDevelopersNatoDesignerPage.Hide();
            NatoForDevelopersNatoDesignerImage.Hide();
            ServerscriptCodeForNatoDevelopersNatoDesigner.Hide();
            ChatGPTDescriptionNatoForDevelopers.Hide();
            ChatGPTInfoTXTNatoForDevelopers.Hide();
            ChatGPTLabelNatoForDevelopers.Hide();
            ChatGPTMainImageNatoForDevelopers.Hide();
            ExplainLikeIamfiveyearsoldChatGPTNatoForDevelopers.Hide();
            GenerateMeSampleHTMLCodeChatGPTNatoForDevelopers.Hide();
            generateimagesChatGPTNatoForDevelopers.Hide();
            HeaderChatGptTutorialNatoDevelopers.Hide();
            NatoWebTutorialHeaderNatoForDevelopers.Hide();
            writetextButtonWebTutorialNatoForDevelopers.Hide();
            buildgraphButtonWebTutorialNatoForDevelopers.Hide();
            displayimageButtonWebTutorialNatoForDevelopers.Hide();
            changebackgroundcolorButtonWebTutorialNatoForDevelopers.Hide();
            mathButtonWebTutorialNatoForDevelopers.Hide();
            styleButtonWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Hide();
            RecommendedStartingPointNatoForDevelopersNatoWebTutorialTXT.Hide();
            ButtonClickDescriptionVBNETNATOFORDEVELOPERS.Hide();
            buttonclickVBNETNATOFORDEVELOPERS.Hide();
            datetimeVBNETNATOFORDEVELOPERS.Hide();
            debugVBNETNATOFORDEVELOPERS.Hide();
            formatform1VBNETNATOFORDEVELOPERS.Hide();
            formatimagesVBNETNATOFORDEVELOPERS.Hide();
            getstartedVBNETNATOFORDEVELOPERS.Hide();
            groupboxesVBNETNATOFORDEVELOPERS.Hide();
            HEADERVBNETNATOFORDEVELOPERS.Hide();
            progressbarVBNETNATOFORDEVELOPERS.Hide();
            VBNETBOXIMGNATOFORDEVELOPERS.Hide();
            VBNETDESCRIPTIONNATOFORDEVELOPERS.Hide();
            VBNETLOGONATOFORDEVELOPERS.Hide();
            headerHTMLNATOFORDEVELOPERS.Hide();

            //Paste
            TutorialsLabelMailNatoDevelopers.Show();
            IntegrateTextBoxMailNatoForDevelopers.Show();
            NatoForDevelopersMailTXTHeader.Show();
            NATOMailTutorialNatoDevelopers.Show();
            TextBoxMailNatoForDevelopers.Show();
            ThisCodeSupportscsTXTNatoForDevelopers.Show();
            NatoForDevelopersMailTXTHeader.Text = "Mail";
        }

        private void LearnMoreBtnNatoForDevelopersNatoDesigner_Click(object sender, EventArgs e)
        {
            SharedItemsNatoForDevelopers.Show();
            MainPageNatoForDevelopers.Show();
            //Others
            label189.Hide();
            guideNATODESIGNERSERVERSCRIPT.Hide();
            guidetxtNATODESIGNERSERVERSCRIPT.Hide();
            DESCRIPTIONNATODESIGNERSERVERSCRIPT.Hide();
            headerNATODESIGNERSERVERSCRIPT.Hide();
            favimgNATODESIGNERSERVERSCRIPT.Hide();
            HeaderNATO67.Hide();
            ContextMenuStripNATO67ForDevelopers.Hide();
            appslistNATO67ForDevelopers.Hide();
            taskmgrNATO67ForDevelopers.Hide();
            settingsNATO67ForDevelopers.Hide();
            DescriptionNato67NatoForDevelopers.Hide();
            NATO67ImageBox.Hide();
            GetStartedChatGPTNatoForDevelopers.Hide();
            VBNETClickabuttontogetstartedNATOFORDEVELOPERS.Hide();
            aAttributeHTMLNATOFORDEVELOPERS.Hide();
            basicHTMLNATOFORDEVELOPERS.Hide();
            jslinkHTMLNATOFORDEVELOPERS.Hide();
            csslinkHTMLNATOFORDEVELOPERS.Hide();
            iframeHTMLNATOFORDEVELOPERS.Hide();
            buttonHTMLNATOFORDEVELOPERS.Hide();
            imgHTMLNATOFORDEVELOPERS.Hide();
            compilerdataHTMLNATOFORDEVELOPERS.Hide();
            compilerHTMLNATOFORDEVELOPERS.Hide();
            HEADERCSNATOFORDEV.Hide();
            String1CSNATOFORDEVELOPERS.Hide();
            String2CSNATOFORDEV.Hide();
            string2imgCSNATOFORDEV.Hide();
            uppercaseletterCSNATOFORDEV.Hide();
            upperdescCSNATOFORDEV.Hide();
            lowerdescCSNATOFORDEV.Hide();
            lowercaseletterCSNATOFORDEV.Hide();
            label190.Hide();
            noticeupperlowerCSNATOFORDEV.Hide();
            formatsHTMLNATOFORDEVELOPERS.Hide();
            compilecodeHTMLNATOFORDEVELOPERS.Hide();
            descriptionHTMLNATOFORDEVELOPERS.Hide();
            TutorialsLabelMailNatoDevelopers.Hide();
            IntegrateTextBoxMailNatoForDevelopers.Hide();
            NatoForDevelopersMailTXTHeader.Hide();
            NATOMailTutorialNatoDevelopers.Hide();
            TextBoxMailNatoForDevelopers.Hide();
            ThisCodeSupportscsTXTNatoForDevelopers.Hide();
            NatoForDevelopersMailTXTHeader.Text = "Nato Designer";
            NatoWebTutorialHeaderNatoForDevelopers.Hide();
            writetextButtonWebTutorialNatoForDevelopers.Hide();
            buildgraphButtonWebTutorialNatoForDevelopers.Hide();
            displayimageButtonWebTutorialNatoForDevelopers.Hide();
            changebackgroundcolorButtonWebTutorialNatoForDevelopers.Hide();
            mathButtonWebTutorialNatoForDevelopers.Hide();
            styleButtonWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopers.Hide();
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Hide();
            RecommendedStartingPointNatoForDevelopersNatoWebTutorialTXT.Hide();
            ChatGPTDescriptionNatoForDevelopers.Hide();
            ChatGPTInfoTXTNatoForDevelopers.Hide();
            ChatGPTLabelNatoForDevelopers.Hide();
            ChatGPTMainImageNatoForDevelopers.Hide();
            ExplainLikeIamfiveyearsoldChatGPTNatoForDevelopers.Hide();
            GenerateMeSampleHTMLCodeChatGPTNatoForDevelopers.Hide();
            generateimagesChatGPTNatoForDevelopers.Hide();
            HeaderChatGptTutorialNatoDevelopers.Hide();
            ButtonClickDescriptionVBNETNATOFORDEVELOPERS.Hide();
            buttonclickVBNETNATOFORDEVELOPERS.Hide();
            datetimeVBNETNATOFORDEVELOPERS.Hide();
            debugVBNETNATOFORDEVELOPERS.Hide();
            formatform1VBNETNATOFORDEVELOPERS.Hide();
            formatimagesVBNETNATOFORDEVELOPERS.Hide();
            getstartedVBNETNATOFORDEVELOPERS.Hide();
            groupboxesVBNETNATOFORDEVELOPERS.Hide();
            HEADERVBNETNATOFORDEVELOPERS.Hide();
            progressbarVBNETNATOFORDEVELOPERS.Hide();
            VBNETBOXIMGNATOFORDEVELOPERS.Hide();
            VBNETDESCRIPTIONNATOFORDEVELOPERS.Hide();
            VBNETLOGONATOFORDEVELOPERS.Hide();
            headerHTMLNATOFORDEVELOPERS.Hide();
            //Mine
            DownloadNatoDesignerBtnNatoForDevelopers1.Show();
            Label2NatoForDevelopersNatoDesigner.Show();
            NatoForDevelopersDesignerBox1.Show();
            NatoForDevelopersDesignerBox2.Show();
            NatoForDevelopersDesignerBox3.Show();
            NatoForDevelopersDesignerBox4.Show();
            NatoForDevelopersDesignerBox5.Show();
            NatoForDevelopersDesignerBox6.Show();
            LabelText1NatoForDevelopersNatoDesigner.Show();
            TXT2ServerscriptNatoForDevelopersNatoDesignerPage.Show();
            NatoForDevelopersNatoDesignerImage.Show();
            ServerscriptCodeForNatoDevelopersNatoDesigner.Show();

        }

        private void MainPageNatoForDevelopers_Click(object sender, EventArgs e)
        {
            SharedItemsNatoForDevelopers.Hide();
        }

        private void DownloadNatoDesignerBtnNatoForDevelopers1_Click(object sender, EventArgs e)
        {
            NatoDesignerSetupWizard.Show();
        }

        private void PreviousNatoForDevelopersTXTSlideshowWeb_Click(object sender, EventArgs e)
        {

        }

        private void FowardNatoForDevelopersTXTSlideshowWeb_Click(object sender, EventArgs e)
        {

        }

        private void writetextButtonWebTutorialNatoForDevelopers_Click(object sender, EventArgs e)
        {
            ImageTopicsWebTutorialNatoForDevelopers.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/web/WriteHelloWorld.png");
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Text = "Command: https://nato-cmd.dev/!print::Hello&spac;World&excl;!";
        }

        private void buildgraphButtonWebTutorialNatoForDevelopers_Click(object sender, EventArgs e)
        {
            ImageTopicsWebTutorialNatoForDevelopers.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/web/Plot1!10.png");
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Text = "Command: https://nato-cmd.dev/!plot-1.10";

        }

        private void displayimageButtonWebTutorialNatoForDevelopers_Click(object sender, EventArgs e)
        {
            ImageTopicsWebTutorialNatoForDevelopers.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/web/PlaceImage.png");
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Text = "Command: https://nato-cmd.dev/!print::Hello&spac;World&excl;!";
        }

        private void changebackgroundcolorButtonWebTutorialNatoForDevelopers_Click(object sender, EventArgs e)
        {
            ImageTopicsWebTutorialNatoForDevelopers.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/web/PlaceImage.png");
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Text = "Command: https://nato-cmd.dev/!image-URL::/myimage.jpg!";
        }

        private void mathButtonWebTutorialNatoForDevelopers_Click(object sender, EventArgs e)
        {
            ImageTopicsWebTutorialNatoForDevelopers.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/web/MathAdd.png");
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Text = "Command: https://nato-cmd.dev/!print::Hello&spac;World&excl;!";
        }

        private void styleButtonWebTutorialNatoForDevelopers_Click(object sender, EventArgs e)
        {
            ImageTopicsWebTutorialNatoForDevelopers.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/web/Styles.png");
            ImageTopicsWebTutorialNatoForDevelopersTXTDescription.Text = "https://winforms.nato-cmd.dev/!new:CheckedListBox,Red,Blue,Green,Yellow,Brown,Black,Violet,Pink!new:button.buttonText::Submit";
        }

        private void ExplainLikeIamfiveyearsoldChatGPTNatoForDevelopers_Click(object sender, EventArgs e)
        {
            ChatGPTDescriptionNatoForDevelopers.Text = "Explain GPT Like I I am 5 years old";
            ChatGPTMainImageNatoForDevelopers.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/gpt/gpt5.png");
        }

        private void GenerateMeSampleHTMLCodeChatGPTNatoForDevelopers_Click(object sender, EventArgs e)
        {
            ChatGPTMainImageNatoForDevelopers.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/gpt/samplehtml.png");
            ChatGPTDescriptionNatoForDevelopers.Text = "Generate sample HTML code";

        }

        private void GetStartedChatGPTNatoForDevelopers_Click(object sender, EventArgs e)
        {
            ChatGPTMainImageNatoForDevelopers.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/gpt/signin.png");
            ChatGPTDescriptionNatoForDevelopers.Text = "(ChatGPT Sign in page)";

        }

        private void generateimagesChatGPTNatoForDevelopers_Click(object sender, EventArgs e)
        {
            ChatGPTMainImageNatoForDevelopers.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/gpt/generateimage.png");
            ChatGPTDescriptionNatoForDevelopers.Text = "Generate a sample image";

        }

        private void settingsNATO67ForDevelopers_Click(object sender, EventArgs e)
        {
            NATO67ImageBox.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/nato 6and7/settings.png");
            DescriptionNato67NatoForDevelopers.Text = "This is the settings application for NATO OS 6 & 7.\n Red: Settings accessible link";

        }

        private void taskmgrNATO67ForDevelopers_Click(object sender, EventArgs e)
        {
            NATO67ImageBox.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/nato 6and7/taskmgr.png");
            DescriptionNato67NatoForDevelopers.Text = "This is the task manager application for NATO OS 6 & 7. This\n app is responsible for showing the active apps, and force\n closing apps. \n Red: Selected task for option\n Green: A cursor selected app\n Blue: the End Task button, for force-closing applications.\n Yellow: To keep monitoring the apps even when the Task Manager App is closed.  ";

        }

        private void appslistNATO67ForDevelopers_Click(object sender, EventArgs e)
        {
            NATO67ImageBox.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/nato 6and7/apps.png");
            DescriptionNato67NatoForDevelopers.Text = "This is the apps list for Nato Os 6 & 7\n Green: Downloaded app, or app that was clicked to download;\n Red: Recommended App\n Blue: Downloading queue & progress.";

        }

        private void ContextMenuStripNATO67ForDevelopers_Click(object sender, EventArgs e)
        {
            NATO67ImageBox.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/nato 6and7/menu.png");

            DescriptionNato67NatoForDevelopers.Text = "This is the right-click menu strip for NATO 6 - 7\n (also known as the Context Menu Strip)\n This Menu has Quick-Actions. Some of\n these actions can be found in settings\n or on this menu.";
        }

        private void debugVBNETNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            VBNETBOXIMGNATOFORDEVELOPERS.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/vbnet/debug.png");
        }

        private void getstartedVBNETNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            VBNETBOXIMGNATOFORDEVELOPERS.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/vbnet/GetStarted.png");

        }

        private void formatform1VBNETNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            VBNETBOXIMGNATOFORDEVELOPERS.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/vbnet/form1.png");

        }

        private void progressbarVBNETNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            VBNETBOXIMGNATOFORDEVELOPERS.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/vbnet/bar.png");

        }

        private void imageformatsVBNETNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            VBNETBOXIMGNATOFORDEVELOPERS.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/vbnet/image.png");

        }

        private void groupboxesVBNETNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            VBNETBOXIMGNATOFORDEVELOPERS.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/vbnet/GroupBox.png");

        }

        private void datetimeVBNETNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            VBNETBOXIMGNATOFORDEVELOPERS.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/vbnet/DateTimePicker.png");

        }

        private void buttonclickVBNETNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            VBNETBOXIMGNATOFORDEVELOPERS.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/SystemIcons.NATODeveloper/ImageSlideshows/vbnet/back.png");

        }

        private void formatsHTMLNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            compilerdataHTMLNATOFORDEVELOPERS.Text = "<!DOCTYPE html>\n<html>\n<body>\n<h1>Example Document</h1>\n\n<h1>Example Document</h1>\n\n<h2>Example Document</h2>\n\n<h3>Example Document</h3>\n\n<h4>Example Document</h4>\n\n<h5>Example Document</h5>\n\n<h6>Example Document</h6>\n\n</body>\n</html>";

            compilerHTMLNATOFORDEVELOPERS.DocumentText = compilerdataHTMLNATOFORDEVELOPERS.Text;

        }

        private void imgHTMLNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            compilerdataHTMLNATOFORDEVELOPERS.Text = "<!DOCTYPE html>\n<html>\n<body>\n<h1>Example Image</h1>\n<img src='https://media.architecturaldigest.com/photos/5da74823d599ec0008227ea8/master/pass/GettyImages-946087016.jpg' width='150' height='100' alt='Image' title='Image of NYC / NON COPYRIGHTED IMG'>\n</body>\n</html>";

            compilerHTMLNATOFORDEVELOPERS.DocumentText = compilerdataHTMLNATOFORDEVELOPERS.Text;

        }

        private void buttonHTMLNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            compilerdataHTMLNATOFORDEVELOPERS.Text = "<!DOCTYPE html>\n<html>\n\n<body>\n<h1>Example Document</h1>\n<p>Click the button</p>\n<button onclick='alert(Date())'>Click Me!</button>\n</body>\n</html>";

            compilerHTMLNATOFORDEVELOPERS.DocumentText = compilerdataHTMLNATOFORDEVELOPERS.Text;

        }

        private void csslinkHTMLNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            compilerdataHTMLNATOFORDEVELOPERS.Text = "<!DOCTYPE html>\n<html>\n    <link rel='stylesheet' type='text/css' href='D:/example.css'>\n <body>\n<h1>Example Document</h1>\n<p>The document is red beacuse of <i>D:/example.css</i></p>\n</body>\n</html>";

            compilerHTMLNATOFORDEVELOPERS.DocumentText = compilerdataHTMLNATOFORDEVELOPERS.Text;

        }

        private void iframeHTMLNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            compilerdataHTMLNATOFORDEVELOPERS.Text = "<!DOCTYPE html>\n<html>\n<body>\n<h1>Example Iframe</h1>\n<i>This iframe will lead to google.com</i><iframe src='https://google.com' width='300' height='300'></iframe>\n</body>\n</html>";

            compilerHTMLNATOFORDEVELOPERS.DocumentText = compilerdataHTMLNATOFORDEVELOPERS.Text;

        }

        private void jslinkHTMLNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            compilerdataHTMLNATOFORDEVELOPERS.Text = "<!DOCTYPE html>\n<html>\n<script src='D:/example.js'></script>\n<body>\n<h1>Example Document</h1>\n<button onclick='thisfunction()'>Click to preform an action from D:/example.js</button>\n</body>\n</html>";

            compilerHTMLNATOFORDEVELOPERS.DocumentText = compilerdataHTMLNATOFORDEVELOPERS.Text;

        }

        private void aAttributeHTMLNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            compilerdataHTMLNATOFORDEVELOPERS.Text = "<!DOCTYPE html>\n<html>\n<body>\n<h1>Example Document</h1>\n<a href='https://google.com'>This is a hyperlink, click me!</a>\n</body>\n</html>";

            compilerHTMLNATOFORDEVELOPERS.DocumentText = compilerdataHTMLNATOFORDEVELOPERS.Text;

        }

        private void basicHTMLNATOFORDEVELOPERS_Click(object sender, EventArgs e)
        {
            compilerdataHTMLNATOFORDEVELOPERS.Text = "<!DOCTYPE html>\n<html>\n<body>\n<h1>Example Document</h1>\n<p>Welcome to my example document.</p>\n</body>\n</html>";
            compilerHTMLNATOFORDEVELOPERS.DocumentText = compilerdataHTMLNATOFORDEVELOPERS.Text;

        }
        private void ImageSlideshowWidget_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                isDragging = false; // Stop dragging
            }
        }
        private void ImageSlideshowWidget_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }

        }

        private void ImageSlideshowWidget_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                ImageSlideshowWidgetNATO.Left += e.X - dragStartPoint.X;
                ImageSlideshowWidgetNATO.Top += e.Y - dragStartPoint.Y;
            }
        }
        private void compilecodeHTMLNATOFORDEVELOPERS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            compilerHTMLNATOFORDEVELOPERS.DocumentText = compilerdataHTMLNATOFORDEVELOPERS.Text;

        }

        private void allappslink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AllAppsBox.Show();

        }

        private void XAllApps_Click(object sender, EventArgs e)
        {
            AllAppsBox.Hide();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form3.Hide();
        }

        private void browserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rdpweb.Show();
        }

        private void notePadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rdpnotepad.Show();
        }

        private void codeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rdpcode.Show();
        }

        private void FancyDivider10_Enter(object sender, EventArgs e)
        {

        }

        private void xboxfornatoos7linkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("XBOX Games for NATO was discontinued 2020, opening NATO GAMES.", "Xbox for NATO OS 7");
            GamesBox.Show();
        }

        private void windowsexcellinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            excelbox.Show();
            excelgridview.Hide();
            button42.Enabled = false;
        }

        private void windowswordlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            microsoftwordbox.Show();
            txtboxpublisher.Hide();
            numericUpDown2.Hide();
        }

        private void windowsmicrosoftoutlooklinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void windowspowerpointlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            powerpointbox.Show();
            powerpointpresentationbox.Hide();
        }

        private void windowsvisualstudiocodelinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisualStudioCodeBoxMicrosoft.Show();
        }

        private void windowsinternetexplorerlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InternetExplorerBox.Show();
            IE_Browser.Navigate("https://msn.com");
            sendWithAuthenticMessengerToolStripMenuItem.Enabled = false;


        }

        private void ExternalAppVmwareWorkstationlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            vmwarebox.Show();
        }

        private void tgaclslinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Access denied.", "NATO-OS 7", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void taskmgrlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            customapp.Show();
            natocmd.Hide();
            settingsbox.Show();
            backgrounddisplaybox.Hide();
            discstoragebox.Hide();
            helpsupportbox.Hide();
            uninstallappsgroup.Hide();
            privacygroup.Hide();
            taskmgrbox.Show();
            DownloadNatoDesigner.Show();

        }

        private void storagelinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            customapp.Show();
            natocmd.Hide();
            settingsbox.Show();
            backgrounddisplaybox.Hide();
            discstoragebox.Hide();
            helpsupportbox.Hide();
            uninstallappsgroup.Hide();
            privacygroup.Hide();
            taskmgrbox.Hide();
            DownloadNatoDesigner.Show();
            discstoragebox.Show();
            cleangroup.Hide();
            storagebar.Value = 18;
            storage1.Value = 18;
            storage2.Value = 17;
            storage3.Value = 19;
            storage4.Value = 4;
            storage5.Value = 6;
            storage6.Value = 23;
            storage7.Value = 6;
            storage8.Value = 10;
            storage9.Value = 100;
        }

        private void settingslinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            customapp.Show();
            natocmd.Hide();
            settingsbox.Show();
            backgrounddisplaybox.Hide();
            discstoragebox.Hide();
            helpsupportbox.Hide();
            uninstallappsgroup.Hide();
            privacygroup.Hide();
            taskmgrbox.Hide();
            DownloadNatoDesigner.Show();

        }

        private void restoreorretrylinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            restoreorresetbox.Show();
        }

        private void remotedesktopconnectionlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            recentapp.Text = "Remote Desktop Connection";

            customapp.Show();
            rdpbox.Show();
            connectlabelrdp.Hide();
            moreoptionboxrdp.Hide();
            form3.Hide();
            filesboxrdp.Hide();
            notepadboxrdp.Hide();

            //RDP Loading
            menurdp.Hide();
            disconnectrdp.Hide();
            browserboxrdp.Hide();
            settingsboxrdp.Hide();
            backgrounddisplayboxrdp.Hide();
            Remotedesktoprdp.Hide();
        }

        private void ryalalink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            groupBox2.Show();
            textBox2.Text = "##BATCH @NEW-SSCRIPY##\n{/nFILE.LOAD(n)\n int n = https://938479817-37181983649-123687619.FBASE.PWS.XYZ\n}\n#ENDIF#";

        }

        private void printtopdflinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PrintToPDFBox.Show();
        }

        private void natolinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Access denied.", "NATO-OS 7", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void notepadlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //NOTEPAD APP
            recentapp.Text = "NotePad";

            groupBox2.Show();
        }

        private void maillinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MailBoxMainBox.Show();
            DownloadNatoDesigner.Show();
        }

        private void magnifierlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MagnifierBox.Show();
        }

        private void mediaplayerlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MediaPlayerApp.Show();
        }

        private void ixviewlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ixviewbox.Show();
        }

        private void iexplorerlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InternetExplorerBox.Show();
            IE_Browser.Navigate("https://msn.com");
            sendWithAuthenticMessengerToolStripMenuItem.Enabled = false;

        }

        private void helplinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            customapp.Show();
            natocmd.Hide();
            settingsbox.Show();
            backgrounddisplaybox.Hide();
            discstoragebox.Hide();
            helpsupportbox.Show();
            uninstallappsgroup.Hide();
            privacygroup.Hide();
            taskmgrbox.Hide();
        }

        private void gameslinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GamesBox.Show();
        }

        private void findlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            findtextapp.Show();
        }

        private void fboostsettingslinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SearchAppsFBoostOptimizer.Enabled = false;
            fboostoptimizer.Show();
        }

        private void einkpadlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EINKPAD.Show();
        }

        private void eventlogslinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            MessageBox.Show("Event Logs has been removed in 7/28/2011,\nRedirecting to event viewer...", "Event Viewer", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            eventviewerbox.Show();

        }

        private void eventviewerlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            eventviewerbox.Show();

        }

        // Event handler for updating the label with elapsed time
        private void UiUpdateWindowsFormsPlayerTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = UpTimeWindowsFormsPlayerBoxTimer.Elapsed;
            ExceptionTimeWindowsFormsPlayer.Text = $"Active Time: {elapsedTime:hh\\:mm\\:ss\\.ff}";
        }
        private void developerlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            recentapp.Text = "NATO Developers";
            NATODevelopersApp.Show();
            SearchResultsForTextNatoDevelopersTXT.Hide();
            TextNatoDevelopersInfo.Hide();
            SharedItemsNatoForDevelopers.Hide();
        }

        private void desktoplinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            filesbox.Show();
            filesdir.Navigate("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/DESKTOP");
        }

        private void designerlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            designertextbox.Show();
        }

        private void codelinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            editorbox.Show();
            recentapp.Text = "Code";
        }

        private void terminallinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            customapp.Show();
            natocmd.Show();
            recentapp.Text = "Command Terminal";
        }

        private void controlpanellinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            customapp.Show();
            natocmd.Hide();
            settingsbox.Show();
            backgrounddisplaybox.Hide();
            discstoragebox.Hide();
            helpsupportbox.Hide();
            uninstallappsgroup.Hide();
            privacygroup.Hide();
            taskmgrbox.Hide();
            DownloadNatoDesigner.Show();
        }

        private void computerlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WelcomeStartupBox.Show();
        }

        private void calculatorlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Calculator.Show();
        }

        private void cameralinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Start_cam();
            cameraapp.Show();
        }

        private void browserlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            browserBox.Show();
        }

        private void backupandrestorelinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            backuprestorebox.Show();
            restorebtnbackupandrestore.Enabled = false;
        }

        private void aodesignerlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AODesignerBox.Show();
            designerboxao.Hide();
        }

        private void appshoplinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DownloadNatoDesigner.Show();
            recentapp.Text = "Store";
            librarybox.Hide();
            SpotifyBtn.Hide();
            DownloadStats.ForeColor = Color.Black;
            DownloadStats.Text = "Downloading: 0%";
            recentapp.Text = "App Shop";
            shopapp.Show();
            HomeAPP1.Enabled = false;
            appbox1.Hide();
            ShowApps1.Show();
            //Download Buttons
            ChromiumDownloadBTN.Hide();
            //Download Stats
            DownloadStats1.Hide();
            DownloadStats1.Value = 0;
            DownloadStats2.Hide();
            DownloadStats2.Value = 0;
            DownloadStats3.Hide();
            DownloadStats3.Value = 0;
            moreappsboxstore.Hide();
            libraryclose.Enabled = true;
            shopappclose.Enabled = true;
            searchappbtn.Enabled = true;
            HomeAPP1.Enabled = true;
            Account1.Enabled = true;
            Library1.Enabled = true;
            closelibrary.Hide();
            OpenFileandPdfReaderandBrowserPlusBox.Hide();
            YourMailandstylusPadBox.Hide();
        }

        private void downloadnatodesignerlinkallapps_Click(object sender, EventArgs e)
        {
            DownloadingNatoDesigner.Hide();
            DownloadBTNNatoDesigner.Enabled = false;
            NatoDesignerSetupWizard.Show();
        }

        private void XAODesigner_Click(object sender, EventArgs e)
        {
            AODesignerBox.Hide();
        }

        private void plainaodesigner_Click(object sender, EventArgs e)
        {
            plaindocumentaodesigner.Show();
            designerboxao.Show();

            ChangeImageAOTextDesigner.Hide();
            AOTextDesignerImg.Hide();
            AOTextDesignerstring1.Hide();
            AOtextdesignerstring2.Hide();
        }

        private void imageparagraphaodesigner_Click(object sender, EventArgs e)
        {
            plaindocumentaodesigner.Hide();
            designerboxao.Show();
            ChangeImageAOTextDesigner.Show();
            AOTextDesignerImg.Show();
            AOTextDesignerstring1.Show();
            AOtextdesignerstring2.Show();

        }

        private void uploadfileaodesigner_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            designerboxao.Hide();

        }

        private void ChangeImageAOTextDesigner_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            AOTextDesignerImg.Image = System.Drawing.Image.FromFile(openFileDialog2.FileName);
        }

        private void backuprestoreboxhide_Click(object sender, EventArgs e)
        {
            backuprestorebox.Hide();
        }

        private async void backupnowbackorrestore_Click(object sender, EventArgs e)
        {
            await Task.Delay(5000);
            progressBarbackuporrestore.Value = 10;
            await Task.Delay(5000);
            progressBarbackuporrestore.Value = 20;

            await Task.Delay(5000);
            progressBarbackuporrestore.Value = 30;


            await Task.Delay(5000);

            progressBarbackuporrestore.Value = 40;
            await Task.Delay(5000);

            progressBarbackuporrestore.Value = 50;
            await Task.Delay(5000);
            progressBarbackuporrestore.Value = 60;
            await Task.Delay(5000);
            progressBarbackuporrestore.Value = 70;
            await Task.Delay(5000);
            progressBarbackuporrestore.Value = 80;
            await Task.Delay(5000);
            progressBarbackuporrestore.Value = 90;
            await Task.Delay(5000);
            progressBarbackuporrestore.Value = 100;
            MessageBox.Show("Backup complete", "Backup & Restore");
            DateTime current = DateTime.Now;
            backupandrestorelist.Items.Add("Backup from:" + current.ToString());
        }

        private async void restorebtnbackupandrestore_Click(object sender, EventArgs e)
        {
            await Task.Delay(2500);
            MessageBox.Show("Restore complete", "Backup & Restore");

        }

        private void backupandrestorelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            restorebtnbackupandrestore.Show();
        }

        private void hidecameraapp_Click(object sender, EventArgs e)
        {
            cameraapp.Hide();
            frame.Stop();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            output = folderBrowserDialog1.SelectedPath;
        }

        private void stopcameraapp_Click(object sender, EventArgs e)
        {
            frame.Stop();

        }

        private void hidecalculatorbox_Click(object sender, EventArgs e)
        {
            Calculator.Hide();
        }

        private void buttonequalcalculator_Click(object sender, EventArgs e)
        {
            num2 = int.Parse(equationboxcalculator.Text);

            if (option.Equals("+"))
                result = num1 + num2;

            if (option.Equals("-"))
                result = num1 - num2;

            if (option.Equals("*"))
                result = num1 * num2;

            if (option.Equals("/"))
                result = num1 / num2;

            equationboxcalculator.Text = result + "";
        }

        private void buttonclearallcalculator_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Clear();
            result = (0);
            num1 = (0);
            num2 = (0);
        }

        private void buttondivide_Click(object sender, EventArgs e)
        {
            option = "/";
            num1 = int.Parse(equationboxcalculator.Text);

            equationboxcalculator.Clear();
        }

        private void buttonmultiplication_Click(object sender, EventArgs e)
        {
            option = "*";
            num1 = int.Parse(equationboxcalculator.Text);

            equationboxcalculator.Clear();
        }

        private void buttonnine_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + "9";

        }

        private void buttonzero_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + "0";

        }

        private void buttonaddition_Click(object sender, EventArgs e)
        {
            option = "+";
            num1 = int.Parse(equationboxcalculator.Text);

            equationboxcalculator.Clear();
        }

        private void buttonsubtraction_Click(object sender, EventArgs e)
        {
            option = "-";
            num1 = int.Parse(equationboxcalculator.Text);

            equationboxcalculator.Clear();
        }

        private void buttoneight_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + "8";

        }

        private void buttonfour_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + "4";

        }

        private void buttonthree_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + "3";

        }

        private void buttonseven_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + "7";

        }

        private void buttonsix_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + "6";

        }

        private void buttontwo_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + "2";

        }

        private void buttonone_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + "1";
        }

        private void buttonfive_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + "5";

        }
        private void linklabel1_Click(object sender, EventArgs e)
        {
            listBoxEventViewer.Items.Add("Attemted to report bug.");

        }
        private void bugslistbtn_Click(object sender, EventArgs e)
        {
            BugsListNATO.Show();
        }

        private void decimalbtncalculator_Click(object sender, EventArgs e)
        {
            equationboxcalculator.Text = equationboxcalculator.Text + ".";

        }

        private void savebtntextdesigner_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Saved file", "Designer", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void aboutdesignertxtboxbtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("©Designer Text - 1997 Last Updated\nInstalled with system from NATO OS-7\nTerminal commands: launch/ designertext.app", "About Designer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void designertextboxclose_Click(object sender, EventArgs e)
        {
            designertextbox.Hide();
        }

        private void addeventeventviewerbtn_Click(object sender, EventArgs e)
        {
            if (EventViewerAddListBox.Text == "")
            {
                MessageBox.Show("Cannot add empty string", "Event Viewer", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                listBoxEventViewer.Items.Add(EventViewerAddListBox.Text);

            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("©Event Viewer - 2020 Last Updated\nInstalled with system from NATO OS-7\nTerminal commands: launch/ evtvwr.app", "About Event Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void XEventViewerBtn_Click(object sender, EventArgs e)
        {
            eventviewerbox.Hide();
        }

        private void notificationsboxNATO_Enter(object sender, EventArgs e)
        {

        }

        private void HideEINKPAD_Click(object sender, EventArgs e)
        {
            EINKPAD.Hide();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            old = e.Location;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                current = e.Location;
                g.DrawLine(pyen, old, current);
                graph.DrawLine(pyen, old, current);
                old = current;
            }
        }
        private Point mouseOffsetPos;
        private bool isMouseDown = false;
        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffsetPos = new Point(-e.X, -e.Y);
                isMouseDown = true;
            }
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffsetPos.X, mouseOffsetPos.Y);
                this.Location = mousePos;
            }
        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void EraserEINK_Click(object sender, EventArgs e)
        {
            pyen.Color = Color.White;
        }

        private void AboutEINKPAd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("©EINKPad - 2021 Last Updated\nInstalled with system from NATO OS-7\nTerminal commands: launch/ eink.app", "E-INK Pad", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void HideEINKPAD_Click_1(object sender, EventArgs e)
        {
            EINKPAD.Hide();
        }

        private void SaveButtonEINK_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Saved!", "E-INK Pad", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void hidefboostoptimizer_Click(object sender, EventArgs e)
        {
            fboostoptimizer.Hide();
        }

        private void resetfboostsettings_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Reset FBoost Settings?", "System Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (DialogResult == DialogResult.Yes)
            {
                cachecomplete.Show();
            }
            else if (DialogResult == DialogResult.No)
            {
                //Aborted
            }
        }

        private async void fboostthispc_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to FBOOST your NATO OS?", "System Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (DialogResult == DialogResult.Yes)
            {
                this.Hide();
                MessageBox.Show("FBoost requires the system to restart.", "FBoost");
                Application.Exit();
                await Task.Delay(2500);
                this.Show();
            }
            else if (DialogResult == DialogResult.No)
            {
                //Aborted
            }
        }

        private void aboutfboost_Click(object sender, EventArgs e)
        {
            MessageBox.Show("When you FBOOST a device, you will reset-optimize the devices, graphics, memory, sound drivers etc.\nTerminal Command: launch/fboost.APP", "About FBoost", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void findtext_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
            textBox2.SelectAll();
            textBox3.SelectAll();
            textBox4.SelectAll();
            textBox5.SelectAll();
            textBox6.SelectAll();
            textBox7.SelectAll();
            textBox8.SelectAll();
            textBox9.SelectAll();
            textBox10.SelectAll();
            textBox11.SelectAll();
            textBox12.SelectAll();
            textBox13.SelectAll();
            richTextBox1.SelectAll();
            from.SelectAll();
            subj.SelectAll();
            content.SelectAll();
            pass.SelectAll();
            to.SelectAll();
            TextBoxCalendarTemplate1.SelectAll();
            TextBoxMailNatoForDevelopers.SelectAll();
            TextAreaCalendarTemplate1.SelectAll();
            string3ParagraphCalendar.SelectAll();
        }

        private void hidefindapp_Click(object sender, EventArgs e)
        {
            findtextapp.Hide();
        }

        private void HideGames_Click(object sender, EventArgs e)
        {
            GamesBox.Hide();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            label200.Text = "Hello, " + textBox14.Text;
        }

        private void IE_Exit_Click(object sender, EventArgs e)
        {
            InternetExplorerBox.Hide();
        }

        private void IE_GoBackButton_Click(object sender, EventArgs e)
        {
            IE_Browser.GoBack();
        }

        private void IE_GoFowardButton_Click(object sender, EventArgs e)
        {
            IE_Browser.GoForward();
        }

        private void IE_SearchBar_Enter(object sender, EventArgs e)
        {
            IE_Browser.Navigate(IE_SearchBar.Text);
        }
        private void IE_SearchBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void IE_Search_Click(object sender, EventArgs e)
        {
            IE_Browser.Navigate(IE_URLBar.Text);

        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IE_Browser.Navigate(IE_URLBar.Text);
        }

        private void createShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void goBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IE_Browser.GoBack();
        }

        private void goFowardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IE_Browser.GoForward();
        }

        private void reportBugsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BugsListNATO.Show();
        }

        private void mSNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IE_Browser.Navigate("https://msn.com");
        }

        private void defaultWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InternetExplorerBox.BackColor = Color.White;
        }

        private void blueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InternetExplorerBox.BackColor = Color.LightBlue;

        }

        private void IXVIEWHide_Click(object sender, EventArgs e)
        {
            ixviewbox.Hide();

        }

        private void disconnectedbtnixview_Click(object sender, EventArgs e)
        {
            ixviewString1.Text = "No Connection";
            ixviewString2.Text = "No avalible network";
            disconnectedbtnixview.Enabled = false;
            diagnosenetworkixview.Enabled = false;
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void helpToolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("©Media Player / Media Recorder - 2019 Last Updated\nInstalled with system from NATO OS-6\nTerminal commands: launch/ media.app", "Media Player / Recorder", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void openToolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void label192_Click(object sender, EventArgs e)
        {

        }

        private void HideMediaPlayerAndRecorder_Click(object sender, EventArgs e)
        {
            MediaPlayerApp.Hide();
        }

        private void ChooseVideosMediaPlayerBtn_Click(object sender, EventArgs e)
        {
            // Configure the OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Video Files|*.mp4;*.avi;*.mov;*.wmv;*.mkv",
                Title = "Select a Video File"
            };

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string videoPath = openFileDialog.FileName;

                // TODO: Add your video player logic to play the selected video
                axWindowsMediaPlayer4.URL = videoPath;
            }
        }

        private void coinsmediaplayerlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string f = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/coins.mp4";
            axWindowsMediaPlayer4.URL = f;
        }

        private void americamediaplayerlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string f = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/america.mp4";
            axWindowsMediaPlayer4.URL = f;
        }

        private void divideby0mediaplayerlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string f = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/divideby0.mp4";
            axWindowsMediaPlayer4.URL = f;
        }

        private void bigcarmediaplayerlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string f = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/bigcar.mp4";
            axWindowsMediaPlayer4.URL = f;
        }

        private void examplevideomediaplayerlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string f = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/Untitled_Video1.mp4";
            axWindowsMediaPlayer4.URL = f;
        }

        private void HideMagnifierApp_Click(object sender, EventArgs e)
        {
            MagnifierBox.Hide();
        }

        private void findaddressbtnmap_Click(object sender, EventArgs e)
        {
            string street = textBox16.Text;
            string city = textBox15.Text;
            string state = textBox18.Text;
            string zip = textBox17.Text;

            StringBuilder queryaddress = new StringBuilder();
            queryaddress.Append("http://google.com/maps?q=");
            if (street != string.Empty)
            {
                queryaddress.Append(street + "," + "+");
            }
            if (city != string.Empty)
            {
                queryaddress.Append(city + "," + "+");
            }
            if (state != string.Empty)
            {
                queryaddress.Append(state + "," + "+");
            }
            if (zip != string.Empty)
            {
                queryaddress.Append(zip + "," + "+");
            }
            GoogleMapsMapApp.Navigate(queryaddress.ToString());
        }

        private void PrintToPDFCloseBtn_Click(object sender, EventArgs e)
        {
            PrintToPDFBox.Hide();
        }

        private void helpToolStripButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NATO® Print to PDF - 2013 \nInstalled with system from NATO OS-4\nTerminal commands: launch/ ptp.app", "NATO® Print to PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void printToolStripButton1_Click(object sender, EventArgs e)
        {
            // Configure the OpenFileDialog
            PrintDialog printdialog = new PrintDialog
            {

            };

            // Show the dialog and check if the user selected a file

        }

        private void openToolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF Files|*.pdf",
                Title = "Open PDF"
            };

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pdf = openFileDialog.FileName;

                // TODO: Add your video player logic to play the selected video
                printtopdfbrowserbox.Navigate(pdf);
            }
        }

        private void hideblenderbox_Click(object sender, EventArgs e)
        {
            //NYC Exit
            batteryparknewyorkcityview.Hide();
            centralparknewyorkcityview.Hide();
            twintowersnewyorkcityview.Hide();
            empirestatenewyorkcityview.Hide();
            jfkairportnewyorkcityview.Hide();
            timessquarenewyorkcityview.Hide();
            newyorkcityhallnewyorkcityview.Hide();
            //SF. EXIT
            goldengatebridgesanfranciscocityview.Hide();
            lombardstsanfranciscocityview.Hide();
            ferrybuildingsanfranciscocityview.Hide();
            coittowersanfranciscocityview.Hide();
            goldengateparksanfranciscocityview.Hide();
            alcatrazislsanfranciscocityview.Hide();
            sanfranciscointlairportsanfranciscocityview.Hide();
            //PANEL EXIT
            blenderbox.Hide();
            spaceviewblenderbox.Hide();
            cityviewboxblender.Hide();
            blendercityviewdialogbox.Hide();
            SpaceViewConsole.Items.Add("Exited project, " + nameboxspaceviewconsole.Text);

        }

        private async void blenderlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SpaceViewConsole.BackColor = Color.White;
            spaceviewblenderbox.Hide();
            selectoptionblender.Hide();
            blenderbox.Hide();
            blenderloadingassetsbox.Show();
            BlenderDialogExtractingAssets.Text = "none avalible";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderPackage.KB100.NewPause";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderPackage.KB100.Animation";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "C# Animation";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "using; Blender.[attribution]";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.Click.KB381";
            blenderloadingpayloadbar.Value = 10;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderPackage.BlenderSpace";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "LiveUpdates";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.GoogleEarthLibrary";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.VisualBasic";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.SoundEffects";
            blenderloadingpayloadbar.Value = 20;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderToolbar";
            blenderloadingpayloadbar.Value = 25;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.PackageBind";
            blenderloadingpayloadbar.Value = 45;
            await Task.Delay(1000);
            blenderloadingpayloadbar.Value = 65;
            await Task.Delay(800);
            blenderloadingpayloadbar.Value = 85;
            await Task.Delay(900);
            blenderloadingpayloadbar.Value = 85;
            await Task.Delay(900);
            blenderloadingpayloadbar.Value = 100;
            BlenderDialogExtractingAssets.Text = "none avalible";
            await Task.Delay(1000);
            blenderloadingassetsbox.Hide();
            blenderbox.Show();


        }

        private void openToolStripButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Blender Files|*.blendproj",
                Title = "Open Blender Files",

            };

        }

        private void saveToolStripButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Blender Files|*.blendproj",
                Title = "Save Blender File",
            };
        }

        private void printToolStripButton2_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog
            {

            };

        }

        private void helpToolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Blender™ \nInstalled with system (if system has Blender API 1.4) from NATO OS-3\nTerminal commands: launch/ blender.app\nSystem Requirements:\n *System Requires 16MB Storage\n *System requires 1MB RAM\nMust have BlenderAPI-1.4 (https://blender.nato.com/oldversion/1.4.html)", "Blender™", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void newprojectblender_Click(object sender, EventArgs e)
        {
            selectoptionblender.Show();
        }

        private async void spaceviewblender_Click(object sender, EventArgs e)
        {
            selectoptionblender.Hide();
            blenderloadingassetsbox.Show();
            BlenderDialogExtractingAssets.Text = "none avalible";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderPackage.KB100.NewPause";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderPackage.KB100.Animation";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "C# Animation";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "using; Blender.[attribution]";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.Click.KB381";
            blenderloadingpayloadbar.Value = 10;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderPackage.BlenderSpace";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "LiveUpdates";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.GoogleEarthLibrary";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.VisualBasic";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.SoundEffects";
            blenderloadingpayloadbar.Value = 20;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderToolbar";
            blenderloadingpayloadbar.Value = 25;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.PackageBind";
            blenderloadingpayloadbar.Value = 45;
            await Task.Delay(1000);
            blenderloadingpayloadbar.Value = 65;
            await Task.Delay(800);
            blenderloadingpayloadbar.Value = 85;
            await Task.Delay(900);
            blenderloadingpayloadbar.Value = 85;
            await Task.Delay(900);
            blenderloadingpayloadbar.Value = 100;
            BlenderDialogExtractingAssets.Text = "none avalible";
            await Task.Delay(1000);
            blenderloadingassetsbox.Hide();
            //Start the project
            spaceviewblenderbox.Show();
            cityviewboxblender.Hide();
            SpaceViewConsole.Items.Add("Space View Project Started");
            SpaceViewConsole.Items.Add("New project, " + nameboxspaceviewconsole.Text);
            await Task.Delay(200);
            toolStripProgressBar1.Value = 50;
            await Task.Delay(100);
            toolStripProgressBar1.Value = 100;
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Angle.Earth.png");
            await Task.Delay(1000);
            toolStripProgressBar1.Value = 10;
            //Interactive Buttons
            clickforeuropespaceview.Hide();
            clickforunitedstatesspaceview.Hide();
            clickforeastcoastspaceview.Hide();
            clickforwestcoastspaceview.Hide();
            clickforparisspaceview.Hide();
            clickforturkeyspaceview.Hide();
            clickforeiffeltowerspaceview.Hide();
            clickforcaliforniaspaceview.Hide();
            allowisPlanet = false;
        }

        private async void cityviewblender_Click(object sender, EventArgs e)
        {
            selectoptionblender.Hide();
            blenderloadingassetsbox.Show();
            BlenderDialogExtractingAssets.Text = "none avalible";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderPackage.KB100.NewPause";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderPackage.KB100.Animation";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "C# Animation";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "using; Blender.[attribution]";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.Click.KB381";
            blenderloadingpayloadbar.Value = 10;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderPackage.BlenderSpace";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "LiveUpdates";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.GoogleEarthLibrary";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.VisualBasic";
            blenderloadingpayloadbar.Value = 0;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.SoundEffects";
            blenderloadingpayloadbar.Value = 20;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "BlenderToolbar";
            blenderloadingpayloadbar.Value = 25;
            await Task.Delay(100);
            BlenderDialogExtractingAssets.Text = "Blender.PackageBind";
            blenderloadingpayloadbar.Value = 45;
            await Task.Delay(1000);
            blenderloadingpayloadbar.Value = 65;
            await Task.Delay(800);
            blenderloadingpayloadbar.Value = 85;
            await Task.Delay(900);
            blenderloadingpayloadbar.Value = 85;
            await Task.Delay(900);
            blenderloadingpayloadbar.Value = 100;
            BlenderDialogExtractingAssets.Text = "none avalible";
            await Task.Delay(1000);
            blenderloadingassetsbox.Hide();
            blendercityviewdialogbox.Show();
            cityviewboxblender.Show();
            spaceviewblenderbox.Hide();
            clickforcaliforniaspaceview.Hide();
            cityviewplayerblender.BackColor = Color.Black;
        }

        private void oknameboxspaceviewconsole_Click(object sender, EventArgs e)
        {
            SpaceViewConsole.Items.Add("Name changed for Space View: " + nameboxspaceviewconsole.Text);
        }

        private async void spaceviewdisplay_Click(object sender, EventArgs e)
        {
            if (allowisPlanet == false)
            {
                SpaceViewConsole.Items.Add("Clicked display box for: Space View Display");

                if (toolStripProgressBar1.Value == 10)

                {
                    //Lets the interactive only be clicked once
                    toolStripProgressBar1.Value = 11;

                    await Task.Delay(400);

                    spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Earth.ScrollFoward.png");
                    await Task.Delay(100);
                    spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Earth.ScrollFoward.png");
                    await Task.Delay(100);
                    spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Earth.ScrollFoward.png");
                    await Task.Delay(100);
                    spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/GiantEarth.Size3.png");
                    await Task.Delay(100);
                    spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Earth4.png");

                    clickforeuropespaceview.Show();
                    clickforunitedstatesspaceview.Show();



                }
                if (toolStripProgressBar1.Value == 1)

                {
                    //Lets the interactive only be clicked once
                    toolStripProgressBar1.Value = 11;

                    await Task.Delay(400);

                    spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/mercury1.png");
                    await Task.Delay(100);
                    spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Earth.ScrollFoward.png");
                    await Task.Delay(100);
                    spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Earth.ScrollFoward.png");
                    await Task.Delay(100);
                    spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/GiantEarth.Size3.png");
                    await Task.Delay(100);
                    spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Earth4.png");

                    clickforeuropespaceview.Show();
                    clickforunitedstatesspaceview.Show();



                }
            }
            if (allowisPlanet == false)
            {

            }
        }

        private void ClickFrontspaceviewdisplay_Click(object sender, EventArgs e)
        {


        }

        private async void clickforunitedstatesspaceview_Click(object sender, EventArgs e)
        {
            clickforunitedstatesspaceview.Hide();
            clickforeuropespaceview.Hide();
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Earthamerica.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Earthamericamain.png");
            clickforeastcoastspaceview.Show();
            clickforwestcoastspaceview.Show();
        }

        private async void clickforeastcoastspaceview_Click(object sender, EventArgs e)
        {
            clickforeastcoastspaceview.Hide();
            clickforwestcoastspaceview.Hide();
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eastcoast1.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eastcoast.png");

        }

        private async void clickforwestcoastspaceview_Click(object sender, EventArgs e)
        {
            clickforeastcoastspaceview.Hide();
            clickforcaliforniaspaceview.Show();
            clickforwestcoastspaceview.Hide();
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/westcoast1.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/westcoast.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/westcoast0.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/westcoast01.png");

        }

        private async void clickforeuropespaceview_Click(object sender, EventArgs e)
        {
            clickforunitedstatesspaceview.Hide();
            clickforeuropespaceview.Hide();
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/europe.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/europe1.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/europe2.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/europe3.png");
            clickforparisspaceview.Show();
            clickforturkeyspaceview.Show();

        }

        private async void clickforparisspaceview_Click(object sender, EventArgs e)
        {
            clickforparisspaceview.Hide();
            clickforturkeyspaceview.Hide();
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/france.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/france1.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/france2.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/france3.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/france4.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/france5.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/france6.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/france7.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/france8.png");
            clickforeiffeltowerspaceview.Show();
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/france9.png");
        }

        private async void clickforturkeyspaceview_Click(object sender, EventArgs e)
        {
            clickforparisspaceview.Hide();
            clickforturkeyspaceview.Hide();
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/turkey.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/turkey1.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/turkey2.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/turkey3.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/turkey4.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/turkey5.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/turkey6.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/turkey7.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/turkey8.png");
        }

        private async void clickforeiffeltowerspaceview_Click(object sender, EventArgs e)
        {
            clickforeiffeltowerspaceview.Hide();
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eiffel.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eiffel1.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eiffel2.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eiffel3.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eiffel4.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eiffel5.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eiffel6.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eiffel7.png");
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/eiffel8.png");
        }

        private async void spaceViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Task.Delay(200);
            toolStripProgressBar1.Value = 50;
            await Task.Delay(100);
            toolStripProgressBar1.Value = 100;
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/Angle.Earth.png");
            await Task.Delay(1000);
            toolStripProgressBar1.Value = 10;
            //Interactive Buttons
            clickforeuropespaceview.Hide();
            clickforunitedstatesspaceview.Hide();
            clickforeastcoastspaceview.Hide();
            clickforwestcoastspaceview.Hide();
            clickforparisspaceview.Hide();
            clickforturkeyspaceview.Hide();
            clickforcaliforniaspaceview.Hide();
            clickforeiffeltowerspaceview.Hide();
        }

        private async void viewConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStripProgressBar1.Value == 0)
            {
                MessageBox.Show("Please select a project first", "Blender");
            }
            else
            {
                SpaceViewConsole.BackColor = Color.White;
                await Task.Delay(500);
                SpaceViewConsole.BackColor = Color.Yellow;
                await Task.Delay(500);
                SpaceViewConsole.BackColor = Color.White;
                await Task.Delay(500);
                SpaceViewConsole.BackColor = Color.Yellow;
                await Task.Delay(500);
                SpaceViewConsole.BackColor = Color.White;
                await Task.Delay(500);
                SpaceViewConsole.BackColor = Color.Yellow;
                await Task.Delay(500);
                SpaceViewConsole.BackColor = Color.White;


            }
        }

        private void nextplanetspaceviewblender_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 1;
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/mercury.png");
            clickforeuropespaceview.Hide();
            clickforunitedstatesspaceview.Hide();
            clickforeastcoastspaceview.Hide();
            clickforwestcoastspaceview.Hide();
            clickforparisspaceview.Hide();
            clickforturkeyspaceview.Hide();
            clickforeiffeltowerspaceview.Hide();
            clickforcaliforniaspaceview.Hide();

        }

        private async void clickforsunspaceview_Click(object sender, EventArgs e)
        {
            await Task.Delay(500);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/mercury.png");

        }

        private void OKChangeProjectName_Click(object sender, EventArgs e)
        {
            CityViewConsole.Items.Add("CityView project name changed to: " + textBox19.Text);
        }

        private void sanfranciscoblendercityviewdialogboxbtn_Click(object sender, EventArgs e)
        {
            timessquarenewyorkcityview.Hide();
            jfkairportnewyorkcityview.Hide();
            empirestatenewyorkcityview.Hide();
            twintowersnewyorkcityview.Hide();
            centralparknewyorkcityview.Hide();
            batteryparknewyorkcityview.Hide();
            //San Francisco
            goldengatebridgesanfranciscocityview.Show();
            lombardstsanfranciscocityview.Show();
            ferrybuildingsanfranciscocityview.Show();
            coittowersanfranciscocityview.Show();
            goldengateparksanfranciscocityview.Show();
            alcatrazislsanfranciscocityview.Show();
            sanfranciscointlairportsanfranciscocityview.Show();

        }

        private void newyorkcityblendercityviewdialogboxbtn_Click(object sender, EventArgs e)
        {
            spaceviewblenderbox.Show();
            cityviewboxblender.Show();
            cityviewplayerblender.Show();
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/NYC.StartScreen.png");
            batteryparknewyorkcityview.Show();
            centralparknewyorkcityview.Show();
            twintowersnewyorkcityview.Show();
            empirestatenewyorkcityview.Show();
            jfkairportnewyorkcityview.Show();
            timessquarenewyorkcityview.Show();
            newyorkcityhallnewyorkcityview.Show();
            goldengatebridgesanfranciscocityview.Hide();
            lombardstsanfranciscocityview.Hide();
            ferrybuildingsanfranciscocityview.Hide();
            coittowersanfranciscocityview.Hide();
            goldengateparksanfranciscocityview.Hide();
            alcatrazislsanfranciscocityview.Hide();
            sanfranciscointlairportsanfranciscocityview.Hide();
        }

        private void CityViewTrackbar_Scroll(object sender, EventArgs e)
        {

        }

        private void newyorkcityhallnewyorkcityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/cityhall1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/cityhall2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/cityhall2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/cityhall2017.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/cityhall.png");

            }
        }

        private void batteryparknewyorkcityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/battery1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/battery2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/battery2013.png");


            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/battery2017.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/battery.png");

            }
        }

        private void centralparknewyorkcityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/centralpark1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/centralpark2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/centralpark2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/centralpark2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/centralpark.png");

            }
        }

        private void twintowersnewyorkcityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/twintowers1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/twintowers2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/twintowers2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/twintowers2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/twintowers.png");

            }
        }

        private void empirestatenewyorkcityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/empirest1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/empirest2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/empirest2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/empirest2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/empirest.png");

            }
        }

        private void jfkairportnewyorkcityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/jfk1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/jfk2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/jfk2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/jfk2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/jfk.png");

            }
        }

        private void timessquarenewyorkcityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/timessquare1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/timessquare2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/timessquare2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/timessquare2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/timessquare.png");

            }
        }

        private void HideProjectsBlender_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Do you want to save changes to " + textBox19.Text, "Blender", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            saveFileDialog1.ShowDialog();
            spaceviewblenderbox.Hide();
            cityviewboxblender.Hide();
            blendercityviewdialogbox.Hide();
            SpaceViewConsole.Items.Add("Exited project, " + nameboxspaceviewconsole.Text);

        }

        private void goldengatebridgesanfranciscocityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/goldengate1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/goldengate2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/goldengate2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/goldengate2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/goldengate.png");

            }
        }

        private void lombardstsanfranciscocityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/lombardst1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/lombardst2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/lombardst2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/lombardst2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/lombardst.png");

            }
        }

        private void ferrybuildingsanfranciscocityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/ferry1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/ferry2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/ferry2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/ferry2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/ferry.png");

            }
        }

        private void coittowersanfranciscocityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/coit1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/coit2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/coit2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/coit2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/coit.png");

            }
        }

        private void goldengateparksanfranciscocityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/park1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            { //Line 10000 3/12/2025
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/park2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/park2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/park2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/park.png");

            }
        }

        private void alcatrazislsanfranciscocityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/alcatraz1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/alcatraz2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/alcatraz2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/alcatraz2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/alcatraz.png");

            }
        }

        private void sanfranciscointlairportsanfranciscocityview_Click(object sender, EventArgs e)
        {
            if (CityViewTrackbar.Value == 0)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/airport1990.png");

            }
            if (CityViewTrackbar.Value == 1)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/airport2001.png");

            }
            if (CityViewTrackbar.Value == 2)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/airport2013.png");

            }
            if (CityViewTrackbar.Value == 3)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/airport2018.png");

            }
            if (CityViewTrackbar.Value == 4)
            {
                cityviewplayerblender.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/CityView/airport.png");

            }
        }

        private void zedrestoreorreset_Click(object sender, EventArgs e)
        {
            restoreorresetbox.Hide();
        }

        private void resetthispcrestoreorresetbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void turnonrestorepointsrestoreorresetbtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Restore points turned on!", "NATO OS-7", MessageBoxButtons.OK, MessageBoxIcon.Information);
            turnonrestorepointsrestoreorresetbtn.Enabled = false;
            createarestorepointrestoreorresetbtn.Enabled = true;

        }

        private void createarestorepointrestoreorresetbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string restoreDir = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\FILES\\System Active\\$Restore Points";
                string restorePointFolder = Path.Combine(tempDir, $"RestorePoint_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}_{sessionID}Logs");
                Directory.CreateDirectory(restorePointFolder);
                Directory.CreateDirectory(Path.Combine(restorePointFolder, "WebData"));
                Directory.CreateDirectory(Path.Combine(restorePointFolder, "PaintTempData"));
                Directory.CreateDirectory(Path.Combine(restorePointFolder, "MediaData"));

                string restoreFile = Path.Combine(restoreDir, $"RestorePoint_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.restorePoint");
                string systemState = GetSystemState(restorePointFolder);

                File.WriteAllText(restoreFile, systemState);
                MessageBox.Show($"Restore point created: {restoreFile}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private Dictionary<Keys, DateTime> OSkeyPressTimes = new Dictionary<Keys, DateTime>();
        private HashSet<Keys> activeKeys = new HashSet<Keys>(); // To track keys that are currently held down

        private void OnThisKeyDown(object sender, KeyEventArgs e)
        {
            //App Retrieve
            // Check for Ctrl+Shift+Alt+P combination for the entire form
            if (e.Control && e.Shift && e.Alt && e.KeyCode == Keys.P)
            {
                SaveSelectedAppAsPackage();
                e.Handled = true; // Consume the key event
            }
            // Add your existing KeyDown logic here if any
            // Example from Form2.cs snippet:
            // if (e.KeyCode == Keys.Up) { /* ... */ }
            // etc.
            //App Retrieve
            string keyInfo = $"Key Pressed: {e.KeyCode}";
            appstatisticsgroup.Items.Add(keyInfo);
            // Only log "Key Pressed" if the key is not already being tracked
            if (!OSkeyPressTimes.ContainsKey(e.KeyCode))
            {
                OSkeyPressTimes[e.KeyCode] = DateTime.Now; // Store press time
                activeKeys.Add(e.KeyCode); // Mark key as active
                appstatisticsgroup.Items.Add($"Key Pressed: {e.KeyCode}");
            }
            //Debug Mode
            // Add key to the queue
            keySequence.Enqueue(e.KeyCode);

            // Keep only the last N keys (same length as konamiCode)
            if (keySequence.Count > konamiCode.Length)
                keySequence.Dequeue();

            // Check if the sequence matches
            if (MatchesKonamiCode())
            {
                if (!isDebugModeAvalible)
                {
                    MessageBox.Show("Enabled");
                    EnableDragging(); // Enable dragging if sandbox is disabled
                }
                else
                {
                    // Disable dragging code
                    isDebugModeAvalible = false; // Set the flag to false to disable dragging
                }
                keySequence.Clear(); // Reset after activation
            }
            //Debug Mode
        }
        //Debug Mode
        private bool MatchesKonamiCode()
        {
            return keySequence.Count == konamiCode.Length && keySequence.ToArray().SequenceEqual(konamiCode);
        }
        //Debug Mode
        private void OnThisKeyUp(object sender, KeyEventArgs e)
        {
            if (OSkeyPressTimes.ContainsKey(e.KeyCode))
            {
                DateTime pressTime = OSkeyPressTimes[e.KeyCode];
                TimeSpan duration = DateTime.Now - pressTime;

                string logEntry = $"Key {e.KeyCode} held for {duration.TotalMilliseconds} ms";
                appstatisticsgroup.Items.Add(logEntry);

                // Remove from tracking
                OSkeyPressTimes.Remove(e.KeyCode);
                activeKeys.Remove(e.KeyCode);
            }
        }
        private bool isDebugModeEnabled = false;
        private void button40_Click(object sender, EventArgs e)
        {
            excelbox.Hide();
            button42.Enabled = false;

        }

        private void blankworkbookexcel_Click(object sender, EventArgs e)
        {
            excelgridview.Show();
            button42.Enabled = true;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void button42_Click(object sender, EventArgs e)
        {
            excelgridview.Hide();
            MessageBox.Show("Saved File", "Excel");
            button42.Enabled = false;
        }

        private void uploaddocumentword_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Upload File"
            };
        }

        private void savedocumentword_Click(object sender, EventArgs e)
        {
            txtboxpublisher.Hide();
            numericUpDown2.Hide();
        }

        private void blankdocumentword_Click(object sender, EventArgs e)
        {
            txtboxpublisher.Show();
            numericUpDown2.Show();
        }

        private void hidemicrosoftwordbtn_Click(object sender, EventArgs e)
        {
            microsoftwordbox.Hide();
            txtboxpublisher.Hide();
            numericUpDown2.Hide();

        }

        private void hidepowerpoint_Click(object sender, EventArgs e)
        {
            powerpointbox.Hide();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Title = "Open Powerpoint File",

            };
        }

        private void fancyitem1_Click(object sender, EventArgs e)
        {
            powerpointpresentationbox.Show();
            splitContainer2.Hide();
        }

        private void saveToolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save PowerPoint Document",
                FileName = "Presentation1.pptx",
            };

            MessageBox.Show("Saved File!", "Microsoft PowerPoint 2013");
            powerpointpresentationbox.Hide();
        }
        private void Slide1PowerPoint2013_Click(object sender, EventArgs e)
        {
            SelectedSlidePowerPoint2013.Text = "Slide 1";
            splitContainer2.Show();
            powerpointslide = 1;
            textBox25.Text = slide1sub;
            textBox26.Text = slide1header;
            SelectedSlidePowerPoint2013.BackgroundImage = slide1image;


        }
        private void Slide2PowerPoint2013_Click(object sender, EventArgs e)
        {
            SelectedSlidePowerPoint2013.Text = "Slide 2";
            splitContainer2.Show();
            powerpointslide = 2;
            textBox25.Text = slide2sub;
            textBox26.Text = slide2header;
            SelectedSlidePowerPoint2013.BackgroundImage = slide2image;

        }
        private void Slide3PowerPoint2013_Click(object sender, EventArgs e)
        {
            SelectedSlidePowerPoint2013.Text = "Slide 3";
            splitContainer2.Show();
            powerpointslide = 3;
            textBox25.Text = slide3sub;
            textBox26.Text = slide3header;
            SelectedSlidePowerPoint2013.BackgroundImage = slide3image;

        }
        private void printToolStripButton3_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog
            {

            };
        }


        private void cutToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void ChangeImageOfSlidePowerPoint2013Btn_Click(object sender, EventArgs e)
        {
            if (powerpointslide == 1)
            {
                openFileDialog2.ShowDialog();
                slide1image = Image.FromFile(openFileDialog2.FileName);
                SelectedSlidePowerPoint2013.BackgroundImage = slide1image;


            }
            if (powerpointslide == 2)
            {
                openFileDialog2.ShowDialog();
                slide2image = Image.FromFile(openFileDialog2.FileName);
                SelectedSlidePowerPoint2013.BackgroundImage = slide2image;

            }
            if (powerpointslide == 3)
            {
                openFileDialog2.ShowDialog();
                slide3image = Image.FromFile(openFileDialog2.FileName);
                SelectedSlidePowerPoint2013.BackgroundImage = slide3image;

            }
        }

        private void Slide1PowerPoint2013_Enter(object sender, EventArgs e)
        {

        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveslidepowerpoint2013_Click(object sender, EventArgs e)
        {
            if (powerpointslide == 1)
            {
                slide1header = textBox26.Text;
                slide1sub = textBox25.Text;
                SelectedSlidePowerPoint2013.BackgroundImage = slide1image;


            }
            if (powerpointslide == 2)
            {
                slide2header = textBox26.Text;
                slide2sub = textBox25.Text;
                SelectedSlidePowerPoint2013.BackgroundImage = slide2image;

            }
            if (powerpointslide == 3)
            {
                slide3header = textBox26.Text;
                slide3sub = textBox25.Text;
                SelectedSlidePowerPoint2013.BackgroundImage = slide3image;

            }
        }

        private void windowspublisherlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            microsoftpublisherapp.Show();
            PublisherDesignerBox.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open other Publications",
            };
        }

        private void hidemicrosoftpublisherapp_Click(object sender, EventArgs e)
        {
            microsoftpublisherapp.Hide();
        }

        private void Blank_8_5x11MicrosoftPublisherImage_Click(object sender, EventArgs e)
        {
            PublisherDesignerBox.Show();
            publisherDocument1.Show();
            publisherDocument2.Hide();
            publisherDocument3.Hide();

        }

        private void Blank_11x8_5MicrosoftPublisherImage_Click(object sender, EventArgs e)
        {
            PublisherDesignerBox.Show();
            publisherDocument1.Hide();
            publisherDocument2.Show();
            publisherDocument3.Hide();

        }

        private void EventFlyerMicrosoftPublisherImage_Click(object sender, EventArgs e)
        {
            PublisherDesignerBox.Show();
            publisherDocument3.Show();
            publisherDocument1.Hide();
            publisherDocument2.Hide();
        }

        private void textboxbuttonpublisher_Click(object sender, EventArgs e)
        {
            this.txtboxpublisher.Font = new Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtboxpublisher.Location = new Point(2, 28);
            this.txtboxpublisher.Name = "wordtxt";
            this.txtboxpublisher.Size = new Size(46, 111);
            this.txtboxpublisher.TabIndex = 27;
            this.txtboxpublisher.Text = "Text Box";
        }

        private void rulerPublisher_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen blackPen = new Pen(Color.Black, 1);
            Font font = new Font("Arial", 8);
            Brush textBrush = Brushes.Black;

            // Define the ruler dimensions
            int rulerHeight = rulerPublisher.Height;
            int rulerWidth = rulerPublisher.Width;
            int majorTickHeight = 15;
            int minorTickHeight = 5;

            // Draw the horizontal ruler
            for (int i = 0; i <= rulerWidth; i += 10) // Increment every 10 pixels
            {
                if (i % 50 == 0) // Major tick every 50 pixels
                {
                    g.DrawLine(blackPen, i, 0, i, majorTickHeight);
                    g.DrawString((i / 10).ToString(), font, textBrush, i - 5, majorTickHeight + 2); // Label below tick
                }
                else // Minor tick
                {
                    g.DrawLine(blackPen, i, 0, i, minorTickHeight);
                }
            }
        }

        private void HideVisualStudioCodeMicrosoft_Click(object sender, EventArgs e)
        {
            VisualStudioCodeBoxMicrosoft.Hide();
        }

        private void usageconsolelinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            usagechartbox.Show();

        }

        private void hideusagechart_Click(object sender, EventArgs e)
        {
            usagechartbox.Hide();
        }

        private void helpToolStripButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usage Chart Compiler\nInstalled with system from NATO OS-1\nTerminal commands: launch/ ntosknl.app", "Usage Chart", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void printToolStripButton4_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog
            {

            };
        }

        private void saveToolStripButton3_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
        }

        private void openToolStripButton4_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void VisualStudioLinkBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void enable3DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            enable3DToolStripMenuItem.Enabled = false;
            enable2DToolStripMenuItem.Enabled = true;

        }

        private void enable2DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
            enable2DToolStripMenuItem.Enabled = false;
            enable3DToolStripMenuItem.Enabled = true;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Refresh Succesful", "Usage Chart", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //OpenSlate
        //OpenSlate Memory
        //Document Name
        string documentnameopenslatecheckedlistbox;
        string documentnameopenslatetodolistbox;
        string documentnameopenslatereminderlistbox;

        //Checked List
        string mem1openslatecheckedlistbox;
        string mem2openslatecheckedlistbox;
        string mem3openslatecheckedlistbox;
        string mem4openslatecheckedlistbox;
        string mem5openslatecheckedlistbox;
        string mem6openslatecheckedlistbox;
        string mem7openslatecheckedlistbox;
        string mem8openslatecheckedlistbox;
        string mem9openslatecheckedlistbox;
        string mem10openslatecheckedlistbox;
        string mem11openslatecheckedlistbox;
        string mem12openslatecheckedlistbox;
        string mem13openslatecheckedlistbox;
        //TODO List
        string mem1openslatetodolistbox;
        string mem2openslatetodolistbox;
        string mem3openslatetodolistbox;
        string mem4openslatetodolistbox;
        string mem5openslatetodolistbox;
        string mem6openslatetodolistbox;
        string mem7openslatetodolistbox;
        string mem8openslatetodolistbox;
        string mem9openslatetodolistbox;
        string mem10openslatetodolistbox;
        string mem11openslatetodolistbox;
        string mem12openslatetodolistbox;
        string mem13openslatetodolistbox;
        //Reminder List
        string mem1openslatereminderlistbox;
        string mem2openslatereminderlistbox;
        string mem3openslatereminderlistbox;
        string mem4openslatereminderlistbox;
        string mem5openslatereminderlistbox;
        string mem6openslatereminderlistbox;
        string mem7openslatereminderlistbox;
        string mem8openslatereminderlistbox;
        string mem9openslatereminderlistbox;
        string mem10openslatereminderlistbox;
        string mem11openslatereminderlistbox;
        string mem12openslatereminderlistbox;
        string mem13openslatereminderlistbox;
        int selectedOpenSlate = 0;

        private void openslatelinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenSlateBox.Show();
            openslatelistbox.Hide();
        }

        private void checkedlistbtnopenslate_Click(object sender, EventArgs e)
        {
            selectedOpenSlate = 1;
            OpenSlateDocumentName.Text = "Untitled Checked List Document";
            openslatelistbox.Show();
            OpenSlateCheckedList.Show();
            string1openslatecheckedlistbox.Show();
            string2openslatecheckedlistbox.Show();
            string3openslatecheckedlistbox.Show();
            string4openslatecheckedlistbox.Show();
            string5openslatecheckedlistbox.Show();
            string6openslatecheckedlistbox.Show();
            string7openslatecheckedlistbox.Show();
            string8openslatecheckedlistbox.Show();
            string9openslatecheckedlistbox.Show();
            string10openslatecheckedlistbox.Show();
            string11openslatecheckedlistbox.Show();
            string12openslatecheckedlistbox.Show();
            string13openslatecheckedlistbox.Show();

            //Clear for a new file
            string1openslatecheckedlistbox.Text = "";
            string2openslatecheckedlistbox.Text = "";
            string3openslatecheckedlistbox.Text = "";
            string4openslatecheckedlistbox.Text = "";
            string5openslatecheckedlistbox.Text = "";
            string6openslatecheckedlistbox.Text = "";
            string7openslatecheckedlistbox.Text = "";
            string8openslatecheckedlistbox.Text = "";
            string9openslatecheckedlistbox.Text = "";
            string10openslatecheckedlistbox.Text = "";
            string11openslatecheckedlistbox.Text = "";
            string12openslatecheckedlistbox.Text = "";
            string13openslatecheckedlistbox.Text = "";

        }
        private void saveopenslate_Click(object sender, EventArgs e)
        {
            //Check Box
            if (selectedOpenSlate == 1)
            {
                documentnameopenslatecheckedlistbox = OpenSlateDocumentName.Text;
                avaliblenotesopenslate.Text = documentnameopenslatecheckedlistbox;
                mem1openslatecheckedlistbox = string1openslatecheckedlistbox.Text;
                mem2openslatecheckedlistbox = string2openslatecheckedlistbox.Text;
                mem3openslatecheckedlistbox = string3openslatecheckedlistbox.Text;
                mem4openslatecheckedlistbox = string4openslatecheckedlistbox.Text;
                mem5openslatecheckedlistbox = string5openslatecheckedlistbox.Text;
                mem6openslatecheckedlistbox = string6openslatecheckedlistbox.Text;
                mem7openslatecheckedlistbox = string7openslatecheckedlistbox.Text;
                mem8openslatecheckedlistbox = string8openslatecheckedlistbox.Text;
                mem9openslatecheckedlistbox = string9openslatecheckedlistbox.Text;
                mem10openslatecheckedlistbox = string10openslatecheckedlistbox.Text;
                mem11openslatecheckedlistbox = string11openslatecheckedlistbox.Text;
                mem12openslatecheckedlistbox = string12openslatecheckedlistbox.Text;
                mem13openslatecheckedlistbox = string13openslatecheckedlistbox.Text;
            }
            if (selectedOpenSlate == 2)
            {
                documentnameopenslatetodolistbox = OpenSlateDocumentName.Text;
                avaliblenotesopenslate.Text = documentnameopenslatetodolistbox;

                mem1openslatetodolistbox = string1openslatecheckedlistbox.Text;
                mem2openslatetodolistbox = string2openslatecheckedlistbox.Text;
                mem3openslatetodolistbox = string3openslatecheckedlistbox.Text;
                mem4openslatetodolistbox = string4openslatecheckedlistbox.Text;
                mem5openslatetodolistbox = string5openslatecheckedlistbox.Text;
                mem6openslatetodolistbox = string6openslatecheckedlistbox.Text;
                mem7openslatetodolistbox = string7openslatecheckedlistbox.Text;
                mem8openslatetodolistbox = string8openslatecheckedlistbox.Text;
                mem9openslatetodolistbox = string9openslatecheckedlistbox.Text;
                mem10openslatetodolistbox = string10openslatecheckedlistbox.Text;
                mem11openslatetodolistbox = string11openslatecheckedlistbox.Text;
                mem12openslatetodolistbox = string12openslatecheckedlistbox.Text;
                mem13openslatetodolistbox = string13openslatecheckedlistbox.Text;
            }
            if (selectedOpenSlate == 3)
            {
                documentnameopenslatereminderlistbox = OpenSlateDocumentName.Text;
                avaliblenotesopenslate.Text = documentnameopenslatereminderlistbox;
                mem1openslatereminderlistbox = string1openslatecheckedlistbox.Text;
                mem2openslatereminderlistbox = string2openslatecheckedlistbox.Text;
                mem3openslatereminderlistbox = string3openslatecheckedlistbox.Text;
                mem4openslatereminderlistbox = string4openslatecheckedlistbox.Text;
                mem5openslatereminderlistbox = string5openslatecheckedlistbox.Text;
                mem6openslatereminderlistbox = string6openslatecheckedlistbox.Text;
                mem7openslatereminderlistbox = string7openslatecheckedlistbox.Text;
                mem8openslatereminderlistbox = string8openslatecheckedlistbox.Text;
                mem9openslatereminderlistbox = string9openslatecheckedlistbox.Text;
                mem10openslatereminderlistbox = string10openslatecheckedlistbox.Text;
                mem11openslatereminderlistbox = string11openslatecheckedlistbox.Text;
                mem12openslatereminderlistbox = string12openslatecheckedlistbox.Text;
                mem13openslatereminderlistbox = string13openslatecheckedlistbox.Text;
            }
            if (selectedOpenSlate == 0)
            {
                MessageBox.Show("Unable to save. Please create a file first before attempting to save.", "Open Slate", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void avaliblenotesopenslate_Click(object sender, EventArgs e)
        {


            //Load Storage From Text

            if (selectedOpenSlate == 1)
            {
                OpenSlateDocumentName.Text = documentnameopenslatecheckedlistbox;
                string1openslatecheckedlistbox.Text = mem1openslatecheckedlistbox;
                string2openslatecheckedlistbox.Text = mem2openslatecheckedlistbox;
                string3openslatecheckedlistbox.Text = mem3openslatecheckedlistbox;
                string4openslatecheckedlistbox.Text = mem4openslatecheckedlistbox;
                string5openslatecheckedlistbox.Text = mem5openslatecheckedlistbox;
                string6openslatecheckedlistbox.Text = mem6openslatecheckedlistbox;
                string7openslatecheckedlistbox.Text = mem7openslatecheckedlistbox;
                string8openslatecheckedlistbox.Text = mem8openslatecheckedlistbox;
                string9openslatecheckedlistbox.Text = mem9openslatecheckedlistbox;
                string10openslatecheckedlistbox.Text = mem10openslatecheckedlistbox;
                string11openslatecheckedlistbox.Text = mem11openslatecheckedlistbox;
                string12openslatecheckedlistbox.Text = mem12openslatecheckedlistbox;
                string13openslatecheckedlistbox.Text = mem13openslatecheckedlistbox;
            }
            if (selectedOpenSlate == 2)
            {
                OpenSlateDocumentName.Text = documentnameopenslatetodolistbox;
                string1openslatecheckedlistbox.Text = mem1openslatetodolistbox;
                string2openslatecheckedlistbox.Text = mem2openslatetodolistbox;
                string3openslatecheckedlistbox.Text = mem3openslatetodolistbox;
                string4openslatecheckedlistbox.Text = mem4openslatetodolistbox;
                string5openslatecheckedlistbox.Text = mem5openslatetodolistbox;
                string6openslatecheckedlistbox.Text = mem6openslatetodolistbox;
                string7openslatecheckedlistbox.Text = mem7openslatetodolistbox;
                string8openslatecheckedlistbox.Text = mem8openslatetodolistbox;
                string9openslatecheckedlistbox.Text = mem9openslatetodolistbox;
                string10openslatecheckedlistbox.Text = mem10openslatetodolistbox;
                string11openslatecheckedlistbox.Text = mem11openslatetodolistbox;
                string12openslatecheckedlistbox.Text = mem12openslatetodolistbox;
                string13openslatecheckedlistbox.Text = mem13openslatetodolistbox;

            }
            if (selectedOpenSlate == 3)
            {
                OpenSlateDocumentName.Text = documentnameopenslatereminderlistbox;
                string1openslatecheckedlistbox.Text = mem1openslatereminderlistbox;
                string2openslatecheckedlistbox.Text = mem2openslatereminderlistbox;
                string3openslatecheckedlistbox.Text = mem3openslatereminderlistbox;
                string4openslatecheckedlistbox.Text = mem4openslatereminderlistbox;
                string5openslatecheckedlistbox.Text = mem5openslatereminderlistbox;
                string6openslatecheckedlistbox.Text = mem6openslatereminderlistbox;
                string7openslatecheckedlistbox.Text = mem7openslatereminderlistbox;
                string8openslatecheckedlistbox.Text = mem8openslatereminderlistbox;
                string9openslatecheckedlistbox.Text = mem9openslatereminderlistbox;
                string10openslatecheckedlistbox.Text = mem10openslatereminderlistbox;
                string11openslatecheckedlistbox.Text = mem11openslatereminderlistbox;
                string12openslatecheckedlistbox.Text = mem12openslatereminderlistbox;
                string13openslatecheckedlistbox.Text = mem13openslatereminderlistbox;
            }
            if (selectedOpenSlate == 0)
            {
                MessageBox.Show("Unable to load. No file available. Please create a file before attempting to load.", "Open Slate", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
        }

        private void CloseOpenSlate_Click_1(object sender, EventArgs e)
        {
            OpenSlateBox.Hide();
        }

        private void todolistbtnopenslate_Click(object sender, EventArgs e)
        {
            OpenSlateDocumentName.Text = "Untitled TODO List Document";
            selectedOpenSlate = 2;
            openslatelistbox.Show();
            OpenSlateCheckedList.Show();
            string1openslatecheckedlistbox.Show();
            string2openslatecheckedlistbox.Show();
            string3openslatecheckedlistbox.Show();
            string4openslatecheckedlistbox.Show();
            string5openslatecheckedlistbox.Show();
            string6openslatecheckedlistbox.Show();
            string7openslatecheckedlistbox.Show();
            string8openslatecheckedlistbox.Show();
            string9openslatecheckedlistbox.Show();
            string10openslatecheckedlistbox.Show();
            string11openslatecheckedlistbox.Show();
            string12openslatecheckedlistbox.Show();
            string13openslatecheckedlistbox.Show();

            //Clear for a new file
            string1openslatecheckedlistbox.Text = "";
            string2openslatecheckedlistbox.Text = "";
            string3openslatecheckedlistbox.Text = "";
            string4openslatecheckedlistbox.Text = "";
            string5openslatecheckedlistbox.Text = "";
            string6openslatecheckedlistbox.Text = "";
            string7openslatecheckedlistbox.Text = "";
            string8openslatecheckedlistbox.Text = "";
            string9openslatecheckedlistbox.Text = "";
            string10openslatecheckedlistbox.Text = "";
            string11openslatecheckedlistbox.Text = "";
            string12openslatecheckedlistbox.Text = "";
            string13openslatecheckedlistbox.Text = "";
        }

        private void reminderlistbtnopenslate_Click(object sender, EventArgs e)
        {
            selectedOpenSlate = 3;
            OpenSlateDocumentName.Text = "Untitled Reminder List Document";

            openslatelistbox.Show();
            OpenSlateCheckedList.Show();
            string1openslatecheckedlistbox.Show();
            string2openslatecheckedlistbox.Show();
            string3openslatecheckedlistbox.Show();
            string4openslatecheckedlistbox.Show();
            string5openslatecheckedlistbox.Show();
            string6openslatecheckedlistbox.Show();
            string7openslatecheckedlistbox.Show();
            string8openslatecheckedlistbox.Show();
            string9openslatecheckedlistbox.Show();
            string10openslatecheckedlistbox.Show();
            string11openslatecheckedlistbox.Show();
            string12openslatecheckedlistbox.Show();
            string13openslatecheckedlistbox.Show();

            //Clear for a new file
            string1openslatecheckedlistbox.Text = "";
            string2openslatecheckedlistbox.Text = "";
            string3openslatecheckedlistbox.Text = "";
            string4openslatecheckedlistbox.Text = "";
            string5openslatecheckedlistbox.Text = "";
            string6openslatecheckedlistbox.Text = "";
            string7openslatecheckedlistbox.Text = "";
            string8openslatecheckedlistbox.Text = "";
            string9openslatecheckedlistbox.Text = "";
            string10openslatecheckedlistbox.Text = "";
            string11openslatecheckedlistbox.Text = "";
            string12openslatecheckedlistbox.Text = "";
            string13openslatecheckedlistbox.Text = "";
        }

        private void uploadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openBackgroundImageNatoContextMenuStripDialog.ShowDialog();
            this.BackgroundImage = Image.FromFile(openBackgroundImageNatoContextMenuStripDialog.FileName);
            this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void takeScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap screenshot = new Bitmap(this.Width, this.Height);

            this.DrawToBitmap(screenshot, new Rectangle(0, 0, this.Width, this.Height));
            Random r = new Random();
            int a = r.Next(2, 1000000);
            screenshot.Save("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Pictures/Screen Captures/screenshot" + a.ToString() + ".png", ImageFormat.Png);
            newscrnshot = newscrnshot + 1;

        }

        private void HideRecordingPCBoxBtn_Click(object sender, EventArgs e)
        {
            RecordBox.Hide();
        }

        private void RightClickMenuNATO_Opening(object sender, CancelEventArgs e)
        {

        }

        private void recordVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordBox.Show();
        }

        private void startrecordingnatobtn_Click(object sender, EventArgs e)
        {
            // Path to the output video
            string outputFile = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/FILES/Videos/screenrecording.mp4";

            // Define the FFmpeg command
            string ffmpegArgs = $"-f gdigrab -i desktop -framerate 30 -vcodec libx264 -preset ultrafast {outputFile}";

            // Start FFmpeg process
            _ffmpegProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg", // Make sure FFmpeg is installed and added to PATH
                    Arguments = ffmpegArgs,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false
                }
            };

            try
            {
                _ffmpegProcess.Start();
                MessageBox.Show("Recording started.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting recording: {ex.Message}");
            }
        }

        private void stoprecordingNATOBtn_Click(object sender, EventArgs e)
        {
            if (_ffmpegProcess != null && !_ffmpegProcess.HasExited)
            {
                _ffmpegProcess.StandardInput.Write("q"); // Stop FFmpeg recording
                _ffmpegProcess.WaitForExit();
                _ffmpegProcess.Dispose();
                _ffmpegProcess = null;
                MessageBox.Show("Recording stopped.");
            }
            else
            {
                MessageBox.Show("No recording in progress.");
            }
        }

        //VMWARE

        private void hidevmwarebox_Click(object sender, EventArgs e)
        {
            vmwarebox.Hide();
        }

        private void helpToolStripButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("©VMWARE WORKSTATION, Designed for NATO-OS 7\nServer: a9B387R948379-RGW83EQ9-a8J9E4T8Q6G", "About VMware Workstation for NATO OS 7", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void saveToolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void showinusagechartvmware_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            usagechartbox.Show();
        }

        private void slot4virtualmachinebuttonvmwareworkstationfornatoos7_Click(object sender, EventArgs e)
        {
            // Open a file dialog to select an ISO file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "ISO files (*.iso)|*.iso";
                openFileDialog.Title = "Select an ISO File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox32.Text = openFileDialog.FileName; // Display the selected path
                }
            }
            string isoPath = textBox32.Text;

            if (!File.Exists(isoPath))
            {
                MessageBox.Show("Please select a valid ISO file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Use PowerShell to mount the ISO
                string command = $"Mount-DiskImage -ImagePath \"{isoPath}\"";
                ExecutePowerShellCommand(command);

                MessageBox.Show("ISO mounted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error mounting ISO: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void slot3virtualmachinebuttonvmwareworkstationfornatoos7_Click(object sender, EventArgs e)
        {
            // Open a file dialog to select an ISO file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "ISO files (*.iso)|*.iso";
                openFileDialog.Title = "Select an ISO File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox32.Text = openFileDialog.FileName; // Display the selected path
                }
            }
            string isoPath = textBox32.Text;

            if (!File.Exists(isoPath))
            {
                MessageBox.Show("Please select a valid ISO file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Use PowerShell to mount the ISO
                string command = $"Mount-DiskImage -ImagePath \"{isoPath}\"";
                ExecutePowerShellCommand(command);

                MessageBox.Show("ISO mounted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error mounting ISO: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void slot2virtualmachinebuttonvmwareworkstationfornatoos7_Click(object sender, EventArgs e)
        {
            // Open a file dialog to select an ISO file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "ISO files (*.iso)|*.iso";
                openFileDialog.Title = "Select an ISO File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox32.Text = openFileDialog.FileName; // Display the selected path
                }
            }
            string isoPath = textBox32.Text;

            if (!File.Exists(isoPath))
            {
                MessageBox.Show("Please select a valid ISO file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Use PowerShell to mount the ISO
                string command = $"Mount-DiskImage -ImagePath \"{isoPath}\"";
                ExecutePowerShellCommand(command);

                MessageBox.Show("ISO mounted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error mounting ISO: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void slot1virtualmachinebuttonvmwareworkstationfornatoos7_Click(object sender, EventArgs e)
        {
            // Open a file dialog to select an ISO file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "ISO files (*.iso)|*.iso";
                openFileDialog.Title = "Select an ISO File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox32.Text = openFileDialog.FileName; // Display the selected path
                }
            }
            string isoPath = textBox32.Text;

            if (!File.Exists(isoPath))
            {
                MessageBox.Show("Please select a valid ISO file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Use PowerShell to mount the ISO
                string command = $"Mount-DiskImage -ImagePath \"{isoPath}\"";
                ExecutePowerShellCommand(command);

                MessageBox.Show("ISO mounted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error mounting ISO: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExecutePowerShellCommand(string command)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-Command {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Verb = "runas" // Requires administrator rights
                }
            };

            process.Start();
            process.WaitForExit();

            string error = process.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
        }

        private void AllAppsBox_Enter(object sender, EventArgs e)
        {

        }



        private void weatherlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            weatherBox.Show();
        }

        private void hideWeather_Click(object sender, EventArgs e)
        {
            weatherBox.Hide();
        }

        private void natopasswordmanagerbox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            changepasswordboxpasswordmanager.Hide();
            // Create a custom message box with password input
            if (passwordManagerLogon == "")
            {


            }
            else
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Enter Password";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Password:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260, PasswordChar = '*' };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        string enteredPassword = textBox.Text;
                        if (enteredPassword == passwordManagerLogon)
                        {
                            passwordmanagerbox.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void hidepasswordmanagerbox_Click(object sender, EventArgs e)
        {
            passwordmanagerbox.Hide();
        }

        private void submitchangepasswordbtnchangepasswordboxpasswordmanager_Click(object sender, EventArgs e)
        {
            passwordManagerLogon = changepasswordtxtpasswordmanager.Text;
            if (changepasswordtxtpasswordmanager.Text == beforePassword)
            {
                MessageBox.Show("Cannot choose the before password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                passwordManagerLogon = changepasswordtxtpasswordmanager.Text;
                beforePassword = changepasswordtxtpasswordmanager.Text;
            }
        }

        private void changepasswordpasswordmanager_Click(object sender, EventArgs e)
        {
            changepasswordboxpasswordmanager.Show();
        }

        private void registryeditorlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registryeditorbox.Show();
        }

        private void hideregistryeditor_Click(object sender, EventArgs e)
        {
            registryeditorbox.Hide();
        }

        private void registryEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registryeditorbox.Show();
        }

        private void openToolStripButton6_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Registry files (*.regedit)|*.regedit";
                openFileDialog.Title = "Registry Editor";
            };

        }

        private void saveToolStripButton5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Registry files (*.regedit)|*.regedit";
                openFileDialog.Title = "Save Registry File";
            };
        }

        private void helpToolStripButton6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Registry Editor\nInstalled with system from NATO OS-1 (2000 E)\nTerminal commands: launch/ regedit.app", "Registry Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void openreadmefilenatoos7linkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Path to the document
            string filePath = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/readme.dot";

            try
            {
                // Open the file with the default application
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true // Required to open with default program
                });
            }
            catch (Exception ex)
            {
                // Handle errors, e.g., file not found or no associated program
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void widgetslinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            widgetsboxselect.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ClockWidgetNATO.Invalidate(); // Force repaint every second

        }

        private void ClockWidgetNATO_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Get center and radius
            int centerX = ClockWidgetNATO.Width / 2;
            int centerY = ClockWidgetNATO.Height / 2;
            int radius = Math.Min(centerX, centerY) - 10;

            // Draw clock face
            g.FillEllipse(Brushes.White, centerX - radius, centerY - radius, radius * 2, radius * 2);
            g.DrawEllipse(Pens.Black, centerX - radius, centerY - radius, radius * 2, radius * 2);

            // Draw hour marks
            for (int i = 0; i < 12; i++)
            {
                double angle = Math.PI / 6 * i; // 30 degrees
                int x1 = centerX + (int)(radius * 0.9 * Math.Cos(angle));
                int y1 = centerY + (int)(radius * 0.9 * Math.Sin(angle));
                int x2 = centerX + (int)(radius * 0.8 * Math.Cos(angle));
                int y2 = centerY + (int)(radius * 0.8 * Math.Sin(angle));
                g.DrawLine(Pens.Black, x1, y1, x2, y2);
            }

            // Get current time
            DateTime now = DateTime.Now;
            int hours = now.Hour % 12;
            int minutes = now.Minute;
            int seconds = now.Second;

            // Calculate angles for hands
            double hourAngle = Math.PI / 6 * hours + Math.PI / 360 * minutes;
            double minuteAngle = Math.PI / 30 * minutes + Math.PI / 1800 * seconds;
            double secondAngle = Math.PI / 30 * seconds;

            // Draw hour hand
            int hourX = centerX + (int)(radius * 0.5 * Math.Cos(hourAngle - Math.PI / 2));
            int hourY = centerY + (int)(radius * 0.5 * Math.Sin(hourAngle - Math.PI / 2));
            g.DrawLine(new Pen(Color.Black, 4), centerX, centerY, hourX, hourY);

            // Draw minute hand
            int minuteX = centerX + (int)(radius * 0.7 * Math.Cos(minuteAngle - Math.PI / 2));
            int minuteY = centerY + (int)(radius * 0.7 * Math.Sin(minuteAngle - Math.PI / 2));
            g.DrawLine(new Pen(Color.Black, 2), centerX, centerY, minuteX, minuteY);

            // Draw second hand
            int secondX = centerX + (int)(radius * 0.9 * Math.Cos(secondAngle - Math.PI / 2));
            int secondY = centerY + (int)(radius * 0.9 * Math.Sin(secondAngle - Math.PI / 2));
            g.DrawLine(new Pen(Color.Red, 1), centerX, centerY, secondX, secondY);

            // Draw clock center
            g.FillEllipse(Brushes.Black, centerX - 5, centerY - 5, 10, 10);
        }

        private void ClockWidgetNATO_MarginChanged(object sender, EventArgs e)
        {

        }
        private void ClockWidgetNATO_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void ClockWidgetNATO_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                ClockWidgetNATO.Left += e.X - dragStartPoint.X;
                ClockWidgetNATO.Top += e.Y - dragStartPoint.Y;
            }
        }

        private void ClockWidgetNATO_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            string rssUrl = "https://www.cbsnews.com/latest/rss/main"; // Replace with a valid RSS feed URL
            listBoxFeeds.Items.Clear();
            listBoxFeeds.Items.Add("Loading RSS feed...");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string rssData = await client.GetStringAsync(rssUrl);
                    using (StringReader stringReader = new StringReader(rssData))
                    {
                        using (XmlReader xmlReader = XmlReader.Create(stringReader))
                        {
                            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
                            if (feed != null)
                            {
                                listBoxFeeds.Items.Clear();
                                foreach (var item in feed.Items)
                                {
                                    listBoxFeeds.Items.Add(item.Title.Text);
                                }
                            }
                            else
                            {
                                listBoxFeeds.Items.Clear();
                                listBoxFeeds.Items.Add("Unable to load RSS feed.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listBoxFeeds.Items.Clear();
                listBoxFeeds.Items.Add("Error loading RSS feed:");
                listBoxFeeds.Items.Add(ex.Message);
            }

        }

        private void MessageCallBack(IAsyncResult aResult)
        {
            try
            {
                byte[] recievedData = new byte[1500];
                recievedData = (byte[])aResult.AsyncState;

                ASCIIEncoding aEncoding = new ASCIIEncoding();
                string recievedMessage = aEncoding.GetString(recievedData);


                listMessage.Items.Add("Friend: " + recievedMessage);

                buffer = new byte[1500];

                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RSSWidgetNATO_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void RSSWidgetNATO_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                RSSWidgetNATO.Left += e.X - dragStartPoint.X;
                RSSWidgetNATO.Top += e.Y - dragStartPoint.Y;
            }
        }



        private void RSSWidgetNATO_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }
        private void MediaWidgetNATO_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void MediaWidgetNATO_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                MediaWidgetNATO.Left += e.X - dragStartPoint.X;
                MediaWidgetNATO.Top += e.Y - dragStartPoint.Y;
            }
        }



        private void MediaWidgetNATO_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }
        private void PuzzlePictureWidgetNATO_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void PuzzlePictureWidgetNATO_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                PuzzlePictureWidgetNATO.Left += e.X - dragStartPoint.X;
                PuzzlePictureWidgetNATO.Top += e.Y - dragStartPoint.Y;
            }
        }
        private void stickynotewidgetnato_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }
        private void stickynotewidgetnato_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void stickynotewidgetnato_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                stickynotewidgetnato.Left += e.X - dragStartPoint.X;
                stickynotewidgetnato.Top += e.Y - dragStartPoint.Y;
            }
        }
        private void Calculator_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }
        private void Calculator_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void Calculator_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                Calculator.Left += e.X - dragStartPoint.X;
                Calculator.Top += e.Y - dragStartPoint.Y;
            }
        }
        private void CalendarWidgetNATO_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }
        private void CalendarWidgetNATO_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void CalendarWidgetNATO_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                CalendarWidgetNATO.Left += e.X - dragStartPoint.X;
                CalendarWidgetNATO.Top += e.Y - dragStartPoint.Y;
            }
        }
        //Nato Designer Combo Box
        private void NATODesignerComboBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }
        private void NATODesignerComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void NATODesignerComboBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                NATODesignerComboBox.Left += e.X - dragStartPoint.X;
                NATODesignerComboBox.Top += e.Y - dragStartPoint.Y;
            }
        }
        private void PuzzlePictureWidgetNATO_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }
        private void UploadMediaWidgetBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            axWindowsMediaPlayer5.URL = openFileDialog1.FileName;
        }

        private void UploadMediaWidget_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            axWindowsMediaPlayer5.URL = openFileDialog1.FileName;
        }
        private void InitializePuzzlePictureWidget()
        {
            // Configure PuzzlePictureWidgetNATO GroupBox
            PuzzlePictureWidgetNATO.Text = "Puzzle Picture";
            PuzzlePictureWidgetNATO.Width = gridSize * tileSize + 20;
            PuzzlePictureWidgetNATO.Height = gridSize * tileSize + 20;

            // Load and split the image
            originalImage = new Bitmap("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/New.SystemIcons/PuzzlePicture1.jpg"); // Replace with your image path
            SplitImageAndCreateTiles();

            // Shuffle tiles
            ShuffleTiles();
        }

        private void SplitImageAndCreateTiles()
        {
            int tileIndex = 0;

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    PictureBox tile = new PictureBox
                    {
                        Width = tileSize,
                        Height = tileSize,
                        BorderStyle = BorderStyle.FixedSingle,
                        Left = col * tileSize,
                        Top = row * tileSize,
                        Tag = tileIndex
                    };

                    // Skip the last tile for empty space
                    if (tileIndex < gridSize * gridSize - 1)
                    {
                        Rectangle cropArea = new Rectangle(col * tileSize, row * tileSize, tileSize, tileSize);

                        // Ensure cropArea is valid
                        if (cropArea.Right > originalImage.Width || cropArea.Bottom > originalImage.Height)
                        {
                            MessageBox.Show("Crop area exceeds image bounds.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        tile.Image = CropImage(originalImage, cropArea);
                    }
                    else
                    {
                        emptyTile = tile; // Keep track of the empty tile
                    }

                    tile.Click += Tile_Click;
                    PuzzlePictureWidgetNATO.Controls.Add(tile);
                    tiles.Add(tile);
                    tileIndex++;
                }
            }
        }
        private Bitmap CropImage(Bitmap original, Rectangle cropArea)
        {
            Bitmap croppedImage = new Bitmap(cropArea.Width, cropArea.Height);
            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(original, new Rectangle(0, 0, cropArea.Width, cropArea.Height), cropArea, GraphicsUnit.Pixel);
            }
            return croppedImage;
        }
        private void ShuffleTiles()
        {
            Random rnd = new Random();
            var shuffledTiles = tiles.OrderBy(x => rnd.Next()).ToList();

            // Rearrange the tiles
            int index = 0;
            foreach (var tile in shuffledTiles)
            {
                int row = index / gridSize;
                int col = index % gridSize;
                tile.Left = col * tileSize;
                tile.Top = row * tileSize;
                index++;
            }
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            PictureBox clickedTile = sender as PictureBox;

            // Check if the clicked tile is adjacent to the empty tile
            if (IsAdjacent(clickedTile, emptyTile))
            {
                // Swap the clicked tile with the empty tile
                SwapTiles(clickedTile, emptyTile);

                // Check if the puzzle is solved
                if (IsPuzzleSolved())
                {
                    MessageBox.Show("Congratulations! You solved the puzzle!");
                }
            }
        }

        private bool IsAdjacent(PictureBox tile1, PictureBox tile2)
        {
            int dx = Math.Abs(tile1.Left - tile2.Left);
            int dy = Math.Abs(tile1.Top - tile2.Top);
            return (dx == tileSize && dy == 0) || (dx == 0 && dy == tileSize);
        }

        private void SwapTiles(PictureBox tile1, PictureBox tile2)
        {
            Point tempLocation = tile1.Location;
            tile1.Location = tile2.Location;
            tile2.Location = tempLocation;
        }

        private void hideWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RSSWidgetNATO.Hide();
        }

        private void HIDECLOCKMENUSTRIP_Opening(object sender, CancelEventArgs e)
        {
            ClockWidgetNATO.Hide();
        }

        private void hideWidgetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PuzzlePictureWidgetNATO.Hide();
        }

        private void hideWidgetToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MediaWidgetNATO.Hide();
        }

        private void MediaPlayerWidgetBtn_Click(object sender, EventArgs e)
        {
            MediaWidgetNATO.Show();
        }

        private void RSSFeedWidgetBtn_Click(object sender, EventArgs e)
        {
            RSSWidgetNATO.Show();
        }

        private void ClockWidgetBtn_Click(object sender, EventArgs e)
        {
            ClockWidgetNATO.Show();
        }

        private void PuzzlePictureWidgetBtn_Click(object sender, EventArgs e)
        {
            PuzzlePictureWidgetNATO.Show();
            InitializePuzzlePictureWidget();

        }

        private void hidewidgetsbox_Click(object sender, EventArgs e)
        {
            widgetsboxselect.Hide();
        }

        private void closeAllWidgetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClockWidgetNATO.Hide();
            RSSWidgetNATO.Hide();
            MediaWidgetNATO.Hide();
            PuzzlePictureWidgetNATO.Hide();
            stickynotewidgetnato.Hide();
            ImageSlideshowWidgetNATO.Hide();
            CalendarWidgetNATO.Hide();
            GameWidgetNATO.Hide();
            CodeSnippetWidgetNATO.Hide();
            RadioWidgetNATO.Hide();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            MediaWidgetNATO.Hide();
            ClockWidgetNATO.Hide();
            RSSWidgetNATO.Hide();
            PuzzlePictureWidgetNATO.Hide();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            MediaWidgetNATO.Show();
        }

        private void showClockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClockWidgetNATO.Show();
        }

        private void showRSSFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RSSWidgetNATO.Show();
        }

        private void showPuzzlePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PuzzlePictureWidgetNATO.Show();

            if (resetPuzzlePicture == 1)
            {
            }
            if (resetPuzzlePicture == 03)
            {
                InitializePuzzlePictureWidget();

                resetPuzzlePicture = 1;
            }
        }

        private void stickynotewigetbtn_Click(object sender, EventArgs e)
        {
            stickynotewidgetnato.Show();
        }

        private void hideWidgetToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            stickynotewidgetnato.Hide();
        }

        private void showStickyNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stickynotewidgetnato.Show();
        }

        private void pictureBox57_Click(object sender, EventArgs e)
        {
            //Calendar
            CalendarWidgetNATO.Show();

        }

        private void hideWidgetToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ImageSlideshowWidgetNATO.Hide();
        }

        private void pictureBox56_Click(object sender, EventArgs e)
        {
            ImageSlideshowWidgetNATO.Show();
        }

        private void hideWidgetToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            CalendarWidgetNATO.Hide();
        }

        private void showImageSlideshowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageSlideshowWidgetNATO.Show();
        }

        private void showCalendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalendarWidgetNATO.Show();
        }

        private void runcodesnippetwidget_Click(object sender, EventArgs e)
        {
            browserBox.Show();
            WebBrowser1.DocumentText = CodeSnippetTxt.Text;
        }

        private void pictureBox56_Click_1(object sender, EventArgs e)
        {
            CodeSnippetWidgetNATO.Show();

        }

        private void hideWidgetToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            CodeSnippetWidgetNATO.Hide();
        }

        private bool IsPuzzleSolved()
        {
            foreach (var tile in tiles)
            {
                int originalIndex = (int)tile.Tag;
                int currentRow = tile.Top / tileSize;
                int currentCol = tile.Left / tileSize;
                int currentIndex = currentRow * gridSize + currentCol;

                if (originalIndex != currentIndex)
                {
                    return false;
                }
            }
            return true;
        }

        private void CodeSnippetNATO_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }
        private void CodeSnippetNATO_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void showCodeSnippetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeSnippetWidgetNATO.Show();
        }

        private void showAllWidgetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MediaWidgetNATO.Show();
            RSSWidgetNATO.Show();
            ClockWidgetNATO.Show();
            PuzzlePictureWidgetNATO.Show();
            stickynotewidgetnato.Show();
            CalendarWidgetNATO.Show();
            ImageSlideshowWidgetNATO.Show();
            CodeSnippetWidgetNATO.Show();
            InitializePuzzlePictureWidget();
            GameWidgetNATO.Show();
            RadioWidgetNATO.Show();
        }

        private void clocktimerlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void hideclocktimer_Click(object sender, EventArgs e)
        {
        }
        private void widgetclocktimerbtn_Click(object sender, EventArgs e)
        {
        }

        private void clockclocktimerbtn_Click(object sender, EventArgs e)
        {

        }

        private void stopwatchbtnclocktimer_Click(object sender, EventArgs e)
        {

        }

        private void timerbtnclocktimer_Click(object sender, EventArgs e)
        {

        }

        private void fishgamebtn_Click(object sender, EventArgs e)
        {
            GameWidgetNATO.Show();
            string1fishgame.Show();
            string3fishgamebtn.Show();
            string4fishgamebtn.Show();
            string2fishgame.Show();
            fishgamesetpoint = 0;
            aipointsfishgame.Hide();
            mypointsfishgame.Hide();
            string5fishgame.Show();
        }



        private async void string4fishgamebtn_Click(object sender, EventArgs e)
        {
            aipointsfishgame.Show();
            mypointsfishgame.Show();
            string1fishgame.Hide();
            string3fishgamebtn.Hide();
            string4fishgamebtn.Hide();
            string2fishgame.Hide();
            fishgamesetpoint = 1;
            GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/cast1.png");
            aipointsfishgame.Text = "AI Points: 0";
            mypointsfishgame.Text = "My Points: 0";
            myfishgamepoints = 0;
            aifishgamepoints = 0;
            string5fishgame.Hide();
            await Task.Delay(3000);
            while (true)
            {
                await Task.Delay(3000);

                // Update points and UI
                aifishgamepoints += 2;
                aipointsfishgame.Text = "AI Points: " + aifishgamepoints;
            }

        }

        private void string3fishgamebtn_Click(object sender, EventArgs e)
        {
            myfishgamepoints = 0;

            string1fishgame.Hide();
            string3fishgamebtn.Hide();
            string4fishgamebtn.Hide();
            string2fishgame.Hide();
            fishgamesetpoint = 1;
            GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/cast1.png");
            aipointsfishgame.Text = "";
            mypointsfishgame.Text = "";
            aipointsfishgame.Hide();
            mypointsfishgame.Show();
            string5fishgame.Hide();
        }

        private void GameWidgetNATO_Enter(object sender, EventArgs e)
        {

        }
        private async void GameWidgetNATO_Click(object sender, EventArgs e)
        {
            if (fishgamesetpoint == 0)
            {
                //No actions required
            }
            if (fishgamesetpoint == 1)
            {
                GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/cast2.png");
                await Task.Delay(3000);
                int randomNumber = random.Next(1, 5);
                myfishgamepoints = myfishgamepoints + randomNumber;
                mypointsfishgame.Text = $"My Points:" + myfishgamepoints;
                if (randomNumber == 1)
                {
                    GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/Fish/Crayfish.png");
                    MessageBox.Show("You found a: Crayfish", "Fish Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/cast1.png");

                }
                if (randomNumber == 2)
                {
                    GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/Fish/Catfish.png");
                    MessageBox.Show("You found a: Catfish", "Fish Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/cast1.png");


                }
                if (randomNumber == 3)
                {
                    GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/Fish/Salmon.png");
                    MessageBox.Show("You found a: Salmon", "Fish Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/cast1.png");


                }
                if (randomNumber == 4)
                {
                    GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/Fish/Cod.png");
                    MessageBox.Show("You found a: Cod", "Fish Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/cast1.png");

                }
                if (randomNumber == 5)
                {
                    GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/Fish/Trout.png");
                    MessageBox.Show("You found a: Trout", "Fish Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GameWidgetNATO.BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/NatoWidgets/FishGame/cast1.png");

                }
            }
        }

        private void hideWidgetToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            GameWidgetNATO.Hide();
        }
        private void GameWidgetNATO_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging
            }
        }
        private void GameWidgetNATO_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void GameWidgetNATO_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                GameWidgetNATO.Left += e.X - dragStartPoint.X;
                GameWidgetNATO.Top += e.Y - dragStartPoint.Y;
            }
        }

        private void quitGameReturnToHomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameWidgetNATO.Show();
            string1fishgame.Show();
            string3fishgamebtn.Show();
            string4fishgamebtn.Show();
            string2fishgame.Show();
            fishgamesetpoint = 0;

            aipointsfishgame.Hide();
            mypointsfishgame.Hide();
            string5fishgame.Show();
            string5fishgame.Text = "Most Fish Caught: " + myfishgamepoints;
        }

        private void showGameWidgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameWidgetNATO.Show();
        }

        private void CodeSnippetNATO_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                CodeSnippetWidgetNATO.Left += e.X - dragStartPoint.X;
                CodeSnippetWidgetNATO.Top += e.Y - dragStartPoint.Y;
            }
        }
        //Draggable Apps
        private void groupBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void groupBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void groupBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                groupBox2.Left += e.X - dragStartPoint.X;
                groupBox2.Top += e.Y - dragStartPoint.Y;
            }
        }
        private void AllAppsBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void AllAppsBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void AllAppsBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                AllAppsBox.Left += e.X - dragStartPoint.X;
                AllAppsBox.Top += e.Y - dragStartPoint.Y;
            }
        }
        private void amhstpaintratingbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void amhstpaintratingbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void amhstpaintratingbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                amhstpaintratingbox.Left += e.X - dragStartPoint.X;
                amhstpaintratingbox.Top += e.Y - dragStartPoint.Y;
            }
        }
        private void YourMailandstylusPadBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void YourMailandstylusPadBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void YourMailandstylusPadBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                YourMailandstylusPadBox.Left += e.X - dragStartPoint.X;
                YourMailandstylusPadBox.Top += e.Y - dragStartPoint.Y;
            }
        }
        private void AmhstVideoPlayerandCamcorderPlusBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void AmhstVideoPlayerandCamcorderPlusBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void AmhstVideoPlayerandCamcorderPlusBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                AmhstVideoPlayerandCamcorderPlusBox.Left += e.X - dragStartPoint.X;
                AmhstVideoPlayerandCamcorderPlusBox.Top += e.Y - dragStartPoint.Y;
            }
        }
        //AO
        private void AODesignerBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void AODesignerBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void AODesignerBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                AODesignerBox.Left += e.X - dragStartPoint.X;
                AODesignerBox.Top += e.Y - dragStartPoint.Y;
            }
        }
        //Application
        private void application_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void application_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void application_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                application.Left += e.X - dragStartPoint.X;
                application.Top += e.Y - dragStartPoint.Y;
            }
        }
        //Premium Player & Background Chooser
        private void AppPremiumPlayer_BackgroundChooserBOX_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void AppPremiumPlayer_BackgroundChooserBOX_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void AppPremiumPlayer_BackgroundChooserBOX_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                AppPremiumPlayer_BackgroundChooserBOX.Left += e.X - dragStartPoint.X;
                AppPremiumPlayer_BackgroundChooserBOX.Top += e.Y - dragStartPoint.Y;
            }
        }
        //Authentic Messenger
        private void AuthenticMessengerApp_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void AuthenticMessengerApp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void AuthenticMessengerApp_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                AuthenticMessengerApp.Left += e.X - dragStartPoint.X;
                AuthenticMessengerApp.Top += e.Y - dragStartPoint.Y;
            }
        }
        //Authentic Movie Viewer
        private void AuthenticMovieViewer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void AuthenticMovieViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void AuthenticMovieViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                AuthenticMovieViewer.Left += e.X - dragStartPoint.X;
                AuthenticMovieViewer.Top += e.Y - dragStartPoint.Y;
            }
        }
        //Authentic NotePad
        private void AuthenticNotePad_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void AuthenticNotePad_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void AuthenticNotePad_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                AuthenticNotePad.Left += e.X - dragStartPoint.X;
                AuthenticNotePad.Top += e.Y - dragStartPoint.Y;
            }
        }
        //Authentic Phone Line
        private void AuthenticPhoneLinkBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void AuthenticPhoneLinkBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void AuthenticPhoneLinkBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                AuthenticPhoneLinkBox.Left += e.X - dragStartPoint.X;
                AuthenticPhoneLinkBox.Top += e.Y - dragStartPoint.Y;
            }
        }
        //Authentic Planner
        private void AuthenticPlanner_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void AuthenticPlanner_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void AuthenticPlanner_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                AuthenticPlanner.Left += e.X - dragStartPoint.X;
                AuthenticPlanner.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Background Changer Box
        private void backgroundchangebox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void backgroundchangebox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void backgroundchangebox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                backgroundchangebox.Left += e.X - dragStartPoint.X;
                backgroundchangebox.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Blender
        private void blenderbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void blenderbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void blenderbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                blenderbox.Left += e.X - dragStartPoint.X;
                blenderbox.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Blender (Startup)
        private void blenderloadingassetsbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void blenderloadingassetsbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void blenderloadingassetsbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                blenderloadingassetsbox.Left += e.X - dragStartPoint.X;
                blenderloadingassetsbox.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Browser
        private void browserBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void browserBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void browserBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                browserBox.Left += e.X - dragStartPoint.X;
                browserBox.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Bugs List
        private void BugsListNATO_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void BugsListNATO_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void BugsListNATO_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                BugsListNATO.Left += e.X - dragStartPoint.X;
                BugsListNATO.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Camera
        private void cameraapp_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void cameraapp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void cameraapp_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                cameraapp.Left += e.X - dragStartPoint.X;
                cameraapp.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Shared App (Custom App)
        private void customapp_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void customapp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void customapp_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                customapp.Left += e.X - dragStartPoint.X;
                customapp.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Debug Window (Designer Code)
        private void DebugWindowDesignerCode_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void DebugWindowDesignerCode_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void DebugWindowDesignerCode_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                DebugWindowDesignerCode.Left += e.X - dragStartPoint.X;
                DebugWindowDesignerCode.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Designer Code
        private void DesignerCode_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void DesignerCode_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void DesignerCode_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                DesignerCode.Left += e.X - dragStartPoint.X;
                DesignerCode.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Designer
        private void designertextbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void designertextbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void designertextbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                designertextbox.Left += e.X - dragStartPoint.X;
                designertextbox.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Code Editor
        private void editorbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void editorbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void editorbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                editorbox.Left += e.X - dragStartPoint.X;
                editorbox.Top += e.Y - dragStartPoint.Y;
            }

        }
        //EINK Pad
        private void EINKPAD_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void EINKPAD_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void EINKPAD_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                EINKPAD.Left += e.X - dragStartPoint.X;
                EINKPAD.Top += e.Y - dragStartPoint.Y;

            }

        }
        //Event Viewer
        private void eventviewerbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void eventviewerbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void eventviewerbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                eventviewerbox.Left += e.X - dragStartPoint.X;
                eventviewerbox.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Microsoft Excel (based 2013)
        private void excelbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void excelbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void excelbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                excelbox.Left += e.X - dragStartPoint.X;
                excelbox.Top += e.Y - dragStartPoint.Y;
            }

        }
        //FBOOST
        private void fboostoptimizer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void fboostoptimizer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void fboostoptimizer_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                fboostoptimizer.Left += e.X - dragStartPoint.X;
                fboostoptimizer.Top += e.Y - dragStartPoint.Y;
            }

        }
        //System Files
        private void filesbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void filesbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void filesbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                filesbox.Left += e.X - dragStartPoint.X;
                filesbox.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Find Text
        private void findtextapp_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void findtextapp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void findtextapp_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                findtextapp.Left += e.X - dragStartPoint.X;
                findtextapp.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Widgets Box
        private void widgetsboxselect_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void widgetsboxselect_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }

        private void widgetsboxselect_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                widgetsboxselect.Left += e.X - dragStartPoint.X;
                widgetsboxselect.Top += e.Y - dragStartPoint.Y;
            }

        }
        //Backup & Restore
        private void backuprestorebox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false; // Stop dragging

            }
        }
        private void backuprestorebox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location; // Store the initial mouse position
            }
        }
        //Windows Forms Designer Code
        private void windowsformsDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            debugWindowsFormsPlayer.Enabled = true;
            windowsformsDesignerCode.Enabled = false;
            DesignerCodeWindowsFormsPlayer.Show();
            ProgrammingLanguageTypeDesignerCode.Text = "C# Windows Forms";
            CodeAreaDesignerCode.Text = @"using System;
using System.Windows.Forms;

namespace MyWindowsCode
{
    public class MyDynamicCode
    {
        public static void Run()
        {
         //REQUIRED CODE   
        }
    }
}
";

            // Create a new GroupBox
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            designercodeplayergroupBox.ContextMenuStrip = contextMenuStrip;
            ToolStripMenuItem setWidth = new ToolStripMenuItem("Set Width");
            ToolStripMenuItem setHeight = new ToolStripMenuItem("Set Height");
            contextMenuStrip.Items.Add(setWidth);
            contextMenuStrip.Items.Add(setHeight);
            setWidth.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Width";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Width:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            designercodeplayergroupBox.Width = width;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };
            setHeight.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Height";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Height:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            designercodeplayergroupBox.Height = Height;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };


            // Enable resizing
            designercodeplayergroupBox.MouseDown += DesignerCodePlayerGroupBox_MouseDown;
            designercodeplayergroupBox.MouseMove += DesignerCodePlayerGroupBox_MouseMove;
            designercodeplayergroupBox.MouseUp += DesignerCodePlayerGroupBox_MouseUp;

            WindowsFormsPlayerDesignerPanel.Controls.Add(designercodeplayergroupBox);
            SubscribeMouseEvents(designercodeplayergroupBox);
        }

        private bool _isResizing;
        private Point _lastMouseScreenPosition;

        private void DesignerCodePlayerGroupBox_MouseDown(object sender, MouseEventArgs e)
        {
            GroupBox groupBox = sender as GroupBox;

            // Check if the mouse is near the bottom-right corner for resizing
            if (e.Button == MouseButtons.Left &&
                e.X >= groupBox.Width - 10 && e.Y >= groupBox.Height - 10)
            {
                _isResizing = true;
                _lastMouseScreenPosition = Control.MousePosition; // Use global mouse position
            }
        }

        private void DesignerCodePlayerGroupBox_MouseMove(object sender, MouseEventArgs e)
        {
            GroupBox groupBox = sender as GroupBox;

            if (_isResizing)
            {
                // Get current mouse position on the screen
                Point currentMouseScreenPosition = Control.MousePosition;

                // Calculate size changes based on mouse movement
                int deltaX = currentMouseScreenPosition.X - _lastMouseScreenPosition.X;
                int deltaY = currentMouseScreenPosition.Y - _lastMouseScreenPosition.Y;

                int newWidth = groupBox.Width + deltaX;
                int newHeight = groupBox.Height + deltaY;

                // Set minimum size
                newWidth = Math.Max(newWidth, 50);
                newHeight = Math.Max(newHeight, 50);

                // Update the size of the GroupBox
                groupBox.Size = new Size(newWidth, newHeight);

                // Update the last mouse position
                _lastMouseScreenPosition = currentMouseScreenPosition;
            }
            else
            {
                // Update cursor for resizing
                if (e.X >= groupBox.Width - 10 && e.Y >= groupBox.Height - 10)
                {
                    groupBox.Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    groupBox.Cursor = Cursors.Default;
                }
            }
        }

        private void DesignerCodePlayerGroupBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isResizing = false; // Stop resizing when the mouse button is released

                // Reset the cursor to default when resizing stops
                GroupBox groupBox = sender as GroupBox;
                groupBox.Cursor = Cursors.Default;
            }
        }


        private void debugWindowsFormsPlayer_Click(object sender, EventArgs e)

        {
            debugWindowsFormsPlayer.Enabled = false;

            isDebuggingWindowsFormsPlayer = true;
            DebugWindowsFormsPlayerBox.Show();
            CloneGroupBox(designercodeplayergroupBox, DebugWindowsFormsPlayerBox);
            DebugWindowsFormsPlayerBox.Width = designercodeplayergroupBox.Width;
            DebugWindowsFormsPlayerBox.Height = designercodeplayergroupBox.Height;
            DebugWindowsFormsPlayerBox.BackColor = designercodeplayergroupBox.BackColor;
            DebugWindowsFormsPlayerBox.Text = FileNameDesignerCode.Text;
            SubscribeMouseEvents(DebugWindowsFormsPlayerBox);
            UpTimeWindowsFormsPlayerBoxTimer.Start();         // Start the stopwatch
            uiUpdateWindowsFormsPlayerTimer.Start();
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem exitDebugger = new ToolStripMenuItem("Exit Debugger");
            DebugWindowsFormsPlayerBox.ContextMenuStrip = contextMenuStrip;
            contextMenuStrip.Items.Add(exitDebugger);
            exitDebugger.Click += (s, args) =>
            {
                isDebuggingWindowsFormsPlayer = false;
                HideClonedControls(DebugWindowsFormsPlayerBox);
                DebugWindowsFormsPlayerBox.Hide();
                UpTimeWindowsFormsPlayerBoxTimer.Stop();           // Stop the stopwatch
                uiUpdateWindowsFormsPlayerTimer.Stop();
                debugWindowsFormsPlayer.Enabled = true;

            };

        }
        private void HideClonedControls(GroupBox targetGroupBox)
        {
            foreach (Control control in targetGroupBox.Controls)
            {
                control.Visible = false; // Set the visibility to false
            }
        }
        private void helpWindowsFormsDesignerCodePlayer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("©Windows Forms Player - 1998 Last Updated\nInstalled with system from NATO OS-2\nTerminal commands: launch/ formsplayer.app,\nInstalled with Designer Code or NATO Designer", "About Windows Forms Player", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void CloneGroupBox(GroupBox sourceGroupBox, GroupBox targetGroupBox)
        {
            try
            {
                // Clear the target GroupBox
                targetGroupBox.Controls.Clear();

                // Clone each control from the source GroupBox
                foreach (Control sourceControl in sourceGroupBox.Controls)
                {
                    Control clonedControl = (Control)Activator.CreateInstance(sourceControl.GetType());

                    // Copy properties from the source control
                    clonedControl.Text = sourceControl.Text;
                    clonedControl.Size = sourceControl.Size;
                    clonedControl.Location = sourceControl.Location;
                    clonedControl.Font = sourceControl.Font;
                    clonedControl.BackColor = sourceControl.BackColor;
                    clonedControl.ForeColor = sourceControl.ForeColor;

                    // Add the cloned control to the target GroupBox
                    targetGroupBox.Controls.Add(clonedControl);
                }
            }
            catch (Exception)
            {
                DebugWindowsFormsPlayerBox.Hide();
                MessageBox.Show("Errors occured while debugging, Please remove these errors and try again.", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void pasteWindowsFormsDesignerCodePlayer_Click(object sender, EventArgs e)
        {

        }

        private void copyWindowsFormsDesignerCodePlayer_Click(object sender, EventArgs e)
        {

        }

        private void cutWindowsFormsDesignerCodePlayer_Click(object sender, EventArgs e)
        {

        }

        private void saveWindowsFormsDesignerCodePlayer_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void openWindowsFormsDesignerCodePlayer_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void newWindowsFormsDesignerCodePlayer_Click(object sender, EventArgs e)
        {

        }

        private void ExitWindowsFormsPlayer_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Save Changes to: " + FileNameDesignerCode.Text + ".sln", "Windows Forms Player", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Windows Forms Player",
                    FileName = FileNameDesignerCode.Text + ".sln",
                };
            }
            else if (dialogResult == DialogResult.No)
            {
                DesignerCodeWindowsFormsPlayer.Hide();
                windowsformsDesignerCode.Enabled = true;
            }
            if (isDebuggingWindowsFormsPlayer == true)
            {
                DialogResult DdialogResult = MessageBox.Show("There are projects that are still Debugging, keep debugging?", "Windows Forms Player", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DdialogResult == DialogResult.No)
                {
                    isDebuggingWindowsFormsPlayer = false;
                    HideClonedControls(DebugWindowsFormsPlayerBox);
                    DebugWindowsFormsPlayerBox.Hide();
                    UpTimeWindowsFormsPlayerBoxTimer.Stop();           // Stop the stopwatch
                    debugWindowsFormsPlayer.Enabled = true;
                    uiUpdateWindowsFormsPlayerTimer.Stop();
                }
            }
        }

        private void webBrowserToolboxWindowsFormsPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Code
            WebBrowser newWebBrowser = new WebBrowser();
            WindowsFormsPlayerFormsList.Items.Add("System.Windows.Forms.WebBrowser " + newWebBrowser.Text);

            // Set the WebBrowser properties (location, size, etc.)
            newWebBrowser.Location = new Point(50, 50 * WtextBoxCount); // Adjust position dynamically
            newWebBrowser.Size = new Size(700, 400);

            // Optionally, navigate to a URL
            newWebBrowser.DocumentText = "<HTML><H1>Placeholder Website</H1><hr color='black'><p>Navigate to a website to display it on this webBrowser element.</p><br><br><br><i>NATO-OS 7 SAMPLE DOCUMENT, LOCATION: OS/SYSTEM FILES/PLACEHOLDERSAMPLE.htm</i></HTML>";

            // Add the WebBrowser to the form's controls
            designercodeplayergroupBox.Controls.Add(newWebBrowser);
            SubscribeMouseEvents(newWebBrowser);
            //Context Menu Strip
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem deleteObject = new ToolStripMenuItem("Delete Object");
            ToolStripMenuItem setWidth = new ToolStripMenuItem("Set Width");
            ToolStripMenuItem setHeight = new ToolStripMenuItem("Set Height");
            contextMenuStrip.Items.Add(setWidth);
            contextMenuStrip.Items.Add(setHeight);
            setWidth.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Width";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Width:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            newWebBrowser.Width = width;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };
            setHeight.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Height";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Height:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            newWebBrowser.Height = Height;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };

            deleteObject.Click += (s, args) =>
            {
                newWebBrowser.Hide();

            };

            contextMenuStrip.Items.Add(deleteObject);

            // Assign the ContextMenuStrip to the PictureBox
            newWebBrowser.ContextMenuStrip = contextMenuStrip;
            ToolStripMenuItem changeName = new ToolStripMenuItem("Change Name");
            contextMenuStrip.Items.Add(setWidth);
            changeName.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Change Name";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = newWebBrowser.Name, Left = 10, Top = 10, AutoSize = true };
                    TextBox DRtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(DRtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(DRtextBox.Text, out int width))
                        {
                            DRtextBox.Text = newWebBrowser.Name;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };


        }

        private void textBoxToolboxWindowsFormsPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TextBox textBox = new TextBox();
            WindowsFormsPlayerFormsList.Items.Add("System.Windows.Forms.TextBox " + textBox.Text);

            designercodeplayergroupBox.Controls.Add(textBox);
            // Set properties of the LinkLabel
            textBox.Text = $"TextBox {++WtextBoxCount}";
            textBox.AutoSize = true;
            textBox.Location = new Point(50, 50 * WtextBoxCount); // Adjust position dynamically

            // Add the LinkLabel to the form (or a container like Panel)
            SubscribeMouseEvents(textBox);
            //Context Menu Strip
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem deleteObject = new ToolStripMenuItem("Delete Object");
            ToolStripMenuItem setWidth = new ToolStripMenuItem("Set Width");
            ToolStripMenuItem setHeight = new ToolStripMenuItem("Set Height");
            contextMenuStrip.Items.Add(setWidth);
            contextMenuStrip.Items.Add(setHeight);
            setWidth.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Width";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Width:", Left = 10, Top = 10, AutoSize = true };
                    TextBox WFtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(WFtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(WFtextBox.Text, out int width))
                        {
                            textBox.Width = width;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };
            setHeight.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Height";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Height:", Left = 10, Top = 10, AutoSize = true };
                    TextBox WFtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(WFtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(WFtextBox.Text, out int width))
                        {
                            WFtextBox.Height = Height;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };



            deleteObject.Click += (s, args) =>
            {
                textBox.Hide();

            };

            contextMenuStrip.Items.Add(deleteObject);
            ToolStripMenuItem itemText = new ToolStripMenuItem("Text");
            itemText.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Text:";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Set Text:", Left = 10, Top = 10, AutoSize = true };
                    TextBox WFFtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };
                    WFFtextBox.Multiline = true;
                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(WFFtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        textBox.Text = WFFtextBox.Text;
                    }
                }
            };
            contextMenuStrip.Items.Add(itemText);
            // Assign the ContextMenuStrip to the PictureBox
            textBox.ContextMenuStrip = contextMenuStrip;
            ToolStripMenuItem changeName = new ToolStripMenuItem("Change Name");
            contextMenuStrip.Items.Add(setWidth);
            changeName.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Change Name";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = textBox.Name, Left = 10, Top = 10, AutoSize = true };
                    TextBox DRtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(DRtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(DRtextBox.Text, out int width))
                        {
                            DRtextBox.Text = textBox.Name;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };

        }

        private void windowsMediaPlayerToolboxWindowsFormsPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create a new instance of AxWindowsMediaPlayer
            AxWindowsMediaPlayer mediaPlayer = new AxWindowsMediaPlayer();
            WindowsFormsPlayerFormsList.Items.Add("AxWindowsMediaPlayer " + mediaPlayer.Text);

            // Add the media player to the form's controls
            ((ISupportInitialize)(mediaPlayer)).BeginInit();
            ((ISupportInitialize)(mediaPlayer)).EndInit();

            // Set properties for the media player
            mediaPlayer.Name = $"mediaPlayer{++WFmediaPlayerCount}";
            mediaPlayer.Size = new Size(300, 200); // Set the size
            mediaPlayer.Location = new Point(10, 10 + (210 * WFmediaPlayerCount)); // Position dynamically

            // Add a ContextMenuStrip (Optional)
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem stopItem = new ToolStripMenuItem("Stop");
            stopItem.Click += (s, args) => mediaPlayer.Ctlcontrols.stop();
            contextMenu.Items.Add(stopItem);

            mediaPlayer.ContextMenuStrip = contextMenu;
            SubscribeMouseEvents(mediaPlayer);
            designercodeplayergroupBox.Controls.Add(mediaPlayer);
            ToolStripMenuItem setWidth = new ToolStripMenuItem("Set Width");
            ToolStripMenuItem setHeight = new ToolStripMenuItem("Set Height");
            contextMenu.Items.Add(setWidth);
            contextMenu.Items.Add(setHeight);
            setWidth.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Width";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Width:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            mediaPlayer.Width = width;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };
            setHeight.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Height";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Height:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            mediaPlayer.Height = Height;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };

            ToolStripMenuItem changeName = new ToolStripMenuItem("Change Name");
            contextMenu.Items.Add(setWidth);
            changeName.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Change Name";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = mediaPlayer.Name, Left = 10, Top = 10, AutoSize = true };
                    TextBox DRtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(DRtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(DRtextBox.Text, out int width))
                        {
                            DRtextBox.Text = mediaPlayer.Name;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };

        }

        private void pictureBoxToolboxWindowsFormsPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create a new PictureBox
            PictureBox pictureBox = new PictureBox();
            WindowsFormsPlayerFormsList.Items.Add("System.Windows.Forms.PictureBox " + pictureBox.Text);

            // Set properties for the PictureBox
            pictureBox.Name = $"pictureBox{++WFpictureBoxCount}";
            pictureBox.Size = new Size(100, 100); // Set PictureBox size
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Adjust image scaling
            pictureBox.Image = Image.FromFile(@"C:\Users\Alex\source\repos\NATO-OS 7\NATO-OS 7\OS\SYSTEM FILES\New.SystemIcons\SystemDefaultPlaceholder.png"); // Replace with a valid image file path
            pictureBox.Location = new Point(10, 10 + 110 * WFpictureBoxCount); // Position dynamically
            pictureBox.BorderStyle = BorderStyle.FixedSingle; // Optional: Add a border
            // Add the PictureBox to the form
            designercodeplayergroupBox.Controls.Add(pictureBox);
            //Context Menu Strip
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            // Add "Change Image..." menu item
            ToolStripMenuItem changeImageItem = new ToolStripMenuItem("Change Image...");
            ToolStripMenuItem deleteObject = new ToolStripMenuItem("Delete Object");
            ToolStripMenuItem setWidth = new ToolStripMenuItem("Set Width");
            ToolStripMenuItem setHeight = new ToolStripMenuItem("Set Height");
            contextMenuStrip.Items.Add(setWidth);
            contextMenuStrip.Items.Add(setHeight);
            setWidth.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Width";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Width:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            pictureBox.Width = width;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };
            setHeight.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Height";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Height:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            pictureBox.Height = Height;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };

            changeImageItem.Click += (s, args) =>
            {
                // Open a file dialog to select a new image
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Change the image of the PictureBox
                    pictureBox.Image = Image.FromFile(openFileDialog.FileName);
                }


            };
            deleteObject.Click += (s, args) =>
            {
                pictureBox.Hide();

            };
            contextMenuStrip.Items.Add(changeImageItem);
            contextMenuStrip.Items.Add(deleteObject);

            // Assign the ContextMenuStrip to the PictureBox
            pictureBox.ContextMenuStrip = contextMenuStrip;
            SubscribeMouseEvents(pictureBox);
            ToolStripMenuItem changeName = new ToolStripMenuItem("Change Name");
            contextMenuStrip.Items.Add(setWidth);
            changeName.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Change Name";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = pictureBox.Name, Left = 10, Top = 10, AutoSize = true };
                    TextBox DRtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(DRtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(DRtextBox.Text, out int width))
                        {
                            DRtextBox.Text = pictureBox.Name;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };

        }


        private void buttonToolboxWindowsFormsPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Button button = new Button();
            WindowsFormsPlayerFormsList.Items.Add("System.Windows.Forms.Button " + button.Text);

            designercodeplayergroupBox.Controls.Add(button);
            // Set properties of the LinkLabel
            button.Text = $"Button {++WFButtonCount}";
            button.AutoSize = true;
            button.Location = new Point(10, 30 * WFButtonCount); // Adjust position dynamically

            // Add the LinkLabel to the form (or a container like Panel)
            SubscribeMouseEvents(button);
            //Context Menu Strip
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem deleteObject = new ToolStripMenuItem("Delete Object");
            ToolStripMenuItem setWidth = new ToolStripMenuItem("Set Width");
            ToolStripMenuItem setHeight = new ToolStripMenuItem("Set Height");
            contextMenuStrip.Items.Add(setWidth);
            contextMenuStrip.Items.Add(setHeight);
            setWidth.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Width";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Width:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            button.Width = width;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };
            setHeight.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Height";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Height:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            button.Height = Height;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };



            deleteObject.Click += (s, args) =>
            {
                button.Hide();

            };
            ToolStripMenuItem itemText = new ToolStripMenuItem("Text");
            itemText.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Text:";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Set Text:", Left = 10, Top = 10, AutoSize = true };
                    TextBox WFFtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };
                    WFFtextBox.Multiline = true;
                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(WFFtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        button.Text = WFFtextBox.Text;
                    }
                }
            };
            contextMenuStrip.Items.Add(itemText);
            contextMenuStrip.Items.Add(deleteObject);

            // Assign the ContextMenuStrip to the PictureBox
            button.ContextMenuStrip = contextMenuStrip;
            ToolStripMenuItem changeName = new ToolStripMenuItem("Change Name");
            contextMenuStrip.Items.Add(setWidth);
            changeName.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Change Name";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = button.Name, Left = 10, Top = 10, AutoSize = true };
                    TextBox DRtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(DRtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(DRtextBox.Text, out int width))
                        {
                            DRtextBox.Text = button.Name;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };

        }

        private void groupBoxToolboxWindowsFormsPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GroupBox groupBox = new GroupBox();
            WindowsFormsPlayerFormsList.Items.Add("System.Windows.Forms.GroupBox " + groupBox.Text);

            designercodeplayergroupBox.Controls.Add(groupBox);
            // Set properties of the LinkLabel
            groupBox.Text = $"GroupBox {++WFGroupBoxCount}";
            groupBox.AutoSize = true;
            groupBox.Location = new Point(10, 30 * WFGroupBoxCount); // Adjust position dynamically

            // Add the LinkLabel to the form (or a container like Panel)
            SubscribeMouseEvents(groupBox);
            //Context Menu Strip
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem deleteObject = new ToolStripMenuItem("Delete Object");
            ToolStripMenuItem setWidth = new ToolStripMenuItem("Set Width");
            ToolStripMenuItem setHeight = new ToolStripMenuItem("Set Height");
            contextMenuStrip.Items.Add(setWidth);
            contextMenuStrip.Items.Add(setHeight);
            setWidth.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Width";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Width:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            groupBox.Width = width;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };
            setHeight.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Height";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Height:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            groupBox.Height = Height;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };



            deleteObject.Click += (s, args) =>
            {
                groupBox.Hide();

            };

            contextMenuStrip.Items.Add(deleteObject);
            ToolStripMenuItem itemText = new ToolStripMenuItem("Text");
            itemText.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Text:";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Set Text:", Left = 10, Top = 10, AutoSize = true };
                    TextBox WFFtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };
                    WFFtextBox.Multiline = true;
                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(WFFtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        groupBox.Text = WFFtextBox.Text;
                    }
                }
            };
            contextMenuStrip.Items.Add(itemText);
            // Assign the ContextMenuStrip to the PictureBox
            groupBox.ContextMenuStrip = contextMenuStrip;
            ToolStripMenuItem changeName = new ToolStripMenuItem("Change Name");
            contextMenuStrip.Items.Add(setWidth);
            changeName.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Change Name";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = groupBox.Name, Left = 10, Top = 10, AutoSize = true };
                    TextBox DRtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(DRtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(DRtextBox.Text, out int width))
                        {
                            DRtextBox.Text = groupBox.Name;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };

        }

        private void linklabelToolboxWindowsFormsPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = new LinkLabel();
            designercodeplayergroupBox.Controls.Add(linkLabel);
            WindowsFormsPlayerFormsList.Items.Add("System.Windows.Forms.LinkLabel " + linkLabel.Text);
            // Set properties of the LinkLabel
            linkLabel.Text = $"Link {++WFlinkLabelCount}";
            linkLabel.AutoSize = true;
            linkLabel.Location = new Point(10, 30 * WFlinkLabelCount); // Adjust position dynamically
            SubscribeMouseEvents(linkLabel);
            // Add the LinkLabel to the form (or a container like Panel)
            //Context Menu Strip
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem deleteObject = new ToolStripMenuItem("Delete Object");
            ToolStripMenuItem setWidth = new ToolStripMenuItem("Set Width");
            ToolStripMenuItem setHeight = new ToolStripMenuItem("Set Height");
            contextMenuStrip.Items.Add(setWidth);
            contextMenuStrip.Items.Add(setHeight);
            setWidth.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Width";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Width:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            linkLabel.Width = width;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };
            setHeight.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Set Height";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Height:", Left = 10, Top = 10, AutoSize = true };
                    TextBox textBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(textBox.Text, out int width))
                        {
                            linkLabel.Height = Height;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };



            deleteObject.Click += (s, args) =>
            {
                linkLabel.Hide();

            };

            ToolStripMenuItem itemText = new ToolStripMenuItem("Text");
            itemText.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Text:";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = "Set Text:", Left = 10, Top = 10, AutoSize = true };
                    TextBox WFFtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };
                    WFFtextBox.Multiline = true;
                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(WFFtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        linkLabel.Text = WFFtextBox.Text;
                    }
                }
            };
            contextMenuStrip.Items.Add(itemText);

            contextMenuStrip.Items.Add(deleteObject);

            // Assign the ContextMenuStrip to the PictureBox
            linkLabel.ContextMenuStrip = contextMenuStrip;

            ToolStripMenuItem changeName = new ToolStripMenuItem("Change Name");
            contextMenuStrip.Items.Add(setWidth);
            changeName.Click += (s, args) =>
            {
                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Change Name";
                    passwordForm.Width = 300;
                    passwordForm.Height = 150;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    Label label = new Label { Text = linkLabel.Name, Left = 10, Top = 10, AutoSize = true };
                    TextBox DRtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button submitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(label);
                    passwordForm.Controls.Add(DRtextBox);
                    passwordForm.Controls.Add(submitButton);
                    passwordForm.Controls.Add(cancelButton);

                    passwordForm.AcceptButton = submitButton;
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(DRtextBox.Text, out int width))
                        {
                            DRtextBox.Text = linkLabel.Name;
                        }
                        else
                        {
                            MessageBox.Show("Not valid.", "Designer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            };

        }

        private void pointerToolboxWindowsFormsPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void THREEDGAMEALLAPPS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void HIDE3DBOX_Click(object sender, EventArgs e)
        {
        }

        private void radiowidgetbtn_Click(object sender, EventArgs e)
        {
            RadioWidgetNATO.Show();
        }
        private void StartRadio(string radioUrl)
        {
            // Initialize Bass
            if (!Bass.Init())
            {
                MessageBox.Show("Error initializing audio system.");
                return;
            }

            // Create the stream from the given URL
            streamHandle = Bass.CreateStream(radioUrl);

            if (streamHandle == 0)
            {
                MessageBox.Show("Error opening stream.");
                return;
            }

            // Start playing the radio stream
            Bass.ChannelPlay(streamHandle);
        }

        private void StopRadio()
        {
            if (streamHandle != 0)
            {
                // Stop the radio stream
                Bass.ChannelStop(streamHandle);
                Bass.Free();  // Free Bass resources
            }
        }
        private void btnStopRadio_Click(object sender, EventArgs e)
        {
            StopRadio();

        }

        private void btnStartRadio_Click(object sender, EventArgs e)
        {
            StartRadio("https://wralfm.com/listen-live");  // Replace with actual FM station's stream URL

        }

        private void playAGameBTNMail_Click(object sender, EventArgs e)
        {
            InitializeGameBoard();
            gameBoardPlayAGame.Show();

        }
        private void InitializeGameBoard()
        {
            gameBoardPlayAGame.Text = "Tic-Tac-Toe";
            gameBoardPlayAGame.ClientSize = new Size(300, 350);

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Button button = new Button
                    {
                        Size = new Size(90, 90),
                        Location = new Point(10 + col * 100, 10 + row * 100),
                        Font = new Font("Arial", 24),
                        Tag = new Tuple<int, int>(row, col)
                    };
                    button.Click += ButtonClick;
                    gameBoardPlayAGame.Controls.Add(button);
                    buttons[row, col] = button;
                }
            }

            Button restartButton = new Button
            {
                Text = "Restart",
                Size = new Size(280, 40),
                Location = new Point(10, 310)
            };
            restartButton.Click += RestartGame;
            gameBoardPlayAGame.Controls.Add(restartButton);
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender is Button button && string.IsNullOrEmpty(button.Text))
            {
                button.Text = "X";
                button.Enabled = false;

                if (CheckWinner("X"))
                {
                    MessageBox.Show("You win!", "Game Over");
                    DisableAllButtons();
                    return;
                }

                if (IsDraw())
                {
                    MessageBox.Show("It's a draw!", "Game Over");
                    return;
                }

                isPlayerTurn = false;
                AITurn();
            }
        }

        private void AITurn()
        {
            Tuple<int, int> bestMove = FindBestMove();
            if (bestMove != null)
            {
                buttons[bestMove.Item1, bestMove.Item2].Text = "O";
                buttons[bestMove.Item1, bestMove.Item2].Enabled = false;

                if (CheckWinner("O"))
                {
                    MessageBox.Show("AI wins!", "Game Over");
                    DisableAllButtons();
                    return;
                }

                if (IsDraw())
                {
                    MessageBox.Show("It's a draw!", "Game Over");
                    return;
                }
            }
            isPlayerTurn = true;
        }

        private Tuple<int, int> FindBestMove()
        {
            int bestScore = int.MinValue;
            Tuple<int, int> bestMove = null;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (string.IsNullOrEmpty(buttons[row, col].Text))
                    {
                        buttons[row, col].Text = "O";
                        int score = Minimax(false);
                        buttons[row, col].Text = "";

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = new Tuple<int, int>(row, col);
                        }
                    }
                }
            }
            return bestMove;
        }

        private int Minimax(bool isMaximizing)
        {
            if (CheckWinner("O")) return 10;
            if (CheckWinner("X")) return -10;
            if (IsDraw()) return 0;

            int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (string.IsNullOrEmpty(buttons[row, col].Text))
                    {
                        buttons[row, col].Text = isMaximizing ? "O" : "X";
                        int score = Minimax(!isMaximizing);
                        buttons[row, col].Text = "";

                        bestScore = isMaximizing
                            ? Math.Max(score, bestScore)
                            : Math.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }

        private bool CheckWinner(string player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text == player && buttons[i, 1].Text == player && buttons[i, 2].Text == player) return true;
                if (buttons[0, i].Text == player && buttons[1, i].Text == player && buttons[2, i].Text == player) return true;
            }

            if (buttons[0, 0].Text == player && buttons[1, 1].Text == player && buttons[2, 2].Text == player) return true;
            if (buttons[0, 2].Text == player && buttons[1, 1].Text == player && buttons[2, 0].Text == player) return true;

            return false;
        }

        private bool IsDraw()
        {
            foreach (Button btn in buttons)
                if (string.IsNullOrEmpty(btn.Text)) return false;
            return true;
        }

        private void DisableAllButtons()
        {
            foreach (Button btn in buttons)
                btn.Enabled = false;
        }

        private void RestartGame(object sender, EventArgs e)
        {
            foreach (Button btn in buttons)
            {
                btn.Text = "";
                btn.Enabled = true;
            }
            isPlayerTurn = true;
        }

        private void HideGameMail_Click(object sender, EventArgs e)
        {
            gameBoardPlayAGame.Hide();
        }

        private async void clickforcaliforniaspaceview_Click(object sender, EventArgs e)
        {
            clickforcaliforniaspaceview.Hide();
            await Task.Delay(100);
            spaceviewdisplay.Image = System.Drawing.Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/california.png");
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Add library",
                Filter = "Blender Library Files|*.blenderlib|.blenderlib Files|*.blenderlib*"
            };
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            clickforeuropespaceview.Hide();
            clickforunitedstatesspaceview.Hide();
            clickforeastcoastspaceview.Hide();
            clickforwestcoastspaceview.Hide();
            clickforparisspaceview.Hide();
            clickforturkeyspaceview.Hide();
            clickforeiffeltowerspaceview.Hide();
            clickforcaliforniaspaceview.Hide();
            isPlanet = isPlanet - 1;
            string isPlainet = "" + isPlanet;

            isPlainet = toolStripTextBox3.Text;
            if (isPlanet == 1)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet1.png");
                allowisPlanet = false;
                clickforeuropespaceview.Show();
                clickforunitedstatesspaceview.Show();
                clickforeastcoastspaceview.Hide();
                clickforwestcoastspaceview.Hide();
                clickforparisspaceview.Hide();
                clickforturkeyspaceview.Hide();
                clickforeiffeltowerspaceview.Hide();
                clickforcaliforniaspaceview.Hide();
            }
            if (isPlanet == 2)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet2.png");
                allowisPlanet = true;

            }
            if (isPlanet == 3)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet3.png");
                allowisPlanet = true;


            }
            if (isPlanet == 4)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet4.png");
                allowisPlanet = true;


            }
            if (isPlanet == 5)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet5.png");
                allowisPlanet = true;


            }
            if (isPlanet == 6)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet6.png");
                allowisPlanet = true;


            }
            if (isPlanet == 7)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet7.png");
                allowisPlanet = true;


            }
            if (isPlanet == 8)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet8.png");
                allowisPlanet = true;


            }


            if (isPlanet == 9)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet9.png");

                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Space View / ISS";
                    passwordForm.Width = 500;
                    passwordForm.Height = 550;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    AxWebBrowser textBox = new AxWebBrowser { Height = 549, Width = 499 };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(cancelButton);
                    textBox.Navigate("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/ISS.htm");
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.Cancel)
                    {

                    }
                }
            }



        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            clickforeuropespaceview.Hide();
            clickforunitedstatesspaceview.Hide();
            clickforeastcoastspaceview.Hide();
            clickforwestcoastspaceview.Hide();
            clickforparisspaceview.Hide();
            clickforturkeyspaceview.Hide();
            clickforeiffeltowerspaceview.Hide();
            clickforcaliforniaspaceview.Hide();
            isPlanet = isPlanet + 1;
            string isPlainet = "" + isPlanet;
            isPlainet = toolStripTextBox3.Text;
            if (isPlanet == 1)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet1.png");
                allowisPlanet = false;
                clickforeuropespaceview.Show();
                clickforunitedstatesspaceview.Show();
                clickforeastcoastspaceview.Hide();
                clickforwestcoastspaceview.Hide();
                clickforparisspaceview.Hide();
                clickforturkeyspaceview.Hide();
                clickforeiffeltowerspaceview.Hide();
                clickforcaliforniaspaceview.Hide();
            }
            if (isPlanet == 2)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet2.png");
                allowisPlanet = true;

            }
            if (isPlanet == 3)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet3.png");
                allowisPlanet = true;


            }
            if (isPlanet == 4)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet4.png");
                allowisPlanet = true;


            }
            if (isPlanet == 5)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet5.png");
                allowisPlanet = true;


            }
            if (isPlanet == 6)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet6.png");
                allowisPlanet = true;


            }
            if (isPlanet == 7)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet7.png");
                allowisPlanet = true;


            }
            if (isPlanet == 8)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet8.png");
                allowisPlanet = true;


            }



            if (isPlanet == 9)
            {
                spaceviewdisplay.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/planet9.png");

                using (Form passwordForm = new Form())
                {
                    passwordForm.Text = "Space View / ISS";
                    passwordForm.Width = 500;
                    passwordForm.Height = 550;
                    passwordForm.StartPosition = FormStartPosition.CenterParent;

                    WebBrowser textBox = new WebBrowser { Height = 549, Width = 499 };
                    Button cancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    passwordForm.Controls.Add(textBox);
                    passwordForm.Controls.Add(cancelButton);
                    textBox.Navigate("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/SolarView/ISS.htm");
                    passwordForm.CancelButton = cancelButton;

                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
            }


        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

        }

        private void widgetsboxselect_Enter(object sender, EventArgs e)
        {

        }

        private async void gameplayer3dlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            blender3dgameplayerloadingassetsbox.Show();
            progressBar2.Value = 0;

            await Task.Delay(500);
            blender3dgameplayerloadingassetsbox.Text = "Blender";
            progressBar2.Value = 10;
            await Task.Delay(250);
            blender3dgameplayerloadingassetsbox.Text = "Blender.CSharp";
            progressBar2.Value = 20;
            await Task.Delay(250);
            blender3dgameplayerloadingassetsbox.Text = "Blender.Ports";
            progressBar2.Value = 30;
            await Task.Delay(250);
            blender3dgameplayerloadingassetsbox.Text = "Blender.Drawing";
            progressBar2.Value = 40;
            await Task.Delay(250);
            blender3dgameplayerloadingassetsbox.Text = "Blender.Drawing.Pen";
            progressBar2.Value = 50;
            await Task.Delay(250);
            blender3dgameplayerloadingassetsbox.Text = "Blender.ACLibrary";
            progressBar2.Value = 60;
            await Task.Delay(250);
            blender3dgameplayerloadingassetsbox.Text = "Blender.Graphics";
            progressBar2.Value = 70;
            await Task.Delay(250);
            blender3dgameplayerloadingassetsbox.Text = "Blender.TouchPanel";
            progressBar2.Value = 80;
            await Task.Delay(250);
            blender3dgameplayerloadingassetsbox.Text = "Blender.3DScreen";
            progressBar2.Value = 90;
            await Task.Delay(250);
            blender3dgameplayerloadingassetsbox.Text = "Blender.System.Linktree";
            progressBar2.Value = 100;
            await Task.Delay(250);
            blender3dgameplayerloadingassetsbox.Hide();
            progressBar2.Value = 0;
            blender3dgamebox.Show();
            Blender3DPlayerExecute.Hide();
        }

        private void openToolStripButton7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable Files|*.exe";
                openFileDialog.Title = "Select an Executable File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RunAppInGroupBox(openFileDialog.FileName);
                }
            }
        }
        private void RunAppInGroupBox(string appPath)
        {
            // Kill existing process if running
            if (_runningProcess != null && !_runningProcess.HasExited)
            {
                _runningProcess.Kill();
                _runningProcess.Dispose();
            }

            // Start the new process
            ProcessStartInfo psi = new ProcessStartInfo(appPath)
            {
                UseShellExecute = false
            };
            _runningProcess = Process.Start(psi);

            if (_runningProcess == null) return;

            // Wait for process to initialize
            Thread.Sleep(1000);

            // Get the main window handle (retry loop)
            IntPtr hWnd = IntPtr.Zero;
            for (int i = 0; i < 10; i++)
            {
                if (_runningProcess.MainWindowHandle != IntPtr.Zero)
                {
                    hWnd = _runningProcess.MainWindowHandle;
                    break;
                }
                Thread.Sleep(500);
            }

            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("Failed to get application window handle.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Embed the application inside the GroupBox
            SetParent(hWnd, Blender3DPlayerExecute.Handle);

            // Resize and reposition it inside the GroupBox
            MoveWindow(hWnd, 0, 0, Blender3DPlayerExecute.Width, Blender3DPlayerExecute.Height, true);

            // Ensure the app remains visible and interactive
            ShowWindow(hWnd, SW_RESTORE); // Restore if minimized
            SetFocus(hWnd); // Set focus for interaction
        }

        private void printToolStripButton5_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void uploadgameblenderplayer_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable Files|*.exe";
                openFileDialog.Title = "Select an Executable File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RunAppInGroupBox(openFileDialog.FileName); Blender3DPlayerExecute.Show();

                }
            }
        }

        private void selectblender3dversion_CheckedChanged(object sender, EventArgs e)
        {
            if (selectblender3dversion.Checked == true)
            {
                blenderchoicegame.Enabled = true;
            }
            else
            {
                blenderchoicegame.Enabled = false;
            }
        }

        private async void blender3dv3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            blenderimagegenerate.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/Game/slMinStart/loading.png");
            await Task.Delay(300);
            blenderimagegenerate.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/Game/slMinStart/3.png");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable Files|*.exe";
                openFileDialog.Title = "Select a Blender 3D File";
                openFileDialog.InitialDirectory = "C:/Users/Alex/source/repos/NATO-OS 7 Programs/Blender/vs/3/dir";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RunAppInGroupBox(openFileDialog.FileName); Blender3DPlayerExecute.Show();

                }
            }
        }

        private async void blender3dv2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            blenderimagegenerate.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/Game/slMinStart/loading.png"); await Task.Delay(300);

            blenderimagegenerate.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/Game/slMinStart/2.png");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable Files|*.exe";
                openFileDialog.Title = "Select a Blender 3D File";
                openFileDialog.InitialDirectory = "C:/Users/Alex/source/repos/NATO-OS 7 Programs/Blender/vs/2/dir";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RunAppInGroupBox(openFileDialog.FileName); Blender3DPlayerExecute.Show();

                }
            }


        }

        private async void blender3dv1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            blenderimagegenerate.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/Game/slMinStart/loading.png"); await Task.Delay(300);
            blenderimagegenerate.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/Game/slMinStart/1.png");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable Files|*.exe";
                openFileDialog.Title = "Select a Blender 3D File";
                openFileDialog.InitialDirectory = "C:/Users/Alex/source/repos/NATO-OS 7 Programs/Blender/vs/1/dir";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RunAppInGroupBox(openFileDialog.FileName); Blender3DPlayerExecute.Show();

                }
            }
        }

        private async void blender3dv0001alpha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            blenderimagegenerate.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/Game/slMinStart/loading.png"); await Task.Delay(300);
            blenderimagegenerate.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Blender/Game/slMinStart/alpha.png");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable Files|*.exe";
                openFileDialog.Title = "Select a Blender 3D File";
                openFileDialog.InitialDirectory = "C:/Users/Alex/source/repos/NATO-OS 7 Programs/Blender/vs/alpha/dir";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RunAppInGroupBox(openFileDialog.FileName);
                    Blender3DPlayerExecute.Show();
                }
            }
        }





        private void blenderconnecttomodem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This device is a newer device, cannot connect to modem!", "Blender Multiplayer", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void pictureBox53_Click(object sender, EventArgs e)
        {
            try
            {
                Task.Run(() =>
                {
                    PlayTetrisMelody();
                });
                TetrisGameBox.Visible = true;
                gameTimer.Start();

                string tetrisMusicURL = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/Packages (x86)/Tetris codec/media/main.mp3";
                tetrisMusic.URL = tetrisMusicURL;
                tetrisMusic.Ctlcontrols.play();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void tetrisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Task.Run(() =>
                {
                    PlayTetrisMelody();
                });
                TetrisGameBox.Visible = true;
                gameTimer.Start();

                string tetrisMusicURL = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/Packages (x86)/Tetris codec/media/main.mp3";
                tetrisMusic.URL = tetrisMusicURL;
                tetrisMusic.Ctlcontrols.play();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ticTacToeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameBoardPlayAGame.Show();
        }

        private void blender3ditem1A_Click(object sender, EventArgs e)
        {

        }

        private void blender3ditem2A_Click(object sender, EventArgs e)
        {

        }

        private void blender3ditem3A_Click(object sender, EventArgs e)
        {

        }

        private void blender3ditem4A_Click(object sender, EventArgs e)
        {

        }

        private void Toolstripbutton9_Click(object sender, EventArgs e)
        {
            Blender3DPlayerExecute.Hide();
        }

        private void NATOPaintBoxClose_Click(object sender, EventArgs e)
        {
            NATOPAINTBOX.Hide();
        }

        private void PaintBtnShow_Click(object sender, EventArgs e)
        {
            NATOPAINTBOX.Show();
        }

        private void toolStripSeparator22_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripButton7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("©Blender - 1995 Last Updated\nInstalled with system from NATO OS-7\nTerminal commands: launch/ runas [APP NAME] if blender.app", "About Blender 3D", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void hideblender3dplayer_Click(object sender, EventArgs e)
        {
            blender3dgamebox.Hide();
        }

        private void backuprestorebox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate new position
                backuprestorebox.Left += e.X - dragStartPoint.X;
                backuprestorebox.Top += e.Y - dragStartPoint.Y;
            }

        }
        private void NATOPaintDrawShape(string shape)
        {
            NATOPaintSelectedTool = shape;
            NATOPaintStatusLabel.Text = $"Selected: {shape}";

            using (Graphics g = Graphics.FromImage(NATOPaintCanvas))
            {
                Pen pen = new Pen(Color.Black, 3);
                switch (shape)
                {
                    case "Square":
                        g.DrawRectangle(pen, 100, 100, 100, 100);
                        break;
                    case "Triangle":
                        g.DrawPolygon(pen, new Point[] { new Point(150, 100), new Point(100, 200), new Point(200, 200) });
                        break;
                    case "Circle":
                        g.DrawEllipse(pen, 100, 100, 100, 100);
                        break;
                    case "Line":
                        g.DrawLine(pen, 100, 100, 200, 200);
                        break;
                    case "Star":
                        g.DrawPolygon(pen, new Point[] { new Point(150, 100), new Point(170, 160), new Point(230, 160), new Point(180, 190), new Point(200, 250), new Point(150, 210), new Point(100, 250), new Point(120, 190), new Point(70, 160), new Point(130, 160) });
                        break;
                }
            }
            NATOPaintPictureBox.Invalidate();
        }
        private void NATOPaintMouseDown(object sender, MouseEventArgs e)
        {
            if (NATOPaintSelecting)
            {
                NATOPaintSelectionBox = new Rectangle(e.Location, new Size(0, 0));
            }
            else
            {
                NATOPaintDrawing = true;
                NATOPaintLastPoint = e.Location;
            }
        }

        private void NATOPaintMouseMove(object sender, MouseEventArgs e)
        {
            if (NATOPaintSelecting)
            {
                NATOPaintSelectionBox = new Rectangle(NATOPaintSelectionBox.Location, new Size(e.X - NATOPaintSelectionBox.Left, e.Y - NATOPaintSelectionBox.Top));
                NATOPaintPictureBox.Invalidate();
            }
            else if (NATOPaintDrawing)
            {
                NATOPaintGraphics.DrawLine(NATOPaintPen, NATOPaintLastPoint, e.Location);
                NATOPaintLastPoint = e.Location;
                NATOPaintPictureBox.Invalidate();
            }
        }

        private void NATOPaintMouseUp(object sender, MouseEventArgs e)
        {
            NATOPaintDrawing = false;
            NATOPaintSelecting = false;
        }

        private void NATOPaintPictureBoxPaint(object sender, PaintEventArgs e)
        {
            if (NATOPaintSelecting)
            {
                using (Pen selectPen = new Pen(Color.Gray, 2))
                {
                    e.Graphics.DrawRectangle(selectPen, NATOPaintSelectionBox);
                }
            }
        }

        private void NATOPaintChangeColor(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    NATOPaintPen.Color = colorDialog.Color;
                }
            }
        }

        private void NATOPaintSaveImage(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|GIF Image|*.gif";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    NATOPaintCanvas.Save(saveDialog.FileName);
                }
            }
        }

        private void paintlinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NATOPAINTBOX.Show();
        }

        private void hideWidgetToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            RadioWidgetNATO.Hide();
        }

        private void showUnitConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RadioWidgetNATO.Show();
        }

        private void tutoriallinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tutorialsbox.Show();
        }

        private void hidetutorialsbox_Click(object sender, EventArgs e)
        {
            tutorialsbox.Hide();
        }

        private void findnatotutorials_Click(object sender, EventArgs e)
        {
            try
            {
                natotutorialsplayer.URL = "C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/SYSTEM FILES/Tutorial/" + lookupnatotutorials.Text + ".mp4";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Your search query was not found. \n Did you try all capitol letters ?\n Example: HOW TO USE BLENDER 3D", ex + " Not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
            }

        }

        private void NATOPaintZoomIn(object sender, EventArgs e)
        {
            NATOPaintZoomFactor *= 1.2f;
            NATOPaintStatusLabel.Text = $"Zoom: {NATOPaintZoomFactor * 100}%";
        }

        private void NATOPaintZoomOut(object sender, EventArgs e)
        {
            NATOPaintZoomFactor /= 1.2f;
            NATOPaintStatusLabel.Text = $"Zoom: {NATOPaintZoomFactor * 100}%";
        }

        private void NATOPaintStartSelection(object sender, EventArgs e)
        {
            NATOPaintSelecting = true;
            NATOPaintSelectedTool = "Select Box";
            NATOPaintStatusLabel.Text = $"Selected: {NATOPaintSelectedTool}";
        }

        private void natocmdscrollbar_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void generateBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void NATOPaintShowShapesPanel(object sender, EventArgs e)
        {
            NATOPaintShapePanel.Visible = !NATOPaintShapePanel.Visible;
        }

        private void appstatisticslinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            appstatisticsbox.Show();
        }

        private void hideappstatisticsbox_Click(object sender, EventArgs e)
        {
            appstatisticsbox.Hide();
        }
        private bool showGrid = false; // Flag to control whether to display the grid

        private void debugmodenato_Click(object sender, EventArgs e)
        {
            if (isDebugModeEnabled == true)
            {
                //Disable Debug Mode
                Console.WriteLine($"WARNING:: IS PERMISSION ASKED? \n DEBUG MODE DISABLED @@{DateTime.Now}");

            }
            if (isDebugModeEnabled == false)
            {
                //Enable Debug Mode
                Console.WriteLine($"WARNING:: IS PERMISSION ASKED? \n DEBUG MODE ENABLED @@{DateTime.Now}");
                //Write Code after this marker
                showCross = !showCross;  // Toggle the flag on button press
                showGrid = !showGrid;  // Toggle grid visibility
                this.Invalidate();  // Redraw the form to trigger Paint event


            }
        }

        private void natocmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string[] lines = natocmd.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);
                string command = lines.Length > 0 ? lines[lines.Length - 1] : "";

                // Echo the command itself to the console (optional)
                natocmd.AppendText("\r\n");

                // Run the command logic
                NATOCmdExecuteCommand(command);

                // Add a new line after command output to separate commands
                natocmd.AppendText("\r\n");
            }
        }

        private void NATOCmdExecuteCommand(string command)
        {
            Control[] controls = { GamesBox, InternetExplorerBox, };

            if (string.IsNullOrWhiteSpace(command)) return;

            if (command.ToLower() == "cls")
            {
                natocmd.Clear();
                return;
            }
            if (command.ToLower() == "exit")
            {
                customapp.Hide();
                return;
            }
            if (command.ToLower() == "about")
            {
                natocmd.AppendText($"Show (OS-2000) commands? or show (WIN) commands?\n Avalible: win | shows Windows Commands;\n os2000 | Shows {syslabel.Text}'s OS's commands;");

                return;
            }
            if (command.ToLower() == "id")
            {
                natocmd.AppendText($"Session ID: {sessionID}");
            }
            if (command.ToLower() == $"launch {controls}")
            {
                
            }
            //Sneaky Deaky Features
            if (command.ToLower() == "sdf")
            {
                natocmd.AppendText($"SNEAKY DEAKY FEATURES\n============\nUsage List:\nsdf show\nsdf script\nsdf deathmode\nsdf selfdest\nsdf sarcastic\nsdf help");

                return;
            }
            if (command.ToLower() == "sdf help")
            {
                natocmd.AppendText($"SNEAKY DEAKY FEATURES\n============\nHELP:\nsdf show\nsdf script\nsdf deathmode\nsdf selfdest\nsdf sarcastic\nsdf help");

                return;
            }
            if (command.ToLower() == "sdf show")
            {
                sneakydeakyfeaturesselectionbox.Show();
                return;
            }
            if (command.ToLower() == "sdf script")
            {
                if (!isSandboxEnabled)
                {
                    EnableSandboxing(this);
                }
                else
                {
                    DisableSandboxing(this);
                }

                isSandboxEnabled = !isSandboxEnabled;
                natocmd.AppendText($"SNEAKY DEAKY FEATURES >> Scriptable Mode Enabled.");

                MessageBox.Show(isSandboxEnabled ? "Sandbox is enabled" : "Sandbox is disabled", "Scripted Sandbox Egg");

                return;
            }
            if (command.ToLower() == "sdf deathmode")
            {
                natocmd.AppendText($"SNEAKY DEAKY FEATURES\n============\nUsage List:\nsdf show\nsdf script\nsdf deathmode\nsdf selfdest\nsdf sarcastic\nsdf help");
                Console.Beep();
                sdfpanel.Show();
                sdfpanel.BackColor = Color.Black;
                return;
            }
            //Sneaky Deaky Features
            if (command.ToLower() == "about")
            {
                natocmd.AppendText($"Show (OS-2000) commands? or show (WIN) commands?\n Avalible: win | shows Windows Commands;\n os2000 | Shows {syslabel.Text}'s OS's commands;");

                return;
            }
            if (command.ToLower() == $"launch")
            {
                customapp.Hide();
                return;
            }
            if (command.ToLower() == "help")
            {
                natocmd.AppendText($"Show (OS-2000) commands? or show (WIN) commands?\n Avalible: win | shows Windows Commands;\n os2000 | Shows {syslabel.Text}'s OS's commands;");
                if (command.ToLower() == "win")
                {
                    NATOCmdRunSystemCommand("win");
                    return;
                }
                if (command.ToLower() == "os2000")
                {
                    return;
                }
                return;
            }
            if (command.ToLower() == "hello")
            {
                natocmd.AppendText($"Hello, {syslabel.Text}! ");
                return;
            }
            //PostBook OneMedia Installation & Database config
            if (command.ToLower() == "launch-bios : pwsdb.dll")
            {

            }

            NATOCmdRunSystemCommand(command);
        }

        private void dEnvironmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void NATOCmdRunSystemCommand(string command)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrEmpty(output))
                        natocmd.AppendText(output + "\r\n");
                    if (!string.IsNullOrEmpty(error))
                        natocmd.AppendText("Error: " + error + "\r\n");
                }
            }
            catch (Exception ex)
            {
                natocmd.AppendText("Error executing command: " + ex.Message + "\r\n");
            }
        }
        private void downloadBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog openFileDialog = new SaveFileDialog())
            {
                openFileDialog.Filter = "Bitmap Files (*.bmp)|*.bmp";
                openFileDialog.Title = "Save Background (BITMAP)";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    backgroundBitmap.Save(openFileDialog.FileName);
                }
            }
        }
        private void FpsTimer_Tick(object sender, EventArgs e)
        {
            // Calculate FPS every second
            fps = frameCount;
            frameCount = 0;

            // Update the label with the current FPS
            lblFPS.Text = $"FPS: {fps}";

            // Optionally, you can reset the FPS counter here for each second.
        }

        private void aboutsneakydeakyfeatures_Click(object sender, EventArgs e)
        {
            using (Form passwordForm = new Form())
            {
                passwordForm.Text = "About Sneaky Deaky Features";
                passwordForm.Width = 681;
                passwordForm.Height = 507;
                passwordForm.StartPosition = FormStartPosition.CenterParent;


                PictureBox picture = new PictureBox { Text = "Cancel", Left = 1, Height = 507, Width = 680 };
                Button cancelButton = new Button { Text = "Cancel", Left = 1, Top = 1, Width = 80, DialogResult = DialogResult.Cancel };
                picture.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/Packages (x86)/Sneaky Deaky Features/about.png");

                passwordForm.Controls.Add(cancelButton);
                passwordForm.Controls.Add(picture);

                passwordForm.CancelButton = cancelButton;

                if (passwordForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void scriptedSandboxBtn_Click(object sender, EventArgs e)
        {
            if (!isSandboxEnabled)
            {
                EnableSandboxing(this);
            }
            else
            {
                DisableSandboxing(this);
            }

            isSandboxEnabled = !isSandboxEnabled;
            MessageBox.Show(isSandboxEnabled ? "Sandbox is enabled" : "Sandbox is disabled", "Scripted Sandbox Egg");
        }


        // Call this method to count the frames every time you render or do some calculations
        private void IncrementFrameCount()
        {
            frameCount++;
        }
        private void EnableSandboxing(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control is Button || control is Label || control is Panel || control is TextBox)
                {
                    control.MouseDown += objectControl_MouseDown;
                }

                // Recursively enable for child controls
                if (control.Controls.Count > 0)
                {
                    EnableSandboxing(control);
                }
            }
        }

        // Disable sandboxing - Remove right-click event handler
        private void DisableSandboxing(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control is Button || control is Label || control is Panel || control is TextBox)
                {
                    control.MouseDown -= objectControl_MouseDown;
                }

                // Recursively disable for child controls
                if (control.Controls.Count > 0)
                {
                    DisableSandboxing(control);
                }
            }
        }

        private void sdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sneakydeakyfeaturesselectionbox.Show();
        }

        private void hidesneakydeakyfeaturesbox_Click(object sender, EventArgs e)
        {
            sneakydeakyfeaturesselectionbox.Hide();
        }

        private void sneakydeakyfeatureslinkallappsbox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sneakydeakyfeaturesselectionbox.Show();
        }

        private void restorepcbtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "Restore Point Files (*.restorePoint)|*.restorePoint",
                    Title = "Select a Restore Point"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string restoreFilePath = openFileDialog.FileName;

                    if (!File.Exists(restoreFilePath))
                    {
                        MessageBox.Show("Error: Restore point file does not exist!", "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string restoreData = File.ReadAllText(openFileDialog.FileName);
                    if (string.IsNullOrWhiteSpace(restoreData))
                    {
                        MessageBox.Show("Error: Restore point file is empty!", "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string restorePointFolder = Path.Combine(tempDir, Path.GetFileNameWithoutExtension(restoreFilePath) + "Logs");

                    if (!restoreData.Contains(sessionID))
                    {
                        DialogResult result = MessageBox.Show(
                            "WARNING!\n This Restore Point is corrupted, has been tampered with, or belongs to another computer. Launching this may not have the OS function correctly. Do you wish to continue?",
                            "Restore Point Unresponsive", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.No) return;
                    }

                    Console.WriteLine($"Applying restore from: {restoreFilePath}");
                    ApplySystemState(restoreData, restorePointFolder);
                    MessageBox.Show("System restored successfully!", "Restored", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Restore failed: {ex}");
            }
        }


        private string GetSystemState(string restorePointFolder)
        {
            var controlStates = new List<string>();
            GetAllControls(this, controlStates, restorePointFolder);
            return string.Join("\n", controlStates);
        }

        private void GetAllControls(Control parent, List<string> stateList, string restorePointFolder)
        {
            foreach (Control ctrl in parent.Controls)
            {
                try
                {
                    stateList.Add($"{ctrl.Name}:{GetControlValue(ctrl, restorePointFolder)}");
                    if (ctrl.HasChildren)
                    {
                        GetAllControls(ctrl, stateList, restorePointFolder);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private string GetControlValue(Control ctrl, string restorePointFolder)
        {
            try
            {
                if (ctrl is TextBox textBox) return textBox.Text;
                if (ctrl is RichTextBox richTextBox) return richTextBox.Text;
                if (ctrl is ComboBox comboBox) return comboBox.SelectedIndex.ToString();
                if (ctrl is CheckBox checkBox) return checkBox.Checked.ToString();
                if (ctrl is RadioButton radioButton) return radioButton.Checked.ToString();
                if (ctrl is ProgressBar progressBar) return progressBar.Value.ToString();
                if (ctrl is ListBox listBox) return string.Join(",", listBox.Items.Cast<object>());
                if (ctrl is TrackBar trackBar) return trackBar.Value.ToString();
                if (ctrl is NumericUpDown numUpDown) return numUpDown.Value.ToString();
                if (ctrl is DataGridView dataGridView)
                {
                    var rows = dataGridView.Rows.Cast<DataGridViewRow>()
                        .Where(row => !row.IsNewRow)
                        .Select(row => string.Join("|", row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString() ?? "")))
                        .ToArray();
                    return string.Join("\n", rows);
                }
                if (ctrl is PictureBox pictureBox && pictureBox.Image != null)
                {
                    string imagePath = Path.Combine(restorePointFolder, "MediaData", $"{ctrl.Name}.png");
                    pictureBox.Image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                    return imagePath;
                }
                if (ctrl is WebBrowser webBrowser)
                {
                    string webDataPath = Path.Combine(restorePointFolder, "WebData", $"{ctrl.Name}.mhtml");
                    webBrowser.Document.ExecCommand("SaveAs", true, webDataPath);
                    return webDataPath;
                }
                return $"{ctrl.Visible},{ctrl.Enabled},{ctrl.Location.X},{ctrl.Location.Y},{ctrl.Size.Width},{ctrl.Size.Height}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }

        private void ApplySystemState(string stateData, string restorePointFolder)
        {
            try
            {
                string[] lines = stateData.Split('\n');
                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length > 1)
                    {
                        string controlName = parts[0];
                        string value = string.Join(":", parts.Skip(1));
                        var control = this.Controls.Find(controlName, true).FirstOrDefault();
                        if (control != null)
                        {
                            SetControlValue(control, value, restorePointFolder);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void SetControlValue(Control ctrl, string value, string restorePointFolder)
        {
            try
            {
                if (ctrl is TextBox textBox)
                    textBox.Text = value;
                else if (ctrl is ComboBox comboBox && int.TryParse(value, out int index))
                    comboBox.SelectedIndex = index;
                else if (ctrl is CheckBox checkBox && bool.TryParse(value, out bool isChecked))
                    checkBox.Checked = isChecked;
                else if (ctrl is RadioButton radioButton && bool.TryParse(value, out bool isRadioChecked))
                    radioButton.Checked = isRadioChecked;
                else if (ctrl is ProgressBar progressBar && int.TryParse(value, out int progressValue))
                    progressBar.Value = progressValue;
                else if (ctrl is ListBox listBox)
                {
                    var splitValues = value.Split('|');
                    listBox.Items.Clear();
                    if (splitValues.Length > 1)
                    {
                        listBox.Items.AddRange(splitValues[0].Split(','));
                        if (int.TryParse(splitValues[1], out int selectedIndex))
                            listBox.SelectedIndex = selectedIndex;
                    }
                }
                else if (ctrl is AxWindowsMediaPlayer mediaPlayer && File.Exists(value))
                    mediaPlayer.URL = File.ReadAllText(value);
                else if (ctrl is PictureBox pictureBox && File.Exists(value))
                    pictureBox.Image = Image.FromFile(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private bool isBackgroundImageRestored = false;

        // Helper method to check if the string is a valid base64 string
        private bool IsBase64String(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length % 4 != 0)
                return false;

            try
            {
                Convert.FromBase64String(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Helper method to get the background image as a base64 string
        private string GetBackgroundImageBase64()
        {
            if (this.BackgroundImage == null)
                return string.Empty;

            using (var ms = new MemoryStream())
            {
                this.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        private void phoneDialerBtn_Click(object sender, EventArgs e)
        {
            phoneDialerBox.Show();
        }

        private void phoneDialerBox_Enter(object sender, EventArgs e)
        {

        }

        private void phonedialerdialbtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Phone Dialer was unable to locate a modem on this system.\nPlease hook up a modem and try again.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

        }

        private void phonedialerbtn1_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "1";
        }

        private void phonedialerbtn2_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "2";
        }

        private void phonedialerbtn3_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "3";
        }

        private void phonedialerbtn6_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "6";
        }

        private void phonedialerbtn5_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "5";
        }

        private void phonedialerbtn4_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "4";
        }

        private void phonedialerbtn7_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "7";
        }

        private void phonedialerbtn8_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "8";
        }

        private void phonedialerbtn9_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "9";
        }

        private void phonedialerbtnHASH_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "#";
        }

        private void phonedialerbtn0_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "0";
        }

        private void phonedialerbtnSTAR_Click(object sender, EventArgs e)
        {
            string dialtext = phoneDialerDialBox.Text;
            phoneDialerDialBox.Text = phoneDialerDialBox.Text + "*";
        }

        private void phonedialerspeeddial8_Click(object sender, EventArgs e)
        {
            if (phonedialerspeeddial8.Text == "")
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial8.Text = compDialogtextBox.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("Phone Dialer was unable to locate a modem on this system.\nPlease hook up a modem and try again.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            ContextMenuStrip MenuStrip = new ContextMenuStrip();
            ToolStripMenuItem ClearDial = new ToolStripMenuItem("Replace Quick Dial");
            MenuStrip.Items.Add(ClearDial);
            phonedialerspeeddial8.ContextMenuStrip = MenuStrip;
            ClearDial.Click += (s, args) =>
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial8.Text = compDialogtextBox.Text;
                    }
                }
            };
        }

        private void phonedialerspeeddial7_Click(object sender, EventArgs e)
        {
            if (phonedialerspeeddial7.Text == "")
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial7.Text = compDialogtextBox.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("Phone Dialer was unable to locate a modem on this system.\nPlease hook up a modem and try again.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            ContextMenuStrip MenuStrip = new ContextMenuStrip();
            ToolStripMenuItem ClearDial = new ToolStripMenuItem("Replace Quick Dial");
            MenuStrip.Items.Add(ClearDial);
            phonedialerspeeddial7.ContextMenuStrip = MenuStrip;
            ClearDial.Click += (s, args) =>
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial7.Text = compDialogtextBox.Text;
                    }
                }
            };
        }

        private void phonedialerspeeddial6_Click(object sender, EventArgs e)
        {
            if (phonedialerspeeddial6.Text == "")
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial6.Text = compDialogtextBox.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("Phone Dialer was unable to locate a modem on this system.\nPlease hook up a modem and try again.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            ContextMenuStrip MenuStrip = new ContextMenuStrip();
            ToolStripMenuItem ClearDial = new ToolStripMenuItem("Replace Quick Dial");
            MenuStrip.Items.Add(ClearDial);
            phonedialerspeeddial6.ContextMenuStrip = MenuStrip;
            ClearDial.Click += (s, args) =>
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial6.Text = compDialogtextBox.Text;
                    }
                }
            };
        }

        private void phonedialerspeeddial5_Click(object sender, EventArgs e)
        {
            if (phonedialerspeeddial5.Text == "")
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial5.Text = compDialogtextBox.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("Phone Dialer was unable to locate a modem on this system.\nPlease hook up a modem and try again.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            ContextMenuStrip MenuStrip = new ContextMenuStrip();
            ToolStripMenuItem ClearDial = new ToolStripMenuItem("Replace Quick Dial");
            MenuStrip.Items.Add(ClearDial);
            phonedialerspeeddial5.ContextMenuStrip = MenuStrip;
            ClearDial.Click += (s, args) =>
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial5.Text = compDialogtextBox.Text;
                    }
                }
            };
        }

        private void hidebeatlabbox_Click(object sender, EventArgs e)
        {
            BeatLabBox.Hide();
        }

        private void beatlablinkallapps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BeatLabBox.Show();
        }

        private void whybeatlablinkbeatlab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            browserBox.Show();
            WebBrowser1.Navigate("C:\\Users\\Alex\\source\\repos\\NATO - OS 7\\NATO - OS 7\\OS\\SYSTEM FILES\\BangLab\\ver\\INDEX.htm");
        }

        private void makeyourownsonginfiveminutesbeatlablink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Form ComppasswordForm = new Form())
            {

                ComppasswordForm.Text = "Make Your Own Song In Five Minutes - BeatLab";
                ComppasswordForm.Width = 300;
                ComppasswordForm.Height = 150;
                ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                AxWindowsMediaPlayer compDialogtextBox = new AxWindowsMediaPlayer { Dock = DockStyle.Fill };
                Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };
                compDialogtextBox.URL = "";
                ComppasswordForm.Controls.Add(compDialogtextBox);
                ComppasswordForm.Controls.Add(compDialogtextBox);
                ComppasswordForm.Controls.Add(compsubmitButton);
                ComppasswordForm.Controls.Add(compcancelButton);

                ComppasswordForm.AcceptButton = compsubmitButton;
                ComppasswordForm.CancelButton = compcancelButton;

                if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void beatlabtutorialbeatlab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Form ComppasswordForm = new Form())
            {

                ComppasswordForm.Text = "Program Speed Dial";
                ComppasswordForm.Width = 300;
                ComppasswordForm.Height = 150;
                ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                ComppasswordForm.Controls.Add(compDialoglabel);
                ComppasswordForm.Controls.Add(compDialogtextBox);
                ComppasswordForm.Controls.Add(compsubmitButton);
                ComppasswordForm.Controls.Add(compcancelButton);

                ComppasswordForm.AcceptButton = compsubmitButton;
                ComppasswordForm.CancelButton = compcancelButton;

                if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                {
                    phonedialerspeeddial4.Text = compDialogtextBox.Text;
                }
            }
        }

        private void phonedialerspeeddial4_Click(object sender, EventArgs e)
        {
            if (phonedialerspeeddial4.Text == "")
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial4.Text = compDialogtextBox.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("Phone Dialer was unable to locate a modem on this system.\nPlease hook up a modem and try again.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            ContextMenuStrip MenuStrip = new ContextMenuStrip();
            ToolStripMenuItem ClearDial = new ToolStripMenuItem("Replace Quick Dial");
            MenuStrip.Items.Add(ClearDial);
            phonedialerspeeddial4.ContextMenuStrip = MenuStrip;
            ClearDial.Click += (s, args) =>
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial4.Text = compDialogtextBox.Text;
                    }
                }
            };
        }

        private void phonedialerspeeddial3_Click(object sender, EventArgs e)
        {
            if (phonedialerspeeddial3.Text == "")
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial3.Text = compDialogtextBox.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("Phone Dialer was unable to locate a modem on this system.\nPlease hook up a modem and try again.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            ContextMenuStrip MenuStrip = new ContextMenuStrip();
            ToolStripMenuItem ClearDial = new ToolStripMenuItem("Replace Quick Dial");
            MenuStrip.Items.Add(ClearDial);
            phonedialerspeeddial3.ContextMenuStrip = MenuStrip;
            ClearDial.Click += (s, args) =>
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial3.Text = compDialogtextBox.Text;
                    }
                }
            };
        }

        private void phonedialerspeeddial2_Click(object sender, EventArgs e)
        {
            if (phonedialerspeeddial2.Text == "")
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial2.Text = compDialogtextBox.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("Phone Dialer was unable to locate a modem on this system.\nPlease hook up a modem and try again.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            ContextMenuStrip MenuStrip = new ContextMenuStrip();
            ToolStripMenuItem ClearDial = new ToolStripMenuItem("Replace Quick Dial");
            MenuStrip.Items.Add(ClearDial);
            phonedialerspeeddial2.ContextMenuStrip = MenuStrip;
            ClearDial.Click += (s, args) =>
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial2.Text = compDialogtextBox.Text;
                    }
                }
            };
        }

        private void phonedialerspeeddial1_Click(object sender, EventArgs e)
        {
            if (phonedialerspeeddial1.Text == "")
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial1.Text = compDialogtextBox.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("Phone Dialer was unable to locate a modem on this system.\nPlease hook up a modem and try again.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            ContextMenuStrip MenuStrip = new ContextMenuStrip();
            ToolStripMenuItem ClearDial = new ToolStripMenuItem("Replace Quick Dial");
            MenuStrip.Items.Add(ClearDial);
            phonedialerspeeddial1.ContextMenuStrip = MenuStrip;
            ClearDial.Click += (s, args) =>
            {
                using (Form ComppasswordForm = new Form())
                {

                    ComppasswordForm.Text = "Program Speed Dial";
                    ComppasswordForm.Width = 300;
                    ComppasswordForm.Height = 150;
                    ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                    Label compDialoglabel = new Label { Text = "Enter a name and number\n to save on this button.", Left = 10, Top = 10, AutoSize = true };
                    TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                    Button compsubmitButton = new Button { Text = "Save", Left = 10, Top = 60, Width = 80, DialogResult = DialogResult.OK };
                    Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                    ComppasswordForm.Controls.Add(compDialoglabel);
                    ComppasswordForm.Controls.Add(compDialogtextBox);
                    ComppasswordForm.Controls.Add(compsubmitButton);
                    ComppasswordForm.Controls.Add(compcancelButton);

                    ComppasswordForm.AcceptButton = compsubmitButton;
                    ComppasswordForm.CancelButton = compcancelButton;

                    if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        phonedialerspeeddial1.Text = compDialogtextBox.Text;
                    }
                }
            };
        }

        private void hidePhoneDialerBox_Click(object sender, EventArgs e)
        {
            phoneDialerBox.Hide();
        }

        private void annotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form ComppasswordForm = new Form())
            {
                ComppasswordForm.Text = "Annotate...";
                ComppasswordForm.Width = 300;
                ComppasswordForm.Height = 150;
                ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                Label compDialoglabel = new Label { Text = "Type text to annotate:\nMust enable speech first", Left = 10, Top = 10, AutoSize = true };
                TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                Button compsubmitButton = new Button { Text = "Submit", Left = 10, Top = 60, Width = 80, };
                Button compcancelButton = new Button { Text = "Cancel", Left = 100, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

                ComppasswordForm.Controls.Add(compDialoglabel);
                ComppasswordForm.Controls.Add(compDialogtextBox);
                ComppasswordForm.Controls.Add(compsubmitButton);
                ComppasswordForm.Controls.Add(compcancelButton);
                compsubmitButton.Click += (s, args) =>
                {


                    NATOVoiceSynthesizer.SpeakAsync(compDialogtextBox.Text);








                };

                ComppasswordForm.CancelButton = compcancelButton;

                if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void EnableSpeechBtn_Click(object sender, EventArgs e)
        {

            if (isSpeechEnabled)
            {
                try
                {
                    NATOVoiceRecognizer.RecognizeAsyncCancel();
                    EnableSpeechBtn.Image = Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\SystemSpeech\\off.png");
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }

            }
            else
            {
                try
                {
                    EnableSpeechBtn.Image = Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\SystemSpeech\\active.png");
                    InitSpeech(); //Speech
                    NATOVoiceRecognizer.RecognizeAsync(RecognizeMode.Multiple);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }


            }

            isSpeechEnabled = !isSpeechEnabled;

        }

        private void speechRecognitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            natospeechrecogbox.Show();
        }

        private void HideSpeechRecogBox_Click(object sender, EventArgs e)
        {
            natospeechrecogbox.Hide();
            EnableSpeechBtn.Image = Image.FromFile("C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\SystemSpeech\\off.png");
            NATOVoiceRecognizer.RecognizeAsyncCancel();
            NATOVoiceSynthesizer.SpeakAsync("Stopped listening");
        }

        private void samplebeatbtnbeatlab_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer { };
            wmp.controls.play();

            wmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\ver\\dbeat.m4a";
        }

        private void NewBeatLabProject_Click(object sender, EventArgs e)
        {
            using (Form ComppasswordForm = new Form())
            {
                ComppasswordForm.Text = "BeatLab Project Settings";
                ComppasswordForm.Width = 300;
                ComppasswordForm.Height = 150;
                ComppasswordForm.StartPosition = FormStartPosition.CenterParent;

                Label compDialoglabel = new Label { Text = "Project Name:", Left = 10, Top = 10, AutoSize = true };
                TextBox compDialogtextBox = new TextBox { Left = 10, Top = 30, Width = 260 };
                Button drums = new Button { Text = "Drums", Left = 100, Top = 60, Width = 80 };
                Button piano = new Button { Text = "Piano", Left = 55, Top = 60, Width = 80 };

                ComppasswordForm.Controls.Add(compDialoglabel);
                ComppasswordForm.Controls.Add(compDialogtextBox);
                ComppasswordForm.Controls.Add(piano);
                ComppasswordForm.Controls.Add(drums);

                drums.Click += (s, args) =>
                {
                    BeatLabBox.Text = compDialogtextBox.Text + " - BeatLab Project";
                    BeatLabProjectBox.Show();
                    BeatLabPianoBox.Hide();

                };
                piano.Click += (s, args) =>
                {
                    BeatLabPianoBox.Text = compDialogtextBox.Text + " - BeatLab Project";
                    BeatLabPianoBox.Show();
                    BeatLabProjectBox.Hide();
                };


                if (ComppasswordForm.ShowDialog() == DialogResult.OK)
                {

                }

            }
            BeatLabProjectBox.Show();
        }
        bool isHIHATopen = false;
        private void HideBeatLabProjectBox_Click(object sender, EventArgs e)
        {
            BeatLabProjectBox.Hide();
        }

        private void HIHATBandLab_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer { };

            if (isHIHATopen == false)
            {
                wmp.controls.play();

                wmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\HI_HAT.mp3";
            }
            else
            {
                wmp.controls.play();

                wmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\HIHAT_OPEN.mp3";
            }
        }

        private void CymbalBandLab_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer { };
            wmp.controls.play();

            wmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\CRASH.mp3";
        }

        private void TomDrumBandLab_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer { };
            wmp.controls.play();

            wmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\TOM.mp3";
        }

        private void CrashSymbalBeatLab_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer { };
            wmp.controls.play();

            wmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\CYMBAL.mp3";
        }

        private void TomDrumBandLab1_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer { };
            wmp.controls.play();

            wmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\TOM.mp3";
        }

        private void SnareDrumBandLab_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer { };
            wmp.controls.play();

            wmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\SNARE.mp3";
        }

        private void KickDrumBandLab_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer { };
            wmp.controls.play();

            wmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\KICK.mp3";
        }

        private void HoldHiHatBtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toggleHIHAT_CheckedChanged(object sender, EventArgs e)
        {

        }



        private void BandLabDrumClickCrash(object sender, EventArgs e)
        {

        }

        private void BandLabDrumClickRide(object sender, EventArgs e)
        {

        }

        private void BandLabDrumClickTomDrums(object sender, EventArgs e)
        {

        }

        private void BandLabDrumClickHIHAT(object sender, EventArgs e)
        {

        }

        private void BandLabDrumClickSnare(object sender, EventArgs e)
        {

        }

        private void BandLabDrumClickTom(object sender, EventArgs e)
        {

        }

        private void BandLabDrumClickKick(object sender, EventArgs e)
        {

        }

        private void CrashSymbalBeatLab_Click_1(object sender, EventArgs e)
        {

        }

        private void CymbalBandLab_Click_1(object sender, EventArgs e)
        {

        }

        private void TickBtnBeatLab_Click(object sender, EventArgs e)
        {
            try
            {
                TickbeepTimer.Interval = (double)(TickNumericUpDownBeatLab.Value * 1000);


                TickbeepTimer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void toggleHiHatBeatLab_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleHiHatBeatLab.Checked == true)
            {
                isHIHATopen = false;
            }
            else
            {
                isHIHATopen = true;
            }
        }
        WindowsMediaPlayer BeatLabwmp = new WindowsMediaPlayer { };

        private void HidePianoBoxBeatLab_Click(object sender, EventArgs e)
        {
            BeatLabPianoBox.Hide();
        }

        private void ridecymbalbeatlab_Click(object sender, EventArgs e)
        {
            BeatLabwmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\CYMBAL.mp3";
            BeatLabwmp.controls.play();
        }

        private void openpostbookonemediabtn_Click(object sender, EventArgs e)
        {
            postbookonemediabox.Show();
        }

        private void hidepostbookonemediabox_Click(object sender, EventArgs e)
        {
            postbookonemediabox.Hide();
        }

        private void darkmodeonemedia_CheckedChanged(object sender, EventArgs e)
        {
            if (darkmodeonemedia.Checked == true)
            {
                postbookonemediabox.BackColor = Color.Black;
                postbookonemediabox.ForeColor = Color.White;
            }
            else
            {
                postbookonemediabox.BackColor = SystemColors.ButtonFace;
                postbookonemediabox.ForeColor = Color.Black;
            }
        }

        private void testsoundbtnonemedia_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer
            {
                URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\Packages (x86)\\pws_OneMediaService_str-lang\\static\\testaudio.mp3",


            };
            mediaPlayer.controls.play();

        }

        private async void testvideoonemediabtn_Click(object sender, EventArgs e)
        {
            AxWindowsMediaPlayer mediaPlayer = new AxWindowsMediaPlayer { };
            mediaPlayer.Enabled = true;
            mediaPlayer.Location = new System.Drawing.Point(56, 100);

            mediaPlayer.Size = new System.Drawing.Size(517, 290);
            postbookonemediasigninbox.Controls.Add(mediaPlayer);
            mediaPlayer.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\Packages (x86)\\pws_OneMediaService_str-lang\\static\\mov_test.mp3";
            mediaPlayer.Show();
            mediaPlayer.Ctlcontrols.play();
            await Task.Delay(10000);
            mediaPlayer.Hide();
            mediaPlayer.Ctlcontrols.stop();

        }

        private void postbookdevonemedialogo_Click(object sender, EventArgs e)
        {
            // Create the form
            Form creditsForm = new Form();
            creditsForm.Text = "App Development Credits";
            creditsForm.Size = new Size(400, 300);

            // Create the TextBox
            TextBox textBox = new TextBox();
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Dock = DockStyle.Fill;
            textBox.ReadOnly = true;
            textBox.Font = new Font("Consolas", 12);

            // Add TextBox to form
            creditsForm.Controls.Add(textBox);

            // Fill credits list
            string[] credits = new string[]
            {
        "Alex Johnson - Project Manager",
        "Riley Smith - Lead Programmer",
        "Jordan Lee - UI/UX Designer",
        "Morgan Kim - Backend Developer",
        "Taylor Scott - Frontend Developer",
        "Casey Morgan - Database Engineer",
        "Cameron Diaz - QA Tester",
        "Avery Parker - QA Tester",
        "Logan Carter - Audio Engineer",
        "Quinn Gray - 3D Artist",
        "Peyton Blake - Animator",
        "Skylar Evans - AI Developer",
        "Rowan Brooks - Gameplay Programmer",
        "Sage Walker - Story Writer",
        "Reese Collins - Network Engineer",
        "Hunter Bailey - DevOps Specialist",
        "Dakota Reed - Systems Analyst",
        "Drew Hudson - Graphic Designer",
        "Phoenix Lewis - Cloud Engineer",
        "Emerson Bell - Documentation Lead",
        "Charlie West - Marketing Manager",
        "River Hayes - Video Editor",
        "Tatum Reed - Sound Designer",
        "Shiloh Fox - Localization Lead",
        "Justice Stone - Build Manager",
        "Jesse Knight - Content Creator",
        "Remy Archer - Full Stack Dev",
        "Marley Lane - Data Analyst",
        "Sasha Pierce - VR Developer",
        "Kendall Cross - Support Engineer",
        "Arden Tate - UI Programmer",
        "Dakari Boone - Tools Developer",
        "Lennox Drew - Concept Artist",
        "Oakley Hart - Product Owner",
        "Ellis Noble - AR Developer",
        "Blair York - Motion Designer",
        "River Quinn - Mobile App Dev",
        "Zion Frost - Code Reviewer",
        "Sloan King - Asset Manager"
            };

            // Add credits text
            foreach (string line in credits)
            {
                textBox.AppendText(line + Environment.NewLine);
            }

            // Create and configure the Timer
            Timer timer = new Timer();
            timer.Interval = 100; // adjust for speed (lower = faster)

            // Scroll effect on each tick
            timer.Tick += (s, args) =>
            {
                // Move the caret down and scroll to it
                if (textBox.SelectionStart < textBox.Text.Length)
                {
                    textBox.SelectionStart++;
                    textBox.ScrollToCaret();
                }
                else
                {
                    timer.Stop();
                }
            };

            // Start the timer when form loads
            creditsForm.Load += (s, args) => timer.Start();

            // Show dialog
            creditsForm.ShowDialog();
        }

        private void aboutpostbookonemediaauthorize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            browserBox.Show();
            WebBrowser1.Navigate("data:text/html;charset=utf-8;base64,PGh0bWw+PGhlYWQ+PHRpdGxlPkFib3V0IEF1dGhvcml6ZSB8IFBvc3RCb29rIE9uZU1lZGlhPC90aXRsZT48L2hlYWQ+PGJvZHk+PGgxPkFib3V0IEF1dGhvcml6ZTwvaDE+PGg1PjxpPlBvc3RCb29rIE9uZU1lZGlhPC9pPjwvaDU+PGhyPjxkaXYgc3R5bGU9ImZsb2F0OnJpZ2h0OyI+PGltZyBzcmM9ImRhdGE6aW1hZ2UvcG5nO2Jhc2U2NCxpVkJPUncwS0dnb0FBQUFOU1VoRVVnQUFBZjRBQUFCVENBSUFBQUR4K1BMMkFBQUFBWE5TUjBJQXJzNGM2UUFBQUFSblFVMUJBQUN4and2OFlRVUFBQUFKY0VoWmN3QUFEc01BQUE3REFjZHZxR1FBQUViMFNVUkJWSGhlN2IxWGpHVFptU1lXMTk4Yk43ekpqUFNtc254MVZmdG1VME96SkhkbWRvYTdtTjBkTEhhMWNwQUE2VW5RZzE3MmtTOTZXMkFoUUFJa1FCS2dsYlFTUnRCZ2xqdVlHWEpKTmpsc3NoMjd1NnE2VEZabFpxWFA4UDU2cSs5RVJHVmxSa1o2VTlsWjhlRldWbHgvN1BkLy83bkhVTDd2Qi9yb280OCsrbmlWY0VHbzMvTjkwL0ZkMXhjNGltUG96dEh6QVlUTmNuem5YSWF0ano3NmVEWFJvWDV3MDFMWmFPZzJROU5EVVQ0b01EeERDU3dGdEs4N1BzQ0FEZDFkcjVtYTVRb3NQUndUZUpZV1dacG5UK0FWaU1PVG5HWVpMazlUdzBraEluT2RFOXZoZW41RmRYSjF5M0Rja01CbW9seVFZMWlHb3FuQUNjYTBDd2piMDV4bUdLNUFVME5KSWJwTDJQcm9vNDgremd3ZEVlcjYvdDBWQll3Y0Zna1ZybGVNVHhjYnBhWnRPMkRzOWlXSEEyNVRUYmV1TzViajRTSGtrQitvYWM1U3lZaUliRWhrc0x0Y01uNjMxSUFsQUNPM2JqbzZRTnQ0ZUtWcDV5dUdhcmlkb3p1QTErUWExa2JkREF1c3hOTzI0NjlVakh1clNyWnVtWFlya0tlQWJXSFRkdzFiSDMzMDBjZVpvVVA5b0tleWFnK0VlRXR6MXZOYXRXbUJsOWVyMWtiRlBCb253bUNBNzdJMXE2clloYXJaUG1pNW51bDRDWWxWRy9aNlFTZHZLUnRWeGRrb0c4Y21md0k4QTJ5K3R5RXhiR0xNd2h6ZHFKbXJlUjJ2enRiTWhZSytYakVibXRPNTZIUmd1L3VFclk4Kyt1ampiUENpNlJrcXVLRTd2MzFRV2R6UVlpSTduUklydXIzYUltZ0hUb0ZQMkJ6RXJWdUV2dHRIQVB3RmxZTFVMTmVIaHdCcWExL3B1SUdTWXEvWHpKcHFsNXN0dXNYTEtBci80NWtmUDZybXlnYmVNaGpobTdyemRGWFJUU0tIY1JtNEVhL0FSdHJIdHhNbEhvM2J3ZDJ0c3g1eFNIWWhVZ1FEWjN0ZXdOS2tqZXRaVnYxeXJsNnVtVUdHbm9nTGl1ayt6cXF3VW0yUEFYZmhDWGdMM2tXZTBYb0lmaUNhaURqWmFRRkhzRXVTb3JXTC83QzFnN2RIMk5xWElYYTRER1lWejhTMW02ZndHOC9EMjNGY3QwbFNiNTV0QTNzN3d3YVFHMXZwanh0eENuODdUL044NG5pMWMyZlhRUFhSUngrdkZwZ2YvZWhIK0ErODhHQkRIWThMNjBYOWg5L01wR05Da0dleWRac05CRURLMFJDSEMxVFRVMHhIc3p4c2l1R0F4em1XY3IwQWZtdTJTNWpGQlN0NTRCb1FER2dMVWpyYnNNYVNZaUxDUVpEYnRxZmFYcjVocFlLczd3WCs0TjBCbHFPcnVpTXlWTEZxUm1RMkZHUVZ3MVZORjM5eHUycTVUY05CTVBBaWlpS1VCNEltQjAwWHhJZkFOQXlYWnlpRzZYeVFXS3VhanUxUlhpQWU0WjJXL2RBTUIwNEF4OUkwM1duSGh5a3BOR3c4UjZTb3NNUisrMDVTRkJpQlkvQ1ErYUl4azVieUZUTVI1aEZCVUhQRGNFekh4eFBhcjNkOGY2Tm13bko0TG5rbW5vWnJpazNiQWJIYUhvN2dMcGk0cHVGcWxndEQ2TU9WOG4yS0psOFIxcXNtcmdIMUpxTTh6OU1xQ1QrSktXN0I5VWd2SHM5dGZYZlJIUkkxbUJQVDllcTZpdzF2RWZuT3h3Z2N4NzBJRXNKbXdEUStEeHZTcVBWcVloRVJPOFFkWnNHd1BKeUVPVGVJbVF4NEhzbGwvR1ZPODhOR0gzMzA4YlZBZDRlVE5pZUF5S0FjUXdKdDJWNmxZWUg5d2VPL1hhaHBwcHNJc3FXbTlZdlo2c01OQlJwWnQ5eG5SYVBVZEVDcmh1WFdWR2NscitWckJpZzdKck1Sa2NtRSthaklyaGVNVDJacjVQbUJRRkJnNGhFTzZoazh2bFl4ZUpacWFvN1pVdnIzVnBXUEZtcDRiMHJtbm1UVmYzdTNWS2hiR3NpUnRKWjRqN1A2aDNNMXlnOGtaUlpFLzVNSFpieGEwVjk4S3NERFJZRVE2KytXR2cvV2xFOGZWejk1WElIWDBqNjdFNGhtT3NvUHhZV0JNRGVaRW1xYW5TMGJOZDE1bk5YeWRSTmhFRm5xazhYR1Vra3ZWRTNkOUg3MnFPSzZQdHlGam5pbUFoL08xMWNLK3M5K1Y2eHJ6bHhSZjFiUWd4d0ovT3lHT3B0VjFpdUdZVzVUN1FqcVdzWDhkS0ZlYkZqeElBdjIvODE4N2ZPbFprT0RsZkpnb3FxcWMzKzF1Vkl4UU9zd2JGK3RLWC85b0F5M3FTWC9BekI3czFrdFZ5TmhDM0xVeHdoYmtZUU41emlhdnIrbXJKVE4xWW9KWmtkTy9lYXJDckxqM3BvS3VvZGxSazZWNjliY2FuT1BieUY5OU5ISEs0SnRxdjl5V3RSMGIzb29pTjIvdkYrK1BCaGN5Nmtoa1UwbnhHY2xQU0d5Y3l1RVQyay9FQS96NEUzSzg0dUtFd3R5aGFMeDY3dmxwWnhlcUJoclJZTmphS2g0TUJSWVhsSHNuM3lTWDhwcGtzQ0VJL3g4VVI5TGlSRDRSZFVCaTcwNUhsclowS3ROKzYycmNkdjFINjZyTXducDU3OHIzbjlXSDQ0SnVBeW1hSGxER1l3TDBLMmd0b21vOFBsczlmT245YkJBeDBJa0NCdEZYUlpaUEh5ZFNQSUEvSlVWdkpkbkdoV3pVREduaHVTeGdTQUxvZHZDcHVvWEtBcDBPVGtVeEVHaW13TVViSTloK1NHQmtZTU03RTI5WnY3aWkrSlNWcjh6SGZsZ3J2N1daRlF6Yk5YeUl4S3pzS1plR3cvanJxZDVQUlBtbHRZMVVPM29nRlJTYlV1elA3cGYrV3kyZG0wMDlEQ25EVVFFeTNMeDlxSmlRL1V6Z1FEOHA2Sm1ROE9YeStadkgxUU0zVTJHaVI5UUI3bGJYanpFUGNwcWozUGFzTXg5K2FUNmJGMGJqQXJKQ0tkb1RyMWh3Mk9Bc2N6WHJHYmQvTVhuUlp4OWN5YjY4eWZWdDZZaWF3VWpGdWIrcDErdHZ6TVpxWlNOUjR1TlordHFOTVEyUGYvMmFPaVhuNWUrZkZyTGxZMWN4VmpKNjJPREVqeWVkb0wwMFVjZnJ5WmVVUDh2bjFaSDRrSXN6RnVlZjM5TkhZM3h6YWFkS3hsWHgwSU4yMHVFMkh4QnJ6ZWRtNVBoSzJQaG9ZUlFVaHhRWGtSaWkwMkxZYW5yWTZFYmsrRk1RZ1NYRFNYRk1DU3Q1Vm11UjdrK1pQdTdOeExqZzBITEk4ejd6bFFFNWdRSExkZlBOeXhZZ2tTSXl5VEZ1YncrSGhjZXpoTk4vYTA3cWRHVU9CUVRIbVMxcE1TQksxY3E1bWhjZUxiUzFBM3Y3YXR4Y0RyVStrclY1R25Lc254WllncUtuUXp4QmNWaXFVQzlhaFdyNXJWeEVxUWdwUHZ6NW8yZTFBOVlqbC9URUIzeXdRT2FPQlZpUFRzd1BoQjgvWEpVRmhuRWRESXBndERmdkJTNXQ2WU1oRGllbzFtRy9ueWxlU2tsUG5qV2VHMDZvbnMrM3U1YWZpTEM0eTZFcmFxN21RaS9zS2JBTWpVdDBpaUV0SUlEZzdpclRidHRsbTVOUlliamd1R1F0aDJlZ2o4VFlPRnZtWjZsTzdyaEl2elh4a0k0c2xqU1l3SUxQMEN4dlpUTXVvNFBlL2JHbFJqQ0JxTXlsWkorODFVWk1WMG9HaU13SGt2TlRFSzRNaG9hU29tZytJV0NNWklRcm8rSGIweUVvektIRE1yRVJkak1kc1Q3NktPUFZ4TXZLTUQzQWttWm4weExBK0R1Q0tjMm5LVU5MU1N4S2RnQTB3VmgyYlkvbkJKQVdLa29EeGFEdGRCTTF6QmRVSzNtK0dYVDlTa0tGNE5Qa3hHZVljaVhnUGFUZ3lJem1Ra094SGpvWDRHaklEOC92RnRlemFxTTQ4VWs5dDY2T3BLV1ROdXRhSFk2TENpNmMyczZNcGFXSUpCbGdXMGFyaVF4cXU0V21oYTRGVCttUitTSlFTa1I1c0lpQzlMa09McFlNMkZJWEorMGdDdW01emdCeDRIdklrT2J0LzJHZllHNFlFT2tYQyt3VmpOaHRHWkdaV3lJcWNEVENabEV0dHF3Qlk0dUtUWnMyR0pXdzEydTV4ZXFKdFQ2WUp5SFJjblZ6ZUdVZUhVOE5Kd1VFZVU0bkEyYUFvOXZ0a2Z4TEdtTWdrdUVFQ0g4TXlQeVFFeUFqY1JwdC9WeG9sUzNPSVlpSHpESUNBQVJBUUM1RTViMkF6UWRxR3IyYXNWc210N01TT2h5SzJ3aUNSc0hHNGJvdzN0SnloeGVoN2Zjbm80aW0wRHhFWkVOQzB4UmQ1dU9od2NoZUZORFFWSG84MzRmZmJ6cWVNRUNVSWdna2Z2ejljOW5hNHVyNmxwZWo4cnNhNWNpa0lyZ0pyQVZPQlNTVnBaWU1EaUF2N2JyWlV0R3MyR0hPRnBncWFjRkRjb2RGMjllM3dhSURDU0Y2OEY1dUxXcE9hVzZDYVdmRG5PeElLdVlEczlTaFJxY0RVSndlREpZRDJZRGQrRUo0RUdFU3JlZ2RIMUNtUUVLZENrSlpQQUJ6dUl2WElTNlJrWVBVQUZLNUdoeWZTQVFqeElMRkEvRFhYZ2VpTjNoK1g1VGR5cXFMZk0wakJsU1JHVHB1bXJucStaU1R0c29HcXpuenk0M0Rjc05lQUdFR2RIQVEvTk5hem9scnViMGlNd0dCUVlQa1hrR3Q0TytWd3I2VWs3M2JXOHRwOVVVZTB0YlAzN0NQdnE0UFNKelNGaVNKalNKS2FLbW0rNW1yMzhjZ2ErQURhOXF2WTF5WE4reVBhUXM0cmdadHZXaXdiWEMxaDQ1Z2FjaHJVSVNnMnhDRXNGUWhVU0dDL2d4aVVGKy9IYWhEcyttM2J6VlJ4OTl2T0o0UWYzZ0ZNUDJ3Q09xN2tEL3pvd0diMStLVEdXQ1BFZURFMVdvZTVad0s5Z1pnQW9HOTNwdW9LbmFZSnlVeklvVUJYS0U3czVWVE1odzhEZzJrRXhQOG9Vb2huZ0h4ZU9CcE1HZUNoUnJWcENucTZvZENiS2I3Z0lJSGV5bkd5NW9FZXExcHRtU1NKUE9NSzN6dHV0REk0TVFvZkhCcVdCUC9JaUtqR3E3dk1nS1BJTUxXby9aQzdpaTFabkhCV3VEcGxYRGdjYUhCN0djMTlkTE9oZ1dWQjV3ZkVXelIxSWlETmpWakx4YU1VWUhwUFdxQ1pWZGJsaHdRVmlXVG9VNGdhV3paWE8xb09jcUJtNTBMRS9SSEpoUGtXZmE3d0o5Q3dpd1EwSWxDWjErUi9BSnlIKytiNUplUWFTUExEYkVrZHp3dkpjcW9rbmNHdGRQaG9oNTNnd2JFbzF5L1licWpLU2s5dlhBMXNZY3NIOHl4Q1dESE9QNnF1RTJkR2M1citGSG4vejc2T01WeHd1YWFJT2lBMUQ2MzdpWndEWXhHR3p6eUVDWVg2Mllva1JZRENRTFBscXRHbUFyS1AxVWhBZjN4VU1jU0R3UjVLSVNDelpjSytvZ01sd0Fzc01Ub0hDZnQza1FTd0J5SDB5SThCNTRubFphS2h2a1ZhNVpLWmw3dUtHT0RrcGdOMXdKNHA3TmFla3dYNmdZSUw1TW1KL05hcW1Zd0xTVVBnaHhzYVJMTEsyb052WWhrM0ZMdFduVFhnQlN2MkdTNWlPdytXWmp5eWF3anpCQWNVZERITTdxbGdjcXIrbE9tR2NXMWxYSDlrSWlpOWlCTk9FR0pTTmNJc3dsSS96VWtIeG5KZ3AxbjRtU0R3eGhtZE1zcjk2MElMZVJTakIrQXhGK0RRWXZFSkJGaHR3UzV1QnpqS2FsVzlQUldLanpUUldtRVo0QjNBdkVPUlRrUVBRd1dZV0dSVndOTDZBWkRxeHMwM0NMaXNWQXNNTUo4MzNFQWpZbUxqRklWU1JMR0dHcmtMQ1JocmhXMkNEd3A0YUNDRnY3RlZ1QjJ4RjdKRFdDRGJPVWlmSzRjVG1uRmV0ay9IVG5vajc2Nk9PVnhBdnFoN1RFWDRxaVlpRU91cnNsckR1QXBBVWxRVXFEWW5YYnEyck9ZdEgwdllETTBTTURVaGhPQVV2SG96ekwwUTdwQ08vVkZkdTB2U0RQZ0Z0TjM4K2tSTEFlam9DZllTRmlZVzVpV0lJQ0xUWHRRc09PUyt6c1NsUFJiUmlZZk5PT3hRVEQ4VFhMTFRidDJheWVEakpyUlFNQ09STWpINVpERWM0SkJIVExyYWpPZk1HUUdLcFN0eVFSTm9rbURVUmcvNFkxblJEd282dzY4RlFNMG95MERRZ0R6MUxwT0QrWUVobzYzbUl0bFEzNExrSWdVS2lhRU9rU3oyaTJsMG9LbVpRMFBDQU5wY1NodERRUUo2MU11QjJKaElSNm10Y2lFck9hMDBHbThRaHBWb0xOZ3htTFJOakJsRGc4RU15a2NaYzRtQlJoNXhBd2JIZ3ZERmlRbzJIU2tJeElLNlJrVFhPV0t5YkM2VG1lYWZ0dGE2VGFIc1ZSc3N6aTdFclpCUFVMRkZWdDJHQndiRGlMc0EyMXdvYUViWWNOeHhHMmRnNXVBbzlDTWpZTlJ3NHl5VGhQQm1YN0FVMTNrRHN3eVoyTCt1aWpqMWNTblI0K29BbW8wUkJIcnhiMG01Tmh5TS8yNlUwTVFuMDNyQWJwbHVObGExWTZ4RlZiTTlKd0F0TzB5Rnc5dU4zekFyYmxab3NHTk9sRVJvSkV4WEZvNFVTWU55SEQ2NVlna05ad0wwQm0yV3hvdU1XRmlCY3A2dEZ5YzN4QW1oa0pEVWY1Mlp6cStaUnV1OHNsNDgyeDBLY1BLcURLOTI4bUlqSTdHaE9YU2pvTWcrbTRheFZ6S2lrK1cxUEEycmN2UlFiam9tcTVodUhpUlFNeFlTUWxHTGF2YUM1TVdQc3pRenNXQ0NHZURLVVBwd0ZoS0xlRzc2WmwxdFRzcHl2cWNGSjg1M29jSkM1eHpIeVJERGhBK01HL3V1bmxxK2JjaW5KcFJHWUppVk1mek5iZW13emZtMjlNRFFmSEJpUXdPMTZSbHZsbkpUMEFpamRkV0VlNEJiV0dOYittUU9BanlnaWJCZGFPQ3RQcFlOTjBTM0F3WERJaURJNkxiM3JMT1gwa1RUNFJ3L1JHUkFhMmRyVnE0dTB3VGhtWm0xdFY0RXk4ZVNVV0M3THdHK1lMT3NJR1R3VmhNMHdQRnV2SlN2UFNTS2dDVGpmSld4Qk9HRXQ0RmRtYVhWSXNtTzE4d3c3eFRLbGlGaXZHWkVZbTMrRmJmbElmZmZUeGFxSkQ4WkNtTndabHNCdGtlL3ZJVm9EWEhOdUxzdlNneklZNVJxYXB4L04xa0JvTWhzaFFRWm9DK2VHNEJTVytyR2lHazBrSXNraStCazhseER2RE11MzRzeXZLM2ZtR3FUdDRTTE51WTFPYmpxWGFzODhhbno2cUpzUDg3OTFPc2t6QU50Mk15QTdJYkpDaHdZSS8vU1JYYWRxdnowVFRyV2srTGN0SjhnekNJTE1rREo4K0tLOFY5T2tSbWJSMFUxUmNaRUcxVGMxaFdRbzBOeHpoUVBCZnp0V1djNlEzVGhzSVVreGc1UUNGdDVNd05PeDhYdi9vZm1WMldSMUtDdCs0bVlCOEJtUFNucCtCQVdCb0JJTVAwRXRyeWtkZmxlc3FtZUVIbmdYTTNqKzhrM3d3WDRjck16TWFRdEsxSCs0NGJscGtzWThiWllZcGw0M1BIbGRoU21FSllyQ0NUYnVwT3JBY3J1dkpGSlVKc2xHQnhHSjFYZjNxV1FQZXh1WFJFQklOejRFMTBsUW5KYkZKaVEzNi91eGlBMncrTXlMRCtDRnNsSXV3Y2Uyd2lhMncvYllWTnRQeGhzUDgvTG9DODlZR0FnYjNKMGpUdURndTBNV2kvbml4SVV0c09rYkdaSFF1NnFPUFBsNUpFRzNZL2dYcC9lQlpBeG9XNmxMWW9mckI4dERwRHhZYnVaSmgyS1FISWFUbHBXRTVIR1N6WmVQaFVqTlhOdkMwd1RoL1pTdzBQaWkxeVFVc3RyQ2h6cStwdUgwb0lkeWVpYTRYOWZzTGpmWXpvWjlEQWdPMU96MHNrMTZZcmNuWFNuWHJzeWRWQ0hiYjhjSkI1dlowWkd3dzJBNlA2L3BWMWJrN1Z5dTNKdHFVQk9icW1EdzFKSVBPd01oemErcWp4Y1pBWExnNUZZNEV1ZldTL21oWndTMnZUVVZHQjhUMkd3SE5jR2RYbXJnWXZ5R05vYWJqSVM0UjVlRjJDRHdPa003MWp1TVY2OWFYVDJzd0pKcnB3WWxKUnJtM0lMcERaTDVsei9kL2ZiK2NMWnVJN0RkdkpUZlRpalN3R0RBMjFVTE5SUGc1amdhaDM1eU1qS1RFNWJ6K2FLbVJqZ28zcDhOaGljeFg4WFJGV2NwcnV1bHlESVUwdnp3V2FpdnhwWkl4dTY1S2ZxQ2hrbWxUNjRvem1CUnVUSWJ4RUNScE8yeWxodlhGRTRTTmZKcEczRk1SOXMycjhhak1mVFpiUlZLL2VUbDZkU0xVYXYwaVFacGRiajVlVWFwd0x3Um1OQ1hjbW83QUNXdC9ZZTZqano1ZVdieWdmbkFpbUZvU2FHalBiUzM5endFZVVYUXk0NExuazk2SFFaR1J3Q0UwQlJaV2NkejJLSW9DRDRKTU45dUxjQ25ZRGV5SjN5SlA0eFpjaG9lMHoxSUIwazZDNHlMUGJMNFI3RjlYYmRKTG4zUldJVFB0YkcxOWNsd2ZkR3kxdXZUZ1hyeHI4MTd3b0c2NHVCaHZRZkR3SXNRSXg3ZUdCMENRY0NVMi9FWUFtRlkvZXRCMGw3VUQ3VFpVMG5VSTEwTSs4eHdWRHBLUlpXM1VGTUxMdUFVSHQ2WVZGSGREc3hFOGhCOHBBd01wU3d6K3RoTEJ4WTkyMlBCTUpMVnV1YmdlUGdPaTBENk9KN1NwWC9USkI0bkxvekxpaTdQdGg3UmZBZXdXTnRobTAzSmhxTnFmSmRwQUlpaUdBMmNPTVVWK3dWUzBYOVJISDMyOHluaEIvWDI4ZE1DZ05RMXZ0YUN0NS9WMFRIanpTbzkrTzMzMDBVY2Z4OGMycWR2SHl3VzhFTXQwaXhXanFkbHdoanBIKytpamp6NU9HbjErT1Vkb3R4MEpIRE0rR0lUcWJ4M3JvNDgrK2poNTlCdDgrdWlqano1ZU9mUlZmeDk5OU5ISEs0Yys5ZmZSUng5OXZITG9VMzhmZmZUUnh5dUhQdlgzMFVjZmZieHllQlUvOHlMR0xsa2puYXozcTdWbXNheHJka1VqQzgwSEJTWVRJWU83Qkk2V2VNYnpYc3lmZk5wQU5wQlFiWjl1am1yTjNYK0VRVmkyNDNzQk1xRXBOdHNoRHlWRDMzb08xVHM4dk5hY3JNY3NONjMxOWp1L3p5ZmFnK1o2UlpOaUdiTHlSR2Z2Mk1BN0xES2hYbzlYVVFHeTVORUpwbE83akIwejc0UVRxaFM3cDNBM2tBSXNXZkRqSlpTWWRvcmgxUWdBZGtHWXRrczY0M1hObG5oS2FNKzBTT3JLbHJlaDhya3VXZDJFUGNid3pGZUkraEZQbERQZDl2SjFhN2xpNUJ0V1F5ZXowYlhuZE5Nc0Q2a0lvbzlJSkV0QlRHR0pTWWE0a1pnd25aSlExRTkxM2h1VXJiSnFMeFFNL08wY2FnSEZheURNVFNTbHNNQnNMbU93TDFDalBubldxR2xPWFhjUXdhakl3cEs5UFJsT2hRNjBkczBlUUdHQnZkeW9tWE1GM2V3MTNkTUJNWkVVcnd4SVNPMlhVWmNQQk12eEgyd29LQ283SnpsRnFCTWhkaW9weGNpMHJMMVhwRGdna0o2RzQ2MVZ6R2NsbE1FZTZSa1NtRGZHUTJHeWFFL255SkdCZDVtT3QxSXhsa3BHajFnZEdETnA2Y3FnUkhURU1ZSUUyakZzYjdGc3JGYk1nNnlyUWRKY1ptRnlSSjRlamdveVQ0UCt1T2ZUWjUwZUlKcldLc2FERFJWVnlTU1QzNUxaWHlTT21VZ0tiNHlIU2ZrOXpRS3NXdTZqRGZWWlVVZGFiZVZwTXBHQndFeW14R3VEc25qVU92MnFVRCt5RUptSFJGd3NHYm02Q2ZJcUtRNGtmK2YwRGlCTFJSYWxqU3dsQnVvZmpRdlhob1BJOHRNdzlRamJhc1VBV2MvbU5QQjE1MmdMeU9Pa3pFNmxwUnREOHBYQklONStrR3pXTGU5Zi9XeTFyTmg0R3FGK2lkU1ovL2o5VENiRVNUd2RFbG5tcUxGb0IvWG5qeXRQODNwN3pabWo0Zlh4MEIvZVRNRHppb1c0cmlrMHpnTmdPNzljVVg3MXBKb2p5eVIwRG00Q0dpQWlzZU1KOGZaWTZGSktrc1VqZUdVRVNENlV5YzhXRzQrejJucTFOd1BDRFgxdk92eWR5M0dVUm9pUDR4QU44bTR1cjM3d3BMWllOQTdDdGoyQkFMd3pHZm1qV3duTmNGTlJzdVJxNThRaEFhM3pjRVA5MjZlMWxZcDVrQ25FRVhWSU1aUi9NRjBtSWtSRUpoSWtXVEFVNVpINFI4eUFBd0NKOXRGQy9jKy9LRFowQjZvUlIvQ3VJTS9jSHBYLzBadHBGcmFackFwMUtnVVlTWVJDK0l2WjZrSkIxNUJoV3hJSjZSQVNtU3NEd2U5Y2pVOG5VWi9KZERpZGN3Zkd4YWQrWkI1c0pyaCtOcXZlWFZVV2lyM2wxUjVBWmw5S1MyOU9oTWJpNHZXaDRJazNBWUZvVUFkUXZMcDRmeE1vYW05TmhINzRXaW9rMEpIdHN3YjFoR0s0LyszL082KzI1aW5heEgvMTdXSE85UkgwcVNFNUVXbXZFSHhvUUh0OHR0VDhYMzY5QWV2U09YUWtYTXZJLytTdDlIcE9HeCtVeGxyeXYzUGlmQURxK0gvKzFjYTlWUVUvT29kMkFEVDgycWo4UjdlU296R3lCbzc0ZkMyMmd3TlcrZUc2OW45K25JTUQyam5VQzNBOS84VWZqamVhOW1CTWlNanNrYWZlQTNuOC9GSDF6MzVYUUhYb0hEbzg4TzZiSTZILzZOMkJSNHVOYStPaDBaUjB0UENBMTM1OHQvVHY3cGVPSUNEd1BuakFtYWlBV2duZk1SUGpKNVBTNlJtQW56NnMvT3VQY3AyZDU0QU8rOVBYMDQybUZROXhrME9kK1NWUEZzaW0vL3ZUL0svbjZ0WXVoUkQyN3p0WDR1K05oeDNYRzAxTGgyWC84MVhsVGh6Zy9hYmhQTTZxZjNXLy9CZDNTNURWUjlBN0tLWlA4OXFmL2E3NDQzdWxKem1OeUxNVE5aZVE4c2ptUm11eXVaNkFkektiMHhHTFFzMnFxZmJSWG80d1Z4cldWNHVONWJ4bUhKVzR3WGQ0empGNUgwQ1MybzYzbU5XK2V0Wm9UNlYzcmdBZnV0aTA5eTRxc0lKZnJhdVBzdXBHMmNoWHpIWnJ3S0dnR0I2c3k5NjhEOEI3ZzY5M2Q3NHh1NkljNFMyYlFERkRzaCtIOTl0bzU5MzhtbnB2dnJFYksrMEwwRFNTOTJpT0krN0JqU3RsNDRQWjZ2LytVZTV2SGxRZWJpaklMd1NzYzhYcGd6UllXZTVTamhUZ1JtczY5eE5IMDNTcktwbXFzck8vQTRycGxsVzczTFFXMXRWODFUd3NLVjFrNmtkYXFLWUR2L0l2NzVVL1hXb2NzOUI3TFFQd2YzeWNoLzhGTVhpQ3hheXQ0dmQrWWxXMVAxbHFzQ3oxNEZtZExINTVOUGdCeS9ZcUlMV2oxdGlUQmFLY0s1dTJmWVpWOW1DQWtnV1I3UHNCRWxlc1ZNeGN3OTRvbTdtS2NhaG9RRDVVRk92TGxXWm5mMCtBOGNGckR4WWJUYzA1YkEwL2NiU2xOY0t6VVNMckxyMWNvRkovOHF6eHIvNzkycStlVmd2Tk0xMStEbS95UFI4VlN0RlBQbE5ROXViemVyRzVseXhBZVNzcFZ0VWd5OUF1WnRYREN0S0xUUDJHQTFWRjlQNWM0Y1ZxTGNjQjBuYWpadjczUDE5djZJNnFPMmZKV0hnWlN2YVR2TTR3OUpNVlpWOVcyZ3N2blR4YTN6QlFYaEVPaUpwelIveUh3Yk9pRHJsbDJPNUtUdGQzOTl0MnduRjgySXl1ci9wN2dxemtESUY1bHRxMko1QjNUaXVpRm5GL3owWG1JVTBnNy82M0Q3UGd5ck5rL3paTzQzMndxVS95YWxIWnAzZ1VtMDVaZDJKaFR0VmRNRkxuNk1IUVdhRHg0Z0VsWUtHb2YvQ2tPbC9RZDZzc1laRzlNUlI4YXpMODdtVGtlOWZpZDhia1RGUkloM2pWOHRxZmRIb0NMRnhXbmVtRUNMa3F0bFozNlp3NEJ1WUsrbGZyWlBXWVBRQW5GOTdHKzVlaThPOUdCL2I2NUlBcmYvcW8wdFZlOGRaRTJETGN1bW9ud3Z3SWFSazhkTnQwRzJ0Vjg5UEZ6bUk3bXhCWStsb21pRmZNREVnSDJkNmNDSnVha3kwUnBYeHJLa0lXMitrODZiemdaNCtyTzV2Z3dpSVNqUml0em42QTlIY2FqWXRSZ2FrcGRyQzE3TTlCUHNTRHhJdE4rMmVQS3h1MUY3S09wZ0p4bWUxWjhMNS9MYjZhMXlBd3h3YURzZkRSRjlkRVhkaFp6R1NCdVRFa3Z6RWU2c3FqbnR2bEFRbTUzS2liMmJLSlFMeHhPYnB6TWRjREF1NzRrM3kzSm90STdJMWgrZlV4RXBpcGxKUU9jeExISkdRdUpEQVErSHVidmJydXpCWDFtME5CbXFaUHNPOE5hT1RlbXRMWmVZNmt6RjBaQ0packpuNlBEa2l0ZkcrZk9SazBEZmVqaFFicVdtZC9GNkNtcDhQOFZFSXMxc3lneUNZai9NR0RjV0ZWdjJaNzkxYVV1YnpXVTV3UGhMbS9menYxbjM4ejgzdFRrV0dKNHgydlZEVDBob1Bmcnc4Ri83TnZadjZUOXpQamlSZHJlMjBGaE02akRmVnBRVjh2NmFweHd1Mytld0RsSGlwZ3JxakZ3OXo4bW5xdTFESXE1eC9mU2s1SHhmRVFmNUROTjl6NVZjVjJQTERZaVZhWlU4UkFoUCtUMTlPWDB1Slc0a1VtckZZTnExVUlxazNyd0pLY1VrMzN5WmFsUXdHUm8vLzVOM1l0ZGFlSDRTai8rOWZqMDFHaEs0OTZibU1oM3RhY1dmaWRIaGsxY3VLWk41a1V2MzhsTmgwaGdabU84RzhOeTM5OE00SHRINytSL3ErL04vSlAzeG00UFNydkpucWdkVmJLeG8vdmx3dDFzNEY2ZjU0cXlLR0FrQytWaklwcTc4c3QwQkNremNkMEJJNHVWRTMvTUI3SXhhUitGSUo3cTgzNzYycDdnYkN0UUhtOWxKYStQUk5MQ1hTaG9LK3NxeXRaYlBwcVRsdkxhNnY0dmFHdXI2dTA2WDMzU3V5OTZVam50dTNRTFBmRGhYbzB4RDE0MWp6T2w3ZkRvbWs0WDIybzZiaXdVZEtoQVR0SHp3RTRob3BKVEs2b0lmVU9zaTJzS1hXVmZPT0xoVGoyQkFkSG5TYmcyWXdueGNta0pHMTNtQ0ROVU9ka0VjTGZhUjZzeFJVQ2RxVmlOcmQ4MzBheGhQY0F6MGs2ODNVYVlISmtqdDVBWGRpUlRUMDMyR3hGYzVCbmlmQnhoNG5zUkpDbkJUcXczZzRNS2lPcTU1clMzcklibXVRRjNoNE4vY1BYVTVjSGc1MGJ0Z01WLys2cU1wdlRzbFZETTg5T2xwMHNZTE9lNXJYUzl0YWUwYmp3N21RRWZ6djd6MUZXbllydVJFSWNNcVc5THVFQmNRR3BIL2xkVm13NHRyazZxdVEyb0tBaTdlNE15NklmeUJiMHhRMFZmbXRkc1hYTDVWcUxLVW9pV1FyUnROeEt6YVJNOTBaYXVqVXNkMjdlanFXeTNyQzlobWF2RlhYbkZMNTJSVVRTYlJsK2EyZS9CZmgzYXpVcnE5aWl3Q3hzYU9lcVl5NEViN1ZwVnhWYjBSM0Q4dmJlTE5zUENrd2t5RjBlbGJjdUozbk9nZXlZVElreGlheWV2NG1HN3RZTmwrTm8xWEJLRFd0ZjVZVmNxK24yd3cxMWErN0JkdDRZM2xYUG5qYmdmc0ZsZ2VsU2RMY3JwM1p1dHVPSEpEWWljOWNtd3FjeHpoR2Faak13MkZDb1NuVnJOYTh2UUpNVmRLM3BSR2pxcmRIUW0rUGhuZzFmRGQzNWRLa1pvQUx6NnlvWnV2WTFaSC9kOXJKMXM2dHo5bFJLK3M2VjJNUU9weER4cldpT0xMS0liS0VLLy9PZ0ViNkExQStiK2F5b3IvVWFKWmlTdVp1RHdSQk5yZVcwVW8yNDZaRWdPelVVdkRVVnZuTXA4dnBNOUkzV2Ruc21PalVjaEFCSkJibDNKc0pEVWI1ei94Ymc0VitzTkFmaXd0eWFRcHA5T29kUERPa0k5OTVVSkxQajFRM0QvV0pWZ2ZCZklhdTZueVBoM3dZY3o0bE04UGFsQ05KejN3MlhYUjRObmRLSW1OTUFQTHpSbUJDWHQzMlpjRHcvVjRjdEkxMWZEdExtZ3p5cmFjN2M5bVp1dUJRM2gyWDk1ZlZ6UmVDaGUxQVhEcGgzZDZZak02TnlUL0k5UGlEUlFoSXpQZHdKRFA3ZW1vN2NnT1pOaXdKTGFab1RZYWs3bWVDdDRXRFA5eThVOWFydVZocldTbDQvSjUzWkRnNFVIL2lSWVBPdDVRaWFZQ0RNRGNmNFZJaVR0bjljTVIwUHd0LzBmSXFtY2hYajRFUjBBYWtmRW42K29CZVY3bjVScUYxWEJvTVptZHNvR2hCb0hFc2xJL3pWOGRCYlYrUGZ1Smw4NDByMDZsaG9ha2llSEpJdmo0VGV2aHA3NTNwaUtDRk9KYVgzTC9WZUkvZittaHFMY09XR1ZhNmZmSi9pc01EY0dwRmZHd2wxdWRTVzR5MldkTlgxZUo3ZUtCbm5UZFNnakU0UHkwaTl0Ni90djcxNUpSby9oVWFEMHdPS0RSbmpIV1FGYmx1WTEydW03bmxCa1cxb2pxcnZROSt3SC9tNlZkMHlmSStocUhpUVRRWFp1bmJ3RGo4bkQ0bW40WVFkTU85UVg2RDlUeW5yOEZSWlpLNk1rV3FJZDMzalJ2eTdieVN4ZmZ0TzZ0MGJjY2dGdUY5UmdmbjJUSFFzMFdNeE8xVEczeTdVRXpISU12VTBlbDZlS2hENEozbTFzbjJzUUVKbWd4eGRWMnlabzFQaGJqbFlWdXlpYWtkREhIeHU4OEJqYmk0YTlTT2I4dzJMVk1VZFNRQUZQUjRYZE0ycE5TMkdwaE5oSGxMaXJhdXh3YmpBb3d4M1FCUkhhNk53TUIzako5TGl1NU5obUkzT1U3YWcyTFJVMjQwRXVlVzhaaDE3bEZNWFlQUkZEclpLQ25MZDdTR3E2WDJ5MUJ4S1NVOWJYMG83Ujg4TjJ1bDRRSFR1K1pxQWRPNXFXQU1oTGhia09vZGFLQ3EyWWhIcWIycE9xZDcrNk5zYjhNZXJxbjEvZTQ4UnFKQ1p0RlNvbXM0eDVzWTRQcEFablZ3NUdEcTNuUkk2TCtrQStnQWJ5MUJSbWJzMElyOTNQWFpwV0E1enpPL2ZTUFNzbTErdHE2TElPSzZYcjFwZkwrSHZ1UDVheFd4dTc2azVGaGM1UDdDVTFlQndEdTlvQ2FocWRsR3pZMkhPc3IzeW5zVnZLeTRhOVVOK1AxaFh0L2FaYXdNRmRUb3R4Z1VHU2htL2d5SUQ3WEJ6S2l6c0tUcHhpcWJKL0JpWDBsTG4wSFlzbE14b2lGM0txaWZlMVFkZWhPdjZzRGxUNmU3V1BiaDRjR3RvaHNJTFMvV1hxUk5mUVZScWRrcm1JTkk3K3kxNG5yOWVzd3c0M1pCZ2RXdVBVUmM0VWRQZFI5bnUxcDRiUS9MYUNZMCt1ZkJBcllSMGc3OTRaVFIwZVNCNGViQkgzWVNSZnBUVEVsSHU2WXB5cUkrZkx4MlE4RlhWMmRyY2cvaU94c2dIc2VXc3hsUDA0STRlbklidFZaVDJCMGQvbzJ5MlN0bit1R2pVejlCVVJiTVZzM3QwUTB4aVlUaE4wOVZNaDN5c0c1UmVtNDRjcEtVU3FSd1d1WGN2OWU3cWs2MlpvWllBaEdRN2NYRlJWMjFGc2Q0Y0QzZjJ0d0FSL01uanl1WHgwRWNQU3lmZTF0VEhIaWpXVEZURE9OenY3V1ZubzJacXJoZVdPY1Z3b0FNNlIzZkF0UDFDdzlvNmR3VWVBMjB4bVJMWGlrYm5VQjhIQTV5QWdRajNEMTVQZGZhMzQxblJpSVNFaGs2bU9qajdjVjVIZytYNEQ3TnFlWHU3SC9TZkxETGdybGJSY29JOGs1SjNDSCtkdFBuQTd5dzN6UGFBdTMxeDBhaGZ0ejNGY0hmbWRDckV5UnpkVkIwNGo2bVlNRFlvSGJ4ekF1bDlrWkc3T3R1MHNWbzFvVDVBdnZrS1RPOUpVci9qa1lqb2hpdXgxRVN5Vy9qYnJyOVlNaEJaV1dTenBVTlAzOUhIa1FGeFZXMWFTWm1KYlJmK0VHdUs2VWtpM1ZDY0lwbFFwVWVXd0RuSU42d3ZscFd0NStCM1hoa0k1a29HcEVNL0d3OExoaUhmUDlPaEhoMHhsa3BHSXNMRGRWN082UWR2QVgrNWdOQmNMQnFON1orTHh1SUMyeHJJRGU1Q3NScUtjQ003dW5pV0ZEdXJXSm1FQU5sUnFoK0lFQzRhOVZjMVcrczFHWGxjNWtTR1VqUWJpUnNMY1ptNFNCODQ2cUI4MU0rZFBXcUJmTjBTQllabDZHTDloQnRxWVVkQUJURHlwdXE4TTlIdGN5QnJtNGI3NlVwelBCTjg4S3grcm5wNVhtd2dvUXRWYXpnc3dPL3VIR29CbVpWdFFKbVJCVFFxamQ3dHJUaFcxNTJGb3Q3WmIwSGdxT3NaYWEyZzQyeVhJOS9IdmtDQ0JYbjJTcVpIbTAvVGNIVGJoUkF1Vm8xek5RaG1ENVNhVmtteDdPMmR4Y2VUQXV2NWRkV0crQlI0YWl3aERNZTZUUjFwODFFaDkwa0p5bFVPMU9aem9hZ2ZxaXBYdDhDSm5mMHRJRXVkMEJRY2JUaUo0U0FUSkNQeUR3RWtlaXE4N2N0ZUc2Ymp3YzdBZ1docURseU5FMlJnUEFxYllYbE4xVVpPZHhFTmdQSkJ4c0V6WkFiRXZUOHRuZzJRQ0dNRDBtaGFDb25iNVBERlE3VmhSUVF5dTBEWFo2S05tcVc2WGpqSTFUWFNKNzByUjdDbldkNTYxZHphR29rbndKMGZpUW9iWmRMYTg3S29YK1FaNU4xd1NneCtmY1pZYkFKTytWaXZJZERJQUxDL0tORElDOVRUOHkrUEVNYjVvbDdiM2lWSjR1bEVrSE5zVHpNSWQ4a0NFNUc0cE15RnQ5Y3kzRkxWbkxKbWh5UVdxdjhnRFJBWGl2ckJ3akI5V2k4TEwzSTBRNUdtTkZra28xRU9Mdm5iWUdreTRVOW5aenRNMjJOWk1xR1ZjUXFqQjJITTRNR3hQbG5icEhQb09UeGt0dXA4dWFxTUQwcVBsNVdYMitLdld4N0VMQmRrV1lrcHFQYlR2TGJicGlLVk9qZDlYUUY3ek5EVVFKZ1BDZHVLVVVXMUc0WUxya0V0TGUrWWlobGxvNnphc3psdGEwYUJ0a1pqZ21XNml1NkE5aytwbS96ZVFKaFhhaVlmNUNpQnlTbTc1dDFjUVVmRU8vZWNKeUROa3FIZWRiTnB1anhMZy9XYnVuUCt1L2tnblBNRnZiRzliODlnbU9kcENpU0FzMUFWNEM0WWdIU1lHNGgwSzFHd1FiWnBKYU84b3JrUUg1Mmp1K05pcWY3V3FpdzdHL3BSbzFESFVQbkFwTENLU01HV3AzaFFRSXUxN1cxbmZ6c00yK1VZR3E4QXFaMEcvOEpYZFczMzFvZ2MyV0Y3TE5mN2JLa3B5aXlJcHJMZnRPK25pcnJoL1BKcDdYNVd1NWZWZmpWWCs1c0hsYi8rcXZkR1ZwdXpQTmpMY3kvQ2RrVTdreWRUWXBjakNHbFpnTGRPQkQ3eHc3cGlpTEpYVnV5dTFoNnAxWDkzdFhXUVplalRHQnk3TDRwTisyL242dmR6MnBmcktqSnh0N3o3NmNQS2N0bXdISkozblR2UEIxQTlvWXM3Tzl1aG1oNVoyWmdpanRyTDdUaDdFQ0MwY0J5NzV1K2JTSW9VYWUwaEs0ZUQxdU1oTW5uZlVFd1lpWFczUHl1bWk2d1VCUVlzbEMzcCs5YXZDMFg5cUhRb21qdXBYMkFwaHFJZytaRWNQRXNKaDU5ckVMZUhkbWtqTWh5aStsRzh3R2luNFZRaUl4R2pkSWkvT2R3OWJ3bmUxakRkZTJ2cVJDYjRaRlU1MGMvTWh3T1NIYXJ3czZYR3Z0djlOYlhVdEZieUdxVHhLYVRXR1FIbEFkUVBSZGJWUklPcWl4d0ppbXhkdGJzME1rUkpvV0hWdDJnNjNJdENOUkVYc3EwT3g5Q25MMlVpQjgxeW4rUzFybXphdWYxdXFmRmdRNjAyN2VXc2RxNittbElCcW11WXhTWU14MFhkUkExcWFOdTZTNTVESUhRTEJSM0ZZMnVsWUdscU9DYjRyU1VCR0RvUUQwUDFrMkYwU1prYml2SkV6bTRCMktlbU94WGRGbms2V3piM2xhRVhpdnFSRWtUMTc0aXp3TkkwVFhyRklOVWdyTVREVXo5TWJuejd6QzJiUUExSEhvRDBJYzlQcVhUaDdla1E5LzVNZE9mb0ZkdjFQbnJXR0VxSnkzbGQyOUdsOVJ3Q2hidGN0Mzd6VldXbG9PMDAwbDhYUU5kSFJDWVY0c1R0QSs1S2lnM3Fqd1E1emZTMmZ1eEZ0U3cwclljYjIzcnVvOWlRUHRvK0JUNGwxTS9SY0M0NzU4NGZVTGJKMEtHNjljdTdwWTN5L3FMeXpFQXM2SzRldWNkQUpFUDJXUjdRT1hvdUFmSDBNS3QydGZiRWdpejhRdDF5d1MwaXo0UWt0dTBYSXNyeElBc0QwTDVzRTFYTldhdVo2YmpRMUoxOUp3VzVXTlJQVVZDU081c1M0RXBER3JndG9rSHRhdnVBaHdLdWorNUMvWERRMkZaKzRQbW5WeDhRN0V5RWYzdXl1NDgvM2dneWZaQlZKeUg4VjE1eWkvOEJBUXRwMk83Y3FvSy9uVU5mUzFBakNURXBieXNWcU1BbDFiWjh6M0hjYk5uWW5Nb04yVlRaTVc5UGtHTm1VdEpHaWJUMkNDeVpzdWI4OE9sdVFONkJpUjR2S2M1NVl0SXUvYnNUcmMrOG5kL25Fd2pkZXRWVXR5L0FONWtVQTQ3WDFDRDVTWmYwelZVQndBYkRjWEcwMTFSdXVhYVRpZ3FtNWU2N1pPT0ZvdjU5Z2JTQUJvQVE2T3dmQmwwOXJqWUJzd3huRWh1STdQUzRIMWtPamZuZVZHVG44QUxIOVg0NVc1MFpsUjh0TnR1RGlXVGgvS3BIbEZxa2t1OEZGS1BWTytwckMyVEV6SUMwYzJxL1hNT3FtMjRvU0NaMTJHd1cxMnczVjdmd3Q3M2JSbEJncm1XQ1Mxa1ZtU3Z3Wk5xN1EwdVNNd1JDaHJ3RDlhT01nMktRZzE4WElNQ3E0YlNWMzduRllza2dxYm85akZOSkNkVGY2dEVmU0VZNVVILzdPUEppS01LUDc1aS9DTUlQUXJBOXRtdXRzTS9NdnE4RTlVT0xvY1JDR3VBdlRaTXBVem9uRGd5a1lkZm5sMDJJUEEzT3h6bTRHNTFEcHdQNExnTVIvdVpJOXlUU2VHMmhhVDhyR3dNSjRmRnkwM1c5c3ljUW1xYmdGVTBreFgwM2tGMmxRWnJHenJmL2ZTREFINC9MSExkZFNSU2JoUHBqSVY3UjNYS2RyTGFCeU9icjl1UHM5bG1hVzNNeGVrVFR1WkFqc3NoRVNjVitDZlFFRzVhUXVhNXM2ckdseEd1RFFXaEozSUlZdGU4OUo5aXRLSWtjVGJwZXRLVEdlUVpwN1ZsWHUwWnloVVUySkRLa1Q0VGxjaHdkbGxoaHkwSVJNTVB4SU5zMXJoQ282YVROSnhIaDZ1b0w1ZEVURjRyNmtjVjhxN05OWi84NTJuS2NlUzZGajhDTUtENWRLeWRzUXVaWmE2OVpXMDRNQ0haQ1ppSDh1N2dHc0Z6djEzUDFLK09oeGF5bW4wbGd1b0JTK0NldnAvN2dldUx2WG92dnZRbCtZS1BZV3RmNHpPM1RpWU9qeWZnYWVHT2QvUlpNbXhTVjlpZi8xaVM2aEhZcW1nMVoxN21paFNCUFgwcUp1WXFKQzNtT0hrNGRZbmo1eVFLT3l6KzRuZHczNy83T2xWakE5bFlMclI1SzV5bnZVTmgzRzdFbHNvelQ2dHh4emdzYkFyaFUxclh0clQyak1ZSDIvZGFLdTFROHpFVzNkMGtIeTBFSUR1L281OU0wbkd6ZGpFZDQ4RUNoTlMvOWJyaFkxRTlhL1hwUVAxSDlSR2ZCbnlZam9RK3JXWEMxNC9xMTdkT29ia0xnYURKekVwbFM2blQ5ZFR4YjVKaVpnZUQxSFYxOUVLR0ZvcTQ0bnNUVHl6bjk3TGsveUROWEJpUkhzN1dHdGZlMnRLN1dXMnVSeXlJTFg2RjkrOWNVeVBDcGxOUzFwZ0tTUHQrd0lmeGxpU3pZQzlXbW11NUd6VUtkN0Z6Umdzd3psMUlTckNCK293aE5aS1NYVlJYaHJrMGxCRnZkSisvVW1yVzRycmJYNTRJQ1BYdlBjamVnc0hlbDdTWlFIVkJ6Q1MzQXRUbXZaUTNoUS9Hb2tFbDR0MVhicVpUbzJhU2hINHlmamdvdHAvQUZVSFV5VVdIbldEYkw4U3VhWTdvK3o5THJSYjFsOTNyalFsRS93TFBVenBaOHBDbmNQaEFOMkJrcGNZUTF0ZUEzZEgyQmFRTVp3RE1Vbm85M25vRnFRL0dGdnY0R0VmN2RCVm0zdlk4V0dwZEc1UGwxNWN5Wm41Z2xHTlJucmRYNzVsYVVPZnpkWlNOQzJDY1dlbXhBNHJmUGV2KzFBN0lqSGVKVFpLMnFiUkVwSzNiTmNDSkJGcEt0VkxlS1RYdStzSzFMVEt1TmhaVVl1dEswOEpDZ3lLUjJmRE00TXlBQVVNMEwrK1hkL0pwU3FKcTRHTEdkekFUUGo5bEd1dVozR2RRQzE2cmRpK3dJUFR2T0RLQ21oeHZxMXRVNkFaU1E0Umcva2hCblJrSzNwaU5UUThIUWptNG1VWW5KUkxxN2VBSjEzVm12VzhrSVY2cWJVTDJkb3p0d29hZ2ZwUkdaemZmNmlrdWE0OG1NQytRNzFhRUg5dm10UWVHOU9rdTFQNmlpNXFCa2tVdzQ1ZktGcDBNaHpneElPMWVvZ0huL2ZMa1psRm5TbmZ6c3ViOE5rZ2owUUZ5WXlzaFRRM3R0TUZHWFh0NTZoQ2NJZ2FVR1FseEUydGE1RUNxaERCOFJ4ZEVQRkd0V29Xa3RiaC9KQlNkcElpNmladG9PVVdldEZTTmVjbEtnNUFvOE01Z1E5OCs3WVhsNitCeFJQM3p1cGRZMEdGMUFqQ0lpYTVnZXFqeVM5OXk2bUxibkx4UzBydFllaU5TaTRoaUlRa0tJSmNXYTdUM1lVTyt1S2x1M0IrdHFzV214TzZpZkRNK3Vtc2tvcjV0ZXRibnJwTzRYaXZwQnZXR1I2YmwwQTBReHhENTRFemJBdFBkd2czb0RacVBRNkpHSXd6RUJIaGtxTUg3RDRUaUQwb1VDSFpXNGIxNktkaWxOQUl6eitZb3lPVVE2aXJ3VWpZTjNpanp6NXBYWUg3OC8rTU05dDcvN2Rqb2U1bDVPS0U4VXlJU0pwSmlKZEZ2aWZOTXE2VTRxSm1pV1UyamFFR0tkRXkyZ2xFNG54YzJsSTZhRzVaZWVGSGg5T01pK2MrMUFlU2VMaCs4cGNUcEF4VFBoc215M3JHMUlISU4wVnNqSzJiNHNrdjc5blJQbkRKcnB3V3V4dGc4Mk5oM3YzOTR0L3N1ZnJPeTkvZFZYNVoxclVobTJWMVFzcmpYaVpHMzNoZnd1RlBYRHNJL0VoRWl2WWJkSVhNMzFZcTBKbHJ2YTFQWUhGZkM4UUs1TytqWjBZVHdoMXBvV1pBV3FyaWljaGJMQUN5U2V2akVjMnJsc0wvREJiRzI4NVl6djI5UDU5QUFEaUFEc3UzV3UvcG9ERVpsS1NTTnhvU3MrUmNXdUd1NTRSdkpvNnRIR3RqVzVFSFY0Q1prSUIrckhMcHpVVEVJNEQwWVFRZGlhUVh0c25Sdk9CMUNkbitaN1VQOVlYSVFzc3h6a0FLb0RBbjBlaXh5bzQrNXFzM25TaTJ3M0RYZTVTdWFzWGk4YWVFWG42SFpjS09vSDRqTFhjL2JCalpxcDJGNHlJcml0UHZpZG93ZUQ0L3BQZDFsQmFXWkFiSy9FalMwU2JCV3cwd2RlTWhEbS92Qldzck8vQlUzVG1jMXJyaytXOXpwbk5mVENBaDUzVkdLNzVvTFZUYmVpV1lMQWNEeXpXTndtR3FCQXgrSkN2a3FHMnZNY1BaZ1VkL3JzZlJ3UVNNTW5PYzNvOVIxdU9pM1VtN2JyK2h4TGd3VFo4K0tvYklQdGtvYityVXYzbkFqQUEwc1ZjekFobXBaYjNiRktlUnNYamZwRmxnN3lQVHI1d0F4V05ZZHBmWkt0a202YUJ4WCtjSmNhdXZ1cko5WE8vblpBV1ZSYnZkUkZuaWJFZjFhbEMxSnhLaVh1bk1rWm9mMW9vZEh5UWpwSCtqaHQwRFNjUDJGb2U1c1BpdGQ2MWZ6MVhPMXBYak8yTDVzVUVaaVpwTGhlTUZCY1pKR2RHSlRPcHlBOS8yalh6Wjg5cnZhc3pQREdxZzNJTXRMV0h3dXhxSjZkRStjSmx1Y1hteFpDMmRrL0lSaVdsMjlha2tnajdodTdMT1YwMGFqZkQvZ3hpWk8zakgxb3cvUDliTjFVSEUvZ2FUS04zNEdIOXNFc3IxWE5yazdaYlV5blJNTndWWjIwMHFVZ0s4Nnd2d3JvSmhYaXZuVTUxdG5mZ3NXUy91TjdwWkpDQmhQMWNRWUFnMCtteE5FZFBheUxpdjNydWZvWEs4MnRHY0ZRY0JHNG1NUVdhOFFWRURnNmt4RDdkdm9JUUtxcWx2dmJoZnBTcjdxSjJwRUlzdVVhY2EzQ01pc0p6TlpPOGVjRUNOdnNoZ3BWZXVKVjFXc042MTJ2Vy9Fd3Yxc1h6NHRHL2FoYU00UFN3QTQ1REt6WHpKSm1rNm0xTExlaGRvK1o3Z2trR1h5Rm56OG1xeTkyRG0zQk42YWphd1hkc3NuczdlbTRjTVp1dThneHQwYmtuUk1KNkxiMzRWeTk1MUpsZlp3R2tPc2hrVWxIMkM3QllUbGtiRmRsKzNBUWlhY3pFVTdUSGNOeU9aWktSamo0aTUxemZSd1lxTHltNHkyWERmaFYrTkU1dWdYdlRrVXFOYk0xcndrMUVIdjVIYWg2QXRSL2YwMVJkN1QyakNXRTI2T2gxOGNPdE4wWkM0MGx4SjBORG9yaExwYU53YVRRMEJ4c08rbnVvaFU3bXFZbWsySW1pcnp1VG91bTZjNlhkTVh6Slo3WnVaTEdUaUN0NnJxTHN2WFZ1dG81dEFVUmtaMFpJRU55NEt1QjlBZmp3aGw3bElqZllKVC81a3kwczc4RllKekRmc251NHppQTRCaU1DRDNYY2V0Q1JHSW5FbUsyUW56d29NaU1wcVcrNUQ4c1VMUWh4UllLK2k4ZVY5ZGJFMHQwQVhYejFyQzhrdFBoc2tPV2phUkUvdkNUOVo0QkRNZGZxWmhkYXhlSkhQM0dXUGlkc2REdElma2cyOXVqNFRmR1FvbnQwd2dDa0lEWnVpbExMUFJydGxmbjF3dW9PTWdjNkFsaDUrd1dxR3lMSmVOeFFmTllNcVozdCtuWTJvQkJybXZPcDR2TkQyYXJQYSs4TXlycm1sT0g5eER3d3hLWGpKQWxGRHJuemdTZ0RKbW4zNWtNeDNmRXRJOHpCalRYYUVJWWpuYTMrWFFCQlNRbXNZTWhMbDh4S1lxU0JIWW9LZUpINTNRZit3RWM2WGhrMlVXb3NRK2VWRDlaYkhST2JNZHJvN0puZVZVeWFiWWZDYklEa0dYbjd4dXY1L3V3VzNXRHpDL1dPZFJDSmlLTVJnWFc5UzNWc1ErdzBiWTNHdWFtVXQzRGVzRmdWYzJwNlE0VXhucXh4d2ovQzBqOXFHQlhNOEhSdUxDVGl1R0R6K2EwaDNuZERQaU5Wb2RmcE1qV05FRXV1RDdwK0ErRCtlbGk0OS9kTDNYMXlHNERiUHZlWlBocGE1SmtNUDU0SmlqZ2JXZGV1cWdBQllIejFrVDNUTTR2QlloK1BNekZRaHdaTitlVGtyM24xcm5yWWdCeFQ0YzQrSnA3OTZtRm9FdUZPTmZ4RmMyQnB4aVJtZkE1TU52SUNvYWg0bUUrMmxyODcrWG1IWjR0OFBSZ1FreEZlV0dMVkVjbE5SMnZyTmp6QmUzenBlWmYzaTkrL0t3Mzd5T0YzeG9MemE4cWJjay9rU0hqWU0raGZZVU51N3ZhM05rci8vS0FhQmpPd3JxNnNLNnNGZlY4MWRoN0kyc25PUDVFVXR4SmQ2cnBMcFNOd1lSUWJ0am1qazVRRjVMNktmRCtwUUdwNTJxNm11VjkvS3orNTNkTHMza3QxeUNEN0dGNGNSQ2JZcm8xemM0M3JJV2kvamNQcXYvbTAzeTFOZHRNRjFDZXZuc2xyaXBPcmtMNnpISXNmWFZNM2xwTXp3d296eUdSL2Y3MXhCRVduemxad0RIU0hQL1dUUFQ2cFFqTDB5WEZSc0x1c1VHUG5IU25ocGNNaHFMU1lUNFYybXMrQmtqK3liaXdVVFJRU1NXQkhpT3RQUytma2l6YmN3UFVyY3ZSbVlsUWdOMC83MnE5R281UENycmxjaUw3emR2SjI1ZWpMRWRYVkJ0MGp5M2Z0Q0RhL3YyanlyLytLUCsvZnBoOVZ1elJnZ0VJTFAzN054TE51dFd1bXlMUFhCNlZ6K2ZYRkFRUE1lcjZpTmp1TW1EcXJtcTRzTWVUbWVDZG1lanJlMjdYeGtMRENRRmxUOXErYWhDZzJkNXl4UmlJaTQ3ckY2cmRVN21SeWRNN1B5OFFFS2VxWnY4L254Wit1MUR2SE9vRldXREdFK0pJdkRNS0RIYTRvdGhMWldPdFZ3TmlHNmlwUXpIaHYvbmU2SS8vZHNOc2ZlQ0ZyUGpCMittZWd3a09qci82cXZ4dlBzbDNkbHE0bEphK2Z6bFdLdW5saG5WN092ck85VmhQdHdLWnB4anVYM3haL01uRFN1ZlFMdmd2dnpXc1ZNM2x2SFpwV0g3M2Voenl2SFBpa0lEYStoOStzZGJaZVE2a0F6d2hsTC9PL242QVcvYTlhM0ZRNFRuc2Q5SEd2L2ovRnJxS3dWaEMvT0d0eFBxNm1pdWJQM2duZlgwODNKVWh5MlhqTDc0c2ZiYlVXNDBDMXpMQmYzUTc5ZkZYWlVWM0JwUGkzM3QzTUxLbGliWmRhUC9IRDlhZjVMb0hrZngzZnpMOW0zc2wrQXJmdnBPYUpoTmdITkZnL1BWWDVmOXJlekVENEtra1pBNWJaMzlQNE1VM1IrUWYzRWdFdWVPNnVYLzJXZUhIOTBxZG5SUENkeTdIM2htVFA3eGJCdG1oVElMM3YzVW5LZmVTZ0FmSFR4L0MzdVE2Tzg5eGVVRDY0YzNrMDhVR2N1MGJ0eEpUWksyRnpxa0RZcjFtL3N1ZnJNQ1VkdlpiU0lhNC8vQ2R3YlUxWmJXZ1IwUGN0MjRucDRma2ZaL3MrdjVjWHZ2ekwwcVBzOTFmSllkanduLzYzdURqaFVZcXhuLzdUbkpybmwxQTFROGdnakxQdkQwWkJvRjJEdldDWnJud0h6OXNMU09PRFpyaXM2Vm10cjdyRjJBOE5oTVYvdm03QTM5N3Q5aWVGMGtTbUhkdnhMdFc2VHRMSUNkaHdQN090ZmpMSFJia3dtcXFEaEx6Z05zWHkwM1ZjQjR2TjJzcURPNEZFUitaS0wrenc5VW00SmtsWmM1eDJ1dXNraDc5OGk2clBaOHh3SkpRK2wwWnROczJWOUIrdDlTMGJQZitRZ001ZUg2eURuWHpqMTVMdmo4VitXSzJoaGhoTnlTenIxK09TZng1L0JLR1l2RDVjbFBiMGRvems1Wk13OUVNRjlWNUtDbkdaS0tORUplOU41YW1ocUlDNUZUbktWdWd0c1o0RGlVRnVFRmRQZG92SnZVRFBFc1Q0WHd0Zm1Vd3VOdjNWeFJjMi9YaGM4RUdZTk10ejNROHNGam45SGJBWU1KRitNZHZwT2FYbXVYV1JOZ0NSNzk1SlJvUGJadEgrK3lCMkVVazl1MkpTR2YvSlFFTWpzUTg0S1paRGdyaW8rWG14dzhyRUxPZFIzek5nUklTbDduNExrdUVSeVYySk1vWHFpYUtGM2gvZkVBOEp6TWlJRHdvODEwWnRNZW1XbVRGcS9zTDlkOCtxTzQyVWY1WkFva1lFZG4vNGo4WUdwRzVMMmVyN1dtbEpZRjk3M284RVg3SmRYTTNJTlVlcktrN2wxSUJaZW02cTVzdUlqVVE1VU83bEtXZGdQNGJKTE40ZHNjVzFtV3hiR1JTSXQ1Rnl0NFdicnV3MUkvc2owck1wWlQ0M2N1eE44YkR3UjJEdkE0T1BBcTNmL05TOUh0WFl2bThuaXNUKzhsejlQaWcxUEsrWDM0YVNqejkzYXV4ODlsNXVTZElDYVFDaW1hdjV2V1hzcmJNYVFCYWZpd2g3SnhVdFkxWWtCMk5DVmt5WlJzbGljeHc2dXZhclpOa0ZoVm9xUFp5WGlOenpMKzh2RU5LcGtQY0Q2NG4vc2xiYVVkelZqYlVjdDFDY09DRjM1bUpUZ3dFejJBZTlhT2hZYmp3dExwVUprZ21JYk82NFVDTWNod2RDcklIcjlFb2V5aGdPNTFPNUU5WnRlR2RRYWV1Rmw0c0ZnMWNXT29IV0lhR0pKOUlDTytNaDc5M05YWXRFNVFPK1RrVWRWTmc2UnREOHQrN2xaaUpDNVppcitSUTNNbW5YZkQremFsSU9IZ3UxcXpnYUFwNjRRZlg0N3VWRlFoUzZBTHlRZTk0L1ROUTBTRlZwV1BZMFRZUW5uYlhLc3Z4RUtxTDhjRUpLVVBtV0NkZHk3ckxCRXBSVXViWVFLQ3UycWlsWVltTm5IbmZIcy96V1pvNmZvOEFoQitQSW5sbmU4aStyV3h5Y0tBY2NnelZjNUxkZllFQVpLSThhdVh2MzRpanpJK0hPVTkzbGpZMDhENU9pVHlEaW5sMUxCUVVtZk5wWEpGNjh3WGRnQVBWT2REQmFGenc0UkMzbHRlT2gzbjVNRi9DRU5OVWlKc1o2RzdmUmo1QitCZWJOc3BiZG51YkQvT2pILzJvOC9NaW90Vlp6ZWRoRWtVMkxEQXdqR0dKek9yc0I2ZzlDQWQxR0pWektNWlBwNlRyUThHWmhKZ1cyRUxGSUF0Z3RicjBnUGRmbTQ2TURpQnpUcUJ3SVJqNWhyVlJzK0N2U1J5RExTUXdSRDlHaFhLTkxMWXdHQmRIMHZ0MEFFY2x5a1I0NkFVRUNmSUJaZy9hcC8yMGhNeGRHWlFtWXZ3NkpMYnBwcUxDU0ZwQ0RlbmNlUmdncUpiakkwaDRDMm5jT09vMkhoY1J1NVhXZ21JenczSWtkT3lQaGljS0VCcVplOGYyQkxhVGhtU0cvYVE0R3VXcmRRdVZjM3BFVGtkN1RMY0o2akVkcjZHVE5vZk5lNUdiNHdueENueDV6WUhUTFlzTW5NV2VHUXBDYk0vd3ZIa3Z0blNZZTNjeThteGRCVmRNWm9JZ0JieWxjOE5oQUpJMkhkSmlnL3U3c3VOUTIwUkNHb3Z5YzJzcVRWUFg0RThUaGoxMGVGcUI4Y0JFcUl4ZHo5OXRRekVlamdtUU9OY3k4blJLSEk4TG95R084NmxzVVYvTTZTWktQa1BGd3Z6bFVmbjJwV2hFUHBHcVNjSlpiRnJaaHNWUW5icUpMU3lTd29BQTROVklCUEFBSk9iQjB3Q1ZhS0ZvckZSTlVQdm1NNk1TKzg1azJMZjhVdFZBTVpnWmtjY0hnd2Z2TjRpWHc1VHF0cjlTTWJZV252YVRaOUtTWjNzd2pTQ3V6WS9lRjdPSHoxWWdncFdtdFY0MExOdm5lQm9WcUtEWVpjMXR0QnlyOXRmYVRZQ0FlSllDdVVkRUpoUGlCa0o4aUtlaDlPYzNWTWNoZlFaUXZPQ3F2M2tsTnBJU1RxcXRGdlMzV2pIdnJTcVY1MzFKOGFLMHpNbFVZSFpKUVJidDBjTm5LMERLZGMyNXQ5SnNtQTZaR0tSem1IenhCdS9uaXNaYW5peUtlMnM2OHRhVldIVEg4TCtEQUlVRjFUVmJOMmV6MnFGWHZOa0NGRWREczU4dWt3WEYvdmo5UVZTZW85SFpLUUdsNHRHR05wdFRVVUxhUitnQU5SemxBcmEvbkZWVnplM1p3d2RBVnNLWGY1b2xuME0zVlIyb01SWGtaSWFhWDIwcXVqc1lGNzc3Um1vZzFzTnkySzRIQmZEeHMvclc3dDVSa1ptS0M1ODhJSk9VZmZmMW8vZndRVklqU0tzVmZRSEZvRnR4SGdMeElLYzFyWWVMVFk2bC92UzdJOG5vVWN3MkFtTzUvbUpSV3lsMWYzN2NEWGhMa0tkakVodms2R0xkcWl0MnJtelVGQnN2SjIxb0FwMk1DR09EMHUycGlIQnl2VG5Cd3JtRzlXQmR6YmJtWEdvREVSNEljUkpGUFppdlIyVHVDRDE4MW12bXh3c05oVlRTVHR4RmxwNk1DOHZyYXFGaUltRy8vWHJxTWl6YllmcHVvT3l0VmF4UEZ1dGRNME1FT2VaeVVwd0RMYWoyTzlmaklKTjJVQzgrOVFPSUk2cmM3QW9aSWlIeU5IeWZaRlNJeWF6cCtsMGp0b2h3RUZtR29XQXRjbVd6V0RQekZUSURGSW9kcUNrZTV1SVI3dTFyOFo2Szd6akFLNnFLamRlMUpubjBVUmxnb2xmeVdrTjF3a0gyenFYb20xZWpCNmxnbGFhOWxOVXN4dzJLTDFxaUZOMVpMeGc1RWhFUFl2L2RhM0dJTlVrOGV2Vm9xczdDaG9wYVIvanBTTVhIOHp4WVU5UDBZR1gvNUZ0RG1lVExHQkczSjVBanF3V2RWTTVXQlhHOVFMbHVMbTZvS0Vpd1VqOTRPNDAwN0Jsa1hJN0NrNitaYmlzcjhlOTVidXFvZS9ER29Oei84TDAwZnJRdTc0Ym5CYkpWbzlwOHNad1EwdmtKNlJEbENoenp2VGRUVTBQeWNjYW1JbXhMRzFwRGE5bnRvK1lkcWhJaWhmRDhzeCtNUk9WRENONnR3TXRMTlhNNXJ5c0hDd3dTQlA0V1NuaTF0YXBsUytrU05ZWWlIWktZVEZLOE9oWWVpSi9Lb0hwa3dVYkZjSitiS052eEN4V2ovWmtxSGVQZnY1V1lHRHdjOWFNNE5YVW5YNFVlN2RoNHVPUExPUjI4NzNoZU1zeUQrc2NHRHYwMUNDR3NnRWJxS0hzdlVoT2hYU3ZvenpaVUZKdXI0MkVVb1haZGV5V292dzBVVm5Ecmc4WEdLandBeThXdXhETXcydTJ6YmZPTDZ0cFFMZVJvTzgxeENLa0VlNERpQlVmcDNSc3g1UEZwbEMyOGFIRkQrL2hocGRRZ0M3OTBqa0xwUVA1SCtiZXV4aUFCRGxJT2tKbFAxNVNQSGxZVmlNN3RMVnE0bldPWlZJU0Q1RXoza3B3SEIycmc0K1htRjA5ckJnbnNsbmNjRWdnRGlPT0gzOHdrZTAyMzkzS0JpSDN3WldsdVhUV2hUcmRFa2FaSmo5N3Z2cDZHUzc1YkdoYXE1cGR6ZFZqSHJxK2d1RGNtYzdjdlJXL1BSSFl6ZGFpMFQ5YVVEKytWOWUzdnhlVkJnZjJEZDlNajZXTjVTQ0Q5Qnd1TkIwc044OWg1Rnd2eGYvcWQ0YTZGQ2c0T3ZMeXUydmZtNmpBa2NMTU9HQmlrRzE0Tk9jeXd0Ty81c1RBUGlydzVGU1pMcGJaT25UZ1FMdkRtUnc4cnNNZW9VNTJqTGNCK1p4TEM3OTFPRE1ZUE4vMHFucmxSMW45NXR3dzlBV08vQ1R5RDUra2JFK0U3TTlFampMekJZOWRMT3NvdHJPUFd4N1loY0RRSzdmZmZUcjl5MUE4ZzQyQm1JWHhnc1pGR0ZUS2ROMWxnQytXdXZjaGk2NThQMWMrMFJabm5oNExzUkNZNG5KS0dVd0lVQlU2UjQ2Y0FsUDZGTmZYTCtYcnRlUWxERFVmMlg1OE12VFlkMlUwazdnU3gvRTNyOHljMWxGUklKRVN0ZlJ6dURpcko2NWRqNFAxalRqU0hSNnE2ODJ4RG05OVF3Q0N0VkRzS1lOamV1QktiSGdvZVI4YWVIdXFxODlsc2RTV25hMmFuQTNzN1IyNU9SUzZQQnVGWHRhN3FBYkttVzhXQXlJRFNoNXBySDBSazRjQmRHdy9kdmhUWiswTUxpdWhuVDJwenEwcHpTN2ZYaU16ZW1vemNuTWE5eDhvODVCMmNqL2sxZFNtdkVjbDVqTHo3eHMzRStHQ1BPU01QRGhST09MdnpxK3BLa2ZTZTJEY3dlSlVrMENqQWtzQU94UGhrbEljUnBHa2E0dXcwU0g4VHB1MHVidWhmTGRiTERkdCtydE5SYU5OeDRlWmsrUEpJNkFpMEFGbncyZVBxNDJWbHE0M25PUnJWNGZYTDBXUkVPRnEzVk12MkY3UHFGMDhSMUcxZE9SSENrWlR3M3Mxa0p0N3BnZlpxVVQrQTZJSlkyNTBUSUZwMXc5Rk1ENm5mSUpQcmtxU2dBNVFvMG1HSlEyS2h2b2tjRGNjVzJjeHhwNkQydHdBQmc3T25HYVREZTZjT3RBWnJRR05pYSswZkZJaWdhaml1NDVNSlBKOW5MMG9TSW9KSG9aNTBEaDBEMEdpbTVhTktiSnFXSXdEVkZiNFVoMGllYXNvZUZZaWpxcnNtc1o1YmNvUWgwNjZKWkYzaHZRS05UQVRwbzVpOTBJa1VzUndTVCtQMnZlT0x0Mm1tYThCcjM1SzJLSTFCZ1lITk9INWFJVDdrS3hmaWRieThDMGxrdnFiTy9sR0JPRUk5b09RZlpHNGd2SXhpeUdxTHFJb29Oa2lUWXlmR2dZQWNBVjBnVTV3dGJoemVUZnBsQ016UlBpM2dPYWlrSGIvNStUTVJMd2hNWlBRUmJFa2JuYUFhRHZuVXREVkZTYjhEV3BiWXplci95bEgvSmhCdHhCMGFHYlcwdlhWT3RNUWRFZ2psQ3lXNzdXQ2VHVWdndG1mSTBkNis4em5BaVVlazUxc09oYk5NMnlPZ1J3UXBRa0FIeE02NmRmRDRIdWZlZzZCSDFBNkpFd3pQSVFKem1QUS9XZlFNNUhFUzRjUWZ1SW1lVHdhMlB2elZwZjQrK3VpamoxY1d4MnYwN2FPUFB2cm80MnVJUHZYMzBVY2ZmYnh5NkZOL0gzMzAwY2NyaGtEZy93YzJkK0J0ZFdlMzRRQUFBQUJKUlU1RXJrSmdnZz09IiBhbHQ9IkRldiBsb2dvIj48L2Rpdj48ZGl2IHN0eWxlPSJ3aWR0aDoxOGNtOyBoZWlnaHQ6MThjbTsiPjxpbWcgc3JjPSJkYXRhOmltYWdlL3BuZztiYXNlNjQsaVZCT1J3MEtHZ29BQUFBTlNVaEVVZ0FBQUpNQUFBQThDQUlBQUFEdHpSOTVBQUFBQVhOU1IwSUFyczRjNlFBQUFBUm5RVTFCQUFDeGp3djhZUVVBQUFBSmNFaFpjd0FBRHNNQUFBN0RBY2R2cUdRQUFBTjBTVVJCVkhoZTdaeFJqcHRBRUVRNUZlZmhOaHpHeWxWOEF2L3NEYXhJbGlWa2FWTTlYUXlEb2IzMndqbzlTVDlaR3p3VVBkQVB4bnlsT1FkMUV1WnFKY3pWU3BpcmxUQlhLMkd1VnNKY3JZUzVXbWxPUVozRU0xY3JZYTVXd2x5dGhEbDNOQTloS013NWhJb01HQXB6RHFFaUE0YkNuRU9veUlDaE1PY1FLakpnS013NWhJb01HQXB6RHFFaUE0YkNuRU9veUFDQlg0a3c1dzRxTWtBQTJqNCtQc0tjTzZqSUFJRXc1eFFxTWtBZ3pEbUZpZ3dRQ0hOT29TSURCTjVoVG1mNjMvNXVCRVVlZ0VBOGN6K0NObmNMYXNnQ2dmYzljeE9IN3JOcHI4ZTBmZXh2YmY4N2JlNURXZndCdTgrN04ycklBb0g5ekIydjdSTWRBNWV1SGJydTFxZnM5enBvSHpVcnZpUWYrTks4cjUra05uY0xhc2dDZ1hlWTA1bEdrT3N1NThQd2pRNW16S1BteFpkOHo5emZRQTFaSUVCelhmdlpOUGpjdWtPNm5PT1ZJKzF3U0Y4bks3b3RmNGUrdStXamV1YS9ldXpRc2c0bHg0clN3VzdReWFUcHpBeXQxT1ZJMldYTmMrK2k5U3ZGalFQVDEvbEZTbVl4Yno0M3FYUHAwbDc1TUpEemQ1ZXR6ZDJDR3JKQVlEU25GM2pFdVdHbEVRMnFFQ2N2SjdscUxtdkdMYTRqT1ROSFoxSlFXdHVFRFZuVFpBS015SEU2YTVxZ21ZMUlCMHNCMnZlRnR0WGkxb0hZR09mRlQyTWFYSnNYZHlKR3lqcXo4eHd2ZUI3WUJUVmtnUURONlFtUTBvRnV2enBpZ2xDK2JmTzlielQzeVpHSlY0by8yTFVjdWRzMVBuQzY0SXlmMmNWcmM3ZWdoaXdRZUljNW5VbkExY3JkcXFRRERuYS9uaHpKWVBENTRtV0Z1MTNMa1drWHl1SzVsQzBaVklVL2d4cXlRT0M1MVZKVytJRXZnOHUxVWJmTEVRTXVZaVB5RmUrQkRYOGtpbFZvdm1waE9jNFpYZG1tVms2c0ZPOTc4OEN5QXJjWDh5N0M0L2tvS2MrbUZOVVMydHd0cUNFTEJMNThRNkVNbkJzRCtKbGZOWGUrNExxYThZNHMwWmx5Ym9KRjIxdCtjUmpINzk1QnBHVXBLVy84TW5oSUMyTjV5NjhWYjNzOXA1VUR5MTduN2J0NWkvRzBJYXBTTlNrb2M4bGRyUG43TjVUdHFDRUxCR2hPMDhGZWFITzNvSVlzRUtBNTNrazdmWFR1ak02a2MvenpmL1AxYmlRVk0wRWduam1uVUpFQkFtSE9LVlJrZ0FETWdURG5EaW95WUFneC9odTRnWW9NR0FwekRxRWlBNGJDbkVPb3lJQ2hNT2NRS2pKZ0tNdzVoSW9NR0FwekRxRWlBNGJDbkVPb3lJQ2hNT2NRS2pKZ0tNelZTNWlybFRCWEsyR3VWdUovYkt1VmVPWnFKY3pWU3BpcmxUQlhLMkd1VnVMZHNrNU9wei9TR3lxdjRiQnoxd0FBQUFCSlJVNUVya0pnZ2c9PSIgYWx0PSJBYm91dCBBdXRob3JpemUgSW1hZ2UiPjxicj48Y2FwdGlvbj5BIHNlbGVjdGVkIEFib3V0IEF1dGhvcml6ZSBCdXR0b248L2NhcHRpb24+PGJyPjxwPlBvc3Rib29rIEF1dGhvcml6ZSBpcyBhIHVzZXIgYXV0aGVudGljYXRpb24gYW5kIGF1dGhvcml6YXRpb24gc3lzdGVtIGRlc2lnbmVkIHNwZWNpZmljYWxseSBmb3IgdGhlIFBvc3Rib29rIHBsYXRmb3JtLiBJdCBlbnN1cmVzIHRoYXQgb25seSB2ZXJpZmllZCB1c2VycyBjYW4gYWNjZXNzLCBtYW5hZ2UsIGFuZCBpbnRlcmFjdCB3aXRoIGNvbnRlbnQgb24gUG9zdGJvb2ssIHdoZXRoZXIgaXQlRTIlODAlOTlzIHBvc3RpbmcgdXBkYXRlcywgbWFuYWdpbmcgcHJvZmlsZXMsIG9yIHVzaW5nIGludGVncmF0ZWQgdG9vbHMgd2l0aGluIHRoZSBwbGF0Zm9ybS4gQXQgaXRzIGNvcmUsIFBvc3Rib29rIEF1dGhvcml6ZSBoYW5kbGVzIHR3byBtYWpvciB0YXNrczogYXV0aGVudGljYXRpb24gKGNvbmZpcm1pbmcgdXNlciBpZGVudGl0eSkgYW5kIGF1dGhvcml6YXRpb24gKG1hbmFnaW5nIHVzZXIgcGVybWlzc2lvbnMpLiBXaGVuIGEgdXNlciBzaWducyBpbnRvIFBvc3Rib29rLCBBdXRob3JpemUgY2hlY2tzIHRoZSBwcm92aWRlZCBjcmVkZW50aWFscyBhZ2FpbnN0IGEgc2VjdXJlZCBkYXRhYmFzZS4gUGFzc3dvcmRzIGFyZSBlbmNyeXB0ZWQgdXNpbmcgbW9kZXJuIGhhc2hpbmcgYWxnb3JpdGhtcywga2VlcGluZyB1c2VyIGRhdGEgc2FmZSBmcm9tIGJyZWFjaGVzIG9yIGxlYWtzLiBPbmNlIGF1dGhlbnRpY2F0ZWQsIFBvc3Rib29rIEF1dGhvcml6ZSBhc3NpZ25zIHNwZWNpZmljIHBlcm1pc3Npb25zIGJhc2VkIG9uIHRoZSB1c2VyJ3Mgcm9sZS4gRm9yIGV4YW1wbGUsIGEgcmVndWxhciB1c2VyIG1pZ2h0IG9ubHkgYmUgYWxsb3dlZCB0byBwb3N0IHVwZGF0ZXMgYW5kIGNvbW1lbnQsIHdoaWxlIGEgbW9kZXJhdG9yIGNvdWxkIGhhdmUgYWRkaXRpb25hbCByaWdodHMgdG8gZGVsZXRlIGNvbnRlbnQsIGJhbiB1c2Vycywgb3IgYWNjZXNzIGZsYWdnZWQgcG9zdHMuIFRoaXMgc3lzdGVtIGVuc3VyZXMgb3JnYW5pemVkLCBzZWN1cmUsIGFuZCBjb250cm9sbGVkIGFjY2VzcyB0byBkaWZmZXJlbnQgc2VjdGlvbnMgYW5kIGFjdGlvbnMgd2l0aGluIFBvc3Rib29rLiBUaGUgc3lzdGVtIGFsc28gc3VwcG9ydHMgbXVsdGktZmFjdG9yIGF1dGhlbnRpY2F0aW9uIChNRkEpLCBhZGRpbmcgYW4gZXh0cmEgbGF5ZXIgb2YgcHJvdGVjdGlvbiBieSByZXF1aXJpbmcgdXNlcnMgdG8gdmVyaWZ5IHRoZWlyIGlkZW50aXR5IHRocm91Z2ggYSBzZWNvbmRhcnkgbWV0aG9kLCBzdWNoIGFzIGEgdGV4dCBtZXNzYWdlIG9yIGVtYWlsIGNvbmZpcm1hdGlvbi4gRm9yIGRldmVsb3BlcnMsIFBvc3Rib29rIEF1dGhvcml6ZSBvZmZlcnMgYSBmbGV4aWJsZSBBUEksIGFsbG93aW5nIHRoaXJkLXBhcnR5IGFwcHMgYW5kIHNlcnZpY2VzIHRvIGludGVncmF0ZSBzYWZlbHkgd2l0aCBQb3N0Ym9vayB3aGlsZSByZXNwZWN0aW5nIGF1dGhvcml6YXRpb24gcnVsZXMuIFRva2VucyBhbmQgcGVybWlzc2lvbnMgYXJlIGNhcmVmdWxseSBtYW5hZ2VkIHRvIHByZXZlbnQgbWlzdXNlIG9yIHVuYXV0aG9yaXplZCBkYXRhIGFjY2Vzcy4gUG9zdGJvb2sgQXV0aG9yaXplIGlzIHNjYWxhYmxlLCBtZWFuaW5nIGl0IGNhbiBoYW5kbGUgdGhvdXNhbmRzIG9mIHNpbXVsdGFuZW91cyBsb2dpbnMgYW5kIHJvbGUgY2hhbmdlcyB3aXRob3V0IGJvdHRsZW5lY2tzLiBUaGlzIGVuc3VyZXMgc21vb3RoIG9wZXJhdGlvbiBmb3IgbGFyZ2UgY29tbXVuaXRpZXMgYW5kIGVudGVycHJpc2UgdXNlIGNhc2VzLiBJbiBzaG9ydCwgUG9zdGJvb2sgQXV0aG9yaXplIGlzbiVFMiU4MCU5OXQganVzdCBhIGxvZ2luIHN5c3RlbSAlRTIlODAlOTQgaXQlRTIlODAlOTlzIGEgY29tcHJlaGVuc2l2ZSBzZWN1cml0eSBiYWNrYm9uZSB0aGF0IGtlZXBzIFBvc3Rib29rIGZ1bmN0aW9uYWwsIGZhaXIsIGFuZCBzZWN1cmUgZm9yIGV2ZXJ5b25lIG9uIHRoZSBwbGF0Zm9ybS48L3A+PC9kaXY+PC9ib2R5PjwvaHRtbD4=");
        }

        private void button45_Click(object sender, EventArgs e)
        {

        }

        private void crashsymbalbeatlab_Click_2(object sender, EventArgs e)
        {
            BeatLabwmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\CRASH.mp3";
            BeatLabwmp.controls.play();
        }

        private void floortomdrumbeatlab_Click(object sender, EventArgs e)
        {
            BeatLabwmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\TOM.mp3";
            BeatLabwmp.controls.play();
        }

        private void DesignerCopilotDesignerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            designercopilotboxdesignercode.Show();
        }

        private async void sendpromptdesignercopilot_Click(object sender, EventArgs e)
        {
            // Clear previous output and show a loading message
            responcetextdesignercopilot.Text = "Generating/Modifying code, please wait...";
            responcetextdesignercopilot.Text = ""; // Clear non-code output
            sendpromptdesignercopilot.Enabled = false; // Disable button during generation

            string userPrompt = prompdesignercopilot.Text;
            string existingCode = CodeAreaDesignerCode.Text;

            // Call the updated API client method
            GeminiResponse apiResponse = await GeminiApiClient.GenerateCodeFromPrompt(userPrompt, existingCode);

            // Display the generated code and non-code text in separate textboxes
            CodeAreaDesignerCode.Text = apiResponse.Code;
            responcetextdesignercopilot.Text = apiResponse.NonCodeText;

            sendpromptdesignercopilot.Enabled = true; // Re-enable button
        }

        private void hidedesignercopilot_Click(object sender, EventArgs e)
        {
            designercopilotboxdesignercode.Hide();
        }

        private void signinwithgithubdesignercode_Click(object sender, EventArgs e)
        {

        }

        private void sysappdownloaderpkglink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*AppInstallerForm installer = new AppInstallerForm();
            installer.Text = "NATO OS 7 - App Installer";
            installer.Show(); // non-modal, change to ShowDialog() if you want modal
            */
            OpenAppInstallerGroupBox(sender, e); // This line calls the method to show the groupbox
        }

        private void kickdrumbeatlab_Click(object sender, EventArgs e)
        {
            BeatLabwmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\KICK.mp3";
            BeatLabwmp.controls.play();
        }

        private void tomdrumbeatlab_Click(object sender, EventArgs e)
        {
            BeatLabwmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\TOM.mp3";
            BeatLabwmp.controls.play();
        }

        private void snaredrumbeatlab_Click(object sender, EventArgs e)
        {
            BeatLabwmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\SNARE.mp3";
            BeatLabwmp.controls.play();
        }

        private void hihatcymbalbeatlab_Click(object sender, EventArgs e)
        {
            if (isHIHATopen == false)
            {
                BeatLabwmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\HI_HAT.mp3";
                BeatLabwmp.controls.play();
            }
            else
            {
                BeatLabwmp.URL = "C:\\Users\\Alex\\source\\repos\\NATO-OS 7\\NATO-OS 7\\OS\\SYSTEM FILES\\BangLab\\assets\\HIHAT_OPEN.mp3";
                BeatLabwmp.controls.play();
            }
        }

        private void explainsthepurposeofkbddrumslinklabelbeatlab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Form passwordForm = new Form())
            {
                passwordForm.Text = "About Sneaky Deaky Features";
                passwordForm.Width = 681;
                passwordForm.Height = 507;
                passwordForm.StartPosition = FormStartPosition.CenterParent;


                PictureBox picture = new PictureBox { Text = "Cancel", Left = 1, Height = 507, Width = 680 };
                Button cancelButton = new Button { Text = "Cancel", Left = 1, Top = 1, Width = 80, DialogResult = DialogResult.Cancel };
                picture.Image = Image.FromFile("C:/Users/Alex/source/repos/NATO-OS 7/NATO-OS 7/OS/Packages (x86)/Sneaky Deaky Features/about.png");

                passwordForm.Controls.Add(cancelButton);
                passwordForm.Controls.Add(picture);

                passwordForm.CancelButton = cancelButton;

                if (passwordForm.ShowDialog() == DialogResult.OK)
                {

                }
            }

        }


        // Event handler for right-click
        private void objectControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (isSandboxEnabled && e.Button == MouseButtons.Right)
            {
                Control clickedControl = sender as Control;
                if (clickedControl != null)
                {
                    string objectName = clickedControl.Name;
                    ShowScriptEditor(objectName);
                }
            }
            else if (isSandboxEnabled && e.Button == MouseButtons.Left)
            {
                Control clickedControl = sender as Control;
                if (clickedControl != null && objectScripts.ContainsKey(clickedControl.Name))
                {
                    ExecuteScript(clickedControl.Name, clickedControl);
                }
            }
        }

        // Show the script editor for the selected object
        private void ShowScriptEditor(string objectName)
        {
            using (ScriptEditorForm editor = new ScriptEditorForm(objectName, objectScripts))
            {
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    objectScripts[objectName] = editor.ScriptCode; // Save script
                }
            }

        }

        // Compile and run the code dynamically
        public void ExecuteScript(string objectName, Control control)
        {
            if (!objectScripts.ContainsKey(objectName)) return;

            string userCode = objectScripts[objectName];

            string fullCode = $@"
using System;
using System.Windows.Forms;
using NATO_OS_7;

public class ScriptClass {{
    public void Run(Control sender) {{
        {userCode}
    }}
}}";

            // Create a Roslyn Compilation
            var syntaxTree = CSharpSyntaxTree.ParseText(fullCode);
            var references = new MetadataReference[]
            {
        MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        MetadataReference.CreateFromFile(typeof(Control).Assembly.Location),
        MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location)
            };

            var compilation = CSharpCompilation.Create("ScriptAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(references)
                .AddSyntaxTrees(syntaxTree);

            using (var ms = new System.IO.MemoryStream())
            {
                var result = compilation.Emit(ms);
                if (!result.Success)
                {
                    string errors = string.Join("\n", result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error)
                                                                        .Select(d => d.GetMessage()));
                    MessageBox.Show($"Compilation failed:\n{errors}", "Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ms.Seek(0, System.IO.SeekOrigin.Begin);
                Assembly assembly = Assembly.Load(ms.ToArray());
                object instance = assembly.CreateInstance("ScriptClass");
                MethodInfo method = instance.GetType().GetMethod("Run");
                method.Invoke(instance, new object[] { control });
            }
        }

        // ==========================
        // 🚀 NESTED SCRIPT EDITOR FORM
        // ==========================
        private class ScriptEditorForm : Form
        {
            private string objectName;
            private TextBox codeTextBox;
            public string ScriptCode { get; private set; }

            public ScriptEditorForm(string objectName, Dictionary<string, string> existingScripts)
            {
                this.objectName = objectName;
                this.Text = $"Editing Code for {objectName}"; // Window title

                // Initialize UI
                InitializeUI();

                // Load existing script if available
                if (existingScripts.ContainsKey(objectName))
                {
                    codeTextBox.Text = existingScripts[objectName];
                }
            }


            private void InitializeUI()
            {

                codeTextBox = new TextBox
                {
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Dock = DockStyle.Fill,
                    Font = new System.Drawing.Font("Consolas", 10),
                    Text = "// Write your script in C# here\nMessageBox.Show(\"Hello from " + objectName + "!\");"
                };

                Button saveButton = new Button
                {
                    Text = "Save",
                    Dock = DockStyle.Bottom
                };
                saveButton.Click += SaveButton_Click;

                this.Controls.Add(codeTextBox);
                this.Controls.Add(saveButton);
            }

            private void SaveButton_Click(object sender, EventArgs e)
            {
                ScriptCode = codeTextBox.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void ImportDatabase()
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Connection string — adjust Data Source if your SQL instance name is different
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Alex\source\repos\NATO-OS 7\server\NATOCloud.sql;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(1) FROM Users WHERE Username=@username AND Password=@password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 1)
                    {
                        MessageBox.Show("Login Successful!");
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting to database:\n" + ex.Message);
                }
            }

        }
        private Dictionary<string, Control> appControls;

        private void InitSystemAppDataBase()
        {
            appControls = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase)
    {
        { "games",GamesBox  },
        {"msinternet",InternetExplorerBox },
        {"ixview",ixviewbox },
        {"magnif",MagnifierBox },
        {"mail",MailBox },
        {"media",MediaPlayerBox },
        {"mspublisher",microsoftpublisherapp },
        {"sysmedia",MediaPlayerApp },
        {"msword", microsoftwordbox},
        {"natoptlsignin", NatoDesignerSignInBox },
        {"natoselect",NATODesignerComboBox },
        {"natodev",NATODevelopersApp },
        {"openslate", OpenSlateBox },
        {"pswdmgr", passwordmanagerbox},
        {"power", powerbox },
        {"mspowerpoint", powerpointbox },
        {"printtopdf",PrintToPDFBox },



        // Add or remove entries as you create/delete controls in your NATO-OS.
    };
        }
        

    }
    public class DelayProvider : ISampleProvider
    {
        private readonly ISampleProvider source;
        private readonly List<float> delayBuffer;
        private int delaySamples;
        private int index = 0;

        public WaveFormat WaveFormat => source.WaveFormat;

        public DelayProvider(ISampleProvider source, int delayMilliseconds)
        {
            this.source = source;
            this.delaySamples = (source.WaveFormat.SampleRate * delayMilliseconds) / 1000;
            this.delayBuffer = new List<float>(new float[delaySamples]);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = source.Read(buffer, offset, count);
            for (int i = 0; i < samplesRead; i++)
            {
                float inputSample = buffer[offset + i];
                float delayedSample = delayBuffer[index];

                buffer[offset + i] = inputSample + delayedSample * 0.5f; // Mix delay effect
                delayBuffer[index] = inputSample;

                index = (index + 1) % delaySamples;
            }
            return samplesRead;
        }
        
    }
    
    delegate float afabe();
}
