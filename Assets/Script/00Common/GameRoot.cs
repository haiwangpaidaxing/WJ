using System.Collections;
using UnityEngine;
using System.CodeDom;
using System.IO;
using System.CodeDom.Compiler;
using UnityEditor;
using WMEffectsSkill;
using System;

public interface IInit
{
    void Init();
}
public class GameRoot : MonoSingle<GameRoot>
{
    [HideInInspector]
    public AudioSvc audioSvc;
    [HideInInspector]
    public ResourceSvc resourceSvc;
    [HideInInspector]
    public NetSvc netSvc;
    [HideInInspector]
    public MessageSvc messageSvc;
    [HideInInspector]
    public TimerSvc globalTimerSvc;
    public UISvc uISvc;
    public LogicSys logicSys;

    private void Awake()
    {

        Debug.Log("GameRootInit...");
        DontDestroyOnLoad(gameObject);
        //初始化服务
        GetSvc(ref resourceSvc);
        GetSvc(ref audioSvc);
        GetSvc(ref uISvc);
        GetSvc(ref messageSvc);
        GetSvc(ref globalTimerSvc);
        GetSvc(ref netSvc);



        //初始化系统
        logicSys = GetComponent<LogicSys>();
        logicSys.Init();

        //WMData.EquipData equipData = new WMData.EquipData();
        //equipData.EquipQualityType = cfg.Data.EquipQualityType.Rare;
        //equipData.entryKey = new System.Collections.Generic.List<WMData.EntryKey>();
        //equipData.OpenEquip();
    }
    public void GetSvc<T>(ref T t) where T : MonoSingle<T>
    {

        try
        {
            t = GetComponent<T>();
            if (t == null)
            {
                t = gameObject.AddComponent<T>();
            }
            t.Init();
        }
        catch
        {

        }
    }
    private void Update()
    {
        globalTimerSvc.UpdateTask();
    }
    public GameObject CreateRole(cfg.Data.RoleData roleData)
    {
        GameObject role = resourceSvc.LoadOrCreate<GameObject>(HeroPath.Hero + "/" + roleData.ResName);
        role.AddComponent<RoleAttribute>().Init(roleData.RoleAttribute, resourceSvc.CurrentArchiveData.equipSoltDatas);

        role.GetComponent<RoleController>().Init();
        role.GetComponent<WUBT.HeroDatabase>().Init();
        role.GetComponent<RoleTree>().Init();
        return role;
    }


    /// <summary>
    /// 设置技能效果
    /// </summary>
    /// <param name="effectsSkillData"></param>
    public void SetSkillEffect(EffectsSkillData effectsSkillData)
    {
        SkillData skillData = null;
        foreach (var skill in resourceSvc.CurrentArchiveData.playerData.skillDatas)
        {
            if (effectsSkillData.skillID == skill.id)
            {
                skillData = skill;
                break;
            }
        }
        if (skillData == null)
        {
            return;
        }
        Type type = Type.GetType("WMEffectsSkill." + effectsSkillData.className);
        Type[] types = new Type[1];
        types[0] = typeof(EffectsSkillData);
        object obj = Activator.CreateInstance(type, new object[1] { effectsSkillData });
        skillData.baseEffectsSkills[effectsSkillData.skillEffectsIndex] = (BaseEffectsSkill)obj;
        resourceSvc.Save();
    }
    public Coroutine skillCDCoroutine;
}
