using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroNormalAttack1 : BaseNormalGroundAttack
{
    ZeroController zeroController;
    public ZeroNormalAttack1(ZeroController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
        this.zeroController = roleController;
    }
    public override void USE(Action cb)
    {
        base.USE(cb);
    }

    protected override void AnimatorClipCB()
    {
        base.AnimatorClipCB();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
    //普通攻击结束流程      进入=》动画结束=》后摇结束=》技能结束
    protected override void AnimatorSkillOver()
    {
        //动画结束
        base.AnimatorSkillOver();
    }

    public override void AttackBackswingOver()
    {
        base.AttackBackswingOver();

    }
}
