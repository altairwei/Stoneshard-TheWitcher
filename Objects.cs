using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddObjects()
    {
        AddCharacterObjects();
        AddBookObjects();
        AddAlchemyObjects();
        AddPerkObjects();
    }

    private void AddCharacterObjects()
    {
        Msl.AddObject(
            name: "o_white_wolf",
            parentName: "o_player",
            spriteName: "s_char_select",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );
    }

    private void AddBookObjects()
    {
        Msl.AddObject(
            name: "o_inv_book_witcher1",
            spriteName: "s_inv_bookG",
            parentName: "o_inv_treatise",
            isVisible: true,
            isAwake: true,
            isPersistent: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddObject(
            name: "o_inv_book_witcher2",
            spriteName: "s_inv_bookB",
            parentName: "o_inv_treatise",
            isVisible: true,
            isAwake: true,
            isPersistent: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddObject(
            name: "o_loot_book_witcher1",
            spriteName: "s_loot_BookG",
            parentName: "o_loot_treatise",
            isVisible: true,
            isAwake: true,
            isPersistent: false,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddObject(
            name: "o_loot_book_witcher2",
            spriteName: "s_loot_BookB",
            parentName: "o_loot_treatise",
            isVisible: true,
            isAwake: true,
            isPersistent: false,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );
    }

    private void AddAlchemyObjects()
    {
        Msl.AddObject(
            name: "o_skill_witcher_alchemy",
            parentName: "o_skill",
            spriteName: "s_witcher_alchemy",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddObject(
            name: "o_skill_witcher_alchemy_ico",
            parentName: "o_skill_ico",
            spriteName: "s_witcher_alchemy",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddObject(
            name: "o_witcherAlchemyCraftingMenu",
            parentName: "o_craftingMenu",
            spriteName: "s_cooking_menu",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Box
        );

        Msl.AddObject(
            name: "o_inv_alcohol_essentia",
            parentName: "o_inv_dishes_beverage",
            spriteName: "s_inv_alcohol_essentia",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_loot_alcohol_essentia",
            parentName: "o_consument_loot",
            spriteName: "s_loot_alcohol_essentia",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_inv_weapon_oil_water",
            parentName: "o_inv_bottle_water_flask",
            spriteName: "s_inv_weapon_oil_water",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_loot_weapon_oil_water",
            parentName: "o_consument_loot",
            spriteName: "s_loot_weapon_oil_water",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_inv_weapon_oil_empty",
            parentName: "o_inv_dishes_flask",
            spriteName: "s_inv_weapon_oil_empty",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_loot_weapon_oil_empty",
            parentName: "o_consument_loot",
            spriteName: "s_loot_weapon_oil_empty",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_inv_weapon_oil_parent",
            parentName: "o_inv_consum_active",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_loot_weapon_oil_parent",
            parentName: "o_consument_loot",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_inv_witcher_potion_water",
            parentName: "o_inv_bottle_water_flask",
            spriteName: "s_inv_witcher_potion_water",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_loot_witcher_potion_water",
            parentName: "o_consument_loot",
            spriteName: "s_loot_witcher_potion_water",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_inv_witcher_potion_empty",
            parentName: "o_inv_dishes_flask",
            spriteName: "s_inv_witcher_potion_empty",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_loot_witcher_potion_empty",
            parentName: "o_consument_loot",
            spriteName: "s_loot_witcher_potion_empty",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_inv_witcher_potion",
            parentName: "o_inv_dishes_beverage",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_loot_witcher_potion",
            parentName: "o_consument_loot",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_inv_witcher_decoction_water",
            parentName: "o_inv_bottle_water_flask",
            spriteName: "s_inv_witcher_decoction_water",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_loot_witcher_decoction_water",
            parentName: "o_consument_loot",
            spriteName: "s_loot_witcher_decoction_water",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_inv_witcher_decoction_empty",
            parentName: "o_inv_dishes_flask",
            spriteName: "s_inv_witcher_decoction_empty",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_loot_witcher_decoction_empty",
            parentName: "o_consument_loot",
            spriteName: "s_loot_witcher_decoction_empty",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_inv_witcher_decoction",
            parentName: "o_inv_dishes_beverage",
            isVisible: true,
            isPersistent: true,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_loot_witcher_decoction",
            parentName: "o_consument_loot",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: $"o_b_decoction_buff",
            parentName: "o_buff",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );
    }

    private void AddPerkObjects()
    {
        Msl.AddObject(
            name: "o_perk_professional_witcher",
            parentName: "o_perks",
            spriteName: "s_skills_passive_power_rune",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );

        Msl.AddObject(
            name: "o_perk_blaviken_butcher",
            parentName: "o_perks",
            spriteName: "s_skills_passive_power_rune",
            isVisible: true,
            isPersistent: false,
            isAwake: true
        );
    }
}
