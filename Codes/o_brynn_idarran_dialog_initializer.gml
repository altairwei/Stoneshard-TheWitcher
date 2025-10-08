global.__dialogue_flow_data.brynn_genetic_recruitment =
{
    RootFragment: "brynn_genetic_recruitment",
    Monologue: false,

    Fragments:
    {
        brynn_genetic_recruitment: "introGeneticExperiment01",
        introGeneticExperiment01: "response_hub",
        response_hub: ["introGeneticExperiment01_pc", "introGeneticExperiment_reject_pc"],
        introGeneticExperiment01_pc: "instruction_INS_BringToCellar",
        instruction_INS_BringToCellar: "@dialogue_end",
        introGeneticExperiment_reject_pc: "@dialogue_end"
    },

    Specs:
    {
        response_hub: { hub: true },
        instruction_INS_BringToCellar: { action: true }
    },

    Scripts:
    {
        instruction_INS_BringToCellar: function() {
            global.position_tag = "BrynnUniversityCellar"
            scr_smoothRoomChange(r_BrynnUniversityCellar, [4], -1, true)
        }
    },

    Speakers: 
    {
        Player: ["introGeneticExperiment01_pc", "introGeneticExperiment_reject_pc"],
        Student: ["introGeneticExperiment01"]
    }
}

global.__dialogue_flow_data.geneticist_idarran =
{
    RootFragment: "geneticist_idarran",
    Monologue: false,

    Fragments:
    {
        geneticist_idarran: "condition_CND_isRestReading",
        condition_CND_isRestReading: ["HUB_Cnd_isRestReading_positive", "HUB_Cnd_isRestReading_negative"],
        HUB_Cnd_isRestReading_negative: "condition_CND_firstMeet",
        HUB_Cnd_isRestReading_positive: "rest_reading",
        rest_reading: "@dialogue_end",

        condition_CND_firstMeet: ["HUB_Cnd_firstMeet_positive", "HUB_Cnd_firstMeet_negative"],
        HUB_Cnd_firstMeet_negative: "greeting_idarran",
        HUB_Cnd_firstMeet_positive: "condition_CND_isGeralt",
        condition_CND_isGeralt: ["HUB_Cnd_isGeralt_positive", "HUB_Cnd_isGeralt_negative"],
        HUB_Cnd_isGeralt_positive: "introGeralt01",
        HUB_Cnd_isGeralt_negative: "introWitcherExperiment01",

        greeting_idarran: "hub",
        hub: ["trade", "chat", "leave"],
        trade: "@dialogue_end",

        chat: "condition_CND_haveChats",
        condition_CND_haveChats: ["HUB_Cnd_haveChats_positive", "HUB_Cnd_haveChats_negative"],
        HUB_Cnd_haveChats_positive: "idarran_chat",
        idarran_chat: "idarran_end_chat",
        idarran_end_chat: "@dialogue_end",
        HUB_Cnd_haveChats_negative: "idarran_no_chat",
        idarran_no_chat: "@dialogue_end",

        introWitcherExperiment01: "",

        introGeralt01: "introGeralt01_pc",
        introGeralt01_pc: "introGeralt02",
        introGeralt02: "introGeralt02_pc",
        introGeralt02_pc: "introGeralt03",
        introGeralt03: "introGeralt03_pc",
        introGeralt03_pc: "introGeralt04",
        introGeralt04: "introGeralt05",
        introGeralt05: "introGeralt05_pc",
        introGeralt05_pc: "introGeralt06",
        introGeralt06: "introGeralt06_pc",
        introGeralt06_pc: "introGeralt07",
        introGeralt07: "@dialogue_end"
    },

    Specs:
    {
        hub: { hub: true },
        trade: { generic: true },
        chat: { generic: true },
        idarran_chat: { generic: true },
        idarran_no_chat: { generic: true },
        idarran_end_chat: { generic: true },
        HUB_Cnd_haveChats_positive: { hub: true },
        HUB_Cnd_haveChats_negative: { hub: true },
        HUB_Cnd_isRestReading_positive: { hub: true },
        HUB_Cnd_isRestReading_negative: { hub: true },
        HUB_Cnd_firstMeet_positive: { hub: true },
        HUB_Cnd_firstMeet_negative: { hub: true },
        HUB_Cnd_isGeralt_positive: { hub: true },
        HUB_Cnd_isGeralt_negative: { hub: true }
    },

    Scripts:
    {
        condition_CND_firstMeet: function() {
            return !(scr_dialogue_complete("introGeralt01") || scr_dialogue_complete("introWitcherExperiment01"))
        },

        condition_CND_isGeralt: function() {
            return scr_atr("nameKey") == "Geralt"
        },

        condition_CND_haveChats: function() {
            with (owner)
            {
                var _chats_left = scr_npc_get_global_info("idarran_chats_left")
                if (_chats_left > 0)
                {
                    scr_npc_set_global_info("idarran_chats_left", _chats_left - 1)
                    return true;
                }
                else
                    return false;
            }
        },

        condition_CND_isRestReading: function() {
            with (owner)
            {
                return is_rest
            }
        }
    },

    Speakers: 
    {
        Player: [
            "trade", "chat", "leave",
            "introGeralt01_pc", "introGeralt02_pc", "introGeralt03_pc",
            "introGeralt05_pc", "introGeralt06_pc"
        ],
        Idarran: [
            "introGeralt01", "introGeralt02", "introGeralt03",
            "introGeralt04", "introGeralt05", "introGeralt06", "introGeralt07"]
    }
}