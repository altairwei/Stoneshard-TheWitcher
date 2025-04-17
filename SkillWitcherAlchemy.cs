using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void PatchSkill_Witcher_Alchemy()
    {
        AdjustSkillIcon("s_witcher_alchemy");

        // Skill - Witcher Alchemy

        Msl.InjectTableSkillsLocalization(
            new LocalizationSkill(
                id: "Witcher_Alchemy",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "The Witcher's Alchemy"},
                    {ModLanguage.Chinese, "猎魔人炼金术"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, @"Opens the menu for ~w~crafting weapon coating oil~/~."},
                    {ModLanguage.Chinese, @"能够打开~w~猎魔人炼金术~/~界面，学会制作~w~剑油~/~。"}
                }
            )
        );

        Msl.InjectTableSkillsStats(
            id: "Witcher_Alchemy",
            Object: "object",
            hook: Msl.SkillsStatsHook.BASIC
        );

        UndertaleGameObject o_skill_witcher_alchemy = Msl.AddObject(
            name: "o_skill_witcher_alchemy",
            parentName: "o_skill",
            spriteName: "s_witcher_alchemy",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_witcher_alchemy.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                skill = ""Witcher_Alchemy""
                scr_skill_atr(""Witcher_Alchemy"")
            "),
            new MslEvent(eventType: EventType.Other, subtype: 13, code: @"
                with (o_player)
                    lock_movement = false
            "),
            new MslEvent(eventType: EventType.Other, subtype: 14, code: @"
                event_inherited()
                is_ready = true
            "),
            new MslEvent(eventType: EventType.Other, subtype: 17, code: @"
                event_inherited()
                MPcost = 0
                KD = 0
            ")
        );

        UndertaleGameObject o_skill_witcher_alchemy_ico = Msl.AddObject(
            name: "o_skill_witcher_alchemy_ico",
            parentName: "o_skill_ico",
            spriteName: "s_witcher_alchemy",
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_witcher_alchemy_ico.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                child_skill = o_skill_witcher_alchemy
                event_perform_object(child_skill, ev_create, 0)
            ")
        );
    }
}