using Datenbankverbindung;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace KursVerwaltung
{
    public partial class CourseListWindow : Window
    {
        private readonly DbService _service = new DbService();
        public ObservableCollection<CourseViewModel> Courses { get; set; }
        private string _roomRnr;

        public CourseListWindow(string roomRnr)
        {
            InitializeComponent();
            _roomRnr = roomRnr;
            LoadCourses(roomRnr);
        }

        private void LoadCourses(string roomRnr)
        {
            Courses = new ObservableCollection<CourseViewModel>();
            var table = _service.GetCoursesByRoom(roomRnr);

            foreach (var row in table.Rows)
            {
                Courses.Add(new CourseViewModel
                {
                    Name = row.Fields[1].Value.ToString(),
                    Duration = row.Fields[2].Value.ToString(),
                    Price = row.Fields[3].Value.ToString(),
                    Booked = row.Fields[4].Value.ToString() == "Y" ? "Yes" : "No",
                    Id = row.Fields[0].Value.ToString() 
                });
            }

            CoursesDataGrid.ItemsSource = Courses;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CourseViewModel course)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete course '{course.Name}'?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _service.DeleteCourseByPKey(course.Id); 
                    Courses.Remove(course);
                }
            }
        }
    }
}
