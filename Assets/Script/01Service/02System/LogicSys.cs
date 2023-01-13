using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogicSys : BaseSys<LogicSys>
{
    public LogicPanel logicPanel;
    public SelectRolePanel selectRolePanel;
    public ArchivePanel archivePanel;
    private int archiveID;//当前选中存档的ID
    public override void Init()
    {
        base.Init();
        logicPanel = UISvc.Single.GetPanel<LogicPanel>(UIPath.LogicPanel, UISvc.StateType.Close);
        archivePanel = UISvc.Single.GetPanel<ArchivePanel>(UIPath.ArchivePanel, UISvc.StateType.Close);
        selectRolePanel = UISvc.Single.GetPanel<SelectRolePanel>(UIPath.SelectRolePanel, UISvc.StateType.Close);
        EnterLogicPanel();
        Debug.Log("初始化登录系统....");
        //UISvc.Single.AddTips("初始化登录系统....");
    }

    public void EntGame(int archiveID)
    {
        UISvc.Single.CloseAll();
        resourceSvc.SetArchiveData(archiveID);
        Debug.Log("进入主场景当前选中的存档的ID为：" + archiveID);
        // UISvc.Single.AddTips("进入主场景当前选中的存档的ID为：" + archiveID);
        resourceSvc.JumpSceme(ScenePath.MainScene, () => { MainSceneSys.Single.Init(); });
    }

    /// <summary>
    /// 进入单机游戏存档面板
    /// </summary>
    public void EnterSingleGame()
    {
        UISvc.Single.SetSinglePanel(archivePanel, UISvc.StateType.Show);
    }

    /// <summary>
    /// 进入登录界面
    /// </summary>
    public void EnterLogicPanel()
    {
        UISvc.Single.SetSinglePanel(logicPanel, UISvc.StateType.Show);
    }

    /// <summary>
    /// 选择英雄界面
    /// </summary>
    public void EnterSelectHeroPanel()
    {
        ///TODO 读取英雄配置 
        ///创建选择英雄Item并初始化  显示的图片 名字 展示属性 
        ///当按下开始游戏时 记录当前角色ID  进入主场景时创建出预制体
        cfg.Data.RoleData[] roleDatas = resourceSvc.RoleDataConfig;
        for (int i = 0; i < roleDatas.Length; i++)
        {
            SelectRoleItem selectRoleItem = resourceSvc.LoadOrCreate<GameObject>(UIItemPath.SelectRoleItem).GetComponent<SelectRoleItem>();
            selectRoleItem.Init(roleDatas[i].UnitName, resourceSvc.Load<Sprite>("sprites/UI/" + roleDatas[i].ResName), roleDatas[i].UntID);
            selectRolePanel.TransformChildAdd(selectRolePanel.content.transform, selectRoleItem.gameObject.transform);
        }
        UISvc.Single.SetSinglePanel(selectRolePanel, UISvc.StateType.Show);
    }

    /// <summary>
    /// 进入网络游戏
    /// </summary>
    public void EnterNetGame()
    {
        EnterLogicPanel();
    }
}
