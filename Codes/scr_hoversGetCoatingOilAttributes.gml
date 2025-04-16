function scr_hoversGetCoatingOilAttributes()
{
    var _oil = ds_map_find_value_ext(data, "coating_oil", "")
    var _count = ds_map_find_value_ext(data, "oil_available_count", 0)
    var _max = ds_map_find_value_ext(data, "oil_available_maxcount", 0)

    if (_oil != "" && _count > 0)
        return [_oil, _count, _max]

    return []
}