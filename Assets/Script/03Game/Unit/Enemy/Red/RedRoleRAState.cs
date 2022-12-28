using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMEffectsSkill;
using WMState;
using WUBT;

public class RedRoleRAState : MonsterAttackState
{
    RedMonsterDatabase redData;
    public RedRoleRAState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
    {
    }
    public override void Init(Database database)
    {
        base.Init(database);
        redData = this.database as RedMonsterDatabase;

    }
    protected override void Enter()
    {
        base.Enter();   
        isEnd = false;
        redData.attackState = RedRoleStateCheck.AttackState.RA;
        roleController.animatorClipCb += CreateArreow;
      

    }
    private void CreateArreow()
    {
        //创建弓箭
        GameObject arrowGameObject = ResourceSvc.Single.LoadOrCreate<GameObject>(WeaponPath.RedArrow);
        RedArrow redArrow = arrowGameObject.GetComponent<RedArrow>();
        arrowGameObject.transform.position = redData.createArrowPos.position;
        redArrow.targetPos = redData.tackingRangeTarget;
        redData.diaup.skillData.value = 2;
        redData.repel.skillData.value = 2;
        redData.injuredData.baseEffectsSkillList = new BaseEffectsSkill[2] { redData.diaup, redData.repel };
        redData.injuredData.harm = database.roleAttribute.GetHarm();
        redData.injuredData.releaser = database.roleController;
        redArrow.InjuredData = redData.injuredData;
        redData.currentArrow--;
        
        GameObject.Destroy(redArrow, 5);
    }
    
    bool isEnd;
    protected override BTResult Execute()
    {
        ISAnimatorOver();
        if (isEnd)
        { 
            redData.attackState = RedRoleStateCheck.AttackState.Null;
            return BTResult.Ended;
        }
        else
        {
            return BTResult.Running;
        }
      
    }
    public override void ISAnimatorOver()
    {
        if (!isAnimatorOver)
        {
            AnimatorStateInfo animatorInfo;
            animatorInfo = roleController.animator.GetCurrentAnimatorStateInfo(0);  //要在update获取
            if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName(animName)||animatorInfo.IsName("Idle")))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字
            {
                isAnimatorOver = true;
                AnimatorSkillOver();
            }
        }
    }
    protected override void AnimatorSkillOver()
    {
      //  Debug.Log(isEnd);
        TimerSvc.instance.AddTask(2 * 1000, () => { 
            isEnd = true;
        });
    }

    public override void Clear()
    {
        base.Clear();
        redData.attackState = RedRoleStateCheck.AttackState.Null;
        roleController.animatorClipCb -= CreateArreow;
    }
}
