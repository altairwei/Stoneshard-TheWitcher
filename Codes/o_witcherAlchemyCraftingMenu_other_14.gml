with (consumsContainer)
    contentType = o_inv_slot
var _object = asset_get_index("o_inv_" + idName)
var _data = ds_map_find_value(global.recipes_witcher_alchemy_data, idName)
var _char_num = 0
var _target = noone
var _xp = 0
if _object
{
    var _count = max(1, string_to_real(ds_map_find_value(_data, "AMOUNT")))
    repeat _count
    {
        with (scr_inventory_add_item(_object, id, noone, true, noone, false))
        {
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
    var _size = array_length(item_array)
    _char_num = 0
    for (var i = 0; i < _size; i++)
    {
        with (item_array[i])
        {
            if (object_index == o_inv_craftkit)
            {
                if (!other.workbench)
                {
                    charge--
                    if (charge <= 0)
                        instance_destroy()
                }
            }
            else
                instance_destroy()
        }
    }
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
