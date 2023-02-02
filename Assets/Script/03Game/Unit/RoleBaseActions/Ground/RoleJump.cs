using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;
using WMBT;

public class RoleGroundJump : BaseRoleState
{
    public RoleGroundJump(string animName, string audioName = "") : base(animName, audioName)
    {
    }

    protected override void Enter()
    {
        base.Enter();
        database.roleController.MoveY(1, database.roleAttribute.GetJumpHeight());
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
