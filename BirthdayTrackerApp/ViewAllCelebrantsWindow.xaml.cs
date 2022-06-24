using BirthdayTrackerApp.Classes;
using SQLite;
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
using System.Windows.Shapes;

namespace BirthdayTrackerApp
{
    /// <summary>
    /// Interaction logic for ViewAllCelebrantsWindow.xaml
    /// </summary>
    public partial class ViewAllCelebrantsWindow : Window
    {
        public ViewAllCelebrantsWindow()
        {
            InitializeComponent();

            Binding listItemBinding = new Binding("Content");

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            List<Celebrant> celebrants;
            using (SQLiteConnection connection = new SQLiteConnection(App.databaseLocation))
            {
                connection.CreateTable<Celebrant>();
                celebrants = connection.Table<Celebrant>().ToList();
            }

            List<Celebrant> sortedCelebrants = (from c in celebrants
                                                orderby c.FirstName
                                                select c).ToList();

            CelebrantsListView.ItemsSource = sortedCelebrants;
        }

        private void CelebrantsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Celebrant selectedCelebrant = CelebrantsListView.SelectedItem as Celebrant;

            if (selectedCelebrant != null)
            {
                CelebrantDetailsWindow celebrantDetailsWindow = new CelebrantDetailsWindow(selectedCelebrant);
                celebrantDetailsWindow.ShowDialog();
            }

            ReadDatabase();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
