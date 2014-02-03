using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FitnesseUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings(args);

            if (settings.Load())
            {
                if (settings.Clear)
                {
                    Clear(settings.LocalDirectory);
                }

                if (settings.Get)
                {
                    Get(settings);
                }
            }
            else
            {
                Console.WriteLine(settings.ValidationError);
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void Get(Settings settings)
        {
            Get(settings, settings.RemoteDirectory);
        }

        private static readonly string[] ignoreExtensions = new string[] { ".zip",  };

        private static List<string> fileExtensions = new List<string>();

        static void Get(Settings settings, string remoteDirectoryPath)
        {
            foreach (string remoteFilePath in Directory.GetFiles(remoteDirectoryPath))
            {
                if (!ignoreExtensions.Contains(Path.GetExtension(remoteFilePath)))
                {
                    string localFilePath = Path.Combine(settings.LocalDirectory,remoteFilePath.Substring(remoteDirectoryPath.Length + 1));

                    if (!fileExtensions.Contains(Path.GetExtension(remoteFilePath)))
                    {
                        fileExtensions.Add(Path.GetExtension(remoteFilePath));
                    }

                    //File.Copy(remoteFilePath, localFilePath);
                }
            }

            foreach (string subDirectoryPath in Directory.GetDirectories(remoteDirectoryPath))
            {
                Get(settings, subDirectoryPath);
            }

        }

        private static void Clear(string directoryPath)
        {
            foreach (string subDirectoryPath in Directory.GetDirectories(directoryPath))
            {
                Clear(subDirectoryPath);
                Directory.Delete(subDirectoryPath);
            }

            foreach (string filePath in Directory.GetFiles(directoryPath))
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }
        }
    }
}
