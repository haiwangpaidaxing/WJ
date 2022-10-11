using cfg.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WMData;

public class OpenWeaponPanel : BasePanelCancelDrag, IDropHandler
{
    public EquipInfoPanel equipInfo;
    public void OnDrop(PointerEventData eventData)
    {
        IEquipData equip = eventData.pointerDrag.GetComponent<IEquipData>();
        if (equip == null)
        {
            return;
        }
        WMData.EquipData equipData = equip.EquipData;
        equipData.ISOpen = true;
        OpenWeapon(ref equipData);
        equip.EquipData = equipData;
        SaveArchive.BagEquipSet(equipData.bagIndex, equip.EquipData);
        equipInfo.Show(equipData);
        equipInfo.gameObject.SetActive(true);
        MainSceneSys.Single.UpdateOpenWeaponBag();
        equip.CB();
    }

    public void OpenWeapon(ref WMData.EquipData equipData)
    {
        //根据品质判断概率
        int prob = 0;
        switch (equipData.EquipQualityType)
        {
            case EquipQualityType.Rare:
                prob = 6;
                break;
            case EquipQualityType.Superio:
                prob = 5;
                break;
            case EquipQualityType.Ordinary:
                prob = 3;
                break;
            case EquipQualityType.Rough:
                prob = 2;
                break;
        }
        equipData.EntryNumber = RandomEntryGet(prob);//根据品质的概率,获取词条数量
        if (equipData.EntryNumber > 4)
        {
            equipData.EntryNumber = 4;
        }
        UnlockEntry(ref equipData.entryKey, ref equipData.EntryNumber);
        RandomEntryPromote(ref equipData);
    }
    /// <summary>
    /// 词条数量
    /// </summary>
    /// <param name="prob"></param>
    /// <returns></returns>
    public int RandomEntryGet(int prob)
    {
        int number = 1;
        int r = Random.Range(0, 10);
        if (prob >= r)//如果概率百分百会出现死循环
        {
            return number + RandomEntryGet(prob);
        }
        if (number > 4)
        {
            number = 4;
        }
        return number;
    }

    public void UnlockEntry(ref List<EntryKey> entryKey, ref int EntryNumber)//随机解锁词条
    {
        while (true)
        {
            if (entryKey.Count==4)
            {
                return;
            }
            //随机解锁第几个词条
            int r = Random.Range(0, (int)EntryKey.Count);
            EntryKey ek = (EntryKey)r;
            if (!entryKey.Contains(ek))
            {
                entryKey.Add(ek);
                if (entryKey.Count == EntryNumber)
                {
                    return;
                }
            }
        }
    }
    //随机提升词条
    public WMData.EquipData RandomEntryPromote(ref WMData.EquipData equipData)
    {
        //随机到的词条
        int r = Random.Range(0, equipData.entryKey.Count);
        switch (equipData.entryKey[r])
        {
          
            case EntryKey.Harm:
                equipData.Attribute_Harm += Random.Range(10, 100); 
                break;
            case EntryKey.Hp:
                equipData.Attribute_HP += Random.Range(50, 200);
                break;
            case EntryKey.CritHarm:
                equipData.Attribute_CritHarm += Random.Range(1, 20);
                break;
            case EntryKey.CriticalChance:
                equipData.Attribute_CriticalChance += Random.Range(1, 20); ;
                break;
        }
        return equipData;
    }
}
