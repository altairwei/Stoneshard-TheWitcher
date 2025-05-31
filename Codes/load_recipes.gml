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
        "XP":       "10",
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
        "XP":       "10",
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
        "XP":       "10",
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
        "XP":       "10",
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
        "XP":       "10",
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
        "XP":       "10",
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
        "XP":       "10",
        "SOURCE":   "Skill"
    },
    "thunderbolt_potion": {
        "CAT":      "witcher_potion",
        "Recipe1":  "spirit, wine1",
        "Recipe2":  "hop",
        "Recipe3":  "henbane",
        "Recipe4":  "henbane",
        "Recipe5":  "-",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "25",
        "SOURCE":   "Skill"
    },
    "blizzard_potion": {
        "CAT":      "witcher_potion",
        "Recipe1":  "spirit, wine",
        "Recipe2":  "hornet_honey",
        "Recipe3":  "mindwort",
        "Recipe4":  "mindwort",
        "Recipe5":  "-",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "25",
        "SOURCE":   "Skill"
    },
    "petri_philter": {
        "CAT":      "witcher_potion",
        "Recipe1":  "coffee",
        "Recipe2":  "sapphire",
        "Recipe3":  "mindwort",
        "Recipe4":  "agrimony",
        "Recipe5":  "-",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "25",
        "SOURCE":   "Skill"
    },
    "swallow_potion": {
        "CAT":      "witcher_potion",
        "Recipe1":  "spirit",
        "Recipe2":  "honey",
        "Recipe3":  "honey",
        "Recipe4":  "burdock, bogbean, burnet",
        "Recipe5":  "burdock, bogbean, burnet",
        "Recipe6":  "fleawort",
        "AMOUNT":   "1",
        "XP":       "25",
        "SOURCE":   "Skill"
    },
    "tawny_owl": {
        "CAT":      "witcher_potion",
        "Recipe1":  "mead",
        "Recipe2":  "garlic",
        "Recipe3":  "thyme",
        "Recipe4":  "thyme",
        "Recipe5":  "-",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "25",
        "SOURCE":   "Skill"
    },
    "golden_oriole": {
        "CAT":      "witcher_potion",
        "Recipe1":  "spirit, wine1",
        "Recipe2":  "nettle",
        "Recipe3":  "burdock, burnet",
        "Recipe4":  "poppy, wormwood, henbane",
        "Recipe5":  "-",
        "Recipe6":  "-",
        "AMOUNT":   "1",
        "XP":       "25",
        "SOURCE":   "Skill"
    }
}')

global.recipes_witcher_alchemy_categories_order_list = __dsDebuggerListCreate()
ds_list_add(global.recipes_witcher_alchemy_categories_order_list, "weapon_oil", "witcher_potion")

global.recipes_witcher_alchemy_category_order_map = json_decode(@'{
    "weapon_oil": ["hanged_man_venom", "vampire_oil", "necrophage_oil", "specter_oil", "insectoid_oil", "hybrid_oil", "ogroid_oil"],
    "witcher_potion": ["thunderbolt_potion", "blizzard_potion", "petri_philter", "swallow_potion", "tawny_owl", "golden_oriole"]
}')
