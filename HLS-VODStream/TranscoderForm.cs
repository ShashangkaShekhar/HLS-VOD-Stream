using FFMpegSharp.Utilities;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLS_VODStream
{
    public partial class TranscoderForm : Form
    {
        public TranscoderForm()
        {
            InitializeComponent();
        }

        private void brnCancel_Click(object sender, EventArgs e)
        {
            bool isProcessing = IsProcessRunning("ffmpeg");
            if (isProcessing)
            {
                KillProcess("ffmpeg");
            }

            this.Close();
        }

        private void btnTranscode_Click(object sender, EventArgs e)
        {
            try
            {
                bool isProcessing = IsProcessRunning("ffmpeg");
                if (!isProcessing)
                {
                    if (txtFile.Text != string.Empty)
                    {
                        string _rootPath = Environment.CurrentDirectory;
                        string ffmpegOutput = ConfigurationManager.AppSettings["ffmpegOutput"];
                        this.Cursor = Cursors.WaitCursor;
                        this.Text = "Transcoding...";
                        btnTranscode.Text = "Transcoding..";

                        string inputFile = txtFile.Text.ToString();
                        string fileOutput = ffmpegOutput + toUnderscore(Path.GetFileNameWithoutExtension(inputFile)) + "\\";
                        if (!Directory.Exists(fileOutput))
                        {
                            SetFolderPermission(fileOutput);
                            DirectoryInfo di = Directory.CreateDirectory(fileOutput);
                        }

                        //Create index as master playlist
                        string path = fileOutput + "index.m3u8";
                        File.Create(path).Dispose();
                        string[] line ={
                            "#EXTM3U",
                            "#EXT-X-VERSION:3",
                            "#EXT-X-STREAM-INF:BANDWIDTH=10000,RESOLUTION=426x240",
                            "240p.m3u8",
                            "#EXT-X-STREAM-INF:BANDWIDTH=420000,RESOLUTION=640x360",
                            "360p.m3u8",
                            "#EXT-X-STREAM-INF:BANDWIDTH=680000,RESOLUTION=842x480",
                            "480p.m3u8",
                            "#EXT-X-STREAM-INF:BANDWIDTH=1256000,RESOLUTION=1280x720",
                            "720p.m3u8",
                            "#EXT-X-STREAM-INF:BANDWIDTH=2000000,RESOLUTION=1920x1080",
                            "1080p.m3u8"
                        };
                        File.WriteAllLines(path, line);

                        //Command
                        string conversionArgs = string.Format("-hide_banner -y" +
                                                                " -i \"{0}\" -vf scale=w=426:h=240:force_original_aspect_ratio=decrease -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 4 -hls_playlist_type vod -b:v 240k -maxrate 240k -bufsize 480k -b:a 64k -hls_segment_filename " + fileOutput + "240p_%d.ts " + fileOutput + "240p.m3u8" +
                                                                " -i \"{0}\" -vf scale=w=640:h=360:force_original_aspect_ratio=decrease -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 4 -hls_playlist_type vod -b:v 800k -maxrate 856k -bufsize 1200k -b:a 96k -hls_segment_filename " + fileOutput + "360p_%d.ts " + fileOutput + "360p.m3u8" +
                                                                " -i \"{0}\" -vf scale=w=842:h=480:force_original_aspect_ratio=decrease -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 4 -hls_playlist_type vod -b:v 1400k -maxrate 1498k -bufsize 2100k -b:a 128k -hls_segment_filename " + fileOutput + "480p_%d.ts " + fileOutput + "480p.m3u8" +
                                                                " -i \"{0}\" -vf scale=w=1280:h=720:force_original_aspect_ratio=decrease -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 4 -hls_playlist_type vod -b:v 2800k -maxrate 2996k -bufsize 4200k -b:a 128k -hls_segment_filename " + fileOutput + "720p_%d.ts " + fileOutput + "720p.m3u8" +
                                                                " -i \"{0}\" -vf scale=w=1920:h=1080:force_original_aspect_ratio=decrease -c:a aac -ar 48000 -c:v h264 -profile:v main -crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -hls_time 4 -hls_playlist_type vod -b:v 5000k -maxrate 5350k -bufsize 7500k -b:a 192k -hls_segment_filename " + fileOutput + "1080p_%d.ts " + fileOutput + "1080p.m3u8", inputFile, fileOutput);

                        //Process
                        FFMpeg encoder = new FFMpeg();
                        encoder.OnProgress += encoder_OnProgress;
                        Task.Run(() => encoder.ToTS(inputFile, conversionArgs));
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void btnPickFile_Click(object sender, EventArgs e)
        {
            try
            {
                txtFile.Text = string.Empty;
                OpenFileDialog ofdFile = new OpenFileDialog();
                ofdFile.Multiselect = false;
                ofdFile.Filter = "Video files (*.mp4)|*.mp4";
                ofdFile.Title = "Select File.";
                if (ofdFile.ShowDialog() == DialogResult.OK)
                {
                    var file = ofdFile.FileNames;
                    txtFile.Text = file[0].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        void SetFolderPermission(string folderPath)
        {
            try
            {
                var directoryInfo = new DirectoryInfo(folderPath);
                var directorySecurity = directoryInfo.GetAccessControl();
                var currentUserIdentity = WindowsIdentity.GetCurrent();
                var fileSystemRule = new FileSystemAccessRule(currentUserIdentity.Name, FileSystemRights.Read, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);
                directorySecurity.AddAccessRule(fileSystemRule);
                directoryInfo.SetAccessControl(directorySecurity);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        void encoder_OnProgress(int percentage)
        {
            try
            {
                //Update UI
                Invoke(new Action(() =>
                {
                    progressBar1.Value = percentage;
                    this.Text = "Transcoding..." + percentage + "%";
                }));

                if (percentage == 100)
                {
                    Invoke(new Action(() =>
                    {
                        this.btnTranscode.Text = "Transcode";
                        this.Cursor = Cursors.Default;
                    }));
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        string toUnderscore(string stringReplace)
        {
            string _stringReplace = stringReplace.Replace(" ", "_");
            return _stringReplace;
        }

        bool IsProcessRunning(string sProcessName)
        {
            bool isProcessing = false;
            try
            {
                Process[] proc = Process.GetProcessesByName(sProcessName);
                if (proc.Length > 0)
                    isProcessing = true;
                else
                    isProcessing = false;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return isProcessing;
        }

        bool KillProcess(string sProcessName)
        {
            bool isProcessing = false;
            try
            {
                Process[] proc = Process.GetProcessesByName(sProcessName);
                if (proc.Length > 0)
                {
                    foreach (var process in proc)
                    {
                        process.Kill();
                    }
                    isProcessing = true;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return isProcessing;
        }
    }
}
