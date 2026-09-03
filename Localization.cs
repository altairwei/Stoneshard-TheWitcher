
using ModShardLauncher;
using ModShardLauncher.Mods;

namespace TheWitcher;

public class Localization
{
    public static void AddLocalizationAll()
    {
        AddSkillTexts();
        AddItemTexts();
        AddGeraltTexts();
        AddIdarranTexts();
    }

    private static void AddSkillTexts()
    {
        TableUtils.LocalizationTable("gml_GlobalScript_table_skills")
            .MatchFrom("skill_name_end;")
            .InsertAbove(
                new Loc("Witcher_Alchemy")
                .English("The Witcher's Alchemy")
                .Chinese("猎魔人炼金术")
            )
            .MatchFrom("skill_desc_end;")
            .InsertAbove(
                new Loc("Witcher_Alchemy")
                .English(@"Opens the menu for ~w~crafting weapon coating oil~/~.")
                .Chinese(@"能够打开~w~猎魔人炼金术~/~界面，学会制作~w~剑油~/~、~w~魔药~/~以及~w~煎药~/~。")
            )
            .Save();
    }

    private static void AddItemTexts()
    {
        TableUtils.LocalizationTable("gml_GlobalScript_table_text")
            .MatchFrom("crafting_category_end;")
            .InsertAbove(
                new Loc("weapon_oil")
                .English("Weapon Oil")
                .Chinese("剑油"),

                new Loc("witcher_potion")
                .English("Potion")
                .Chinese("魔药"),

                new Loc("witcher_decoction")
                .English("Decoction")
                .Chinese("煎药")
            )
            .Save();

        TableUtils.InjectItemsToTable(
            table: "gml_GlobalScript_table_items",
            anchor: "workbench;Верстак;Workbench;",
            defaultKey: 2,
            new Dictionary<int, string>
            {
                [0] = "alchemy_workbench",
                [2] = "Alchemy Station",
                [3] = "炼金台"
            }
        );

        TableUtils.LocalizationTable("gml_GlobalScript_table_items")
            .MatchFrom("consum_name_end;")
            .InsertAbove(
                new Loc("alcohol_essentia").English("Alcohol Essentia").Chinese("醇素")
            )
            .MatchFrom("consum_mid_end;")
            .InsertAbove(
                new Loc("alcohol_essentia")
                .English("The ~lg~quintessential spirit~/~, coaxed from wine and ale. Highly concentrated and volatile, ~r~not for casual consumption~/~.")
                .Chinese("从葡萄酒与麦酒中诱出的~lg~精髓之灵~/~。浓度极高，性质躁动，~r~不宜直接饮用~/~。")
            )
            .MatchFrom("consum_desc_end;")
            .InsertAbove(
                new Loc("alcohol_essentia")
                .English("A bottle of high-proof alcohol, serving as the fundamental solvent and base for countless alchemical concoctions.")
                .Chinese("一瓶高度酒精，作为无数炼金配方中不可或缺的基底与溶剂。")
            )
            .Save();
    }

