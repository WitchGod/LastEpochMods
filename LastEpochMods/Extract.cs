﻿using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using LastEpochMods.Db;
using MelonLoader;
using Newtonsoft.Json;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;
using UniverseLib;
using static MelonLoader.MelonLogger;

namespace LastEpochMods
{
    public class Extract : MelonMod
    {
        //Functions        
        private UnityEngine.Object GetObject(string name)
        {
            UnityEngine.Object objet = new UnityEngine.Object();
            foreach (UnityEngine.Object obj in RuntimeHelper.FindObjectsOfTypeAll(typeof(UnityEngine.Object)))
            {
                if ((name != "") && (obj.name.Contains(name)))
                {
                    System.Type type = obj.GetActualType();
                    if (type != typeof(TextAsset))
                    {
                        objet = obj;
                        break;
                    }                                 
                }
            }
            return objet;
        }        
        private void GetItems()
        {
            LoggerInstance.Msg("Get All Items");
            UnityEngine.Object obj = GetObject("MasterItemsList");
            System.Type type = obj.GetActualType();            
            if (type == typeof(ItemList))
            {
                ItemList item_list = obj.TryCast<ItemList>();
                int index = 0;
                foreach (Db.Get.Type.Type_Structure type_struct in Db.Get.Type.TypesArray)
                {
                    if (type_struct.Id < 34)
                    {
                        if ((type_struct.Id != 11) && (type_struct.Id != 24)) //Fist and Crosbow
                        {
                            Db.Json.Items new_list_item = new Db.Json.Items();
                            string base_type = type_struct.Name;
                            LoggerInstance.Msg("");
                            LoggerInstance.Msg("Get All Basic " + type_struct.Name);
                            new_list_item.Basic = new System.Collections.Generic.List<Db.Json.Basic>();
                            List<ItemList.EquipmentItem> items = item_list.GetEquipmentSubItems(type_struct.Id);
                            //ItemList.BaseEquipmentItem BaseEquipmentItem = item_list.GetEquipmentBaseItem(type_struct.Id);
                            
                            foreach (var item in items)
                            {
                                LoggerInstance.Msg("Name : " + item.name + ", Id : " + item.subTypeID);
                                System.Collections.Generic.List<string> list_implicits = new System.Collections.Generic.List<string>();
                                list_implicits.Add(item_list.GetItemImplicit(type_struct.Id, item.subTypeID, 0).property.ToString());
                                list_implicits.Add(item_list.GetItemImplicit(type_struct.Id, item.subTypeID, 1).property.ToString());
                                list_implicits.Add(item_list.GetItemImplicit(type_struct.Id, item.subTypeID, 2).property.ToString());
                                new_list_item.Basic.Add(new Db.Json.Basic
                                {
                                    BaseName = item.name,
                                    BaseId = item.subTypeID,
                                    Level = item.levelRequirement,
                                    Implicit = list_implicits
                                });
                            }

                            LoggerInstance.Msg("");
                            LoggerInstance.Msg("Get All Unique " + type_struct.Name);
                            new_list_item.Unique = new System.Collections.Generic.List<Db.Json.Unique>();
                            UnityEngine.Object obj2 = GetObject("UniqueList");
                            System.Type type2 = obj2.GetActualType();
                            if (type2 == typeof(UniqueList))
                            {
                                UniqueList unique_list = obj2.TryCast<UniqueList>();
                                List<UniqueList.Entry> Uniques_List_Entry = unique_list.uniques;
                                foreach (UniqueList.Entry ul_entry in Uniques_List_Entry)
                                {
                                    if (ul_entry.baseType == type_struct.Id)
                                    {
                                        if (!ul_entry.isSetItem)
                                        {
                                            List<UniqueItemMod> mods = ul_entry.mods;
                                            System.Collections.Generic.List<string> unique_mod_list = new System.Collections.Generic.List<string>();
                                            foreach (UniqueItemMod mod in mods)
                                            {
                                                unique_mod_list.Add(mod.property.ToString());
                                            }
                                            LoggerInstance.Msg("Unique : Name = " + ul_entry.name + ", Id = " + ul_entry.uniqueID);
                                            new_list_item.Unique.Add(new Db.Json.Unique
                                            {
                                                BaseId = ul_entry.baseType,
                                                BaseName = "",
                                                Implicit = new System.Collections.Generic.List<string>(),
                                                UniqueId = ul_entry.uniqueID,
                                                UniqueName = ul_entry.name,
                                                Unique_Affixs = unique_mod_list,
                                                Level = ul_entry.levelRequirement
                                            });
                                        }
                                    }
                                }
                            }

                            LoggerInstance.Msg("");
                            LoggerInstance.Msg("Get All Set " + type_struct.Name);
                            new_list_item.Set = new System.Collections.Generic.List<Db.Json.Set>();
                            UnityEngine.Object obj3 = GetObject("UniqueList");
                            System.Type type3 = obj3.GetActualType();
                            if (type3 == typeof(UniqueList))
                            {
                                UniqueList unique_list = obj3.TryCast<UniqueList>();
                                List<UniqueList.Entry> Uniques_List_Entry = unique_list.uniques;
                                foreach (UniqueList.Entry ul_entry in Uniques_List_Entry)
                                {
                                    if (ul_entry.baseType == type_struct.Id)
                                    {
                                        if (ul_entry.isSetItem)
                                        {
                                            LoggerInstance.Msg("Set : Name = " + ul_entry.name + ", Id = " + ul_entry.uniqueID);
                                            List<UniqueItemMod> mods = ul_entry.mods;
                                            System.Collections.Generic.List<string> unique_mod_list = new System.Collections.Generic.List<string>();
                                            foreach (UniqueItemMod mod in mods)
                                            {
                                                unique_mod_list.Add(mod.property.ToString());
                                            }
                                            new_list_item.Set.Add(new Db.Json.Set
                                            {
                                                BaseId = ul_entry.baseType,
                                                BaseName = "",
                                                Implicit = new System.Collections.Generic.List<string>(),
                                                SetId = ul_entry.uniqueID,
                                                SetName = ul_entry.name,
                                                Set_Refs = new System.Collections.Generic.List<Db.Json.Set_Ref>(),
                                                Unique_Affixs = unique_mod_list,
                                                Level = ul_entry.levelRequirement
                                            });
                                        }
                                    }
                                }
                            }
                            LoggerInstance.Msg(type_struct.Name + " : Basic = " + new_list_item.Basic.Count +
                                ", Unique = " + new_list_item.Unique.Count +
                                ", Set = " + new_list_item.Set.Count);
                            string jsonString = JsonConvert.SerializeObject(new_list_item);
                            string path = System.IO.Directory.GetCurrentDirectory() + @"\Mods\Out_LastEpoch" + type_struct.Path;
                            string filename = type_struct.Name.Replace(' ', '_');
                            LoggerInstance.Msg("Save : Path = " + path + ", Filename = " + filename);
                            if (System.IO.File.Exists(path + filename)) { System.IO.File.Delete(path + filename); }
                            if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }
                            System.IO.File.WriteAllText(path + filename, jsonString);
                        }
                    }
                    else if (type_struct.Id == 101) //Affixs
                    {
                        LoggerInstance.Msg("");
                        LoggerInstance.Msg("Get All Affixs");
                        UnityEngine.Object obj5 = GetObject("MasterAffixesList");
                        System.Type type5 = obj5.GetActualType();
                        if (type5 == typeof(AffixList))
                        {
                            if (!Db.Data.Data_init) { Db.Data.InitData(); }
                            AffixList affix_list = obj5.TryCast<AffixList>();
                            Il2CppReferenceArray<AffixList.SingleAffix> single_affix = affix_list.singleAffixes;
                            Il2CppReferenceArray<AffixList.MultiAffix> multi_affix = affix_list.multiAffixes;
                            int i = 0;
                            foreach (AffixList.SingleAffix a in single_affix)
                            {
                                Db.Json.Affixs.Affix shard = Data.SingleAffixToShard(a);
                                Db.Data.DB_Single_Affixs.List.Add(shard);
                                i++;
                            }
                            LoggerInstance.Msg(Db.Data.DB_Single_Affixs.List.Count + " / " +
                                single_affix.Count + " Single Affixs Add in Data.Prefixs");
                            i = 0;
                            foreach (AffixList.MultiAffix a in multi_affix)
                            {
                                Db.Json.Affixs.Affix shard = Data.MultiAffixToShard(a);
                                Db.Data.DB_Multi_Affixs.List.Add(shard);
                                i++;
                            }
                            LoggerInstance.Msg(Db.Data.DB_Multi_Affixs.List.Count + " / " +
                                multi_affix.Count + " Multi Affixs Add in Data.Suffixs");
                            LoggerInstance.Msg("Save " + (Db.Data.DB_Single_Affixs.List.Count + Db.Data.DB_Multi_Affixs.List.Count) + " affixs in Affixs.json");
                            Db.Save.AffixsJson(this);
                        }
                    }
                    index++;
                }
            }
        }
        //Events
        private string scene_name = "";
        private string[] menu_scene_names = { "PersistentUI", "Login", "CharacterSelectScene" };
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            scene_name = sceneName;
        }
        public override void OnLateUpdate()
        {
            if (scene_name != "")
            {
                if ((!menu_scene_names.Contains(scene_name)) && (Input.GetKeyDown(KeyCode.F11)))
                //if ((scene_name != "PersistentUI") && (scene_name != "Login") &&
                //    (scene_name != "CharacterSelectScene") && (Input.GetKeyDown(KeyCode.F11)))
                {
                    GetItems();
                }
                else if (Input.GetKeyDown(KeyCode.F11))
                {
                    LoggerInstance.Msg("Lauch a character before doing this");
                }
            }
        }
    }
}
