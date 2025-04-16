// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;
public partial class TheWitcher : Mod
{
    public override string Author => "Altair";
    public override string Name => "The Witcher";
    public override string Description => "The Witcher";
    public override string Version => "1.0.0.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        Msl.AddFunction(ModFiles.GetCode("scr_apply_coating_oil.gml"), "scr_apply_coating_oil");
        Msl.AddFunction(ModFiles.GetCode("scr_coating_oil_damage_calc.gml"), "scr_coating_oil_damage_calc");
        Msl.AddFunction(ModFiles.GetCode("scr_hoversGetCoatingOilAttributes.gml"), "scr_hoversGetCoatingOilAttributes");

        PatchCoatingDisplay();
        PatchWeaponCoatingSkill();
        PatchHangedManVenom();

        Msl.LoadAssemblyAsString("gml_GlobalScript_scr_damage_calculation")
            .MatchFrom("ret.v")
            .InsertAbove(@"push.v arg.argument0
call.i gml_Script_scr_coating_oil_damage_calc(argc=2)
pop.v.v local._damage
pushloc.v local._damage")
            .Save();
    }

    private void PatchWeaponCoatingSkill()
    {
        UndertaleGameObject o_skill_weapon_coating = Msl.AddObject(
            name: "o_skill_weapon_coating",
            spriteName: "sprite1",
            parentName: "o_weapon_skills",
            isVisible: true,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_weapon_coating.ApplyEvent(ModFiles,
            new MslEvent("o_skill_weapon_coating_create_0.gml", EventType.Create, 0),
            new MslEvent("o_skill_weapon_coating_other_11.gml", EventType.Other, 11),
            new MslEvent("o_skill_weapon_coating_other_20.gml", EventType.Other, 20)
        );
    }

    private void PatchCoatingDisplay()
    {
        Msl.LoadGML("gml_Object_o_hoverWeapon_Create_0")
            .MatchFrom("cursedName = ")
            .InsertAbove(ModFiles, "o_hoverWeapon_create_0.gml")
            .Save();

        Msl.LoadGML("gml_Object_o_hoverWeapon_Other_20")
            .MatchFrom("cursedNameHeight = 0")
            .InsertAbove(ModFiles, "o_hoverWeapon_other_20.gml")
            .Save();

        Msl.LoadGML("gml_Object_o_hoverWeapon_Other_21")
            .MatchFrom("if middleHeight")
            .InsertAbove(ModFiles, "o_hoverWeapon_other_21.gml")
            .Save();

        Msl.LoadGML("gml_Object_o_inv_slot_Draw_0")
            .MatchFrom("if _wounded")
            .InsertAbove(@"
                var _oil = ds_map_find_value_ext(data, ""coating_oil"", """")
                var _count = ds_map_find_value_ext(data, ""oil_available_count"", 0)
                if (_oil != """" && _count > 0)
                {
                    draw_sprite_ext(s_ico_coating, 0, (x + _icons_offset_x), (y + 1), 1, 1, 0, c_white, image_alpha)
                    _icons_offset_x += (sprite_get_width(s_ico_coating) + 4)
                }
            ")
            .Save();
    }
}
