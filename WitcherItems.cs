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

        UndertaleGameObject o_inv_witcher_medallion_wolf = Msl.AddObject(
            name: "o_inv_witcher_medallion_wolf",
            parentName: "o_inv_timer_consum",
            spriteName: "s_inv_wolfschoolmedallion",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        UndertaleGameObject o_loot_witcher_medallion_wolf = Msl.AddObject(
            name: "o_loot_witcher_medallion_wolf",
            parentName: "o_consument_loot",
            spriteName: "s_loot_wolfschoolmedallion",
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

                ds_map_add_list(data, ""uniqueBossKill"", __dsDebuggerListCreate())

                scr_consum_attribute_simple_add(""Nature_Resistance"", 9);
                scr_consum_attribute_simple_add(""Magic_Resistance"", 9);
                scr_consum_attribute_simple_add(""Received_XP"", 6);
                scr_consum_attribute_simple_add(""VSN"", 1);

                slot = ""Amulet""
                can_equip = true

                enemy_count = 0
                secret_room = 0
            "),

            new MslEvent(eventType: EventType.Other, subtype: 24, code: @"
                enemy_count = 0
                secret_room = 0

                audio_play_sound(snd_skill_search, 4, 0)

                with (o_enemy)
                {
                    if (scr_is_prey_animal())
                        continue

                    if (!visible && scr_tile_distance(o_player, id) <= (o_player.VSN * 3))
                    {
                        // 非 NPC 敌人
                        if (!is_o_NPC_ancestor)
                        {
                            if (!object_is_ancestor(object_index, o_bird_parent) && !object_is_ancestor(object_index, o_Hive))
                            {
                                other.enemy_count++
                                scr_hearing_indicator_create()
                            }
                        }
                        // NPC 敌人
                        else if (!is_neutral)
                        {
                            other.enemy_count++
                            scr_hearing_indicator_create()
                        }
                    }
                }

                if (instance_exists(o_secret_door))
                {
                    with (o_secret_door)
                    {
                        if (!o_secret_door.is_open)
                        {
                            if (scr_tile_distance(o_player, id) <= (o_player.VSN * 3))
                            {
                                scr_characterStatsUpdateAdd(""secretRoomsFound"", 1)
                                o_secret_door.is_open = true
                                audio_play_sound(snd_secret_room_find, 4, 0)
                                event_user(1)
                                
                                with (o_fogrender)
                                    event_user(2)
                                
                                scr_psy_change(""MoraleSituational"", 10, ""trap_find"")
                                other.secret_room++
                            }
                        }
                    }
                }

                charge++
                event_inherited()
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
            Material: Msl.ItemStatsMaterial.silver,
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
                    {ModLanguage.English, "Every ~lg~12~/~ turns, the medallion scans within a range ~lg~5~/~ times the wielder’s sight. If enemies are present, " +
                        "it vibrates and yanks sharply on its chain. You can also ~lg~use~/~ the medallion actively to perform a scan.##" +
                        "~lg~Trophies~/~ placed into the witcher’s alchemy interface together with the ~y~Wolf School Medallion~/~ can grant it corresponding enhancements." },
                    {ModLanguage.Chinese, "每~lg~60~/~回合，徽章会在~lg~3~/~倍视野范围内做侦测，当敌人存在时就会震动并且猛拉挂着它的链子。也可主动~lg~使用~/~徽章进行侦测。##" +
                        "佩戴~y~狼学派徽章~/~首次击杀关底头目时，可以获得相应的加成。"}
                },
                description: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "The Witcher’s medallion is a silver amulet, crafted in different shapes to represent the various witcher schools. " +
                        "Geralt’s wolf-school medallion, however, for some unknown reason, has become tainted with the path of “sacrifice” in this new world..." },
                    {ModLanguage.Chinese, "猎魔人徽章是一种银制的护符，做成不同的形状来代表猎魔人们所属的不同学派。杰洛特的这枚狼学派徽章，却不知为何在新世界沾染上了“献祭”之道..."}
                }
            )
        );

        // Msl.InjectTableArmor(
        //     hook: Msl.ArmorHook.NECKLACES,
        //     name: "Wolf School Medallion",
        //     Tier: Msl.ArmorTier.Tier5,
        //     id: "witcher_medallion_wolf",
        //     Slot: Msl.ArmorSlot.Amulet,
        //     Class: Msl.ArmorClass.Light,
        //     rarity: Msl.ArmorRarity.Unique,
        //     Mat: Msl.ArmorMaterial.silver,
        //     Price: 200,
        //     MaxDuration: 80,
        //     Nature_Resistance: 9,
        //     Magic_Resistance: 9,
        //     Received_XP: 6,
        //     VSN: 1,
        //     tags: Msl.ArmorTags.specialexc
        // );

        // Msl.InjectTableWeaponTextsLocalization(
        //     new LocalizationWeaponText(
        //         id: "Wolf School Medallion",
        //         name: new Dictionary<ModLanguage, string>() {
        //             {ModLanguage.English, "Wolf School Medallion"},
        //             {ModLanguage.Chinese, "狼学派徽章"}
        //         },
        //         description: new Dictionary<ModLanguage, string>() {
        //             {ModLanguage.English, "A witcher medallion is a silver symbol of the witchers' profession. " +
        //                 "Each one is shaped to represent the school a witcher comes from. " +
        //                 "The medallions vibrate in response to magic in all its forms." },
        //             {ModLanguage.Chinese, "猎魔人徽章是一个银制的护符，做成不同的形状来代表猎魔人们所属的不同学派。" +
        //                 "猎魔人徽章对所有魔法形式敏感，当侦测到魔法存在时徽章就会震动并且猛拉挂着它的链子。" }
        //         }
        //     )
        // );
    }
}