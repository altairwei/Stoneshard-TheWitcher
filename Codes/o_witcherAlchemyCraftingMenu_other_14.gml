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
else if (idName == "alcohol")
{
    _target = scr_craft_alcohol(true)
}
else if _object
{
    var _size = array_length(item_array)
    _char_num = 0
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
