#nullable enable
using System.Text.RegularExpressions;
using ModShardLauncher;
using UndertaleModLib;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Models;

namespace TheWitcher;

// <summary>
// Fluent API for localization table injection, mirroring the stats table fluent pattern.
// Wraps the assembly bytecode injection mechanism.
// <para>
// Automatically parses the table header from the assembly to discover prefix columns
// (fields between id and the first language column, e.g. Tags, Role, Type, Faction, Settlement).
// </para>
// <para>
// Implements <see cref="IDisposable"/> — calling <see cref="Dispose"/> auto-saves pending insertions.
// </para>
// <example>
// <code>
// TableUtils.LocalizationTable("gml_GlobalScript_table_lines")
//     .PrefixDefault("any")
//     .MatchFrom("[NPC] GREETINGS;")
//     .InsertAbove(new Loc("my_greeting").Set("Type", "warrior").En("Hello!"))
//     .Save();
// </code>
// </example>
// </summary>
public class LocalizationTable : IDisposable
{
    private readonly string _tableName;
    private readonly List<(string anchor, IEnumerable<string> lines, bool below)> _pending = new();
    private bool _saved;

    // <summary>
    // Prefix column names parsed from the table header (between id and first language column).
    // Empty if the table has no prefix columns.
    // </summary>
    public IReadOnlyList<string> PrefixColumns { get; }

    // <summary>
    // Default value for prefix columns not explicitly set via <see cref="Loc.Set(string, string)"/>.
    // </summary>
    public string DefaultPrefix { get; private set; } = "";

    public LocalizationTable(string tableName)
    {
        _tableName = tableName;
        PrefixColumns = ParsePrefixColumns(tableName);
    }

    // <summary>
    // Set the default value for prefix columns not explicitly set on individual <see cref="Loc"/> entries.
    // </summary>
    public LocalizationTable PrefixDefault(string value)
    {
        DefaultPrefix = value;
        return this;
    }

    // <summary>
    // Find the anchor line matching <paramref name="pattern"/> and return a matched context
    // for chained operations (<c>InsertAbove</c>, <c>InsertBelow</c>).
    // </summary>
    public LocTableMatched MatchFrom(string pattern)
    {
        return new LocTableMatched(this, pattern);
    }

    // <summary>
    // Queue lines for insertion relative to the specified anchor.
    // </summary>
    internal void AddPending(string anchor, IEnumerable<string> lines, bool below)
    {
        _pending.Add((anchor, lines, below));
    }

    // <summary>
    // Build a <see cref="Loc"/> entry into lines using this table's prefix schema.
    // </summary>
    internal IEnumerable<string> BuildLoc(Loc loc)
    {
        return loc.BuildLines(PrefixColumns, DefaultPrefix);
    }

    // <summary>
    // Apply all pending insertions to the assembly bytecode and save.
    // </summary>
    public void Save()
    {
        if (_saved) return;
        if (_pending.Count == 0) return;

        // Split into above (before anchor) and below (after anchor) groups
        var aboveItems = _pending.Where(p => !p.below).ToList();
        var belowItems = _pending.Where(p => p.below).ToList();

        // Build the combined injection function
        Func<IEnumerable<string>, IEnumerable<string>>? aboveInjection = null;
        Func<IEnumerable<string>, IEnumerable<string>>? belowInjection = null;

        if (aboveItems.Count > 0)
        {
            var grouped = aboveItems
                .GroupBy(p => p.anchor)
                .Select(g => (g.Key, g.SelectMany(p => p.lines)))
                .ToArray();
            aboveInjection = TableInjection.CreateInjectionTable(grouped);
        }

        if (belowItems.Count > 0)
        {
            var grouped = belowItems
                .GroupBy(p => p.anchor)
                .Select(g => (g.Key, g.SelectMany(p => p.lines)))
                .ToArray();
            belowInjection = TableInjection.CreateInjectionTableBelow(grouped);
        }

        // Compose: apply above first, then below
        Func<IEnumerable<string>, IEnumerable<string>> combined = input =>
        {
            var result = input;
            if (aboveInjection != null) result = aboveInjection(result);
            if (belowInjection != null) result = belowInjection(result);
            return result;
        };

        TableInjection.InjectTable(_tableName, combined);
        _saved = true;
    }

