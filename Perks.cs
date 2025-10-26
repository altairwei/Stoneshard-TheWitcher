using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
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
}