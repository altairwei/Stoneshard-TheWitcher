with (consumsContainer)
    contentType = o_inv_slot
var _object = asset_get_index("o_inv_" + idName)
var _data = ds_map_find_value(global.recipes_witcher_alchemy_data, idName)
var _char_num = 0
var _target = noone
var _xp = 0

if (idName == "oil")
{
    _target = scr_craft_oil(true)
}
else if (idName == "alcohol_essentia")
{
    _target = scr_craft_alcohol_essentia(true)
}
else if _object
{
    var _size = array_length(item_array)
    var _char_map = __dsDebuggerMapCreate()

    for (var i = 0; i < _size; i++)
    {
        with (item_array[i])
        {
            switch (_object)
            {
                case o_inv_swallow_potion:
                    switch (object_index)
                    {
                        case o_inv_hyssop:
                            ds_map_accumulate(_char_map, "max_hp_res", 50)
                            ds_map_accumulate(_char_map, "Health_Restoration", 80)
                        break;

                        case o_inv_spleenwort:
                            ds_map_accumulate(_char_map, "Condition", 45)
                            ds_map_accumulate(_char_map, "Bleeding_Resistance", 50)
                        break;
                    }
                break;

                case o_inv_blizzard_potion:
                    switch (object_index)
                    {
                        case o_inv_hornet_honey:
                            ds_map_accumulate(_char_map, "Hit_Chance", 25)
                            ds_map_accumulate(_char_map, "CTA", 50)
                        break;

                        case o_inv_bear_fat:
                            ds_map_accumulate(_char_map, "PRR", 25)
                            ds_map_accumulate(_char_map, "CTA", 25)
                        break;

                        case o_inv_truffle:
                            ds_map_accumulate(_char_map, "FMB", -25)
                            ds_map_accumulate(_char_map, "Mainhand_Efficiency", 25)
                        break;
                    }
                break;

                case o_inv_thunderbolt_potion:
                    switch (object_index)
                    {
                        case o_inv_hop:
                            ds_map_accumulate(_char_map, "Weapon_Damage", 25)
                            ds_map_accumulate(_char_map, "CRTD", 50)
                        break;

                        case o_inv_rotbloom:
                            ds_map_accumulate(_char_map, "FMB", -25)
                            ds_map_accumulate(_char_map, "CRT", 15)
                        break;
                    }
                break;

                case o_inv_petri_philter:
                    switch (object_index)
                    {
                        case o_inv_moose_kidney:
                            ds_map_accumulate(_char_map, "Manasteal", 25)
                            ds_map_accumulate(_char_map, "Miracle_Power", 50)
                        break;

                        case o_inv_azurecap:
                            ds_map_accumulate(_char_map, "Magic_Power", 25)
                            ds_map_accumulate(_char_map, "Miracle_Chance", 25)
                            ds_map_accumulate(_char_map, "Cooldown_Reduction", -25)
                        break;
                    }
                break;

                case o_inv_golden_oriole:
                    switch (object_index)
                    {
                        case o_inv_spidervine:
                            ds_map_accumulate(_char_map, "Physical_Resistance", 15)
                            ds_map_accumulate(_char_map, "Stun_Resistance", 30)
                            ds_map_accumulate(_char_map, "Bleeding_Resistance", 30)
                        break;

                        case o_inv_puffball:
                            ds_map_accumulate(_char_map, "Nature_Resistance", 30)
                            ds_map_accumulate(_char_map, "Magic_Resistance", 30)
                            ds_map_accumulate(_char_map, "Pain_Resistance", 30)
                        break;

                        case o_inv_dragonfruit:
                            ds_map_accumulate(_char_map, "Received_XP", 50)
                            ds_map_accumulate(_char_map, "Crit_Avoid", 50)
                        break;
                    }
                break;

                case o_inv_tawny_owl:
                    switch (object_index)
                    {
                        case o_inv_silverleaf:
                            ds_map_accumulate(_char_map, "Fatigue", -25)
                            ds_map_accumulate(_char_map, "Cooldown_Reduction", 30)
                        break;

                        case o_inv_wolf_tongue:
                            ds_map_accumulate(_char_map, "max_mp_res", 25)
                            ds_map_accumulate(_char_map, "Abilities_Energy_Cost", -25)
                        break;
                    }
                break;
            }

            instance_destroy()
        }
    }

    var _count = max(1, string_to_real(ds_map_find_value(_data, "AMOUNT")))
    repeat _count
    {
        with (scr_inventory_add_item(_object, id, noone, true, noone, false))
        {
            // Add additional attributes
            _size = ds_map_size(_char_map)
            var _key = ds_map_find_first(_char_map)

            _char_num = 0
            for (var i = 0; i < _size; i++)
            {
                var _value = ds_map_find_value(_char_map, _key)
                _char_num = scr_consum_char_add(_key, _value, _char_num)
                _key = ds_map_find_next(_char_map, _key)
            }

            sh_diss = 200
            _target = id

            ds_map_replace(data, "i_index", i_index)

            if (!(scr_inventory_add(other.id, id, [other.consumsContainer])))
            {
                forced_drop = true
                event_user(15)
                _target = loot_object
            }
        }
    }

    __dsDebuggerMapDestroy(_char_map)

    _xp = string_to_real(ds_map_find_value(_data, "XP"))
}

with (o_player)
{
    with (scr_guiAnimation(s_gui_anim_craft))
        ownerAlpha = false
    scr_audio_play_at(snd_skill_craft)
}

with (consumsContainer)
    contentType = o_loot

if (_xp > 0)
{
    scr_get_XP(_xp)
    scr_actionsLogXP("craft", [scr_id_get_name(o_player), scr_id_get_name(_target)], _xp)
}
else
    scr_actionsLog("craft", [scr_id_get_name(o_player), scr_id_get_name(_target)])

scr_characterStatsUpdateAdd("craftedItems", 1)
event_inherited()
