using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddCharacters()
    {
        AddGeralt();
    }

    private void AddPerkBlavikenButcher()
    {
        UndertaleGameObject o_perk_blaviken_butcher = Msl.AddObject(
            name: "o_perk_blaviken_butcher",
            parentName: "o_perks",
            spriteName: "s_skills_passive_power_rune",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        o_perk_blaviken_butcher.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                ds_map_add(data, ""Weapon_Damage"", 10)
                ds_map_add(data, ""CRTD"", 20)
                ds_map_add(data, ""Magic_Power"", 10)
                ds_map_add(data, ""Miracle_Power"", 20)
                ds_map_add(data, ""Piercing_Resistance"", -20)
            "),

            new MslEvent(eventType: EventType.Other, subtype: 14, code: @"
                event_inherited()
                scr_skill_map_processor(1, 10)
            ")
        );

        Msl.InjectTableSkillsLocalization(
            new LocalizationSkill(
                id: "blaviken_butcher",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Blaviken Butcher"},
                    {ModLanguage.Chinese, "布拉维坎的屠夫"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "For each enemy killed, receive ~lg~+10%~/~ Weapon Damage and Magic Power, ~lg~+20%~/~ Crit Efficiency and Miracle Power, ~r~-20%~/~ Piercing Resistance for ~w~10~/~ turns.##This effect stacks."},
                    {ModLanguage.Chinese, "每杀一个敌人，兵器伤害与法力便~lg~+10%~/~，暴击效果与奇观效果便~lg~+20%~/~，同时穿刺抗性~r~-20%~/~，效果存续~w~10~/~回合。##这个效果可以叠加。"}
                }
            )
        );
    }

    private void AddGeralt()
    {
        AddPerkBlavikenButcher();

        string[] sprites = new string[4] {
            "s_GeraltHead_normal",
            "s_GeraltHead_helmet_normal",
            "s_GeraltHead_helmet_blood",
            "s_GeraltHead_blood"
        };

        for (int i = 0; i < sprites.Length; i++)
        {
            UndertaleSprite sprite = Msl.GetSprite(sprites[i]);
            sprite.OriginX = 15;
            sprite.OriginY = 36;
        }

        UndertaleGameObject o_gwynbleidd = Msl.AddObject(
            name: "o_gwynbleidd",
            parentName: "o_player",
            spriteName: "s_char_select",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        o_gwynbleidd.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                c_index = 4
                with (o_inventory)
                {
                    if (!is_start_equipment)
                    {
                        if (!global.is_load_game)
                        {
                            scr_atr_set_simple(""Head"", ""s_GeraltHead"")
                            scr_atr_set_simple(""CorpseSprite"", sprite_get_name(s_Geralt_dead))
                            scr_atr_set_simple(""BodySprite"", sprite_get_name(s_human_male))
                            with (scr_equip(""Worn Cloak"", (1 << 0)))
                                scr_inv_atr_set(""Duration"", 100)
                            with (scr_equip(""Peasant Pitchfork"", (3 << 0)))
                            {
                                scr_inv_atr_set(""Duration"", 80)
                                ds_map_set(data, ""identified"", true)
                                identified = true
                            }
                            with (scr_equip(""Fine Shirt"", (1 << 0)))
                                scr_inv_atr_set(""Duration"", 100)
                            with (scr_equip(""Apprentice Cowl"", (1 << 0)))
                                scr_inv_atr_set(""Duration"", 100)
                            with (scr_equip(""Peasant Shoes"", (1 << 0)))
                                scr_inv_atr_set(""Duration"", 100)
                            with (scr_inventory_add_item(o_inv_moneybag))
                                scr_container_add_gold(250)
                            with(scr_inventory_add_item(o_inv_witcher_medallion_wolf))
                            {
                                onStart_equipped = true
                                if (!equipped)
                                    event_user(5)
                            }
                            scr_inventory_add_item(o_inv_dumpling)
                            scr_inventory_add_item(o_inv_wineskin)
                            scr_inventory_add_item(o_inv_splint)
                            scr_inventory_add_item(o_inv_splint)
                            scr_inventory_add_item(o_inv_rag)
                            scr_inventory_add_item(o_inv_salve)
                            scr_inventory_add_item(o_inv_salve)
                            scr_inventory_add_item(o_inv_lockpicks)
                            scr_inventory_add_item(o_inv_map_osbrook)
                            scr_inventory_add_item(o_inv_leafshaped_bolts)
                        }
                        else
                            scr_load_player()
                        with (other.id)
                        {
                            alarm[11] = 3
                            scr_playerSpriteInit()
                        }
                    }
                    is_start_equipment = true
                }
                sprite_index = __asset_get_index(scr_atr(""BodySprite""))
            ")
        );

        Msl.LoadGML("gml_Object_o_dataLoader_Other_10")
            .MatchFrom("global.player_class = ")
            .InsertAbove(@"scr_classCreate(
                o_gwynbleidd, s_Geralt, ""Geralt"", ""Male"", ""Human"", ""Aldor"", ""Gwynbleidd"",
                10, 11, 10, 11, 11,
                [global.swords_tier1, global.swords2h_tier1, global.daggers_tier1, global.bows_tier1, global.armor_tier1, global.athletics_tier1, global.combat_tier1,
                    [""Witcher"", o_skill_witcher_alchemy_ico, o_skill_quen_sign_ico, o_skill_axii_sign_ico, o_skill_yrden_sign_ico]],
                [o_perk_blaviken_butcher], (1 << 0), false)")
            .Save();

        AddCharacterLocalization(
            id: "Geralt",
            class_id: "Gwynbleidd",
            char_name: "杰洛特",
            char_class: "白狼",
            char_desc: "猎魔人是人类术士们以天球交汇后出现的种种怪物为蓝本制造出来的杰作，" +
                "而作为狼学派三代弟子中最出色的一位，杰洛特被布洛克莱昂的林精们称为Gwynbleidd，" +
                "古语中意为白狼。他在北境与尼弗迦德战争中的种种经历不再赘述，然而这样一位传奇，" +
                "却可笑地死于农夫的草叉？##布拉维坎的屠夫，上古余血的养父，利维亚的杰洛特啊，" +
                "冥冥中的力量将你从死亡之中拯救而出，在奥尔多的土地上，你又会以怎样的故事取悦那双注视着你的眼睛呢。"
        );
    }

    private void AddCharacterLocalization(string id, string class_id, string char_name, string char_class, string char_desc)
    {
        List<string> table = Msl.ThrowIfNull(ModLoader.GetTable("gml_GlobalScript_table_text"));

        int startIndex = table.FindIndex(item => item.Contains("char_name;char_name;"));
        int endIndex = table.FindIndex(item => item.Contains("char_name_end;char_name_end;"));
        table.Insert(endIndex, $"{id};" + string.Concat(Enumerable.Repeat($"{char_name};", 9)));

        startIndex = table.FindIndex(item => item.Contains("class_name;class_name;"));
        endIndex = table.FindIndex(item => item.Contains("class_name_end;class_name_end;"));
        table.Insert(endIndex, $"{class_id};" + string.Concat(Enumerable.Repeat($"{char_class};", 9)));

        startIndex = table.FindIndex(item => item.Contains("char_desc;char_desc;"));
        endIndex = table.FindIndex(item => item.Contains("char_desc_end;char_desc_end;"));
        table.Insert(endIndex, $"{id};" + string.Concat(Enumerable.Repeat($"{char_desc};", 9)));

        ModLoader.SetTable(table, "gml_GlobalScript_table_text");
    }
}