    // <summary>
    // Auto-save on dispose.
    // </summary>
    public void Dispose()
    {
        if (!_saved) Save();
    }

    // --- Header parsing ---

    // <summary>
    // Parse the table header from the assembly to discover prefix columns.
    // Prefix columns are fields between the id (first field) and the first language column.
    // </summary>
    private static List<string> ParsePrefixColumns(string tableName)
    {
        try
        {
            UndertaleCode code = Msl.GetUMTCodeFromFile(tableName);
            string asm = code.Disassemble(DataLoader.data.Variables, DataLoader.data.CodeLocals.For(code));

            // Extract all push.s values and reverse to get table row order
            var rows = Regex.Matches(asm, @"push\.s ""(.+?)""")
                .Select(m => m.Groups[1].Value)
                .Reverse()
                .ToList();

            if (rows.Count == 0) return new();

            // First row is the header
            string header = rows[0];
            string[] fields = header.Split(';');

            // Find prefix columns: skip id (first field), collect until first language column
            var prefixCols = new List<string>();
            foreach (string f in fields.Skip(1))
            {
                string trimmed = f.Trim();
                if (string.IsNullOrEmpty(trimmed)) continue;
                if (Loc.LanguageAliases.ContainsKey(trimmed)) break; // hit a language column
                prefixCols.Add(trimmed);
            }
            return prefixCols;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Could not parse header for {tableName}: {ex.Message}");
            return new();
        }
    }
}

// <summary>
// Represents a matched anchor position in the localization table fluent chain.
// Provides <c>InsertAbove</c> and <c>InsertBelow</c> operations.
// </summary>
public class LocTableMatched
{
    private readonly LocalizationTable _parent;
    private readonly string _anchor;

    internal LocTableMatched(LocalizationTable parent, string anchor)
    {
        _parent = parent;
        _anchor = anchor;
    }

    // --- InsertAbove ---

    // <summary>
    // Insert a single <see cref="Loc"/> entry above the matched anchor.
    // </summary>
    public LocalizationTable InsertAbove(Loc loc)
    {
        _parent.AddPending(_anchor, _parent.BuildLoc(loc), below: false);
        return _parent;
    }

    // <summary>
    // Insert multiple <see cref="Loc"/> entries above the matched anchor.
    // </summary>
    public LocalizationTable InsertAbove(params Loc[] locs)
    {
        _parent.AddPending(_anchor, locs.SelectMany(l => _parent.BuildLoc(l)), below: false);
        return _parent;
    }

    // <summary>
    // Insert a raw line above the matched anchor.
    // </summary>
    public LocalizationTable InsertAbove(string rawLine)
    {
        _parent.AddPending(_anchor, new[] { rawLine }, below: false);
        return _parent;
    }

    // <summary>
    // Insert multiple raw lines above the matched anchor.
    // </summary>
    public LocalizationTable InsertAbove(IEnumerable<string> rawLines)
    {
        _parent.AddPending(_anchor, rawLines, below: false);
        return _parent;
    }

    // --- InsertBelow ---

    // <summary>
    // Insert a single <see cref="Loc"/> entry below the matched anchor.
    // </summary>
    public LocalizationTable InsertBelow(Loc loc)
    {
        _parent.AddPending(_anchor, _parent.BuildLoc(loc), below: true);
        return _parent;
    }

    // <summary>
    // Insert multiple <see cref="Loc"/> entries below the matched anchor.
    // </summary>
    public LocalizationTable InsertBelow(params Loc[] locs)
    {
        _parent.AddPending(_anchor, locs.SelectMany(l => _parent.BuildLoc(l)), below: true);
        return _parent;
    }

    // <summary>
    // Insert a raw line below the matched anchor.
    // </summary>
    public LocalizationTable InsertBelow(string rawLine)
    {
        _parent.AddPending(_anchor, new[] { rawLine }, below: true);
        return _parent;
    }

    // <summary>
    // Insert multiple raw lines below the matched anchor.
    // </summary>
    public LocalizationTable InsertBelow(IEnumerable<string> rawLines)
    {
        _parent.AddPending(_anchor, rawLines, below: true);
        return _parent;
    }
}
