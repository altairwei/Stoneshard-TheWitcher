using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddSkill_Quen_Sign()
    {
        AdjustSkillIcon("s_skills_quen_sign");

        Msl.InjectTableSkillsLocalization(
            new LocalizationSkill(
                id: "Quen_Sign",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Quen Sign"},
                    {ModLanguage.Chinese, "昆恩法印"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, @"Opens the menu for ~w~crafting weapon coating oil~/~."},
                    {ModLanguage.Chinese, @"被动性法印，形成一个法力护盾围绕猎魔人，让他暂时无法被伤害，可以吸收物理伤害。"}
                }
            )
        );

        Msl.InjectTableSkillsStats(
            id: "Quen_Sign",
            Object: "o_b_magical_shield",
            hook: Msl.SkillsStatsHook.MAGICMASTERY,
            Range: "0",
            KD: 6,
            MP: 20,
            Duration: 6,
            Class: Msl.SkillsStatsClass.spell,
            Branch: "witcher",
            Spell: true
        );

        UndertaleGameObject o_skill_quen_sign = Msl.AddObject(
            name: "o_skill_quen_sign",
            parentName: "o_skill",
            spriteName: "s_skills_quen_sign",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_quen_sign.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                skill = ""Quen_Sign""
                scr_skill_atr(""Quen_Sign"")
                can_learn = true
                ds_list_add(attribute, ds_map_find_value(global.attribute, ""Magic_Power""))
                ignore_interact = true
                is_moving = false
            "),
            new MslEvent(eventType: EventType.Other, subtype: 13, code: @"
                event_inherited()
                scr_stop_player()
            ")
        );

        UndertaleGameObject o_skill_quen_sign_ico = Msl.AddObject(
            name: "o_skill_quen_sign_ico",
            parentName: "o_skill_ico",
            spriteName: "s_skills_quen_sign",
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_quen_sign_ico.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                child_skill = o_skill_quen_sign
                event_perform_object(child_skill, ev_create, 0)
            ")
        );

        // Buff: Magical Shield

        Msl.InjectTableModifiersLocalization(
            new LocalizationModifier(
                id: "o_b_magical_shield",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Magical Shield"},
                    {ModLanguage.Chinese, "法力护盾"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, @"Opens the menu for ~w~crafting weapon coating oil~/~."},
                    {ModLanguage.Chinese, @"法力护盾（耐久：~lg~/*Shield_Duration*///*Max_Duration*/~/~）围绕猎魔人，让他暂时无法被伤害，可以吸收物理伤害。"}
                }
            )
        );

        UndertaleGameObject o_b_magical_shield = Msl.AddObject(
            name: "o_b_magical_shield",
            parentName: "o_magical_buff",
            spriteName: "s_b_stone_armor",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        o_b_magical_shield.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                duration = 6
                Max_Duration = 25
                Shield_Duration = Max_Duration
                scr_buff_atr()
                buff_snd = snd_spell_stone_armor
            "),

            new MslEvent(eventType: EventType.Destroy, subtype: 0, code: @"
                event_inherited()
                if instance_exists(owner)
                {
                    scr_audio_play_at(choose(snd_spell_stone_armor_explode_1, snd_spell_stone_armor_explode_2))
                }
            "),

            new MslEvent(eventType: EventType.Alarm, subtype: 2, code: @"
                event_inherited()
                ds_map_clear(data)
                ds_map_add(data, ""MP_Restoration"", -25)
                if instance_exists(owner)
                {
                    Max_Duration = 25 + 25 * (owner.Magic_Power / 100)
                    Shield_Duration = Max_Duration
                }
            "),

            // gml_Object_o_b_stone_armor_Other_13 每次攻击就会调用？
            // scr_damage => event_user(3) 有 add_damage 感觉可以抵消伤害
            // 力挽狂澜貌似可以抵消伤害。
            // 闪避可以避免消耗护盾耐久
            new MslEvent(eventType: EventType.Other, subtype: 13, code: @"
                event_inherited()
                add_damage = -1 * damage
            "),

            new MslEvent(eventType: EventType.Other, subtype: 14, code: @"
                event_inherited()
                with (target)
                    scr_guiAnimation_ext(x, y, s_brace_yourself_proc)
                if (Shield_Duration <= 0)
                    instance_destroy()
            "),

            new MslEvent(eventType: EventType.Other, subtype: 25, code: @"
                event_inherited()
                if instance_exists(owner)
                {
                    ds_map_replace(text_map, ""Max_Duration"", Max_Duration)
                    ds_map_replace(text_map, ""Shield_Duration"", Shield_Duration)
                }
            ")
        );

        Msl.LoadGML("gml_GlobalScript_scr_attack")
            .MatchFrom("var DMG_r = 0")
            .InsertAbove(@"
                with (scr_instance_exists_in_list(o_b_magical_shield, _target.buffs))
                {
                    P_proc = true
                }
            ")
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_damage_physical_calc")
            .MatchFrom("var _dmgReal = math_round(argument3 * (max(((argument1 - argument4 * _partDamageNormalizer - argument0.tmpDEF * (1 - Armor_Piercing / 100)) * (1 - argument2 / 100)), 0)))")
            .InsertAbove(@"
        with (scr_instance_exists_in_list(o_b_magical_shield, argument0.buffs))
        {
            var _durReduc = min(argument1, (Shield_Duration * _partDamageNormalizer))
            argument1 = math_round(max(argument1 - Shield_Duration * _partDamageNormalizer, 0))
            Shield_Duration -= _durReduc
            event_user(4)
        }
            ")
            .MatchFrom("var _dmgReal = math_round(argument3 * (max(((argument1 - argument4 * _partDamageNormalizer - argument0.tmpDEF * 0.5 * argument6) * (1 - argument2 / 100)), 0)))")
            .InsertAbove(@"
        with (scr_instance_exists_in_list(o_b_magical_shield, argument0.buffs))
        {
            var _durReduc = min(argument1, (Shield_Duration * _partDamageNormalizer))
            argument1 = math_round(max(argument1 - Shield_Duration * _partDamageNormalizer, 0))
            Shield_Duration -= _durReduc
            event_user(4)
        }
            ")
            .Save();
    }
}