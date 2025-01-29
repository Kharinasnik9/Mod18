using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Mod18

{
    class Program
    {
        static async Task Main(string[] args)
        {
            var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Video");

            Console.WriteLine("Доступные команды:");
            Console.WriteLine("info [url] - получить информацию о видео");
            Console.WriteLine("download [url] - скачать видео");

            while (true)
            {
                Console.Write("\nВведите команду: ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                var parts = input.Split(' ');
                var commandName = parts[0].ToLower();
                var videoUrl = parts.Length > 1 ? parts[1] : null;

                try
                {
                    ICommand command = commandName switch
                    {
                        "info" when !string.IsNullOrEmpty(videoUrl) =>
                            new GetVideoInfoCommand(videoUrl),

                        "download" when !string.IsNullOrEmpty(videoUrl) =>
                            new DownloadVideoCommand(videoUrl, outputDirectory),

                        _ => null
                    };

                    if (command == null)
                    {
                        Console.WriteLine("Некорректная команда!");
                        continue;
                    }

                    await command.ExecuteAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}
