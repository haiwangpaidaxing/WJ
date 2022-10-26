using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroRoll : BaseSkill
{
    public ZeroRoll(RoleController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
    }

    public override void USE(Action skillOverCB)
    {
        roleController.MoveX(0, 0);//防止在移动中使用时 会因为力而进行滑动
        roleController.SyncImage(InputController.Single.InputDir.x);
        roleController.RoleRigidbody.mass = 0.5f;
        roleController.MoveX(roleController.roleDir, 3,ForceMode2D.Impulse);
        base.USE(skillOverCB);
      
    }
    public override void OnFixedUpdate()
    {

        base.OnFixedUpdate();
    }
    protected override void AnimatorSkillOver()
    {
        roleController.MoveX(0, 0);
        skillEndCB.Invoke();
    }
}
