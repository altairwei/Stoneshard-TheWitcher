#nullable enable

namespace TheWitcher;

// <summary>
// Fluent API for stats table manipulation, mirroring the <c>LoadGML.MatchFrom.InsertBelow.Save</c> pattern.
// <para>
// Entry point: <c>TableUtils.StatsTable("gml_GlobalScript_table_weapons")</c>
// </para>
// <example>
// <code>
// TableUtils.StatsTable("gml_GlobalScript_table_weapons")
//     .Append(new Row()
//         .Set("name", "my_weapon")
//         .Set("Tier", "1"))
//     .Save();
// </code>
// </example>
// </summary>
public class TableFluent
{
    private readonly Table _table;

    internal TableFluent(Table table)
    {
        _table = table;
    }

    // <summary>
    // Access the underlying <see cref="Table"/> building block for advanced operations.
    // </summary>
    public Table Open() => _table;

    // <summary>
    // Find the first row containing <paramref name="pattern"/> and return a matched context
    // for chained operations (<c>InsertBelow</c>, <c>InsertAbove</c>, <c>ReplaceBy</c>, <c>Remove</c>).
    // </summary>
    public TableFluentMatched MatchFrom(string pattern)
    {
        (int index, string? row) = _table.FindRow(pattern);
        if (row == null)
        {
            throw new Exception($"Cannot find pattern '{pattern}' in table");
        }
        return new TableFluentMatched(this, _table, index);
    }

    // <summary>
    // Append a new row configured via a callback.
    // </summary>
    public TableFluent Append(Action<TableRow> configure)
    {
        TableRow row = _table.NewRow();
        configure(row);
        _table.AddRow(row);
        return this;
    }

    // <summary>
    // Append a new row configured via column-value tuples.
    // </summary>
    public TableFluent Append(params (string column, string value)[] fields)
    {
        TableRow row = _table.NewRow();
        foreach (var (column, value) in fields)
            row[column] = value;
        _table.AddRow(row);
        return this;
    }

    // <summary>
    // Append a schema-free <see cref="Row"/> (column names resolved automatically from the table header).
    // </summary>
    public TableFluent Append(Row row)
    {
        _table.AddRow(row.ToTableRow(_table));
        return this;
    }

    // <summary>
    // Append a raw string row.
    // </summary>
    public TableFluent Append(string rawRow)
    {
        _table.AddRow(rawRow);
        return this;
    }

    // --- Query ---

    // <summary>
    // Check whether any data row contains <paramref name="pattern"/> as a substring.
    // </summary>
    public bool Exists(string pattern) => _table.Contains(pattern);

    // <summary>
    // Find the first row containing <paramref name="pattern"/> and return a matched context,
    // or <c>null</c> if no match is found. Use this for queries where the row may not exist.
    // </summary>
    public TableFluentMatched? TryMatchFrom(string pattern)
    {
        (int index, string? row) = _table.FindRow(pattern);
        if (row == null) return null;
        return new TableFluentMatched(this, _table, index);
    }

    // <summary>
    // Filter data rows by a predicate. Returns matching <see cref="TableRow"/> instances.
    // </summary>
    public IEnumerable<TableRow> Where(Func<TableRow, bool> predicate)
        => _table.Where(predicate).Select(x => x.row);

    // <summary>
    // Save the table back to the game data.
    // </summary>
    public void Save() => _table.Save();
}

// <summary>
// Represents a matched position in the table fluent chain. Provides operations relative to the match.
// </summary>
public class TableFluentMatched
{
    private readonly TableFluent _parent;
    private readonly Table _table;
    private readonly int _matchIndex;

    internal TableFluentMatched(TableFluent parent, Table table, int matchIndex)
    {
        _parent = parent;
        _table = table;
        _matchIndex = matchIndex;
    }

    // <summary>
    // Clone the matched row, ensuring it carries the table's schema for named-column access.
    // </summary>
    private TableRow CloneMatchedRow()
    {
        // Parse directly from raw data with the table schema to guarantee schema is attached.
        return new TableRow(_table.RawData[_matchIndex], _table.Schema).Clone();
    }

    // --- Query ---

    // <summary>
    // Get the value of a specific column from the matched row.
    // </summary>
    public string Get(string column) => _table.GetRow(_matchIndex)[column];

    // <summary>
    // Get the matched row as a <see cref="TableRow"/>.
    // </summary>
    public TableRow GetRow() => _table.GetRow(_matchIndex);

    // <summary>
    // Get the raw index of the matched row in the table.
    // </summary>
    public int Index => _matchIndex;

    // --- InsertBelow ---

    // <summary>
    // Insert a new row below the matched row, configured via a callback.
    // </summary>
    public TableFluent InsertBelow(Action<TableRow> configure)
    {
        TableRow row = _table.NewRow();
        configure(row);
        _table.InsertAt(_matchIndex + 1, row);
        return _parent;
    }

    // <summary>
    // Insert a new row below the matched row, configured via column-value tuples.
    // </summary>
    public TableFluent InsertBelow(params (string column, string value)[] fields)
    {
        TableRow row = _table.NewRow();
        foreach (var (column, value) in fields)
            row[column] = value;
        _table.InsertAt(_matchIndex + 1, row);
        return _parent;
    }

