using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddCharacters()
    {
        AddPerk_ProfessionalWitcher();
        AddGeralt();
    }

    private void AddGeralt()
    {
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

        UndertaleSprite body = Msl.GetSprite("s_GeraltBody");
        body.OriginX = 22;
        body.OriginY = 34;
        body.CollisionMasks.RemoveAt(0);
        body.IsSpecialType = true;
        body.SVersion = 3;
        body.GMS2PlaybackSpeed = 1;
        body.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;

        UndertaleGameObject o_white_wolf = Msl.GetObject("o_white_wolf");

        o_white_wolf.ApplyEvent(
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
                            scr_atr_set_simple(""BodySprite"", sprite_get_name(s_GeraltBody))
                            with(scr_inventory_add_item(o_inv_witcher_medallion_wolf))
                            {
                                onStart_equipped = true
                                if (!equipped)
                                    event_user(5)
                            }
                            with (scr_equip(""Worn Cloak"", (1 << 0)))
                                scr_inv_atr_set(""Duration"", 100)
                            with (scr_equip(""Geralt Steel Sword"", (6 << 0)))
                                scr_inv_atr_set(""Duration"", 100)
                            with (scr_equip(""Fine Shirt"", (1 << 0)))
                                scr_inv_atr_set(""Duration"", 100)
                            with (scr_equip(""Apprentice Cowl"", (1 << 0)))
                                scr_inv_atr_set(""Duration"", 100)
                            with (scr_equip(""Peasant Shoes"", (1 << 0)))
                                scr_inv_atr_set(""Duration"", 100)
                            with (scr_inventory_add_weapon(""Training Crossbow"", (1 << 0)))
                                scr_inv_atr_set(""Duration"", 100);
                            with (scr_inventory_add_item(o_inv_moneybag))
                                scr_container_add_gold(250)
                            with (scr_inventory_add_weapon(""Peasant Pitchfork"", (1 << 0)))
                                scr_inv_atr_set(""Duration"", 20)
                            scr_inventory_add_item(o_inv_leafshaped_bolts)
                            scr_inventory_add_item(o_inv_dumpling)
                            scr_inventory_add_item(o_inv_wineskin)
                            scr_inventory_add_item(o_inv_splint)
                            scr_inventory_add_item(o_inv_splint)
                            scr_inventory_add_item(o_inv_rag)
                            scr_inventory_add_item(o_inv_salve)
                            scr_inventory_add_item(o_inv_salve)
                            scr_inventory_add_item(o_inv_lockpicks)
                            scr_inventory_add_item(o_inv_map_osbrook)
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
                medallion_turns = 60
            "),

            new MslEvent(eventType: EventType.Other, subtype: 12, code: @"
                event_inherited()

                if (--medallion_turns > 0)
                    exit;

                medallion_turns = 60

                with (o_inv_witcher_medallion_wolf)
                {
                    if (equipped)
                    {
                        event_user(14)

                        if (secret_room > 0)
                            with (other)
                                scr_random_speech(""perceiveSecretRoomGeralt"", 100)
                        else if (enemy_count >= 5)
                            with (other)
                                scr_random_speech(""perceiveMassEnemyGeralt"", 35)
                        else if (enemy_count >= 3)
                            with (other)
                                scr_random_speech(""perceiveMediumEnemyGeralt"", 35)
                        else if (enemy_count >= 1)
                            with (other)
                                scr_random_speech(""perceiveFewEnemyGeralt"", 35)
                        
                    }
                }
            ")
        );

        Msl.LoadGML("gml_Object_o_dataLoader_Other_10")
            .MatchFrom("global.player_class = ")
            .InsertAbove(@"scr_classCreate(
                o_white_wolf, s_Geralt, ""Geralt"", ""Male"", ""Human"", ""Aldor"", ""WhiteWolf"",
                10, 11, 10, 11, 11,
                [
                    global.swords_tier1, global.swords2h_tier1, global.daggers_tier1, global.bows_tier1, global.armor_tier1, global.athletics_tier1, global.combat_tier1,
                    [""Witcher"", o_skill_quen_sign_ico, o_skill_axii_sign_ico, o_skill_yrden_sign_ico, o_skill_aard_sign_ico, o_skill_igni_sign_ico, o_skill_trial_of_grasses]
                ],
                [o_perk_professional_witcher], (1 << 0), false)")
            .Save();

        AddCharacterLocalization(
            id: "Geralt",
            class_id: "WhiteWolf",
            char_name: "杰洛特",
            char_class: "白狼",
            char_desc: "猎魔人是人类术士们以天球交汇后出现的种种怪物为蓝本制造出来的杰作，" +
                "而作为狼学派三代弟子中最出色的一位，杰洛特被布洛克莱昂的林精们称为Gwynbleidd，" +
                "古语中意为白狼。他在北境与尼弗迦德战争中的种种经历不再赘述，然而这样一位传奇，" +
                "却可笑地死于农夫的草叉？##布拉维坎的屠夫，上古余血的养父，利维亚的杰洛特啊，" +
                "冥冥中的力量将你从死亡之中拯救而出，在奥尔多的土地上，你又会以怎样的故事取悦那双注视着你的眼睛呢。"
        );

        Msl.InjectTableSpeechesLocalization(
            new LocalizationSpeech(
                id: "perceiveFewEnemyGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "The medallion is vibrating."},
                    {ModLanguage.Chinese, "徽章有动静。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "My medallion is vibrating."},
                    {ModLanguage.Chinese, "我的微章在振动。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "The medallion is vibrating, stay alert."},
                    {ModLanguage.Chinese, "徽章在振动，警醒一点。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "The medallion is vibrating... A sorceress nearby?"},
                    {ModLanguage.Chinese, "徽章在振动，附近有女术士？"}
                }
            ),
            new LocalizationSpeech(
                id: "perceiveMediumEnemyGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "The medallion is vibrating strongly!"},
                    {ModLanguage.Chinese, "徽章震动的真厉害！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "The medallion senses enemies all around it!"},
                    {ModLanguage.Chinese, "徽章感知到周围有不少敌人！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "The medallion won't stop vibrating!"},
                    {ModLanguage.Chinese, "徽章跳个不停！"}
                }

            ),
            new LocalizationSpeech(
                id: "perceiveMassEnemyGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "The medallion is vibrating violently!"},
                    {ModLanguage.Chinese, "徽章剧烈地震动着！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "My medallion is vibrating violently."},
                    {ModLanguage.Chinese, "我的徽章剧烈地震动着！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "In danger! The medallion won't stop vibrating!"},
                    {ModLanguage.Chinese, "有危险！徽章跳个不停！"}
                }
            ),
            new LocalizationSpeech(
                id: "killBossGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Hmm, the medallion’s reacting."},
                    {ModLanguage.Chinese, "唔，徽章有动静。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "The magic is being absorbed."},
                    {ModLanguage.Chinese, "魔力正在被吸收。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Soul harvesting? The medallion never had that ability before."},
                    {ModLanguage.Chinese, "掠夺灵魂？这徽章以前可没这种能力。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Would love to have Yennefer take a look at this."},
                    {ModLanguage.Chinese, "真想让叶奈法拿去研究研究。"}
                }
            ),
            new LocalizationSpeech(
                id: "perceiveSecretRoomGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "There seems to be a hidden room nearby."},
                    {ModLanguage.Chinese, "附近好像有隐蔽的房间。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "I can sense a surge of magic emanating from within the walls."},
                    {ModLanguage.Chinese, "我感受到了墙壁中传来的魔力波动。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "There seems to be something behind the wall."},
                    {ModLanguage.Chinese, "墙壁后面好像有东西。"}
                }
            )
        );

        Msl.InjectTableDialogLocalization(
            // --- [CHARACTERS] BASIC LINES ---
            new LocalizationSentence(
                id: "custom_rent_room",
                sentence: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Do you have a room available for the night? ~lg~[allows to save the game]~/~" },
                    { ModLanguage.Chinese, "可有房间留宿？~lg~[可以保存游戏进度]~/~" }
                }
            )
            { Type = "geralt" },

            new LocalizationSentence(
                "custom_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Let’s talk about the latest news you have heard." },
                    { ModLanguage.Chinese, "聊聊你最近听说的事。" }
                }
            )
            { Type = "geralt" },

            new LocalizationSentence(
                "custom_trade",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Let’s do some trading." },
                    { ModLanguage.Chinese, "来做点买卖。" }
                }
            )
            { Type = "geralt" },

            new LocalizationSentence(
                "custom_leave",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Farewell." },
                    { ModLanguage.Chinese, "再会。" }
                }
            )
            { Type = "geralt" },

            new LocalizationSentence(
                "custom_back",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "*Nods in greeting*" },
                    { ModLanguage.Chinese, "*点头致意*" }
                }
            )
            { Type = "geralt" },

            new LocalizationSentence(
                "contractGet_pc",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Got any tricky business that needs taking care of?" },
                    { ModLanguage.Chinese, "有什么棘手的事要处理么？" }
                }
            )
            { Type = "geralt" },

            // --- Trader Skadia ---
            new LocalizationSentence(
                "greeting",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Welmy radowy striye." },
                    { ModLanguage.Chinese, "韦尔迷'拉多以'斯特莱耶。" }
                }
            )
            { Tags = "any", Role = "trader_skadia", Type = "geralt", Settlement = "Brynn" },

            new LocalizationSentence(
                "greeting",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Jak zmohu razpomosc?" },
                    { ModLanguage.Chinese, "亚克'兹莫乌'拉兹泼莫茨？" }
                }
            )
            { Tags = "any", Role = "trader_skadia", Type = "geralt", Settlement = "Brynn" },

            // --- Trader Nistra ---
            new LocalizationSentence(
                "greeting",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Emporia apodi Nistiria!" },
                    { ModLanguage.Chinese, "恩泼利亚'阿泼蒂'尼斯特利亚！" }
                }
            )
            { Tags = "any", Role = "trader_nistra", Type = "geralt", Settlement = "Brynn" },

            new LocalizationSentence(
                "greeting",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Nistrijeve dobro!" },
                    { ModLanguage.Chinese, "尼斯特里耶维'多布洛！" }
                }
            )
            { Tags = "any", Role = "trader_nistra", Type = "geralt", Settlement = "Brynn" },

            // --- Trader Jibey ---
            new LocalizationSentence(
                "greeting",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Nezi erzulu, arzeci?" },
                    { ModLanguage.Chinese, "涅齐'厄祖鲁，阿切斯？" }
                }
            )
            { Tags = "any", Role = "trader_jibey", Type = "geralt", Settlement = "Brynn" },

            // --- Trader Fjall ---
            new LocalizationSentence(
                "greeting",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Var vra Fjall! Skad ar hodt!" },
                    { ModLanguage.Chinese, "瓦尔'弗勒'弗约！斯加得'阿'霍特！" }
                }
            )
            { Tags = "any", Role = "trader_fjall", Type = "geralt", Settlement = "Brynn" },

            new LocalizationSentence(
                "greeting",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Har du tvagir?" },
                    { ModLanguage.Chinese, "哈尔'杜'特瓦基尔？" }
                }
            )
            { Tags = "any", Role = "trader_fjall", Type = "geralt", Settlement = "Brynn" }
        );


        Msl.InjectTableSpeechesLocalization(
            new LocalizationSpeech(
                id: "enemyGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Challenging a fully armed witcher...interesting."},
                    {ModLanguage.Chinese, "挑衅一位全副武装的猎魔人么...有趣"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Alright then, looks like you lot are tired of living..."},
                    {ModLanguage.Chinese, "好吧，看来你们活得不耐烦了..."}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "In the name of the Gwynbleidd!"},
                    {ModLanguage.Chinese, "以白狼之名！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Good time to stretch my muscles!"},
                    {ModLanguage.Chinese, "正好活动筋骨！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Since you’re so eager to spar with me..."},
                    {ModLanguage.Chinese, "既然你这么想和我练练手..."}
                }
            ),

            new LocalizationSpeech(
                id: "killGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Job’s done!"},
                    {ModLanguage.Chinese, "收工！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Hope the next one lasts a bit longer."},
                    {ModLanguage.Chinese, "希望下一个能多撑一会。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Next time, stay clear of witchers."},
                    {ModLanguage.Chinese, "下辈子，记得躲着猎魔人走。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "See? Even your blood admits I’m faster!"},
                    {ModLanguage.Chinese, "看，你的血都承认我更快！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "No harder than slaying a ghoul."},
                    {ModLanguage.Chinese, "不比杀一头孽鬼费劲多少。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Warm-up’s over!"},
                    {ModLanguage.Chinese, "热身完毕！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Don’t stain my medallion."},
                    {ModLanguage.Chinese, "别弄脏我的徽章。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Was that really necessary?"},
                    {ModLanguage.Chinese, "何必呢？"}
                }
            ),

            new LocalizationSpeech(
                id: "fatigueGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Damn... my bones are about to fall apart."},
                    {ModLanguage.Chinese, "该死...这身骨头快散架了。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Give me a bed now, and I could sleep till the next century."},
                    {ModLanguage.Chinese, "现在给我张床，我能睡到下个世纪。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "I need a vacation back at Kaer Morhen."},
                    {ModLanguage.Chinese, "我需要回凯尔莫罕休个假。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Even Vesemir’s training wasn’t this exhausting."},
                    {ModLanguage.Chinese, "当年在维瑟米尔手下训练都没这么累。"}
                }
            ),

            new LocalizationSpeech(
                id: "painInjuryGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Pain... proof that I’m still alive."},
                    {ModLanguage.Chinese, "疼痛……是我还活着的证明。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Damn it, my insides are all messed up..."},
                    {ModLanguage.Chinese, "该死，五脏六腑都在……"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Need a White Honey. No—make that two!"},
                    {ModLanguage.Chinese, "得来瓶白蜂蜜，不，两瓶！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "This pain... not nearly enough."},
                    {ModLanguage.Chinese, "这点疼痛，还不够……"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "My blood’s boiling..."},
                    {ModLanguage.Chinese, "血液在沸腾……"}
                }
            ),

            new LocalizationSpeech(
                id: "attackMagicBuffGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "In Aedd Gynvael, magic was never this stingy."},
                    {ModLanguage.Chinese, "在奥尔多，魔法可没那么吝啬。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Who says witchers only fight with swords!"},
                    {ModLanguage.Chinese, "谁说猎魔人只会用剑！"}
                }
            ),

            new LocalizationSpeech(
                id: "critGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Too slow!"},
                    {ModLanguage.Chinese, "太慢了！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Try dodging this!"},
                    {ModLanguage.Chinese, "试着躲躲我这一记！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Free lesson!"},
                    {ModLanguage.Chinese, "这招免费教学！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Lambert should’ve seen that!"},
                    {ModLanguage.Chinese, "真该让兰伯特观摩观摩！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Crippled!"},
                    {ModLanguage.Chinese, "残废！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Still not dead?!"},
                    {ModLanguage.Chinese, "这都没死透么？！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "On your knees!"},
                    {ModLanguage.Chinese, "跪下！"}
                }
            ),

            new LocalizationSpeech(
                id: "attackChargeGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Form a line, everyone—your turn will come!"},
                    {ModLanguage.Chinese, "排好队各位，按顺序上路！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Come on, let’s see what you’ve got!"},
                    {ModLanguage.Chinese, "来啊，让你们见识下！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Looks like another fine day... for a hot bath afterward."},
                    {ModLanguage.Chinese, "看来今天…又是个适合洗热水澡的日子。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Time to loosen up!"},
                    {ModLanguage.Chinese, "是该活动活动了！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "I’m done holding back!"},
                    {ModLanguage.Chinese, "我要动真格了！"}
                }
            ),

            new LocalizationSpeech(
                id: "injuryGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "You’ll never break a witcher..."},
                    {ModLanguage.Chinese, "别妄想击垮一个猎魔人……"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Not even Vilgefortz hit that hard..."},
                    {ModLanguage.Chinese, "还不如威戈弗特兹敲我的几棍子……"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Heh... walked right into that one."},
                    {ModLanguage.Chinese, "呵……是我着了道"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Dumber than getting skewered by a pitchfork..."},
                    {ModLanguage.Chinese, "这比被草叉捅死还要蠢……"}
                }
            ),

            new LocalizationSpeech(
                id: "maneuverGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Flawless!"},
                    {ModLanguage.Chinese, "无懈可击！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Defend, just for a moment!"},
                    {ModLanguage.Chinese, "稍作防御！"}
                }
            ),

            new LocalizationSpeech(
                id: "counterCritGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Block... and strike!"},
                    {ModLanguage.Chinese, "守……转攻！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Stop struggling."},
                    {ModLanguage.Chinese, "别再挣扎了。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Too slow, too sloppy!"},
                    {ModLanguage.Chinese, "动作太慢，准头太差！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "That hit’s weaker than a drowner’s swipe!"},
                    {ModLanguage.Chinese, "这力度，尚不如一头水鬼！"}
                }
            ),

            new LocalizationSpeech(
                id: "killCrit_Geralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Pathetic!"},
                    {ModLanguage.Chinese, "不堪一击！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Next!"},
                    {ModLanguage.Chinese, "再来几个！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Leave none alive!"},
                    {ModLanguage.Chinese, "一个不留！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Next life, pick an easier target!"},
                    {ModLanguage.Chinese, "下辈子挑个软柿子捏！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Lesson learned? Shame about the cost..."},
                    {ModLanguage.Chinese, "学到教训了么，至于代价……"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "That’s what you get for provoking a witcher!"},
                    {ModLanguage.Chinese, "挑衅猎魔人的下场！"}
                }
            ),

            new LocalizationSpeech(
                id: "killCritShot_Geralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "One shot... straight through the heart!"},
                    {ModLanguage.Chinese, "一箭……穿心！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Bullseye!"},
                    {ModLanguage.Chinese, "正中目标！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Lesson learned? Shame about the cost..."},
                    {ModLanguage.Chinese, "学到教训了么，至于代价……"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "That’s what you get for provoking a witcher!"},
                    {ModLanguage.Chinese, "挑衅猎魔人的下场！"}
                }
            ),

            new LocalizationSpeech(
                id: "useWitcherPotion",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Huh, this brew will do... barely."},
                    {ModLanguage.Chinese, "呼，这个配方勉强够用。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Could use another bottle."},
                    {ModLanguage.Chinese, "应该还能再来一瓶。"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Good effect."},
                    {ModLanguage.Chinese, "效果不错！"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "All set. Time to begin."},
                    {ModLanguage.Chinese, "准备充分，可以开始了。"}
                }
            ),

            new LocalizationSpeech(
                id: "useTrapGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Hope this actually works..."},
                    {ModLanguage.Chinese, "希望能有点作用……"}
                },
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Witchers rarely rely on these..."},
                    {ModLanguage.Chinese, "猎魔人很少用这些……"}
                }
            ),

            new LocalizationSpeech(
                id: "prayGeralt",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Will the Holy One bless even a stranger like me..."},
                    {ModLanguage.Chinese, "圣主也会庇佑我等异乡的客人么……"}
                }
            ),

            new LocalizationSpeech(
                id: "prayGeraltCD",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "Generous Holy One, my thanks for Your protection."},
                    {ModLanguage.Chinese, "慷慨的圣主，感谢您的庇佑。"}
                }
            ),

            new LocalizationSpeech(
                id: "prayGeraltDwarfAltar",
                new Dictionary<ModLanguage, string> {
                    {ModLanguage.English, "A magical fluctuation? Hm... can’t absorb it."},
                    {ModLanguage.Chinese, "魔力的波动？唔，没法吸收。"}
                }
            )
        );

        InjectItemsToTable(
            table: "gml_GlobalScript_table_text",
            anchor: "examineKingStatue_Mahir",
            defaultKey: 2,
            new Dictionary<int, string>
            {
                [0] = "examineKingStatue_Geralt",
                [2] = "There are plenty of statues like this back in the North... The difference is, most of those kings are still alive — their heads have just been empty for years.",
                [3] = "北境也有很多这样的雕像…… 不同的是，那些国王大多还活着，只是脑袋早就空了。"
            }
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