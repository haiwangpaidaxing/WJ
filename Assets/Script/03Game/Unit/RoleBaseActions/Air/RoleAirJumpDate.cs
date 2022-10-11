using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class RoleAirJumpState : RoleIdleState
{
    public RoleAirJumpState(string animName, string audioName = "") : base(animName, audioName)
    {
    }
    protected override void Enter()
    {
        base.Enter();
    }

    protected override BTResult Execute()
    {
        database.roleController.MoveX(database.InputDir.x, database.roleAttribute.GetMoveSpeed());
        return base.Execute();
    }
    protected override void Exit()
    {
        base.Exit();
    }
}
