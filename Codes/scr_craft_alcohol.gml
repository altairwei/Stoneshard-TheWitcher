function scr_craft_alcohol()
{
    var arg0 = false
    if (!is_undefined(argument0))
        arg0 = argument0

    var _items = [
        o_inv_spirit, 1,
        o_inv_brandy, 1,
        o_inv_ale, 4,
        o_inv_mead, 2,
        o_inv_wine, 2,
        o_inv_wine1, 2,
        o_inv_wine2, 2
    ]

    return scr_craft_alchemy_base(_items, o_inv_alcohol, arg0)
}