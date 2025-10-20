using Datenbankverbindung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursVerwaltung
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string Booked { get; set; }
    }
}
