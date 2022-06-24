using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BirthdayTrackerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string databaseName = "BirthdayCelebrants.db";
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string databaseLocation = System.IO.Path.Combine(path, databaseName);
    }
}
