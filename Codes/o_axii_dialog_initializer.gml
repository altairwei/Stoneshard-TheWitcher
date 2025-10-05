var _Fragments = variable_struct_get(global.__dialogue_flow_data.npc_bandit_fence, "Fragments")
var _Scripts = variable_struct_get(global.__dialogue_flow_data.npc_bandit_fence, "Scripts")
var _Specs = variable_struct_get(global.__dialogue_flow_data.npc_bandit_fence, "Specs")

_Fragments.npc_bandit_fence = "condition_CND_inspection"
_Fragments.condition_CND_inspection = ["Cnd_inspection_positive", "Cnd_inspection_negative"]
_Specs.Cnd_inspection_positive = { hub: true }
_Specs.Cnd_inspection_negative = { hub: true }
_Fragments.Cnd_inspection_negative = "condition_CND_92CC01CD"
_Fragments.Cnd_inspection_positive = "npc_bandit_fence_axii_charm_inspection"
_Fragments.npc_bandit_fence_axii_charm_inspection = "@dialogue_end"
_Scripts.condition_CND_inspection = function()
{
    var _is_charmed = scr_mod_is_charmed_within(4)
    with (owner)
        ds_map_delete(data, "axii_charmed_time")
    return _is_charmed
}

_Fragments.HUB_Cnd_85031BD8_negative = ["npc_bandit_fence_axii_charm", "leave"]
_Scripts.embedded_npc_bandit_fence_axii_charm = function() { return o_skill_axii_sign_ico.is_open }

_Fragments.npc_bandit_fence_axii_charm = "instruction_INS_applyAxiiSign"
_Fragments.instruction_INS_applyAxiiSign = "npc_bandit_fence_axii_was_charmed"
_Specs.instruction_INS_applyAxiiSign = { action: true }
_Scripts.instruction_INS_applyAxiiSign = asset_get_index("scr_mod_apply_axii_charm_in_dialog")
_Fragments.npc_bandit_fence_axii_was_charmed = "instruction_INS_openTheDoor"
_Fragments.instruction_INS_openTheDoor = "@dialogue_end"
_Specs.instruction_INS_openTheDoor = { action: true }
_Scripts.instruction_INS_openTheDoor = function() { scr_smoothRoomChange(r_FenceHideout1floor, [4], -1, true) }


// Skinflint Homs
_Fragments = variable_struct_get(global.__dialogue_flow_data.npc_fence, "Fragments")
_Scripts = variable_struct_get(global.__dialogue_flow_data.npc_fence, "Scripts")
_Specs = variable_struct_get(global.__dialogue_flow_data.npc_fence, "Specs")

_Fragments.npc_fence = "condition_CND_alreadyCharmed"
_Fragments.condition_CND_alreadyCharmed = ["Cnd_alreadyCharmed_true", "Cnd_alreadyCharmed_false"]
_Specs.Cnd_alreadyCharmed_true = { hub: true }
_Specs.Cnd_alreadyCharmed_false = { hub: true }
_Fragments.Cnd_alreadyCharmed_true = "skinflint_homs_was_charmed"
_Scripts.condition_CND_alreadyCharmed = function() { return scr_mod_is_charmed_within(2) }

_Fragments.Cnd_alreadyCharmed_false = "condition_CND_alreadyKnown"
_Fragments.condition_CND_alreadyKnown = ["Cnd_alreadyKnown_positive", "Cnd_alreadyKnown_negative"]
_Specs.Cnd_alreadyKnown_positive = { hub: true }
_Specs.Cnd_alreadyKnown_negative = { hub: true }
_Fragments.Cnd_alreadyKnown_positive = "condition_CND_0FD35A55"
_Scripts.condition_CND_alreadyKnown = function() { return scr_dialogue_complete("banditFenceHideout00_pc") }

_Fragments.Cnd_alreadyKnown_negative = "skinflint_homs_dont_know_player"
_Fragments.skinflint_homs_dont_know_player = ["skinflint_homs_charm_pc", "leave"]
_Fragments.skinflint_homs_charm_pc = "instruction_CND_applyAxiiSign"
_Fragments.instruction_CND_applyAxiiSign = "skinflint_homs_was_charmed"
_Specs.instruction_CND_applyAxiiSign = { action: true }
_Scripts.instruction_CND_applyAxiiSign = asset_get_index("scr_mod_apply_axii_charm_in_dialog")

