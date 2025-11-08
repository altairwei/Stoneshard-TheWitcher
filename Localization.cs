
using ModShardLauncher;
using ModShardLauncher.Mods;

namespace TheWitcher;

public class Localization
{
    public static void AddLocalizationAll()
    {
        AddIdarranTexts();
    }

    private static void AddIdarranTexts()
    {
        Utils.InjectItemsToTable(
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

        Msl.InjectTableDialogLocalization(
            // --- Greeting Lines ---
            new LocalizationSentence(
                id: "greeting_idarran",
                sentence: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "杰洛特，又在和命运讨价还价？" },
                    { ModLanguage.English, "Geralt, still bargaining with fate, I see." }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "今天实验顺利，你要不要也做个对照组？" },
                    { ModLanguage.English, "Today’s experiment went well. Care to be my control sample?" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "你的徽章还在震动吗？也许是它在想我。" },
                    { ModLanguage.English, "Your medallion still vibrating? Perhaps it remembers me." }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "你看起来比昨天更人类一点，真可惜。" },
                    { ModLanguage.English, "You look a bit more human today... pity." }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "别担心，我暂时不需要新的猎魔人样本。" },
                    { ModLanguage.English, "Relax, I’m not in need of a new witcher sample—yet." }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "你看到了么？完美的生命正在我的手中重塑。" },
                    { ModLanguage.English, "Do you see it? Perfection itself, reshaped by my hands." }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "阿尔祖曾说：‘造物的界限，只是怯懦者的借口。’" },
                    { ModLanguage.English, "Alzur once said: ‘The limits of creation are excuses for the timid.’" }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "猎魔人？啊，是我的半成品。" },
                    { ModLanguage.English, "A witcher? Ah, one of my unfinished prototypes." }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "科西莫用符号写下秩序，而我用血液写下进化。" },
                    { ModLanguage.English, "Cosimo wrote order in sigils, I wrote evolution in blood." }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "我并非创造怪物，我只是让真相剥离伪装。" },
                    { ModLanguage.English, "I do not create monsters. I simply peel away the illusion of humanity." }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "我很欣赏你那种被逼出来的理智。它和疯狂只差一滴突变液。" },
                    { ModLanguage.English, "I admire that forced composure of yours. It’s just one drop of mutagen away from madness." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "见到你总让我想起一个问题：进化和退化，究竟谁在赢？" },
                    { ModLanguage.English, "Seeing you always reminds me of a question—evolution or regression, which one’s winning?" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "有趣，你今天闻起来不像沼泽或血。新洗的甲胄？" },
                    { ModLanguage.English, "Interesting, you don’t smell of swamps or blood today. Fresh armor, perhaps?" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "欢迎。请别乱碰那边的瓶子，除非你想提前成为样本。" },
                    { ModLanguage.English, "Welcome. Don’t touch the flasks unless you wish to become part of the study early." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "很好，我正缺少对照组。" },
                    { ModLanguage.English, "Excellent, I was missing a control group." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "放松，我只在实验阶段需要尸体。" },
                    { ModLanguage.English, "Relax. I only require corpses during the testing phase." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "你的眼神像是见过混沌的人。我喜欢这样的样本。" },
                    { ModLanguage.English, "Those eyes... they’ve seen Chaos. I like that kind of specimen." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "奥尔多的人大多无知，而你至少有求知的气味。" },
                    { ModLanguage.English, "Most in Aldor reek of ignorance. You, at least, smell of curiosity." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "你带来了消息，金币，还是新的生物组织？" },
                    { ModLanguage.English, "Have you brought news, gold, or biological tissue?" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "greeting_idarran",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "希望你是来谈话的，而不是来燃烧我的研究。" },
                    { ModLanguage.English, "I trust you’re here to talk, not to torch my research." }
                }
            )
            { Role = "geneticist", Type = "any" }
        );

        Msl.InjectTableDialogLocalization(
            new LocalizationSentence(
                id: "rest_reading",
                sentence: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "……能安静点吗？知识不喜欢被噪音打断。" },
                    { ModLanguage.English, "...Could you be quiet? Knowledge dislikes being interrupted by noise." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "rest_reading",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "我在读书，不是开会。##如果你没带新发现，那就让文字继续说话。" },
                    { ModLanguage.English, "I’m reading, not hosting a conference.##Unless you bring new discoveries, let the words keep speaking instead." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "rest_reading",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "这本书讲的是痛觉传导……要不要我现场演示？" },
                    { ModLanguage.English, "This volume is about pain conduction... care for a live demonstration?" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "rest_reading",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "思考是一种炼金术，打扰它就像掺错了催化剂。" },
                    { ModLanguage.English, "Thought is alchemy—disturbing it is like adding the wrong catalyst." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "rest_reading",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "除非你能比这页脚注更有趣，否则别开口。" },
                    { ModLanguage.English, "Unless you’re more interesting than this footnote, don’t speak." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "rest_reading",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "我已经记下你的脚步频率，别让我在报告里写‘干扰因素’。" },
                    { ModLanguage.English, "I’ve memorized your step rhythm. Don’t make me file it under ‘experimental interference’." }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "rest_reading",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.Chinese, "……公式对不上，变量太多。或者，是我太人类了。" },
                    { ModLanguage.English, "...The formula doesn’t balance. Too many variables. Or perhaps I’m still too human." }
                }
            )
            { Role = "geneticist", Type = "any" }
        );

        Msl.InjectTableDialogLocalization(
            // --- [NPC] CHATS ---
            new LocalizationSentence(
                id: "idarran_chat",
                sentence: new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English,
                    "You call it Aldor. I call it an error—an alchemical byproduct of time itself.##" +
                    "I was attempting interdimensional genome recombination... and then, the door opened.##" +
                    "The magic here is unlike our Chaos—purer, yet more feral." },
                    { ModLanguage.Chinese,
                    "你称它为‘奥尔多’，而我称它为‘误差’——一次时空炼金的副产物。##" +
                    "我当时正在尝试跨维基因重组实验……然后，门开了。##" +
                    "这里的魔力结构与你我熟知的混沌截然不同——更纯净，也更野蛮。" }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "idarran_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English,
                    "I’ve studied a native creature called the Gulon—its cells devour mana itself. Beautiful, isn’t it?##" +
                    "This world made me believe Chaos isn’t a flaw—it’s the embryo of higher order." },
                    { ModLanguage.Chinese,
                    "我研究了一种本地生物，名为‘谷隆’，它的细胞能主动吞噬法力。很美妙，不是吗？##" +
                    "这个世界让我重新相信，混乱并非错误，而是更高秩序的雏形。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English,
                    "Still hunting my children, are you? The ones your world called monsters.##" +
                    "Ironic, isn’t it? Here they worship me as a god, and you as a beast.##" +
                    "Witchers were our most perfect design—you lot just refuse to admit it.##" +
                    "Ever get the feeling that perhaps humanity was the real monster all along?" },
                    { ModLanguage.Chinese,
                    "你依然在狩猎我的孩子们么？那些曾在你的世界被称作怪物的生命。##" +
                    "讽刺吧？这个世界把我视作神，而把你当成野兽。##" +
                    "猎魔人……其实是我们最完美的作品，只是你们自己不愿承认。##" +
                    "你有过这样的错觉么？也许‘人类’才是异界中真正的怪物。" }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "idarran_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English,
                    "If Alzur could see my work now, he’d either smile... or frown deeply.##" +
                    "Cosimo... that old man would rather die in an equation than live in a miracle.##" +
                    "They thought I sought immortality. I simply despised endings." },
                    { ModLanguage.Chinese,
                    "阿尔祖若看到我如今的成果，一定会微笑。或者——皱眉。##" +
                    "科西莫……啊，那老家伙宁愿死在公式里，也不肯活在奇迹中。##" +
                    "他们都以为我在追求永生。其实我只是讨厌结局。" }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "idarran_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English,
                    "You call yourself human, yet deny the blood that made you something else.##" +
                    "We’re both ghosts of creation, Geralt—only I create, and you destroy.##" +
                    "I don’t hate you. In truth, I’ve always considered you our proudest failure.##" +
                    "Funny thing—no matter the world, witchers remain a lonely species." },
                    { ModLanguage.Chinese,
                    "你称自己为人，却拒绝承认自己的血液早已与人类不同。##" +
                    "我们都是造物的幽灵，杰洛特——只不过我还在造，而你在毁。##" +
                    "我不憎恨你。事实上，我一直把你视为我们最骄傲的失败。##" +
                    "有趣的是，无论在哪个世界，猎魔人总是孤独的物种。" }
                }
            )
            { Role = "geneticist", Type = "geralt" },

            new LocalizationSentence(
                "idarran_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English,
                    "The beauty of life lies in its endless attempts to correct itself. I merely help it hurry along.##" +
                    "In Aldor, I’ve seen fish that breathe air, bones that sing, and metals that heal themselves. Nature is never short of imagination.##" +
                    "My failure rate is low. Most so-called failures simply didn’t have time to finish evolving.##" +
                    "They call my work a blasphemy of life. They forget—creation itself was the first blasphemy." },
                    { ModLanguage.Chinese,
                    "生命的美妙在于它不断试图自我修正，而我，只是帮它快一点。##" +
                    "在奥尔多，我见过能在体外呼吸的鱼、会唱歌的骨头，还有自愈的铁。自然从不缺乏想象力。##" +
                    "我的实验失败率很低，大多数‘失败’只是没来得及完成。##" +
                    "有人说我在亵渎生命，可他们忘了——创造本身就是最古老的亵渎。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English,
                    "People fear the unknown, though the unknown simply can’t be bothered to introduce itself.##" +
                    "Ignorance brings happiness... but happiness corrodes the nervous system.##" +
                    "Sanity is a fragile potion—the more you use it, the less it works.##" +
                    "After studying hundreds of species, I found one universal truth—stupidity survives evolution." },
                    { ModLanguage.Chinese,
                    "人们总害怕未知，其实未知只是懒得自我介绍。##" +
                    "无知能带来幸福，但幸福会腐蚀神经系统。##" +
                    "理智是脆弱的药剂，用多了就失效。##" +
                    "我研究了几百个物种，唯一恒定的结论是：愚蠢比进化更顽强。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English,
                    "Aldor is a living laboratory. The air is reagent, the ground a petri dish.##" +
                    "I’m not sure whether I’ve changed Aldor—or Aldor is rewriting me.##" +
                    "Magic here flows like blood... though no one’s figured out whose body it belongs to.##" +
                    "They say Aldor devours outsiders. I say it’s merely selecting the worthy." },
                    { ModLanguage.Chinese,
                    "奥尔多是一座活着的实验室。空气是试剂，大地是培养皿。##" +
                    "我不确定是我改变了奥尔多，还是奥尔多正在重写我。##" +
                    "这里的魔力像血液一样流动，只是还没人弄清它属于谁的身体。##" +
                    "有人说奥尔多在吞噬外来者，但我认为它是在筛选合格者。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English,
                    "Fate is just statistics wearing a robe.##" +
                    "Order is an illusion—much like stable sanity.##" +
                    "Chaos doesn’t need worship—it demands comprehension.##" +
                    "Gods? I prefer miracles that can be replicated." },
                    { ModLanguage.Chinese,
                    "命运不过是统计学的另一种说法。##" +
                    "秩序是一种幻觉，就像稳定的精神状态。##" +
                    "混沌不需要崇拜，它需要理解。##" +
                    "神？我更喜欢可重复验证的奇迹。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English,
                    "If I’m not mistaken, you just stepped on a potion worth a month’s funding.##" +
                    "Don’t touch that flask. The last time it exploded, this arm was still growing.##" +
                    "Experiment failed? No, the world simply didn’t cooperate fast enough.##" +
                    "I tried sleeping once. Inspiration was louder than my dreams." },
                    { ModLanguage.Chinese,
                    "如果我没认错，你刚踩到了一瓶价值一个月研究经费的药。##" +
                    "别碰那只瓶子。它上次爆炸时，我还没长出现在这条手臂。##" +
                    "实验失败了？不，是世界配合得不够快。##" +
                    "我试过睡觉，但灵感总比梦更吵。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            // --- No Chat Lines ---
            new LocalizationSentence(
                "idarran_no_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Let’s end the talk here. Words tend to dilute thought." },
                    { ModLanguage.Chinese, "我们的谈话到此为止吧，言语会稀释思考。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_no_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "I’ve no new conclusions, and you’ve no new questions. A perfect moment for silence." },
                    { ModLanguage.Chinese, "我暂时没有新的结论，你也没新的问题。完美的沉默时刻。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_no_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Any further talk would waste air—and I still need it for experiments." },
                    { ModLanguage.Chinese, "继续聊下去只会浪费空气，而我还得留着做实验。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            // --- Return Lines ---
            new LocalizationSentence(
                "idarran_end_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "A pleasant chat. Once I find the proper reagent, we can continue." },
                    { ModLanguage.Chinese, "愉快的谈话。等我找到合适的试剂，我们可以继续。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_end_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Glad you understood. Most people just hear ‘madman’." },
                    { ModLanguage.Chinese, "很高兴你能听懂。大多数人只听到‘疯子’。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_end_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "When you visit next time, bring something interesting—words, blood, either works." },
                    { ModLanguage.Chinese, "如果下次再来，记得带点新奇的素材。文字、血液都行。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_end_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "I should return to my notes. Chaos waits for no one." },
                    { ModLanguage.Chinese, "我得回去写报告了。对混沌的研究从不会等人。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_end_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "An amusing chat. Next time, I’ll record your neural response." },
                    { ModLanguage.Chinese, "真有趣的谈话，下次我会记录你的脑电反应。" }
                }
            )
            { Role = "geneticist", Type = "any" },

            new LocalizationSentence(
                "idarran_end_chat",
                new Dictionary<ModLanguage, string>
                {
                    { ModLanguage.English, "Farewell, stranger. Or perhaps... see you next dissection." },
                    { ModLanguage.Chinese, "再见，陌生人。或者，下次再解剖见。" }
                }
            )
            { Role = "geneticist", Type = "any" }
        );

        Msl.InjectTableDialogLocalization(
            new LocalizationSentence(
                id: "introGeneticExperiment01",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese,
                    "打扰一下，先生。布林大学遗传学部正在进行强化生理研究，我们需要一些……有经验的志愿者。##" +
                    "这是经由乌里沃的艾达兰教授亲自监督的项目，成功者将获得力量、耐性——甚至更好的生理极限。##" +
                    "这是改变命运的机会。你不想永远只靠一把剑吃饭，对吧？##" +
                    "艾达兰教授不是普通的学者。他见过世界之外的秩序。参与他的实验，就等于参与创造本身。##" +
                    "当然，全程合法，受布林学院伦理委员会……暂时批准。##" +
                    "实验失败？不太可能。即使失败，你的身体也将继续为人类知识服务。"},
                    {ModLanguage.English,
                    "Excuse me, sir. The University of Brynn’s genetics division is conducting a study on physical enhancement, and we need... experienced volunteers.##" +
                    "The project is personally overseen by Professor Idarran of Ulivo. Success means greater strength, endurance—even improved physiology.##" +
                    "It’s a chance to change your fate. Surely you’re tired of living by the sword forever?##" +
                    "Professor Idarran isn’t a mere scholar. He’s glimpsed order beyond this world. Joining his work is joining creation itself.##" +
                    "Of course, entirely legal—provisionally approved by the Brynn Ethics Board... for now.##" +
                    "Failure? Unlikely. Even if it happens, your body will still serve the cause of knowledge." }
                }
            ),

            new LocalizationSentence(
                id: "introGeneticExperiment01_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "我想看看所谓的‘科学’能否比魔法更危险。"},
                    {ModLanguage.English, "I’d like to see if this ‘science’ of yours is deadlier than magic."}
                }
            ),
            new LocalizationSentence(
                id: "introGeneticExperiment01_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "只要能学到点东西，我不介意流点血。"},
                    {ModLanguage.English, "If I learn something from it, I don’t mind shedding some blood."}
                }
            ),
            new LocalizationSentence(
                id: "introGeneticExperiment_reject_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "听起来像炼金术士的陷阱。你们打算剥皮还是抽髓？"},
                    {ModLanguage.English, "Sounds like an alchemist’s trap. You planning to skin me or drain me?"}
                }
            ),
            new LocalizationSentence(
                id: "introGeneticExperiment_reject_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "去告诉你那位教授，我更喜欢喝酒而不是被煮。"},
                    {ModLanguage.English, "Tell your professor I prefer drinking over boiling, thank you."}
                }
            ),


            // ---- Introduction to Geralt ----
            new LocalizationSentence(
                id: "introGeralt01",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese,
                    "呵……我没想到在这个世界还能见到熟面孔，一个猎魔人。时间真是个幽默的幻术师。##" +
                    "你一定在想，我怎么还活着。其实我也想问同样的问题。##" +
                    "这里没有北方，也没有尼弗迦德。只有一个新的舞台，等待新的神明。##" +
                    "猎魔人，你叫什么名字？"},
                    {ModLanguage.English,
                    "Heh... Never thought I’d see a familiar face in this world, a witcher. Time truly is a jester with a cruel sense of humor.##" +
                    "You’re wondering how I’m still alive. Truth is, I’ve been wondering the same thing.##" +
                    "No North here, no Nilfgaard either. Just a new stage... waiting for new gods to rise.##" +
                    "What's Your Name, Witcher?"}
                }
            ),

            new LocalizationSentence(
                id: "introGeralt01_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "利维亚的杰洛特。"},
                    {ModLanguage.English, "Geralt of Rivia."}
                }
            ),

            new LocalizationSentence(
                id: "introGeralt02",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "杰洛特……有趣，我以为猎魔人早已灭绝。看来阿尔祖的‘赎罪计划’确实留下了火种。##" +
                        "你身上的突变……比我当年的实验稳定得多。是谁改进了它？科西莫？不，他太保守。##" +
                        "告诉我，猎魔人，你是他们造的最后一件作品，还是——你们终于学会自我繁殖了？"
                    },
                    {
                        ModLanguage.English,
                        "Geralt... fascinating. I thought witchers had long gone extinct. Seems Alzur’s ‘atonement project’ left a spark after all.##" +
                        "Your mutations... far more stable than my early prototypes. Who refined them? Cosimo? No, too cautious for that.##" +
                        "Tell me, witcher—are you the final product of their craft, or have your kind learned to reproduce at last?"
                    }
                }
            ),

            new LocalizationSentence(
                id: "introGeralt02_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "我知道你是谁，艾达兰。你的造物在北境流了足够的血。连猎魔人都不得不清理你的‘遗产’。" +
                        "如果你真觉得那是‘进化’，那我宁愿倒退回野兽时代。"
                    },
                    {
                        ModLanguage.English,
                        "I know who you are, Idarran. Your creations spilled enough blood in the North for witchers to clean up your ‘legacy.’" +
                        "If that’s what you call evolution, I’d rather regress back to beasts."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introGeralt03",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "呵……野兽至少遵循本能，而人类连本能都能被教育成谎言。##" +
                        "别误会，我并非想再造怪物。我只是想看看，人类的极限究竟有多脆弱。"
                    },
                    {
                        ModLanguage.English,
                        "Heh... beasts at least obey instinct. Humans, on the other hand, can be taught to lie about theirs.##" +
                        "Don’t mistake me, witcher. I no longer wish to create monsters—only to test how fragile humanity truly is."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introGeralt03_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "我听够了这种话。通常它们的结尾都写着尸体和灰烬。" +
                        "你要是还在做实验，就祈祷别让我碰上。"
                    },
                    {
                        ModLanguage.English,
                        "I’ve heard enough of that talk. It usually ends in corpses and ashes. " +
                        "If you’re still running experiments, pray I don’t stumble upon them."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introGeralt04",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "呵……典型的猎魔人回答。威胁、警告、再加一点道德优越。##" +
                        "放轻松，杰洛特。我暂时只解剖真理。你还不够稀有。"
                    },
                    {
                        ModLanguage.English,
                        "Heh... classic witcher response. A warning, a threat, and a pinch of moral superiority.##" +
                        "Relax, Geralt. For now, I only dissect truth—and you’re not rare enough to warrant the table."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introGeralt05",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "事实上，在这个世界上，我可能是少数真正能理解你构造的人之一。##" +
                        "我已经解析了两个世界的魔法体系——你的法印与奥尔多的原初咒式在结构上惊人地相似。##" +
                        "原来所谓‘力量’只是不同语言书写的同一条公式。##" +
                        "我还重现了不少旧日的炼金药剂——白蜂蜜、雷霆、暴风雪，甚至那种会让心跳错拍的突变煎药。##" +
                        "若你在这片世界行走时需要……额外的优势，可以来找我。"

                    },
                    {
                        ModLanguage.English,
                        "In fact, I might be one of the few left who truly understands what you’re made of.##" +
                        "I’ve analyzed both magical systems—your Signs and Aldor’s primal incantations share an astonishing structural symmetry.##" +
                        "It seems ‘power’ is merely the same formula written in different tongues.##" +
                        "I’ve also reproduced many of the old alchemical brews—White Honey, Thunderbolt, Blizzard, even a few of those heartbeat-skipping mutagen decoction.##" +
                        "If you ever need... an advantage in this world, you know where to find me."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introGeralt05_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "听起来你在这儿活得比我自在。" +
                        "不过我通常只在万不得已的时候，才接受法师的‘帮助’。"
                    },
                    {
                        ModLanguage.English,
                        "Sounds like you’re settling in better than I am.##" +
                        "Though I usually only take a mage’s ‘help’ when there’s absolutely no other choice."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introGeralt06",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "随你。选择拒绝工具是猎魔人的浪漫，我尊重它。##" +
                        "只是记得——当理智都崩塌时，科学仍然可靠。至少，对我而言。"
                    },
                    {
                        ModLanguage.English,
                        "As you wish. Rejecting tools is a witcher’s form of romance, I respect that.##" +
                        "Just remember—when reason itself breaks, science remains reliable. At least, for me."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introGeralt06_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "那就祈祷你这份‘科学’别再造出我不得不去杀的东西。"},
                    {ModLanguage.English, "Then pray your ‘science’ doesn’t spawn anything I’ll have to kill again."}
                }
            ),

            new LocalizationSentence(
                id: "introGeralt07",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "呵……我等这句话已经几个世纪了。##" +
                        "别担心，猎魔人——这次，我只在创造理解，而不是怪物。"}
                    ,
                    {
                        ModLanguage.English,
                        "Heh... I’ve waited centuries to hear those words again.##" +
                        "Don’t worry, witcher—this time, I’m creating comprehension, not creatures."}
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment01",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "你迟到了。##" +
                        "不过没关系，时间在这里早已失去意义。"
                    },
                    {
                        ModLanguage.English,
                        "You’re late.##" +
                        "No matter—time lost its meaning down here long ago."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment01_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "……你就是那个教授？艾达兰？" +
                        "听说你在找‘志愿者’。"
                    },
                    {
                        ModLanguage.English,
                        "...You’re the professor? Idarran?" +
                        "Heard you were looking for... volunteers."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment02",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "‘志愿者’……真是个礼貌的词。##" +
                        "我更喜欢称呼你们为‘未完成的公式’。"
                    },
                    {
                        ModLanguage.English,
                        "‘Volunteer’... such a polite word.##" +
                        "I prefer calling you an ‘unfinished equation.’"
                    }
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment02_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "听上去不太让人安心。" +
                        "我只是想要更强的身体，活得久一点。"
                    },
                    {
                        ModLanguage.English,
                        "Doesn’t sound very reassuring." +
                        "I just want a stronger body—live a little longer, maybe."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment03",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "长久只是肉体的错觉。##" +
                        "我能给你的，是一种被改写的存在方式。##" +
                        "力量、感知、精准——以痛苦为代价的恩赐。"
                    },
                    {
                        ModLanguage.English,
                        "Longevity is merely the body’s illusion.##" +
                        "What I offer is a rewritten way of being.##" +
                        "Strength, perception, precision—gifts purchased through pain."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment03_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "……我不会退缩。只希望这痛苦值得。"},
                    {ModLanguage.English, "...I won’t run. Just hope the pain’s worth it."}
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment04",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "别紧张。深呼吸。##" +
                        "你即将见证一次古老仪式的重生，它被称作‘突变试炼’。##" +
                        "那时的人类还没准备好承受自己的进化。也许你可以。##" +
                        "这些药剂会改变你的血液。你的身体会抗拒，你的神经会尖叫。##" +
                        "但若你能熬过去，你将脱离人类的范畴。"
                    },
                    {
                        ModLanguage.English,
                        "Easy now. Breathe.##" +
                        "You’re about to witness the rebirth of an ancient rite, they called it the Trial of the Grasses.##" +
                        "Humanity wasn’t ready for its own evolution then. Perhaps you are.##" +
                        "These elixirs will change your blood. Your body will resist, your nerves will scream.##" +
                        "But if you survive, you’ll stand beyond the limits of man."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment04_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "……我该害怕吗？"},
                    {ModLanguage.English, "...Should I be afraid?"}
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment05",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "恐惧是旧人类的防御机制。##" +
                        "若你想成为新的猎魔人，就必须学会用理智取代恐惧。"
                    },
                    {
                        ModLanguage.English,
                        "Fear is the old man’s reflex.##" +
                        "If you wish to become a new witcher, you must replace fear with reason."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment05_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "那我现在该做什么？"},
                    {ModLanguage.English, "What do I do now?"}
                }
            ),

            new LocalizationSentence(
                id: "introWitcherExperiment06",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "准备好自己。##" +
                        "把恐惧关在门外，带着问题回来。##" +
                        "当你能直视未知而不退缩时，再来找我。那时，仪式就能开始。"
                    },
                    {
                        ModLanguage.English,
                        "Prepare yourself.##" +
                        "Leave your fear outside, bring your questions back.##" +
                        "When you can look at the unknown without flinching, come find me. Then the ritual can begin."
                    }
                }
            ),


            new LocalizationSentence(
                id: "readyToTrialOfGrasses_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "我准备好接受实验了。"},
                    {ModLanguage.English, "I'm ready for the experiment."}
                }
            ),

            new LocalizationSentence(
                id: "readyToTrialOfGrasses02",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "……很好。##" +
                        "看来你已经下定决心了。##" +
                        "跟我来。"
                    },
                    {
                        ModLanguage.English,
                        "...Good.##" +
                        "Looks like you’ve made your decision.##" +
                        "Come here."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introTrialOfGrasses01",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {ModLanguage.Chinese, "床就在这边，别愣着。"},
                    {ModLanguage.English, "The cot’s right there, don’t just stand there staring."}
                }
            ),

            new LocalizationSentence(
                id: "introTrialOfGrasses02",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "放松。##" +
                        "你的心跳太快，会干扰药剂的扩散。##" +
                        "这是第一瓶——血液解构液，它会让你的血脉‘忘记’人类的形态。"
                    },
                    {
                        ModLanguage.English,
                        "Relax.##" +
                        "Your heart’s racing—it’ll disrupt the diffusion.##" +
                        "This is the first vial—Hemolytic Reagent. It’ll teach your veins to forget their human form."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introTrialOfGrasses03",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "（液体灌入的声音）##" +
                        "很好……现在，专注在呼吸上。##" +
                        "感受灼烧、冰冷与脉动，那是你的身体在学习新语言。"
                    },
                    {
                        ModLanguage.English,
                        "(Sound of liquid being poured)##" +
                        "Good... now focus on your breathing.##" +
                        "The burning, the chill, the pulse—that’s your body learning a new language."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introTrialOfGrasses03_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "（痛苦呻吟）" +
                        "我——我看不清了……眼睛在烧！"
                    },
                    {
                        ModLanguage.English,
                        "(Groan of pain)" +
                        "I—can’t see... my eyes—they’re burning!"
                    }
                }
            ),

            new LocalizationSentence(
                id: "introTrialOfGrasses04",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "不要挣扎。##" +
                        "视觉正在重组，你看到的是光谱的残影。##" +
                        "再坚持十秒，你的瞳孔会记住真相。"
                    },
                    {
                        ModLanguage.English,
                        "Don’t fight it.##" +
                        "Your vision’s reconstructing—you’re seeing afterimages of the spectrum.##" +
                        "Hold for ten seconds more, and your pupils will remember the truth."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introTrialOfGrasses04_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "……（喘息）" +
                        "这到底是什么……"
                    },
                    {
                        ModLanguage.English,
                        "...(panting)" +
                        "What... what is this?"
                    }
                }
            ),

            new LocalizationSentence(
                id: "introTrialOfGrasses05",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "新陈代谢正在崩解，这是第二阶段——共振试剂。##" +
                        "它会让你的感官彼此对话。听觉会嗅到光，皮肤会尝到空气。##" +
                        "别怕，这是通往感知的真正入口。"
                    },
                    {
                        ModLanguage.English,
                        "Your metabolism’s collapsing—that’s the second stage, the Resonance Serum.##" +
                        "It lets your senses speak to one another. Hearing will smell light, skin will taste air.##" +
                        "Don’t fear it—that’s the true threshold of perception."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introTrialOfGrasses05_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "（数秒静默）" +
                        "……我还能感觉到自己的心跳。" +
                        "它……变了。"
                    },
                    {
                        ModLanguage.English,
                        "(Silence)" +
                        "...I can still feel my heartbeat." +
                        "It’s... different."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introTrialOfGrasses06",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "很好。你听到了自己身体的回声。##" +
                        "那不是幻觉，而是进化的声音。##" +
                        "当一切归于平静，你将不再是‘人’，但也不再畏惧‘人’。"
                    },
                    {
                        ModLanguage.English,
                        "Good. You’re hearing your body’s echo.##" +
                        "It isn’t a hallucination—it’s the sound of evolution.##" +
                        "When the silence returns, you’ll no longer be ‘human,’ yet you’ll no longer fear humanity."
                    }
                }
            ),

            new LocalizationSentence(
                id: "introTrialOfGrasses07",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "现在休息。##" +
                        "等你的血液不再拒绝新的自己时，再睁开眼。"
                    },
                    {
                        ModLanguage.English,
                        "Now rest.##" +
                        "When your blood stops rejecting what you’ve become, open your eyes again."
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses01",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "（空气流动的声音，水滴在石地上回荡）##" +
                        "……你终于醒了。##" +
                        "很好。你的体温还在，说明血液已经接受了重组。"
                    },
                    {
                        ModLanguage.English,
                        "(Sound of air shifting, droplets echoing on stone)##" +
                        "...You’re awake at last.##" +
                        "Good. Your temperature’s stable—means the blood accepted its restructuring."
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses01_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "……我……我还活着？" +
                        "身体……感觉不一样……像是有另一层在呼吸。"
                    },
                    {
                        ModLanguage.English,
                        "...I... I’m alive?" +
                        "My body... feels different... like something else inside me is breathing."
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses02",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "那是你的感官在重新校准。##" +
                        "尝试睁开眼。告诉我——你看到了什么颜色？"
                    },
                    {
                        ModLanguage.English,
                        "That’s your senses recalibrating.##" +
                        "Try opening your eyes. Tell me—what color do you see?"
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses02_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "……一切都发着光。空气在闪烁。" +
                        "我甚至能看见你的呼吸在流动……"
                    },
                    {
                        ModLanguage.English,
                        "...Everything’s glowing. The air’s shimmering." +
                        "I can even see your breath moving..."
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses03",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "完美。视觉突变成功。##" +
                        "看来你比我想象中更耐受。也许……下一阶段可以提前。"
                    },
                    {
                        ModLanguage.English,
                        "Perfect. Visual mutation confirmed.##" +
                        "You’re more resilient than I expected. Perhaps... the next phase can be advanced."
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses03_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "下一阶段？" +
                        "你还打算对我做什么？"
                    },
                    {
                        ModLanguage.English,
                        "Next phase?" +
                        "What else are you planning to do to me?"
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses04",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "别误会。##" +
                        "你现在需要的是稳定，而不是更多痛苦。##" +
                        "喝下这个——它会抑制体内的化学风暴。"
                    },
                    {
                        ModLanguage.English,
                        "Don’t misunderstand.##" +
                        "What you need now is stability, not more pain.##" +
                        "Drink this—it’ll quiet the chemical storm inside you."
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses04_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "（金属杯落在木桌上，液体声）" +
                        "味道像……铁和灰。"
                    },
                    {
                        ModLanguage.English,
                        "(Sound of a metal cup on wood, liquid sloshing)" +
                        "Tastes like... iron and ash."
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses05",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "那是生命在被重写的味道。##" +
                        "别急着起身。##" +
                        "你的神经还在判断自己是否属于这个身体。"
                    },
                    {
                        ModLanguage.English,
                        "That’s the taste of life being rewritten.##" +
                        "Don’t rush to stand.##" +
                        "Your nerves are still deciding whether they belong to this body."
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses05_pc",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "……你打算拿我做什么？猎魔人？怪物？还是别的东西？"
                    },
                    {
                        ModLanguage.English,
                        "...What do you plan to make of me? Witcher? Monster? Something else?"
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses06",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "定义是给哲学家用的。##" +
                        "而你——是我的证明。##" +
                        "证明理智比神明更擅长创造。"
                    },
                    {
                        ModLanguage.English,
                        "Definitions are for philosophers.##" +
                        "You, however, are my proof.##" +
                        "Proof that reason outperforms gods at creation."
                    }
                }
            ),

            new LocalizationSentence(
                id: "afterTrialOfGrasses07",
                sentence: new Dictionary<ModLanguage, string>
                {
                    {
                        ModLanguage.Chinese,
                        "你先休息吧。##" +
                        "这具身体尚且陌生，需要时间与经验去磨合。##" +
                        "等你真正掌握它的力量后，才有资格参与我的研究计划。##" +
                        "另外——我这里有几种专为这种身体调制的炼金药水与配方，" +
                        "还有关于魔法潜能的研究笔记。若你感兴趣……可以花点钱买。"
                    },
                    {
                        ModLanguage.English,
                        "Rest for now.##" +
                        "This body is still unfamiliar—you’ll need time and experience to make it your own.##" +
                        "Once you’ve truly mastered its power, you may earn the right to join my research.##" +
                        "Oh, and—I happen to have a few alchemical potions and formulas tailored for that body, " +
                        "along with some notes on its magical potential. If you’re interested... they’re for sale."
                    }
                }
            )
        );
    }
}
