#nullable enable
using ModShardLauncher.Mods;

namespace TheWitcher;

// <summary>
// Fluent localization row builder. Replaces the need for individual <c>LocalizationXxx</c> element classes
// and <c>Dictionary&lt;ModLanguage, string&gt;</c> boilerplate.
// <para>
// Each <c>Loc</c> always builds a single line. For multi-line Speech entries, use multiple <c>Loc</c>
// objects (start marker, body lines, end marker) via batch <c>InsertAbove</c>/<c>InsertBelow</c>.
// </para>
// <para>
// Non-language fields (e.g. Tags, Role for NPC lines) are set via <see cref="Set(string, string)"/>
// and resolved automatically by <see cref="LocalizationTable"/> using the table's header schema.
// </para>
// <example>
// <code>
// // Single-line (most tables):
// new Loc("my_skill").En("My Skill").Ru("Мой навык")
//
// // NPC lines (prefix fields resolved from table header):
// new Loc("my_line").Set("Type", "warrior").En("Hello")
//
// // Speech start/end markers (no translations → id fills all fields):
// new Loc("my_speech")       // → "my_speech;my_speech;...;my_speech;"
// new Loc("my_speech_end")   // → "my_speech_end;my_speech_end;...;my_speech_end;"
//
// // Speech body lines (empty id):
// new Loc("").En("Line text").Ru("Текст строки")
// </code>
// </example>
// </summary>
public class Loc
{
    internal readonly string Id;
    private readonly Dictionary<ModLanguage, string> _translations = new();
    private readonly Dictionary<string, string> _extraFields = new();

    // <summary>Number of languages in <see cref="ModLanguage"/> (game convention order).</summary>
    private static readonly int LanguageCount = Enum.GetNames(typeof(ModLanguage)).Length;

    // <summary>
    // Maps known language names (short, full, native script) to <see cref="ModLanguage"/>.
    // Used by <see cref="Set(string, string)"/> to distinguish language fields from extra fields.
    // </summary>
    internal static readonly Dictionary<string, ModLanguage> LanguageAliases = new(StringComparer.OrdinalIgnoreCase)
    {
        // Short form
        { "Ru", ModLanguage.Russian },
        { "En", ModLanguage.English },
        { "Zh", ModLanguage.Chinese },
        { "De", ModLanguage.German },
        { "Es", ModLanguage.Spanish },
        { "Fr", ModLanguage.French },
        { "It", ModLanguage.Italian },
        { "Pt", ModLanguage.Portuguese },
        { "Pl", ModLanguage.Polish },
        { "Tr", ModLanguage.Turkish },
        { "Ja", ModLanguage.Japanese },
        { "Ko", ModLanguage.Korean },
        // Full enum name
        { "Russian", ModLanguage.Russian },
        { "English", ModLanguage.English },
        { "Chinese", ModLanguage.Chinese },
        { "German", ModLanguage.German },
        { "Spanish", ModLanguage.Spanish },
        { "French", ModLanguage.French },
        { "Italian", ModLanguage.Italian },
        { "Portuguese", ModLanguage.Portuguese },
        { "Polish", ModLanguage.Polish },
        { "Turkish", ModLanguage.Turkish },
        { "Japanese", ModLanguage.Japanese },
        { "Korean", ModLanguage.Korean },
        // Native script (as in table headers)
        { "Русский", ModLanguage.Russian },
        { "中文", ModLanguage.Chinese },
        { "Deutsch", ModLanguage.German },
        { "Español (LATAM)", ModLanguage.Spanish },
        { "Français", ModLanguage.French },
        { "Italiano", ModLanguage.Italian },
        { "Português", ModLanguage.Portuguese },
        { "Polski", ModLanguage.Polish },
        { "Türkçe", ModLanguage.Turkish },
        { "日本語", ModLanguage.Japanese },
        { "한국어", ModLanguage.Korean },
    };

    public Loc(string id)
    {
        Id = id;
    }

    // --- Fluent language setters (short form) ---

