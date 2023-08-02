using HarmonyLib;

namespace LastEpochMods.Hooks
{
    public class Common
    {
        public static QuestManager QuestManager;
        public static MonolithProgressManager MonolithProgressManager;
    }

    [HarmonyPatch(typeof(QuestManager), nameof(QuestManager.Awake))]
    public class QuestManager_Awake
    {
        public static void Postfix(ref QuestManager __instance)
        {
            Common.QuestManager = __instance;
        }
    }

    [HarmonyPatch(typeof(MonolithProgressManager), nameof(MonolithProgressManager.Awake))]
    public class MonolithProgressManager_Awake
    {
        public static void Postfix(ref MonolithProgressManager __instance)
        {
            Common.MonolithProgressManager = __instance;
        }
    }
}
