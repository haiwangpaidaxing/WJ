using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroNormalAttack2 : BaseNormalGroundAttack
{
    ZeroController zeroController;
    public ZeroNormalAttack2(ZeroController roleController, ref SkillData skillData) : base(roleController, ref skillData)
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
    //��ͨ������������ ����=����������=����ҡ����=�����ܽ���
    protected override void AnimatorSkillOver()
    {
        base.AnimatorSkillOver();
    }

    protected override void Damage(IInjured enemy)
    {
        base.Damage(enemy);
        CameraShake();
    }

    public override void AttackBackswingOver()
    {
        base.AttackBackswingOver();

    }
}
