using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddWitcherItems()
    {
        AddMedallionWolf();
    }

    private void AddMedallionWolf()
    {
        UndertaleSprite ico = Msl.GetSprite("s_inv_wolfschoolmedallion");
        ico.CollisionMasks.RemoveAt(0);
        ico.IsSpecialType = true;
        ico.SVersion = 3;
        ico.Width = 27;
        ico.Height = 54;
        ico.OriginX = 0;
        ico.OriginY = 0;
        ico.MarginLeft = 2;
        ico.MarginRight = 24;
        ico.MarginBottom = 49;
        ico.MarginTop = 2;
        ico.GMS2PlaybackSpeed = 1;
        ico.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;

        foreach (var tte in ico.Textures)
        {
            tte.Texture.TargetX = 2;
            tte.Texture.TargetY = 3;
            tte.Texture.TargetWidth = 23;
            tte.Texture.TargetHeight = 48;
            tte.Texture.BoundingWidth = 27;
            tte.Texture.BoundingHeight = 54;
        }

        ico = Msl.GetSprite("s_loot_wolfschoolmedallion");
        ico.CollisionMasks.RemoveAt(0);
        ico.IsSpecialType = true;
        ico.SVersion = 3;
        ico.Width = 23;
        ico.Height = 18;
        ico.OriginX = 0;
        ico.OriginY = 0;
        ico.MarginLeft = 2;
        ico.MarginRight = 20;
        ico.MarginBottom = 15;
        ico.MarginTop = 2;
        ico.GMS2PlaybackSpeed = 1;
        ico.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;

        foreach (var tte in ico.Textures)
        {
            tte.Texture.TargetX = 0;
            tte.Texture.TargetY = 0;
            tte.Texture.TargetWidth = 23;
            tte.Texture.TargetHeight = 18;
            tte.Texture.BoundingWidth = 23;
            tte.Texture.BoundingHeight = 18;
        }

        // UndertaleGameObject o_inv_witcher_medallion_wolf = Msl.AddObject(
        //     name: "o_inv_witcher_medallion_wolf",
        //     parentName: "o_inv_consum_passive",
        //     spriteName: "s_inv_witcher_medallion_wolf",
        //     isVisible: true,
        //     isPersistent: true,
        //     isAwake: true
        // );

        // UndertaleGameObject o_loot_witcher_medallion_wolf = Msl.AddObject(
        //     name: "o_loot_witcher_medallion_wolf",
        //     parentName: "o_consument_loot",
        //     spriteName: "s_loot_witcher_medallion_wolf",
        //     isVisible: true,
        //     isPersistent: false,
        //     isAwake: true
        // );

        // o_inv_witcher_medallion_wolf.ApplyEvent(
        //     new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
        //         event_inherited()
        //         scr_consum_atr(""witcher_medallion_wolf"")

        //         ds_map_set(data, ""quality"", (7 << 0))
        //         ds_map_set(data, ""Colour"", make_colour_rgb(229, 193, 85))
        //         if object_is_ancestor(object_index, o_inv_slot_parent)
        //             alarm[11] = shineDelay
                
        //         slot = ""Amulet""
        //         can_equip = true
        //         is_execute = false
        //     ")
        // );

        // o_loot_witcher_medallion_wolf.ApplyEvent(
        //     new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
        //         event_inherited()
        //         inv_object = o_inv_witcher_medallion_wolf
        //         number = 0
        //     ")
        // );

        Msl.InjectTableArmor(
            hook: Msl.ArmorHook.NECKLACES,
            name: "Wolf School Medallion",
            Tier: Msl.ArmorTier.Tier5,
            id: "witcher_medallion_wolf",
            Slot: Msl.ArmorSlot.Amulet,
            Class: Msl.ArmorClass.Light,
            rarity: Msl.ArmorRarity.Unique,
            Mat: Msl.ArmorMaterial.silver,
            Price: 200,
            MaxDuration: 80,
            Nature_Resistance: 9,
            Magic_Resistance: 9,
            Received_XP: 6,
            VSN: 1,
            tags: Msl.ArmorTags.specialexc
        );

        Msl.InjectTableWeaponTextsLocalization(
            new LocalizationWeaponText(
                id: "Wolf School Medallion",
                name: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Wolf School Medallion"},
                    {ModLanguage.Chinese, "狼学派徽章"}
                },
                description: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "A witcher medallion is a silver symbol of the witchers' profession. " +
                        "Each one is shaped to represent the school a witcher comes from. " +
                        "The medallions vibrate in response to magic in all its forms." },
                    {ModLanguage.Chinese, "猎魔人徽章是一个银制的护符，做成不同的形状来代表猎魔人们所属的不同学派。" +
                        "猎魔人徽章对所有魔法形式敏感，当侦测到魔法存在时徽章就会震动并且猛拉挂着它的链子。" }
                }
            )
        );
    }
}