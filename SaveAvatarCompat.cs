using ModShardLauncher;
using ModShardLauncher.Mods;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    // save.map.avatar 永远存原版 sprite 名(卸载后存档页不崩溃), mod 原名另存 mod_avatar;
    // mod 在场时由显示层用 mod_avatar 还原 mod 头像, 卸载后这段代码不存在, 读到的就是原版名。
    private void SaveAvatarCompat()
    {
        // 1) 写侧清洗: 挂在 save.map 的唯一写入出口上(手动存档/损坏重建/读档置失效都走这里),
        //    任何路径写出的存档都是安全的原版名
        Msl.LoadGML("gml_GlobalScript_scr_slotSaveMapSave")
            .MatchFrom("var _salt = scr_slotSaveGetSalt(")
            .InsertAbove(@"
var _avatarRawName = ds_map_find_value(argument2, ""avatar"")
if (is_string(_avatarRawName))
{
    var _avatarSafeName = scr_mod_avatar_fallback(_avatarRawName)
    if (_avatarSafeName != _avatarRawName)
    {
        ds_map_set(argument2, ""mod_avatar"", _avatarRawName)
        ds_map_set(argument2, ""avatar"", _avatarSafeName)
    }
}")
            .Save();

        // 2) 显示层还原: 存档槽渲染时用 mod_avatar 还原 mod 头像(save.map 已加载, 不额外读盘)
        Msl.LoadGML("gml_GlobalScript_scr_saveMenuSaveCreate")
            .MatchFrom("var _avatarSprite = __asset_get_index(ds_map_find_value(_saveMap, \"avatar\"))")
            .InsertBelow(@"
var _modAvatarName = ds_map_find_value_ext(_saveMap, ""mod_avatar"", """")
if (is_string(_modAvatarName) && _modAvatarName != """")
{
    var _modAvatarSprite = __asset_get_index(_modAvatarName)
    if (sprite_exists(_modAvatarSprite))
        _avatarSprite = _modAvatarSprite
}")
            .Save();
    }
}
