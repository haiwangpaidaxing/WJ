using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// »Ã’ﬂøÿ÷∆∆˜
/// </summary>
public class NinjaController : RoleController
{
    /// <summary>
    ///  ‹…À◊¥Ã¨
    /// </summary>
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
