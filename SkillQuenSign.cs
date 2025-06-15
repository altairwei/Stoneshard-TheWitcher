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
                    {ModLanguage.Chinese, @"触发~w~6~/~回合耐久为~lg~/*Shield_Duration*/~/~点的~lg~“法力护盾”~/~：##精力自动恢复~r~-25%~/~##触发时有~w~/*Cure_Chance*/%~/~的概率会消除~r~流血~/~和~o~着火~/~状态。##护盾会吸收所有物理、自然和魔法伤害，直到破损为止。"}
                }
            )
        );

        Msl.InjectTableSkillsStats(
            id: "Quen_Sign",
            Object: "o_b_magical_shield",
            hook: Msl.SkillsStatsHook.MAGICMASTERY,
            Range: "0",
            KD: 8,
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

        string Shield_Duration = "40 * power(owner.Magic_Power / 100, 3)";
        string Cure_Chance = "45 * owner.Magic_Power / 100";

        o_skill_quen_sign.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                skill = ""Quen_Sign""
                scr_skill_atr(""Quen_Sign"")
                ds_list_add(attribute, ds_map_find_value(global.attribute, ""Magic_Power""))
                ignore_interact = true
                is_moving = false
            "),
            new MslEvent(eventType: EventType.Other, subtype: 13, code: @$"
                event_inherited()
                scr_stop_player()
                if instance_exists(owner)
                {{
                    var _cure = is_crit ? 1 : scr_chance_value({Cure_Chance})
                    if (_cure)
                    {{
                        with (scr_instance_exists_in_list(o_db_bleed_parent, owner.buffs))
                            instance_destroy()
                        with (scr_instance_exists_in_list(o_db_gaping_wound, owner.buffs))
                            instance_destroy()
                        with (scr_instance_exists_in_list(o_db_fire, owner.buffs))
                            instance_destroy()
                    }}
                }}
            "),
            new MslEvent(eventType: EventType.Other, subtype: 17, code: @$"
                if instance_exists(owner)
                {{
                    ds_map_replace(data, ""Shield_Duration"", {Shield_Duration})
                    ds_map_replace(data, ""Cure_Chance"", {Cure_Chance})
                }}
                event_inherited()
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
                    {ModLanguage.English, @"Magical Shield absorbs all physical, natural and magical damage until broken. Current duration: ~lg~/*Shield_Duration*///*Max_Duration*/~/~"},
                    {ModLanguage.Chinese, @"法力护盾会吸收所有物理、自然和魔法伤害，直到破损为止。当前耐久：~lg~/*Shield_Duration*///*Max_Duration*/~/~"}
                }
            )
        );

        UndertaleGameObject o_b_magical_shield = Msl.AddObject(
            name: "o_b_magical_shield",
            parentName: "o_magical_buff",
            spriteName: "s_b_magical_shield",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        o_b_magical_shield.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                stack = 1
                duration = 6
                should_execute = false
                Damage_Got = 0
                Part_Normalizer = 1
                Damage_After = 0
                Max_Duration = 25
                Shield_Duration = Max_Duration
                scr_buff_atr()
                buff_snd = snd_abillity_shield_block
            "),

            new MslEvent(eventType: EventType.Destroy, subtype: 0, code: @"
                event_inherited()
                if instance_exists(owner)
                {
                    scr_audio_play_at(choose(snd_spell_stone_armor_explode_1, snd_spell_stone_armor_explode_2))
                }
            "),

            new MslEvent(eventType: EventType.Alarm, subtype: 2, code: @$"
                event_inherited()
                ds_map_clear(data)
                ds_map_add(data, ""MP_Restoration"", -25)
                if instance_exists(owner)
                {{
                    Max_Duration = {Shield_Duration}

                    if (is_crit)
                        Max_Duration *= max(1, owner.Miracle_Power / 100)

                    Shield_Duration = Max_Duration
                }}
            "),

            new MslEvent(eventType: EventType.Other, subtype: 14, code: @"
                event_inherited()
                if (should_execute)
                {
                    should_execute = false

                    with (target)
                        scr_guiAnimation_ext(x, y, s_magical_shield_proc)

                    var _durReduc = min(Damage_Got, (Shield_Duration * Part_Normalizer))
                    Damage_After = math_round(max(Damage_Got - Shield_Duration * Part_Normalizer, 0))
                    Shield_Duration -= _durReduc

                    if (Shield_Duration <= 0)
                        instance_destroy()
                }
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

        // Melee attack
        Msl.LoadGML("gml_GlobalScript_scr_attack")
            .MatchFrom("var DMG_r = 0")
            .InsertAbove(@"
                with (scr_instance_exists_in_list(o_b_magical_shield, _target.buffs))
                {
                    should_execute = true
                    P_proc = true
                }
            ")
            .Save();

        // Spell attack
        Msl.LoadGML("gml_GlobalScript_scr_skill_damage")
            .MatchFrom("    var dmg = 0")
            .InsertBelow(@"
    var _magic_shield = false
    with (scr_instance_exists_in_list(o_b_magical_shield, _target.buffs))
    {
        should_execute = true
        _magic_shield = true
    }")
            .MatchFrom("    if _need_log")
            .InsertAbove(@"
    if (_magic_shield && scr_actionsLogVisible(_target))
    {
        audio_play_sound(choose(snd_block_1, snd_block_2, snd_block_3, snd_block_4), 4, 0)
    }
")
            .Save();

        // Arrow attack
        Msl.LoadGML("gml_Object_o_arrow_Other_10")
            .MatchFrom("if P_proc")
            .InsertAbove(@"
            with (scr_instance_exists_in_list(o_b_magical_shield, _target.buffs))
            {
                should_execute = true
                P_proc = true
            }")
            .Save();

        // Throwed item attack
        Msl.LoadGML("gml_Object_o_throwed_loot_Other_10")
            .MatchFrom("if P_proc")
            .InsertAbove(@"
            with (scr_instance_exists_in_list(o_b_magical_shield, _target.buffs))
            {
                should_execute = true
                P_proc = true
            }")
            .Save();

        // Damage reduction by magical shield
        Msl.LoadGML("gml_GlobalScript_scr_damage_physical_calc")
            .MatchFrom("var _dmgReal = math_round(argument3 * (max(((argument1 - argument4 * _partDamageNormalizer - argument0.tmpDEF * (1 - Armor_Piercing / 100)) * (1 - argument2 / 100)), 0)))")
            .InsertAbove(@"
        with (scr_instance_exists_in_list(o_b_magical_shield, argument0.buffs))
        {
            Damage_Got = argument1
            Part_Normalizer = _partDamageNormalizer
            event_user(4)
            argument1 = Damage_After            
        }
            ")
            .MatchFrom("var _dmgReal = math_round(argument3 * (max(((argument1 - argument4 * _partDamageNormalizer - argument0.tmpDEF * 0.5 * argument6) * (1 - argument2 / 100)), 0)))")
            .InsertAbove(@"
        with (scr_instance_exists_in_list(o_b_magical_shield, argument0.buffs))
        {
            Damage_Got = argument1
            Part_Normalizer = _partDamageNormalizer
            event_user(4)
            argument1 = Damage_After            
        }
            ")
            .Save();

        UndertaleSprite animation = Msl.GetSprite("s_magical_shield_proc");
        animation.CollisionMasks.RemoveAt(0);
        animation.OriginX = 20;
        animation.OriginY = 15;
        animation.IsSpecialType = true;
        animation.SVersion = 3;
        animation.GMS2PlaybackSpeed = 0.5f;
        animation.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;
    
        AdjustBuffIcon("s_b_magical_shield");
    }

    private void AdjustBuffIcon(string name)
    {
        UndertaleSprite buff_ico = Msl.GetSprite(name);
        buff_ico.OriginX = 13;
        buff_ico.OriginY = 13;
        buff_ico.IsSpecialType = true;
        buff_ico.SVersion = 3;
        buff_ico.GMS2PlaybackSpeed = 1;
        buff_ico.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;
    }
}