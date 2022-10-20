using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class RoleWallSlider : BaseRoleState
{

    public RoleWallSlider(string animName, string audioName = "") : base(animName, audioName)
    {
    }

    public int enterDir;
    protected override void Enter()
    {
        enterDir = (int)roleController.roleDir;
        base.Enter();
    }

    protected override BTResult Execute()
    {
        if (database.InputDir.x != 0 && enterDir != database.InputDir.x)
        {
            roleController.MoveX(database.InputDir.x, 4);
            roleController.MoveY(1, 7);

        }
        else
        {
            roleController.MoveY(1, (-1 + -Time.deltaTime));
        }
        return base.Execute();
    }
    protected override void Exit()
    {
        base.Exit();
    }
}
