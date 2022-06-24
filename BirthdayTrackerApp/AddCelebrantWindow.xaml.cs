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
using BirthdayTrackerApp.Classes;
using SQLite;

namespace BirthdayTrackerApp
{
    /// <summary>
    /// Interaction logic for AddCelebrantWindow.xaml
    /// </summary>
    public partial class AddCelebrantWindow : Window
    {
        public AddCelebrantWindow()
        {
            InitializeComponent();
            BirthdayDatePicker.DisplayDateEnd = DateTime.Today;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = (DateTime)BirthdayDatePicker.SelectedDate;

            Celebrant celebrant = new Celebrant()
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                Email = EmailTextBox.Text,
                BirthDay = selectedDate.Day,
                BirthMonth = selectedDate.Month, 
                BirthYear = selectedDate.Year
            };

            if (IsValid(celebrant))
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.databaseLocation))
                {
                    connection.CreateTable<Celebrant>();
                    connection.Insert(celebrant);
                };

                this.Close();
            }
        }

        private bool IsValid(Celebrant celebrant)
        {
            if (celebrant.FirstName == "")
                return false;
            if (celebrant.LastName == "")
                return false;
            if (celebrant.PhoneNumber == "")
                return false;
            if (celebrant.Email == "")
                return false;
            if (celebrant.BirthDay == null)
                return false;
            if (celebrant.BirthMonth == null)
                return false;
            if (celebrant.BirthYear == null)
                return false;
            return true;
        }
    }
}
