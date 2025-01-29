using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Mod18
{
    public class GetVideoInfoCommand : ICommand
    {
        private readonly string _videoUrl;

        public GetVideoInfoCommand(string videoUrl)
        {
            _videoUrl = videoUrl;
        }

        public async Task ExecuteAsync()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "yt-dlp.exe",
                    Arguments = $"--dump-json --skip-download {_videoUrl}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var jsonOutput = await process.StandardOutput.ReadToEndAsync();
            process.WaitForExit();

            var json = JObject.Parse(jsonOutput);

            Console.WriteLine($"Название: {json["title"]}");
            Console.WriteLine($"Описание: {json["description"]}");
            Console.WriteLine($"Длительность: {json["duration_string"]}");
            Console.WriteLine($"Автор: {json["uploader"]}");
        }
    }
}
