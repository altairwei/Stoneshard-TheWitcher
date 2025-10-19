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
        hub: ["trade", "readyToTrialOfGrasses_pc", "chat", "leave"],
        trade: "@dialogue_end",

        chat: "condition_CND_haveChats",
        condition_CND_haveChats: ["HUB_Cnd_haveChats_positive", "HUB_Cnd_haveChats_negative"],
        HUB_Cnd_haveChats_positive: "idarran_chat",
        idarran_chat: "idarran_end_chat",
        idarran_end_chat: "@dialogue_end",
        HUB_Cnd_haveChats_negative: "idarran_no_chat",
        idarran_no_chat: "@dialogue_end",

        introWitcherExperiment01: "introWitcherExperiment01_pc",
        introWitcherExperiment01_pc: "introWitcherExperiment02",
        introWitcherExperiment02: "introWitcherExperiment02_pc",
        introWitcherExperiment02_pc: "introWitcherExperiment03",
        introWitcherExperiment03: "introWitcherExperiment03_pc",
        introWitcherExperiment03_pc: "introWitcherExperiment04",
        introWitcherExperiment04: "introWitcherExperiment04_pc",
        introWitcherExperiment04_pc: "introWitcherExperiment05",
        introWitcherExperiment05: "introWitcherExperiment05_pc",
        introWitcherExperiment05_pc: "introWitcherExperiment06",

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
        introGeralt07: "@dialogue_end",

        readyToTrialOfGrasses_pc: "readyToTrialOfGrasses02",
        readyToTrialOfGrasses02: "instruction_INS_moveToBed",
        instruction_INS_moveToBed: "@dialogue_end"
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
        HUB_Cnd_isGeralt_negative: { hub: true },
        instruction_INS_moveToBed: { action: true, cutscene: true }
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
        },

        embedded_readyToTrialOfGrasses_pc: function() {
            return (scr_atr("nameKey") != "Geralt")
                    && scr_dialogue_complete("introWitcherExperiment01")
        },

        instruction_INS_moveToBed: function() {
            scr_close_dialog()
            with (owner)
            {
                with (o_cutscene_controller)
                    event_user(0)

                scr_camera_set_target(id)

                with (instance_create_depth(x, y, 0, o_npc_walk_controller))
                {
                    owner = other.id
                    target = other.grass_target
                    completion_callback = function() {
                        with (o_npc_Idarran)
                            scr_dialogue_start("trial_of_grasses")
                    }
                }
            }
        }
    },

    Speakers: 
    {
        Player: [
            "trade", "chat", "leave",
            "introGeralt01_pc", "introGeralt02_pc", "introGeralt03_pc",
            "introGeralt05_pc", "introGeralt06_pc",
            "introWitcherExperiment01_pc", "introWitcherExperiment02_pc", "introWitcherExperiment03_pc",
            "introWitcherExperiment04_pc", "introWitcherExperiment05_pc",
            "readyToTrialOfGrasses_pc"
        ],
        Idarran: [
            "introGeralt01", "introGeralt02", "introGeralt03",
            "introGeralt04", "introGeralt05", "introGeralt06", "introGeralt07",
            "introWitcherExperiment01", "introWitcherExperiment02", "introWitcherExperiment03",
            "introWitcherExperiment04", "introWitcherExperiment05", "introWitcherExperiment06"]
    }
}

global.__dialogue_flow_data.trial_of_grasses =
{
    RootFragment: "trial_of_grasses",
    Monologue: false,

    Fragments:
    {
        trial_of_grasses: "instruction_INS_letPlayerCome",
        instruction_INS_letPlayerCome: "introTrialOfGrasses01",
        introTrialOfGrasses01: "introTrialOfGrasses02",
        introTrialOfGrasses02: "introTrialOfGrasses03",
        introTrialOfGrasses03: "introTrialOfGrasses03_pc",
        introTrialOfGrasses03_pc: "instruction_INS_createIllusion",
        instruction_INS_createIllusion: "introTrialOfGrasses04",
        introTrialOfGrasses04: "introTrialOfGrasses04_pc",
        introTrialOfGrasses04_pc: "introTrialOfGrasses05",
        introTrialOfGrasses05: "introTrialOfGrasses05_pc",
        introTrialOfGrasses05_pc: "introTrialOfGrasses06",
        introTrialOfGrasses06: "introTrialOfGrasses07",
        introTrialOfGrasses07: "instruction_INS_sleepAfterTrial",
        instruction_INS_sleepAfterTrial: "afterTrialOfGrasses01",
        afterTrialOfGrasses01: "afterTrialOfGrasses01_pc",
        afterTrialOfGrasses01_pc: "afterTrialOfGrasses02",
        afterTrialOfGrasses02: "afterTrialOfGrasses02_pc",
        afterTrialOfGrasses02_pc: "afterTrialOfGrasses03",
        afterTrialOfGrasses03: "afterTrialOfGrasses03_pc",
        afterTrialOfGrasses03_pc: "afterTrialOfGrasses04",
        afterTrialOfGrasses04: "afterTrialOfGrasses04_pc",
        afterTrialOfGrasses04_pc: "instruction_INS_completeTrial",
        instruction_INS_completeTrial: "afterTrialOfGrasses05",
        afterTrialOfGrasses05: "afterTrialOfGrasses05_pc",
        afterTrialOfGrasses05_pc: "afterTrialOfGrasses06",
        afterTrialOfGrasses06: "afterTrialOfGrasses07",
        afterTrialOfGrasses07: "@dialogue_end"
    },

    Specs: {},

    Scripts:
    {
        instruction_INS_letPlayerCome: function() {
            scr_dialogue_cutscene_seq_start_walk(o_player, 624, 546)

            with (o_cutscene_controller)
                event_user(1)
        },

        instruction_INS_createIllusion: function() {
            with (o_player)
            {
                scr_effect_create(o_db_bad_trip, 1024, id, id)
                scr_effect_create(o_db_drug_nikkaf, 1024, id, id)
            }
        },

        instruction_INS_sleepAfterTrial: function() {
            with (o_cutscene_controller)
                event_user(0)

            scr_dialogue_cutscene_time_skip(24)
            
            with (o_player)
                scr_pain_change(id, 50)
        },

        instruction_INS_completeTrial: function() {
            with (o_cutscene_controller)
                event_user(1)

            with (o_player)
                scr_pain_change(id, 50)

            scr_skill_branch_study([o_skill_trial_of_grasses])
        }
    },

    Speakers: 
    {
        Player: [
            "introTrialOfGrasses03_pc", "introTrialOfGrasses04_pc", "introTrialOfGrasses05_pc",
            "afterTrialOfGrasses01_pc", "afterTrialOfGrasses02_pc", "afterTrialOfGrasses03_pc",
            "afterTrialOfGrasses04_pc", "afterTrialOfGrasses05_pc"],
        Idarran: [
            "introTrialOfGrasses01", "introTrialOfGrasses02", "introTrialOfGrasses03",
            "introTrialOfGrasses04", "introTrialOfGrasses05", "introTrialOfGrasses06",
            "introTrialOfGrasses07", "afterTrialOfGrasses01", "afterTrialOfGrasses02",
            "afterTrialOfGrasses03", "afterTrialOfGrasses04", "afterTrialOfGrasses05",
            "afterTrialOfGrasses06", "afterTrialOfGrasses07"]
    }
}