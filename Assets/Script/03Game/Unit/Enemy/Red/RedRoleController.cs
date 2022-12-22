using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRoleController : MonsterController
{
    public RedRoleTree roleTree;

    public override void Init()
    {
        base.Init();
        roleTree = GetComponent<RedRoleTree>();
        roleTree.Init();
    }

    public override void Injured(InjuredData injuredData)
    {
        base.Injured(injuredData);
    }
}
