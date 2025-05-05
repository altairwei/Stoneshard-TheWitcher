using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddSkill_Axii_Sign()
    {
        AdjustSkillIcon("s_skills_axii_sign");

        Msl.InjectTableSkillsLocalization(
            new LocalizationSkill(
                id: "Axii_Sign",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Axii Sign"},
                    {ModLanguage.Chinese, "亚克西法印"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, @""},
                    {ModLanguage.Chinese, @"有~lg~/*Charm_Chance*/%~/~的概率催眠敌人（灵术抗性能抵御催眠），使其变为友方单位~w~/*Charm_Time*/~/~回合。如果目标~y~没有察觉~/~，那么必然催眠成功。之后敌人陷入~w~12~/~回合的~r~“慌乱”~/~。"}
                }
            )
        );

        Msl.InjectTableSkillsStats(
            id: "Axii_Sign",
            Object: "o_axii_charm_birth",
            hook: Msl.SkillsStatsHook.MAGICMASTERY,
            Target: Msl.SkillsStatsTarget.TargetObject,
            Range: "5",
            KD: 26,
            MP: 48,
            Duration: 12,
            Class: Msl.SkillsStatsClass.spell,
            Branch: "witcher",
            Spell: true,
            AP: "x",
            Bonus_Range: true
        );

        UndertaleGameObject o_skill_axii_sign = Msl.AddObject(
            name: "o_skill_axii_sign",
            parentName: "o_skill",
            spriteName: "s_skills_axii_sign",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        string Charm_Chance = "5 * owner.WIL";
        string Charm_Time = "round(4 * owner.Magic_Power / 100)";

        o_skill_axii_sign.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                skill = ""Axii_Sign""
                scr_skill_atr(""Axii_Sign"")
                can_learn = true
                ds_list_add(attribute,
                    ds_map_find_value(global.attribute, ""Magic_Power""),
                    ds_map_find_value(global.attribute, ""WIL""),
                    ds_map_find_value(global.attribute, ""Bonus_Range""))
                ignore_interact = true
                is_moving = false
                click_snd = snd_skill_sealofshackles_startcast
            "),

            // Format the description text
            new MslEvent(eventType: EventType.Other, subtype: 17, code: @$"
                if instance_exists(owner)
                {{
                    ds_map_replace(data, ""Charm_Chance"", {Charm_Chance})
                    ds_map_replace(data, ""Charm_Time"", {Charm_Time})
                }}
                event_inherited()
            ")
        );

        UndertaleGameObject o_skill_axii_sign_ico = Msl.AddObject(
            name: "o_skill_axii_sign_ico",
            parentName: "o_skill_ico",
            spriteName: "s_skills_axii_sign",
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_axii_sign_ico.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                child_skill = o_skill_axii_sign
                event_perform_object(child_skill, ev_create, 0)
            ")
        );

        Msl.InjectTableModifiersLocalization(
            new LocalizationModifier(
                id: "o_db_axii_charm",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Charm"},
                    {ModLanguage.Chinese, "魅惑"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "charmed by Axii Sign."},
                    {ModLanguage.Chinese, "被亚克西法印所魅惑。"}
                }
            )
        );

        UndertaleGameObject o_axii_charm_birth = Msl.AddObject(
            name: "o_axii_charm_birth",
            parentName: "o_target_spell",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        UndertaleGameObject o_db_axii_charm = Msl.AddObject(
            name: "o_db_axii_charm",
            parentName: "o_magical_buff",
            spriteName: "s_b_receptivity",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        o_axii_charm_birth.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
            "),

            // Control the chance of charm
            new MslEvent(eventType: EventType.Alarm, subtype: 0, code: @$"
                if (target.ai_is_on)
                {{
                    var _charm_chance = {Charm_Chance} - target.Psionic_Resistance
                    if (target.state == ""idle"" || target.state == ""search"" || target.state == ""alarm"")
                        _charm_chance = 100

                    scr_actionsLogUpdate(""Psionic_Resistance:"" + string(target.Psionic_Resistance))
                    scr_actionsLogUpdate(""Charm_Chance:"" + string(_charm_chance))
                    if (scr_chance_value(_charm_chance))
                        scr_effect_create(o_db_axii_charm, {Charm_Time}, target, owner)
                    else
                        scr_effect_create(o_db_confuse, 12, target, target)
                }}

                instance_destroy()
            ")
        );

        Msl.AddFunction(ModFiles.GetCode("scr_charm_npc_to_ally.gml"), "scr_charm_npc_to_ally");
        Msl.AddFunction(ModFiles.GetCode("scr_charm_enemy_to_ally.gml"), "scr_charm_enemy_to_ally");

        o_db_axii_charm.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                scr_buff_atr()
                buff_snd = snd_skill_sigil_of_binding_startcast
                stack = 0
                stage = 1
                signTarget = noone
                sign_loop = noone
                target_fraction = """"
                target_subfraction = """"
            "),

            new MslEvent(eventType: EventType.Destroy, subtype: 0, code: @"
                event_inherited()
                audio_stop_sound(sign_loop)
                scr_onUnitAnimationDestroy(signTarget)

                with (target)
                {
                    faction_key = other.target_fraction
                    subfaction_key = other.target_subfraction
                    scr_delete_from_enemy_list(other.target_subfraction)
                    target = noone
                    if (state != ""work"" && state != ""KO"" && state != ""transition"")
                        state = ""move_to_work""
                    scr_add_to_enemy_list(""Player"")
                    state = ""alarm""
                    scr_enemy_choose_state_new()
                    scr_random_speech(hostile_voice_tag, noone, id)
                }

                scr_effect_create(o_db_confuse, 12, target, target)
            "),

            // Charm the enemy
            new MslEvent(eventType: EventType.Alarm, subtype: 2, code: @"
                event_inherited()
                var _id = id
                with (target)
                {
                    other.signTarget = scr_onUnitAnimationCreate(
                        s_axiicharm_start, s_axiicharm_loop, s_axiicharm_end, -1, true)
                    with (other.signTarget)
                        _id.sigil_loop = scr_audio_play_at_loop(snd_skill_sigil_of_binding_loop)

                    scr_charm_enemy_to_ally()
                }
            "),

            // Attack the charmed enemy will destroy this buff
            new MslEvent(eventType: EventType.Other, subtype: 13, code: @"
                if (attacker.id == owner.id)
                {
                    scr_actionsLogUpdate(""YourAttackWakeItUp"")
                    instance_destroy()
                }
            ")
        );

        // Adjust animation
        UndertaleSprite img = Msl.GetSprite("s_axiicharm_start");
        img.CollisionMasks.RemoveAt(0);
        img.OriginX = 25;
        img.OriginY = 56;
        img.IsSpecialType = true;
        img.SVersion = 3;
        img.GMS2PlaybackSpeed = 0.3f;
        img.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;

        img = Msl.GetSprite("s_axiicharm_loop");
        img.CollisionMasks.RemoveAt(0);
        img.OriginX = 25;
        img.OriginY = 56;
        img.MarginLeft = 5;
        img.MarginRight = 42;
        img.MarginBottom = 49;
        img.MarginTop = 26;
        img.IsSpecialType = true;
        img.SVersion = 3;
        img.GMS2PlaybackSpeed = 0.3f;
        img.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;

        img = Msl.GetSprite("s_axiicharm_end");
        img.CollisionMasks.RemoveAt(0);
        img.OriginX = 25;
        img.OriginY = 56;
        img.MarginLeft = 5;
        img.MarginRight = 42;
        img.MarginBottom = 51;
        img.MarginTop = 27;
        img.IsSpecialType = true;
        img.SVersion = 3;
        img.GMS2PlaybackSpeed = 0.3f;
        img.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;
    }
}