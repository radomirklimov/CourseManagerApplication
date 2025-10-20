using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datenbankverbindung
{
    public class Row
    {
        public List<ITableField> Fields { get; } = new();

        public void AddField(ITableField field) => Fields.Add(field);

        public override string ToString()
        {
            return string.Join(" | ", Fields.Select(f => f.ValueAsString));
        }
    }

}
