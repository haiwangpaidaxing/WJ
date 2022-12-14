using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;
using cfg.Data;


public class RoleAttribute : MonoBehaviour
{
    Attribute baseAttribute;
    [SerializeField]
    WMData.EquipData[] equipDatas;//装备属性
    public void Init(Attribute roleAttribut, WMData.EquipData[] equipDatas)
    {
        this.equipDatas = equipDatas;
        this.baseAttribute = roleAttribut;
        SaveArchive.bagEquipUpdateCB = (data) => { equipDatas = data; };
    }

    public void Init(Attribute roleAttribut)
    {
        this.baseAttribute = roleAttribut;  
    }

    public float GetEquipHarm()
    {
        float harm = 0;
        for (int i = 0; i < equipDatas.Length; i++)
        {
            harm += equipDatas[i].Attribute_Harm;
        }
        return harm;
    }
    public float GetEquipCritHarm()
    {
        float critHarm = 0;
        for (int i = 0; i < equipDatas.Length; i++)
        {
            critHarm += equipDatas[i].Attribute_CritHarm;
        }
        return critHarm;
    }
    public float GetEquipCriticalChance()
    {
        float value = 0;
        for (int i = 0; i < equipDatas.Length; i++)
        {
            value += equipDatas[i].Attribute_CriticalChance;
        }
        return value;
    }
    public float GetEquipHp()
    {
        float value = 0;
        for (int i = 0; i < equipDatas.Length; i++)
        {
            value += equipDatas[i].Attribute_HP;
        }
        return value;
    }
    public float GetHP()
    {
        return baseAttribute.AttributeHP+GetEquipHp();
    }
    public float GetMP()
    {
        return baseAttribute.AttributeMP;
    }
    public float GetHarm()
    {
        float cuttrntCritHarm = GetCriticalChance();
        float baseHarm = baseAttribute.AttributeHarm + GetEquipHarm();
        if (cuttrntCritHarm >= GetRandom(0f, 101f))
        {
            float ch = GetCritHarm();//爆伤百分比
            return baseHarm + (baseHarm * (ch / 100f));
        }
        else
        {
            return baseHarm;
        }
    }

    /// <summary>
    /// 暴击伤害
    /// </summary>
    /// <returns></returns>
    public float GetCritHarm()
    {
        //基础暴击伤害+武器伤害
        return baseAttribute.AttributeCritHarm+GetEquipCritHarm();
    }
    /// <summary>
    /// 暴击率
    /// </summary>
    /// <returns></returns>
    public float GetCriticalChance()
    {
        //基础暴击率+武器暴击率
        return baseAttribute.AttributeCriticalChance+GetEquipCriticalChance();
    }


  

    public float GetJumpHeight()
    {
        return baseAttribute.AttributeJumpHeight;
    }
    public float GetMoveSpeed()
    {
        return baseAttribute.AttributeMoveSpeed;
    }
    private float GetRandom(float min, float max)
    {
        float r = Random.Range(min, max);
        return r;
    }
}
