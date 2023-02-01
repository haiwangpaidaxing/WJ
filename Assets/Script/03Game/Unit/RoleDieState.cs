using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;

public class RoleDieState : BaseRoleState
{
    public RoleDieState(string animName, string audioName = "") : base(animName, audioName)
    {
    }

    protected override void Enter()
    {
        base.Enter();
        roleController.MoveX(0, 0);
    }
}
