using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mod18
{
    public class DownloadVideoCommand : ICommand
    {
        private readonly string _videoUrl;
        private readonly string _outputDirectory;

        public DownloadVideoCommand(string videoUrl, string outputDirectory)
        {
            _videoUrl = videoUrl;
            _outputDirectory = outputDirectory;
        }

        public async Task ExecuteAsync()
        {
            Directory.CreateDirectory(_outputDirectory);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "yt-dlp.exe",
                    Arguments = $"-o \"{_outputDirectory}/%(title)s.%(ext)s\" --merge-output-format mp4 {_videoUrl}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            await process.WaitForExitAsync();

            Console.WriteLine("Видео успешно скачано!");
        }
    }
}
