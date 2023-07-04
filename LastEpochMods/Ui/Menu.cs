﻿using UnityEngine;

namespace LastEpochMods.Ui
{
    public class Menu
    {
        public static Texture2D windowBackground = null;
        public static Texture2D texture_grey = null;
        public static Texture2D texture_green = null;

        public static void Update()
        {
            if (!Config.Data.mods_config.Equals(Config.Data.mods_config_duplicate))
            {
                Config.Data.mods_config_duplicate = Config.Data.mods_config;
                Config.Save.Mods();
            }
            if (windowBackground == null) { windowBackground = Functions.MakeTextureFromColor(2, 2, Color.black); }
            if (texture_grey == null) { texture_grey = Functions.MakeTextureFromColor(2, 2, Color.grey); }
            if (texture_green == null) { texture_green = Functions.MakeTextureFromColor(2, 2, Color.green); }
            if (isMenuOpen)
            {
                GUI.Box(new Rect(0f, 0f, Screen.width, Screen.height), "");
                float start_x = (Screen.width / 2) - (Menu_Window_Rect.width / 2);
                float start_y = 0;
                float pos_x = start_x;
                float pos_y = start_y;
                GUI.DrawTexture(new Rect(pos_x, pos_y, Menu_Window_Rect.width, Menu_Size_h), windowBackground);
                GUI.depth = 0;
                pos_x += btn_margin_w;
                pos_y += btn_margin_h;

                if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Drop", Styles.Button_Style(ShowItemDropSection))) { Btn_ItemsDrop_Click(); }
                pos_x += btn_size_w + btn_margin_w;
                if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Character", Styles.Button_Style(ShowCharacterSection))) { Btn_Character_Click(); }
                pos_x += btn_size_w + btn_margin_w;
                if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Scene", Styles.Button_Style(ShowSceneSection))) { Btn_Scene_Click(); }
                pos_x += btn_size_w + btn_margin_w;
                if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Items", Styles.Button_Style(ShowItemsSection))) { Btn_Items_Click(); }
                pos_x += btn_size_w + btn_margin_w;
                if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Dev Mode", Styles.Button_Style(Mods.Developer.DevLoaded))) { Mods.Developer.ShowHide_Dev(); }
                pos_x += btn_size_w + btn_margin_w;

                //Drop
                if (ShowItemDropSection)
                {
                    pos_x = start_x;
                    pos_y = start_y + Menu_Size_h;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), ItemsDrop_h), windowBackground);
                    pos_x += 5f;
                    Config.Data.mods_config.items.DeathItemDrop_goldMultiplier = CustomControls.FloatValue("Gold Multiplier", 0f, 255f, Config.Data.mods_config.items.DeathItemDrop_goldMultiplier, pos_x, pos_y, Config.Data.mods_config.items.Enable_DeathItemDrop_goldMultiplier, Ui.Menu.Btn_ItemsDrop_OnDeath_GoldMultiplier_Click);
                    pos_y += 85;
                    Config.Data.mods_config.items.DeathItemDrop_ItemMultiplier = CustomControls.FloatValue("Items Multiplier", 0f, 255f, Config.Data.mods_config.items.DeathItemDrop_ItemMultiplier, pos_x, pos_y, Config.Data.mods_config.items.Enable_DeathItemDrop_ItemMultiplier, Ui.Menu.Btn_ItemsDrop_OnDeath_ItemsMultiplier_Click);
                    pos_y += 85;
                    if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Item Data", Styles.Button_Style(ShowItemDataSection))) { Btn_ItemData_Click(); }
                    pos_y += 45;
                    if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Auto Pickup", Styles.Button_Style(ShowAutoPickupSection))) { Btn_AutoPickup_Click(); }
                    pos_y += 45;
                    if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Bonuses", Styles.Button_Style(ShowBonusesSection))) { Btn_Bonuses_Click(); }
                    pos_y += 45;
                    Config.Data.mods_config.items.DeathItemDrop_Experience = CustomControls.LongValue("Experience Multiplier", 0, 9999, Config.Data.mods_config.items.DeathItemDrop_Experience, pos_x, pos_y, Config.Data.mods_config.items.Enable_DeathItemDrop_Experience, Ui.Menu.Btn_ItemsDrop_OnDeath_Experience_Click);                    
                }
                else { ShowItemDataSection = false; ShowAutoPickupSection = false; ShowBonusesSection = false; }
                if (ShowItemDataSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 1);
                    pos_y = start_y + Menu_Size_h + 170;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), item_data_h), windowBackground);
                    pos_x += 5;
                    Config.Data.mods_config.items.GenerateItem_Rarity = CustomControls.ByteValue("Rarity", 0, 9, Config.Data.mods_config.items.GenerateItem_Rarity, pos_x, pos_y, Config.Data.mods_config.items.Enable_Rarity, Ui.Menu.Btn_ItemsDrop_Rarity_Roll_Click);
                    pos_y += 85;
                    Config.Data.mods_config.items.Roll_Implicit = CustomControls.ByteValue("Implicits", 0, 255, Config.Data.mods_config.items.Roll_Implicit, pos_x, pos_y, Config.Data.mods_config.items.Enable_RollImplicit, Ui.Menu.Btn_ItemsDrop_Implicit_Roll_Click);
                    pos_y += 85;
                    Config.Data.mods_config.items.Roll_ForgingPotencial = CustomControls.ByteValue("Forgin Potencial", 0, 255, Config.Data.mods_config.items.Roll_ForgingPotencial, pos_x, pos_y, Config.Data.mods_config.items.Enable_ForgingPotencial, Ui.Menu.Btn_ItemsDrop_ForginPotencial_Roll_Click);
                    pos_y += 85;
                    Config.Data.mods_config.items.Roll_AffixValue = CustomControls.ByteValue("Affix Values", 0, 255, Config.Data.mods_config.items.Roll_AffixValue, pos_x, pos_y, Config.Data.mods_config.items.Enable_AffixsValue, Ui.Menu.Btn_ItemsDrop_Affix_Values_Click);
                    pos_y += 85;
                    Config.Data.mods_config.items.Roll_AffixTier = CustomControls.ByteValue("Affix Tier", 1, 7, Config.Data.mods_config.items.Roll_AffixTier, pos_x, pos_y, Config.Data.mods_config.items.Enable_AffixsTier, Ui.Menu.Btn_ItemsDrop_Affix_Tier_Click);
                    pos_y += 85;
                    Config.Data.mods_config.items.Roll_UniqueMod = CustomControls.ByteValue("Unique Mods", 0, 255, Config.Data.mods_config.items.Roll_UniqueMod, pos_x, pos_y, Config.Data.mods_config.items.Enable_UniqueMod, Ui.Menu.Btn_ItemsDrop_Unique_Mods_Click);
                    pos_y += 85;
                    Config.Data.mods_config.items.Roll_Legendary_Potencial = System.Convert.ToInt32(CustomControls.FloatValue("Legendary Potencial", 0f, 4f, Config.Data.mods_config.items.Roll_Legendary_Potencial, pos_x, pos_y, Config.Data.mods_config.items.Enable_RollLegendayPotencial, Ui.Menu.Btn_ItemsDrop_Legendary_Potencial_Click));
                    pos_y += 85;
                    Config.Data.mods_config.items.Roll_Weaver_Will = System.Convert.ToInt32(CustomControls.FloatValue("Weaver Will", 5f, 28f, Config.Data.mods_config.items.Roll_Weaver_Will, pos_x, pos_y, Config.Data.mods_config.items.Enable_RollWeaverWill, Ui.Menu.Btn_ItemsDrop_Weaver_Will_Click));
                }                
                if (ShowAutoPickupSection)
                {
                    pos_x = start_x + (btn_size_w + btn_margin_w);
                    pos_y = start_y + Menu_Size_h + 215;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), AutoPickup_h), windowBackground);
                    pos_x += 5;
                    CustomControls.EnableButton("Gold", pos_x, pos_y, Config.Data.mods_config.auto_loot.AutoPickup_Gold, Ui.Menu.Btn_AutoPickup_Gold_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("Keys", pos_x, pos_y, Config.Data.mods_config.auto_loot.AutoPickup_Key, Ui.Menu.Btn_AutoPickup_Key_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("Unique & Set", pos_x, pos_y, Config.Data.mods_config.auto_loot.AutoPickup_UniqueAndSet, Ui.Menu.Btn_AutoPickup_UniqueAndSet_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("Xp Tome", pos_x, pos_y, Config.Data.mods_config.auto_loot.AutoPickup_XpTome, Ui.Menu.Btn_AutoPickup_XpTome_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("Materials", pos_x, pos_y, Config.Data.mods_config.auto_loot.AutoPickup_Craft, Ui.Menu.Btn_AutoPickup_Materials_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("AutoStore Materials", pos_x, pos_y, Config.Data.mods_config.auto_loot.AutoStore_Materials, Ui.Menu.Btn_AutoSrore_Materials_Click);
                }
                if (ShowBonusesSection)
                {
                    pos_x = start_x + (btn_size_w + btn_margin_w);
                    pos_y = start_y + Menu_Size_h + 260;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), Bonuses_h), windowBackground);
                    pos_x += 5;
                    Config.Data.mods_config.items.increase_equipment_droprate = CustomControls.FloatValue("Equipement", 0f, 255f, Config.Data.mods_config.items.increase_equipment_droprate, pos_x, pos_y, Config.Data.mods_config.items.Enable_increase_equipment_droprate, Ui.Menu.Btn_Bonuses_Equipement_Click);
                    pos_y += 85;
                    Config.Data.mods_config.items.increase_equipmentshards_droprate = CustomControls.FloatValue("Shards", 0f, 255f, Config.Data.mods_config.items.increase_equipmentshards_droprate, pos_x, pos_y, Config.Data.mods_config.items.Enable_increase_equipmentshards_droprate, Ui.Menu.Btn_Bonuses_Shards_Click);
                    pos_y += 85;
                    Config.Data.mods_config.items.increase_uniques_droprate = CustomControls.FloatValue("Unique", 0f, 255f, Config.Data.mods_config.items.increase_uniques_droprate, pos_x, pos_y, Config.Data.mods_config.items.Enable_increase_uniques_droprate, Ui.Menu.Btn_Bonuses_Unique_Click);
                    pos_y += 85;
                }
                //Character
                if (ShowCharacterSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 1);
                    pos_y = start_y + Menu_Size_h;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), Character_h), windowBackground);
                    pos_x += 5;
                    Config.Data.mods_config.character.attack_rate = CustomControls.FloatValue("Attack Rate", 0, 255, Config.Data.mods_config.character.attack_rate, pos_x, pos_y, Config.Data.mods_config.character.Enable_attack_rate, Ui.Menu.Btn_Character_AttackRate_Click);
                    pos_y += 85;
                    Config.Data.mods_config.character.leach_rate = CustomControls.FloatValue("Leach Rate", 0, 255, Config.Data.mods_config.character.leach_rate, pos_x, pos_y, Config.Data.mods_config.character.Enable_leach_rate, Ui.Menu.Btn_Character_LeachRate_Click);
                    pos_y += 85;
                    if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Skills", Styles.Button_Style(ShowSkillsSection))) { Btn_Skills_Click(); }
                    pos_y += 45;
                    if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Tree", Styles.Button_Style(ShowTreeSection))) { Btn_Tree_Click(); }
                }
                else { ShowSkillsSection = false; ShowTreeSection = false; }
                if (ShowSkillsSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 2);
                    pos_y = start_y + Menu_Size_h + 170;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), Skills_h), windowBackground);
                    pos_x += 5;
                    Config.Data.mods_config.character.number_of_unlocked_slots = CustomControls.ByteValue("Unlocked Slots", 0, 5, Config.Data.mods_config.character.number_of_unlocked_slots, pos_x, pos_y, Config.Data.mods_config.character.Enable_number_of_unlocked_slots, Ui.Menu.Btn_Character_NbUnlockedSlot_Click);
                    pos_y += 85;
                    Config.Data.mods_config.character.skilltree_level = CustomControls.ByteValue("Skills Points", 0, 255, Config.Data.mods_config.character.skilltree_level, pos_x, pos_y, Config.Data.mods_config.character.Enable_skilltree_level, Ui.Menu.Btn_Character_SkillTree_Click);
                    pos_y += 85;
                    if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Remove", Styles.Button_Style(ShowSkillsRemoveSection))) { Btn_SkillsRemove_Click(); }
                    pos_y += 45;
                    CustomControls.EnableButton("Channel : ManaRegen", pos_x, pos_y, Config.Data.mods_config.character.Enable_noManaRegenWhileChanneling, Ui.Menu.Btn_Character_NoManaWhileChanneling_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("NoMana : Don't Stop", pos_x, pos_y, Config.Data.mods_config.character.Enable_stopWhenOutOfMana, Ui.Menu.Btn_Character_StopWhenOutMana_Click);
                }
                else { ShowSkillsRemoveSection = false; }
                if (ShowSkillsRemoveSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 3);
                    pos_y = start_y + Menu_Size_h + 340;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), SkillsRemove_h), windowBackground);
                    pos_x += 5;
                    CustomControls.EnableButton("Mana Cost", pos_x, pos_y, Config.Data.mods_config.character.Enable_manaCost, Ui.Menu.Btn_Character_ManaCost_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("Channel Cost", pos_x, pos_y, Config.Data.mods_config.character.Enable_channel_cost, Ui.Menu.Btn_Character_ChannelCost_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("Cooldown", pos_x, pos_y, Config.Data.mods_config.character.Enable_RemoveCooldown, Ui.Menu.Btn_Character_RemoveCooldown_Click);
                }
                if (ShowTreeSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 2);
                    pos_y = start_y + Menu_Size_h + 215;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), Tree_h), windowBackground);
                    pos_x += 5;
                    Config.Data.mods_config.character.passiveTree_pointsEarnt = CustomControls.UshortValue("Passive Tree Points", 0, 65535, Config.Data.mods_config.character.passiveTree_pointsEarnt, pos_x, pos_y, Config.Data.mods_config.character.Enable_passiveTree_pointsEarnt, Ui.Menu.Btn_Character_PassivePointsEarn_Click);
                }
                //Scene
                if (ShowSceneSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 2);
                    pos_y = start_y + Menu_Size_h;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), Scene_h), windowBackground);
                    pos_x += 5;
                    Config.Data.mods_config.scene.SpawnerPlacementManager_defaultSpawnerDensity = CustomControls.FloatValue("Density Multiplier", 0f, 255f, Config.Data.mods_config.scene.SpawnerPlacementManager_defaultSpawnerDensity, pos_x, pos_y, Config.Data.mods_config.scene.Enable_SpawnerPlacementManager_defaultSpawnerDensity, Ui.Menu.Btn_Scene_Density_Click);
                    pos_y += 85;
                    Config.Data.mods_config.scene.SpawnerPlacementManager_IncreaseExperience = CustomControls.FloatValue("Experience Multiplier", 0f, 255f, Config.Data.mods_config.scene.SpawnerPlacementManager_IncreaseExperience, pos_x, pos_y, Config.Data.mods_config.scene.Enable_SpawnerPlacementManager_IncreaseExperience, Ui.Menu.Btn_Scene_Experience_Click);
                    pos_y += 85;
                    CustomControls.EnableButton("Waypoint Unlock", pos_x, pos_y, Config.Data.mods_config.scene.Enable_Waypoint_Unlock, Ui.Menu.Btn_Scene_Waypoint_Unlock_Click);
                    pos_y += 45;
                    if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Dungeons", Styles.Button_Style(ShowDungeonsSection))) { Btn_Dungeons_Click(); }
                    pos_y += 45;
                    if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Monoliths", Styles.Button_Style(ShowMonolithsSection))) { Btn_Monoliths_Click(); }                    
                }
                else { ShowDungeonsSection = false; ShowMonolithsSection = false; }
                if (ShowDungeonsSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 3);
                    pos_y = start_y + Menu_Size_h + 215;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), Dungeons_h), windowBackground);
                    pos_x += 5;
                    CustomControls.EnableButton("No Key Need", pos_x, pos_y, Config.Data.mods_config.scene.Enable_Dungeons_WithoutKey, Ui.Menu.Btn_Dungeons_WithoutKey_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("Objective Reveal", pos_x, pos_y, Config.Data.mods_config.scene.Enable_Dungeons_ObjectiveReveal, Ui.Menu.Btn_Dungeon_Objective_Reveal_OnStart_Click);
                }
                if (ShowMonolithsSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 3);
                    pos_y = start_y + Menu_Size_h + 260;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), Monolith_h), windowBackground);
                    pos_x += 5;
                    CustomControls.EnableButton("Stability Max", pos_x, pos_y, Config.Data.mods_config.scene.Enable_Monolith_Stability, Ui.Menu.Btn_Monolith_Stability_Click);
                    pos_y += 45;
                    Config.Data.mods_config.scene.Max_Stability = System.Convert.ToInt32(CustomControls.FloatValue("Max Stability", 0f, 255f, Config.Data.mods_config.scene.Max_Stability, pos_x, pos_y, Config.Data.mods_config.scene.Enable_Monolith_Overide_Max_Stability, Ui.Menu.Btn_Monolith_Override_Max_Stability_Click));
                    pos_y += 85;
                    Config.Data.mods_config.scene.Monolith_EnnemiesDefeat_OnStart = System.Convert.ToInt32(CustomControls.FloatValue("Enemies Defeat", 0f, 255f, Config.Data.mods_config.scene.Monolith_EnnemiesDefeat_OnStart, pos_x, pos_y, Config.Data.mods_config.scene.Enable_Monolith_EnnemiesDefeat_OnStart, Ui.Menu.Btn_Monolith_Enemies_Defeat_OnStart_Click));
                    pos_y += 85;
                    CustomControls.EnableButton("Objective Reveal", pos_x, pos_y, Config.Data.mods_config.scene.Enable_Monolith_ObjectiveReveal, Ui.Menu.Btn_Monolith_Objective_Reveal_OnStart_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("Complete Objective", pos_x, pos_y, Config.Data.mods_config.scene.Enable_Monolith_Complete_Objective, Ui.Menu.Btn_Monolith_Complete_Objective_OnStart_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("No Lost When Die", pos_x, pos_y, Config.Data.mods_config.scene.Enable_Monolith_NoDie, Ui.Menu.Btn_Monolith_NoDie_Click);
                }
                //Items
                if (ShowItemsSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 3);
                    pos_y = start_y + Menu_Size_h;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), Items_h), windowBackground);
                    pos_x += 5;
                    if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Remove Req", Styles.Button_Style(ShowRemoveReqSection))) { Btn_RemoveReq_Click(); }
                    pos_y += 45;
                    if (GUI.Button(new Rect(pos_x, pos_y, btn_size_w, btn_size_h), "Affixs", Styles.Button_Style(ShowAffixsSection))) { Btn_Affixs_Click(); }
                    pos_y += 45;
                }
                else { ShowRemoveReqSection = false; ShowAffixsSection = false; }
                if (ShowRemoveReqSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 4);
                    pos_y = start_y + Menu_Size_h;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), RemoveReq_h), windowBackground);
                    pos_x += 5;
                    CustomControls.EnableButton("Remove Level", pos_x, pos_y, Config.Data.mods_config.items.Remove_LevelReq, Ui.Menu.Btn_Items_Remove_Level_Req_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("Remove Class", pos_x, pos_y, Config.Data.mods_config.items.Remove_ClassReq, Ui.Menu.Btn_Items_Remove_Class_Req_Click);
                    pos_y += 45;
                    CustomControls.EnableButton("Remove SubClass", pos_x, pos_y, Config.Data.mods_config.items.Remove_SubClassReq, Ui.Menu.Btn_Items_Remove_SubClass_Req_Click);
                }
                if (ShowAffixsSection)
                {
                    pos_x = start_x + ((btn_size_w + btn_margin_w) * 4);
                    pos_y = start_y + Menu_Size_h + 45;
                    GUI.DrawTexture(new Rect(pos_x, pos_y, (btn_size_w + (2 * btn_margin_w)), Affixs_h), windowBackground);
                    pos_x += 5;
                    Config.Data.mods_config.affixs.Affixs_Multiplier = (int)CustomControls.IntValue("multiply Rolls", 0f, 255f, Config.Data.mods_config.affixs.Affixs_Multiplier, pos_x, pos_y, Config.Data.mods_config.affixs.Enable_Affixs_Multiplier, Ui.Menu.Btn_Affix_Multiplier_Click);
                    pos_y += 85;
                }
            }
        }
        
        #region Menu
        public static bool isMenuOpen;
        public static int WindowId = 0;
        public static Rect Menu_Window_Rect = new Rect(5f, 5f, 1030f, 860f);
        private static float Menu_Size_h = 50f;                
        private static float btn_size_w = 200f;
        private static float btn_size_h = 40f;
        private static float btn_margin_w = 5f;
        private static float btn_margin_h = 5f;
        #endregion
        #region ItemDrop
        private static bool ShowItemDropSection = false;
        public static float ItemsDrop_h = 390f;

        public static void Btn_ItemsDrop_Click()
        {
            ShowCharacterSection = false;
            ShowSceneSection = false;
            ShowItemsSection = false;        
            ShowItemDropSection = !ShowItemDropSection;            
        }
        public static void Btn_ItemsDrop_OnDeath_GoldMultiplier_Click()
        {
            Config.Data.mods_config.items.Enable_DeathItemDrop_goldMultiplier = !Config.Data.mods_config.items.Enable_DeathItemDrop_goldMultiplier;            
        }
        public static void Btn_ItemsDrop_OnDeath_ItemsMultiplier_Click()
        {
            Config.Data.mods_config.items.Enable_DeathItemDrop_ItemMultiplier = !Config.Data.mods_config.items.Enable_DeathItemDrop_ItemMultiplier;           
        }
        public static void Btn_ItemsDrop_OnDeath_AdditionalRare_Click()
        {
            Config.Data.mods_config.items.Enable_DeathItemDrop_AdditionalRare = !Config.Data.mods_config.items.Enable_DeathItemDrop_AdditionalRare;            
        }
        public static void Btn_ItemsDrop_OnDeath_Experience_Click()
        {
            Config.Data.mods_config.items.Enable_DeathItemDrop_Experience = !Config.Data.mods_config.items.Enable_DeathItemDrop_Experience;            
        }        
        #region ItemData
        private static bool ShowItemDataSection = false;
        public static float item_data_h = 680f;

        public static void Btn_ItemData_Click()
        {
            ShowAutoPickupSection = false;
            ShowBonusesSection = false;
            ShowItemDataSection = !ShowItemDataSection;
        }
        public static void Btn_ItemsDrop_Rarity_Roll_Click()
        {
            Config.Data.mods_config.items.Enable_Rarity = !Config.Data.mods_config.items.Enable_Rarity;            
        }
        public static void Btn_ItemsDrop_Implicit_Roll_Click()
        {
            Config.Data.mods_config.items.Enable_RollImplicit = !Config.Data.mods_config.items.Enable_RollImplicit;            
        }
        public static void Btn_ItemsDrop_ForginPotencial_Roll_Click()
        {
            Config.Data.mods_config.items.Enable_ForgingPotencial = !Config.Data.mods_config.items.Enable_ForgingPotencial;            
        }
        public static void Btn_ItemsDrop_Affix_Values_Click()
        {
            Config.Data.mods_config.items.Enable_AffixsValue = !Config.Data.mods_config.items.Enable_AffixsValue;            
        }
        public static void Btn_ItemsDrop_Affix_Tier_Click()
        {
            Config.Data.mods_config.items.Enable_AffixsTier = !Config.Data.mods_config.items.Enable_AffixsTier;            
        }
        public static void Btn_ItemsDrop_Unique_Mods_Click()
        {
            Config.Data.mods_config.items.Enable_UniqueMod = !Config.Data.mods_config.items.Enable_UniqueMod;            
        }
        public static void Btn_ItemsDrop_Legendary_Potencial_Click()
        {
            Config.Data.mods_config.items.Enable_RollLegendayPotencial = !Config.Data.mods_config.items.Enable_RollLegendayPotencial;            
        }
        public static void Btn_ItemsDrop_Weaver_Will_Click()
        {
            Config.Data.mods_config.items.Enable_RollWeaverWill = !Config.Data.mods_config.items.Enable_RollWeaverWill;            
        }
        #endregion        
        #region AutoLoot
        private static bool ShowAutoPickupSection = false;
        public static float AutoPickup_h = 270f;

        public static void Btn_AutoPickup_Click()
        {
            ShowItemDataSection = false;
            ShowBonusesSection = false;
            ShowAutoPickupSection = !ShowAutoPickupSection;            
        }
        public static void Btn_AutoPickup_Gold_Click()
        {
            Config.Data.mods_config.auto_loot.AutoPickup_Gold = !Config.Data.mods_config.auto_loot.AutoPickup_Gold;            
        }
        public static void Btn_AutoPickup_Key_Click()
        {
            Config.Data.mods_config.auto_loot.AutoPickup_Key = !Config.Data.mods_config.auto_loot.AutoPickup_Key;            
        }
        public static void Btn_AutoPickup_UniqueAndSet_Click()
        {
            Config.Data.mods_config.auto_loot.AutoPickup_UniqueAndSet = !Config.Data.mods_config.auto_loot.AutoPickup_UniqueAndSet;            
        }
        public static void Btn_AutoPickup_XpTome_Click()
        {
            Config.Data.mods_config.auto_loot.AutoPickup_XpTome = !Config.Data.mods_config.auto_loot.AutoPickup_XpTome;            
        }
        public static void Btn_AutoPickup_Materials_Click()
        {
            Config.Data.mods_config.auto_loot.AutoPickup_Craft = !Config.Data.mods_config.auto_loot.AutoPickup_Craft;            
        }        
        public static void Btn_AutoSrore_Materials_Click()
        {
            Config.Data.mods_config.auto_loot.AutoStore_Materials = !Config.Data.mods_config.auto_loot.AutoStore_Materials;            
        }
        #endregion
        #region Bonuses
        private static bool ShowBonusesSection = false;
        public static float Bonuses_h = 255f;

        public static void Btn_Bonuses_Click()
        {
            ShowItemDataSection = false;
            ShowAutoPickupSection = false;
            ShowBonusesSection = !ShowBonusesSection;
        }
        public static void Btn_Bonuses_Equipement_Click()
        {
            Config.Data.mods_config.items.Enable_increase_equipment_droprate = !Config.Data.mods_config.items.Enable_increase_equipment_droprate;            
        }
        public static void Btn_Bonuses_Shards_Click()
        {
            Config.Data.mods_config.items.Enable_increase_equipmentshards_droprate = !Config.Data.mods_config.items.Enable_increase_equipmentshards_droprate;            
        }
        public static void Btn_Bonuses_Unique_Click()
        {
            Config.Data.mods_config.items.Enable_increase_uniques_droprate = !Config.Data.mods_config.items.Enable_increase_uniques_droprate;            
        }
        #endregion
        #endregion
        #region Character
        private static bool ShowCharacterSection = false;
        public static float Character_h = 260f;

        public static void Btn_Character_Click()
        {
            ShowItemDropSection = false;
            ShowSceneSection = false;
            ShowItemsSection = false;
            ShowCharacterSection = !ShowCharacterSection;
        }
        public static void Btn_Character_AttackRate_Click()
        {
            Config.Data.mods_config.character.Enable_attack_rate = !Config.Data.mods_config.character.Enable_attack_rate;            
        }
        public static void Btn_Character_LeachRate_Click()
        {
            Config.Data.mods_config.character.Enable_leach_rate = !Config.Data.mods_config.character.Enable_leach_rate;            
        }
        #region Skills
        private static bool ShowSkillsSection = false;
        public static float Skills_h = 305;

        public static void Btn_Skills_Click()
        {
            ShowTreeSection = false;
            ShowSkillsSection = !ShowSkillsSection;
        }
        #region Remove
        private static bool ShowSkillsRemoveSection = false;
        public static float SkillsRemove_h = 135;

        public static void Btn_SkillsRemove_Click()
        {
            ShowSkillsRemoveSection = !ShowSkillsRemoveSection;
        }
        public static void Btn_Character_NbUnlockedSlot_Click()
        {
            Config.Data.mods_config.character.Enable_number_of_unlocked_slots = !Config.Data.mods_config.character.Enable_number_of_unlocked_slots;            
        }
        public static void Btn_Character_ManaCost_Click()
        {
            Config.Data.mods_config.character.Enable_manaCost = !Config.Data.mods_config.character.Enable_manaCost;            
        }
        public static void Btn_Character_ChannelCost_Click()
        {
            Config.Data.mods_config.character.Enable_channel_cost = !Config.Data.mods_config.character.Enable_channel_cost;            
        }
        public static void Btn_Character_NoManaWhileChanneling_Click()
        {
            Config.Data.mods_config.character.Enable_noManaRegenWhileChanneling = !Config.Data.mods_config.character.Enable_noManaRegenWhileChanneling;            
        }
        public static void Btn_Character_StopWhenOutMana_Click()
        {
            Config.Data.mods_config.character.Enable_stopWhenOutOfMana = !Config.Data.mods_config.character.Enable_stopWhenOutOfMana;            
        }
        public static void Btn_Character_RemoveCooldown_Click()
        {
            Config.Data.mods_config.character.Enable_RemoveCooldown = !Config.Data.mods_config.character.Enable_RemoveCooldown;            
        }
        public static void Btn_Character_SkillTree_Click()
        {
            Config.Data.mods_config.character.Enable_skilltree_level = !Config.Data.mods_config.character.Enable_skilltree_level;            
        }
        #endregion
        #endregion
        #region Passive
        private static bool ShowTreeSection = false;
        public static float Tree_h = 85f;

        public static void Btn_Tree_Click()
        {
            ShowSkillsSection = false;
            ShowTreeSection = !ShowTreeSection;
        }
        public static void Btn_Character_PassivePointsEarn_Click()
        {
            Config.Data.mods_config.character.Enable_passiveTree_pointsEarnt = !Config.Data.mods_config.character.Enable_passiveTree_pointsEarnt;            
        }
        #endregion        
        #endregion
        #region Scenes
        private static bool ShowSceneSection = false;
        public static float Scene_h = 305f;

        public static void Btn_Scene_Click()
        {
            ShowItemDropSection = false;
            ShowCharacterSection = false;            
            ShowItemsSection = false;
            ShowSceneSection = !ShowSceneSection;            
        }
        public static void Btn_Scene_Density_Click()
        {
            Config.Data.mods_config.scene.Enable_SpawnerPlacementManager_defaultSpawnerDensity = !Config.Data.mods_config.scene.Enable_SpawnerPlacementManager_defaultSpawnerDensity;            
        }
        public static void Btn_Scene_Experience_Click()
        {
            Config.Data.mods_config.scene.Enable_SpawnerPlacementManager_IncreaseExperience = !Config.Data.mods_config.scene.Enable_SpawnerPlacementManager_IncreaseExperience;            
        }        
        public static void Btn_Scene_Waypoint_Unlock_Click()
        {
            Config.Data.mods_config.scene.Enable_Waypoint_Unlock = !Config.Data.mods_config.scene.Enable_Waypoint_Unlock;            
        }
        #region Dungeons
        private static bool ShowDungeonsSection = false;
        public static float Dungeons_h = 90f;

        public static void Btn_Dungeons_Click()
        {
            ShowMonolithsSection = false;
            ShowDungeonsSection = !ShowDungeonsSection;
        }
        public static void Btn_Dungeons_WithoutKey_Click()
        {
            Config.Data.mods_config.scene.Enable_Dungeons_WithoutKey = !Config.Data.mods_config.scene.Enable_Dungeons_WithoutKey;            
        }
        public static void Btn_Dungeon_Objective_Reveal_OnStart_Click()
        {
            Config.Data.mods_config.scene.Enable_Dungeons_ObjectiveReveal = !Config.Data.mods_config.scene.Enable_Dungeons_ObjectiveReveal;            
        }
        #endregion
        #region Monoliths
        private static bool ShowMonolithsSection = false;
        public static float Monolith_h = 350f;

        public static void Btn_Monoliths_Click()
        {
            ShowDungeonsSection = false;
            ShowMonolithsSection = !ShowMonolithsSection;
        }
        public static void Btn_Monolith_Stability_Click()
        {
            Config.Data.mods_config.scene.Enable_Monolith_Stability = !Config.Data.mods_config.scene.Enable_Monolith_Stability;            
        }
        public static void Btn_Monolith_Override_Max_Stability_Click()
        {
            Config.Data.mods_config.scene.Enable_Monolith_Overide_Max_Stability = !Config.Data.mods_config.scene.Enable_Monolith_Overide_Max_Stability;            
        }
        public static void Btn_Monolith_Enemies_Defeat_OnStart_Click()
        {
            Config.Data.mods_config.scene.Enable_Monolith_EnnemiesDefeat_OnStart = !Config.Data.mods_config.scene.Enable_Monolith_EnnemiesDefeat_OnStart;            
        }
        public static void Btn_Monolith_Objective_Reveal_OnStart_Click()
        {
            Config.Data.mods_config.scene.Enable_Monolith_ObjectiveReveal = !Config.Data.mods_config.scene.Enable_Monolith_ObjectiveReveal;            
        }
        public static void Btn_Monolith_Complete_Objective_OnStart_Click()
        {
            Config.Data.mods_config.scene.Enable_Monolith_Complete_Objective = !Config.Data.mods_config.scene.Enable_Monolith_Complete_Objective;            
        }
        public static void Btn_Monolith_NoDie_Click()
        {
            Config.Data.mods_config.scene.Enable_Monolith_NoDie = !Config.Data.mods_config.scene.Enable_Monolith_NoDie;            
        }
        #endregion
        #endregion        
        #region Items
        private static bool ShowItemsSection = false;
        public static float Items_h = 90;

        public static void Btn_Items_Click()
        {
            ShowItemDropSection = false;
            ShowCharacterSection = false;
            ShowSceneSection = false;
            ShowItemsSection = !ShowItemsSection;
        }
        #region Remove Req
        private static bool ShowRemoveReqSection = false;
        public static float RemoveReq_h = 135f;

        public static void Btn_RemoveReq_Click()
        {
            ShowAffixsSection = false;
            ShowRemoveReqSection = !ShowRemoveReqSection;
        }
        public static void Btn_Items_Remove_Level_Req_Click()
        {
            Config.Data.mods_config.items.Remove_LevelReq = !Config.Data.mods_config.items.Remove_LevelReq;            
        }
        public static void Btn_Items_Remove_Class_Req_Click()
        {
            Config.Data.mods_config.items.Remove_ClassReq = !Config.Data.mods_config.items.Remove_ClassReq;            
        }
        public static void Btn_Items_Remove_SubClass_Req_Click()
        {
            Config.Data.mods_config.items.Remove_SubClassReq = !Config.Data.mods_config.items.Remove_SubClassReq;            
        }
        #endregion
        #region Affixs
        private static bool ShowAffixsSection = false;
        public static float Affixs_h = 85;

        public static void Btn_Affixs_Click()
        {
            ShowRemoveReqSection = false;
            ShowAffixsSection = !ShowAffixsSection;
        }
        public static void Btn_Affix_Multiplier_Click()
        {
            Config.Data.mods_config.affixs.Enable_Affixs_Multiplier = !Config.Data.mods_config.affixs.Enable_Affixs_Multiplier;            
        }
        #endregion
        #endregion
    }
}
