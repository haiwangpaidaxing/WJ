using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;
public class RoleIdleState : BaseRoleState
{
    public RoleIdleState(string animName, string audioName = "") : base(animName, audioName)
    {
    }

    protected override void Enter()
    {
        database.roleController.MoveX(0,0);
        base.Enter();
    }
    protected override BTResult Execute()
    {
        base.Execute();
       
        return BTResult.Running;
    }
    protected override void Exit()
    {
        base.Exit();
    }
}
