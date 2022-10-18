using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;
using cfg.Data;

public class RoleAttribute : MonoBehaviour
{
    Attribute baseAttribute;
    [SerializeField]
    WMData.EquipData[] equipDatas;//×°±¸ÊôĞÔ
    public void Init(Attribute roleAttribut, WMData.EquipData[] equipDatas)
    {
        this.equipDatas = equipDatas;
        this.baseAttribute = roleAttribut;
        SaveArchive.bagEquipUpdateCB = (data) => { equipDatas = data; };
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
            float ch = GetCritHarm();//±¬ÉË°Ù·Ö±È
            return baseHarm + (baseHarm * (ch / 100f));
        }
        else
        {
            return baseHarm;
        }
    }

    /// <summary>
    /// ±©»÷ÉËº¦
    /// </summary>
    /// <returns></returns>
    public float GetCritHarm()
    {
        //»ù´¡±©»÷ÉËº¦+ÎäÆ÷ÉËº¦
        return baseAttribute.AttributeCritHarm+GetEquipCritHarm();
    }
    /// <summary>
    /// ±©»÷ÂÊ
    /// </summary>
    /// <returns></returns>
    public float GetCriticalChance()
    {
        //»ù´¡±©»÷ÂÊ+ÎäÆ÷±©»÷ÂÊ
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
