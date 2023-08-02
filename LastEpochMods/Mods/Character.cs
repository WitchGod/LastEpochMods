using Il2CppSystem.Linq.Expressions;
using LastEpochMods.Hooks;
using UniverseLib;
using System;
using MelonLoader;
using UniverseLib.Utility;
using System.Collections.Generic;
using System.Text;

namespace LastEpochMods.Mods
{
    public class Character
    {
        private static Actor Player = PlayerFinder.getPlayerActor();
        private static ExperienceTracker ExpTracker = PlayerFinder.getExperienceTracker();
        private static CharacterDataTracker DataTracker = PlayerFinder.getPlayerDataTracker();
        private static LocalTreeData TreeData = PlayerFinder.getLocalTreeData();
        private static BaseHealth Health = PlayerFinder.getLocalPlayerHealth();

        public static void LevelUp_Once(bool displayEffect = true)
        {
            ExpTracker.LevelUp(displayEffect);
        }

        public static void LevelUp_Max()
        {
            for (int i = 0; i < ExperienceTracker.maxLevel; i++)
            {
                LevelUp_Once(false); // To avoid the level-up effect lag.
            }
        }

        public static bool GetIsMastered()
        {
            bool result = false;
            if (TreeData != null)
            {
                if (TreeData.chosenMastery > 0) { result = true; }
            }

            return result;
        }

        public static void ResetMasteries()
        {
            if (DataTracker != null)
            {
                DataTracker.charData.ChosenMastery = 0;
                DataTracker.charData.SaveData();
            }
            else { Main.logger_instance.Error("DataTracker is null"); }

            if (TreeData != null) { TreeData.chosenMastery = 0; }
            else { Main.logger_instance.Error("TreeData is null"); }

            UI.MasteriesUI.Show();
        }

        public static void GodMode()
        {
            if (Health != null)
            {
                bool result = false;
                if (Config.Data.mods_config.character.characterstats.Enable_GodMode) { result = true; }
                Health.damageable = result;
                Health.canDie = result;
                Config.Data.mods_config.character.characterstats.Enable_GodMode = !Config.Data.mods_config.character.characterstats.Enable_GodMode;
            }
        }

        public static void CompleteCampaignQuests()
        {
            if (Hooks.Common.QuestManager == null) { return; }
            Hooks.Common.QuestManager.questList.maxLevelBelowQuestForFullXP = float.MaxValue;

            List<int> l_BannedQuests = new List<int>() { 1, 17 };

            foreach (Quest quest in Hooks.Common.QuestManager.questList.quests)
            {
                if (!quest.playerVisible || string.IsNullOrEmpty(quest.displayName) || quest.IsNullOrDestroyed() || quest == null || quest.questType != QuestType.Normal) { continue; }
                // Funnily enough, the "starting" Monolith quests are marked as QuestType.Normal. We'll just remove them manually.
                if (quest.displayName.Contains("Monolith:")) { continue; }
                // We'll also remove some specific IDs because they defy the above conditions.
                if (l_BannedQuests.Contains(quest.id)) { continue; }

                // Just for logging purposes.
                StringBuilder sb = new StringBuilder();
                sb.Append("Id: ").Append(quest.id);
                sb.Append(" Name: ").Append(quest.name);
                sb.Append(" DisplayName: ").Append(quest.displayName);
                sb.Append(" Visible: ").Append(quest.playerVisible);
                sb.Append(" MainLine: ").Append(quest.mainLineQuest);
                sb.Append(" QuestType: ").Append(quest.questType);

                try
                {
                    quest.startQuest();
                    quest.completeQuest();

                    sb.Append(" Status: STARTED AND COMPLETED.");
                }
                catch (Exception ex)
                {
                    sb.Append(" Status: ERROR.");
                }
                MelonLogger.Msg(sb.ToString());
            }

            UnlockPortalInteraction.unlockPortal(PlayerFinder.getPlayerActor().gameObject);
        }

        public static void UnlockMonoliths()
        {
            if (Hooks.Common.MonolithProgressManager == null) { return; }

            foreach (TimelineID timelineID in Enum.GetValues(typeof(TimelineID)))
            {
                Hooks.Common.MonolithProgressManager.unlockTimeline(timelineID, MonolithTimeline.Difficulty.NORMAL);
                Hooks.Common.MonolithProgressManager.unlockTimeline(timelineID, MonolithTimeline.Difficulty.EMPOWERED);
            }

            MonolithSidebarTracker monolithSidebarTracker = new MonolithSidebarTracker();
            foreach (MonolithBridgeInfo monolithBridgeInfo in monolithSidebarTracker.bridges)
                monolithBridgeInfo.SetBridgeUnlocked(true);
        }
    }
}
