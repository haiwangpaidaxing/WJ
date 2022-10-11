using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAirNormalAttack : BaseAttack
{
    public BaseAirNormalAttack(RoleController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
    }
    public override void USE(Action cb)
    {
        base.USE(cb);
        roleController.SyncImage(InputController.Single.InputDir.x);
        roleController.MoveX(0, 0);//防止在移动中使用时 会因为力而进行滑动
        roleController.MoveY(0, 0);//防止在移动中使用时 会因为力而进行滑动
                                   //第一种情况 如果 输入方向=人物方向  角色会有向前一小段的位置
                                   //第二种情况 输入方向！=人物方向 ||输入方向==0  角色站立不动
        if (InputController.Single.InputDir.x != 0)
        {
            //Vector3 dir = Vector3.zero;
            //dir.x = InputController.Single.InputDir.x;
            //roleController.transform.position += dir * 0.5f;
        }
        SetGravity(0);//设置重力 滞空
        Debug.Log("空中使用普通攻击");
    }

    public virtual void SetGravity(float scale)
    {
        roleController.RoleRigidbody.gravityScale = scale;
    }


    protected override void AnimatorClipCB()
    {
        //TODO制造伤害
        base.AnimatorClipCB();
        enemyFinder.enemyCB = Damage;
        enemyFinder.OpenFindTargetAll();
    }
    protected override void Damage(IInjured enemy)
    {
        base.Damage(enemy);
        InjuredData injuredData = new InjuredData();
        injuredData.baseEffectsSkillList = skillData.baseEffectsSkills;
        injuredData.harm = roleController.roleAttribute.GetHarm();
        injuredData.releaser = roleController;
        enemy.Injured(injuredData);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
      //  roleController.MoveY(0, 0);
        base.OnFixedUpdate();
    }
    //普通攻击结束流程      进入=》动画结束=》后摇结束=》技能结束
    protected override void AnimatorSkillOver()
    {
        //动画结束
        base.AnimatorSkillOver();
        enemyFinder.Close();
        //攻击的后摇
        TimerSvc.instance.AddTask(skillData.attackBackswing * 1000, AttackBackswingOver, "----普通攻击后摇---");//
    }
    public override void AttackBackswingOver()
    {
        skillEndCB?.Invoke();
        skillData.comboCurrentValidTime = skillData.comboValidTime;
        roleController.animatorClipCb = null;
        //下一个技能的有效时间
        SetGravity(1);
        //技能的后摇结束
    }

}
