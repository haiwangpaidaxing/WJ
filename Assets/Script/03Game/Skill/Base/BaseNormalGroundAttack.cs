using System;
using UnityEngine;

public class BaseNormalGroundAttack : BaseAttack
{
    public BaseNormalGroundAttack(RoleController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
    }
    /// <summary>
    /// 使用技能时候调用且只调用一次
    /// </summary>
    /// <param name="cb">技能结束时候的回调</param>
    public override void USE(Action cb)
    {
        base.USE(cb);
      
        roleController.SyncImage(InputController.Single.InputDir.x);
        roleController.MoveX(0, 0);
        roleController.MoveY(0, 0);//防止在移动中使用时 会因为力而进行滑动
                                   //第一种情况 如果 输入方向=人物方向  角色会有向前一小段的位置
                                   //第二种情况 输入方向！=人物方向 ||输入方向==0  角色站立不动
        if (InputController.Single.InputDir.x != 0)
        {
            Vector2 dir = Vector2.zero;
            dir.x = InputController.Single.InputDir.x;
            Debug.DrawRay(roleController.transform.position,dir*2,Color.red);
            if (RayCheck.Check(roleController.transform.position, dir, 2))
            {
                return;
            }
         //   roleController.MoveX(dir.x, 10, ForceMode2D.Impulse);//滑动不美观
          //  roleController.RoleRigidbody.position += dir * 1f;//瞬间移动会穿越强 可使用射线检测前方是否有墙壁 TODO
        }
       
    }

    protected override void AnimatorClipCB()
    {
        base.AnimatorClipCB();
        //  Debug.Log("TODO制造伤害");
        //TODO制造伤害
        enemyFinder.enemyCB = Damage;
        //TODO制造伤害
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
        base.OnFixedUpdate();
    }
    //普通攻击结束流程      进入=》动画结束=》后摇结束=》技能结束
    protected override void AnimatorSkillOver()
    {
        //动画结束
        base.AnimatorSkillOver();
        //攻击的后摇
        TimerSvc.instance.AddTask(skillData.attackBackswing * 1000, AttackBackswingOver, "----普通攻击后摇---");//
    }
    public override void AttackBackswingOver()
    {
        skillEndCB?.Invoke();
        skillData.comboCurrentValidTime = skillData.comboValidTime;
        //下一个技能的有效时间
        roleController.animatorClipCb = null;
        enemyFinder.Close();
        //技能的后摇结束
    }
}
