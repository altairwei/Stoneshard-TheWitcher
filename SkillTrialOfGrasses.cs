using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddSkill_Trial_Of_Grasses()
    {
        AdjustSkillIcon("s_skills_trial_of_grasses");

        Msl.InjectTableSkillsLocalization(
            new LocalizationSkill(
                id: "Trial_Of_Grasses",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Trial Of Grasses"},
                    {ModLanguage.Chinese, "青草试炼"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, @"No translation"},
                    {ModLanguage.Chinese, string.Join("##",
                        "解锁~w~煎药~/~制作。",
                        "此外，令猎魔人：",
                        "免疫变化幅度~lg~+0.02~/~#生命上限~lg~+/*MAXHP*/~/~#生命自动恢复~lg~+/*HR*/%~/~#视野~lg~+2~/~#距离加成~lg~+1~/~#反击几率~lg~+/*CTA*/%~/~#闪躲几率~lg~+/*EVS*/%~/~"
                    )}
                }
            )
        );

        UndertaleGameObject o_skill_trial_of_grasses = Msl.AddObject(
            name: "o_skill_trial_of_grasses",
            parentName: "o_skill_passive",
            spriteName: "s_skills_trial_of_grasses",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_trial_of_grasses.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                scr_skill_atr(""Trial_Of_Grasses"")
                ds_list_add(attribute,
                    ds_map_find_value(global.attribute, ""AGL""),
                    ds_map_find_value(global.attribute, ""Vitality"")
                )
            "),

            new MslEvent(eventType: EventType.Other, subtype: 17, code: @"
                ds_map_clear(data)

                var _maxhp = 1 * owner.Vitality
                ds_map_replace(text_map, ""MAXHP"", _maxhp)

                var _hr = 1.5 * owner.Vitality
                ds_map_replace(text_map, ""HR"", _maxhp)

                var _cta = 1.5 * owner.AGL
                ds_map_replace(text_map, ""CTA"", _cta)

                var _evs = 1.5 * owner.AGL
                ds_map_replace(text_map, ""EVS"", _evs)

                if (is_open)
                {
                    ds_map_add(data, ""Immunity_Change"", 0.02)
                    ds_map_add(data, ""max_hp"", _maxhp)
                    ds_map_add(data, ""Health_Restoration"", _hr)
                    ds_map_add(data, ""VSN"", 2)
                    ds_map_add(data, ""Bonus_Range"", 1)
                    ds_map_add(data, ""CTA"", _cta)
                    ds_map_add(data, ""EVS"", _evs)
                }

                event_inherited()
            ")
        );
    }
}