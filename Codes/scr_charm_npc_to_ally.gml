function scr_charm_npc_to_ally()
{
    if (state == "transition")
        exit;
    
    if (state == "jailed")
        exit;

    if (is_neutral)
    {
        is_neutral = false;

        event_user(7);
        is_player_enemy = false;

        state = "idle";
        scr_update_FSM_NPC();
        
        if (ai_script == scr_enemy_choose_state_npc_prey)
        {
            scr_stateFleeAbs();
        }
        else
        {
            scr_stateAttackAbs();
        }

        if (fight_sprite != noone)
            sprite_index = fight_sprite;

        other.target_fraction = faction_key
        other.target_subfraction = subfaction_key
        faction_key = "Charmed"
        subfaction_key = "Charmed"

        scr_add_to_enemy_list(other.target_subfraction)
    }
}