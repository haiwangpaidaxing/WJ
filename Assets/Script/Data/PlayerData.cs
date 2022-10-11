using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    //武器 金币  等级.....
    public int selectRoleId;
    public int gold;
    public int heroLevel;
    public cfg.Data.RoleData roleData;
    public List<SkillData> skillDatas;
}
