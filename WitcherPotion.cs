using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddWitcherPotion()
    {
        UndertaleSprite ico = Msl.GetSprite("s_inv_witcher_potion");
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
            tte.Texture.TargetX = 4;
            tte.Texture.TargetY = 5;
            tte.Texture.TargetWidth = 18;
            tte.Texture.TargetHeight = 43;
            tte.Texture.BoundingWidth = 27;
            tte.Texture.BoundingHeight = 54;
        }

        ico = Msl.GetSprite("s_loot_witcher_potion");
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

        // Make these attributes show duration text when hovered
        Msl.LoadGML("gml_Object_o_textLoader_Other_25")
            .MatchFrom("ds_list_add(global.attribute_duration, ")
            .InsertBelow(@"ds_list_add(global.attribute_duration, ""EVS"", ""PRR"", ""Shock_Damage"")")
            .Save();

        UndertaleGameObject o_inv_witcher_potion = Msl.AddObject(
            name: "o_inv_witcher_potion",
            parentName: "o_inv_dishes_beverage",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        UndertaleGameObject o_loot_witcher_potion = Msl.AddObject(
            name: "o_loot_witcher_potion",
            parentName: "o_consument_loot",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        o_inv_witcher_potion.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                charge = 2
                drop_gui_sound = snd_beverage_drop
                pickup_sound = snd_beverage_pick
                max_charge = charge
                sec_charge = charge
                draw_charges = false
                bar_color = make_color_rgb(88, 175, 19)
                ds_map_set(data, ""quality"", (3 << 0))
                ds_map_set(data, ""Colour"", make_colour_rgb(76, 127, 255))
                dishes_object = o_inv_potion02_empty
            "),

            new MslEvent(eventType: EventType.Other, subtype: 24, code: @"
                event_inherited()
                audio_play_sound(snd_gui_drink_potion, 3, 0)

                with (o_player)
                    scr_guiAnimation(s_drinking, 1, 1, 0)

                if (false)
                    scr_atr_incr(""Intoxication"", 100)
            "),

            new MslEvent(eventType: EventType.Other, subtype: 25, code: @"
                event_inherited()
                with (loot_object)
                {
                    image_index = other.i_index
                    i_index = other.i_index
                }
            "),

            new MslEvent(eventType: EventType.Draw, subtype: 0, code: "scr_draw_consum_scale()")
        );

        o_loot_witcher_potion.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                charge = 2
                number = 0
            ")
        );

        AddWitcherPotionObject(
            id: "thunderbolt_potion",
            effects: new Dictionary<string, int>()
            {
                {"Intoxication",     15},
                {"Weapon_Damage",    50},
                {"CRT",              30},
                {"EVS",             -10},
                {"PRR",             -10}
            },
            name: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Thunderbolt"},
                {ModLanguage.Chinese, "雷霆"}
            },
            midtext: new Dictionary<ModLanguage, string>()
            {
                {ModLanguage.English, "Without having undergone ~o~The Trial of Grasses~/~, drinking this potion results in instant ~r~Deadly Intoxication~/~."},
                {ModLanguage.Chinese, "如果没有通过~o~青草试炼~/~，那么喝下此药会立即~r~迷醉濒死~/~。"}
            },
            description: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Witchers take this potion before fighting strong, heavily armored opponents. Imbibing Thunderbolt causes witchers to enter into a battle trance. While in this state, witchers attack more efficiently and cause greater damage, while at the same time neglecting their own defense and becoming an easier target."},
                {ModLanguage.Chinese, "猎魔人在对抗强大且身披重甲的敌人前会服用这种药剂。服下雷霆药剂后，猎魔人会进入战斗狂热状态。在这种状态下，他们攻击更为高效、伤害更高，但同时会忽视自身防御，变得更容易受到攻击。"}
            }
        );

        AddWitcherPotionObject(
            id: "swallow_potion",
            effects: new Dictionary<string, int>()
            {
                {"Intoxication",       10},
                {"Pain",              -25},
                {"max_hp_res",         50},
                {"Condition",          25},
                {"Healing_Received",   25},
                {"Health_Restoration", 15}
            },
            code: @"
                var _changeValue = ds_map_find_value_ext(attributes_data, ""Condition"", 0)
                with (o_player)
                {
                    var key = ds_map_find_first(Body_Parts_map)
                    var _size = ds_map_size(Body_Parts_map)

                    repeat (_size)
                    {
                        var _condition = ds_map_find_value(Body_Parts_map, key)
                        ds_map_set(Body_Parts_map, key, min(100, _condition + _changeValue))
                        key = ds_map_find_next(Body_Parts_map, key)
                    }
                }

                with (o_physical_debuff)
                {
                    if (is_player(target))
                        instance_destroy()
                }

                with (o_wound_debuff)
                {
                    if (is_player(target))
                        instance_destroy()
                }

                with (o_db_bleed_parent)
                {
                    if (is_player(target))
                        instance_destroy()
                }

                event_inherited()
            ",
            name: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Swallow"},
                {ModLanguage.Chinese, "燕子"}
            },
            midtext: new Dictionary<ModLanguage, string>()
            {
                {ModLanguage.English, "~lg~Removes~/~ all ~r~negative~/~ physical effects. Without having undergone ~o~The Trial of Grasses~/~, drinking this potion results in instant ~r~Deadly Intoxication~/~."},
                {ModLanguage.Chinese, "可以~lg~移除~/~所有~r~负面~/~物理效果。##如果没有通过~o~青草试炼~/~，那么喝下此药会立即~r~迷醉濒死~/~。"}
            },
            description: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "There is no bird more beautiful than the swallow, the harbinger of spring. Even the dark mages who developed the formula for witchers' potions appreciated the charm of this bird, lending its name to the potion that accelerates regeneration of a mutated organism."},
                {ModLanguage.Chinese, "没有比燕子更美的鸟了，它是春天的使者。即便是研发猎魔人魔药配方的黑巫师，也赞赏这种鸟的魅力，并以它为名，为能加速突变体恢复的药剂命名。"}
            }
        );

        Msl.InjectTableItemsLocalization(potion_texts.ToArray());
    }

    private int potion_idx = 0;
    private List<LocalizationItem> potion_texts = new List<LocalizationItem>();
    private void AddWitcherPotionObject(
        string id, Dictionary<string, int> effects, Dictionary<ModLanguage, string> name,
        Dictionary<ModLanguage, string> midtext, Dictionary<ModLanguage, string> description,
        string code = "event_inherited()")
    {
        UndertaleGameObject inv = Msl.AddObject(
            name: $"o_inv_{id}",
            parentName: "o_inv_witcher_potion",
            spriteName: "s_inv_witcher_potion",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        UndertaleGameObject loot = Msl.AddObject(
            name: $"o_loot_{id}",
            parentName: "o_loot_witcher_potion",
            spriteName: "s_loot_weapon_oil",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.InjectTableItemStats(
            id: id,
            Price: 200,
            Cat: Msl.ItemStatsCategory.beverage,
            Subcat: Msl.ItemStatsSubcategory.potion,
            Material: Msl.ItemStatsMaterial.glass,
            Weight: Msl.ItemStatsWeight.Light,
            Duration: 20,
            tags: Msl.ItemStatsTags.special,
            bottle: true
        );

        string effects_string = string.Join(
            "\n", effects.Select(kv => $"scr_consum_set_attribute(\"{kv.Key}\", {kv.Value})")
        );

        inv.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @$"
                event_inherited()
                scr_consum_atr(""{id}"")
                i_index = {potion_idx}
                {effects_string}
            "),

            new MslEvent(eventType: EventType.Other, subtype: 24, code: code)
        );

        loot.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @$"
                event_inherited()
                inv_object = o_inv_{id}
            ")
        );

        potion_texts.Add(
            new LocalizationItem(
                id: id,
                name: name,
                effect: midtext,
                description: description
            )
        );

        potion_idx++;
    }
}