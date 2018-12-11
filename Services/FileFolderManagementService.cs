using System;
using AutoRepairShop.Tools;

namespace AutoRepairShop.Services
{
    static class FileFolderManagementService
    {
        public const string DatetimeFormat = "dd-MM-yyyy";
        public static void CreateFolder()
        {
            string folderName = @"D:\test";
            string pathString = System.IO.Path.Combine(folderName,
                $"AgreementsFor_{TimeTool.GetGameTime().ToString(DatetimeFormat)}"); //add a timestamp
           
            System.IO.Directory.CreateDirectory(pathString);

            // Create a file name for the file you want to create. 
            string fileName = System.IO.Path.GetRandomFileName();

            // This example uses a random string for the name, but you also can specify
            // a particular name.
            //string fileName = "MyNewFile.txt";

            // Use Combine again to add the file name to the path.
            pathString = System.IO.Path.Combine(pathString, fileName);

            // Verify the path that you have constructed.
            Console.WriteLine("Path to my file: {0}\n", pathString);

            // Check that the file doesn't already exist. If it doesn't exist, create
            // the file and write integers 0 - 99 to it.
            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.
            if (!System.IO.File.Exists(pathString))
            {

            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
            }
        }
    }
}