using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

namespace WMState
{

    public class RoleInjuredState : BaseRoleState
    {
        //½ûÖ¹ÈÎºÎ²Ù×÷
        public bool isInjured;
        HeroDatabase heroDatabase;
        public override void Init(Database database)
        {
            base.Init(database);
            heroDatabase = (HeroDatabase)database;
        }
        public RoleInjuredState(string animName, string audioName = "") : base(animName, audioName)
        {
            ;
        }
        protected override void Enter()
        {
            base.Enter();
            EnterInjured();
            database.updateInjuredCB = EnterInjured;
            roleController.MoveX(0, 0);
            roleController.MoveY(0, 0);
        }

        private void EnterInjured()
        {
            isInjured = false;
            roleController.injuredState.Enter(database.injuredData, () =>
           {
               isInjured = true;
           });
        }

        protected override BTResult Execute()
        {
            bool isGround = BoxCheck.Check(database.GroundCheckPos, database.GroundSize, database.GroundMask);
            if (isAnimatorOver)
            {
                if (isInjured)
                {
                    if (isGround)
                    {
                        heroDatabase.roleState = RoleState.Null;
                    }
                }
            }
            return base.Execute();
        }
        public override void Clear()
        {
            base.Clear();
            database.injuredData = null;
            database.updateInjuredCB = Enter;
        }
    }

}