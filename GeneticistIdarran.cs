using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddNewNPCs()
    {
        AddNewNPC_Idarran();
        EditRoom_BrynnUniversityCellar();
    }

    private void AddNewNPC_Idarran()
    {
        UndertaleSprite s_npc_Idarran = Msl.GetSprite("s_npc_Idarran");
        s_npc_Idarran.CollisionMasks.RemoveAt(0);
        s_npc_Idarran.IsSpecialType = true;
        s_npc_Idarran.SVersion = 3;
        s_npc_Idarran.Width = 44;
        s_npc_Idarran.Height = 49;
        s_npc_Idarran.MarginLeft = 11;
        s_npc_Idarran.MarginRight = 31;
        s_npc_Idarran.MarginBottom = 39;
        s_npc_Idarran.MarginTop = 5;
        s_npc_Idarran.OriginX = 22;
        s_npc_Idarran.OriginY = 34;
        s_npc_Idarran.GMS2PlaybackSpeed = 1;
        s_npc_Idarran.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;

        UndertaleGameObject o_npc_Idarran = Msl.AddObject(
            name: "o_npc_Idarran",
            parentName: "o_npc_brynn",
            spriteName: "s_npc_Idarran",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_npc_Idarran.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                name = ds_map_find_value(global.npc_info, ""Idarran"")
                occupation = ""geneticist""
                avatar = s_npc_Idarran_P
                ds_list_clear(myfloor_list)
                ds_list_add(myfloor_list, ""H1"", ""H1"", ""H1"", ""H1"")
                myfloor = ""H1""

                chat = true
                rumors = false
                scr_buying_loot_category(
                    ""alcohol"", ""beverage"", ""drug"", ""ingredient"",
                    ""jewelry"", ""valuable"", ""medicine"", ""potion"")
                scr_npc_gold_init(200, 600)
                Restock_Time = 24
                Ingredient_BP_Mod += 0.33
                Ingredient_SP_Mod += 0.33
                Food_SP_Mod += 0.33
                Treatise_SP_Mod += 0.33
                Alcohol_SP_Mod += 0.33
                Selling_Prices += 0.1

                scr_create_skill_map(""Discharge"")
                scr_create_skill_map(""Impulse"")
                scr_create_skill_map(""Short_Circuit"")
                scr_create_skill_map(""Chain_Lightning"")
                scr_create_skill_map(""Tempest"")
                scr_create_skill_map(""Curse"")
                scr_create_skill_map(""Life_Leech"")
                scr_create_skill_map(""Seal_of_Shackles"")
                scr_create_skill_map(""Seal_of_Power"")
                scr_create_skill_passive(o_pass_skill_dispersion)
                scr_create_skill_passive(o_pass_skill_residual_charge)
                scr_create_skill_passive(o_pass_skill_unlimited_power)
                scr_create_skill_passive(o_pass_skill_inner_reserves)
            "),

            new MslEvent(eventType: EventType.PreCreate, subtype: 0, code: @"
                event_inherited()
                descID = ""student""
            "),

            new MslEvent(eventType: EventType.Other, subtype: 19, code: @"
                event_inherited()
                scr_selling_loot_object(
                    o_inv_antitoxin, irandom_range(1, 2),
                    o_inv_herbal, irandom_range(1, 3),
                    o_inv_antivenom, irandom_range(2, 3),
                    o_inv_inhaler, irandom_range(1, 2),

                    o_inv_oil, irandom_range(1, 3),
                    o_inv_spirit, irandom_range(2, 4),

                    o_inv_stool, irandom_range(1, 3),
                    o_inv_flyagaric, irandom_range(1, 3),
                    o_inv_henbane, irandom_range(1, 3),
                    o_inv_citrus, irandom_range(0, 1),
                    o_inv_ginger, irandom_range(0, 1),
                    o_inv_hornet_honey, irandom_range(0, 1),
                    o_inv_honey, irandom_range(1, 3),
                    o_inv_harpy_egg_raw, irandom_range(0, 1),
                    o_inv_troll_gland, irandom_range(0, 1),
                    o_inv_gulon_liver, irandom_range(0, 1),
                    o_inv_mandibles, irandom_range(0, 1),

                    choose(o_inv_hanged_man_venom, o_inv_vampire_oil), 1,
                    choose(o_inv_necrophage_oil, o_inv_specter_oil, o_inv_insectoid_oil), 1,
                    choose(o_inv_hybrid_oil, o_inv_ogroid_oil), 1,
                    choose(o_inv_thunderbolt_potion, o_inv_blizzard_potion, o_inv_petri_philter), 1,
                    choose(o_inv_swallow_potion, o_inv_tawny_owl, o_inv_golden_oriole), 1,
                    choose(o_inv_ghoul_decoction, o_inv_crawler_decoction,
                        o_inv_harpy_decoction, o_inv_troll_decoction, o_inv_gulon_decoction), 1
                )
                scr_selling_loot_category(""herb"", irandom_range(8, 16))
            ")
        );

        InjectItemsToTable(
            table: "gml_GlobalScript_table_names",
            anchor: "NPC_info;NPC_info;NPC_info;",
            new Dictionary<int, string>
            {
                [0] = "geneticist",
                [2] = "Geneticist",
                [3] = "遗传学家"
            },
            new Dictionary<int, string>
            {
                [0] = "Idarran",
                [2] = "Idarran",
                [3] = "艾达兰"
            }
        );

        /*
        InjectItemsToTable(
            table: "gml_GlobalScript_table_mobs_stats",
            anchor: "// NPCS;;;;;;;;;;;;",
            new Dictionary<string, string>
            {
                ["name"] = "Idarran",
                ["Tier"] = "5",
                ["ID"] = "o_npc_Idarran",
                ["type"] = "human",
                ["faction"] = "GrandMagistrate",
                ["pattern"] = "Mage",
                ["weapon"] = "2hStaff",
                ["armor"] = "Light",
                ["size"] = "medium",
                ["matter"] = "flesh",
                ["VIS"] = "12",
                ["HP"] = "145",
                ["MP"] = "220",
                ["Head_DEF"] = "3",
                ["Body_DEF"] = "6",
                ["Arms_DEF"] = "3",
                ["Legs_DEF"] = "6",
                ["Hit_Chance"] = "86",
                ["EVS"] = "40",
                ["PRR"] = "27",
                ["Block_Power"] = "20",
                ["Block_Recovery"] = "50",
                ["Crit_Avoid"] = "20",
                ["CRT"] = "5",
                ["CRTD"] = "15",
                ["CTA"] = "10",
                ["FMB"] = "6",
                ["Magic_Power"] = "100",
                ["Miscast_Chance"] = "-20",
                ["Miracle_Chance"] = "35",
                ["Miracle_Power"] = "50",
                ["MP_Restoration"] = "40",
                ["Cooldown_Reduction"] = "-70",
                ["Fortitude"] = "50",
                ["Health_Restoration"] = "15",
                ["Manasteal"] = "20",
                ["Bleeding_Resistance"] = "25",
                ["Knockback_Resistance"] = "25",
                ["Stun_Resistance"] = "30",
                ["Daze_Chance"] = "35",
                ["Knockback_Chance"] = "35"
            }
        );
        */

        InjectItemsToTable(
            table: "gml_GlobalScript_table_lines",
            anchor: ";;;;;;;[NPC] GREETINGS;;;;;;;;;;;",
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "Geralt, still bargaining with fate, I see.",
                ["中文"] = "杰洛特，又在和命运讨价还价？"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Today’s experiment went well. Care to be my control sample?",
                ["中文"] = "今天实验顺利，你要不要也做个对照组？"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "Your medallion still vibrating? Perhaps it remembers me.",
                ["中文"] = "你的徽章还在震动吗？也许是它在想我。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "You look a bit more human today... pity.",
                ["中文"] = "你看起来比昨天更人类一点，真可惜。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "Relax, I’m not in need of a new witcher sample—yet.",
                ["中文"] = "别担心，我暂时不需要新的猎魔人样本。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "Do you see it? Perfection itself, reshaped by my hands.",
                ["中文"] = "你看到了么？完美的生命正在我的手中重塑。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "Alzur once said: ‘The limits of creation are excuses for the timid.’",
                ["中文"] = "阿尔祖曾说：‘造物的界限，只是怯懦者的借口。’"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "A witcher? Ah, one of my unfinished prototypes.",
                ["中文"] = "猎魔人？啊，是我的半成品。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "Cosimo wrote order in sigils, I wrote evolution in blood.",
                ["中文"] = "科西莫用符号写下秩序，而我用血液写下进化。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "I do not create monsters. I simply peel away the illusion of humanity.",
                ["中文"] = "我并非创造怪物，我只是让真相剥离伪装。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "I admire that forced composure of yours. It’s just one drop of mutagen away from madness.",
                ["中文"] = "我很欣赏你那种被逼出来的理智。它和疯狂只差一滴突变液。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Seeing you always reminds me of a question—evolution or regression, which one’s winning?",
                ["中文"] = "见到你总让我想起一个问题：进化和退化，究竟谁在赢？"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Interesting, you don’t smell of swamps or blood today. Fresh armor, perhaps?",
                ["中文"] = "有趣，你今天闻起来不像沼泽或血。新洗的甲胄？"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Welcome. Don’t touch the flasks unless you wish to become part of the study early.",
                ["中文"] = "欢迎。请别乱碰那边的瓶子，除非你想提前成为样本。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Excellent, I was missing a control group.",
                ["中文"] = "很好，我正缺少对照组。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Relax. I only require corpses during the testing phase.",
                ["中文"] = "放松，我只在实验阶段需要尸体。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Those eyes... they’ve seen Chaos. I like that kind of specimen.",
                ["中文"] = "你的眼神像是见过混沌的人。我喜欢这样的样本。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Most in Aldor reek of ignorance. You, at least, smell of curiosity.",
                ["中文"] = "奥尔多的人大多无知，而你至少有求知的气味。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Have you brought news, gold, or biological tissue?",
                ["中文"] = "你带来了消息，金币，还是新的生物组织？"
            },
            new Dictionary<string, string>
            {
                ["id"] = "greeting",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "I trust you’re here to talk, not to torch my research.",
                ["中文"] = "希望你是来谈话的，而不是来燃烧我的研究。"
            }
        );

        InjectItemsToTable(
            table: "gml_GlobalScript_table_lines",
            anchor: ";;;;;;;[NPC] RESTING LINES;;;;;;;;;;;",
            new Dictionary<string, string>
            {
                ["id"] = "rest_pm",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "...Could you be quiet? Knowledge dislikes being interrupted by noise.",
                ["中文"] = "……能安静点吗？知识不喜欢被噪音打断。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "rest_pm",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "I’m reading, not hosting a conference.##Unless you bring new discoveries, let the words keep speaking instead.",
                ["中文"] = "我在读书，不是开会。##如果你没带新发现，那就让文字继续说话。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "rest_pm",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "This volume is about pain conduction... care for a live demonstration?",
                ["中文"] = "这本书讲的是痛觉传导……要不要我现场演示？"
            },
            new Dictionary<string, string>
            {
                ["id"] = "rest_pm",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Thought is alchemy—disturbing it is like adding the wrong catalyst.",
                ["中文"] = "思考是一种炼金术，打扰它就像掺错了催化剂。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "rest_pm",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Unless you’re more interesting than this footnote, don’t speak.",
                ["中文"] = "除非你能比这页脚注更有趣，否则别开口。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "rest_pm",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "I’ve memorized your step rhythm. Don’t make me file it under ‘experimental interference’.",
                ["中文"] = "我已经记下你的脚步频率，别让我在报告里写‘干扰因素’。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "rest_pm",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "...The formula doesn’t balance. Too many variables. Or perhaps I’m still too human.",
                ["中文"] = "……公式对不上，变量太多。或者，是我太人类了。"
            }
        );

        InjectItemsToTable(
            table: "gml_GlobalScript_table_lines",
            anchor: ";;;;;;;[NPC] CHATS;;;;;;;;;;;",
            new Dictionary<string, string>
            {
                ["id"] = "chat",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "You call it Aldor. I call it an error—an alchemical byproduct of time itself.##" +
                    "I was attempting interdimensional genome recombination... and then, the door opened.##" +
                    "The magic here is unlike our Chaos—purer, yet more feral.",
                ["中文"] = "你称它为‘奥尔多’，而我称它为‘误差’——一次时空炼金的副产物。##" +
                    "我当时正在尝试跨维基因重组实验……然后，门开了。##" +
                    "这里的魔力结构与你我熟知的混沌截然不同——更纯净，也更野蛮。##"
            },
            new Dictionary<string, string>
            {
                ["id"] = "chat",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "I’ve studied a native creature called the Gulon—its cells devour mana itself. Beautiful, isn’t it?##" +
                    "This world made me believe Chaos isn’t a flaw—it’s the embryo of higher order.",
                ["中文"] = "我研究了一种本地生物，名为‘谷隆’，它的细胞能主动吞噬法力。很美妙，不是吗？##" +
                    "这个世界让我重新相信，混乱并非错误，而是更高秩序的雏形。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "chat",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "Still hunting my children, are you? The ones your world called monsters.##" +
                    "Ironic, isn’t it? Here they worship me as a god, and you as a beast.##" +
                    "Witchers were our most perfect design—you lot just refuse to admit it.##" +
                    "Ever get the feeling that perhaps humanity was the real monster all along?",
                ["中文"] = "你依然在狩猎我的孩子们么？那些曾在你的世界被称作怪物的生命。##" +
                    "讽刺吧？这个世界把我视作神，而把你当成野兽。##" +
                    "猎魔人……其实是我们最完美的作品，只是你们自己不愿承认。##" +
                    "你有过这样的错觉么？也许‘人类’才是异界中真正的怪物。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "chat",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "If Alzur could see my work now, he’d either smile... or frown deeply.##" +
                    "Cosimo... that old man would rather die in an equation than live in a miracle.##" +
                    "They thought I sought immortality. I simply despised endings.",
                ["中文"] = "阿尔祖若看到我如今的成果，一定会微笑。或者——皱眉。##" +
                    "科西莫……啊，那老家伙宁愿死在公式里，也不肯活在奇迹中。##" +
                    "他们都以为我在追求永生。其实我只是讨厌结局。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "chat",
                ["Role"] = "geneticist",
                ["Type"] = "geralt",
                ["English"] = "You call yourself human, yet deny the blood that made you something else.##" +
                    "We’re both ghosts of creation, Geralt—only I create, and you destroy.##" +
                    "I don’t hate you. In truth, I’ve always considered you our proudest failure.##" +
                    "Funny thing—no matter the world, witchers remain a lonely species.",
                ["中文"] = "你称自己为人，却拒绝承认自己的血液早已与人类不同。##" +
                    "我们都是造物的幽灵，杰洛特——只不过我还在造，而你在毁。##" +
                    "我不憎恨你。事实上，我一直把你视为我们最骄傲的失败。##" +
                    "有趣的是，无论在哪个世界，猎魔人总是孤独的物种。"
            },

            new Dictionary<string, string>
            {
                ["id"] = "chat",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "The beauty of life lies in its endless attempts to correct itself. I merely help it hurry along.##" +
                    "In Aldor, I’ve seen fish that breathe air, bones that sing, and metals that heal themselves. Nature is never short of imagination.##" +
                    "My failure rate is low. Most so-called failures simply didn’t have time to finish evolving.##" +
                    "They call my work a blasphemy of life. They forget—creation itself was the first blasphemy.",
                ["中文"] = "生命的美妙在于它不断试图自我修正，而我，只是帮它快一点。##" +
                    "在奥尔多，我见过能在体外呼吸的鱼、会唱歌的骨头，还有自愈的铁。自然从不缺乏想象力。##" +
                    "我的实验失败率很低，大多数‘失败’只是没来得及完成。##" +
                    "有人说我在亵渎生命，可他们忘了——创造本身就是最古老的亵渎。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "chat",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "People fear the unknown, though the unknown simply can’t be bothered to introduce itself.##" +
                    "Ignorance brings happiness... but happiness corrodes the nervous system.##" +
                    "Sanity is a fragile potion—the more you use it, the less it works.##" +
                    "After studying hundreds of species, I found one universal truth—stupidity survives evolution.",
                ["中文"] = "人们总害怕未知，其实未知只是懒得自我介绍。##" +
                    "无知能带来幸福，但幸福会腐蚀神经系统。##" +
                    "理智是脆弱的药剂，用多了就失效。##" +
                    "我研究了几百个物种，唯一恒定的结论是：愚蠢比进化更顽强。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "chat",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Aldor is a living laboratory. The air is reagent, the ground a petri dish.##" +
                    "I’m not sure whether I’ve changed Aldor—or Aldor is rewriting me.##" +
                    "Magic here flows like blood... though no one’s figured out whose body it belongs to.##" +
                    "They say Aldor devours outsiders. I say it’s merely selecting the worthy.",
                ["中文"] = "奥尔多是一座活着的实验室。空气是试剂，大地是培养皿。##" +
                    "我不确定是我改变了奥尔多，还是奥尔多正在重写我。##" +
                    "这里的魔力像血液一样流动，只是还没人弄清它属于谁的身体。##" +
                    "有人说奥尔多在吞噬外来者，但我认为它是在筛选合格者。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "chat",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Fate is just statistics wearing a robe.##" +
                    "Order is an illusion—much like stable sanity.##" +
                    "Chaos doesn’t need worship—it demands comprehension.##" +
                    "Gods? I prefer miracles that can be replicated.",
                ["中文"] = "命运不过是统计学的另一种说法。##" +
                    "秩序是一种幻觉，就像稳定的精神状态。##" +
                    "混沌不需要崇拜，它需要理解。##" +
                    "神？我更喜欢可重复验证的奇迹。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "chat",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "If I’m not mistaken, you just stepped on a potion worth a month’s funding.##" +
                    "Don’t touch that flask. The last time it exploded, this arm was still growing.##" +
                    "Experiment failed? No, the world simply didn’t cooperate fast enough.##" +
                    "I tried sleeping once. Inspiration was louder than my dreams.",
                ["中文"] = "如果我没认错，你刚踩到了一瓶价值一个月研究经费的药。##" +
                    "别碰那只瓶子。它上次爆炸时，我还没长出现在这条手臂。##" +
                    "实验失败了？不，是世界配合得不够快。##" +
                    "我试过睡觉，但灵感总比梦更吵。"
            },

            new Dictionary<string, string>
            {
                ["id"] = "no_chat",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Let’s end the talk here. Words tend to dilute thought.",
                ["中文"] = "我们的谈话到此为止吧，言语会稀释思考。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "no_chat",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "I’ve no new conclusions, and you’ve no new questions. A perfect moment for silence.",
                ["中文"] = "我暂时没有新的结论，你也没新的问题。完美的沉默时刻。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "no_chat",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Any further talk would waste air—and I still need it for experiments.",
                ["中文"] = "继续聊下去只会浪费空气，而我还得留着做实验。"
            },

            new Dictionary<string, string>
            {
                ["id"] = "return",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "A pleasant chat. Once I find the proper reagent, we can continue.",
                ["中文"] = "愉快的谈话。等我找到合适的试剂，我们可以继续。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "return",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Glad you understood. Most people just hear ‘madman’.",
                ["中文"] = "很高兴你能听懂。大多数人只听到‘疯子’。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "return",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "When you visit next time, bring something interesting—words, blood, either works.",
                ["中文"] = "如果下次再来，记得带点新奇的素材。文字、血液都行。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "return",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "I should return to my notes. Chaos waits for no one.",
                ["中文"] = "我得回去写报告了。对混沌的研究从不会等人。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "return",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "An amusing chat. Next time, I’ll record your neural response.",
                ["中文"] = "真有趣的谈话，下次我会记录你的脑电反应。"
            },
            new Dictionary<string, string>
            {
                ["id"] = "return",
                ["Role"] = "geneticist",
                ["Type"] = "any",
                ["English"] = "Farewell, stranger. Or perhaps... see you next dissection.",
                ["中文"] = "再见，陌生人。或者，下次再解剖见。"
            }
        );
    }

    private void EditRoom_BrynnUniversityCellar()
    {
        UndertaleRoom room = Msl.GetRoom("r_BrynnUniversityCellar");
        UndertaleRoom.Layer LayerTarget = Msl.GetLayer(room, UndertaleRoom.LayerType.Instances, "Targets");
        UndertaleRoom.Layer LayerNPC = Msl.GetLayer(room, UndertaleRoom.LayerType.Instances, "NPC");

        UndertaleRoom.GameObject student04uni = LayerNPC.InstancesData.Instances.First(
                t => t.ObjectDefinition.Name.Content == "o_npc_Student04UNI");
        LayerNPC.InstancesData.Instances.Remove(student04uni);
        room.GameObjects.Remove(student04uni);

        UndertaleRoom.GameObject stand_position = Msl.ThrowIfNull(
            LayerTarget.InstancesData.Instances.First(
                t => t.ObjectDefinition.Name.Content == "o_NPC_target" && t.X == 338 && t.Y == 520));

        UndertaleRoom.GameObject sitedown_position = Msl.ThrowIfNull(
            LayerTarget.InstancesData.Instances.First(
                t => t.ObjectDefinition.Name.Content == "o_NPC_target" && t.X == 520 && t.Y == 182));

        string creationCode = @$"
            target_array = [{stand_position.InstanceID}, {stand_position.InstanceID}, {stand_position.InstanceID}, {sitedown_position.InstanceID}]
            myfloor_counter = ""H1""
            idle_state = false
            time_period_night = time_period_day
        ";

        room.AddGameObject(
            LayerNPC,
            "o_npc_Idarran",
            Msl.AddCode(creationCode, "gml_RoomCC_r_BrynnUniversityCellar_100_Create"),
            x: 609, y: 432
        );

        UndertaleSprite s_npc_Idarran_idle = Msl.GetSprite("s_npc_Idarran_idle");
        s_npc_Idarran_idle.CollisionMasks.RemoveAt(0);
        s_npc_Idarran_idle.IsSpecialType = true;
        s_npc_Idarran_idle.SVersion = 3;
        s_npc_Idarran_idle.Width = 29;
        s_npc_Idarran_idle.Height = 52;
        s_npc_Idarran_idle.MarginLeft = 4;
        s_npc_Idarran_idle.MarginRight = 27;
        s_npc_Idarran_idle.MarginBottom = 42;
        s_npc_Idarran_idle.MarginTop = 3;
        s_npc_Idarran_idle.OriginX = 19;
        s_npc_Idarran_idle.OriginY = 34;
        s_npc_Idarran_idle.GMS2PlaybackSpeed = 0.3f;
        s_npc_Idarran_idle.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;

        UndertaleSprite s_npc_Idarran_work = Msl.GetSprite("s_npc_Idarran_work");
        s_npc_Idarran_work.CollisionMasks.RemoveAt(0);
        s_npc_Idarran_work.IsSpecialType = true;
        s_npc_Idarran_work.SVersion = 3;
        s_npc_Idarran_work.Width = 48;
        s_npc_Idarran_work.Height = 48;
        s_npc_Idarran_work.MarginLeft = 11;
        s_npc_Idarran_work.MarginRight = 33;
        s_npc_Idarran_work.MarginBottom = 36;
        s_npc_Idarran_work.MarginTop = 9;
        s_npc_Idarran_work.OriginX = 13;
        s_npc_Idarran_work.OriginY = 50;
        s_npc_Idarran_work.GMS2PlaybackSpeed = 0.3f;
        s_npc_Idarran_work.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;

        Msl.LoadGML("gml_RoomCC_r_BrynnUniversityCellar_34_Create")
            .MatchFrom("npc_sprite = s_npc_Student04uni_idle")
            .ReplaceBy("npc_sprite = s_npc_Idarran_idle")
            .Save();

        Msl.LoadGML("gml_RoomCC_r_BrynnUniversityCellar_33_Create")
            .MatchFrom("npc_sprite = s_npc_Student04uni_work")
            .ReplaceBy("npc_sprite = s_npc_Idarran_work\nis_rest = true")
            .Save();
    }
}