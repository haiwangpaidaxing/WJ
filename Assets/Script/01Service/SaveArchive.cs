using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class TestData
{
    public int id;
}
public static class SaveArchive
{
    public static Action<WMData.EquipData[]> bagEquipUpdateCB;
    public static int NewPlayerDataArchive(ArchiveData archiveData)
    {
        archiveData.archiveID = GetArchiveID();
        ResourceSvc.Single.ArchiveDataConfig.dataList.Add(archiveData);
        archiveData.bagEquipData = new List<WMData.EquipData>();
        archiveData.equipSoltDatas = new WMData.EquipData[5];
        archiveData.playerData.skillDatas = ResourceSvc.Single.Load<SkillDataConfig>(DataPath.BaseSkillData).skillDatas;
        SavePlayDataArchive(archiveData);
        for (int i = 0; i < archiveData.playerData.skillDatas.Count; i++)
        {
            archiveData.playerData.skillDatas[i].baseEffectsSkills = new WMEffectsSkill.BaseEffectsSkill[archiveData.playerData.skillDatas[i].skillEffectsNuumber];
            for (int a = 0; a < archiveData.playerData.skillDatas[i].baseEffectsSkills.Length; a++)
            {
                archiveData.playerData.skillDatas[i].baseEffectsSkills[a] = new WMEffectsSkill.BaseEffectsSkill();
                archiveData.playerData.skillDatas[i].baseEffectsSkills[a].skillData = new WMEffectsSkill.EffectsSkillData("");
            }
        }
        return archiveData.archiveID;
    }
    static int aID;
    static int GetArchiveID()
    {
        aID++;
        for (int i = 0; i < ResourceSvc.Single.ArchiveDataConfig.dataList.Count; i++)
        {
            if (ResourceSvc.Single.ArchiveDataConfig.dataList[i].archiveID == aID)
            {
                return GetArchiveID();
            }
        }
        return aID;
    }
    public static void SavePlayDataArchive(ArchiveData newData)
    {
        ///保存玩家数据 如： 金币 等级 选择英雄ID 等
        newData.saveDataTime = DateTime.Now.ToString();
        ResourceSvc.Single.SetArchiveByID(newData);
        // ResourceSvc.Single.ArchiveDataConfig.dataList.Remove(archiveData);
        //  ResourceSvc.Single.ArchiveDataConfig.dataList.Add(newData);
        string pathJson = JsonUtility.ToJson(ResourceSvc.Single.ArchiveDataConfig, true);
        using (StreamWriter streamWriter = File.CreateText(ResPath.SaveFilePath + "ArchiveDataConfig.json"))
        {
            streamWriter.Write(pathJson);
            streamWriter.Close();
        }
    }

    public static void BagEquipAdd(WMData.EquipData equipData)
    {
        ArchiveData archiveData = ResourceSvc.Single.CurrentArchiveData;
        equipData.bagIndex = archiveData.bagEquipData.Count;
        archiveData.bagEquipData.Add(equipData);
        SaveArchive.SavePlayDataArchive(archiveData);
    }

    public static void BagEquipSet(int index,WMData.EquipData equipData)
    {
        ArchiveData archiveData = ResourceSvc.Single.CurrentArchiveData;
        archiveData.bagEquipData[index] = equipData;
        SaveArchive.SavePlayDataArchive(archiveData);
    }

    public static void BagEquipRemove(WMData.EquipData equipData)
    {
        ArchiveData archiveData = ResourceSvc.Single.CurrentArchiveData;
        archiveData.bagEquipData.Remove(equipData);
        SaveArchive.SavePlayDataArchive(archiveData);
    }

    /// <summary>
    /// 装备栏
    /// </summary>
    /// <param name="equipData"></param>
    /// <param name="index"></param>
    public static void EquipSoltAdd(WMData.EquipData equipData, int index)
    {
        ArchiveData archiveData = ResourceSvc.Single.CurrentArchiveData;
        archiveData.equipSoltDatas[index] = equipData;
        SaveArchive.SavePlayDataArchive(archiveData);
        bagEquipUpdateCB?.Invoke(archiveData.equipSoltDatas);
    }
    /// <summary>
    /// 装备栏
    /// </summary>
    /// <param name="equipData"></param>
    /// <param name="index"></param>
    public static void BagEquipSoltRemove(WMData.EquipData equipData, int index)
    {
        ArchiveData archiveData = ResourceSvc.Single.CurrentArchiveData;
        archiveData.equipSoltDatas[index] = new WMData.EquipData();
        SaveArchive.SavePlayDataArchive(archiveData);
        bagEquipUpdateCB?.Invoke(archiveData.equipSoltDatas);
    }

}
