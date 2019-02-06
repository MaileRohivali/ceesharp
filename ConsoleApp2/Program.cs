using System;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

//The following code is written so that I could understand what I do, why I do and be less time consuming for me.
namespace zip
{
    class Program
    {
        static void Main(string[] args)
        {
            //Here is the necessary information gathered so as Microsoft said it should be done
            string zipPath, extractPath;

            Console.WriteLine("Please enter a zipfile-path:");
            zipPath = (Console.ReadLine());

            Console.WriteLine("Thank you. Where to extract the file?");
            extractPath = (Console.ReadLine());

            ZipFile.ExtractToDirectory(zipPath, extractPath);

            //finding each files compression ratio to get a more detailed picture
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    float compressedRatio = (float)entry.CompressedLength / entry.Length;
                    float reductionPercentage = 100 - (compressedRatio * 100);

                    Console.WriteLine(string.Format("File: Compressed {0:F2}%", reductionPercentage));
                }
            }

            //Oldest file should be found like this
            FileSystemInfo fileInfo = new DirectoryInfo(extractPath).GetFileSystemInfos()
            .OrderBy(fi => fi.CreationTime).First();
            var days = (DateTime.Now.Date - fileInfo.LastWriteTime.Date).TotalDays;

            //To see the value of the oldest file this should be used
            Console.WriteLine($"{fileInfo} is {days} days old");
            Console.ReadLine();
        }
    }
}
