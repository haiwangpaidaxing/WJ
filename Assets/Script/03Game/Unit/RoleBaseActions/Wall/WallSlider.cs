﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class RoleWallSlider :BaseRoleState
{
    public RoleWallSlider(string animName, string audioName = "") : base(animName, audioName)
    {
    }


    protected override void Enter()
    {
        base.Enter();
    }

    protected override BTResult Execute()
    {
        roleController.MoveY(1,-1);
        return base.Execute();
    }

    protected override void Exit()
    {
        base.Exit();
    }
}