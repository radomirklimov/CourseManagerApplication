using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Datenbankverbindung
{
    public class Table
    {
        public string Name { get; set; }
        public List<Row> Rows { get; } = new();

        public Table(string name) => Name = name;

        public void AddRow(Row row) => Rows.Add(row);

        public override string ToString()
        {
            var sb = new StringBuilder($"{Name}:\n");
            foreach (var row in Rows)
                sb.AppendLine(row.ToString());
            return sb.ToString();
        }
    }
}
