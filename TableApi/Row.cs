
namespace TheWitcher;

// <summary>
// Lightweight schema-free row builder for the stats table fluent API.
// Stores column-name → value pairs; the actual schema resolution happens
// when the row is passed to <c>InsertBelow</c>, <c>InsertAbove</c>, <c>Append</c>, etc.
// <example>
// <code>
// TableUtils.StatsTable("gml_GlobalScript_table_weapons")
//     .Append(new Row()
//         .Set("name", "my_weapon")
//         .Set("Tier", "1")
//         .Set("Slot", "sword"))
//     .Save();
// </code>
// </example>
// </summary>
public class Row
{
    internal readonly Dictionary<string, string> Fields = new();

    // <summary>
    // Set a column value. Returns this row for chaining.
    // </summary>
    public Row Set(string column, string value)
    {
        Fields[column] = value;
        return this;
    }

    // <summary>
    // Convert this schema-free row into a <see cref="TableRow"/> using the given table's schema.
    // </summary>
    internal TableRow ToTableRow(Table table)
    {
        TableRow row = table.NewRow();
        foreach (var (column, value) in Fields)
            row[column] = value;
        return row;
    }
}
