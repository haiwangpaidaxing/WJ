using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMEffectsSkill;
using WUBT;

public class RedMonsterDatabase : MonsterDatabase
{
    [Header("远程攻击距离")]
    public float RangeAttackDis;
    [Header("箭创建位置")]
    public Transform createArrowPos;
    [Header("当前箭的数量")]
    public int currentArrow;
    [Header("箭的最大数量")]
    public int arrowMax;

    public RedRoleStateCheck.AttackState attackState;
    public InjuredData injuredData = new InjuredData();
    public EffectsSkillData efData = new EffectsSkillData();
    public Diaup diaup;
    public Repel repel;
 
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Vector2 tr = transform.position;
        Gizmos.DrawRay(transform.position, transform.right * RangeAttackDis);
    }
    public override void Init()
    {
        base.Init();
        diaup = new Diaup(efData);
        repel = new Repel(efData);
        tackingRangeTarget = GameObject.FindGameObjectWithTag("Hero").transform;
    }
}
