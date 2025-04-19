using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddWeaponOil()
    {
        UndertaleSprite ico = Msl.GetSprite("s_inv_weapon_oil");
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
            tte.Texture.TargetY = 9;
            tte.Texture.TargetWidth = 20;
            tte.Texture.TargetHeight = 37;
            tte.Texture.BoundingWidth = 27;
            tte.Texture.BoundingHeight = 54;
        }

        ico = Msl.GetSprite("s_loot_weapon_oil");
        ico.CollisionMasks.RemoveAt(0);
        ico.IsSpecialType = true;
        ico.SVersion = 3;
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

        UndertaleGameObject o_inv_weapon_oil_parent = Msl.AddObject(
            name: "o_inv_weapon_oil_parent",
            parentName: "o_inv_consum_active",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        o_inv_weapon_oil_parent.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                drop_gui_sound = snd_beverage_drop
                pickup_sound = snd_beverage_pick
                charge = 3
                sec_charge = charge
                max_charge = charge
                bar_color = make_color_rgb(88, 175, 19)
                can_merge = true
                skill = o_skill_weapon_coating
            "),
            new MslEvent(eventType: EventType.Other, subtype: 10, code: "event_inherited()"),
            new MslEvent(eventType: EventType.Other, subtype: 24, code: @"
                event_inherited()
                var _name = ds_map_find_value(data, ""idName"")
                with (skill)
                    coating_oil = _name
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

        UndertaleGameObject o_loot_weapon_oil_parent = Msl.AddObject(
            name: "o_loot_weapon_oil_parent",
            parentName: "o_consument_loot",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        o_loot_weapon_oil_parent.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                image_speed = 0
                number = 0
            ")
        );

        AddWeaponOilTexts();
        AddWeaponOilObject("hanged_man_venom");
        AddWeaponOilObject("vampire_oil");
    }

    private int oil_idx = 0;
    private void AddWeaponOilObject(string id)
    {
        UndertaleGameObject obj = Msl.AddObject(
            name: $"o_inv_{id}",
            parentName: "o_inv_weapon_oil_parent",
            spriteName: "s_inv_weapon_oil",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        obj.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @$"
                event_inherited()
                scr_consum_atr(""{id}"")
                i_index = {oil_idx}
            ")
        );

        UndertaleGameObject loot = Msl.AddObject(
            name: $"o_loot_{id}",
            parentName: "o_loot_weapon_oil_parent",
            spriteName: "s_loot_weapon_oil",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        loot.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @$"
                event_inherited()
                inv_object = o_inv_{id}
            ")
        );

        Msl.InjectTableItemStats(
            id: id,
            Material: Msl.ItemStatsMaterial.glass,
            Weight: Msl.ItemStatsWeight.Medium,
            Price: 200,
            Duration: 60,
            Stacks: 3,
            Cat: Msl.ItemStatsCategory.tool,
            bottle: true,
            tags: Msl.ItemStatsTags.special
        );

        oil_idx++;
    }

    private void AddWeaponOilTexts()
    {
        Msl.InjectTableItemsLocalization(
            new LocalizationItem(
                id: "hanged_man_venom",
                name: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Hanged man's venom"},
                    {ModLanguage.Chinese, "吊死鬼之毒"}
                },
                effect: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Applied to a weapon, it increases the damage dealt to humanoids by ~lg~20%~/~."},
                    {ModLanguage.Chinese, "应用于武器，对类人生物造成的伤害增加~lg~20%~/~。"}
                },
                description: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Hanged Man's Venom is a toxin that is equally lethal to humans, elves and dwarves. Applied to a blade, it deals more damage than any other coating. This oil is ineffective against monsters."},
                    {ModLanguage.Chinese, "吊死鬼之毒对于人类、精灵与矮人而言是致命的毒素。只要涂抹在剑刃上，它会比其他涂油造成更多的伤害。这种涂油对于怪物没多大效用。"}
                }
            ),

            new LocalizationItem(
                id: "vampire_oil",
                name: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Vampire Oil"},
                    {ModLanguage.Chinese, "吸血鬼油"}
                },
                effect: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Applied to a weapon, it increases the damage dealt to vampires by ~lg~20%~/~."},
                    {ModLanguage.Chinese, "应用于武器，对变节信徒造成的伤害增加~lg~20%~/~。"}
                },
                description: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Whosoever seeks to destroy a vampire, to banish it from this world forever, should prepare St. Gregory's Oil, called Vampire Oil by witchers. No fleder or bruxa can withstand it."},
                    {ModLanguage.Chinese, "无论谁想消灭吸血鬼，将其永远驱逐出这个世界，都应该准备圣格雷戈里油，猎魔人称之为吸血鬼油。任何蝙蝠或巫婆都无法抵御它。"}
                }
            )
        );
    }
}