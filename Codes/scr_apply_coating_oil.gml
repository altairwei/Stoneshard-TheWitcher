function scr_apply_coating_oil()
{
    ds_map_replace(data, "coating_oil", argument0)
    ds_map_replace(data, "oil_available_count", 20)
    ds_map_replace(data, "oil_available_maxcount", 20)
}
