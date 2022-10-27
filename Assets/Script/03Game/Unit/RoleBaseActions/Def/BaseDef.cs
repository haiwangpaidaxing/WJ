using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;

public class BaseDef : BaseSkill
{

    public BaseDef(RoleController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {

    }

    public void Injured(InjuredData data)
    {
        Animator[] animators = new Animator[2] { roleController.animator, data.releaser.animator };
        GameRoot.Single.PauseFrame(animators);
        GameRoot.Single.LensBlurEffects(roleController.transform);
        WMEffectsSkill.EffectsSkillData effectsSkillData = new WMEffectsSkill.EffectsSkillData();
        effectsSkillData.value = 3;
        WMEffectsSkill.Diaup diaup = new WMEffectsSkill.Diaup(effectsSkillData);
        data.baseEffectsSkillList = new WMEffectsSkill.BaseEffectsSkill[1] { diaup };
        data.harm = database.roleAttribute.GetHarm()/2;
        data.releaser.GetComponent<IInjured>().Injured(data);
    }

    public override void USE(Action skillOverCB)
    {
        roleController.injuredCB += Injured;
        roleController.MoveX(0, 0);
        base.USE(skillOverCB);
    }
    public override void OnFixedUpdate()
    {

        base.OnFixedUpdate();
    }
    protected override void AnimatorSkillOver()
    {
        roleController.injuredCB -= Injured;
        roleController.MoveX(0, 0);
        skillEndCB.Invoke();
    }
}
public class ZeroDef : BaseDef
{
    ZeroController zeroController;
    public ZeroDef(RoleController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
        zeroController = (ZeroController)roleController;
    }

    public override void USE(Action skillOverCB)
    {
        zeroController.heroDatabase.roleState = RoleState.Def;
        base.USE(skillOverCB);
    }
    protected override void AnimatorSkillOver()
    {
        zeroController.heroDatabase.roleState = RoleState.Null;
        base.AnimatorSkillOver();
    }
}