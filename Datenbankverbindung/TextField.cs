using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Datenbankverbindung
{
    public class TextField : ITableField
    {
        public string Name {  get; }
        public object Value { get; private set; }
        public string ValueAsString => Value.ToString();

        public TextField(string name)
        {
            Name = name;
        }

        public TextField(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public bool TrySetValue(string input, out string error)
        {
            if (!string.IsNullOrEmpty(input))
            {
                Value = input;
                error = null;
                return true;
            }
            else
            {
                error = "Input cannot be empty.";
                return false; 
            }
        }
    }
}