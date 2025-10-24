using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddWitcherDecoction()
    {
        AdjustBuffIcon("s_b_witcher_decoction");

        UndertaleSprite ico = Msl.GetSprite("s_inv_decoction");
        ico.CollisionMasks.RemoveAt(0);
        ico.IsSpecialType = true;
        ico.SVersion = 3;
        ico.Width = 27;
        ico.Height = 54;
        ico.OriginX = 0;
        ico.OriginY = 0;
        ico.MarginLeft = 3;
        ico.MarginRight = 23;
        ico.MarginBottom = 47;
        ico.MarginTop = 5;
        ico.GMS2PlaybackSpeed = 1;
        ico.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;

        foreach (var tte in ico.Textures)
        {
            tte.Texture.TargetX = 3;
            tte.Texture.TargetY = 7;
            tte.Texture.TargetWidth = 21;
            tte.Texture.TargetHeight = 40;
            tte.Texture.BoundingWidth = 27;
            tte.Texture.BoundingHeight = 54;
        }

        ico = Msl.GetSprite("s_loot_decoction");
        ico.CollisionMasks.RemoveAt(0);
        ico.IsSpecialType = true;
        ico.SVersion = 3;
        ico.BBoxMode = 2;
        ico.Width = 10;
        ico.Height = 14;
        ico.OriginX = 0;
        ico.OriginY = 0;
        ico.MarginLeft = 1;
        ico.MarginRight = 7;
        ico.MarginBottom = 12;
        ico.MarginTop = 5;
        ico.GMS2PlaybackSpeed = 1;
        ico.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;

        UndertaleGameObject o_inv_witcher_decoction = Msl.AddObject(
            name: "o_inv_witcher_decoction",
            parentName: "o_inv_dishes_beverage",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        UndertaleGameObject o_loot_witcher_decoction = Msl.AddObject(
            name: "o_loot_witcher_decoction",
            parentName: "o_consument_loot",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        UndertaleGameObject o_b_decoction_buff = Msl.AddObject(
            name: $"o_b_decoction_buff",
            parentName: "o_buff",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        o_inv_witcher_decoction.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                max_charge = 1
                can_merge = false
                drop_gui_sound = snd_gui_drop_potion
                pickup_sound = snd_gui_pick_potion
                ds_map_set(data, ""quality"", (3 << 0))
                ds_map_set(data, ""Colour"", make_colour_rgb(76, 127, 255))
                dishes_object = o_inv_potion01_empty
                scr_consum_set_attribute(""Intoxication"", 40)
                scr_consum_set_attribute(""Toxicity_Change"", 0.1, true)
                scr_consum_set_attribute(""Toxicity_Resistance"", -10, true)
            "),

            new MslEvent(eventType: EventType.Other, subtype: 24, code: @"
                event_inherited()
                audio_play_sound(snd_gui_drink_potion, 3, 0)
                scr_random_speech(""useDrug"")

                with (o_player)
                    scr_guiAnimation(s_drinking, 1, 1, 0)

                if (!o_skill_trial_of_grasses.is_open)
                    with (o_player)
                        instance_destroy()
            "),

            new MslEvent(eventType: EventType.Other, subtype: 25, code: @"
                event_inherited()
                with (loot_object)
                {
                    image_index = other.i_index
                    i_index = other.i_index
                }
            ")
        );

        o_loot_witcher_decoction.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                charge = 1
                number = 0
            ")
        );

        o_b_decoction_buff.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                type = ds_list_find_value(global.buff_type_text, 4)
                type_col = make_colour_rgb(158, 27, 49)
                buff_type = o_b_decoction_buff

                buff_snd = noone
                snd_loop = noone
                stack = 1
                stage = 1
                max_stage = 6
                have_stages = true
                scr_buff_atr()
            "),

            new MslEvent(eventType: EventType.Destroy, subtype: 0, code: @"
                event_inherited()

                if (duration <= 0)
                {
                    scr_effect_create(o_db_weak, 600, target, target)
                }
            "),

            new MslEvent(eventType: EventType.Alarm, subtype: 2, code: @"
                event_inherited()
                event_user(5)
            ")
        );

        Msl.LoadGML("gml_Object_o_inv_antivenom_Other_24")
            .MatchAll()
            .InsertBelow(@"
                with (o_b_decoction_buff)
                {
                    if (is_player(target))
                        instance_destroy()
                }
            ")
            .Save();

        Msl.LoadAssemblyAsString("gml_Object_o_inv_antitoxin_Other_24")
            .MatchFromUntil("call.i gml_Script_scr_effect_create", "popz.v")
            .InsertBelow(@"pushi.e o_b_decoction_buff
conv.i.v
call.i @@This@@(argc=0)
pushloc.v local._duration_dec
callv.v 1
popz.v")
            .Save();

        string HP_Restoration = "5";
        string HP_Got = "20";
        string Weapon_Damage = "10";
        string Magic_Power = "10";
        AddWitcherDecoctionObject(
            id: "ghoul_decoction",
            name: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Ghoul Decoction"},
                {ModLanguage.Chinese, "食尸鬼煎药"}
            },
            midtext: new Dictionary<ModLanguage, string>()
            {
                {ModLanguage.English, "No Translation"},
                {ModLanguage.Chinese, string.Join("##",
                    $"允许在战斗中~lg~自动恢复~/~生命值，并且每一层煎药效果使生命自动恢复~lg~+{HP_Restoration}%~/~。",
                    "击杀目标会令煎药效果叠加~lg~1~/~层（最多叠到~w~六~/~层）。每过~r~90~/~回合，煎药效果消减~r~1~/~层。",
                    $"从第~w~二~/~层开始，如果生命目前少于~r~20%~/~，则会消耗煎药效果的~r~所有层数~/~以恢复生命。每消耗一层煎药效果恢复生命上限~lg~{HP_Got}%~/~的生命，并使煎药效果持续时间变为缩短~r~20~/~回合。",
                    $"从第~w~四~/~层开始，每一层煎药效果令兵器伤害~lg~+{Weapon_Damage}%~/~，法力~lg~+{Magic_Power}%~/~。"
                )}
            },
            description: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "WIP"},
                {ModLanguage.Chinese, "WIP"}
            },

            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                turn_count = 0
                max_hp_record = 0
            "),

            new MslEvent(eventType: EventType.Alarm, subtype: 2, code: @"
                max_hp_record = target.max_hp
                event_inherited()
            "),

            new MslEvent(eventType: EventType.Other, subtype: 15, code: @$"
                event_inherited()

                ds_map_clear(data)
                ds_map_add(data, ""Health_Restoration"", stage * {HP_Restoration})

                if (stage > 3)
                {{
                    ds_map_add(data, ""Weapon_Damage"", {Weapon_Damage} * (stage - 3))
                    ds_map_add(data, ""Magic_Power"", {Magic_Power} * (stage - 3))
                }}
            "),

            new MslEvent(eventType: EventType.Other, subtype: 10, code: @"
                event_inherited()

                with (target)
                {
                    if (in_battle)
                    {
                        if (HP < max_hp)
                        {
                            if (ds_list_empty(lock_regen))
                            {
                                Health_Restoration_Bar += Health_Restoration
                                
                                if (Health_Restoration_Bar >= 100)
                                {
                                    var dhp = ceil(0.05 * max_hp)
                                    scr_restore_hp(id, dhp)
                                    Health_Restoration_Bar -= 100
                                }
                            }
                        }
                    }
                }

                turn_count++

                if (turn_count == 90)
                {
                    turn_count = 0
                    stage--
                    event_user(5)
                }
            "),

            new MslEvent(eventType: EventType.Other, subtype: 11, code: @$"
                event_inherited()

                if (stage >= 2)
                {{
                    if (target.HP < (max_hp_record * 0.2))
                    {{
                        var _eff = stage - 1
                        stage -= _eff

                        with (target)
                        {{
                            scr_restore_hp(id, other.max_hp_record * ({HP_Got} * _eff) / 100, other.name)
                        }}

                        var _dur = math_round(20 * _eff)
                        duration -= _dur

                        if (duration <= 0)
                            instance_destroy()
                        
                        event_user(5)
                    }}
                }}
            "),

            new MslEvent(eventType: EventType.Other, subtype: 19, code: @"
                event_inherited()
                stage++
                event_user(5)
            ")
        );


        AddWitcherDecoctionObject(
            id: "crawler_decoction",
            name: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Crawler Decoction"},
                {ModLanguage.Chinese, "巨蛛煎药"}
            },
            midtext: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "No Translation"},
                {ModLanguage.Chinese, string.Join("##",
                    "每层煎药效果使~cg~腐蚀伤害+1~/~，精力吸取~lg~+5%~/~，准度~lg~+5%~/~。",
                    "发动攻击技能或咒法会令煎药效果叠加~lg~1~/~层（最多叠到六层）。击打或射击失手或法咒失误会令会使煎药效果消减~r~1~/~层，并使所有能力当前剩余冷却时间缩短~lg~1~/~个回合。",
                    "从~sy~第四层~/~开始，每层使限制移动几率~lg~+15%~/~。",
                    "当叠加至~sy~第六层~/~，煎药会使暴击几率和奇观几率~lg~+25%~/~，如果发生暴击或奇观，则消耗~r~3~/~层煎药效果，并使所有能力当前剩余冷却时间缩短~lg~3~/~个回合。"
                )}
            },
            description: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "WIP"},
                {ModLanguage.Chinese, "WIP"}
            },

            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
            "),

            new MslEvent(eventType: EventType.Other, subtype: 15, code: @$"
                event_inherited()

                ds_map_clear(data)
                ds_map_add(data, ""Caustic_Damage"", stage * 1)
                ds_map_add(data, ""Manasteal"", stage * 5)
                ds_map_add(data, ""Hit_Chance"", stage * 5)

                if (stage >3)
                {{
                    ds_map_add(data, ""Immob_Chance"", (stage - 3) * 15)
                }}

                if (stage == 6)
                {{
                    ds_map_add(data, ""CRT"", 25)
                    ds_map_add(data, ""Miracle_Chance"", 25)
                }}
            "),

            // 近战攻击
            new MslEvent(eventType: EventType.Other, subtype: 12, code: @"
                event_inherited()

                var _kd = 0
                if (attack_result == ""fumble"" || attack_result == ""fumbleBlock"")
                {
                    _kd = 1
                }
                else if (stage == 6 && attack_result == ""crit"")
                {
                    _kd = 3
                }

                if (_kd && stage > 1)
                {
                    stage -= _kd

                    with (target)
                    {
                        if (is_player())
                            scr_skill_category_change_KD(o_skill_category, _kd)
                        else
                            scr_skill_category_change_KD_enemy(id, -_kd)
                    }

                    event_user(5)
                }
            "),

            new MslEvent(eventType: EventType.Other, subtype: 14, code: @"
                var _category = scr_get_value_Dmap(other.skill, ""Category"", other.map_skills)
                var _is_attack_skill = ds_list_find_index(_category, ""Attack"") >= 0
                var _is_spell = ds_list_find_index(_category, ""Spell"") >= 0
                if (_category != 0 && (_is_attack_skill || _is_spell))
                {
                    stage++
                    event_user(5)
                }

                if (_category != 0 && _is_spell)
                {
                    if (other.is_fumble)
                    {
                        attack_result = ""fumble""
                        event_user(2)
                    }
                    else if (other.is_crit)
                    {
                        attack_result = ""crit""
                        event_user(2)
                    }
                }
            ")
        );

        AddWitcherDecoctionObject(
            id: "harpy_decoction",
            name: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Harpy Decoction"},
                {ModLanguage.Chinese, "哈比煎药"}
            },
            midtext: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "No Translation"},
                {ModLanguage.Chinese, string.Join("##",
                    "每层煎药效果使闪躲几率~lg~+5%~/~，暴击几率~lg~+5%~/~，冷却时间~lg~-5%~/~。",
                    "主手近身主动攻击命中目标（没有失手）会令煎药效果叠加~lg~1~/~层。击杀目标则叠加~lg~3~/~层（最多叠到六层）。受到近身攻击~r~伤害~/~或者每过~r~90~/~回合，煎药效果消减~r~1~/~层。",
                    "所有邻近敌人每回合有~lg~3%~/~的几率咳嗽，并且每一层煎药效果使此几率~lg~+3%~/~。",
                    "从~sy~第四层~/~开始，每一层煎药效果令所有~w~突进~/~技能距离~lg~+1~/~。"
                )}
            },
            description: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "WIP"},
                {ModLanguage.Chinese, "WIP"}
            },

            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                turn_count = 0

                with (o_skill_ico)
                {
                    if (!passive)
                    {
                        var _category = scr_get_value_Dmap(skill, ""Category"")
                        
                        if (_category != 0)
                        {
                            if (ds_list_find_index(_category, ""Charge"") >= 0)
                                Original_Special_Bonus_Range = Special_Bonus_Range
                        }
                    }
                }
            "),

            new MslEvent(eventType: EventType.Destroy, subtype: 0, code: @"
                event_inherited()
                scr_onUnitEffectDestroy(stink)

                with (o_skill_ico)
                {
                    if (!passive)
                    {
                        var _category = scr_get_value_Dmap(skill, ""Category"")
                        
                        if (_category != 0)
                        {
                            if (ds_list_find_index(_category, ""Charge"") >= 0)
                                Special_Bonus_Range = Original_Special_Bonus_Range
                        }
                    }
                }
            "),

            new MslEvent(eventType: EventType.Alarm, subtype: 2, code: @"
                event_inherited();

                with (target)
                    other.stink = scr_onUnitEffectCreate(s_undead_stink, s_undead_stink, s_empty, -1)
            "),

            new MslEvent(eventType: EventType.Other, subtype: 15, code: @"
                event_inherited()

                ds_map_clear(data)
                ds_map_add(data, ""EVS"", stage * 5)
                ds_map_add(data, ""CRT"", stage * 5)
                ds_map_add(data, ""Cooldown_Reduction"", stage * -5)

                if (stage > 3)
                {
                    with (o_skill_ico)
                    {
                        if (!passive)
                        {
                            var _category = scr_get_value_Dmap(skill, ""Category"")
                            
                            if (_category != 0)
                            {
                                if (ds_list_find_index(_category, ""Charge"") >= 0)
                                    Special_Bonus_Range = Original_Special_Bonus_Range + other.stage - 3
                            }
                        }
                    }
                }
            "),

            new MslEvent(eventType: EventType.Other, subtype: 10, code: @"
                event_inherited()

                var _chance = 3 * stage
                with (target)
                {
                    var _enemy_array = [];
                    if (is_player())
                        _enemy_array = scr_enemy_count_player(1, true)
                    else
                        _enemy_array = scr_enemy_count_around(1, false, false, true)

                    for (var i = 0; i < array_length(_enemy_array); i++)
                    {
                        with (_enemy_array[i])
                        {
                            if (!scr_instance_exists_in_list(o_b_drug_paregoric) && scr_chance_value(_chance))
                                scr_effect_create(o_db_cough, 1, id, id)
                        }
                    }
                }

                turn_count++

                if (turn_count == 90)
                {
                    turn_count = 0
                    stage--
                    event_user(5)
                }
            "),

            new MslEvent(eventType: EventType.Other, subtype: 12, code: @"
                event_inherited()

                if (!global.contrattack && !target.is_offhand_attack
                        && (!instance_exists(o_inv_left_hand)
                                || !instance_exists(o_inv_left_hand.children)
                                || !o_inv_left_hand.children.equipped)
                        && attack_result != ""miss"" && attack_result != ""fumble""
                        && attack_result != ""fumbleBlock"")
                {
                    stage++
                    event_user(5)
                }
            "),

            new MslEvent(eventType: EventType.Other, subtype: 13, code: @"
                event_inherited()

                if (damage > 0)
                {
                    stage--
                    event_user(5)
                }
            "),

            new MslEvent(eventType: EventType.Other, subtype: 19, code: @"
                event_inherited()
                stage += 3
                event_user(5)
            ")
        );

        AddWitcherDecoctionObject(
            id: "troll_decoction",
            name: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Troll Decoction"},
                {ModLanguage.Chinese, "巨魔煎药"}
            },
            midtext: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "No Translation"},
                {ModLanguage.Chinese, string.Join("##",
                    "每层煎药效果使生命上限~lg~+10~/~，所受伤害~lg~-5%~/~，位移抗性~lg~+5%~/~，击退几率~lg~+10%~/~，坚忍~lg~+10%~/~，饥饿抗性~r~-10%~/~。",
                    "运用~sy~机动~/~技能、~sy~站姿~/~技能、跳过一个回合或者切换武器可令煎药效果叠加~lg~1~/~层（最多叠到六层）。",
                    "从~sy~第四层~/~开始，如果获得~r~出血~/~、~r~硬直~/~、~r~眩晕~/~、~r~失衡~/~或~r~移动受限~/~，那么立刻移除所获状态，并使煎药效果消减~r~1~/~层。",
                    "吃的再多也不会~r~呕吐~/~，只会延长~lg~满足~/~效果的持续时间。"
                )}
            },
            description: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "WIP"},
                {ModLanguage.Chinese, "WIP"}
            },

            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                buff_id = noone
            "),

            new MslEvent(eventType: EventType.Other, subtype: 15, code: @"
                event_inherited()

                ds_map_clear(data)
                ds_map_add(data, ""max_hp"", stage * 10)
                ds_map_add(data, ""Damage_Received"", stage * -5)
                ds_map_add(data, ""Knockback_Resistance"", stage * 5)
                ds_map_add(data, ""Knockback_Chance"", stage * 10)
                ds_map_add(data, ""Fortitude"", stage * 10)
                ds_map_add(data, ""Hunger_Resistance"", stage * -10)
            "),

            new MslEvent(eventType: EventType.Other, subtype: 14, code: @"
                if (buff_id != noone && instance_exists(buff_id))
                {
                    if (stage > 3)
                    {
                        with (buff_id)
                            instance_destroy()
                        
                        buff_id = noone

                        // buff was decreased due to debuff removal
                        stage--
                        event_user(5)
                    }
                }
                else if (object_is_ancestor(other, o_skill))
                {
                    var _category = scr_get_value_Dmap(other.skill, ""Category"", other.map_skills)
                    var _is_maneuver = ds_list_find_index(_category, ""Maneuver"") >= 0
                    var _is_Stance = ds_list_find_index(_category, ""Stance"") >= 0
                    if (_category != 0 && (_is_maneuver || _is_Stance))
                    {
                        // buff was called by maneuver or stance skill
                        stage++
                        event_user(5)
                    }
                }
                else
                {
                    // buff was called skip turns or switch weapon
                    stage++
                    event_user(5)
                }
            ")

        );

        AddWitcherDecoctionObject(
            id: "gulon_decoction",
            name: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Gulon Decoction"},
                {ModLanguage.Chinese, "谷隆煎药"}
            },
            midtext: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "No Translation"},
                {ModLanguage.Chinese, string.Join("##",
                    "每层煎药效果令生命吸取~lg~+5%~/~，出血几率~lg~+10%~/~，肢体伤害~lg~+10%~/~，护甲穿透~lg~+10%~/~。",
                    "发动攻击技能会令煎药效果叠加~lg~1~/~层（最多叠到六层），并恢复该技能所耗精力~lg~30%~/~的生命值，每层煎药效果使恢复量~lg~+30%~/~。",
                    "从~sy~第二层~/~开始：如果本次击打造成~r~出血~/~，依据当前煎药效果层数，立刻令所有身体部位状态改善~lg~10%~/~，所有能力当前剩余冷却时间缩短~lg~1~/~个回合，并使煎药效果消减~r~1~/~层。"
                )}
            },
            description: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "WIP"},
                {ModLanguage.Chinese, "WIP"}
            },

            new MslEvent(eventType: EventType.Other, subtype: 15, code: @$"
                event_inherited()

                ds_map_clear(data)
                ds_map_add(data, ""Lifesteal"", stage * 5)
                ds_map_add(data, ""Bleeding_Chance"", stage * 10)
                ds_map_add(data, ""Bodypart_Damage"", stage * 10)
                ds_map_add(data, ""Armor_Piercing"", stage * 10)
            "),

            new MslEvent(eventType: EventType.Other, subtype: 14, code: @"
                var _category = scr_get_value_Dmap(other.skill, ""Category"", other.map_skills)
                var _is_attack_skill = ds_list_find_index(_category, ""Attack"") >= 0
                if (_category != 0 && _is_attack_skill)
                {
                    var _mp_res = MP_Increase * 0.3 * stage
                    with (target)
                        scr_restore_hp(id, _mp_res, other.name)

                    stage++
                    event_user(5)
                }
            "),

            new MslEvent(eventType: EventType.Other, subtype: 12, code: @"
                event_inherited()

                if (stage > 1 && is_bleeding)
                {
                    with (target)
                    {
                        if (is_player())
                            scr_skill_category_change_KD(o_skill_category, other.stage)
                        else
                            scr_skill_category_change_KD_enemy(id, -other.stage)
                    }

                    with (o_player)
                    {
                        var key = ds_map_find_first(Body_Parts_map)
                        var _size = ds_map_size(Body_Parts_map)

                        repeat (_size)
                        {
                            var _condition = ds_map_find_value(Body_Parts_map, key)
                            ds_map_set(Body_Parts_map, key, min(100, _condition + 10 * other.stage))
                            key = ds_map_find_next(Body_Parts_map, key)
                        }
                    }

                    stage--
                    event_user(5)
                }
            ")
        );

        AddHooksForDecoctionBuff();

        Msl.InjectTableItemsLocalization(decoction_texts.ToArray());
        Msl.InjectTableModifiersLocalization(decoction_buff_texts.ToArray());
    }

    private void AddHooksForDecoctionBuff()
    {
        // NPC 的昏迷将会触发 event_user(9)
        Msl.LoadGML("gml_Object_o_NPC_Other_16")
            .MatchFrom("is_life = false")
            .InsertBelow(@"
                with (o_b_decoction_buff)
                {
                    if (other.last_attacker == target.id)
                    {
                        attacker_target = other.id
                        event_user(9)
                        attacker_target = -4
                    }
                }
            ")
            .Save();

        // 使用能力（包括技能和咒法）将会触发 event_user(4)
        Msl.LoadAssemblyAsString("gml_Object_o_skill_Other_13")
            .MatchFrom("bf [end]")
            .ReplaceBy("bf [1093]")
            .MatchFrom("bf [end]")
            .ReplaceBy("bf [1093]")
            .MatchFrom(":[end]")
            .InsertAbove(@"
:[1093]
pushi.e o_b_decoction_buff
pushenv [1096]

:[1094]
push.v other.owner
push.v self.target
cmp.v.v EQ
bf [1096]

:[1095]
pushloc.v local._mp_increase
pop.v.v self.MP_Increase
pushi.e 4
conv.i.v
call.i event_user(argc=1)
popz.v

:[1096]
popenv [1094]
")
            .Save();

        // 巨魔煎药的叠层
        Msl.LoadAssemblyAsString("gml_GlobalScript_scr_skip_turn")
            .MatchFromUntil("call.i gml_Script_scr_unitTurnGetTime", "bf [")
            .InsertBelow(@"
:[1000]
push.v self.id
pushi.e o_b_troll_decoction
conv.i.v
call.i gml_Script_scr_skill_call_buff(argc=2)
popz.v
")
            .Save();

        Msl.LoadAssemblyAsString("gml_Object_o_inv_switch_Other_10")
            .MatchFromUntil("call.i gml_Script_scr_skill_call_passive", "popz.v")
            .InsertBelow(@"
pushi.e o_player
conv.i.v
pushi.e o_b_troll_decoction
conv.i.v
call.i gml_Script_scr_skill_call_buff(argc=2)
popz.v
")
            .Save();

        // 巨魔煎药的效果触发
        foreach (string buff in new string[] { "o_db_daze", "o_db_immob", "o_db_stagger", "o_db_stun" })
        {
            Msl.LoadGML($"gml_Object_{buff}_Alarm_1")
                .MatchAll()
                .InsertBelow(@"
                    if (!is_load && instance_exists(target))
                    {
                        with (o_b_troll_decoction)
                        {
                            if (target.id == other.target.id)
                            {
                                buff_id = other.id
                                event_user(4)
                            }
                        }
                    }
                ")
                .Save();
        }

        Msl.AddNewEvent("o_db_bleed_parent", eventType: EventType.Alarm, subtype: 1, eventCode: @"
            if (!is_load && instance_exists(target))
            {
                with (o_b_troll_decoction)
                {
                    if (target.id == other.target.id)
                    {
                        buff_id = other.id
                        event_user(4)
                    }
                }
            }
        ");

        Msl.LoadGML("gml_GlobalScript_scr_hunger_incr")
            .MatchFrom("scr_effect_create(o_db_nause, o_damage_dealer)")
            .ReplaceBy(@"
        {
            if (!scr_instance_exists_in_list(o_b_troll_decoction, o_player.buffs))
                scr_effect_create(o_db_nause, o_damage_dealer)
            else
            {
                with (o_db_hunger0)
                {
                    duration += max(1, (dur * 4))
                    stage = max(stage, argument1)
                }
            }
        }")
            .Save();
    }

    private int decoction_idx = 0;
    private List<LocalizationItem> decoction_texts = new List<LocalizationItem>();
    private List<LocalizationModifier> decoction_buff_texts = new List<LocalizationModifier>();
    private void AddWitcherDecoctionObject(
        string id, Dictionary<ModLanguage, string> name,
        Dictionary<ModLanguage, string> midtext, Dictionary<ModLanguage, string> description,
        params MslEvent[] buffEvents)
    {
        UndertaleGameObject inv = Msl.AddObject(
            name: $"o_inv_{id}",
            parentName: "o_inv_witcher_decoction",
            spriteName: "s_inv_decoction",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        UndertaleGameObject loot = Msl.AddObject(
            name: $"o_loot_{id}",
            parentName: "o_loot_witcher_decoction",
            spriteName: "s_loot_decoction",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.InjectTableItemStats(
            id: id,
            Price: 500,
            Cat: Msl.ItemStatsCategory.beverage,
            Subcat: Msl.ItemStatsSubcategory.potion,
            Material: Msl.ItemStatsMaterial.glass,
            Weight: Msl.ItemStatsWeight.Light,
            Duration: 120,
            tags: Msl.ItemStatsTags.special,
            bottle: true
        );

        UndertaleGameObject buff = Msl.AddObject(
            name: $"o_b_{id}",
            parentName: "o_b_decoction_buff",
            spriteName: "s_b_witcher_decoction",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        inv.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @$"
                event_inherited()
                scr_consum_atr(""{id}"")
                i_index = {decoction_idx}
            "),

            new MslEvent(eventType: EventType.Other, subtype: 24, code: @$"
                event_inherited()
                scr_effect_create(o_b_{id}, 1200)
            ")
        );

        loot.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @$"
                event_inherited()
                inv_object = o_inv_{id}
            ")
        );

        buff.ApplyEvent(buffEvents);

        decoction_buff_texts.Add(
            new LocalizationModifier(
                id: $"o_b_{id}",
                name: name,
                description: midtext
            )
        );

        midtext[ModLanguage.English] += "##Without having undergone ~o~The Trial of Grasses~/~, drinking this potion results in instant ~r~Death~/~.";
        midtext[ModLanguage.Chinese] += "##如果没有通过~o~青草试炼~/~，那么喝下此药会立即~r~死亡~/~。";

        decoction_texts.Add(
            new LocalizationItem(
                id: id,
                name: name,
                effect: midtext,
                description: description
            )
        );

        decoction_idx++;
    }
}