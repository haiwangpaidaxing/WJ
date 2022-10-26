using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;
using WUBT;

/// <summary>
/// 角色下落
/// </summary>
public class RoleFall : BaseRoleState
{
    public RoleFall(string animName, string audioName = "") : base(animName, audioName)
    {
    }

    protected override BTResult Execute()
    {
        database.roleController.MoveX(database.InputDir.x, database.roleAttribute.GetMoveSpeed());
        database.roleController.Fall();
        return base.Execute();
    }
}
