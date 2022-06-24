using BirthdayTrackerApp.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for GreetWindow.xaml
    /// </summary>
    public partial class GreetWindow : Window
    {
        Celebrant celebrant;
        public GreetWindow(Celebrant celebrant)
        {
            InitializeComponent();
            this.celebrant = celebrant;

            Title = $"Greet {celebrant.FirstName} on their birthday.";

            if (DateTime.Now.Day == celebrant.BirthDay && DateTime.Now.Month == celebrant.BirthMonth)
            {
                GreetNameLabel.Content = $"It's {celebrant.FirstName}'s birthday today!";
            }
            else
            {
                DateTime birthday = new DateTime(DateTime.Now.Year, celebrant.BirthMonth, celebrant.BirthDay);
                GreetNameLabel.Content = $"It's {celebrant.FirstName}'s birthday this {birthday.ToString("MMMM dd", DateTimeFormatInfo.InvariantInfo)}!";
            }

            PhoneNumberTextBox.Text = celebrant.PhoneNumber;
            EmailTextBox.Text = celebrant.Email;
        }

        private void GreetButton_Click(object sender, RoutedEventArgs e)
        {
            string subject = $"Happy Birthday {celebrant.FirstName}!!";
            string body = "Another year, another challenge my friend. But, you need to remember " +
                "to keep smiling and have a positive outlook on life, and everything is going to be fine. Happy birthday!";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo($"mailto:{celebrant.Email}?subject={subject}&body={body}") { UseShellExecute = true });
        }
    }
}
