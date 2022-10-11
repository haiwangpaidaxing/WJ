using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : RoleController
{
    NinjaInjuredState injuredState;
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        injuredState = new NinjaInjuredState(this);
    }
    public override void InputEvene()
    {
    }

    public override void Injured(InjuredData injuredData)
    {
        base.Injured(injuredData);
        GameObject injuredEffects = ResourceSvc.Single.LoadOrCreate<GameObject>("Prefabs/Effects/InjuredEffects");
        injuredEffects.transform.position = injuredPos.position;
       // Destroy(injuredEffects, 0.2f);
        injuredState.Enter(injuredData, () =>
        {
            animator.Play("Idle");
        });
    }
}
