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

        PatchSkill_Witcher_Alchemy();

        // Add Skill Branch

        Msl.InjectTableTextTreesLocalization(
            new LocalizationTextTree(
                id: "Witcher",
                tier: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Witcher"},
                    {ModLanguage.Chinese, "猎魔人"}
                },
                hover: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Gained the ability to leech blood through an occult ritual, but also became extremely tyrannical and bloodthirsty.##~y~Main focus:~/~#~w~High Damage~/~, ~w~Bleeding~/~, ~w~Survivability~/~"},
                    {ModLanguage.Chinese, "通过秘仪获得了吸血能力，但也因此变得异常暴虐嗜血。##~y~能力要义：~/~#~w~高伤害~/~、~w~造成出血~/~、~w~生存能力~/~"}
                }
            )
        );

        UndertaleGameObject o_skill_category_witcher = Msl.AddObject(
            name: "o_skill_category_witcher", 
            spriteName: "", 
            parentName: "o_skill_category_weapon", 
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_category_witcher.ApplyEvent(ModFiles,
            new MslEvent("o_skill_category_witcher_create_0.gml", EventType.Create, 0),
            new MslEvent("o_skill_category_witcher_other_24.gml", EventType.Other, 24)
        );

        /*
        o_skill_category_witcher.ApplyEvent(
            new MslEvent(eventType: EventType.Other, subtype: 10, code: @"
                event_inherited()
                if (learning_skill.object_index == o_skill_witcher_alchemy_ico)
                {
                    show_message(""o_skill_category_witcher_other_10"")
                }
            ")
        );
        */

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