function scr_craft_oil()
{
    var arg0 = false
    if (!is_undefined(argument0))
        arg0 = argument0

    return scr_craft_alchemy_base([o_inv_lentils, 2, o_inv_meat_fat_raw, 1], o_inv_oil, arg0)
}
