using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BirthdayTrackerApp.Classes;
using SQLite;

namespace BirthdayTrackerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateBirthdaysToday();
        }

        private void AddCelebrantButton_Click(object sender, RoutedEventArgs e)
        {
            AddCelebrantWindow addCelebrantWindow = new AddCelebrantWindow();
            addCelebrantWindow.ShowDialog();

            UpdateBirthdaysToday();
        }

        private void ViewAllCelebrantsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllCelebrantsWindow viewAllCelebrantsWindow = new ViewAllCelebrantsWindow();
            viewAllCelebrantsWindow.ShowDialog();

            UpdateBirthdaysToday();
        }

        private void UpdateBirthdaysToday()
        {
            // figure out what day (month and day) it is today) and determine which celebrant will be having theirs today

            DateTime now = DateTime.Now;

            List<Celebrant> celebrants;
            List<Celebrant> filteredCelebrants;

            using (SQLiteConnection connection = new SQLiteConnection(App.databaseLocation))
            {
                connection.CreateTable<Celebrant>();
                celebrants = connection.Table<Celebrant>().ToList();

                filteredCelebrants = (from c in celebrants
                                     where (now <= new DateTime(now.Year, c.BirthMonth, c.BirthDay) && new DateTime(now.Year, c.BirthMonth, c.BirthDay) <= now.AddDays(31))                                 
                                     orderby c.BirthMonth, c.BirthDay
                                     select c).ToList();

                BirthdaysTodayListView.ItemsSource = filteredCelebrants;
            };
        }

        private void BirthdaysTodayListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Celebrant celebrant = (Celebrant)BirthdaysTodayListView.SelectedItem;

            if (celebrant != null)
            {
                GreetWindow greetWindow = new GreetWindow(celebrant);
                greetWindow.ShowDialog();
            }

            UpdateBirthdaysToday();
        }
    }
}
