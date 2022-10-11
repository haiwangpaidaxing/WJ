using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroAirNormalAttack2 :BaseAirNormalAttack
{
    ZeroController zeroController;
    public ZeroAirNormalAttack2(ZeroController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
        this.zeroController = roleController;
    }
    protected override void Damage(IInjured enemy)
    {
        roleController.MoveY(1, 2);
        base.Damage(enemy);
    }
   
}
    