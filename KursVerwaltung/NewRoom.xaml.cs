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
    public partial class NewRoom : Window
    {
        public string NameInput => NameTextBox.Text;
        public string SizeInput => SizeTextBox.Text;
        public string MaxCapacityInput => MaxCapacityTextBox.Text;

        private DbService service = new DbService();

        public NewRoom()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameInput))
            {
                MessageBox.Show("Please enter a room name.");
                return;
            }
            if (string.IsNullOrWhiteSpace(SizeInput))
            {
                MessageBox.Show("Please enter a room size.");
                return;
            }
            if (string.IsNullOrWhiteSpace(MaxCapacityInput))
            {
                MessageBox.Show("Please enter a room max capacity.");
                return;
            }

            Add_Room();

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public void Add_Room()
        {
            Row room = new Row();
            string errorMessage;

            TextField nameField = new TextField("Name");
            if (nameField.TrySetValue(NameInput, out errorMessage))
                room.AddField(nameField);
            else
                Console.WriteLine($"Error (Name): {errorMessage}");

            NumberField sizeField = new NumberField("Size");
            if (sizeField.TrySetValue(SizeInput, out errorMessage))
                room.AddField(sizeField);
            else
                Console.WriteLine($"Error (Size): {errorMessage}");

            NumberField capacityField = new NumberField("Capacity");
            if (capacityField.TrySetValue(MaxCapacityInput, out errorMessage))
                room.AddField(capacityField);
            else
                Console.WriteLine($"Error (Capacity): {errorMessage}");

            service.AddNewRoom(room);
        }

    }
}