    private static void AddGeraltTexts()
    {
        TableUtils.InjectItemsToTable(
            table: "gml_GlobalScript_table_text",
            anchor: ";char_name;char_name;",
            defaultKey: 2,
            new Dictionary<int, string>
            {
                [0] = "Geralt",
                [2] = "Geralt",
                [3] = "杰洛特"
            }
        );

        TableUtils.InjectItemsToTable(
            table: "gml_GlobalScript_table_text",
            anchor: ";class_name;class_name;",
            defaultKey: 2,
            new Dictionary<int, string>
            {
                [0] = "WhiteWolf",
                [2] = "White Wolf",
                [3] = "白狼"
            }
        );

        TableUtils.InjectItemsToTable(
            table: "gml_GlobalScript_table_text",
            anchor: ";char_desc;char_desc;",
            defaultKey: 2,
            new Dictionary<int, string>
            {
                [0] = "Geralt",
                [2] = "The witchers were the masterpiece forged by human sorcerers after the Conjunction of the Spheres, modeled upon the very monsters that invaded their world. " +
                      "Among the third generation of the Wolf School, none surpassed Geralt — called Gwynbleidd, the White Wolf, by the dryads of Brokilon. " +
                      "His deeds during the wars between the Northern Kingdoms and Nilfgaard need no retelling, such tales have long passed into legend. " +
                      "And yet, this legend met his end on a peasant’s pitchfork — a cruel jest of fate.##" +
                      "Butcher of Blaviken, guardian of Elder Blood, Geralt of Rivia... some unseen force has drawn you back from death itself. " +
                      "Here, upon the lands of Aedalan, what new tale will you spin to amuse the eyes that watch you still?",
                [3] = "猎魔人是人类术士们以天球交汇后出现的种种怪物为蓝本制造出来的杰作，" +
                      "而作为狼学派三代弟子中最出色的一位，杰洛特被布洛克莱昂的林精们称为Gwynbleidd，" +
                      "古语中意为白狼。他在北境与尼弗迦德战争中的种种经历不再赘述，然而这样一位传奇，" +
                      "却可笑地死于农夫的草叉？##布拉维坎的屠夫，上古余血的养父，利维亚的杰洛特啊，" +
                      "冥冥中的力量将你从死亡之中拯救而出，在奥尔多的土地上，你又会以怎样的故事取悦那双注视着你的眼睛呢。"
            }
        );

        TableUtils.LocalizationTable("gml_GlobalScript_table_speech")
            .MatchFrom("FORBIDDEN MAGIC;")
            .InsertAbove(
                new Loc("perceiveFewEnemyGeralt"),
                new Loc("").English("The medallion is vibrating.").Chinese("徽章有动静。"),
                new Loc("").English("My medallion is vibrating.").Chinese("我的微章在振动。"),
                new Loc("").English("The medallion is vibrating, stay alert.").Chinese("徽章在振动，警醒一点。"),
                new Loc("").English("The medallion is vibrating... A sorceress nearby?").Chinese("徽章在振动，附近有女术士？"),
                new Loc("perceiveFewEnemyGeralt_end"),

                new Loc("perceiveMediumEnemyGeralt"),
                new Loc("").English("The medallion is vibrating strongly!").Chinese("徽章震动的真厉害！"),
                new Loc("").English("The medallion senses enemies all around it!").Chinese("徽章感知到周围有不少敌人！"),
                new Loc("").English("The medallion won't stop vibrating!").Chinese("徽章跳个不停！"),
                new Loc("perceiveMediumEnemyGeralt_end"),

                new Loc("perceiveMassEnemyGeralt"),
                new Loc("").English("The medallion is vibrating violently!").Chinese("徽章剧烈地震动着！"),
                new Loc("").English("My medallion is vibrating violently.").Chinese("我的徽章剧烈地震动着！"),
                new Loc("").English("In danger! The medallion won't stop vibrating!").Chinese("有危险！徽章跳个不停！"),
                new Loc("perceiveMassEnemyGeralt_end"),

                new Loc("killBossGeralt"),
                new Loc("").English("Hmm, the medallion’s reacting.").Chinese("唔，徽章有动静。"),
                new Loc("").English("The magic is being absorbed.").Chinese("魔力正在被吸收。"),
                new Loc("").English("Soul harvesting? The medallion never had that ability before.").Chinese("掠夺灵魂？这徽章以前可没这种能力。"),
                new Loc("").English("Would love to have Yennefer take a look at this.").Chinese("真想让叶奈法拿去研究研究。"),
                new Loc("killBossGeralt_end"),

                new Loc("perceiveSecretRoomGeralt"),
                new Loc("").English("There seems to be a hidden room nearby.").Chinese("附近好像有隐蔽的房间。"),
                new Loc("").English("I can sense a surge of magic emanating from within the walls.").Chinese("我感受到了墙壁中传来的魔力波动。"),
                new Loc("").English("There seems to be something behind the wall.").Chinese("墙壁后面好像有东西。"),
                new Loc("perceiveSecretRoomGeralt_end")
            )
            .Save();

        /*TODO 去布林，让杰洛特与不会奥尔多语的精灵对话看看,如果没问题则不修改
        TableUtils.ModifyItemsInTable(
            table: "gml_GlobalScript_table_lines",
            match: "greeting;any;elf_guard, elf_woman;",
            new Dictionary<string, string>
            {
                ["Type"] = "arna, jonna, dirwin, velmir, leosthenes, jorgrim, hilda, geralt",
            }
        );
        
        TableUtils.ModifyItemsInTable(
            table: "gml_GlobalScript_table_lines",
            match: "greeting;any;elf_noble;",
            new Dictionary<string, string>
            {
                ["Type"] = "arna, jonna, dirwin, velmir, leosthenes, jorgrim, hilda, geralt",
            }
        );

        TableUtils.ModifyItemsInTable(
            table: "gml_GlobalScript_table_lines",
            match: "greeting;any;elf_noble, elf_guard;arna, jonna",
            new Dictionary<string, string>
            {
                ["Type"] = "arna, jonna, dirwin, velmir, leosthenes, jorgrim, hilda, geralt",
            }
        );
        */

        TableUtils.LocalizationTable("gml_GlobalScript_table_lines")
            .PrefixDefault("any")
            .MatchFrom("[NPC] GREETINGS;")
            .InsertBelow(
                new Loc("custom_rent_room")
                    .Set("Type", "geralt")
                    .English("Do you have a room available for the night? ~lg~[allows to save the game]~/~")
                    .Chinese("可有房间留宿？~lg~[可以保存游戏进度]~/~"),
                new Loc("custom_chat")
                    .Set("Type", "geralt")
                    .English("Let’s talk about the latest news you have heard.")
                    .Chinese("聊聊你最近听说的事。"),
                new Loc("custom_trade")
                    .Set("Type", "geralt")
                    .English("Let’s do some trading.")
                    .Chinese("来做点买卖。"),
                new Loc("custom_leave")
                    .Set("Type", "geralt")
                    .English("Farewell.")
                    .Chinese("再会。"),
                new Loc("custom_back")
                    .Set("Type", "geralt")
                    .English("*Nods in greeting*")
                    .Chinese("*点头致意*"),
                new Loc("contractGet_pc")
                    .Set("Type", "geralt")
                    .English("Got any tricky business that needs taking care of?")
                    .Chinese("有什么棘手的事要处理么？"),

                new Loc("greeting")
                    .Set("Tags", "any")
                    .Set("Role", "trader_skadia")
                    .Set("Type", "geralt")
                    .Set("Settlement", "Brynn")
                    .English("Welmy radowy striye.")
                    .Chinese("韦尔迷'拉多以'斯特莱耶。"),
                new Loc("greeting")
                    .Set("Tags", "any")
                    .Set("Role", "trader_skadia")
                    .Set("Type", "geralt")
                    .Set("Settlement", "Brynn")
                    .English("Jak zmohu razpomosc?")
                    .Chinese("亚克'兹莫乌'拉兹泼莫茨？"),
                new Loc("greeting")
                    .Set("Tags", "any")
                    .Set("Role", "trader_nistra")
                    .Set("Type", "geralt")
                    .Set("Settlement", "Brynn")
                    .English("Emporia apodi Nistiria!")
                    .Chinese("恩泼利亚'阿泼蒂'尼斯特利亚！"),
                new Loc("greeting")
                    .Set("Tags", "any")
                    .Set("Role", "trader_nistra")
                    .Set("Type", "geralt")
                    .Set("Settlement", "Brynn")
                    .English("Nistrijeve dobro!")
                    .Chinese("尼斯特里耶维'多布洛！"),
                new Loc("greeting")
                    .Set("Tags", "any")
                    .Set("Role", "trader_jibey")
                    .Set("Type", "geralt")
                    .Set("Settlement", "Brynn")
                    .English("Nezi erzulu, arzeci?")
                    .Chinese("涅齐'厄祖鲁，阿切斯？"),
                new Loc("greeting")
                    .Set("Tags", "any")
                    .Set("Role", "elf_guard, elf_woman")
                    .Set("Type", "geralt")
                    .Set("Settlement", "Brynn")
                    .English("Nezi erzulu, arzeci?")
                    .Chinese("涅齐'厄祖鲁，阿切斯？"),
                new Loc("greeting")
                    .Set("Tags", "any")
                    .Set("Role", "trader_fjall")
                    .Set("Type", "geralt")
                    .Set("Settlement", "Brynn")
                    .English("Var vra Fjall! Skad ar hodt!")
                    .Chinese("瓦尔'弗勒'弗约！斯加得'阿'霍特！"),
                new Loc("greeting")
                    .Set("Tags", "any")
                    .Set("Role", "trader_fjall")
                    .Set("Type", "geralt")
                    .Set("Settlement", "Brynn")
                    .English("Har du tvagir?")
                    .Chinese("哈尔'杜'特瓦基尔？")
            )
            .Save();

        TableUtils.LocalizationTable("gml_GlobalScript_table_speech")
            .MatchFrom("FORBIDDEN MAGIC;")
            .InsertAbove(
                new Loc("enemyGeralt"),
                new Loc("")
                    .English("Challenging a fully armed witcher...interesting.")
                    .Chinese("挑衅一位全副武装的猎魔人么...有趣"),
                new Loc("")
                    .English("Alright then, looks like you lot are tired of living...")
                    .Chinese("好吧，看来你们活得不耐烦了..."),
                new Loc("")
                    .English("In the name of the Gwynbleidd!")
                    .Chinese("以白狼之名！"),
                new Loc("")
                    .English("Good time to stretch my muscles!")
                    .Chinese("正好活动筋骨！"),
                new Loc("")
                    .English("Since you’re so eager to spar with me...")
                    .Chinese("既然你这么想和我练练手..."),
                new Loc("enemyGeralt_end"),

                new Loc("killGeralt"),
                new Loc("")
                    .English("Job’s done!")
                    .Chinese("收工！"),
                new Loc("")
                    .English("Hope the next one lasts a bit longer.")
                    .Chinese("希望下一个能多撑一会。"),
                new Loc("")
                    .English("Next time, stay clear of witchers.")
                    .Chinese("下辈子，记得躲着猎魔人走。"),
                new Loc("")
                    .English("See? Even your blood admits I’m faster!")
                    .Chinese("看，你的血都承认我更快！"),
                new Loc("")
                    .English("No harder than slaying a ghoul.")
                    .Chinese("不比杀一头孽鬼费劲多少。"),
                new Loc("")
                    .English("Warm-up’s over!")
                    .Chinese("热身完毕！"),
                new Loc("")
                    .English("Don’t stain my medallion.")
                    .Chinese("别弄脏我的徽章。"),
                new Loc("")
                    .English("Was that really necessary?")
                    .Chinese("何必呢？"),
                new Loc("killGeralt_end"),

                new Loc("fatigueGeralt"),
                new Loc("")
                    .English("Damn... my bones are about to fall apart.")
                    .Chinese("该死...这身骨头快散架了。"),
                new Loc("")
                    .English("Give me a bed now, and I could sleep till the next century.")
                    .Chinese("现在给我张床，我能睡到下个世纪。"),
                new Loc("")
                    .English("I need a vacation back at Kaer Morhen.")
                    .Chinese("我需要回凯尔莫罕休个假。"),
                new Loc("")
                    .English("Even Vesemir’s training wasn’t this exhausting.")
                    .Chinese("当年在维瑟米尔手下训练都没这么累。"),
                new Loc("fatigueGeralt_end"),

                new Loc("painInjuryGeralt"),
                new Loc("")
                    .English("Pain... proof that I’m still alive.")
                    .Chinese("疼痛……是我还活着的证明。"),
                new Loc("")
                    .English("Damn it, my insides are all messed up...")
                    .Chinese("该死，五脏六腑都在……"),
                new Loc("")
                    .English("Need a White Honey. No—make that two!")
                    .Chinese("得来瓶白蜂蜜，不，两瓶！"),
                new Loc("")
                    .English("This pain... not nearly enough.")
                    .Chinese("这点疼痛，还不够……"),
                new Loc("")
                    .English("My blood’s boiling...")
                    .Chinese("血液在沸腾……"),
                new Loc("painInjuryGeralt_end"),

                new Loc("attackMagicBuffGeralt"),
                new Loc("")
                    .English("In Aedd Gynvael, magic was never this stingy.")
                    .Chinese("在奥尔多，魔法可没那么吝啬。"),
                new Loc("")
                    .English("Who says witchers only fight with swords!")
                    .Chinese("谁说猎魔人只会用剑！"),
                new Loc("attackMagicBuffGeralt_end"),

                new Loc("critGeralt"),
                new Loc("")
                    .English("Too slow!")
                    .Chinese("太慢了！"),
                new Loc("")
                    .English("Try dodging this!")
                    .Chinese("试着躲躲我这一记！"),
                new Loc("")
                    .English("Free lesson!")
                    .Chinese("这招免费教学！"),
                new Loc("")
                    .English("Lambert should’ve seen that!")
                    .Chinese("真该让兰伯特观摩观摩！"),
                new Loc("")
                    .English("Crippled!")
                    .Chinese("残废！"),
                new Loc("")
                    .English("Still not dead?!")
                    .Chinese("这都没死透么？！"),
                new Loc("")
                    .English("On your knees!")
                    .Chinese("跪下！"),
                new Loc("critGeralt_end"),

                new Loc("attackChargeGeralt"),
                new Loc("")
                    .English("Form a line, everyone—your turn will come!")
                    .Chinese("排好队各位，按顺序上路！"),
                new Loc("")
                    .English("Come on, let’s see what you’ve got!")
                    .Chinese("来啊，让你们见识下！"),
                new Loc("")
                    .English("Looks like another fine day... for a hot bath afterward.")
                    .Chinese("看来今天…又是个适合洗热水澡的日子。"),
                new Loc("")
                    .English("Time to loosen up!")
                    .Chinese("是该活动活动了！"),
                new Loc("")
                    .English("I’m done holding back!")
                    .Chinese("我要动真格了！"),
                new Loc("attackChargeGeralt_end"),

                new Loc("injuryGeralt"),
                new Loc("")
                    .English("You’ll never break a witcher...")
                    .Chinese("别妄想击垮一个猎魔人……"),
                new Loc("")
                    .English("Not even Vilgefortz hit that hard...")
                    .Chinese("还不如威戈弗特兹敲我的几棍子……"),
                new Loc("")
                    .English("Heh... walked right into that one.")
                    .Chinese("呵……是我着了道"),
                new Loc("")
                    .English("Dumber than getting skewered by a pitchfork...")
                    .Chinese("这比被草叉捅死还要蠢……"),
                new Loc("injuryGeralt_end"),

                new Loc("maneuverGeralt"),
                new Loc("")
                    .English("Flawless!")
                    .Chinese("无懈可击！"),
                new Loc("")
                    .English("Defend, just for a moment!")
                    .Chinese("稍作防御！"),
                new Loc("maneuverGeralt_end"),

                new Loc("counterCritGeralt"),
                new Loc("")
                    .English("Block... and strike!")
                    .Chinese("守……转攻！"),
                new Loc("")
                    .English("Stop struggling.")
                    .Chinese("别再挣扎了。"),
                new Loc("")
                    .English("Too slow, too sloppy!")
                    .Chinese("动作太慢，准头太差！"),
                new Loc("")
                    .English("That hit’s weaker than a drowner’s swipe!")
                    .Chinese("这力度，尚不如一头水鬼！"),
                new Loc("counterCritGeralt_end"),

                new Loc("killCrit_Geralt"),
                new Loc("")
                    .English("Pathetic!")
                    .Chinese("不堪一击！"),
                new Loc("")
                    .English("Next!")
                    .Chinese("再来几个！"),
                new Loc("")
                    .English("Leave none alive!")
                    .Chinese("一个不留！"),
                new Loc("")
                    .English("Next life, pick an easier target!")
                    .Chinese("下辈子挑个软柿子捏！"),
                new Loc("")
                    .English("Lesson learned? Shame about the cost...")
                    .Chinese("学到教训了么，至于代价……"),
                new Loc("")
                    .English("That’s what you get for provoking a witcher!")
                    .Chinese("挑衅猎魔人的下场！"),
                new Loc("killCrit_Geralt_end"),

                new Loc("killCritShot_Geralt"),
                new Loc("")
                    .English("One shot... straight through the heart!")
                    .Chinese("一箭……穿心！"),
                new Loc("")
                    .English("Bullseye!")
                    .Chinese("正中目标！"),
                new Loc("")
                    .English("Lesson learned? Shame about the cost...")
                    .Chinese("学到教训了么，至于代价……"),
                new Loc("")
                    .English("That’s what you get for provoking a witcher!")
                    .Chinese("挑衅猎魔人的下场！"),
                new Loc("killCritShot_Geralt_end"),

                new Loc("useWitcherPotion"),
                new Loc("")
                    .English("Huh, this brew will do... barely.")
                    .Chinese("呼，这个配方勉强够用。"),
                new Loc("")
                    .English("Could use another bottle.")
                    .Chinese("应该还能再来一瓶。"),
                new Loc("")
                    .English("Good effect.")
                    .Chinese("效果不错！"),
                new Loc("")
                    .English("All set. Time to begin.")
                    .Chinese("准备充分，可以开始了。"),
                new Loc("useWitcherPotion_end"),

                new Loc("useTrapGeralt"),
                new Loc("")
                    .English("Hope this actually works...")
                    .Chinese("希望能有点作用……"),
                new Loc("")
                    .English("Witchers rarely rely on these...")
                    .Chinese("猎魔人很少用这些……"),
                new Loc("useTrapGeralt_end"),

                new Loc("prayGeralt"),
                new Loc("")
                    .English("Will the Holy One bless even a stranger like me...")
                    .Chinese("圣主也会庇佑我等异乡的客人么……"),
                new Loc("prayGeralt_end"),

                new Loc("prayGeraltCD"),
                new Loc("")
                    .English("Generous Holy One, my thanks for Your protection.")
                    .Chinese("慷慨的圣主，感谢您的庇佑。"),
                new Loc("prayGeraltCD_end"),

                new Loc("prayGeraltDwarfAltar"),
                new Loc("")
                    .English("A magical fluctuation? Hm... can’t absorb it.")
                    .Chinese("魔力的波动？唔，没法吸收。"),
                new Loc("prayGeraltDwarfAltar_end")
            )
            .Save();

        TableUtils.InjectItemsToTable(
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

    private static void AddIdarranTexts()
    {
        TableUtils.InjectItemsToTable(
            table: "gml_GlobalScript_table_names",
            anchor: "NPC_info;NPC_info;NPC_info;",
            defaultKey: 2,
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

        TableUtils.LocalizationTable("gml_GlobalScript_table_lines")
            .PrefixDefault("any")
            .MatchFrom("[NPC] GREETINGS;")
            .InsertBelow(
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("杰洛特，又在和命运讨价还价？")
                    .English("Geralt, still bargaining with fate, I see."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("今天实验顺利，你要不要也做个对照组？")
                    .English("Today’s experiment went well. Care to be my control sample?"),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .Chinese("你的徽章还在震动吗？也许是它在想我。")
                    .English("Your medallion still vibrating? Perhaps it remembers me."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .Chinese("你看起来比昨天更人类一点，真可惜。")
                    .English("You look a bit more human today... pity."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .Chinese("别担心，我暂时不需要新的猎魔人样本。")
                    .English("Relax, I’m not in need of a new witcher sample—yet."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .Chinese("你看到了么？完美的生命正在我的手中重塑。")
                    .English("Do you see it? Perfection itself, reshaped by my hands."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .Chinese("阿尔祖曾说：‘造物的界限，只是怯懦者的借口。’")
                    .English("Alzur once said: ‘The limits of creation are excuses for the timid.’"),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .Chinese("猎魔人？啊，是我的半成品。")
                    .English("A witcher? Ah, one of my unfinished prototypes."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .Chinese("科西莫用符号写下秩序，而我用血液写下进化。")
                    .English("Cosimo wrote order in sigils, I wrote evolution in blood."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .Chinese("我并非创造怪物，我只是让真相剥离伪装。")
                    .English("I do not create monsters. I simply peel away the illusion of humanity."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("我很欣赏你那种被逼出来的理智。它和疯狂只差一滴突变液。")
                    .English("I admire that forced composure of yours. It’s just one drop of mutagen away from madness."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("见到你总让我想起一个问题：进化和退化，究竟谁在赢？")
                    .English("Seeing you always reminds me of a question—evolution or regression, which one’s winning?"),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("有趣，你今天闻起来不像沼泽或血。新洗的甲胄？")
                    .English("Interesting, you don’t smell of swamps or blood today. Fresh armor, perhaps?"),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("欢迎。请别乱碰那边的瓶子，除非你想提前成为样本。")
                    .English("Welcome. Don’t touch the flasks unless you wish to become part of the study early."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("很好，我正缺少对照组。")
                    .English("Excellent, I was missing a control group."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("放松，我只在实验阶段需要尸体。")
                    .English("Relax. I only require corpses during the testing phase."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("你的眼神像是见过混沌的人。我喜欢这样的样本。")
                    .English("Those eyes... they’ve seen Chaos. I like that kind of specimen."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("奥尔多的人大多无知，而你至少有求知的气味。")
                    .English("Most in Aldor reek of ignorance. You, at least, smell of curiosity."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("你带来了消息，金币，还是新的生物组织？")
                    .English("Have you brought news, gold, or biological tissue?"),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("希望你是来谈话的，而不是来燃烧我的研究。")
                    .English("I trust you’re here to talk, not to torch my research."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("……能安静点吗？知识不喜欢被噪音打断。")
                    .English("...Could you be quiet? Knowledge dislikes being interrupted by noise."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("我在读书，不是开会。##如果你没带新发现，那就让文字继续说话。")
                    .English("I’m reading, not hosting a conference.##Unless you bring new discoveries, let the words keep speaking instead."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("这本书讲的是痛觉传导……要不要我现场演示？")
                    .English("This volume is about pain conduction... care for a live demonstration?"),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("思考是一种炼金术，打扰它就像掺错了催化剂。")
                    .English("Thought is alchemy—disturbing it is like adding the wrong catalyst."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("除非你能比这页脚注更有趣，否则别开口。")
                    .English("Unless you’re more interesting than this footnote, don’t speak."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("我已经记下你的脚步频率，别让我在报告里写‘干扰因素’。")
                    .English("I’ve memorized your step rhythm. Don’t make me file it under ‘experimental interference’."),
                new Loc("greeting_idarran")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .Chinese("……公式对不上，变量太多。或者，是我太人类了。")
                    .English("...The formula doesn’t balance. Too many variables. Or perhaps I’m still too human."),

                new Loc("idarran_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .English("You call it Aldor. I call it an error—an alchemical byproduct of time itself.##" +
                    "I was attempting interdimensional genome recombination... and then, the door opened.##" +
                    "The magic here is unlike our Chaos—purer, yet more feral.")
                    .Chinese("你称它为‘奥尔多’，而我称它为‘误差’——一次时空炼金的副产物。##" +
                    "我当时正在尝试跨维基因重组实验……然后，门开了。##" +
                    "这里的魔力结构与你我熟知的混沌截然不同——更纯净，也更野蛮。"),
                new Loc("idarran_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("I’ve studied a native creature called the Gulon—its cells devour mana itself. Beautiful, isn’t it?##" +
                    "This world made me believe Chaos isn’t a flaw—it’s the embryo of higher order.")
                    .Chinese("我研究了一种本地生物，名为‘谷隆’，它的细胞能主动吞噬法力。很美妙，不是吗？##" +
                    "这个世界让我重新相信，混乱并非错误，而是更高秩序的雏形。"),
                new Loc("idarran_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .English("Still hunting my children, are you? The ones your world called monsters.##" +
                    "Ironic, isn’t it? Here they worship me as a god, and you as a beast.##" +
                    "Witchers were our most perfect design—you lot just refuse to admit it.##" +
                    "Ever get the feeling that perhaps humanity was the real monster all along?")
                    .Chinese("你依然在狩猎我的孩子们么？那些曾在你的世界被称作怪物的生命。##" +
                    "讽刺吧？这个世界把我视作神，而把你当成野兽。##" +
                    "猎魔人……其实是我们最完美的作品，只是你们自己不愿承认。##" +
                    "你有过这样的错觉么？也许‘人类’才是异界中真正的怪物。"),
                new Loc("idarran_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .English("If Alzur could see my work now, he’d either smile... or frown deeply.##" +
                    "Cosimo... that old man would rather die in an equation than live in a miracle.##" +
                    "They thought I sought immortality. I simply despised endings.")
                    .Chinese("如果阿尔祖尔现在能看到我的工作，他要么会微笑……要么会深深皱眉。##" +
                    "科西莫……那个老头宁愿死在方程式中，也不愿活在奇迹里。##" +
                    "他们以为我追求永生。我只是厌恶终结。"),
                new Loc("idarran_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "geralt")
                    .English("You call yourself human, yet deny the blood that made you something else.##" +
                    "We’re both ghosts of creation, Geralt—only I create, and you destroy.##" +
                    "I don’t hate you. In truth, I’ve always considered you our proudest failure.##" +
                    "Funny thing—no matter the world, witchers remain a lonely species.")
                    .Chinese("你称自己为人，却拒绝承认自己的血液早已与人类不同。##" +
                    "我们都是造物的幽灵，杰洛特——只不过我还在造，而你在毁。##" +
                    "我不憎恨你。事实上，我一直把你视为我们最骄傲的失败。##" +
                    "有趣的是，无论在哪个世界，猎魔人总是孤独的物种。"),
                new Loc("idarran_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("The beauty of life lies in its endless attempts to correct itself. I merely help it hurry along.##" +
                    "In Aldor, I’ve seen fish that breathe air, bones that sing, and metals that heal themselves. Nature is never short of imagination.##" +
                    "My failure rate is low. Most so-called failures simply didn’t have time to finish evolving.##" +
                    "They call my work a blasphemy of life. They forget—creation itself was the first blasphemy.")
                    .Chinese("生命的美妙在于它不断试图自我修正，而我，只是帮它快一点。##" +
                    "在奥尔多，我见过能在体外呼吸的鱼、会唱歌的骨头，还有自愈的铁。自然从不缺乏想象力。##" +
                    "我的实验失败率很低，大多数‘失败’只是没来得及完成。##" +
                    "有人说我在亵渎生命，可他们忘了——创造本身就是最古老的亵渎。"),
                new Loc("idarran_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("People fear the unknown, though the unknown simply can’t be bothered to introduce itself.##" +
                    "Ignorance brings happiness... but happiness corrodes the nervous system.##" +
                    "Sanity is a fragile potion—the more you use it, the less it works.##" +
                    "After studying hundreds of species, I found one universal truth—stupidity survives evolution.")
                    .Chinese("人们总害怕未知，其实未知只是懒得自我介绍。##" +
                    "无知能带来幸福，但幸福会腐蚀神经系统。##" +
                    "理智是脆弱的药剂，用多了就失效。##" +
                    "我研究了几百个物种，唯一恒定的结论是：愚蠢比进化更顽强。"),
                new Loc("idarran_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("Aldor is a living laboratory. The air is reagent, the ground a petri dish.##" +
                    "I’m not sure whether I’ve changed Aldor—or Aldor is rewriting me.##" +
                    "Magic here flows like blood... though no one’s figured out whose body it belongs to.##" +
                    "They say Aldor devours outsiders. I say it’s merely selecting the worthy.")
                    .Chinese("奥尔多是一座活着的实验室。空气是试剂，大地是培养皿。##" +
                    "我不确定是我改变了奥尔多，还是奥尔多正在重写我。##" +
                    "这里的魔力像血液一样流动，只是还没人弄清它属于谁的身体。##" +
                    "有人说奥尔多在吞噬外来者，但我认为它是在筛选合格者。"),
                new Loc("idarran_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("Fate is just statistics wearing a robe.##" +
                    "Order is an illusion—much like stable sanity.##" +
                    "Chaos doesn’t need worship—it demands comprehension.##" +
                    "Gods? I prefer miracles that can be replicated.")
                    .Chinese("命运不过是统计学的另一种说法。##" +
                    "秩序是幻觉，就像稳定的理智一样。##" +
                    "混乱不需要崇拜，它要求理解。##" +
                    "神？我更喜欢那些可以复制的奇迹。"),
                new Loc("idarran_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("If I’m not mistaken, you just stepped on a potion worth a month’s funding.##" +
                    "Don’t touch that flask. The last time it exploded, this arm was still growing.##" +
                    "Experiment failed? No, the world simply didn’t cooperate fast enough.##" +
                    "I tried sleeping once. Inspiration was louder than my dreams.")
                    .Chinese("如果我没看错，你刚踩了瓶价值一个月研究经费的药剂。##" +
                    "别碰那个瓶子。上次它爆炸的时候，我这只手还在长呢。##" +
                    "实验失败？不，这个世界只是没配合我快点完成。##" +
                    "我试过睡觉。灵感比梦更吵。"),

                new Loc("idarran_no_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("Let’s end the talk here. Words tend to dilute thought.")
                    .Chinese("我们的谈话到此为止吧，言语会稀释思考。"),
                new Loc("idarran_no_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("I’ve no new conclusions, and you’ve no new questions. A perfect moment for silence.")
                    .Chinese("我暂时没有新的结论，你也没新的问题。完美的沉默时刻。"),
                new Loc("idarran_no_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("Any further talk would waste air—and I still need it for experiments.")
                    .Chinese("继续聊下去只会浪费空气，而我还得留着做实验。"),
                new Loc("idarran_end_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("A pleasant chat. Once I find the proper reagent, we can continue.")
                    .Chinese("愉快的谈话。等我找到合适的试剂，我们可以继续。"),
                new Loc("idarran_end_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("Glad you understood. Most people just hear ‘madman’.")
                    .Chinese("很高兴你能听懂。大多数人只听到‘疯子’。"),
                new Loc("idarran_end_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("When you visit next time, bring something interesting—words, blood, either works.")
                    .Chinese("如果下次再来，记得带点新奇的素材。文字、血液都行。"),
                new Loc("idarran_end_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("I should return to my notes. Chaos waits for no one.")
                    .Chinese("我得回去写报告了。对混沌的研究从不会等人。"),
                new Loc("idarran_end_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("An amusing chat. Next time, I’ll record your neural response.")
                    .Chinese("真有趣的谈话，下次我会记录你的脑电反应。"),
                new Loc("idarran_end_chat")
                    .Set("Role", "geneticist")
                    .Set("Type", "any")
                    .English("Farewell, stranger. Or perhaps... see you next dissection.")
                    .Chinese("再见，陌生人。或者，下次再解剖见。"),

                new Loc("introGeneticExperiment01")
                    .English(
                    "Excuse me, sir. The University of Brynn’s genetics division is conducting a study on physical enhancement, and we need... experienced volunteers.##" +
                    "The project is personally overseen by Professor Idarran of Ulivo. Success means greater strength, endurance—even improved physiology.##" +
                    "It’s a chance to change your fate. Surely you’re tired of living by the sword forever?##" +
                    "Professor Idarran isn’t a mere scholar. He’s glimpsed order beyond this world. Joining his work is joining creation itself.##" +
                    "Of course, entirely legal—provisionally approved by the Brynn Ethics Board... for now.##" +
                    "Failure? Unlikely. Even if it happens, your body will still serve the cause of knowledge.")
                    .Chinese(
                    "打扰一下，先生。布林大学遗传学部正在进行强化生理研究，我们需要一些……有经验的志愿者。##" +
                    "这是经由乌里沃的艾达兰教授亲自监督的项目，成功者将获得力量、耐性——甚至更好的生理极限。##" +
                    "这是改变命运的机会。你不想永远只靠一把剑吃饭，对吧？##" +
                    "艾达兰教授不是普通的学者。他见过世界之外的秩序。参与他的实验，就等于参与创造本身。##" +
                    "当然，全程合法，受布林学院伦理委员会……暂时批准。##" +
                    "实验失败？不太可能。即使失败，你的身体也将继续为人类知识服务。"),
                new Loc("introGeneticExperiment01_pc")
                    .English("I’d like to see if this ‘science’ of yours is deadlier than magic.")
                    .Chinese("我想看看所谓的‘科学’能否比魔法更危险。"),
                new Loc("introGeneticExperiment01_pc")
                    .English("If I learn something from it, I don’t mind shedding some blood.")
                    .Chinese("只要能学到点东西，我不介意流点血。"),
                new Loc("introGeneticExperiment_reject_pc")
                    .English("Sounds like an alchemist’s trap. You planning to skin me or drain me?")
                    .Chinese("听起来像炼金术士的陷阱。你们打算剥皮还是抽髓？"),
                new Loc("introGeneticExperiment_reject_pc")
                    .English("Tell your professor I prefer drinking over boiling, thank you.")
                    .Chinese("去告诉你那位教授，我更喜欢喝酒而不是被煮。"),

                new Loc("introGeralt01")
                    .English("Heh... Never thought I’d see a familiar face in this world, a witcher. Time truly is a jester with a cruel sense of humor.##" +
                    "You’re wondering how I’m still alive. Truth is, I’ve been wondering the same thing.##" +
                    "No North here, no Nilfgaard either. Just a new stage... waiting for new gods to rise.##" +
                    "What's Your Name, Witcher?")
                    .Chinese("呵……我没想到在这个世界还能见到熟面孔，一个猎魔人。时间真是个幽默的幻术师。##" +
                    "你一定在想，我怎么还活着。其实我也想问同样的问题。##" +
                    "这里没有北方，也没有尼弗迦德。只有一个新的舞台，等待新的神明。##" +
                    "猎魔人，你叫什么名字？"),
                new Loc("introGeralt01_pc")
                    .English("Geralt of Rivia.")
                    .Chinese("利维亚的杰洛特。"),
                new Loc("introGeralt02")
                    .English("Geralt… interesting, I thought witchers were long extinct. Seems like Arzu’s ‘Redemption Plan’ did leave a spark.##" +
                    "Your mutations... are more stable than my early experiments.##" +
                    "I’ve seen your kind in Aldor, but you’re the first one I’ve met who can actually talk.##" +
                    "Tell me, Geralt of Rivia, how do you feel about being a failed experiment?")
                    .Chinese("杰洛特……有趣，我以为猎魔人早已灭绝。看来阿尔祖的‘赎罪计划’确实留下了火种。##" +
                    "你身上的突变……比我当年的实验稳定多了。##" +
                    "我在奥尔多见过你们这种人，但你是我遇到的第一个能说话的猎魔人。##" +
                    "告诉我，利维亚的杰洛特，你觉得自己是个失败的实验怎么样？"),
                new Loc("introGeralt02_pc")
                    .English(
                    "I know who you are, Idarran. Your creations spilled enough blood in the North for witchers to clean up your ‘legacy.’" +
                    "If that’s what you call evolution, I’d rather regress back to beasts.")
                    .Chinese(
                    "我知道你是谁，艾达兰。你的造物在北境流了足够的血。连猎魔人都不得不清理你的‘遗产’。" +
                    "如果你真觉得那是‘进化’，那我宁愿倒退回野兽时代。"),
                new Loc("introGeralt03")
                    .English(
                    "Heh... beasts at least obey instinct. Humans, on the other hand, can be taught to lie about theirs.##" +
                    "Don’t mistake me, witcher. I no longer wish to create monsters—only to test how fragile humanity truly is.")
                    .Chinese(
                    "呵……野兽至少遵循本能，而人类连本能都能被教育成谎言。##" +
                    "别误会，我并非想再造怪物。我只是想看看，人类的极限究竟有多脆弱。"),
                new Loc("introGeralt03_pc")
                    .English(
                    "I’ve heard enough of that talk. It usually ends in corpses and ashes. " +
                    "If you’re still running experiments, pray I don’t stumble upon them.")
                    .Chinese(
                    "我听够了这种话。通常它们的结尾都写着尸体和灰烬。" +
                    "你要是还在做实验，就祈祷别让我碰上。"),
                new Loc("introGeralt04")
                    .English("Heh... classic witcher response. A warning, a threat, and a pinch of moral superiority.##" +
                    "Relax, Geralt. For now, I only dissect truth—and you’re not rare enough to warrant the table.")
                    .Chinese("呵……典型的猎魔人回答。威胁、警告、再加一点道德优越。##" +
                    "放轻松，杰洛特。我暂时只解剖真理。你还不够稀有。"),
                new Loc("introGeralt05")
                    .English(
                    "In fact, I might be one of the few left who truly understands what you’re made of.##" +
                    "I’ve analyzed both magical systems—your Signs and Aldor’s primal incantations share an astonishing structural symmetry.##" +
                    "It seems ‘power’ is merely the same formula written in different tongues.##" +
                    "I’ve also reproduced many of the old alchemical brews—White Honey, Thunderbolt, Blizzard, even a few of those heartbeat-skipping mutagen decoction.##" +
                    "If you ever need... an advantage in this world, you know where to find me.")
                    .Chinese(
                    "事实上，在这个世界上，我可能是少数真正能理解你构造的人之一。##" +
                    "我已经解析了两个世界的魔法体系——你的法印与奥尔多的原初咒式在结构上惊人地相似。##" +
                    "原来所谓‘力量’只是不同语言书写的同一条公式。##" +
                    "我还重现了不少旧日的炼金药剂——白蜂蜜、雷霆、暴风雪，甚至那种会让心跳错拍的突变煎药。##" +
                    "若你在这片世界行走时需要……额外的优势，可以来找我。"),
                new Loc("introGeralt05_pc")
                    .English("Sounds like you’re settling in better than I am.##" +
                    "Though I usually only take a mage’s ‘help’ when there’s absolutely no other choice.")
                    .Chinese("听起来你在这儿活得比我自在。" +
                    "不过我通常只在万不得已的时候，才接受法师的‘帮助’。"),
                new Loc("introGeralt06")
                    .English(
                    "As you wish. Rejecting tools is a witcher’s form of romance, I respect that.##" +
                    "Just remember—when reason itself breaks, science remains reliable. At least, for me.")
                    .Chinese(
                    "随你。选择拒绝工具是猎魔人的浪漫，我尊重它。##" +
                    "只是记得——当理智都崩塌时，科学仍然可靠。至少，对我而言。"),

                new Loc("introGeralt06")
                    .Chinese("随你。选择拒绝工具是猎魔人的浪漫，我尊重它。##" +
                            "只是记得——当理智都崩塌时，科学仍然可靠。至少，对我而言。")
                    .English("As you wish. Rejecting tools is a witcher’s form of romance, I respect that.##" +
                            "Just remember—when reason itself breaks, science remains reliable. At least, for me."),
                new Loc("introGeralt06_pc")
                    .Chinese("那就祈祷你这份‘科学’别再造出我不得不去杀的东西。")
                    .English("Then pray your ‘science’ doesn’t spawn anything I’ll have to kill again."),
                new Loc("introGeralt07")
                    .Chinese("呵……我等这句话已经几个世纪了。##" +
                            "别担心，猎魔人——这次，我只在创造理解，而不是怪物。")
                    .English("Heh... I’ve waited centuries to hear those words again.##" +
                            "Don’t worry, witcher—this time, I’m creating comprehension, not creatures."),
                new Loc("introWitcherExperiment01")
                    .Chinese("你迟到了。##" +
                            "不过没关系，时间在这里早已失去意义。")
                    .English("You’re late.##" +
                            "No matter—time lost its meaning down here long ago."),
                new Loc("introWitcherExperiment01_pc")
                    .Chinese("……你就是那个教授？艾达兰？" +
                            "听说你在找‘志愿者’。")
                    .English("...You’re the professor? Idarran?" +
                            "Heard you were looking for... volunteers."),
                new Loc("introWitcherExperiment02")
                    .Chinese("‘志愿者’……真是个礼貌的词。##" +
                            "我更喜欢称呼你们为‘未完成的公式’。")
                    .English("‘Volunteer’... such a polite word.##" +
                            "I prefer calling you an ‘unfinished equation.’"),
                new Loc("introWitcherExperiment02_pc")
                    .Chinese("听上去不太让人安心。" +
                            "我只是想要更强的身体，活得久一点。")
                    .English("Doesn’t sound very reassuring." +
                            "I just want a stronger body—live a little longer, maybe."),
                new Loc("introWitcherExperiment03")
                    .Chinese("长久只是肉体的错觉。##" +
                            "我能给你的，是一种被改写的存在方式。##" +
                            "力量、感知、精准——以痛苦为代价的恩赐。")
                    .English("Longevity is merely the body’s illusion.##" +
                            "What I offer is a rewritten way of being.##" +
                            "Strength, perception, precision—gifts purchased through pain."),
                new Loc("introWitcherExperiment03_pc")
                    .Chinese("……我不会退缩。只希望这痛苦值得。")
                    .English("...I won’t run. Just hope the pain’s worth it."),
                new Loc("introWitcherExperiment04")
                    .Chinese("别紧张。深呼吸。##" +
                            "你即将见证一次古老仪式的重生，它被称作‘突变试炼’。##" +
                            "那时的人类还没准备好承受自己的进化。也许你可以。##" +
                            "这些药剂会改变你的血液。你的身体会抗拒，你的神经会尖叫。##" +
                            "但若你能熬过去，你将脱离人类的范畴。")
                    .English("Easy now. Breathe.##" +
                            "You’re about to witness the rebirth of an ancient rite, they called it the Trial of the Grasses.##" +
                            "Humanity wasn’t ready for its own evolution then. Perhaps you are.##" +
                            "These elixirs will change your blood. Your body will resist, your nerves will scream.##" +
                            "But if you survive, you’ll stand beyond the limits of man."),
                new Loc("introWitcherExperiment04_pc")
                    .Chinese("……我该害怕吗？")
                    .English("...Should I be afraid?"),
                new Loc("introWitcherExperiment05")
                    .Chinese("恐惧是旧人类的防御机制。##" +
                            "若你想成为新的猎魔人，就必须学会用理智取代恐惧。")
                    .English("Fear is the old man’s reflex.##" +
                            "If you wish to become a new witcher, you must replace fear with reason."),
                new Loc("introWitcherExperiment05_pc")
                    .Chinese("那我现在该做什么？")
                    .English("What do I do now?"),
                new Loc("introWitcherExperiment06")
                    .Chinese("准备好自己。##" +
                            "把恐惧关在门外，带着问题回来。##" +
                            "当你能直视未知而不退缩时，再来找我。那时，仪式就能开始。")
                    .English("Prepare yourself.##" +
                            "Leave your fear outside, bring your questions back.##" +
                            "When you can look at the unknown without flinching, come find me. Then the ritual can begin."),
                new Loc("readyToTrialOfGrasses_pc")
                    .Chinese("我准备好接受实验了。")
                    .English("I'm ready for the experiment."),
                new Loc("readyToTrialOfGrasses02")
                    .Chinese("……很好。##" +
                            "看来你已经下定决心了。##" +
                            "跟我来。")
                    .English("...Good.##" +
                            "Looks like you’ve made your decision.##" +
                            "Come here."),
                new Loc("introTrialOfGrasses01")
                    .Chinese("床就在这边，别愣着。")
                    .English("The cot’s right there, don’t just stand there staring."),
                new Loc("introTrialOfGrasses02")
                    .Chinese("放松。##" +
                            "你的心跳太快，会干扰药剂的扩散。##" +
                            "这是第一瓶——血液解构液，它会让你的血脉‘忘记’人类的形态。")
                    .English("Relax.##" +
                            "Your heart’s racing—it’ll disrupt the diffusion.##" +
                            "This is the first vial—Hemolytic Reagent. It’ll teach your veins to forget their human form."),
                new Loc("introTrialOfGrasses03")
                    .Chinese("（液体灌入的声音）##" +
                            "很好……现在，专注在呼吸上。##" +
                            "感受灼烧、冰冷与脉动，那是你的身体在学习新语言。")
                    .English("(Sound of liquid being poured)##" +
                            "Good... now focus on your breathing.##" +
                            "The burning, the chill, the pulse—that’s your body learning a new language."),
                new Loc("introTrialOfGrasses03_pc")
                    .Chinese("（痛苦呻吟）" +
                            "我——我看不清了……眼睛在烧！")
                    .English("(Groan of pain)" +
                            "I—can’t see... my eyes—they’re burning!"),
                new Loc("introTrialOfGrasses04")
                    .Chinese("不要挣扎。##" +
                            "视觉正在重组，你看到的是光谱的残影。##" +
                            "再坚持十秒，你的瞳孔会记住真相。")
                    .English("Don’t fight it.##" +
                            "Your vision’s reconstructing—you’re seeing afterimages of the spectrum.##" +
                            "Hold for ten seconds more, and your pupils will remember the truth."),
                new Loc("introTrialOfGrasses04_pc")
                    .Chinese("……（喘息）" +
                            "这到底是什么……")
                    .English("...(panting)" +
                            "What... what is this?"),
                new Loc("introTrialOfGrasses05")
                    .Chinese("新陈代谢正在崩解，这是第二阶段——共振试剂。##" +
                            "它会让你的感官彼此对话。听觉会嗅到光，皮肤会尝到空气。##" +
                            "别怕，这是通往感知的真正入口。")
                    .English("Your metabolism’s collapsing—that’s the second stage, the Resonance Serum.##" +
                            "It lets your senses speak to one another. Hearing will smell light, skin will taste air.##" +
                            "Don’t fear it—that’s the true threshold of perception."),
                new Loc("introTrialOfGrasses05_pc")
                    .Chinese("（数秒静默）" +
                            "……我还能感觉到自己的心跳。" +
                            "它……变了。")
                    .English("(Silence)" +
                            "...I can still feel my heartbeat." +
                            "It’s... different."),
                new Loc("introTrialOfGrasses06")
                    .Chinese("很好。你听到了自己身体的回声。##" +
                            "那不是幻觉，而是进化的声音。##" +
                            "当一切归于平静，你将不再是‘人’，但也不再畏惧‘人’。")
                    .English("Good. You’re hearing your body’s echo.##" +
                            "It isn’t a hallucination—it’s the sound of evolution.##" +
                            "When the silence returns, you’ll no longer be ‘human,’ yet you’ll no longer fear humanity."),
                new Loc("introTrialOfGrasses07")
                    .Chinese("现在休息。##" +
                            "等你的血液不再拒绝新的自己时，再睁开眼。")
                    .English("Now rest.##" +
                            "When your blood stops rejecting what you’ve become, open your eyes again."),
                new Loc("afterTrialOfGrasses01")
                    .Chinese("（空气流动的声音，水滴在石地上回荡）##" +
                            "……你终于醒了。##" +
                            "很好。你的体温还在，说明血液已经接受了重组。")
                    .English("(Sound of air shifting, droplets echoing on stone)##" +
                            "...You’re awake at last.##" +
                            "Good. Your temperature’s stable—means the blood accepted its restructuring."),
                new Loc("afterTrialOfGrasses01_pc")
                    .Chinese("……我……我还活着？" +
                            "身体……感觉不一样……像是有另一层在呼吸。")
                    .English("...I... I’m alive?" +
                            "My body... feels different... like something else inside me is breathing."),
                new Loc("afterTrialOfGrasses02")
                    .Chinese("那是你的感官在重新校准。##" +
                            "尝试睁开眼。告诉我——你看到了什么颜色？")
                    .English("That’s your senses recalibrating.##" +
                            "Try opening your eyes. Tell me—what color do you see?"),
                new Loc("afterTrialOfGrasses02_pc")
                    .Chinese("……一切都发着光。空气在闪烁。" +
                            "我甚至能看见你的呼吸在流动……")
                    .English("...Everything’s glowing. The air’s shimmering." +
                            "I can even see your breath moving..."),
                new Loc("afterTrialOfGrasses03")
                    .Chinese("完美。视觉突变成功。##" +
                            "看来你比我想象中更耐受。也许……下一阶段可以提前。")
                    .English("Perfect. Visual mutation confirmed.##" +
                            "You’re more resilient than I expected. Perhaps... the next phase can be advanced."),
                new Loc("afterTrialOfGrasses03_pc")
                    .Chinese("下一阶段？" +
                            "你还打算对我做什么？")
                    .English("Next phase?" +
                            "What else are you planning to do to me?"),
                new Loc("afterTrialOfGrasses04")
                    .Chinese("别误会。##" +
                            "你现在需要的是稳定，而不是更多痛苦。##" +
                            "喝下这个——它会抑制体内的化学风暴。")
                    .English("Don’t misunderstand.##" +
                            "What you need now is stability, not more pain.##" +
                            "Drink this—it’ll quiet the chemical storm inside you."),
                new Loc("afterTrialOfGrasses04_pc")
                    .Chinese("（金属杯落在木桌上，液体声）" +
                            "味道像……铁和灰。")
                    .English("(Sound of a metal cup on wood, liquid sloshing)" +
                            "Tastes like... iron and ash."),
                new Loc("afterTrialOfGrasses05")
                    .Chinese("那是生命在被重写的味道。##" +
                            "别急着起身。##" +
                            "你的神经还在判断自己是否属于这个身体。")
                    .English("That’s the taste of life being rewritten.##" +
                            "Don’t rush to stand.##" +
                            "Your nerves are still deciding whether they belong to this body."),
                new Loc("afterTrialOfGrasses05_pc")
                    .Chinese("……你打算拿我做什么？猎魔人？怪物？还是别的东西？")
                    .English("...What do you plan to make of me? Witcher? Monster? Something else?"),
                new Loc("afterTrialOfGrasses06")
                    .Chinese("定义是给哲学家用的。##" +
                            "而你——是我的证明。##" +
                            "证明理智比神明更擅长创造。")
                    .English("Definitions are for philosophers.##" +
                            "You, however, are my proof.##" +
                            "Proof that reason outperforms gods at creation."),
                new Loc("afterTrialOfGrasses07")
                    .Chinese("你先休息吧。##" +
                            "这具身体尚且陌生，需要时间与经验去磨合。##" +
                            "等你真正掌握它的力量后，才有资格参与我的研究计划。##" +
                            "另外——我这里有几种专为这种身体调制的炼金药水与配方，" +
                            "还有关于魔法潜能的研究笔记。若你感兴趣……可以花点钱买。")
                    .English("Rest for now.##" +
                            "This body is still unfamiliar—you’ll need time and experience to make it your own.##" +
                            "Once you’ve truly mastered its power, you may earn the right to join my research.##" +
                            "Oh, and—I happen to have a few alchemical potions and formulas tailored for that body, " +
                            "along with some notes on its magical potential. If you’re interested... they’re for sale.")
            )
            .Save();

    }
}
