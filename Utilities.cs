using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    /*
    private void InjectItemsToTable(string table, string? anchor = null, params Dictionary<string, string>[] items)
    {
        List<string> lines = Msl.ThrowIfNull(ModLoader.GetTable(table));
        List<string> header = lines[0].Split(';').ToList();

        List<string> targets = new(items.Length);
        foreach (var item in items)
        {
            string[] record = new string[header.Count];
            foreach (var kv in item)
            {
                int idx = header.FindIndex(h => h == kv.Key);

                if (idx == -1)
                    continue;

                record[idx] = kv.Value;
            }

            targets.Add(string.Join(';', record));
        }

        if (anchor != null)
            lines.InsertRange(lines.FindIndex(l => l.StartsWith(anchor)), targets);
        else
            lines.AddRange(targets);

        ModLoader.SetTable(lines, table);
    }

    private void InjectItemsToTable(string table, string? anchor = null, params Dictionary<int, string>[] items)
    {
        List<string> lines = Msl.ThrowIfNull(ModLoader.GetTable(table));
        List<string> header = lines[0].Split(';').ToList();

        List<string> targets = new(items.Length);
        foreach (var item in items)
        {
            string[] record = new string[header.Count];
            foreach (var kv in item)
                record[kv.Key] = kv.Value;

            targets.Add(string.Join(';', record));
        }

        if (anchor != null)
            lines.InsertRange(lines.FindIndex(l => l.StartsWith(anchor)), targets);
        else
            lines.AddRange(targets);

        ModLoader.SetTable(lines, table);
    }
    */

    private void InjectItemsToTable(string table, string? anchor = null, params Dictionary<int, string>[] items)
    {
        List<string> lines = Msl.ThrowIfNull(ModLoader.GetTable(table));
        int colCount = lines[0].Split(';').Length;

        var targets = new List<string>(items.Length);
        foreach (var item in items)
        {
            var record = new string[colCount];
            foreach (var kv in item)
            {
                if (kv.Key >= 0 && kv.Key < colCount)
                    record[kv.Key] = kv.Value;
            }
            targets.Add(string.Join(';', record));
        }

        if (anchor != null)
        {
            int pos = lines.FindIndex(l => l.StartsWith(anchor));
            pos = pos < 0 ? lines.Count : pos;
            lines.InsertRange(pos + 1, targets);
        }
        else
        {
            lines.AddRange(targets);
        }

        ModLoader.SetTable(lines, table);
    }

    private void InjectItemsToTable(string table, string? anchor = null, params Dictionary<string, string>[] items)
    {
        List<string> lines = Msl.ThrowIfNull(ModLoader.GetTable(table));
        var header = lines[0].Split(';');
        var name2idx = header
            .Select((name, idx) => new KeyValuePair<string, int>(name, idx))
            .ToDictionary(p => p.Key, p => p.Value);

        var mapped = new Dictionary<int, string>[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            var src = items[i];
            var dst = new Dictionary<int, string>(src.Count);
            foreach (var kv in src)
                if (name2idx.TryGetValue(kv.Key, out int idx))
                    dst[idx] = kv.Value;
            mapped[i] = dst;
        }

        InjectItemsToTable(table, anchor, mapped);
    }

}