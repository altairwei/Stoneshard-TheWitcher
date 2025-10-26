using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddPerk_BlavikenButcher()
    {
        UndertaleGameObject o_perk_blaviken_butcher = Msl.GetObject("o_perk_blaviken_butcher");

        o_perk_blaviken_butcher.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
            "),

            // 叠层效果
            new MslEvent(eventType: EventType.Other, subtype: 14, code: @"
                event_inherited()

                var _buffs = [
                    ""Weapon_Damage"",          10,
                    ""CRTD"",                   20,
                    ""Magic_Power"",            10,
                    ""Miracle_Power"",          20,
                    ""Piercing_Resistance"",    -5
                ]

                for (var i = 0; i < array_length(_buffs); i += 2)
                {
                    with (scr_temp_incr_atr(_buffs[i], _buffs[i+1], 10, o_player, o_player, true, false))
                        can_save = false
                }
                
                scr_atr_calc(o_player)
            "),

            new MslEvent(eventType: EventType.Other, subtype: 16, code: @"
                var _enchant_names = []
                var _enchant_values = []
                var _boss_id = """"
                with (EnemyId)
                {
                    _boss_id = object_get_name(object_index)
                    switch (faction_key)
                    {
                        case ""Brigand"":
                            array_push(_enchant_names, ""CRT"", ""Cooldown_Reduction"")
                            array_push(_enchant_values, 0.5 * Tier, -0.5 * Tier)
                            break;
                        case ""Vampire"":
                            array_push(_enchant_names, ""Miracle_Chance"", ""Lifesteal"")
                            array_push(_enchant_values, 1.2 * Tier, 0.5 * Tier)
                            break;
                        case ""Undead"":
                            array_push(_enchant_names, ""PRR"", ""Manasteal"")
                            array_push(_enchant_values, 0.7 * Tier, 0.5 * Tier)
                            break;
                        case ""Hive"":
                        case ""carnivore"":
                            array_push(_enchant_names, ""Damage_Received"")
                            array_push(_enchant_values, -1 * Tier)
                            break;
                    }
                }

                if (array_length(_enchant_names) == 0)
                    exit;

                with (o_inv_witcher_medallion_wolf)
                {
                    if (!equipped)
                        exit;

                    if (ds_list_find_index(ds_map_find_value(data, ""uniqueBossKill""), _boss_id) != -1)
                        exit;
                    else
                        ds_list_add(ds_map_find_value(data, ""uniqueBossKill""), _boss_id)

                    for (var m = 0; m < array_length(_enchant_names); m++)
                    {
                        var _name = _enchant_names[m]
                        var _value = _enchant_values[m]

                        var _char_index = -1
                        var _current_char_count = 0

                        i = 0
                        while (i < 10)
                        {
                            if (!(__is_undefined(ds_map_find_value(data, (""Char"" + string(i))))))
                            {
                                _current_char_count++
                                i++
                            }
                            else
                                break
                        }

                        if (!(__is_undefined(ds_map_find_value(data, _name))))
                        {
                            var j = 0
                            while (j < 10)
                            {
                                var _key = ""Char"" + string(j)
                                var _char = ds_map_find_value(data, _key)

                                if __is_undefined(_char)
                                    break
                                else if (string_pos(_name, _char) != 0)
                                {
                                    var _ov = ds_map_find_value(data, _name)
                                    ds_map_delete(data, _name)
                                    ds_map_delete(data, _key)
                                    _char_index = j
                                    scr_consum_char_add(_name, _ov + _value, _char_index, false)
                                    break
                                }
                                else
                                    j++
                            }
                        }
                        else
                        {
                            _char_index = _current_char_count
                            scr_consum_char_add(_name, _value, _char_index, false)
                        }
                    }

                    scr_random_speech(""killBossGeralt"", 100)
                }
            ")
        );

        Msl.AddNewEvent(objectName: "o_skill_trial_of_grasses", eventType: EventType.Alarm, subtype: 4, eventCode: @"
            if instance_exists(o_perk_blaviken_butcher)
                scr_skill_open(id)
        ");

        Msl.InjectTableSkillsLocalization(
            new LocalizationSkill(
                id: "blaviken_butcher",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Blaviken Butcher"},
                    {ModLanguage.Chinese, "布拉维坎的屠夫"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "For each enemy killed, receive ~lg~+10%~/~ Weapon Damage and Magic Power, ~lg~+20%~/~ Crit Efficiency and Miracle Power, ~r~-5%~/~ Piercing Resistance for ~w~10~/~ turns. This effect stacks.##" +
                        "Starts the game with ~w~\"Trial Of Grasses\"~/~ already learned and a ~y~Wolf School Medallion~/~ " +
                        "when worn, killing a Mini-Boss for the first time grants the ~y~Wolf School Medallion~/~ various ~lg~enhancements~/~."},
                    {ModLanguage.Chinese, "每杀一个敌人，兵器伤害与法力便~lg~+10%~/~，暴击效果与奇观效果便~lg~+20%~/~，同时穿刺抗性~r~-5%~/~，效果存续~w~10~/~回合。这个效果可以叠加。##" +
                        "游戏开局习得~w~“青草试炼”~/~，还有一个~y~狼学派徽章~/~，佩戴时首次击杀关底头目会令~y~狼学派徽章~/~获得各种~lg~加成~/~。"}
                }
            )
        );

        int index = DataLoader.data.GameObjects.IndexOf(
            DataLoader.data.GameObjects.First(x => x.Name.Content == "o_perk_suum_cuique"));
        Msl.LoadAssemblyAsString("gml_Object_o_enemy_Destroy_0")
            .MatchFrom($"pushi.e {index}")
            .InsertAbove(@"
pushi.e o_perk_blaviken_butcher
pushenv [1001]

:[1000]
push.v other.id
pop.v.v self.EnemyId
pushi.e 6
conv.i.v
call.i event_user(argc=1)
popz.v

:[1001]
popenv [44]")
            .Save();
    }

    private void AddPerk_ProfessionalWitcher()
    {
        UndertaleGameObject o_perk_professional_witcher = Msl.GetObject("o_perk_professional_witcher");

        o_perk_professional_witcher.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                scr_atr_add_simple(""PotionRewarded"", 0)
                scr_atr_add_simple(""PotionBonus"", 0)

                if (__is_undefined(scr_atr(""PotionTypeList"")))
                    scr_atr_add_list(""PotionTypeList"", __dsDebuggerListCreate())

                scr_atr_add_simple(""DecoctionRewarded"", 0)
                scr_atr_add_simple(""DecoctionBonus"", 0)

                if (__is_undefined(scr_atr(""DecoctionTypeList"")))
                    scr_atr_add_list(""DecoctionTypeList"", __dsDebuggerListCreate())

                item = noone
            "),

            // 每次喝完魔药或煎药都检查一下
            new MslEvent(eventType: EventType.Other, subtype: 15, code: @"
                var _object_name = object_get_name(item)

                if (object_is_ancestor(item, o_inv_witcher_potion))
                {
                    var _limits = scr_atr(""LVL"") div 5
                    var _bonus = scr_atr(""PotionBonus"")
                    if ((_limits - _bonus) > 0)
                    {
                        var _potionTypeList = scr_atr(""PotionTypeList"")
                        if (ds_list_find_index(_potionTypeList, _object_name) == -1)
                        {
                            ds_list_add(_potionTypeList, _object_name)
                            scr_atr_incr(""PotionRewarded"", 1)
                            
                            if (scr_atr(""PotionRewarded"") == 5)
                            {
                                scr_random_speech(""geraltPerkRewarded"", 100)
                                ds_list_clear(_potionTypeList)
                                scr_atr_set(""PotionRewarded"", 0)
                                scr_atr_incr(""PotionBonus"", 1)
                                scr_atr_incr(""AP"", 1)
                            }
                            else
                            {
                                scr_random_speech(""geraltPerkProgressed"", 100)
                            }
                        }
                    }
                    else
                    {
                        scr_random_speech(""geraltPerkUndigested"", 30)
                    }
                }
                else if (object_is_ancestor(item, o_inv_witcher_decoction))
                {
                    var _limits = scr_atr(""LVL"") div 5
                    var _bonus = scr_atr(""DecoctionBonus"")
                    if ((_limits - _bonus) > 0)
                    {
                        var _decoctionTypeList = scr_atr(""DecoctionTypeList"")
                        if (ds_list_find_index(_decoctionTypeList, _object_name) == -1)
                        {
                            ds_list_add(_decoctionTypeList, _object_name)
                            scr_atr_incr(""DecoctionRewarded"", 1)
                            
                            if (scr_atr(""DecoctionRewarded"") == 5)
                            {
                                scr_random_speech(""geraltPerkRewarded"", 100)
                                ds_list_clear(_decoctionTypeList)
                                scr_atr_set(""DecoctionRewarded"", 0)
                                scr_atr_incr(""DecoctionBonus"", 1)
                                scr_atr_incr(""SP"", 1)
                            }
                            else
                            {
                                scr_random_speech(""geraltPerkProgressed"", 100)
                            }
                        }
                    }
                    else
                    {
                        scr_random_speech(""geraltPerkUndigested"", 30)
                    }
                }
            ")
        );

        Msl.AddNewEvent(objectName: "o_skill_trial_of_grasses", eventType: EventType.Alarm, subtype: 4, eventCode: @"
            if instance_exists(o_perk_professional_witcher)
                scr_skill_open(id)
        ");

        Msl.InjectTableSkillsLocalization(
            new LocalizationSkill(
                id: "professional_witcher",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Professional Witcher"},
                    {ModLanguage.Chinese, "猎魔大师"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English,
                     "WIP"},
                    {ModLanguage.Chinese,
                     "每习得一项剑术（双手剑或单手剑）能力，兵器伤害便~lg~+2%~/~，暴击几率便~lg~+1%~/~。##" +
                     "角色每升五级，喝下~w~5~/~瓶不同的魔药后便获得~lg~1~/~个属性点，喝下~w~5~/~瓶不同的煎药后便获得~lg~1~/~个能力点。"}
                }
            )
        );

        Msl.InjectTableSpeechesLocalization(
            new LocalizationSpeech(
                id: "geraltPerkRewarded",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Feels... different. Stronger, maybe."},
                    {ModLanguage.Chinese, "感觉……不一样了。或许更强了。"}
                },

                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Power’s settling in. Guess the pain was worth it."},
                    {ModLanguage.Chinese, "力量在体内安定下来了。看来那些痛苦没白受。"}
                },

                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Another lesson learned in blood and alchemy."},
                    {ModLanguage.Chinese, "又上一课，用血与炼金换来的。"}
                }
            ),

            new LocalizationSpeech(
                id: "geraltPerkProgressed",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Another mix... body’s adapting, slowly but surely."},
                    {ModLanguage.Chinese, "又一瓶……身体在慢慢适应。"}
                },

                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Different brew, same burn. Guess that counts for progress."},
                    {ModLanguage.Chinese, "换了种药，灼烧感还是一样。算是进步吧。"}
                },

                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "The mutations never stop changing... nor do I."},
                    {ModLanguage.Chinese, "突变从未停止……我也是。"}
                }
            ),

            new LocalizationSpeech(
                id: "geraltPerkUndigested",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Not yet... body’s not ready for more."},
                    {ModLanguage.Chinese, "还不行……身体还没准备好。"}
                },

                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Need more time... or less poison."},
                    {ModLanguage.Chinese, "还需要时间……或者少点毒素。"}
                },

                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Can’t rush evolution. Not if I want to stay alive."},
                    {ModLanguage.Chinese, "进化急不得，除非我不想活了。"}
                }
            )
        );
    }
}