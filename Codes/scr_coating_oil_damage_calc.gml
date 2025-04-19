function scr_coating_oil_damage_calc()
{
    var _target = argument0
    var _damage = argument1

    if (!is_player())
        return _damage;

    var _oil = ""
    var _count = 0

    with (scr_get_weapon_id_equipped())
    {
        _oil = ds_map_find_value_ext(data, "coating_oil", "")
        _count = ds_map_find_value_ext(data, "oil_available_count", 0)
        if (_count > 0)
            ds_map_replace(data, "oil_available_count", _count - 1)
    }

    if (_count < 1)
        return _damage;

    var _multiplier = 1

    switch (_oil)
    {
        case "hanged_man_venom":
            switch (_target.typeID)
            {
                case "human":
                case "elf":
                case "dwarf":
                    _multiplier = 1.2
                break;
            }
            break;
        case "vampire_oil":
            if (_target.typeID == "vampire")
                _multiplier = 1.2
            break;
    }

    if (_multiplier > 1)
        scr_actionsLogUpdate("Target:" + object_get_name(_target.object_index))

    return math_round(_damage * _multiplier)
}
