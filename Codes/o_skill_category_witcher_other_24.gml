event_inherited()
// Tier 1
// var _alchemy = new ctr_SkillPoint(connectionsRender, o_skill_witcher_alchemy_ico, 24, 55)
var _quen = new ctr_SkillPoint(connectionsRender, o_skill_quen_sign_ico, 62, 55)
var _axii = new ctr_SkillPoint(connectionsRender, o_skill_axii_sign_ico, 100, 55)
var _yrden = new ctr_SkillPoint(connectionsRender, o_skill_yrden_sign_ico, 138, 55)
// Tier 2
var _grasses = new ctr_SkillPoint(connectionsRender, o_skill_trial_of_grasses, 24, 55)
var _aard = new ctr_SkillPoint(connectionsRender, o_skill_aard_sign_ico, 81, 111)
var _igni = new ctr_SkillPoint(connectionsRender, o_skill_igni_sign_ico, 119, 111)

// Lines
// var _line1 = new ctr_SkillLine(connectionsRender, s_witcher_skill_line_a, 24, 70)
// _grasses.addConnectedPoints([_alchemy])
// _grasses.addConnectedLines([_line1])
// _line1.addConnectedPoints([_alchemy])

var _line2 = new ctr_SkillLine(connectionsRender, s_witcher_skill_line_b, 62, 70)
var _line3 = new ctr_SkillLine(connectionsRender, s_witcher_skill_line_c, 81, 70)
var _line4 = new ctr_SkillLine(connectionsRender, s_witcher_skill_line_d, 81, 70)
_aard.addConnectedPoints([_quen], [_axii], [_yrden])
_aard.addConnectedLines([_line2], [_line3], [_line4])
_igni.addConnectedPoints([_quen], [_axii], [_yrden])
_igni.addConnectedLines([_line2], [_line3], [_line4])
_line2.addConnectedPoints([_quen])
_line3.addConnectedPoints([_axii])
_line4.addConnectedPoints([_yrden])