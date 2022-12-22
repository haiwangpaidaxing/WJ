using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillManager : MonoBehaviour
{
    [SerializeField] BaseSkill[] baseSkillList;
    public SkillData currentSkill;
    public RoleAttribute roleAttribute;
    /// <summary>
    /// 技能CD的携程
    /// </summary>
    List<Coroutine> skillCDCoroutineList;
    public void InitSkillManager(BaseSkill[] baseSkillList)
    {
        this.baseSkillList = baseSkillList;
        skillCDCoroutineList = new List<Coroutine>();
    }
    public BaseSkill USESkill(int skillID, ref RoleAttribute roleAttribute)
    {
        this.roleAttribute = roleAttribute;

        BaseSkill _currentSkill = null;
        if (FindSkillByID(skillID, ref _currentSkill))
        {
            return _currentSkill;
        }
        return null;
        //if (CheckSkillConditions(ref currentSkill.skillData))
        //{
        //    if (currentSkill.skillData.nexSkillID != 0)
        //    {
        //        if (currentSkill.skillData.comboCurrentValidTime > 0)
        //        {
        //            currentSkill = FindSkillByID(currentSkill.skillData.id);
        //        }
        //    }
        //    return currentSkill;
        //}
    }

    //根据ID查找技能
    public bool FindSkillByID(int skillID, ref BaseSkill skill)
    {
        foreach (var currentSkill in baseSkillList)
        {
            if (currentSkill.skillData.id == skillID)
            {
                skill = currentSkill;
                break;
            }
        }//先查找技能
        if (skill == null)
        {
            Debug.Log(skillID + "Skill Null");
            return false;
        }
        return CheckSkillConditions(ref skill.skillData);
    }
    private bool CheckSkillConditions(ref SkillData skillData)
    {
        //if (skillData.comboCurrentValidTime > 0)
        //{
        //    BaseSkill nextSkill = FindSkillByID(skillData.nexSkillID);
        //    nextSkill.skillData.currentCD = nextSkill.skillData.cd + nextSkill.skillData.skillTime;
        //    skillCDCoroutine = StartCoroutine(SkillCD(nextSkill.skillData));
        //    return true;
        //}
        if (skillData.currentCD <= 0 && (roleAttribute.GetMP() - skillData.MP >= 0))
        {
            roleAttribute.SetMP((int)skillData.MP);
            skillData.currentCD = skillData.cd + skillData.skillTime;
            AddCDTask(ref skillData);
            //技能的动画时间+技能cd
            //for (int i = 0; i < skillCDCoroutineList.Length; i++)
            //{
            //    if (skillCDCoroutineList[i] == null)
            //    {
            //        skillCDCoroutineList[i] = StartCoroutine(SkillCD(skillData, i));
            //        break;
            //    }
            //}
            //TimerSvc.instance.AddTask(skillData.currentCD, () => { SkillCD( skillData); },"CD");
            return true;
        }
        return false;
    }
    public void AddCDTask(ref SkillData skillData)
    {
        skillCDCoroutineList.Add(StartCoroutine(SkillCD(skillData, skillCDCoroutineList.Count)));
    }

    private IEnumerator SkillCD(SkillData skillData, int index)
    {
        while (true)
        {
            if (skillData.currentCD <= 0)
            {
                StopCoroutine(skillCDCoroutineList[index]);
            }
            skillData.currentCD -= 0.02f;
            //Debug.Log(skillData.id + "______" + skillData.currentCD + skillData.animName + "冷却中...");
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
}
