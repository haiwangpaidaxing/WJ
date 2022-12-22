using System;
using UnityEngine;
using WUBT;

namespace WMState
{
    public class BaseRoleState : BTAction
    {
        protected Animator animator;
        AnimatorStateInfo stateinfo;
        protected AudioSource audioSource;
        protected string animName;
        protected string audioName;
        protected RoleController roleController;
        protected bool isAnimatorOver;
        public BaseRoleState(string animName, string audioName = "")
        {
            this.animName = animName;
            this.audioName = audioName;
        }
        public override void Init(WUBT.Database database)
        {
            base.Init(database);
            roleController = database.GetComponent<RoleController>();
            animator = database.GetComponentInChildren<Animator>();
        }

        protected override BTResult Execute()
        {
            Debug.Log(database.gameObject.name + "-" + animName + "....");
            stateinfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateinfo.IsName(animName))
            {
                animator.Play(animName);
            }
            ISAnimatorOver();
            return base.Execute();
        }

        protected override void Enter()
        {
            base.Enter();
            if (animName != "")
            {
                if (animator == null)
                {
                    Debug.Log("未添加动画组件");
                }
                animator.Play(animName);
            }
            if (audioName != "")
            {
                if (audioSource == null)
                {
                    Debug.Log("未添音频组件");
                }
                Debug.Log("音效.....");
            }
        }

        protected override void Exit()
        {
            base.Exit();
        }
        public virtual void ISAnimatorOver()
        {
            if (!isAnimatorOver)
            {
                AnimatorStateInfo animatorInfo;
                animatorInfo = roleController.animator.GetCurrentAnimatorStateInfo(0);  //要在update获取
                if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName(animName)))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字
                {
                    isAnimatorOver = true;
                    AnimatorSkillOver();
                }
            }
        }

        protected virtual void AnimatorSkillOver()
        {
        }
    }
}