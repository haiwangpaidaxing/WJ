using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillManager
{
    public List<MonsterSkillData> monsterSkillDatas;

    public MonsterSkillManager()
    {

    }
    public MonsterSkillManager(List<MonsterSkillData> monsterSkillDatas)
    {
        this.monsterSkillDatas = monsterSkillDatas;
    }
    public bool Find(int id)
    {
        foreach (var item in monsterSkillDatas)
        {
            if (item.id == id)
            {
                return item.ISCD;
            }
        }
        return false;
    }
    public bool USE(int id)
    {
        foreach (var item in monsterSkillDatas)
        {
            if (item.id == id)
            {
                if (item.ISCD)
                {
                    return false;
                }
                else
                {
                    TimerSvc.Single.AddTask(item.cd * 1000, () =>
                    {
                        item.ISCD = false;
                    });
                    item.ISCD = true;
                }
            }
        }
        return false;
    }

}
[System.Serializable]
public class MonsterSkillData
{
    public int id;//技能ID
    public string name;
    [UnityEngine.Header("技能CD")]
    public float cd;//技能CD

    public bool ISCD;
}