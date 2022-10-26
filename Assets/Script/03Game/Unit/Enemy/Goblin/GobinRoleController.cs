using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class GobinRoleController : MonsterController
{

    public GoblinRoleTree goblinRoleTree;
    public override void Init()
    {
        base.Init();
        goblinRoleTree = GetComponent<GoblinRoleTree>();
    }
}
