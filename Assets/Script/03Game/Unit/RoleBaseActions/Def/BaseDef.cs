using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;

public class BaseDef : BaseSkill
{

    public BaseDef(RoleController roleController, Action<RoleController> InjuredCB, ref SkillData skillData) : base(roleController, ref skillData)
    {
        InjuredCB += Injured;

    }

    public void Injured(RoleController releaser)
    {
         Animator[] animators = new Animator[2] { roleController.animator, releaser.animator };
        GameRoot.Single.PauseFrame(animators);
        GameRoot.Single.LensBlurEffects(roleController.transform);
    }

    public override void USE(Action skillOverCB)
    {     
        roleController.MoveX(0, 0);
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
public class ZeroDef : BaseDef
{
    ZeroController zeroController;
    public ZeroDef(RoleController roleController, Action<RoleController> InjuredCB, ref SkillData skillData) : base(roleController, InjuredCB, ref skillData)
    {
        zeroController = (ZeroController)roleController;
    }

    public override void USE(Action skillOverCB)
    {
        zeroController.state = RoleState.Def;
        base.USE(skillOverCB);
    }
    protected override void AnimatorSkillOver()
    {
        zeroController.state = RoleState.Null;
        base.AnimatorSkillOver();
    }
}