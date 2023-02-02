using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;

public class GobinRoleController : MonsterController
{

    public GoblinRoleTree goblinRoleTree;
    public override void Init()
    {
        base.Init();
        goblinRoleTree = GetComponent<GoblinRoleTree>();
    }

    public override void Injured(InjuredData injuredData)
    {
        base.Injured(injuredData);
    }
}