    public Loc Ru(string text) { _translations[ModLanguage.Russian] = text; return this; }
    public Loc En(string text) { _translations[ModLanguage.English] = text; return this; }
    public Loc Zh(string text) { _translations[ModLanguage.Chinese] = text; return this; }
    public Loc De(string text) { _translations[ModLanguage.German] = text; return this; }
    public Loc Es(string text) { _translations[ModLanguage.Spanish] = text; return this; }
    public Loc Fr(string text) { _translations[ModLanguage.French] = text; return this; }
    public Loc It(string text) { _translations[ModLanguage.Italian] = text; return this; }
    public Loc Pt(string text) { _translations[ModLanguage.Portuguese] = text; return this; }
    public Loc Pl(string text) { _translations[ModLanguage.Polish] = text; return this; }
    public Loc Tr(string text) { _translations[ModLanguage.Turkish] = text; return this; }
    public Loc Ja(string text) { _translations[ModLanguage.Japanese] = text; return this; }
    public Loc Ko(string text) { _translations[ModLanguage.Korean] = text; return this; }

    // --- Fluent language setters (full name) ---

    public Loc Russian(string text) => Ru(text);
    public Loc English(string text) => En(text);
    public Loc Chinese(string text) => Zh(text);
    public Loc German(string text) => De(text);
    public Loc Spanish(string text) => Es(text);
    public Loc French(string text) => Fr(text);
    public Loc Italian(string text) => It(text);
    public Loc Portuguese(string text) => Pt(text);
    public Loc Polish(string text) => Pl(text);
    public Loc Turkish(string text) => Tr(text);
    public Loc Japanese(string text) => Ja(text);
    public Loc Korean(string text) => Ko(text);

    // --- Unified Set ---

    // <summary>
    // Set a translation for a specific <see cref="ModLanguage"/>.
    // </summary>
    public Loc Set(ModLanguage lang, string text) { _translations[lang] = text; return this; }

    // <summary>
    // Set a field by name. If the name matches a known language (short form, full name, or native script),
    // it sets the translation. Otherwise, it stores the field as an extra (prefix) field.
    // Extra fields are resolved by <see cref="LocalizationTable"/> using the table's header schema.
    // </summary>
    public Loc Set(string field, string value)
    {
        if (LanguageAliases.TryGetValue(field, out ModLanguage lang))
            _translations[lang] = value;
        else
            _extraFields[field] = value;
        return this;
    }

    // --- Build ---

    // <summary>
    // Build a single localization line: <c>"id;textRu;textEn;...;textKo;"</c>.
    // <para>
    // If no translations are set, the id is used for all language fields (for Speech markers).
    // Extra fields set via <see cref="Set(string, string)"/> are ignored in this overload;
    // use the schema-aware <see cref="Build(IReadOnlyList{string}, string)"/> instead.
    // </para>
    // </summary>
    public string Build()
    {
        return Build(null, "");
    }

    // <summary>
    // Build a localization line with prefix columns resolved from the table schema.
    // </summary>
    // <param name="prefixColumns">Prefix column names parsed from the table header. Null if no prefix.</param>
    // <param name="prefixDefault">Default value for prefix columns not set via <see cref="Set(string, string)"/>.</param>
    internal string Build(IReadOnlyList<string>? prefixColumns, string prefixDefault)
    {
        // Translation part
        string translationPart;
        if (_translations.Count == 0)
        {
            translationPart = string.Concat(Enumerable.Repeat($"{Id};", LanguageCount));
        }
        else
        {
            Dictionary<ModLanguage, string> filled = TableInjection.FillDictionary(_translations);
            translationPart = string.Concat(filled.Values.Select(x => $"{x};"));
        }

        // Prefix part (from schema)
        if (prefixColumns != null && prefixColumns.Count > 0)
        {
            string prefixPart = string.Concat(prefixColumns.Select(col =>
                _extraFields.TryGetValue(col, out string? val) ? $"{val};" : $"{prefixDefault};"));
            return $"{Id};{prefixPart}{translationPart}";
        }

        return $"{Id};{translationPart}";
    }

    // <summary>
    // Build for internal use by <see cref="LocalizationTable"/>.
    // </summary>
    internal IEnumerable<string> BuildLines(IReadOnlyList<string>? prefixColumns, string prefixDefault)
    {
        yield return Build(prefixColumns, prefixDefault);
    }
}
