using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroController : HeroController
{ 
    public ZeroMagicArtsWeapon zeroMagicArtsWeapon;
    //  public List<SkillData> skillData;
    public Transform weapPos;

    public override void Init()
    {
        base.Init();
        skillDataList = ResourceSvc.Single.CurrentArchiveData.playerData.skillDatas;
  
        SkillData data = skillDataList[0];
  
        SkillData data1 = skillDataList[1];

        SkillData data2 = skillDataList[2];
        SkillData data3 = skillDataList[3];
        SkillData data4 = skillDataList[4];

        SkillData data5 = skillDataList[5];//Aor1
        SkillData data6 = skillDataList[6];//Air2
        SkillData data7 = skillDataList[7];//Air3

        SkillData data8 = skillDataList[8];//法术攻击

        ZeroDef def = new ZeroDef(this, ref data4);

        ZeroNormalAttack1 zeroNormalAttack1 = new ZeroNormalAttack1(this, ref data1);
        ZeroNormalAttack2 zeroNormalAttack2 = new ZeroNormalAttack2(this, ref data2);
        ZeroNormalArrack3 zeroNormalAttack3 = new ZeroNormalArrack3(this, ref data3);

        BaseSkill[] groundSkill = new BaseSkill[3] { zeroNormalAttack1, zeroNormalAttack2, zeroNormalAttack3 };

        ZeroGroundNormalController zeroGroundNoralController = new ZeroGroundNormalController(this, ref data, ref groundSkill);

        ZeroAirNormalAttack1 zeroAirNormalAttack1 = new ZeroAirNormalAttack1(this, ref data5);
        ZeroAirNormalAttack2 zeroAirNormalAttack2 = new ZeroAirNormalAttack2(this, ref data6);
        ZeroAirNormalAttack3 zeroAirNormalAttack3 = new ZeroAirNormalAttack3(this, ref data7);
        BaseSkill[] airSkill = new BaseSkill[3] { zeroAirNormalAttack1, zeroAirNormalAttack2, zeroAirNormalAttack3 };
        ZeroAirNormalController zeroAirNormalController = new ZeroAirNormalController(this, ref data, ref airSkill);


        ZeroNormalController zeroNormalController = new ZeroNormalController(this, data, zeroAirNormalController, zeroGroundNoralController);

        ZeroMagicArts zeroMagicArts = new ZeroMagicArts(this, ref data8);
        BaseSkill[] baseSkills = new BaseSkill[3] { zeroNormalController, def, zeroMagicArts };
        skillManager.InitSkillManager(baseSkills);

        zeroMagicArtsWeapon = ResourceSvc.Single.LoadOrCreate<GameObject>(WeaponPath.ZeroCastWeapon).GetComponent<ZeroMagicArtsWeapon>();
        zeroMagicArtsWeapon.Init(weapPos);
    }
  
}


