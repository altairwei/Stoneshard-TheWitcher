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
        UndertaleSprite ico = Msl.GetSprite("s_inv_witcher_medallion_wolf");
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

        ico = Msl.GetSprite("s_loot_witcher_medallion_wolf");
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

        UndertaleGameObject o_inv_witcher_medallion_wolf = Msl.AddObject(
            name: "o_inv_witcher_medallion_wolf",
            parentName: "o_inv_consum_passive",
            spriteName: "s_inv_witcher_medallion_wolf",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        UndertaleGameObject o_loot_witcher_medallion_wolf = Msl.AddObject(
            name: "o_loot_witcher_medallion_wolf",
            parentName: "o_consument_loot",
            spriteName: "s_loot_witcher_medallion_wolf",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        o_inv_witcher_medallion_wolf.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                scr_consum_atr(""witcher_medallion_wolf"")

                ds_map_set(data, ""quality"", (7 << 0))
                ds_map_set(data, ""Colour"", make_colour_rgb(229, 193, 85))
                if object_is_ancestor(object_index, o_inv_slot_parent)
                    alarm[11] = shineDelay
                
                slot = ""Amulet""
                can_equip = true
            ")
        );

        o_loot_witcher_medallion_wolf.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                inv_object = o_inv_witcher_medallion_wolf
                number = 0
            ")
        );

        Msl.InjectTableItemStats(
            id: "witcher_medallion_wolf",
            Price: 200,
            EffPrice: 45,
            Cat: Msl.ItemStatsCategory.treasure,
            Material: Msl.ItemStatsMaterial.metal,
            Weight: Msl.ItemStatsWeight.Light,
            tags: Msl.ItemStatsTags.special
        );

        Msl.InjectTableItemsLocalization(
            new LocalizationItem(
                id: "witcher_medallion_wolf",
                name: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Wolf School Medallion"},
                    {ModLanguage.Chinese, "狼学派徽章"}
                },
                effect: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "The witcher medallion is sensitive to magic, vibrating and tugging on its chain when spells are being cast or magical beings, like genies or even mages, are present. The medallions vibrate in response to magic in all its forms, including curses, charms, and spells. They also warn of lurking monsters born of magic or magic experimentation."},
                    {ModLanguage.Chinese, "猎魔人徽章对所有魔法形式敏感，包括法术、诅咒、魅惑等；魔法物体、魔法生物、由魔法或实验制造出来的生物体甚至隐形的魔法生物也会被它感应到。当侦测到魔法存在时徽章就会震动并且猛拉挂着它的链子。"}
                },
                description: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "A witcher medallion is a silver symbol of the witchers' profession. Each one is shaped to represent the school a witcher comes from. This magic medallion is given to every young witcher candidate who has passed the Trial of the Grasses. The origin of the first medallions is unknown, though it is assumed they were made in the magic forge at Kaer Morhen."},
                    {ModLanguage.Chinese, "猎魔人徽章是猎魔人职业的一个银制的护符，做成不同的形状来代表猎魔人们所属的不同学派。每个通过了青草试炼的年轻猎魔人学徒都会得到一个徽章。人们并不确定最早的一枚徽章是何时和在哪儿制作的，不过据推测应该是在凯尔·莫罕的魔法熔炉里锻造出来的。"}
                }
            )
        );
    }
}