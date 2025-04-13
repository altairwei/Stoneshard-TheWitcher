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
        PatchWeaponCoatingSkill();
        PatchHangedManVenom();
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
}
