// 通用 mod 人物头像回退映射。
// 注册表 global.mod_save_avatars 由各色角色 mod 自行登记(不存在则创建,追加不覆盖):
//   if (!variable_global_exists("mod_save_avatars"))
//       global.mod_save_avatars = []
//   array_push(global.mod_save_avatars, ["s_Ciri", "s_Jonna"])   // [mod sprite 名, 原版回退名]
// 传入 avatar 名,若在注册表则返回对应的原版回退名,否则原样返回。
function scr_mod_avatar_fallback()
{
    var arg0 = argument[0]
    if (variable_global_exists("mod_save_avatars"))
    {
        for (var _r = 0; _r < array_length(global.mod_save_avatars); _r++)
        {
            var _entry = global.mod_save_avatars[_r]
            if (arg0 == _entry[0])
                return _entry[1]
        }
    }
    return arg0
}
