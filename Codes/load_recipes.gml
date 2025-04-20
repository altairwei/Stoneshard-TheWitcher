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
        "SOURCE":   "Skill"
    },
    "vampire_oil": {
        "CAT":      "weapon_oil",
        "Recipe1":  "oil",
        "Recipe2":  "silver_nugget",
        "Recipe3":  "hemp",
        "Recipe4":  "hemp",
        "Recipe5":  "garlic",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "1",
        "SOURCE":   "Skill"
    },
    "necrophage_oil": {
        "CAT":      "weapon_oil",
        "Recipe1":  "oil",
        "Recipe2":  "church_candle",
        "Recipe3":  "mindwort",
        "Recipe4":  "rhubarb",
        "Recipe5":  "-",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "1",
        "SOURCE":   "Skill"
    },
    "specter_oil": {
        "CAT":      "weapon_oil",
        "Recipe1":  "oil",
        "Recipe2":  "skull",
        "Recipe3":  "horsetail",
        "Recipe4":  "horsetail",
        "Recipe5":  "-",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "1",
        "SOURCE":   "Skill"
    },
    "insectoid_oil": {
        "CAT":      "weapon_oil",
        "Recipe1":  "oil",
        "Recipe2":  "flyagaric",
        "Recipe3":  "flyagaric",
        "Recipe4":  "henbane",
        "Recipe5":  "-",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "1",
        "SOURCE":   "Skill"
    },
    "hybrid_oil": {
        "CAT":      "weapon_oil",
        "Recipe1":  "oil",
        "Recipe2":  "flyagaric",
        "Recipe3":  "henbane",
        "Recipe4":  "burnet",
        "Recipe5":  "citrus",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "1",
        "SOURCE":   "Skill"
    },
    "ogroid_oil": {
        "CAT":      "weapon_oil",
        "Recipe1":  "oil",
        "Recipe2":  "stool",
        "Recipe3":  "stool",
        "Recipe4":  "henbane",
        "Recipe5":  "citrus",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "1",
        "SOURCE":   "Skill"
    }
}')

global.recipes_witcher_alchemy_categories_order_list = __dsDebuggerListCreate()
ds_list_add(global.recipes_witcher_alchemy_categories_order_list, "weapon_oil")

global.recipes_witcher_alchemy_category_order_map = json_decode(@'{
    "weapon_oil": ["hanged_man_venom", "vampire_oil", "necrophage_oil", "specter_oil", "insectoid_oil", "hybrid_oil", "ogroid_oil"]
}')
