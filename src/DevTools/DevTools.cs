using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using RpgTools.Saving;
using RpgTools;

public class DevTools : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] int experience = 200;
    static int ExperienceToAdd = 0;

    static int selectedBattleLevel = 0;
    [SerializeField] int battleLevel;

    [SerializeField] int choosenCampaign;
    static int selectedCampaign;

    private void Awake()
    {
        ExperienceToAdd = this.experience;
        selectedBattleLevel = battleLevel;
        selectedCampaign = choosenCampaign;
    }
#endif

#if UNITY_EDITOR
    [MenuItem("DevTools/Add Level Boost")]
    static void AddLevelBoost()
    {
        DataManager.instance.LevelBoost++;
    }
#endif

#if UNITY_EDITOR
    [MenuItem("DevTools/Increase Hero Level by 1")]
    static void AddLevelToHeroes()
    {
        foreach (var hero in DataManager.instance.Heroes)
        {
            if (!hero.Unlocked) continue;

            hero.AddExperience(hero.ExperienceNeeded);
        }
    }
#endif

#if UNITY_EDITOR
    [MenuItem("DevTools/Add Exp")]
    static void AddExperience()
    {
        foreach (var hero in DataManager.instance.Heroes)
        {
            if (!hero.Unlocked) continue;

            hero.AddExperience(ExperienceToAdd);
        }
    }
#endif

#if UNITY_EDITOR
    [MenuItem("DevTools/Add 100 gold")]

    static void AddGold()
    {
        DataManager.instance.AddGold(100);
    }
#endif

#if UNITY_EDITOR
    [MenuItem("DevTools/Add 100 diamonds")]

    static void AddDiamond()
    {
        DataManager.instance.AddDiamond(100);
    }
#endif

#if UNITY_EDITOR
    [MenuItem("DevTools/Level/Unlock All")]
    static void UnlockAllLevels()
    {
        for (int i = 0; i < DataManager.instance.Campaigns.Count; i++)
        {
            var campaign = DataManager.instance.Campaigns[i];

            for (int y = 0; y < campaign.campaignLevels.Count; y++)
            {
                var level = campaign.campaignLevels[i];
                level.Available = true;
                level.Completed = true;
            }
        }

        for (int i = 0; i < DataManager.instance.TrainingGround.Stages.Length; i++)
        {
            var stages = DataManager.instance.TrainingGround.Stages[i];

            for (int y = 0; y < stages.campaignLevels.Count; y++)
            {
                var level = stages.campaignLevels[i];
                level.Available = true;
                level.Completed = true;
            }
        }
    }
#endif

#if UNITY_EDITOR
    [MenuItem("DevTools/Calculate Experience Up to Choosen BattleLevel")]

    static void CalculateExperience()
    {
        int exp = 0;

        for (int i = 0; i < DataManager.instance.Campaigns[selectedCampaign].campaignLevels.Count; i++)
        {
            var level = DataManager.instance.Campaigns[selectedCampaign].campaignLevels[i];
            exp += level.ExperienceReward.HeroExperience;

            if (i == selectedBattleLevel) break;
        }

        Debug.Log(exp + " gained by completing each level up to level " + selectedBattleLevel + " once");
    }
#endif

#if UNITY_EDITOR
    [MenuItem("DevTools/Screenshots/Take Screenshot")]
    static void Screenshot()
    {
        string fileName = Application.persistentDataPath + "_Screenshot.png";
        ScreenCapture.CaptureScreenshot(fileName);
    }
#endif

#if UNITY_EDITOR
    [MenuItem("DevTools/Save/Save")]
#endif
    public static void SaveGame()
    {
        PlayerCampaignData campaignData = new PlayerCampaignData(DataManager.instance);
        PlayerHeroesData heroesData = new PlayerHeroesData(DataManager.instance);
        PlayerInventoryData inventoryData = new PlayerInventoryData(InventoryManager.instance);
        ChestSaveData chestData = new ChestSaveData(ChestManager.instance);
        PlayerData playerData = new PlayerData(DataManager.instance, EnergyManager.instance);
        TeamData teamData = new TeamData(PlayerTeam.instance.BattleTeam, PlayerTeam.instance.SideTeam, PlayerTeam.instance.RecentlyUsed);

        var save = new SaveData(campaignData, heroesData, playerData, teamData, inventoryData, chestData);
        SaveSystem.SavePlayer(save);

        GPGSCloudSave.AutoSave();
    }

#if UNITY_EDITOR
    [MenuItem("DevTools/Save/Delete Save")]
#endif
    static void DeleteSave()
    {
        SaveSystem.Delete();

        RefreshEditorProjectWindow(); // refreshes files so it doesnt load a delete save file.
    }


#if UNITY_EDITOR
    [MenuItem("DevTools/Add Item/Rare")]
    public static void AddRareItem()
    {
        var items = InventoryManager.GetEquipmentItemDatabase();
        foreach (var item in items)
        {
            if (item.ItemRarity == Item.Rarity.Rare)
            {
                item.Store(1);
                break;
            }
        }
    }
#endif

#if UNITY_EDITOR
    [MenuItem("DevTools/Add Item/Epic")]
    public static void AddEpicItem()
    {
        var items = InventoryManager.GetEquipmentItemDatabase();
        foreach (var item in items)
        {
            if (item.ItemRarity == Item.Rarity.Epic)
            {
                item.Store(1);
                break;
            }
        }
    }
#endif

    static void RefreshEditorProjectWindow()
    {
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
        #endif
    }
}