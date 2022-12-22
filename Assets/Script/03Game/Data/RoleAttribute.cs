using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;
using cfg.Data;


public class RoleAttribute : MonoBehaviour
{
    public System.Action<int> hpValueCB;
    public System.Action<int> mpValueCB;
    Attribute baseAttribute;
    [SerializeField]
    WMData.EquipData[] equipDatas;//װ������
    public int BaseHp;
    public int BaseMp;

    public int MaxHP
    {
        get
        {
            return (baseAttribute.AttributeHP + GetEquipHp());
        }
    }

    public int MaxMP
    {
        get
        {
            return (baseAttribute.AttributeMP + GetEquipMP());
        }
    }

    public void Init(Attribute roleAttribut, WMData.EquipData[] equipDatas)
    {
        this.equipDatas = equipDatas;
        this.baseAttribute = roleAttribut;
        BaseHp = baseAttribute.AttributeHP;
        BaseMp = baseAttribute.AttributeMP;
        SaveArchive.bagEquipUpdateCB = (data) => { equipDatas = data; };
    }

    public void Init(Attribute roleAttribut)
    {
        this.baseAttribute = roleAttribut;
        BaseHp = baseAttribute.AttributeHP;
        BaseMp = baseAttribute.AttributeMP;
    }
    public bool ISEquip()
    {
        if (equipDatas == null)
        {
            return true;
        }

        return false;
    }
    public int GetEquipHarm()
    {
        if (ISEquip())
        {
            return 0;
        }
        float harm = 0;
        for (int i = 0; i < equipDatas.Length; i++)
        {
            harm += equipDatas[i].Attribute_Harm;
        }
        return (int)harm;
    }
    public int GetEquipCritHarm()
    {
        if (ISEquip())
        {
            return 0;
        }
        float critHarm = 0;
        for (int i = 0; i < equipDatas.Length; i++)
        {
            critHarm += equipDatas[i].Attribute_CritHarm;
        }
        return (int)critHarm;
    }
    public int GetEquipCriticalChance()
     {
        if (ISEquip())
        {
            return 0;
        }
        float value = 0;
        for (int i = 0; i < equipDatas.Length; i++)
        {
            value += equipDatas[i].Attribute_CriticalChance;
        }
        return (int)value;
    }
    /// <summary>
    /// װ���ӳɵ�HP
    /// </summary>
    /// <returns></returns>
    public int GetEquipHp()
    {
        if (ISEquip())
        {
            return 0;
        }
        float value = 0;
        for (int i = 0; i < equipDatas.Length; i++)
        {
            value += equipDatas[i].Attribute_HP;
        }
        return (int)value;
    }

    public int GetEquipMP()
    {
        if (ISEquip())
        {
            return 0;
        }
        float value = 0;
        return (int)value;
    }

    public int GetHP()
    {
        return BaseHp + GetEquipHp();
    }
    public int GetMP()
    {
        return (int)BaseMp + GetEquipHp();
    }
    public int GetHarm()
    {
        float cuttrntCritHarm = GetCriticalChance();
        float baseHarm = baseAttribute.AttributeHarm + GetEquipHarm();
        if (cuttrntCritHarm >= GetRandom(0f, 101f))
        {
            float ch = GetCritHarm();//���˰ٷֱ�
            return (int)(baseHarm + (baseHarm * (ch / 100f)));
        }
        else
        {
            return (int)baseHarm;
        }
    }

    /// <summary>
    /// �����˺�
    /// </summary>
    /// <returns></returns>
    public float GetCritHarm()
    {
        //���������˺�+�����˺�
        return baseAttribute.AttributeCritHarm + GetEquipCritHarm();
    }
    /// <summary>
    /// ������
    /// </summary>
    /// <returns></returns>
    public float GetCriticalChance()
    {
        //����������+����������
        return baseAttribute.AttributeCriticalChance + GetEquipCriticalChance();
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

    public void SetHp(int value)
    {
        BaseHp -= value;
        hpValueCB?.Invoke(GetHP());
    }
    public void SetMP(int value)
    {
        BaseMp -= value;
        mpValueCB?.Invoke(GetMP());
    }
    public void SetMPAdd(int value)
    {
        BaseMp += value;
        mpValueCB?.Invoke(GetMP());
    }
    float time;
    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (GetMP() <= MaxMP)
        {
            if (time >= 1)//ÿ��ظ�10��
            {
                time = 0;
                SetMPAdd(10);
            }
        }
    }
}
