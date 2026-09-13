function scr_mod_save_avatar_purge()
{
    var arg0 = argument[0]
    if (!scr_slotExists(arg0))
        return;
    var _orderList = scr_slotSavesGetOrderList(arg0);
    var _orderListSize = ds_list_size(_orderList);
    for (var _i = 0; _i < _orderListSize; _i++)
    {
        var _saveDirName = ds_list_find_value(_orderList, _i);
        if (!scr_slotSaveExists(arg0, _saveDirName))
            continue;
        var _saveMap = scr_slotSaveMapLoad(arg0, _saveDirName);
        if (_saveMap == -4)
            continue;
        var _avatarName = ds_map_find_value(_saveMap, "avatar");
        // 无条件清洗注册表内的 mod 资源名:改写为各 mod 指定的原版回退名,不管 sprite 当前是否存在。
        // 只改 save.map(卸载后存档页读它),data.sav 不动(mod 在场时玩家属性仍是原值)。
        if (is_string(_avatarName))
        {
            var _fallbackName = scr_mod_avatar_fallback(_avatarName);
            if (_fallbackName != _avatarName)
            {
                ds_map_set(_saveMap, "avatar", _fallbackName);
                scr_slotSaveMapSave(arg0, _saveDirName, _saveMap);
            }
        }
        _saveMap = __dsDebuggerMapDestroy(_saveMap);
    }
    _orderList = __dsDebuggerListDestroy(_orderList);
}
