using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WMEffectsSkill
{
    [System.Serializable]
    public class BaseEffectsSkill
    {
        public EffectsSkillData skillData;
        public virtual void USE(RoleController roleController, RoleController releaser) { }

        public static implicit operator BaseEffectsSkill(Type v)
        {
            throw new NotImplementedException();
        }
    }
    [System.Serializable]
    public class Repel : BaseEffectsSkill
    {
        public Repel( EffectsSkillData effectsSkillData)
        {
            this.skillData = effectsSkillData;    
        }
        public override void USE(RoleController roleController, RoleController releaser)
        {
            roleController.MoveX(releaser.roleDir,skillData.value);
            roleController.SyncImage(-releaser.roleDir);
        }
    }

    [System.Serializable]
    public class Diaup : BaseEffectsSkill
    {
        public Diaup( EffectsSkillData effectsSkillData)
        {
            this.skillData = effectsSkillData;
        }
        public override void USE(RoleController roleController, RoleController releaser)
        {
            roleController.MoveY(1,skillData.value);
        }
    }
    [System.Serializable]
    public struct EffectsSkillData
    {
        public string className;
        public float value;
        public string name;
        public string describe;
        public int skillEffectsIndex;//有多个技能效果  对应哪个技能效果
        public int skillID;//对应哪个技能

        public EffectsSkillData(string className, float value=0, string name="未设定", string describe="", int skillEffectsIndex=0, int skillID = 0)
        {
            this.className = className;
            this.value = value;
            this.name = name;
            this.describe = describe;
            this.skillEffectsIndex = skillEffectsIndex;
            this.skillID = skillID;
        }   
    }

    [System.Serializable]
    public class EffectsSkillDataConfig
    {
        public List<EffectsSkillData> effectsSkillDataList = new List<EffectsSkillData>();
    }
}