_Fragments.skinflint_homs_was_charmed = "instruction_INS_openTrade"
_Fragments.instruction_INS_openTrade = "@dialogue_end"
_Specs.instruction_INS_openTrade = { action: true }
_Scripts.instruction_INS_openTrade = function() { scr_dialogue_open_trade() }

// Steal
_Fragments = variable_struct_get(global.__dialogue_flow_data.npc_spot_steal_loot, "Fragments")
_Scripts = variable_struct_get(global.__dialogue_flow_data.npc_spot_steal_loot, "Scripts")
_Specs = variable_struct_get(global.__dialogue_flow_data.npc_spot_steal_loot, "Specs")

array_push(_Fragments.player_thieveryReaction, "player_thieveryReaction_axii_charm_pc")
_Fragments.player_thieveryReaction_axii_charm_pc = "instruction_INS_applyAxiiSign"
_Scripts.instruction_INS_applyAxiiSign = asset_get_index("scr_mod_apply_axii_charm_in_dialog")
_Specs.instruction_INS_applyAxiiSign = { action: true }
_Fragments.instruction_INS_applyAxiiSign = "player_thieveryReaction_axii_charm"
_Fragments.player_thieveryReaction_axii_charm = "@dialogue_end"

// Check you inventory
_Fragments = variable_struct_get(global.__dialogue_flow_data.npc_guard_crime_inv_check, "Fragments")
_Scripts = variable_struct_get(global.__dialogue_flow_data.npc_guard_crime_inv_check, "Scripts")
_Specs = variable_struct_get(global.__dialogue_flow_data.npc_guard_crime_inv_check, "Specs")

array_push(_Fragments.player_crimeInvCheck, "player_thieveryReaction_axii_charm_pc")
_Fragments.player_thieveryReaction_axii_charm_pc = "instruction_INS_applyAxiiSign"
_Scripts.instruction_INS_applyAxiiSign = asset_get_index("scr_mod_apply_axii_charm_in_dialog")
_Specs.instruction_INS_applyAxiiSign = { action: true }
_Fragments.instruction_INS_applyAxiiSign = "player_thieveryReaction_axii_charm"
_Fragments.player_thieveryReaction_axii_charm = "@dialogue_end"

// Brynn Vogt
var _vogts = ["brynn_vogt_smith", "brynn_vogt_tailor", "brynn_vogt_carpenter"]
for (var i = 0; i < array_length(_vogts); i++)
{
    var _vogt_data = variable_struct_get(global.__dialogue_flow_data, _vogts[i])
    _Fragments = variable_struct_get(_vogt_data, "Fragments")
    _Scripts = variable_struct_get(_vogt_data, "Scripts")
    _Specs = variable_struct_get(_vogt_data, "Specs")

    var _old = variable_struct_get(_Fragments, _vogts[i])
    variable_struct_set(_Fragments, _vogts[i], "condition_CND_alreadyCharmed")
    _Fragments.condition_CND_alreadyCharmed = ["Cnd_alreadyCharmed_true", "Cnd_alreadyCharmed_false"]
    _Specs.Cnd_alreadyCharmed_true = { hub: true }
    _Specs.Cnd_alreadyCharmed_false = { hub: true }
    _Fragments.Cnd_alreadyCharmed_true = "skinflint_homs_was_charmed"
    _Fragments.Cnd_alreadyCharmed_false = _old
    _Scripts.condition_CND_alreadyCharmed = function() { return scr_mod_is_charmed_within(2) }

    array_insert(_Fragments.greeting, array_length(_Fragments.greeting) - 2, "skinflint_homs_charm_pc")
    _Fragments.getoutVogt = ["skinflint_homs_charm_pc", "leave"]
    _Fragments.skinflint_homs_charm_pc = "instruction_CND_applyAxiiSign"
    _Fragments.instruction_CND_applyAxiiSign = "skinflint_homs_was_charmed"
    _Specs.instruction_CND_applyAxiiSign = { action: true }
    _Scripts.instruction_CND_applyAxiiSign = asset_get_index("scr_mod_apply_axii_charm_in_dialog")
    _Scripts.embedded_skinflint_homs_charm_pc = function() { return o_skill_axii_sign_ico.is_open && !(room == r_Brynn_SW) }

    _Fragments.skinflint_homs_was_charmed = "instruction_INS_openTrade"
    _Fragments.instruction_INS_openTrade = "@dialogue_end"
    _Specs.instruction_INS_openTrade = { action: true }
    _Scripts.instruction_INS_openTrade = function() { scr_dialogue_open_trade() }
}