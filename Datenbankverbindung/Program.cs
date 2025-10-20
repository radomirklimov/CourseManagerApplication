
using System.Runtime.CompilerServices;

namespace Datenbankverbindung
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DbService db = new DbService();

            Console.WriteLine(db.GetAllRooms());
            Console.WriteLine();
            Console.WriteLine(db.GetCoursesByRoom("R103"));
        }
    }
}
