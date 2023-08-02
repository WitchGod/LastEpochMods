using MelonLoader;
using System.Linq;
using UnityEngine;
using UniverseLib;
using UniverseLib.Utility;

namespace LastEpochMods
{
    public class Main : MelonMod
    {
        public static MelonLoader.MelonLogger.Instance logger_instance = null;
        public static bool UniverseLibLoaded = false;
        public override void OnInitializeMelon()
        {
            logger_instance = LoggerInstance;            
        }
        public void UniverseLib_OnInitialized()
        {
            UniverseLibLoaded = true;
            LoggerInstance.Msg("Mods init completed");
        }
        public void UniverseLib_LogHandler(string message, UnityEngine.LogType type)
        {
            LoggerInstance.Msg("UniverseLib : " + message);
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Scenes.CurrentName = sceneName;
            if (UniverseLibLoaded)
            {                
                if (Scenes.GameScene())
                {                    
                    OnSceneChanged.Spawner_Placement_Manager.Init();
                    OnSceneChanged.Set_Bonuses_List.Init();
                    OnSceneChanged.Local_Tree_Data.Init();
                    OnSceneChanged.Notifications_.Init();
                    OnSceneChanged.GenerateItem.Init();
                    OnSceneChanged.Ability_Mutator.Init();
                }
            }
        }
        public override void OnLateUpdate()
        {
            if (!Config.Data.mods_config_loaded) { Config.Data.mods_config_loaded = true; Config.Load.Mods(); }
            if (!UniverseLibLoaded)
            {                
                if (UniverseLib.Universe.CurrentGlobalState == UniverseLib.Universe.GlobalState.SetupCompleted)
                {
                    UniverseLibLoaded = true;
                    LoggerInstance.Msg("UnityExplorer found");
                    LoggerInstance.Msg("Mods init completed");
                }
                else if (UniverseLib.Universe.CurrentGlobalState == UniverseLib.Universe.GlobalState.WaitingToSetup)
                {
                    UniverseLib.Config.UniverseLibConfig config = new UniverseLib.Config.UniverseLibConfig()
                    {
                        Disable_EventSystem_Override = false,
                        Force_Unlock_Mouse = true,
                        Unhollowed_Modules_Folder = System.IO.Directory.GetCurrentDirectory() + @"\MelonLoader\"
                    };
                    UniverseLib.Universe.Init(1f, UniverseLib_OnInitialized, UniverseLib_LogHandler, config);
                }
            }
            else
            {
                if (!Mods.Items.HeadHunter.Initialized) { Mods.Items.HeadHunter.Init(); }
                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F1)) { Ui.Menu.isMenuOpen = !Ui.Menu.isMenuOpen; }
                if (Scenes.GameScene())
                {                    
                    if (UnityEngine.Input.GetKeyDown(KeyCode.F9))
                    {
                        Main.logger_instance.Msg("Creating buff...");
                        Buff customBuff = new Buff();
                        customBuff.remainingDuration = 255;
                        Stats.Stat tempStat = new Stats.Stat();
                        tempStat.addedValue = 1.0f;
                        tempStat.increasedValue = 0.3f;
                        tempStat.moreValues = null;
                        tempStat.property = SP.Intelligence;
                        tempStat.specialTag = 0;
                        tempStat.tags = AT.None;
                        customBuff.stat = tempStat;
                        customBuff.name = "testing this buff";
                        Main.logger_instance.Msg("Attempting to buff character...");
                        Mods.Character.AddBuff(customBuff); // Why you so buggy.
                        Main.logger_instance.Msg("Buffing successful.");
                    }
                    if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F10)) { Mods.Character.LevelUp_Once(); }
                    if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F11)) { Mods.Character.LevelUp_Max(); }
                }
            }
        }
        public override void OnGUI()
        {
            Ui.Menu.Update();           
        }        
    }
}
