using FFMpegSharp.Helpers;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FFMpegSharp.Utilities
{
    public delegate void ConversionHandler(int percentage);

    public class FFMpeg
    {
        private string _ffmpegPath;
        TimeSpan? _totalVideoTime = null;
        FFProbe _probe;
        Process _process;
        public bool IsConverting { get; private set; }

        private bool _RunProcess(string args)
        {
            bool SuccessState = true;
            _process = new Process();
            _process.StartInfo.FileName = _ffmpegPath;
            _process.StartInfo.Arguments = args;
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.RedirectStandardInput = true;
            try
            {
                _process.Start();
                _process.ErrorDataReceived += OutputData;
                _process.BeginErrorReadLine();
            }
            catch (Exception)
            {
                SuccessState = false;
            }
            finally
            {
                _process.WaitForExit();
                _process.Close();
                IsConverting = false;
            }
            return SuccessState;
        }

        private void OutputData(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (IsConverting && e.Data != null && OnProgress != null)
                {
                    Regex r = new Regex(@"\w\w:\w\w:\w\w");
                    Match m = r.Match(e.Data);
                    if (e.Data.Contains("frame"))
                    {
                        if (m.Success)
                        {
                            TimeSpan t = TimeSpan.Parse(m.Value);
                            int percentage = (int)(t.TotalSeconds / _totalVideoTime.Value.TotalSeconds * 100);
                            OnProgress(percentage);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Video Information is not available. Please use FFProbe to intialize your video object.");
            }
        }

        /// <summary>
        /// Passes the conversion percentage when encoding.
        /// </summary>
        public event ConversionHandler OnProgress;

        /// <summary>
        /// Intializes the FFMPEG encoder.
        /// </summary>
        /// <param name="rootPath">Directory root where ffmpeg.exe can be found. If not specified, root will be loaded from config.</param>
        public FFMpeg(string rootPath = null)
        {
            if (rootPath == null)
                rootPath = Environment.CurrentDirectory;

            FFMpegHelper.RootExceptionCheck(rootPath);
            FFProbeHelper.RootExceptionCheck(rootPath);

            _ffmpegPath = rootPath + "\\ffmpeg.exe";
            _probe = new FFProbe(rootPath);
        }

        /// <summary>
        /// Converts a source video to TS format.
        /// </summary>
        /// <param name="source">Source video file.</param>
        /// <returns>Success state.</returns>
        public bool ToTS(VideoInfo source, string conversionArgs)
        {
            _probe.SetVideoInfo(ref source);
            _totalVideoTime = source.Duration;
            IsConverting = true;
            return _RunProcess(conversionArgs);
        }

        /// <summary>
        /// Converts a source video to TS format.
        /// </summary>
        /// <param name="source">Source video file.</param>
        /// <returns>Success state.</returns>
        public bool ToTS(string source, string conversionArgs)
        {
            return ToTS(new VideoInfo(source), conversionArgs);
        }

        /// <summary>
        /// Stops any current job that FFMpeg is running.
        /// </summary>
        public void Stop()
        {
            if (!_process.HasExited)
            {
                _process.StandardInput.Write('q');
            }
        }

        /// <summary>
        /// Kills the FFMpeg process. NOTE: killing the process will most likely end in video corruption.
        /// </summary>
        public void Kill()
        {
            if (!_process.HasExited)
            {
                _process.Kill();
            }
        }
    }
}
