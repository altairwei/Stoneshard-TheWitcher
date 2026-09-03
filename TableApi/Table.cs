#nullable enable
using ModShardLauncher;

namespace TheWitcher;

// <summary>
// Rich wrapper around <c>ModLoader.GetTable</c>/<c>ModLoader.SetTable</c> with runtime schema discovery,
// hook detection, and convenience methods for row manipulation.
// <para>
// Implements <see cref="IDisposable"/> — calling <see cref="Dispose"/> auto-saves the table.
// </para>
// </summary>
public class Table : IDisposable
{
    private readonly string _tableName;
    private readonly List<string> _data;
    private bool _saved;

    // <summary>
    // Schema parsed from the first row (if it looks like a header). Null for headerless tables.
    // </summary>
    public TableSchema? Schema { get; }

    // <summary>
    // Auto-discovered hook lines: rows matching <c>"// XXX"</c> or <c>"[ XXX ]"</c> patterns.
    // </summary>
    public IReadOnlyList<string> Hooks { get; }

    // <summary>
    // Total number of raw rows in the table (including header and hooks).
    // </summary>
    public int RowCount => _data.Count;

    // <summary>
    // Number of columns (from schema if available, otherwise from first data row).
    // </summary>
    public int ColumnCount => Schema?.ColumnCount ?? (_data.Count > 0 ? _data[0].Split(';').Length - 1 : 0);

    // <summary>
    // The underlying raw data list. Use for advanced access when building blocks don't suffice.
    // </summary>
    public List<string> RawData => _data;

    private Table(string tableName, List<string> data, TableSchema? schema, IReadOnlyList<string> hooks)
    {
        _tableName = tableName;
        _data = data;
        Schema = schema;
        Hooks = hooks;
    }

    // <summary>
    // Load a table by name from the game data.
    // </summary>
    // <param name="tableName">The GML script name, e.g. <c>"gml_GlobalScript_table_weapons"</c>.</param>
    // <returns>A new <see cref="Table"/> instance.</returns>
    public static Table Load(string tableName)
    {
        List<string> data = Msl.ThrowIfNull(ModLoader.GetTable(tableName));

        // Try to parse schema from the first non-hook row
        TableSchema? schema = null;
        foreach (string row in data)
        {
            if (IsHookLine(row)) continue;
            if (LooksLikeHeader(row))
            {
                schema = TableSchema.FromHeader(row);
            }
            break; // only check the first non-hook row
        }

        // Discover hooks
        List<string> hooks = data
            .Where(row => IsHookLine(row))
            .ToList();

        return new Table(tableName, data, schema, hooks.AsReadOnly());
    }

    // <summary>
    // Get a <see cref="TableRow"/> for the row at the given index.
    // </summary>
    public TableRow GetRow(int index) => new(_data[index], Schema);

    // <summary>
    // Get all data rows as <see cref="TableRow"/> instances (excluding header if schema exists).
    // </summary>
    public IEnumerable<TableRow> GetDataRows()
    {
        int start = Schema != null ? 1 : 0;
        for (int i = start; i < _data.Count; i++)
        {
            if (!IsHookLine(_data[i]))
                yield return new TableRow(_data[i], Schema);
        }
    }

    // <summary>
    // Create a new empty <see cref="TableRow"/> linked to this table's schema.
    // </summary>
    public TableRow NewRow()
    {
        if (Schema != null)
            return Schema.NewRow();
        return new TableRow(ColumnCount);
    }

    // --- Row insertion ---

    // <summary>
    // Append a <see cref="TableRow"/> to the end of the table.
    // </summary>
    public void AddRow(TableRow row) => _data.Add(row.Build());

    // <summary>
    // Append a raw string row to the end of the table.
    // </summary>
    public void AddRow(string row) => _data.Add(row);

    // <summary>
    // Find a row containing <paramref name="hook"/> and insert <paramref name="row"/> after it.
    // </summary>
    // <exception cref="Exception">Thrown when the hook is not found.</exception>
    public void InsertAfterHook(string hook, TableRow row) => InsertAfterHook(hook, row.Build());

