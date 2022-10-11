using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroMagicArts : BaseAttack
{
    ZeroController zeroController;
    bool isUSEFlag;
    public ZeroMagicArts(ZeroController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
        this.zeroController = roleController;
        enemyFinder = roleController.GetComponent<EnemyFinder>();
    }
    public override void USE(Action skillOverCB)
    {

        base.USE(skillOverCB);
        roleController.MoveX(0, 0);
        roleController.MoveY(0, 0);
        if (!isUSEFlag && zeroController.zeroMagicArtsWeapon.enemy == null)
        {
            zeroController.zeroMagicArtsWeapon.Use();
            roleController.animator.Play(skillData.animName);
            if (zeroController.zeroMagicArtsWeapon.enemy != null)
            {
                if (zeroController.zeroMagicArtsWeapon.enemy.transform.position.x > roleController.transform.position.x)
                {
                    roleController.SyncImage(1);
                }
                else
                {
                    roleController.SyncImage(-1);
                }

            }
        }
        else if (zeroController.zeroMagicArtsWeapon.enemy != null)
        {
            roleController.animator.Play("MagicArtsMove");
            roleController.RoleRigidbody.gravityScale = 0;
            isUSEFlag = true;
        }
    }
    protected override void PlayAnimator()
    {

    }
    protected override void AnimatorSkillOver()
    {
        //攻击的后摇
        TimerSvc.instance.AddTask(skillData.attackBackswing * 1000, AttackBackswingOver, "----Zero魔法攻击后摇---");//
    }
    public override void AttackBackswingOver()
    {
        skillEndCB?.Invoke();
        enemyFinder.Close();
        roleController.animatorClipCb = null;

        if (zeroController.zeroMagicArtsWeapon.enemy != null && isUSEFlag)
        {
            zeroController.zeroMagicArtsWeapon.Recycle();
            isUSEFlag = !isUSEFlag;
        }

    }

    public override void Init()
    {
        base.Init();
    }

    public override void ISAnimatorOver()
    {

    }

    public override void ISPlayAnimator()
    {
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (isUSEFlag)
        {
            Vector2 dir;
            if (zeroController.zeroMagicArtsWeapon.enemy.transform.position.x > roleController.transform.position.x)
            {
                dir = Vector2.right;
            }
            else
            {
                dir = Vector2.left;
            }
            //dir.y = roleController.transform.position.y;
            if (Math.Abs(roleController.transform.position.x - zeroController.zeroMagicArtsWeapon.enemy.transform.position.x) > 0.5f)
            {
                //roleController.RoleRigidbody.MovePosition(roleController.RoleRigidbody.position + dir * 20 * Time.deltaTime);
                roleController.transform.position = Vector3.MoveTowards(roleController.transform.position,  zeroController.zeroMagicArtsWeapon.enemy.transform.position, Time.deltaTime *20);
            
            }
            else
            {
                roleController.animator.Play("GroundA1");
                ISPlayAnimator("GroundA1");
                ISAnimatorOver("GroundA1");
                roleController.RoleRigidbody.gravityScale = 1;
            }
            roleController.SyncImage(dir.x);
            //Debug.Log(Vector2.Distance(roleController.transform.position, zeroController.zeroMagicArtsWeapon.enemy.transform.position));
        }
        else if (!isUSEFlag)
        {
            ISPlayAnimator(skillData.animName);
            ISAnimatorOver(skillData.animName);
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void Quit()
    {
        base.Quit();
    }

    public override void SkillOver()
    {
        base.SkillOver();
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
     
        base.EnhanceSenseShock();
    }

}
