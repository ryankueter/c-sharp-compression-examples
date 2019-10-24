/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
using System;
using System.IO;
using System.IO.Compression;

namespace IO_Compression
{
    class Program
    {
        // AddSystem.IO.Compression
        // System.IO.Compression.FileSystem
        static void Main(string[] args)
        {
            //CreateTestZip();
            SimpleTest();
            Console.ReadKey();
        }

        public static void CreateTestZip()
        {
            string storage = @"C:\Users\user\Desktop\folder";
            string temp = Path.Combine(storage, "Temp");
            if (Directory.Exists(temp) == false)
            {
                Directory.CreateDirectory(temp);
            }

            string dir = @"C:\Users\user\Desktop\folder\Windows";

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(dir, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(dir, temp));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(dir, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(dir, temp), true);

            string zipPath = @"C:\Users\user\Desktop\folder\result.zip";
            ZipFile.CreateFromDirectory(temp, zipPath);

            //string extractPath = @"c:\example\extract";
            //ZipFile.ExtractToDirectory(zipPath, extractPath);
        }

        public static void SimpleTest()
        {
            // using System.IO.Compression;
            using (FileStream writeFile = new FileStream("CompText.zip", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (GZipStream writeFileZip = new GZipStream(writeFile, CompressionMode.Compress))
                {
                    using (StreamWriter writeFileText = new StreamWriter(writeFileZip))
                    {
                        writeFileText.Write("Hello world");
                    }
                }
            }

            using (FileStream readFile = new FileStream("CompText.zip", FileMode.Open, FileAccess.Read))
            {
                using (GZipStream readFileZip = new GZipStream(readFile, CompressionMode.Decompress))
                {
                    using (StreamReader readFileText = new StreamReader(readFileZip))
                    {
                        string message = readFileText.ReadToEnd();
                        Console.WriteLine("Read text: {0}", message);
                    }
                }
            }
        }

        static void TestCompress()
        {
            // using System.IO.Compression
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ZipTest");
            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            foreach (FileInfo fileToCompress in directorySelected.GetFiles())
            {
                Compress(fileToCompress);
            }
        }

        static void TestDecompress()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ZipTest");
            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
            {
                Decompress(fileToDecompress);
            }
        }

        static void Compress(FileInfo fileToCompress)
        {
            using (System.IO.FileStream originalFileStream = fileToCompress.OpenRead())
            {
                if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                {
                    using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressionStream);
                            //Console.WriteLine("Compressed {0} from {1} to {2} bytes.", fileToCompress.Name, fileToCompress.Length.ToString(), compressedFileStream.Length.ToString())
                        }
                    }
                }
            }
        }

        static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                dynamic newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        //Console.WriteLine("Decompressed: {0}", fileToDecompress.Name)
                    }
                }
            }
        }
    }
}
