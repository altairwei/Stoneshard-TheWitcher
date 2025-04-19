global.recipes_witcher_alchemy_data = json_decode(@'{
    "hanged_man_venom": {
        "CAT":      "weapon_oil",
        "Recipe1":  "oil",
        "Recipe2":  "stool",
        "Recipe3":  "stool",
        "Recipe4":  "henbane",
        "Recipe5":  "-",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "1",
        "SOURCE":   "Base Recipe"
    }
}')

global.recipes_witcher_alchemy_categories_order_list = __dsDebuggerListCreate()
ds_list_add(global.recipes_witcher_alchemy_categories_order_list, "weapon_oil")

global.recipes_witcher_alchemy_category_order_map = json_decode(@'{
    "weapon_oil": ["hanged_man_venom"]
}')
