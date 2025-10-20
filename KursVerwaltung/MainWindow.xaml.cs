using Datenbankverbindung;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace KursVerwaltung
{
    public partial class MainWindow : Window
    {
        private readonly DbService _service = new DbService();
        public ObservableCollection<RoomViewModel> Rooms { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadRooms();
            RoomsDataGrid.SelectionChanged += RoomsDataGrid_SelectionChanged;
        }

        private void LoadRooms()
        {
            Rooms = new ObservableCollection<RoomViewModel>();
            var table = _service.GetAllRooms();

            foreach (var row in table.Rows)
            {
                Rooms.Add(new RoomViewModel
                {
                    Rnr = row.Fields[0].Value.ToString(),
                    Size = row.Fields[1].Value.ToString(),
                    Capacity = row.Fields[2].Value.ToString()
                });
            }

            RoomsDataGrid.ItemsSource = Rooms;
        }

        private void RefreshRooms()
        {
            Rooms.Clear();
            var table = _service.GetAllRooms();

            foreach (var row in table.Rows)
            {
                Rooms.Add(new RoomViewModel
                {
                    Rnr = row.Fields[0].Value.ToString(),
                    Size = row.Fields[1].Value.ToString(),
                    Capacity = row.Fields[2].Value.ToString()
                });
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is RoomViewModel room)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete room {room.Rnr}?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _service.DeleteRoomByPKey(room.Rnr);
                    Rooms.Remove(room);
                }
            }
        }

        private void NewCourse_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new NewCourse { Owner = this };
            bool? result = addWindow.ShowDialog();

            if (result == true)
            {
                MessageBox.Show(
                    $"Course Added:\n\n" +
                    $"Title: {addWindow.TitleInput}\n" +
                    $"Duration: {addWindow.DurationInput}\n" +
                    $"Price: {addWindow.PriceInput}\n" +
                    $"Available: {(addWindow.IsAvailableInput ? "Yes" : "No")}\n" +
                    $"Room: {addWindow.RoomInput}",
                    "Course Added",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void NewRoom_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new NewRoom { Owner = this };
            bool? result = addWindow.ShowDialog();

            if (result == true)
            {
                MessageBox.Show(
                    $"Room Added:\n\n" +
                    $"Name: {addWindow.NameInput}\n" +
                    $"Size: {addWindow.SizeInput}\n" +
                    $"Max Capacity: {addWindow.MaxCapacityInput}",
                    "Room Added",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

               
                RefreshRooms();
            }
        }

        private void RoomsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem is RoomViewModel room)
            {
                var courseWindow = new CourseListWindow(room.Rnr);
                courseWindow.Show(); 
            }

            RoomsDataGrid.SelectedItem = null; 
        }
    }
}
