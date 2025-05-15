function scr_mod_apply_axii_charm_in_dialog()
{
    // TODO: 使技能消耗
    audio_play_sound(snd_skill_sealofinsight_startcast, 1, 0)
    var _charm_chance = 5 * o_player.WIL - owner.Psionic_Resistance

    if (_charm_chance < 100)
    {
        var _gx = global.playerGridX
        var _gy = global.playerGridY
        var _rep = scr_globaltile_reputation_calc(round(_charm_chance - 100))
        scr_globaltile_reputation_update(_rep, _gx, _gy)
        scr_globaltile_reputation_log(scr_settlementReputationLogCreate("dialogue", _rep), _gx, _gy)
    }

    with (owner)
    {
        var _timestamp = scr_timeGetTimestamp()
        ds_map_add(data, "axii_charmed_time", _timestamp)
    }
}