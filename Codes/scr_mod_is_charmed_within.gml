function scr_mod_is_charmed_within()
{
    with (owner)
    {
        var _timestamp = ds_map_find_value(data, "axii_charmed_time")
        if (is_undefined(_timestamp))
            return false
        else
        {
            var _hoursPassed = scr_timeGetPassed(_timestamp, 2)
            if (_hoursPassed <= argument0)
                return true
        }
    }

    return false
}