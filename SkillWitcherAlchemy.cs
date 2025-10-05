using System.Text.RegularExpressions;

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace TheWitcher;

public partial class TheWitcher : Mod
{
    private void AddSkill_Witcher_Alchemy()
    {
        AdjustSkillIcon("s_witcher_alchemy");

        // Skill - Witcher Alchemy

        Msl.InjectTableSkillsLocalization(
            new LocalizationSkill(
                id: "Witcher_Alchemy",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "The Witcher's Alchemy"},
                    {ModLanguage.Chinese, "猎魔人炼金术"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, @"Opens the menu for ~w~crafting weapon coating oil~/~."},
                    {ModLanguage.Chinese, @"能够打开~w~猎魔人炼金术~/~界面，学会制作~w~剑油~/~、~w~魔药~/~以及~w~煎药~/~。"}
                }
            )
        );

        Msl.InjectTableSkillsStats(
            id: "Witcher_Alchemy",
            Object: "object",
            hook: Msl.SkillsStatsHook.BASIC
        );

        UndertaleGameObject o_skill_witcher_alchemy = Msl.AddObject(
            name: "o_skill_witcher_alchemy",
            parentName: "o_skill",
            spriteName: "s_witcher_alchemy",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        AddWitcherAlchemyCraftingMenu();

        o_skill_witcher_alchemy.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                skill = ""Witcher_Alchemy""
                scr_skill_atr(""Witcher_Alchemy"")
            "),
            new MslEvent(eventType: EventType.Other, subtype: 13, code: @"
                with (o_player)
                    lock_movement = false
                if (!instance_exists(o_witcherAlchemyCraftingMenu))
                {
                    with (scr_guiCreateContainer(global.guiBaseContainerSideLeft, o_witcherAlchemyCraftingMenu))
                    {
                        parent = other.id
                        event_user(1)
                    }
                }
            "),
            new MslEvent(eventType: EventType.Other, subtype: 14, code: @"
                event_inherited()
                is_ready = true
            "),
            new MslEvent(eventType: EventType.Other, subtype: 17, code: @"
                event_inherited()
                MPcost = 0
                KD = 0
            ")
        );

        UndertaleGameObject o_skill_witcher_alchemy_ico = Msl.AddObject(
            name: "o_skill_witcher_alchemy_ico",
            parentName: "o_skill_ico",
            spriteName: "s_witcher_alchemy",
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_witcher_alchemy_ico.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                child_skill = o_skill_witcher_alchemy
                event_perform_object(child_skill, ev_create, 0)
            "),

            // 每次游戏加载成功，这个事件就会执行一次。
            new MslEvent(eventType: EventType.Other, subtype: 18, code: @"
                event_inherited()

                var _list = scr_atr(""recipesWitcherAlchemyOpened"")
                if (is_undefined(_list))
                {
                    _list = __dsDebuggerListCreate()
                    scr_atr_set(""recipesWitcherAlchemyOpened"", _list)
                }

                if (ds_list_find_index(_list, ""hanged_man_venom"") < 0)
                {
                    ds_list_add(_list, ""hanged_man_venom"", ""vampire_oil"", ""necrophage_oil"",
                                        ""specter_oil"", ""insectoid_oil"", ""hybrid_oil"", ""ogroid_oil"")
                }
                if (ds_list_find_index(_list, ""thunderbolt_potion"") < 0)
                {
                    ds_list_add(_list, ""thunderbolt_potion"", ""blizzard_potion"", ""petri_philter"", ""swallow_potion"",
                                        ""tawny_owl"", ""golden_oriole"")
                }
                if (ds_list_find_index(_list, ""ghoul_decoction"") < 0)
                {
                    ds_list_add(_list, ""ghoul_decoction"", ""crawler_decoction"", ""harpy_decoction"", ""troll_decoction"",
                                        ""gulon_decoction"")
                }
                with (o_craftingMenu)
                {
                    event_user(11)
                    event_user(13)
                    event_user(12)
                }
            ")
        );
    }

    private void AddWitcherAlchemyCraftingMenu()
    {
        Msl.InjectTableTextCraftingCategoryLocalization(
            new LocalizationCraftingCategory("weapon_oil", new Dictionary<ModLanguage, string>{
                {ModLanguage.English, "Weapon Oil"},
                {ModLanguage.Chinese, "剑油"}
            }),
            new LocalizationCraftingCategory("witcher_potion", new Dictionary<ModLanguage, string>{
                {ModLanguage.English, "Potion"},
                {ModLanguage.Chinese, "魔药"}
            }),
            new LocalizationCraftingCategory("witcher_decoction", new Dictionary<ModLanguage, string>{
                {ModLanguage.English, "Decoction"},
                {ModLanguage.Chinese, "煎药"}
            })
        );

        UndertaleGameObject o_witcherAlchemyCraftingMenu = Msl.AddObject(
            name: "o_witcherAlchemyCraftingMenu",
            parentName: "o_craftingMenu",
            spriteName: "s_cooking_menu",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Box
        );

        o_witcherAlchemyCraftingMenu.Group = 1;

        o_witcherAlchemyCraftingMenu.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                with (mask)
                    title = ds_map_find_value(global.skill_name_text, ""Witcher_Alchemy"")
                with (craftButton)
                    text = ds_list_find_value(global.inv_text, 72)
                rightContainerBG = s_CookingBG_Consums_kit
            "),

            new MslEvent(eventType: EventType.Draw, subtype: 0, code: @"
                event_inherited()
                with (consumsContainer)
                {
                    for (var _i = 0; _i < guiChildrenCount; _i++)
                    {
                        with (ds_list_find_value(guiChildrenList, _i))
                        {
                            for (var _j = 0; _j < guiChildrenCount; _j++)
                            {
                                with (ds_list_find_value(guiChildrenList, _j))
                                {
                                    if (!select)
                                        draw_sprite_ext(s_point, 0, x - 1, y - 1, sprite_width + 2, sprite_height + 2, 0, make_color_rgb(17, 16, 26), 1);
                                }
                            }
                        }
                    }
                }
            "),

            new MslEvent(eventType: EventType.Other, subtype: 11, code: @"
                if instance_exists(parent)
                {
                    if (parent.object_index == o_skill_witcher_alchemy)
                        rightContainerBG = s_CookingBG_Consums
                }
                if (scr_caravanPositionGetDistance() == 0)
                {
                    inventoryCategories = scr_guiCreateContainer(global.guiBaseContainerVisible, o_inventory_categories_menu, -12200, 0, 0, 2)
                    with (inventoryCategories)
                        event_user(0)
                }
                with (consumsContainer)
                    contentType = o_loot
                event_inherited()
            "),

            new MslEvent(eventType: EventType.Other, subtype: 12, code: ""),

            // Check if it is allowed to craft weapon oil or potions
            new MslEvent(eventType: EventType.Other, subtype: 13, code: @"
                item_array = []
                canCraft = scr_crafting_recipe_components_check(activeRecipeButton, true)
                event_inherited()
            "),

            // Crafting
            new MslEvent(eventType: EventType.Other, subtype: 14,
                code: ModFiles.GetCode("o_witcherAlchemyCraftingMenu_other_14.gml")),

            new MslEvent(eventType: EventType.Other, subtype: 24, code: @"
                event_inherited()
                componentsContainer = scr_inventory_cells_container_create(itemsContainer, 5, o_inv_slot, 7, 7)
                scr_inventory_cells_add(id, componentsContainer, 4)
                consumsContainer = scr_inventory_cells_container_create(itemsContainer, 4, o_inv_slot, 21, 140)
                scr_inventory_cells_add(id, consumsContainer, 3)
            ")
        );

        // Add Witcher Alchemy Crafting to existing system
        Msl.LoadGML("gml_Object_o_craftingMenu_Other_23")
            .MatchFrom("case o_craftingFoodMenu:")
            .InsertAbove(@"
    case o_witcherAlchemyCraftingMenu:
        _recipesOpenedList = scr_atr(""recipesWitcherAlchemyOpened"")
        _recipesCategoriesOrderList = global.recipes_witcher_alchemy_categories_order_list
        _recipesCategoryOrderMap = global.recipes_witcher_alchemy_category_order_map
        break;")
            .Save();

        Msl.LoadGML("gml_Object_o_craftingMenu_Other_22")
            .MatchFrom("case o_craftingFoodMenu:")
            .InsertAbove(@"
    case o_witcherAlchemyCraftingMenu:
        _recipeKey = global.craftingWitcherAlchemyMenuRecipeKey
        _scrolledHeight = global.craftingWitcherAlchemyMenuScrolledHeight
        break;")
            .Save();

        Msl.LoadGML("gml_Object_o_craftingMenu_Other_21")
            .MatchFrom("case o_craftingFoodMenu:")
            .InsertAbove(@"
    case o_witcherAlchemyCraftingMenu:
        with (activeRecipeButton)
            global.craftingWitcherAlchemyMenuRecipeKey = idName
        with (scrollbar)
            global.craftingWitcherAlchemyMenuScrolledHeight = scrolledHeight
        break;")
            .Save();

        // Load recorded recipes
        Msl.LoadGML("gml_GlobalScript_scr_characterMapInit")
            .MatchFrom("var _recipesIterableArray = ")
            .InsertBelow(@"
        var _recipesWitcherAlchemyOpenedList = __dsDebuggerListCreate()
        array_push(_recipesIterableArray, global.recipes_witcher_alchemy_data, _recipesWitcherAlchemyOpenedList)")
            .MatchFrom("ds_map_add_list(global.characterDataMap, \"recipesFoodOpened\", _recipesFoodOpenedList)")
            .InsertAbove("ds_map_add_list(global.characterDataMap, \"recipesWitcherAlchemyOpened\", _recipesWitcherAlchemyOpenedList)")
            .Save();

        // Init certain global vars
        Msl.LoadGML("gml_GlobalScript_scr_sessionDataInit")
            .MatchFrom("global.craftingConsumsMenuScrolledHeight = 0")
            .InsertBelow(@"
    global.craftingWitcherAlchemyMenuRecipeKey = ""N/A""
    global.craftingWitcherAlchemyMenuScrolledHeight = 0
            ")
            .Save();

        // Adjust coordinates of alchemy container
        int index = DataLoader.data.GameObjects.IndexOf(
            DataLoader.data.GameObjects.First(x => x.Name.Content == "o_craftingConsumsMenu"));
        Msl.LoadGML("gml_GlobalScript_scr_adaptiveMenusGetOffset")
            .MatchFrom($"case {index}:")
            .InsertBelow("        case o_witcherAlchemyCraftingMenu:")
            .Save();

        // Add Witcher Alchemy to search for recipes
        Msl.LoadGML("gml_GlobalScript_scr_crafting_recipe_get_map")
            .MatchFromUntil("function scr_crafting_recipe_get_map", "}")
            .ReplaceBy(@"
function scr_crafting_recipe_get_map()
{
    var _to_check = [global.recipes_food_data, global.recipes_consums_data, global.recipes_witcher_alchemy_data]
    for (var i = 0; i < array_length(_to_check); i++)
    {
        var _recipeDataMap = ds_map_find_value(_to_check[i], argument0)
        if (!__is_undefined(_recipeDataMap))
            return _recipeDataMap;
    }
}")
            .Save();

        // Load Witcher Alchemy Recipes
        UndertaleGameObject ob = Msl.AddObject("o_mod_witcher_initializer", isPersistent: true);
        Msl.AddNewEvent(ob, ModFiles.GetCode("load_recipes.gml"), EventType.Create, 0);
        UndertaleRoom start = Msl.GetRoom("START");
        start.AddGameObject("Instances", ob);

        // Add interaction with items
        Msl.LoadGML("gml_Object_o_inv_slot_Other_13")
            .MatchFrom("case o_craftingConsumsMenu:")
            .InsertBelow("                case o_witcherAlchemyCraftingMenu:")
            .Save();

        // It seems we need to update this ASM codes every Stoneshard updates.
        int index_menu = DataLoader.data.GameObjects.IndexOf(
            DataLoader.data.GameObjects.First(x => x.Name.Content == "o_craftingConsumsMenu"));

        var fe0 = Msl.LoadAssemblyAsString("gml_Object_o_inv_slot_Mouse_4");
        var matched = fe0.MatchFromUntil($"pushi.e {index}", "bt [");
        var btNum = matched.ienumerable
            .Where(t => t.Item1 == ModShardLauncher.Match.Matching)
            .Select(t => Regex.Match(t.Item2, @"\bbt\s*\[(\d+)\]"))
            .FirstOrDefault(r => r.Success)?
            .Groups[1].Value.ThrowIfNull();

        Msl.LoadAssemblyAsString("gml_Object_o_inv_slot_Mouse_4")
            .MatchFromUntil($"pushi.e {index}", "bt [")
            .InsertBelow(@$"
:[1000]
dup.v 0
pushi.e {DataLoader.data.GameObjects.IndexOf(o_witcherAlchemyCraftingMenu)}
cmp.i.v EQ
bt [{btNum}]")
            .Peek()
            .Save();
        
    }
}