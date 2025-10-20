using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Datenbankverbindung
{
    public class DbService
    {
        private KurslisteDB _kurslisteDB = new KurslisteDB();

        public Table GetAllRooms()
        {
            return _kurslisteDB.GetAllRooms();
        }
        
        public Table GetCoursesByRoom(string name)
        {
            return _kurslisteDB.GetCoursesByRoom(name);
        }

        public void AddNewRoom(Row room)
        {
            _kurslisteDB.AddNewRoom(room);
        }

        public void AddNewCourse(Row course)
        { 
            _kurslisteDB.AddNewCourse(course);
        }   

        public void DeleteRoomByPKey(string name) 
        { 
            _kurslisteDB.DeleteRoomByPKeyAndItsCourses(name);
        }

        public void DeleteCourseByPKey(string id)
        {
            _kurslisteDB.DeleteCourseByPKey(id);
        }
    }
}
