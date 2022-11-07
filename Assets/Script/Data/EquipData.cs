using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg.Data;
namespace WMData
{
    public enum EntryKey
    {
        Harm,
        Hp,
        CritHarm,
        CriticalChance,
        Count
    }
    [System.Serializable]
    public struct EquipData
    {
        public int Id;
        public bool ISOpen;
        public string EquipName;
        public string ResName;
        public string Describe;
        public int EntryNumber;
        public int PriceWeapons;
        public EquipType EquipType;
        public EquipQualityType EquipQualityType;

        // public Dictionary<int, float> entryNumberDic;
        public float Attribute_Harm;
        public float Attribute_HP;
        public float Attribute_CritHarm;
        public float Attribute_CriticalChance;

        public List<EntryKey> entryKey;

        public int bagIndex;
        public void OpenEquip()
        {
            //根据品质判断概率

            // entryNumberDic = new Dictionary<int, float>();
            int prob = 0;
            switch (EquipQualityType)
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
            EntryNumber = RandomEntryGet(prob);//根据品质的概率
            UnlockEntry(entryKey);
            RandomEntryPromote();
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

        public void UnlockEntry(List<EntryKey> entryKey)//随机解锁词条
        {
            while (true)
            {
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
        public void RandomEntryPromote()
        {
            //随机到的词条
            int r = Random.Range(0, entryKey.Count);
            switch (entryKey[r])
            {
                //TODO 测试
                case EntryKey.Harm:
                    Attribute_Harm += Random.Range(10, 10);
                    break;
                case EntryKey.Hp:
                    Attribute_HP += Random.Range(10, 100);
                    break;
                case EntryKey.CritHarm:
                    Attribute_CritHarm += Random.Range(10, 10);
                    break;
                case EntryKey.CriticalChance:
                    Attribute_CriticalChance += Random.Range(10, 10);
                    break;
            }
        }
    }

}
