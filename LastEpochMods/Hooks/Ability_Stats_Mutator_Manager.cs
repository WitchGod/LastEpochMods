﻿using HarmonyLib;

namespace LastEpochMods.Hooks
{
    public class Ability_Stats_Mutator_Manager
    {
        [HarmonyPatch(typeof(AbilityStatsMutatorManager), "OnStatsUpdate")]
        public class OnStatsUpdate
        {
            [HarmonyPostfix]
            static void Postfix(ref AbilityStatsMutatorManager __instance)
            {
                if (Config.Data.mods_config.character.companions.wolf.Enable_summon_max)
                {
                    __instance.canSummonWolvesUpToMaxCompanions = true;
                }
                if (Config.Data.mods_config.character.minions.bone_golem.Enable_addedGolemsPer4Skeletons)
                {
                    __instance.addedGolemsPer4Skeletons = Config.Data.mods_config.character.minions.bone_golem.addedGolemsPer4Skeletons;
                }
            }
        }
    }
}
