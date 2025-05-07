function scr_coating_oil_damage_calc()
{
    var _target = argument1
    var _damage = argument0
    var _oil_damage = 0

    if (!is_player())
        return 0;

    if (!object_is_ancestor(_target.object_index, o_unit))
        return 0;

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
        return _oil_damage;

    switch (_oil)
    {
        case "hanged_man_venom":
            switch (_target.typeID)
            {
                case "human":
                case "elf":
                case "dwarf":
                    _oil_damage = _damage * 0.2
                break;
            }
            break;
        case "vampire_oil":
            if (_target.typeID == "vampire")
                _oil_damage = _damage * 0.2
            break;
        case "necrophage_oil":
            if (_target.typeID == "undead")
                _oil_damage = _damage * 0.2
            break;
        case "specter_oil":
            if (_target.typeID == "spectre")
                _oil_damage = _damage * 0.4
            break;
        case "insectoid_oil":
            if (_target.object_index == o_crawler || _target.object_index == o_hornets)
                _oil_damage = _damage * 0.4
            break;
        case "hybrid_oil":
            if (_target.object_index == o_crawler || _target.object_index == o_hornets)
                _oil_damage = _damage * 0.3
            break;
        case "ogroid_oil":
            if (_target.object_index == o_small_troll || _target.object_index == o_ancientTroll)
                _oil_damage = _damage * 0.3
            break;
    }

    if (_oil_damage > 1)
        scr_actionsLogUpdate("Target:" + object_get_name(_target.object_index))

    return math_round(_oil_damage)
}
