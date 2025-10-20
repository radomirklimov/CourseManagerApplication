using Datenbankverbindung;
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

namespace KursVerwaltung
{
    public partial class NewCourse : Window
    {
        public string TitleInput => TitleTextBox.Text;
        public string DurationInput => DurationTextBox.Text;
        public string PriceInput => PriceTextBox.Text;
        public bool IsAvailableInput => IsAvailableCheckBox.IsChecked == false;
        public string RoomInput => RoomTextBox.Text;

        DbService service = new DbService();  

        public NewCourse()
        {
            InitializeComponent();
            IsAvailableCheckBox.IsChecked = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleInput))
            {
                MessageBox.Show("Please enter a course title.");
                return;
            }
            if (string.IsNullOrWhiteSpace(DurationInput))
            {
                MessageBox.Show("Please enter a course duration.");
                return;
            }
            if (string.IsNullOrWhiteSpace(PriceInput))
            {
                MessageBox.Show("Please enter a course price.");
                return;
            }
            if (string.IsNullOrWhiteSpace(RoomInput))
            {
                MessageBox.Show("Please enter a course room.");
                return;
            }

            Add_Course();

            DialogResult = true; 
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public void Add_Course()
        {
            Row course = new Row();
            string errorMessage;

            TextField roomField = new TextField("Room");
            if (roomField.TrySetValue(RoomInput, out errorMessage))
                course.AddField(roomField);
            else
                Console.WriteLine($"Error (Room): {errorMessage}");

            TextField titleField = new TextField("Name");
            if (titleField.TrySetValue(TitleInput, out errorMessage))
                course.AddField(titleField);
            else
                Console.WriteLine($"Error (Title): {errorMessage}");

            NumberField durationField = new NumberField("Duration");
            if (durationField.TrySetValue(DurationInput, out errorMessage))
                course.AddField(durationField);
            else
                Console.WriteLine($"Error (Duration): {errorMessage}");

            NumberField priceField = new NumberField("Price");
            if (priceField.TrySetValue(PriceInput, out errorMessage))
                course.AddField(priceField);
            else
                Console.WriteLine($"Error (Price): {errorMessage}");

            BoolField bookedField = new BoolField("Booked");
            if (bookedField.TrySetValue(IsAvailableInput ? "N" : "Y", out errorMessage))
                course.AddField(bookedField);
            else
                Console.WriteLine($"Error (Booked): {errorMessage}");

            service.AddNewCourse(course);
        }

    }
}