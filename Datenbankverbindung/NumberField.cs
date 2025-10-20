using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datenbankverbindung
{
    public class NumberField : ITableField
    {
        public string Name { get; }
        public object Value { get; private set; }
        public string ValueAsString => Value.ToString();

        public NumberField(string name)
        {
            Name = name;
        }

        public NumberField(string name, double value)
        {
            Name = name; 
            Value = value;
        }

        public bool TrySetValue(string input, out string error)
        {
            if (double.TryParse(input, out double number))
            {
                Value = number;
                error = null;
                return true;
            }
            else
            {
                error = "Input is not a valid number";
                return false;
            }
        }
    }
}
