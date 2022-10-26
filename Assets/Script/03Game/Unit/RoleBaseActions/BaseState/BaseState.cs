using System;
using UnityEngine;
using WUBT;

namespace WMState
{
    public class BaseRoleState : BTAction
    {
        Animator animator;
        AnimatorStateInfo stateinfo;
        AudioSource audioSource;
        protected string animName;
        protected string audioName;
        protected RoleController roleController;
    
        public BaseRoleState(string animName, string audioName = "")
        {
            this.animName = animName;
            this.audioName = audioName;
        }
        public override void Init(WUBT.Database database)
        {
            base.Init(database);
            roleController = database.roleController;
            animator = database.GetComponent<Animator>();
        }

        protected override BTResult Execute()
        {
            //Debug.Log(database.gameObject.name + "-" + animName + "....");
            stateinfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateinfo.IsName(animName))
            {
                animator.Play(animName);
            }
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
    }
}