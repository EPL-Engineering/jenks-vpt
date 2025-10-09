using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Serilog;

namespace VPTInterface
{
    public partial class MainForm : Form
    {
        private VPTNetwork _network;
        private ProjectionSettings _projectionSettings;

        private string _logFolder;
        private string _logPath;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _network = new VPTNetwork();
            //_network.RemoteMessageHandler += HandleRemoteMessage;

            if (!Directory.Exists(FileLocations.ImageFolder))
            {
                Directory.CreateDirectory(FileLocations.ImageFolder);
            }
            fileBrowser.DefaultFolder = FileLocations.ImageFolder;

            _projectionSettings = ProjectionSettings.Restore();
            ShowSettings();
        }

        private async Task StartLogging()
        {
            _logFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Jenks",
                "VPT",
                "Logs");

            if (!Directory.Exists(_logFolder))
            {
                Directory.CreateDirectory(_logFolder);
            }

            _logPath = Path.Combine(
                _logFolder,
                $"VPTInterface-{DateTime.Now.ToString("yyyyMMdd")}.txt");

            await Task.Run(() =>
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File(path: Path.Combine(_logPath),
                              retainedFileCountLimit: 30,
                              flushToDiskInterval: TimeSpan.FromSeconds(30),
                              buffered: true)
                .CreateLogger()
                );
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            await StartLogging();

            Log.Information($"VPT Interface v{Assembly.GetExecutingAssembly().GetName().Version.ToString()} started");

            Log.Information("Starting discovery server");
            _network.StartDiscoveryServer();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _network.Disconnect();
            Log.Information("Exit");
            Log.CloseAndFlush();
        }

        private void ShowSettings()
        {
            fileBrowser.Value = _projectionSettings.imageName;

            backgroundColorBox.Value = Color.FromArgb(
                (int)(_projectionSettings.backgroundColor[0] * 255),
                (int)(_projectionSettings.backgroundColor[1] * 255),
                (int)(_projectionSettings.backgroundColor[2] * 255)
                );

            distanceNumeric.Value = _projectionSettings.distance;
            widthNumeric.Value = _projectionSettings.width;
            monitorNumeric.Value = _projectionSettings.monitor;
        }

        //private string HandleRemoteMessage(string fullMessage)
        //{
        //    string response = null;

        //    var parts = fullMessage.Split(new char[] { ':' }, 2);
        //    string message = parts[0];
        //    string data = (parts.Length > 1) ? parts[1] : null;

        //    Debug.WriteLine($"message received '{message}'");
        //    Log.Information($"message received '{message}'");

        //    switch (message)
        //    {
        //        case "Open":
        //            Invoke(new Action(() => OpenPTB()));
        //            break;
        //        case "Close":
        //            Invoke(new Action(() => ClosePTB()));
        //            break;
        //    }

        //    return response;
        //}

        private void mmFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mmFileShowLog_Click(object sender, EventArgs e)
        {
            Process.Start(_logPath);

            string ptbLogPath = Path.Combine(
                _logFolder,
                $"PTBInterface-{DateTime.Now.ToString("yyyyMMdd")}.txt");

            if (File.Exists(ptbLogPath))
            {
                Process.Start(ptbLogPath);
            }
        }

        private void fileBrowser_ValueChanged(object sender, EventArgs e)
        {
            _projectionSettings.imageName = Path.GetFileName(fileBrowser.Value);
            _projectionSettings.Save();
        }

        private void backgroundColorBox_ValueChanged(object sender, EventArgs e)
        {
            _projectionSettings.backgroundColor = new double[3]
            {
                (double) backgroundColorBox.Value.R / 255,
                (double) backgroundColorBox.Value.G / 255,
                (double) backgroundColorBox.Value.B / 255
            };
            _projectionSettings.Save();
        }

        private void distanceNumeric_ValueChanged(object sender, EventArgs e)
        {
            _projectionSettings.distance = distanceNumeric.FloatValue;
            _projectionSettings.Save();
        }

        private void widthNumeric_ValueChanged(object sender, EventArgs e)
        {
            _projectionSettings.width = widthNumeric.FloatValue;
            _projectionSettings.Save();
        }

        private void monitorNumeric_ValueChanged(object sender, EventArgs e)
        {
            _projectionSettings.monitor = monitorNumeric.IntValue;
            _projectionSettings.Save();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenPTB();
        }

        private void OpenPTB()
        {
            //openButton.Visible = false;
            _network.SendMessageToPTB("Open", KLib.FileIO.JSONSerializeToString(_projectionSettings));
        }

        private void ClosePTB()
        {
            //openButton.Visible = true;
            _network.SendMessageToPTB("Close");
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            ClosePTB();
        }
    }
}
