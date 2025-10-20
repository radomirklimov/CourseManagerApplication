using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datenbankverbindung
{
    public class BoolField : ITableField
    {
        public string Name { get; }
        public object Value { get; private set; }
        public string ValueAsString => (bool)Value ? "Yes" : "No";

        public BoolField(string name)
        {
            Name = name;
        }

        public BoolField(string name, bool value)
        {
            Name = name;
            Value = value;
        }

        public bool TrySetValue(string input, out string error)
        {
            if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Value = true;
                error = null;
                return true;
            }
            else if (input.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                Value = false;
                error = null;
                return true;
            }
            else
            {
                error = "Please enter 'Y' or 'N'.";
                return false;
            }
        }
    }
}