    // <summary>
    // Insert a schema-free <see cref="Row"/> below the matched row.
    // </summary>
    public TableFluent InsertBelow(Row row)
    {
        _table.InsertAt(_matchIndex + 1, row.ToTableRow(_table));
        return _parent;
    }

    // <summary>
    // Insert a raw string row below the matched row.
    // </summary>
    public TableFluent InsertBelow(string rawRow)
    {
        _table.InsertAt(_matchIndex + 1, rawRow);
        return _parent;
    }

    // --- InsertAbove ---

    // <summary>
    // Insert a new row above the matched row, configured via a callback.
    // </summary>
    public TableFluent InsertAbove(Action<TableRow> configure)
    {
        TableRow row = _table.NewRow();
        configure(row);
        _table.InsertAt(_matchIndex, row);
        return _parent;
    }

    // <summary>
    // Insert a new row above the matched row, configured via column-value tuples.
    // </summary>
    public TableFluent InsertAbove(params (string column, string value)[] fields)
    {
        TableRow row = _table.NewRow();
        foreach (var (column, value) in fields)
            row[column] = value;
        _table.InsertAt(_matchIndex, row);
        return _parent;
    }

    // <summary>
    // Insert a schema-free <see cref="Row"/> above the matched row.
    // </summary>
    public TableFluent InsertAbove(Row row)
    {
        _table.InsertAt(_matchIndex, row.ToTableRow(_table));
        return _parent;
    }

    // <summary>
    // Insert a raw string row above the matched row.
    // </summary>
    public TableFluent InsertAbove(string rawRow)
    {
        _table.InsertAt(_matchIndex, rawRow);
        return _parent;
    }

    // --- ReplaceBy ---

    // <summary>
    // Replace the matched row with a new row configured via a callback.
    // The callback receives the existing row for modification.
    // </summary>
    public TableFluent ReplaceBy(Action<TableRow> configure)
    {
        TableRow row = _table.GetRow(_matchIndex);
        configure(row);
        _table.ReplaceAt(_matchIndex, row);
        return _parent;
    }

    // <summary>
    // Replace the matched row via column-value tuples. Existing values are preserved for columns not specified.
    // </summary>
    public TableFluent ReplaceBy(params (string column, string value)[] fields)
    {
        TableRow row = _table.GetRow(_matchIndex);
        foreach (var (column, value) in fields)
            row[column] = value;
        _table.ReplaceAt(_matchIndex, row);
        return _parent;
    }

    // <summary>
    // Replace the matched row with a schema-free <see cref="Row"/>.
    // </summary>
    public TableFluent ReplaceBy(Row row)
    {
        _table.ReplaceAt(_matchIndex, row.ToTableRow(_table));
        return _parent;
    }

    // <summary>
    // Replace the matched row with a raw string.
    // </summary>
    public TableFluent ReplaceBy(string rawRow)
    {
        _table.ReplaceAt(_matchIndex, rawRow);
        return _parent;
    }

    // --- CloneBelow ---

    // <summary>
    // Clone the matched row, modify via callback, and insert the clone below.
    // The callback receives a copy of the matched row with all its existing values.
    // </summary>
    public TableFluent CloneBelow(Action<TableRow> configure)
    {
        TableRow clone = CloneMatchedRow();
        configure(clone);
        _table.InsertAt(_matchIndex + 1, clone);
        return _parent;
    }

    // <summary>
    // Clone the matched row, override specified columns via tuples, and insert below.
    // </summary>
    public TableFluent CloneBelow(params (string column, string value)[] fields)
    {
        TableRow clone = CloneMatchedRow();
        foreach (var (column, value) in fields)
            clone[column] = value;
        _table.InsertAt(_matchIndex + 1, clone);
        return _parent;
    }

    // <summary>
    // Clone the matched row, override columns from a schema-free <see cref="Row"/>, and insert below.
    // </summary>
    public TableFluent CloneBelow(Row row)
    {
        TableRow clone = CloneMatchedRow();
        foreach (var (column, value) in row.Fields)
            clone[column] = value;
        _table.InsertAt(_matchIndex + 1, clone);
        return _parent;
    }

    // --- CloneAbove ---

    // <summary>
    // Clone the matched row, modify via callback, and insert the clone above.
    // </summary>
    public TableFluent CloneAbove(Action<TableRow> configure)
    {
        TableRow clone = CloneMatchedRow();
        configure(clone);
        _table.InsertAt(_matchIndex, clone);
        return _parent;
    }

    // <summary>
    // Clone the matched row, override specified columns via tuples, and insert above.
    // </summary>
    public TableFluent CloneAbove(params (string column, string value)[] fields)
    {
        TableRow clone = CloneMatchedRow();
        foreach (var (column, value) in fields)
            clone[column] = value;
        _table.InsertAt(_matchIndex, clone);
        return _parent;
    }

    // <summary>
    // Clone the matched row, override columns from a schema-free <see cref="Row"/>, and insert above.
    // </summary>
    public TableFluent CloneAbove(Row row)
    {
        TableRow clone = CloneMatchedRow();
        foreach (var (column, value) in row.Fields)
            clone[column] = value;
        _table.InsertAt(_matchIndex, clone);
        return _parent;
    }

    // --- Remove ---

    // <summary>
    // Remove the matched row.
    // </summary>
    public TableFluent Remove()
    {
        _table.RemoveAt(_matchIndex);
        return _parent;
    }
}
