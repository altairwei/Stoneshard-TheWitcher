using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void PatchWitcherSkills()
    {

        Msl.GetSprite("s_witcher_branch").OriginX = 0;
        Msl.GetSprite("s_witcher_branch").OriginY = 0;

        UndertaleGameObject o_skill_category_witcher = Msl.AddObject(
            name: "o_skill_category_witcher", 
            spriteName: "", 
            parentName: "o_skill_category_weapon", 
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        AddSkill_Witcher_Alchemy();
        AddSkill_Quen_Sign();
        AddSkill_Axii_Sign();
        AddSkill_Yrden_Sign();
        AddSkill_Aard_Sign();
        AddSkill_Igni_Sign();
        AddSkill_Trial_Of_Grasses();

        // Add Skill Branch

        Msl.InjectTableTextTreesLocalization(
            new LocalizationTextTree(
                id: "Witcher",
                tier: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Witcher"},
                    {ModLanguage.Chinese, "猎魔人"}
                },
                hover: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "##~y~Main focus:~/~#~w~Survival~/~, ~w~Support~/~, ~w~Crowd Control~/~"},
                    {ModLanguage.Chinese, "通过青草试炼获得强健的体魄和毒素免疫能力，在战斗中使用各种炼金物品和简单的法术。##~y~能力要义：~/~#~w~生存~/~、~w~辅助~/~、~w~控场~/~"}
                }
            )
        );

        o_skill_category_witcher.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                text = ""Witcher""
                skill = [o_skill_witcher_alchemy_ico, o_skill_quen_sign_ico, o_skill_axii_sign_ico, o_skill_yrden_sign_ico,
                         o_skill_aard_sign_ico, o_skill_igni_sign_ico, o_skill_trial_of_grasses]
                branch_sprite = s_witcher_branch
                owner = gml_Script_scr_GetMobParametr
            "),
            new MslEvent(eventType: EventType.Other, subtype: 24, code: @"
                event_inherited()
                // Tier 1
                scr_guiLayoutOffsetUpdate(o_skill_witcher_alchemy_ico, 24, 55, 1)
                scr_guiLayoutOffsetUpdate(o_skill_quen_sign_ico, 62, 55, 1)
                scr_guiLayoutOffsetUpdate(o_skill_axii_sign_ico, 100, 55, 1)
                scr_guiLayoutOffsetUpdate(o_skill_yrden_sign_ico, 138, 55, 1)
                // Tier 2
                scr_guiLayoutOffsetUpdate(o_skill_trial_of_grasses, 24, 118, 1)
                scr_guiLayoutOffsetUpdate(o_skill_aard_sign_ico, 81, 118, 1)
                scr_guiLayoutOffsetUpdate(o_skill_igni_sign_ico, 138, 118, 1)
            ")
        );

        Msl.LoadGML("gml_Object_o_skillmenu_Create_0")
            .MatchFrom("var _metaCategoriesArray = ")
            .InsertBelow(@"array_push(_metaCategoriesArray[1], o_skill_category_witcher)")
            .Save();
    }

    private static void AdjustSkillIcon(string name)
    {
        UndertaleSprite ico = Msl.GetSprite(name);
        ico.CollisionMasks.RemoveAt(0);
        ico.IsSpecialType = true;
        ico.SVersion = 3;
        ico.OriginX = 12;
        ico.OriginY = 12;
        ico.GMS2PlaybackSpeed = 1;
        ico.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;
    }
}