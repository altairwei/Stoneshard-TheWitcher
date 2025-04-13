event_inherited()
with (o_inv_slot)
{
    if (owner.object_index != o_trade_inventory && ds_map_find_value(data, "Metatype") == "Weapon")
        image_alpha = 1
    else
        image_alpha = 0.25
    can_pick = false
}
