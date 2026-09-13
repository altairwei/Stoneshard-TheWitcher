using ModShardLauncher;
using ModShardLauncher.Mods;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    // save.map.avatar 落盘前经 scr_mod_avatar_fallback 改写为原版 sprite 名(卸载后存档页不崩溃);
    // 映射表 global.mod_save_avatars 开放注册, 其他角色 mod 登记后自动被同一套清洗覆盖。
    private void SaveAvatarCompat()
    {
        // 1) 写侧清洗: 每次存档时把 mod 资源名改写为原版回退名(只动 save.map)
        Msl.LoadGML("gml_GlobalScript_scr_slotSaveUpdate")
            .MatchFrom("ds_map_set(_saveMap, \"avatar\", ds_map_find_value(global.characterDataMap, \"avatar\"))")
            .InsertBelow(@"
ds_map_set(_saveMap, ""avatar"", scr_mod_avatar_fallback(ds_map_find_value(_saveMap, ""avatar"")))")
            .Save();

        // 2) 存量档净化: 读档时清洗旧档里已落盘的 mod 资源名
        Msl.LoadGML("gml_GlobalScript_scr_loadGame")
            .MatchFrom("global.floor_counter = scr_atr")
            .InsertAbove(@"
for (var _si = 1; _si <= 10; _si++)
    scr_mod_save_avatar_purge(""character_"" + string(_si))")
            .Save();
    }
}
