:[0]
call.i event_inherited(argc=0)
popz.v
pushi.e 55
conv.i.v
pushi.e 24
conv.i.v
pushi.e o_skill_witcher_alchemy_ico
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillPoint
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._alchemy
pushi.e 55
conv.i.v
pushi.e 62
conv.i.v
pushi.e o_skill_quen_sign_ico
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillPoint
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._quen
pushi.e 55
conv.i.v
pushi.e 100
conv.i.v
pushi.e o_skill_axii_sign_ico
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillPoint
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._axii
pushi.e 55
conv.i.v
pushi.e 138
conv.i.v
pushi.e o_skill_yrden_sign_ico
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillPoint
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._yrden
pushi.e 111
conv.i.v
pushi.e 24
conv.i.v
pushi.e o_skill_trial_of_grasses
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillPoint
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._grasses
pushi.e 111
conv.i.v
pushi.e 81
conv.i.v
pushi.e o_skill_aard_sign_ico
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillPoint
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._aard
pushi.e 111
conv.i.v
pushi.e 119
conv.i.v
pushi.e o_skill_igni_sign_ico
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillPoint
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._igni
;;; Connect each skills using lines
pushi.e 70
conv.i.v
pushi.e 24
conv.i.v
pushi.e s_witcher_skill_line_a
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillLine
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._line1
pushloc.v local._grasses
pushloc.v local._alchemy
call.i @@NewGMLArray@@(argc=1)
dup.v 1 8
dup.v 0
push.v stacktop.addConnectedPoints
callv.v 1
popz.v
pushloc.v local._grasses
pushloc.v local._line1
call.i @@NewGMLArray@@(argc=1)
dup.v 1 8
dup.v 0
push.v stacktop.addConnectedLines
callv.v 1
popz.v
pushloc.v local._line1
pushloc.v local._alchemy
call.i @@NewGMLArray@@(argc=1)
dup.v 1 8
dup.v 0
push.v stacktop.addConnectedPoints
callv.v 1
popz.v
pushi.e 70
conv.i.v
pushi.e 62
conv.i.v
pushi.e s_witcher_skill_line_b
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillLine
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._line2
pushi.e 70
conv.i.v
pushi.e 81
conv.i.v
pushi.e s_witcher_skill_line_c
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillLine
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._line3
pushi.e 70
conv.i.v
pushi.e 81
conv.i.v
pushi.e s_witcher_skill_line_d
conv.i.v
push.v self.connectionsRender
push.i gml_Script_ctr_SkillLine
conv.i.v
call.i @@NewGMLObject@@(argc=5)
pop.v.v local._line4
pushloc.v local._aard
pushloc.v local._yrden
call.i @@NewGMLArray@@(argc=1)
pushloc.v local._axii
call.i @@NewGMLArray@@(argc=1)
pushloc.v local._quen
call.i @@NewGMLArray@@(argc=1)
dup.v 3 8
dup.v 0
push.v stacktop.addConnectedPoints
callv.v 3
popz.v
pushloc.v local._aard
pushloc.v local._line4
call.i @@NewGMLArray@@(argc=1)
pushloc.v local._line3
call.i @@NewGMLArray@@(argc=1)
pushloc.v local._line2
call.i @@NewGMLArray@@(argc=1)
dup.v 3 8
dup.v 0
push.v stacktop.addConnectedLines
callv.v 3
popz.v
pushloc.v local._igni
pushloc.v local._yrden
call.i @@NewGMLArray@@(argc=1)
pushloc.v local._axii
call.i @@NewGMLArray@@(argc=1)
pushloc.v local._quen
call.i @@NewGMLArray@@(argc=1)
dup.v 3 8
dup.v 0
push.v stacktop.addConnectedPoints
callv.v 3
popz.v
pushloc.v local._igni
pushloc.v local._line4
call.i @@NewGMLArray@@(argc=1)
pushloc.v local._line3
call.i @@NewGMLArray@@(argc=1)
pushloc.v local._line2
call.i @@NewGMLArray@@(argc=1)
dup.v 3 8
dup.v 0
push.v stacktop.addConnectedLines
callv.v 3
popz.v
pushloc.v local._line2
pushloc.v local._quen
call.i @@NewGMLArray@@(argc=1)
dup.v 1 8
dup.v 0
push.v stacktop.addConnectedPoints
callv.v 1
popz.v
pushloc.v local._line3
pushloc.v local._axii
call.i @@NewGMLArray@@(argc=1)
dup.v 1 8
dup.v 0
push.v stacktop.addConnectedPoints
callv.v 1
popz.v
pushloc.v local._line4
pushloc.v local._yrden
call.i @@NewGMLArray@@(argc=1)
dup.v 1 8
dup.v 0
push.v stacktop.addConnectedPoints
callv.v 1
popz.v

:[end]