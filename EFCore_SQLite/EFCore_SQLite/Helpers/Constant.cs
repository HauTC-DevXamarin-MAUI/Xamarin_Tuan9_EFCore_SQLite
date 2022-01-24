using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace EFCore_SQLite.Helpers
{
    public static class Constant
    {
        public static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "item.db3");
        public static string DBPathBackup = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "itemBackup.db3");
    }
}
