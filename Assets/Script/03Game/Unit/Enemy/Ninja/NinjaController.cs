using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : RoleController
{
    MonsterInjuredState injuredState;
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        injuredState = new MonsterInjuredState(this);
    }
   
    public override void Injured(InjuredData injuredData)
    {
        base.Injured(injuredData);
        roleAttribute.SetHp((int)injuredData.harm);
        GameObject injuredEffects = ResourceSvc.Single.LoadOrCreate<GameObject>("Prefabs/Effects/InjuredEffects");
        injuredEffects.transform.position = injuredPos.position;   
        injuredState.Enter(injuredData, () =>
        {
            animator.Play("Idle");
        });
    }
}
