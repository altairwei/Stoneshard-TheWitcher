#nullable enable
using System.Text.RegularExpressions;
using ModShardLauncher;
using ModShardLauncher.Mods;

namespace TheWitcher;

// <summary>
// Assembly-level table injection helpers backing <see cref="LocalizationTable"/>.
// Vendored from ModShardLauncher's <c>Localization</c> utility class
// (CreateInjectionTable / CreateInjectionTableBelow / InjectTable / SetDictionary).
// </summary>
public static class TableInjection
{
    // <summary>
    // All languages defined by <see cref="ModLanguage"/>, in enum (game convention) order.
    // </summary>
    public static readonly IReadOnlyList<ModLanguage> LanguageList = Enum.GetValues<ModLanguage>();

    // <summary>
    // Fill a destination ModLanguage dictionary with the values contained in a source dictionary.
    // The source dictionary is assumed to always have an English key which will be used as default
    // for languages missing from the source.
    // </summary>
    public static Dictionary<ModLanguage, string> FillDictionary(Dictionary<ModLanguage, string> source)
    {
        Dictionary<ModLanguage, string> dest = new();
        string englishName = source[ModLanguage.English];
        foreach (ModLanguage language in LanguageList)
        {
            dest[language] = source.TryGetValue(language, out string? value) ? value : englishName;
        }
        return dest;
    }

    // <summary>
    // Build an assembly-stream transform that inserts <paramref name="datas"/> elements
    // right after the line containing each anchor (which places them ABOVE the anchor
    // in the final table data, because push.s order is reversed), and fixes up the
    // <c>@@NewGMLArray@@</c> argc accordingly.
    // </summary>
    public static Func<IEnumerable<string>, IEnumerable<string>> CreateInjectionTable(params (string anchor, IEnumerable<string> elements)[] datas)
    {
        IEnumerable<string> func(IEnumerable<string> input)
        {
            int extraLines = 0;
            foreach (string item in input)
            {
                if (item.Contains("NewGMLArray"))
                {
                    int nLines = int.Parse(Regex.Match(item, @"argc=(\d+)").Groups[1].Value);
                    yield return $"call.i @@NewGMLArray@@(argc={nLines + extraLines})";
                }
                else
                {
                    yield return item;
                }

                foreach (string element in datas.Where(_ => item.Contains(_.anchor)).SelectMany(_ => _.elements).Reverse())
                {
                    extraLines++;
                    yield return "conv.s.v";
                    yield return $"push.s \"{element}\"";
                }
            }
        }

        return func;
    }

    // <summary>
    // Like <see cref="CreateInjectionTable"/> but inserts elements BEFORE the anchor line
    // in the assembly stream. Because push.s order is reversed, this results in
    // the new entries appearing BELOW the anchor in the final table data.
    // </summary>
    public static Func<IEnumerable<string>, IEnumerable<string>> CreateInjectionTableBelow(params (string anchor, IEnumerable<string> elements)[] datas)
    {
        IEnumerable<string> func(IEnumerable<string> input)
        {
            // We need to look ahead for anchor lines, so buffer the input
            var items = input.ToList();

            for (int i = 0; i < items.Count; i++)
            {
                string item = items[i];

                if (item.Contains("NewGMLArray"))
                {
                    // Pre-count how many lines we'll inject so the array size is correct.
                    int totalExtra = datas.Sum(d => items.Any(line => line.Contains(d.anchor)) ? d.elements.Count() : 0);
                    int nLines = int.Parse(Regex.Match(item, @"argc=(\d+)").Groups[1].Value);
                    yield return $"call.i @@NewGMLArray@@(argc={nLines + totalExtra})";
                    continue;
                }

                // Insert elements BEFORE the anchor line
                var matchingElements = datas.Where(d => item.Contains(d.anchor)).SelectMany(d => d.elements).Reverse();
                foreach (string element in matchingElements)
                {
                    yield return $"push.s \"{element}\"";
                    yield return "conv.s.v";
                }

                yield return item;
            }
        }

        return func;
    }

    // <summary>
    // Apply an assembly-stream transform to a table's GML script and save it back.
    // </summary>
    public static void InjectTable(string tableName, Func<IEnumerable<string>, IEnumerable<string>> createInjectionTable)
    {
        Msl.LoadAssemblyAsString(tableName)
            .Apply(createInjectionTable)
            .Save();
    }
}
