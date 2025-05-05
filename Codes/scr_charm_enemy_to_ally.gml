function scr_charm_enemy_to_ally()
{
    is_player_enemy = false

    state = "idle"

    other.target_fraction = faction_key
    other.target_subfraction = subfaction_key
    faction_key = "Player"
    subfaction_key = "Player"

    scr_delete_from_enemy_list("Player")
    scr_add_to_enemy_list(other.target_subfraction)

    scr_enemy_choose_state_new()
}