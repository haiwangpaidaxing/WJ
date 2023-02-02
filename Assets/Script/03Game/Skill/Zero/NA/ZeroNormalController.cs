using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;

public class ZeroNormalController : BaseSkill
{
    BaseComboController airNAController;
    BaseComboController groundController;
    bool isGround;
    public ZeroNormalController(RoleController roleController, SkillData skillData, BaseComboController air, BaseComboController ground):base(roleController,ref skillData)
    {
        //this.roleController = roleController;
        //database = roleController.GetComponent<Database>();
        //this.skillData = skillData;
        this.airNAController = air;
        this.groundController = ground;
    }

    public override void USE(Action skillOverCB)
    {
        if (this.skillEndCB == null)
        {
            this.skillEndCB = skillOverCB;
            isGround = BoxCheck.Check(database.GroundCheckPos, database.GroundSize, database.GroundMask);
            if (isGround)
            {
                groundController.USE(CurrentSkillOverCB);
            }
            else
            {
                airNAController.USE(CurrentSkillOverCB);
            }
        }   
    }
    protected void CurrentSkillOverCB()
    {
        skillEndCB?.Invoke();
        skillEndCB = null;
    }

    public override void OnFixedUpdate()
    {
        if (isGround)
        {
            groundController.OnFixedUpdate();
        }
        else
        {
            airNAController?.OnFixedUpdate();
        }
    }

    public override void OnUpdate()
    {
        if (isGround)
        {
            groundController.OnUpdate();
        }
        else
        {
            airNAController.OnUpdate();
        }
    }
}
