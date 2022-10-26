using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;
public class BaseSkill
{
    public SkillData skillData;
    //public List<SkillData> skillDataList;
    protected RoleController roleController;
    protected Animator animator;
    /// <summary>
    /// 技能结束回调
    /// </summary>
    protected Action skillEndCB;
    protected Database database;
    protected bool isAnimatorOver = false;//
    public BaseSkill(RoleController roleController, ref SkillData skillData)
    {
        database = roleController.GetComponent<Database>();
        this.roleController = roleController;
        this.skillData = skillData;
        this.animator = roleController.animator;
    }
    public BaseSkill()
    {

    }
    public virtual void Init()
    {

    }
    /// <summary>
    /// 
    /// </summary>                                                        
    /// <param name="skillOverCB">技能结束的回调</param>
    public virtual void USE(Action skillOverCB)
    {
        this.skillEndCB = skillOverCB;
        isAnimatorOver = false;//判断动画是否结束
        roleController.animatorClipCb = AnimatorClipCB;
        PlayAnimator();
    }
    public virtual void SkillOver()
    {
        roleController.animatorClipCb -= AnimatorClipCB;
    }
    protected virtual void PlayAnimator()
    {
        animator.Play(skillData.animName)   ;
    }
    /// <summary>
    /// 技能动画结束时调用
    /// </summary>
    protected virtual void AnimatorSkillOver()
    {
        
    }
    /// <summary>
    /// 动画的帧事件
    /// </summary>
    protected virtual void AnimatorClipCB()
    {

    }
    AnimatorStateInfo stateinfo;
    public virtual void OnUpdate()
    {
        ISAnimatorOver();
        ISPlayAnimator();
    }
    /// <summary>
    /// 判断技能动画是否播放▶结束😎
    /// </summary>
    public virtual void ISAnimatorOver()
    {
        if (!isAnimatorOver)
        {
            AnimatorStateInfo animatorInfo;
            animatorInfo = animator.GetCurrentAnimatorStateInfo(0);  //要在update获取
            if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName(skillData.animName)))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字
            {
                isAnimatorOver = true;
                AnimatorSkillOver();
            }
        }
    }

    public virtual void ISAnimatorOver(string animName)
    {
        if (!isAnimatorOver)
        {
            AnimatorStateInfo animatorInfo;
            animatorInfo = animator.GetCurrentAnimatorStateInfo(0);  //要在update获取
            if ((animatorInfo.normalizedTime > 1.0f) && animatorInfo.IsName(animName))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字
            {
                isAnimatorOver = true;
                AnimatorSkillOver();
            }
        }
    }
    /// <summary>
    /// 判断是否播放技能动画
    /// </summary>
    public virtual void ISPlayAnimator()
    {
        stateinfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateinfo.IsName(skillData.animName))
        {
            animator.Play(skillData.animName);
        }
    }

    /// <summary>
    /// 判断是否播放技能动画
    /// </summary>
    public virtual void ISPlayAnimator(string animName)
    {
        stateinfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateinfo.IsName(animName))
        {
            animator.Play(animName);
        }
    }

    public virtual void OnFixedUpdate()
    {

    }
    /// <summary>
    /// 技能后摇结束
    /// </summary>
    public virtual void AttackBackswingOver() { }


    public virtual void Quit()
    {

    }
}

///把技能效果为可改变类似于，普通攻击对自身会有一小段的向前位置，对敌人会造成击退效果，通过设置可以修改技能对自身的效果与敌人效果
/// 普工1001  重击1002  闪避1003
///技能1TODO  技能2TODO   技能3TODO