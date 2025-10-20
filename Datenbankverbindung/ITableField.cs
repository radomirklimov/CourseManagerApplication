using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datenbankverbindung
{
    public interface ITableField
    {
        string Name { get; }
        object Value { get; } 
        string ValueAsString { get; } 

        bool TrySetValue(string input, out string error); 
    }
}
