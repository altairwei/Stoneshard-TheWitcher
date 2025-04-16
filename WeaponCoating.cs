using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void PatchHangedManVenom()
    {
        UndertaleGameObject o_inv_hanged_man_venom = Msl.AddObject(
            name: "o_inv_hanged_man_venom",
            parentName: "o_inv_consum_active",
            spriteName: "s_inv_bottle",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        o_inv_hanged_man_venom.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                scr_consum_atr(""hanged_man_venom"")
                i_index = irandom(image_number - 1)
                drop_gui_sound = snd_beverage_drop
                pickup_sound = snd_beverage_pick
                charge = 3
                sec_charge = charge
                max_charge = charge
                bar_color = 0x20294F
                can_merge = true
                skill = o_skill_weapon_coating
            "),
            new MslEvent(eventType: EventType.Other, subtype: 10, code: "event_inherited()"),
            new MslEvent(eventType: EventType.Other, subtype: 24, code: @"
                event_inherited()
                var _name = ds_map_find_value(data, ""idName"")
                with (skill)
                    coating_oil = _name
            "),
            new MslEvent(eventType: EventType.Other, subtype: 25, code: @"
                event_inherited()
                with (loot_object)
                {
                    image_index = other.i_index
                    i_index = other.i_index
                }
            "),
            new MslEvent(eventType: EventType.Draw, subtype: 0, code: "scr_draw_consum_scale()")
        );

        UndertaleGameObject o_loot_hanged_man_venom = Msl.AddObject(
            name: "o_loot_hanged_man_venom",
            parentName: "o_consument_loot",
            spriteName: "s_heal_pointon",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        o_loot_hanged_man_venom.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                image_speed = 0
                inv_object = o_inv_hanged_man_venom
                number = 0
            ")
        );

        Msl.InjectTableItemStats(
            id: "hanged_man_venom",
            Material: Msl.ItemStatsMaterial.glass,
            Weight: Msl.ItemStatsWeight.Medium,
            Price: 200,
            Duration: 60,
            Stacks: 3,
            Cat: Msl.ItemStatsCategory.tool,
            bottle: true,
            tags: Msl.ItemStatsTags.special
        );

        Msl.InjectTableItemsLocalization(
            new LocalizationItem(
                id: "hanged_man_venom",
                name: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Hanged man's venom"},
                    {ModLanguage.Chinese, "吊死鬼之毒"}
                },
                effect: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Applied to a weapon, it increases the damage dealt to humanoids by ~lg~20%~/~."},
                    {ModLanguage.Chinese, "应用于武器，对类人生物造成的伤害增加~lg~20%~/~。"}
                },
                description: new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Hanged Man's Venom is a toxin that is equally lethal to humans, elves and dwarves. Applied to a blade, it deals more damage than any other coating. This oil is ineffective against monsters."},
                    {ModLanguage.Chinese, "吊死鬼之毒对于人类、精灵与矮人而言是致命的毒素。只要涂抹在剑刃上，它会比其他涂油造成更多的伤害。这种涂油对于怪物没多大效用。"}
                }
            )
        );
    }
}