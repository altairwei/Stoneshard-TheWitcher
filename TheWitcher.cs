// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib;
using UndertaleModLib.Models;

namespace TheWitcher;
public partial class TheWitcher : Mod
{
    public override string Author => "Altair";
    public override string Name => "The Witcher";
    public override string Description => "The Witcher";
    public override string Version => "1.0.0.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        Msl.AddFunction(ModFiles.GetCode("scr_apply_coating_oil.gml"), "scr_apply_coating_oil");
        Msl.AddFunction(ModFiles.GetCode("scr_coating_oil_damage_calc.gml"), "scr_coating_oil_damage_calc");
        Msl.AddFunction(ModFiles.GetCode("scr_hoversGetCoatingOilAttributes.gml"), "scr_hoversGetCoatingOilAttributes");

        PatchWitcherSkills();
        PatchCoatingDisplay();
        PatchWeaponCoatingSkill();
        AddWeaponOil();
        AddWitcherPotion();
        AddWitcherDecoction();
        AddTestPotionObject();
        AddWitcherSkillBook();
    }

    private void AddWitcherSkillBook()
    {
        UndertaleGameObject o_inv_book_witcher = Msl.AddObject(
            name: "o_inv_book_witcher",
            spriteName: "s_inv_bookG",
            parentName: "o_inv_treatise",
            isVisible: true,
            isAwake: true,
            isPersistent: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        UndertaleGameObject o_loot_book_witcher = Msl.AddObject(
            name: "o_loot_book_witcher",
            spriteName: "s_loot_BookG",
            parentName: "o_loot_treatise",
            isVisible: true,
            isAwake: true,
            isPersistent: false,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_inv_book_witcher.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                gain_xp = 50
                skills_array = [
                    ""Pyromancy"", o_skill_witcher_alchemy_ico, o_skill_quen_sign_ico, o_skill_axii_sign_ico, o_skill_yrden_sign_ico,
                    o_skill_aard_sign_ico, o_skill_igni_sign_ico, o_skill_trial_of_grasses]
            ")
        );

        o_loot_book_witcher.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                inv_object = o_inv_book_witcher
                number = 0
            ")
        );

        Msl.InjectTableItemStats(
            id: "book_witcher",
            Price: 150,
            EffPrice: 25,
            Material: Msl.ItemStatsMaterial.paper,
            tier: Msl.ItemStatsTier.Tier1,
            Subcat: Msl.ItemStatsSubcategory.treatise,
            Weight: Msl.ItemStatsWeight.Light,
            tags: Msl.ItemStatsTags.special
        );

        Msl.InjectTableBooksLocalization(
            new LocalizationBook(
                id: "book_witcher",
                name: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "The Witcher Notes" },
                    { ModLanguage.Chinese, "猎魔人笔记" }
                },
                content: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Notes on the Witcher skills and abilities." },
                    { ModLanguage.Chinese, string.Join("##",
                        "（ . . . ）",
                        "在天球交汇意外裂开的缝隙中，我坠入了一个陌生的大陆，这里的人称它为奥尔多。我很快发现，这个名为奥尔多的世界充斥着陌生的魔法与异于我所熟悉的野兽。在无数个昼夜的流浪与挣扎后，我决心利用自己作为猎魔人的知识，结合本地材料与魔法，探寻在此生存并狩猎怪物的新道路。",
                        "（ . . . ）",
                        "我惊奇地发现此处法师使用一种称作“以太”的能量。经过多次实验，我成功以此世界的草药与矿石代替家乡的炼金材料，制出了类似的魔药。",
                        "（ . . . ）",
                        "奥尔多的怪物种类繁多，与故乡不同。我专注研究了它们的弱点，根据猎物的特征与当地材料，开发了新的剑油，以及全新的煎药。",
                        "（ . . . ）",
                        "我的探索尚未终结，艾尔多大陆的深处依旧充满未知的挑战。我希望此册笔记能为日后误入此界的猎魔人提供参考，也作为自己继续探索未知的见证。"
                    )}
                },
               midText: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "\"Chronicles of Exploration in a Foreign Realm\"##~gr~Allows you to learn the following Witcher abilities:~/~##~lg~The Witcher's Alchemy~/~#~y~Trial Of Grasses~/~#~lg~Quen Sign~/~#~lg~Axii Sign~/~#~lg~Yrden Sign~/~#~lg~Aard Sign~/~#~lg~Igni Sign~/~##Reading this book grants some ~y~Experience~/~." },
                    { ModLanguage.Chinese, "《迷途异境的探索手札》##~gr~可以学习若干猎魔人能力：~/~##~lg~猎魔人炼金术~/~#~y~青草试炼~/~#~lg~昆恩法印~/~#~lg~亚克西法印~/~#~lg~亚登法印~/~#~lg~阿尔德法印~/~#~lg~伊格尼法印~/~##阅读本书可以获得一定的~y~经验~/~。" }
                },
                description: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "A book on the Witcher skills and abilities." },
                    { ModLanguage.Chinese, "这本书详细猎魔人的技能、剑油、魔药以及煎药的制作。此手札的原作者已不可考，但据其内容推测可能是异世界来客。" }
                },
                type: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Written by an unknown author" },
                    { ModLanguage.Chinese, "作者：佚名" }
                }
            )
        );

        Msl.LoadGML("gml_Object_o_player_chest_Alarm_1")
            .MatchFromUntil("if (scr_user_owns_dlc", "}")
            .InsertBelow("scr_inventory_add_item(o_inv_book_witcher)")
            .Save();
    }

    private void PatchWeaponCoatingSkill()
    {
        UndertaleGameObject o_skill_weapon_coating = Msl.AddObject(
            name: "o_skill_weapon_coating",
            spriteName: "sprite1",
            parentName: "o_weapon_skills",
            isVisible: true,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_weapon_coating.ApplyEvent(ModFiles,
            new MslEvent("o_skill_weapon_coating_create_0.gml", EventType.Create, 0),
            new MslEvent("o_skill_weapon_coating_other_11.gml", EventType.Other, 11),
            new MslEvent("o_skill_weapon_coating_other_20.gml", EventType.Other, 20)
        );
    }

    private void PatchCoatingDisplay()
    {
        Msl.LoadGML("gml_Object_o_hoverWeapon_Create_0")
            .MatchFrom("cursedName = ")
            .InsertAbove(ModFiles, "o_hoverWeapon_create_0.gml")
            .Save();

        Msl.LoadGML("gml_Object_o_hoverWeapon_Other_20")
            .MatchFrom("cursedNameHeight = 0")
            .InsertAbove(ModFiles, "o_hoverWeapon_other_20.gml")
            .Save();

        Msl.LoadGML("gml_Object_o_hoverWeapon_Other_21")
            .MatchFrom("if middleHeight")
            .InsertAbove(ModFiles, "o_hoverWeapon_other_21.gml")
            .Save();

        Msl.LoadGML("gml_Object_o_inv_slot_Draw_0")
            .MatchFrom("if _wounded")
            .InsertAbove(@"
                var _oil = ds_map_find_value_ext(data, ""coating_oil"", """")
                var _count = ds_map_find_value_ext(data, ""oil_available_count"", 0)
                if (_oil != """" && _count > 0)
                {
                    draw_sprite_ext(s_ico_coating, 0, (x + _icons_offset_x), (y + 1), 1, 1, 0, c_white, image_alpha)
                    _icons_offset_x += (sprite_get_width(s_ico_coating) + 4)
                }
            ")
            .Save();
    }
}
