with (owner)
{
    var _oil_arr = scr_hoversGetCoatingOilAttributes()
    if (array_length(_oil_arr) > 0)
    {
        other.coatingOilText = ds_map_find_value(global.consum_name, _oil_arr[0])
        other.coatingOilCount = _oil_arr[1]
        other.coatingOilMaxCount = _oil_arr[2]
        other.coatingOilRight = string_join("/", _oil_arr[1], _oil_arr[2])
        other.coatingOilAttributesArray = _oil_arr
    }
}

coatingOilHeight = 0
var _coatingOilAttributesArrayLength = array_length(coatingOilAttributesArray)
if (_coatingOilAttributesArrayLength > 0)
{
    coatingOilHeight = fontDmgHeight + spaceHeight
    _linesHeight += coatingOilHeight
}
