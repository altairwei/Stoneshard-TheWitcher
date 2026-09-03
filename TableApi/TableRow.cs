#nullable enable

namespace TheWitcher;

// <summary>
// Named-column access to a single semicolon-delimited table row.
// Supports both positional (index) and named (column name) access when a <see cref="TableSchema"/> is available.
// </summary>
public class TableRow
{
    private readonly string[] _fields;
    private readonly TableSchema? _schema;

    // <summary>
    // Number of fields in this row.
    // </summary>
    public int FieldCount => _fields.Length;

    // <summary>
    // The schema this row is associated with, if any.
    // </summary>
    public TableSchema? Schema => _schema;

    // <summary>
    // Create an empty row with the specified number of columns, optionally linked to a schema.
    // </summary>
    public TableRow(int columnCount, TableSchema? schema = null)
    {
        _fields = new string[columnCount];
        Array.Fill(_fields, "");
        _schema = schema;
    }

    // <summary>
    // Parse an existing semicolon-delimited row string, optionally linked to a schema.
    // </summary>
    public TableRow(string row, TableSchema? schema = null)
    {
        // Split on ';' and drop the trailing empty element (rows end with ';')
        string[] parts = row.Split(';');
        _fields = parts.Length > 0 && parts[^1] == ""
            ? parts[..^1]
            : parts;
        _schema = schema;
    }

    // <summary>
    // Access a field by positional index.
    // </summary>
    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= _fields.Length)
                throw new IndexOutOfRangeException($"Column index {index} is out of range [0, {_fields.Length}).");
            return _fields[index];
        }
        set
        {
            if (index < 0 || index >= _fields.Length)
                throw new IndexOutOfRangeException($"Column index {index} is out of range [0, {_fields.Length}).");
            _fields[index] = value ?? "";
        }
    }

    // <summary>
    // Access a field by column name. Requires a <see cref="TableSchema"/> to be associated.
    // </summary>
    // <exception cref="InvalidOperationException">Thrown when no schema is associated.</exception>
    // <exception cref="KeyNotFoundException">Thrown when the column name is not found in the schema.</exception>
    public string this[string column]
    {
        get
        {
            if (_schema == null)
                throw new InvalidOperationException("Cannot access column by name without a schema.");
            return _fields[_schema.GetIndex(column)];
        }
        set
        {
            if (_schema == null)
                throw new InvalidOperationException("Cannot access column by name without a schema.");
            _fields[_schema.GetIndex(column)] = value ?? "";
        }
    }

    // <summary>
    // Create a deep copy of this row.
    // </summary>
    public TableRow Clone()
    {
        var clone = new TableRow(_fields.Length, _schema);
        Array.Copy(_fields, clone._fields, _fields.Length);
        return clone;
    }

    // <summary>
    // Fluent setter — set a column value by name and return this row for chaining.
    // </summary>
    public TableRow Set(string column, string value)
    {
        this[column] = value;
        return this;
    }

    // <summary>
    // Fluent setter — set a column value by index and return this row for chaining.
    // </summary>
    public TableRow Set(int index, string value)
    {
        this[index] = value;
        return this;
    }

    // <summary>
    // Build the semicolon-delimited row string (with trailing semicolon).
    // </summary>
    public string Build() => string.Join(";", _fields) + ";";
}
