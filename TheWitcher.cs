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
    public override string Author => "Altair & 北境的救世主";
    public override string Name => "The Witcher";
    public override string Description => "The Witcher";
    public override string Version => "0.3.7";
    public override string TargetVersion => "0.9.2.13";

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
        AddWitcherItems();
        AddCharacters();
    }

    private void AddWitcherSkillBook()
    {
        UndertaleGameObject o_inv_book_witcher1 = Msl.AddObject(
            name: "o_inv_book_witcher1",
            spriteName: "s_inv_bookG",
            parentName: "o_inv_treatise",
            isVisible: true,
            isAwake: true,
            isPersistent: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        UndertaleGameObject o_inv_book_witcher2 = Msl.AddObject(
            name: "o_inv_book_witcher2",
            spriteName: "s_inv_bookB",
            parentName: "o_inv_treatise",
            isVisible: true,
            isAwake: true,
            isPersistent: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        UndertaleGameObject o_loot_book_witcher1 = Msl.AddObject(
            name: "o_loot_book_witcher1",
            spriteName: "s_loot_BookG",
            parentName: "o_loot_treatise",
            isVisible: true,
            isAwake: true,
            isPersistent: false,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        UndertaleGameObject o_loot_book_witcher2 = Msl.AddObject(
            name: "o_loot_book_witcher2",
            spriteName: "s_loot_BookB",
            parentName: "o_loot_treatise",
            isVisible: true,
            isAwake: true,
            isPersistent: false,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_inv_book_witcher1.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                gain_xp = 50
                skills_array = [
                    ""Witcher"", o_skill_witcher_alchemy_ico, o_skill_quen_sign_ico, o_skill_axii_sign_ico, o_skill_yrden_sign_ico]
            ")
        );

        o_inv_book_witcher2.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                gain_xp = 50
                skills_array = [
                    ""Witcher"", o_skill_aard_sign_ico, o_skill_igni_sign_ico, o_skill_trial_of_grasses]
            ")
        );

        o_loot_book_witcher1.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                inv_object = o_inv_book_witcher1
                number = 0
            ")
        );

        o_loot_book_witcher2.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                inv_object = o_inv_book_witcher2
                number = 0
            ")
        );

        Msl.InjectTableItemStats(
            id: "book_witcher1",
            Price: 250,
            EffPrice: 50,
            Material: Msl.ItemStatsMaterial.paper,
            tier: Msl.ItemStatsTier.Tier1,
            Subcat: Msl.ItemStatsSubcategory.treatise,
            Weight: Msl.ItemStatsWeight.Light,
            tags: Msl.ItemStatsTags.special
        );

        Msl.InjectTableItemStats(
            id: "book_witcher2",
            Price: 1200,
            EffPrice: 250,
            Material: Msl.ItemStatsMaterial.paper,
            tier: Msl.ItemStatsTier.Tier2,
            Subcat: Msl.ItemStatsSubcategory.treatise,
            Weight: Msl.ItemStatsWeight.Light,
            tags: Msl.ItemStatsTags.special
        );

        Msl.InjectTableBooksLocalization(
            new LocalizationBook(
                id: "book_witcher1",
                name: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Witcher Notes I" },
                    { ModLanguage.Chinese, "猎魔人笔记一" }
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
                    { ModLanguage.English, "\"Chronicles of Exploration in a Foreign Realm\"##~gr~Allows you to learn the following Witcher abilities:~/~##~lg~The Witcher's Alchemy~/~#~lg~Quen Sign~/~#~lg~Axii Sign~/~#~lg~Yrden Sign~/~##Reading this book grants some ~y~Experience~/~." },
                    { ModLanguage.Chinese, "《迷途异境的探索手札》##~gr~可以学习若干猎魔人能力：~/~##~lg~猎魔人炼金术~/~#~lg~昆恩法印~/~#~lg~亚克西法印~/~#~lg~亚登法印~/~##阅读本书可以获得一定的~y~经验~/~。" }
                },
                description: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "A book on the Witcher skills and abilities." },
                    { ModLanguage.Chinese, "这本书详细介绍了猎魔人的剑油、魔药以及煎药的制作，以及那些名为“法印”的奇怪咒法。作者杰隆·莫吕不知从何而来，在奥尔多游历多年，据手札内容推测可能是异世界来客。" }
                },
                type: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Written by Jerome Moreau" },
                    { ModLanguage.Chinese, "作者：杰隆·莫吕" }
                }
            ),
            new LocalizationBook(
                id: "book_witcher2",
                name: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Witcher Notes II" },
                    { ModLanguage.Chinese, "猎魔人笔记二" }
                },
                content: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, string.Join("##",
                        "（ . . . ）",
                        "When the fabric of reality tore during the Conjunction of the Spheres, two entirely distinct magic systems encountered each other—one from my homeland, harnessing natural forces and alchemical traditions, and the other from Aldor, relying upon the enigmatic energy called \"Aether.\"",
                        "（ . . . ）",
                        "This encounter posed profound challenges yet unprecedented opportunities. By exploring both systems, I discovered ways to blend their strengths, forming entirely new techniques for combat, survival, and alchemy.",
                        "（ . . . ）",
                        "Combining alchemical techniques with Aether significantly enhances potion potency.",
                        "（ . . . ）",
                        "Blending local herbs with my homeland’s alchemical catalysts created sword oils effective against previously resilient creatures.",
                        "（ . . . ）",
                        "Mastering both magic systems provided strategic flexibility, greatly improving combat adaptability.",
                        "（ . . . ）",
                        "This encounter of magics is just the beginning, the possibilities ahead are endless."
                    ) },
                    { ModLanguage.Chinese, string.Join("##",
                        "（ . . . ）",
                        "在天球交汇时现实之幕撕裂开来，两种截然不同的魔法体系相互碰撞——一个来自我的家乡，依靠自然之力与炼金术传统；另一个则来自奥尔多大陆，依靠神秘的以太能量。",
                        "（ . . . ）",
                        "这次碰撞带来了深刻的挑战，也创造了前所未有的机遇。通过探索这两种体系，我找到了融合二者优势的方法，开发出全新的战斗技巧、生存之道以及炼金术。",
                        "（ . . . ）",
                        "将炼金术技巧与以太能量相结合，可显著提高药剂的效力。",
                        "（ . . . ）",
                        "本地草药与家乡的炼金催化剂结合，创造出的剑油对以往难以应对的生物效果显著。",
                        "（ . . . ）",
                        "同时掌握两种魔法体系提供了战略上的灵活性，大幅提升战斗适应能力。",
                        "（ . . . ）",
                        "魔法体系的碰撞只是开始，前方的可能性无限宽广。"
                    )}
                },
                midText: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "\"Encounter Between Two Magics\"##~gr~Allows you to learn the following Witcher abilities:~/~##~lg~Trial Of Grasses~/~#~lg~Aard Sign~/~#~lg~Igni Sign~/~##Reading this book grants some ~y~Experience~/~." },
                    { ModLanguage.Chinese, "《两个魔法体系的碰撞》##~gr~可以学习若干猎魔人能力：~/~##~lg~青草试炼~/~#~lg~阿尔德法印~/~#~lg~伊格尼法印~/~##阅读本书可以获得一定的~y~经验~/~。" }
                },
                description: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "A book on the Witcher skills and abilities." },
                    { ModLanguage.Chinese, "本书详细记录了宇宙裂隙中两种独特魔法传统的交汇，探讨了炼金术与以太融合后创造出的强大新技巧。" }
                },
                type: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Written by Jerome Moreau" },
                    { ModLanguage.Chinese, "作者：杰隆·莫吕" }
                }
            )
        );


        Msl.LoadGML("gml_Object_o_npc_herbalist_osbrook_Other_19")
            .MatchFrom("ds_list_add(singular_stock_list")
            .InsertBelow("ds_list_add(singular_stock_list, o_inv_book_witcher1)")
            .Save();

        Msl.LoadGML("gml_Object_o_npc_lowcrey_Other_19")
            .MatchAll()
            .InsertBelow("ds_list_add(singular_stock_list, o_inv_book_witcher2)")
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
