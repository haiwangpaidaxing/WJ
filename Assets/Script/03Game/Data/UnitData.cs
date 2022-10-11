using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitData
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public int unitID;
    /// <summary>
    /// 角色名称
    /// </summary>
    public string unitName;
    /// <summary>
    /// 角色基础生命值
    /// </summary>
    public int hp;
    /// <summary>
    /// 角色基础MP
    /// </summary>
    public int mp;
    /// <summary>
    /// 角色基础伤害
    /// </summary>
    public float critHarm;
    /// <summary>
    /// 角色基础暴击率
    /// </summary>
    public float criticalChance;
}