    // <summary>
    // Find a row containing <paramref name="hook"/> and insert a raw string row after it.
    // </summary>
    // <exception cref="Exception">Thrown when the hook is not found.</exception>
    public void InsertAfterHook(string hook, string row)
    {
        (int ind, string? found) = FindRow(hook);
        if (found != null)
        {
            _data.Insert(ind + 1, row);
        }
        else
        {
            throw new Exception($"Cannot find hook '{hook}' in table {_tableName}");
        }
    }

    // <summary>
    // Find the first row containing <paramref name="pattern"/>.
    // Returns the index and the row, or (-1, null) if not found.
    // </summary>
    public (int index, string? row) FindRow(string pattern)
    {
        for (int i = 0; i < _data.Count; i++)
        {
            if (_data[i].Contains(pattern))
                return (i, _data[i]);
        }
        return (-1, null);
    }

    // <summary>
    // Insert a row at a specific index.
    // </summary>
    public void InsertAt(int index, TableRow row) => _data.Insert(index, row.Build());

    // <summary>
    // Insert a raw string row at a specific index.
    // </summary>
    public void InsertAt(int index, string row) => _data.Insert(index, row);

    // <summary>
    // Replace the row at a specific index.
    // </summary>
    public void ReplaceAt(int index, TableRow row) => _data[index] = row.Build();

    // <summary>
    // Replace the row at a specific index with a raw string.
    // </summary>
    public void ReplaceAt(int index, string row) => _data[index] = row;

    // <summary>
    // Remove the row at a specific index.
    // </summary>
    public void RemoveAt(int index) => _data.RemoveAt(index);

    // --- Query ---

    // <summary>
    // Check whether any data row contains <paramref name="pattern"/> as a substring.
    // </summary>
    public bool Contains(string pattern)
    {
        int start = Schema != null ? 1 : 0;
        for (int i = start; i < _data.Count; i++)
        {
            if (!IsHookLine(_data[i]) && _data[i].Contains(pattern))
                return true;
        }
        return false;
    }

    // <summary>
    // Filter data rows by a predicate. Returns matching rows with their original indices.
    // </summary>
    public IEnumerable<(int index, TableRow row)> Where(Func<TableRow, bool> predicate)
    {
        int start = Schema != null ? 1 : 0;
        for (int i = start; i < _data.Count; i++)
        {
            if (IsHookLine(_data[i])) continue;
            var row = new TableRow(_data[i], Schema);
            if (predicate(row))
                yield return (i, row);
        }
    }

    // <summary>
    // Find the first data row where <paramref name="column"/> equals <paramref name="value"/>.
    // Returns the index and row, or (-1, null) if not found.
    // </summary>
    public (int index, TableRow? row) FindByColumn(string column, string value)
    {
        foreach (var (index, row) in Where(r => r[column] == value))
            return (index, row);
        return (-1, null);
    }

    // --- Persistence ---

    // <summary>
    // Save the table back to the game data.
    // </summary>
    public void Save()
    {
        if (_saved) return;
        ModLoader.SetTable(_data, _tableName);
        _saved = true;
    }

    // <summary>
    // Auto-save on dispose.
    // </summary>
    public void Dispose()
    {
        if (!_saved) Save();
    }

    // --- Heuristics ---

    private static bool LooksLikeHeader(string row)
    {
        // Reject hook lines outright
        string trimmedRow = row.TrimStart();
        if (trimmedRow.StartsWith("//") || (trimmedRow.StartsWith("[") && trimmedRow.Contains("]")))
            return false;

        // If at least half of non-empty fields are purely alphabetic/underscore, it's likely a header.
        // Empty fields (e.g. unnamed first column like ";Object;Target;...") are simply skipped.
        string[] fields = row.Split(';');
        int nonEmpty = 0;
        int alphaLike = 0;
        foreach (string f in fields)
        {
            string trimmed = f.Trim();
            if (string.IsNullOrEmpty(trimmed)) continue;
            nonEmpty++;
            if (char.IsDigit(trimmed[0])) return false; // data rows start with numbers
            if (trimmed.All(c => char.IsLetter(c) || c == '_' || c == ' '))
                alphaLike++;
        }

        return nonEmpty > 0 && (double)alphaLike / nonEmpty >= 0.5;
    }

    private static bool IsHookLine(string row)
    {
        string trimmed = row.TrimStart();
        return trimmed.StartsWith("//") || (trimmed.StartsWith("[") && trimmed.Contains("]"));
    }
}
