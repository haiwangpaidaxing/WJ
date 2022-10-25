using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class GobinRoleController : RoleController
{
   
    public GoblinRoleTree goblinRoleTree;
    MonsterDatabase mData;
    public override void Init()
    {
        base.Init();
        mData = GetComponent<MonsterDatabase>();
        goblinRoleTree = GetComponent<GoblinRoleTree>();
    }
    public override void Injured(InjuredData injuredData)
    {
        base.Injured(injuredData);
        mData.SetInjuredData(injuredData);
        roleAttribute.SetHp((int)injuredData.harm);
        GameObject injuredEffects = ResourceSvc.Single.LoadOrCreate<GameObject>("Prefabs/Effects/InjuredEffects");
        injuredEffects.transform.position = injuredPos.position;
       
    }
}
