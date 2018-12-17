using System.Runtime.Remoting.Contexts;
using AutoRepairShop.Tools;

namespace AutoRepairShop.Services
{
    [Synchronization]
    internal static class FileFolderManagementService
    {
        
        public const string DatetimeFormat = "dd-MM-yyyy";
        public static void CreateFolder()
        {
            var folderName = @"D:\test";
            var pathString = System.IO.Path.Combine(folderName,
                $"AgreementsFor_{TimeTool.GetGameTime().ToString(DatetimeFormat)}");        
            System.IO.Directory.CreateDirectory(pathString);
        }
    }
}