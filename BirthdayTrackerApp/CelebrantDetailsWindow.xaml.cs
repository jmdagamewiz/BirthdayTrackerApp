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
    /// Interaction logic for CelebrantDetailsWindow.xaml
    /// </summary>
    public partial class CelebrantDetailsWindow : Window
    {
        Celebrant celebrant;
        public CelebrantDetailsWindow(Celebrant celebrant)
        {
            InitializeComponent();
            this.celebrant = celebrant;

            FirstNameTextBox.Text = celebrant.FirstName;
            LastNameTextBox.Text = celebrant.LastName;
            PhoneNumberTextBox.Text = celebrant.PhoneNumber;
            EmailTextBox.Text = celebrant.Email;

            DateTime birthDate = new DateTime(celebrant.BirthYear, celebrant.BirthMonth, celebrant.BirthDay);
            BirthdayDatePicker.SelectedDate = birthDate;

            BirthdayDatePicker.DisplayDateEnd = DateTime.Today;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = (DateTime)BirthdayDatePicker.SelectedDate;

            celebrant.FirstName = FirstNameTextBox.Text;
            celebrant.LastName = LastNameTextBox.Text;
            celebrant.PhoneNumber = PhoneNumberTextBox.Text;
            celebrant.Email = EmailTextBox.Text;
            celebrant.BirthDay = selectedDate.Day;
            celebrant.BirthMonth = selectedDate.Month;
            celebrant.BirthYear = selectedDate.Year;

            if (IsValid(celebrant))
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.databaseLocation))
                {
                    connection.CreateTable<Celebrant>();
                    connection.Update(celebrant);
                };

                this.Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databaseLocation))
            {
                connection.CreateTable<Celebrant>();
                connection.Delete(celebrant);
            };

            this.Close();
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
