using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;
using WMEffectsSkill;

public class MonkDatabase : MonsterDatabase
{
    public InjuredData injuredData = new InjuredData();
    public EffectsSkillData efData = new EffectsSkillData();
    public Diaup diaup;
    public Repel repel;
    public RedRoleStateCheck.AttackState attackState;
    public override void Init()
    {
        base.Init();
        diaup = new Diaup(efData);
        repel = new Repel(efData);  
        tackingRangeTarget = GameObject.FindGameObjectWithTag("Hero").transform;
    }

   
}