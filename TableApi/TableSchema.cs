
namespace TheWitcher;

// <summary>
// Parses a semicolon-delimited header row into a column-name to index mapping.
// Enables position-independent, named-column access to table rows.
// </summary>
public class TableSchema
{
    private readonly Dictionary<string, int> _nameToIndex;

    // <summary>
    // Ordered list of column names as they appear in the header.
    // Empty strings represent separator columns (double semicolons in the original row).
    // </summary>
    public IReadOnlyList<string> ColumnNames { get; }

    // <summary>
    // Total number of columns including separators.
    // </summary>
    public int ColumnCount => ColumnNames.Count;

    private TableSchema(IReadOnlyList<string> columnNames, Dictionary<string, int> nameToIndex)
    {
        ColumnNames = columnNames;
        _nameToIndex = nameToIndex;
    }

    // <summary>
    // Parse a semicolon-delimited header row into a <see cref="TableSchema"/>.
    // </summary>
    // <param name="headerRow">A raw header row like "name;tier;id;type;;HP;MP;"</param>
    // <returns>A new <see cref="TableSchema"/> instance.</returns>
    public static TableSchema FromHeader(string headerRow)
    {
        // Split on ';' and drop the trailing empty element (rows end with ';')
        string[] parts = headerRow.Split(';');
        List<string> columns = parts.Length > 0 && parts[^1] == ""
            ? parts[..^1].ToList()
            : parts.ToList();

        Dictionary<string, int> nameToIndex = new();
        for (int i = 0; i < columns.Count; i++)
        {
            string col = columns[i].Trim();
            // Auto-name the first empty column as "id" if no "id" column exists (game convention)
            if (i == 0 && string.IsNullOrEmpty(col))
            {
                col = "id";
            }
            columns[i] = col;
            if (!string.IsNullOrEmpty(col) && !nameToIndex.ContainsKey(col))
            {
                nameToIndex[col] = i;
            }
        }

        return new TableSchema(columns.AsReadOnly(), nameToIndex);
    }

    // <summary>
    // Get the column index for a given column name.
    // </summary>
    // <exception cref="KeyNotFoundException">Thrown when the column name is not found.</exception>
    public int GetIndex(string columnName)
    {
        if (_nameToIndex.TryGetValue(columnName, out int index))
            return index;
        throw new KeyNotFoundException($"Column '{columnName}' not found in schema. Available columns: {string.Join(", ", _nameToIndex.Keys)}");
    }

    // <summary>
    // Check whether a column name exists in this schema.
    // </summary>
    public bool HasColumn(string columnName) => _nameToIndex.ContainsKey(columnName);

    // <summary>
    // Create a new empty <see cref="TableRow"/> with the correct number of columns, linked to this schema.
    // </summary>
    public TableRow NewRow() => new(ColumnCount, this);
}
