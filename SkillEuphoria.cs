using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddSkill_Euphoria()
    {
        AdjustSkillIcon("s_passive_Euphoria");

        Msl.InjectTableSkillsLocalization(
            new LocalizationSkill(
                id: "euphoria",
                name: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Euphoria" },
                    { ModLanguage.Chinese, "瘾头" }
                },
                description: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.English,
                        string.Join(
                            "##",
                            "The maximum Intoxication is raised to ~w~200%~/~, and the threshold for activating negative effects caused by Intoxication is increased by ~lg~/*INTOX_TOL*/%~/~.",
                            "Grants immunity to the ~r~continuous deterioration of maximum health~/~ caused by ~r~Severe Intoxication~/~, and raises the decoction capacity limit by ~lg~+2~/~.",
                            "Additionally, for every ~lg~1%~/~ Intoxication, both Weapon Damage and Magic Power are increased by ~lg~1%~/~."
                        )
                    },
                    {
                        ModLanguage.Chinese,
                        string.Join(
                            "##",
                            "迷醉值上限提升至~w~200%~/~，且迷醉触发负面效果的临界值便提高~lg~/*INTOX_TOL*/%~/~。",
                            "免疫因~r~极度迷醉~/~导致的~r~生命值上限持续衰减~/~，同时煎药服用数量上限提升 ~lg~+2~/~。",
                            "此外，每有 ~lg~1%~/~ 迷醉，兵器伤害与法力便提升~lg~1%~/~。"
                        )
                    }
                }
            )
        );

        UndertaleGameObject o_pass_skill_euphoria = Msl.AddObject(
            name: "o_pass_skill_euphoria",
            parentName: "o_skill_passive",
            spriteName: "s_passive_Euphoria",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_pass_skill_euphoria.ApplyEvent(
            new MslEvent(
                eventType: EventType.Create,
                subtype: 0,
                code: @"
                event_inherited()
                scr_skill_atr(""euphoria"")
                ds_list_add(attribute, ds_map_find_value(global.attribute, ""Vitality""))"
            ),
            new MslEvent(
                eventType: EventType.Other,
                subtype: 17,
                code: @"
                ds_map_clear(data)

                var _intox_tol = 2.5 * owner.Vitality
                ds_map_replace(text_map, ""INTOX_TOL"", _intox_tol)

                var _intoxication = 0;
                with owner
                    _intoxication = scr_atr(""Intoxication"")

                ds_map_add(data, ""Weapon_Damage"", _intoxication)
                ds_map_add(data, ""Magic_Power"", _intoxication)

                event_inherited()"
            )
        );

        Msl.LoadGML("gml_GlobalScript_scr_intoxication_check")
            .MatchFrom(
                "return [(25 + _wildhunt_modifier), (50 + _wildhunt_modifier), (75 + _wildhunt_modifier)];"
            )
            .ReplaceBy(@"
                var _euphoria_modifier = 0
                if o_pass_skill_euphoria.is_open
                {
                    with o_pass_skill_euphoria
                        _euphoria_modifier = ds_map_find_value(text_map, ""INTOX_TOL"")
                }

                return [(25 + _wildhunt_modifier + _euphoria_modifier), (50 + _wildhunt_modifier + _euphoria_modifier), (75 + _wildhunt_modifier + _euphoria_modifier)];"
            )
            .Save();

        Msl.LoadGML("gml_Object_o_db_tox3_Other_10")
            .MatchFrom("turn_count++")
            .InsertAbove(@"
                if o_pass_skill_euphoria.is_open
                    return;"
            )
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_intoxication_change")
            .MatchFromUntil(
                @"if (scr_atr(""Intoxication"") > 100)",
                @"scr_atr_set(""Intoxication"", 100)"
            )
            .ReplaceBy(@"
                if (scr_atr(""Intoxication"") > 100 && !o_pass_skill_euphoria.is_open)
                    scr_atr_set(""Intoxication"", 100)
                else if (scr_atr(""Intoxication"") > 200)
                    scr_atr_set(""Intoxication"", 200)"
            )
            .Save();
    }
}
