using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillConfig", menuName = "Skill")]
public class SkillDataConfig : ScriptableObject
{
    public List<SkillData> skillDatas;
}

