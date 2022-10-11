using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.CodeDom.Compiler;
using System;

[System.Serializable]
public struct ArchiveData
{
    /// <summary>
    /// 存档ID
    /// </summary>
    public int archiveID;
    public string saveDataTime;
    public PlayerData playerData;
    public WMData.EquipData[] equipSoltDatas;
    public List<WMData.EquipData> bagEquipData;//equipBagData
}

[System.Serializable]
public class ArchiveDataConfig
{
    public List<ArchiveData> dataList = new List<ArchiveData>();
}
