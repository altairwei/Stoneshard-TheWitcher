namespace TheWitcher;

public static partial class TableUtils
{
    // <summary>
    // Entry point for the stats table fluent API.
    // <example>
    // <code>
    // TableUtils.StatsTable("gml_GlobalScript_table_weapons")
    //     .Append(row => { row["name"] = "my_weapon"; })
    //     .Save();
    // </code>
    // </example>
    // </summary>
    // <param name="tableName">The GML script name, e.g. <c>"gml_GlobalScript_table_weapons"</c>.</param>
    public static TableFluent StatsTable(string tableName)
    {
        return new TableFluent(Table.Load(tableName));
    }

    // <summary>
    // Entry point for localization table injection.
    // <example>
    // <code>
    // TableUtils.LocalizationTable("gml_GlobalScript_table_skills")
    //     .MatchFrom("skill_name_end;")
    //     .InsertAbove(new Loc("my_skill").En("My Skill"))
    //     .Save();
    // </code>
    // </example>
    // </summary>
    // <param name="tableName">The GML script name, e.g. <c>"gml_GlobalScript_table_skills"</c>.</param>
    public static LocalizationTable LocalizationTable(string tableName)
    {
        return new(tableName);
    }
}
