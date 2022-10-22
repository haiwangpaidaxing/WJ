using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMMonsterState;
using WUBT;
public class RoleRunStaet : BaseRoleState
{
    public RoleRunStaet(string animName, string audioName = "") : base(animName, audioName)
    {
    }
    protected override void Enter()
    {
        base.Enter();
    }
    protected override void Exit()
    {
        base.Exit();
    }
    protected override BTResult Execute()
    {
        roleController.MoveX(database.InputDir.x, database.roleAttribute.GetMoveSpeed());
        base.Execute();
        return BTResult.Running;
    }

}
