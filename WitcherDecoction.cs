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
            parentName: "o_physical_buff",
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
                buff_snd = noone
                snd_loop = noone
                stack = 1
                scr_buff_atr()
            "),

            new MslEvent(eventType: EventType.Destroy, subtype: 0, code: @"
                event_inherited()

                if (duration <= 0)
                {
                    // scr_effect_create(o_db_weak, 600, target, target)
                }
            ")
        );

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
                    "击杀敌人会令煎药效果叠加~lg~1~/~层（最多叠到~w~六~/~层）。每过~r~90~/~回合，煎药效果消减~r~1~/~层。",
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
                stack = 1
                stage = 1
                max_stage = 6
                have_stages = true
                turn_count = 0
                max_hp_record = 0
            "),

            new MslEvent(eventType: EventType.Alarm, subtype: 2, code: @"
                event_inherited()
                max_hp_record = target.max_hp
                event_user(5)
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

        Msl.LoadGML("gml_Object_o_NPC_Other_16")
            .MatchFrom("is_life = false")
            .InsertBelow(@"
                with (o_b_decoction_buff)
                {
                    if (is_player(target))
                    {
                        attacker_target = other.id
                        event_user(9)
                        attacker_target = -4
                    }
                }
            ")
            .Save();

        Msl.InjectTableItemsLocalization(decoction_texts.ToArray());
        Msl.InjectTableModifiersLocalization(decoction_buff_texts.ToArray());
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