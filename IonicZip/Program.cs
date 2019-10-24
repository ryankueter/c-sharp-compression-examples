/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
using System;
using Ionic.Zip;

namespace IonicZip
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void Zip()
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(@"C:\Users\user\Desktop\folder\Contacts.txt");
                zip.AddFile(@"C:\Users\user\Desktop\folder\contacts.CSV");
                zip.Save(@"C:\Users\user\Desktop\folder\text.zip");
            }
        }

        static void Unzip()
        {
            using (ZipFile zip = new ZipFile(@"C:\Users\user\Desktop\folder\text.zip"))
            {
                zip.ExtractAll(@"C:\Users\user\Desktop\folder\Test", ExtractExistingFileAction.OverwriteSilently);
            }
        }
    }
}
