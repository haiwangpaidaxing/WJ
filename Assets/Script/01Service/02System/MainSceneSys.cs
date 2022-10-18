using cfg.Data;
using System;
using UnityEngine;
using WMEffectsSkill;

public class MainSceneSys : BaseSys<MainSceneSys>
{
    public PlayerData playerData;
    #region UIPanel
    public SkillPanel skillPanel;
    public BagPanel bagPanel;
    public MainScenePanel mainScenePanel;
    public ShopPanel shopPanel;
    public SelectLevelPanel selectLevelPanel;
    public EquipSoltItem[] equipSoltItem;
    #endregion
    public override void Init()
    {
        base.Init();
        EventCenter.AddListener((EventType)MyEvent.EType.UpdateBag, UpdateBag);
        ArchiveData archiveData = resourceSvc.CurrentArchiveData;
        playerData = archiveData.playerData;//获取当前玩家数据
        playerData.roleData = resourceSvc.GetRoleDataByID(playerData.selectRoleId);
        InitUIPanel();
        InitEquipSolt();
        Debug.Log("初始化主场景....");
    }
    private void OnDestroy()
    {
        try
        {
            EventCenter.RemoveListener((EventType)MyEvent.EType.UpdateBag, UpdateBag);
        }
        catch
        {
        }
    }
    public override void Quit()
    {
        EventCenter.RemoveListener((EventType)MyEvent.EType.UpdateBag, UpdateBag);
    }

    #region InitPanel...
    public void InitUIPanel()
    {
        //主场景面板
        mainScenePanel = UISvc.Single.GetPanel<MainScenePanel>(UIPath.MainScenePanel, UISvc.StateType.Show);
        //技能设置面板
        skillPanel = UISvc.Single.GetPanel<SkillPanel>(UIPath.SkillPanel);
        shopPanel = UISvc.Single.GetPanel<ShopPanel>(UIPath.ShopPanel);
        bagPanel = UISvc.Single.GetPanel<BagPanel>(UIPath.BagPanel);
        selectLevelPanel = UISvc.Single.GetPanel<SelectLevelPanel>(UIPath.SelectLevelPanel);
    }
    #endregion

    #region EenPanel...
    /// <summary>
    /// 进入技能设置界面
    /// </summary>
    public void EnterSkillPanel()
    {
        UISvc.Single.SetPanelState(skillPanel, UISvc.StateType.Show);
        SkillData[] skillDataList = resourceSvc.CurrentArchiveData.playerData.skillDatas.ToArray();
        for (int i = 0; i < skillDataList.Length; i++)
        {
            if (skillDataList[i].skillEffectsNuumber == 0)
            {
                continue;
            }
            Transform skillItem = resourceSvc.LoadOrCreate<GameObject>(UIItemPath.SkillSetItem).transform;
            skillPanel.TransformChildAdd(skillPanel.SkillListContent, skillItem);
            SkillSetItem skillSetItem = skillItem.GetComponent<SkillSetItem>();
            skillSetItem.SkillName.text = skillDataList[i].skillName;
            for (int a = 0; a < skillDataList[i].skillEffectsNuumber; a++)
            {
                Transform skillEffectsItem = resourceSvc.LoadOrCreate<GameObject>(UIItemPath.SkillEffectsSetItem).transform;
                SkillEffectsSetItem seItme = skillEffectsItem.GetComponent<SkillEffectsSetItem>();

                seItme.Init(GameRoot.Single.SetSkillEffect, a, skillDataList[i].id, skillPanel);
                if (skillDataList[i].baseEffectsSkills.Length > 0 && skillDataList[i].baseEffectsSkills[a] != null)
                {

                    seItme.skillEffectsNameText.text = skillDataList[i].baseEffectsSkills[a].skillData.name;
                }
                else
                {

                    seItme.skillEffectsNameText.text = "未设定";
                }
                skillSetItem.TransformChildAdd(skillSetItem.conetents, skillEffectsItem);
            }
        }

    }
    /// <summary>
    /// 进入背包
    /// </summary>
    public void EnterBagPanel()
    {
        UISvc.Single.SetPanelState(bagPanel, UISvc.StateType.Show);
        UpdateBag();
    }
    /// <summary>
    /// 进入商店
    /// </summary>
    public void EnterShopPanel()
    {
        UISvc.Single.SetPanelState(shopPanel, UISvc.StateType.Show);
        UpdateEquipSales();
    }
    /// <summary>
    /// 进入选择关卡
    /// </summary>
    public void EnterLevelPanel()
    {
        UISvc.Single.SetPanelState(selectLevelPanel, UISvc.StateType.Show);
        UpdateLevel();
    }
    #endregion
    ///// <summary>
    ///// 设置技能效果
    ///// </summary>
    ///// <param name="effectsSkillData"></param>
    //private void SetSkillEffect(EffectsSkillData effectsSkillData)
    //{
    //    SkillData skillData = null;
    //    foreach (var skill in resourceSvc.CurrentArchiveData.playerData.skillDatas)
    //    {
    //        if (effectsSkillData.skillID == skill.id)
    //        {
    //            skillData = skill;
    //            break;
    //        }
    //    }
    //    if (skillData == null)
    //    {
    //        return;
    //    }
    //    Type type = Type.GetType("WMEffectsSkill." + effectsSkillData.className);
    //    Type[] types = new Type[1];
    //    types[0] = typeof(EffectsSkillData);
    //    object obj = Activator.CreateInstance(type, new object[1] { effectsSkillData });
    //    skillData.baseEffectsSkills[effectsSkillData.skillEffectsIndex] = (BaseEffectsSkill)obj;
    //    resourceSvc.Save();
    //}
    #region SelectLevelFunction
    private void UpdateLevel()
    {
        selectLevelPanel.TransformChildReset(selectLevelPanel.ScrollViewContent);
        LevelConfigData[] levelConfigData = resourceSvc.LevelConfigDatas;
        for (int i = 0; i < levelConfigData.Length; i++)
        {
            GameObject gameObjectItem = resourceSvc.LoadOrCreate<GameObject>(UIItemPath.SelectLevelItem);

            SelectLevelItem selectLevelItem = gameObjectItem.GetComponent<SelectLevelItem>();

            selectLevelItem.Init(levelConfigData[i].LevelName, levelConfigData[i].LevelJumpSceneName);

            selectLevelPanel.TransformChildAdd(selectLevelPanel.ScrollViewContent, gameObjectItem.transform);
        }
    }
    #endregion
    #region BagFunction
    private void UpdateBag()
    {
        bagPanel.TransformChildReset(bagPanel.BgContent);
        ArchiveData archiveData = resourceSvc.CurrentArchiveData;
        for (int i = 0; i < archiveData.bagEquipData.Count; i++)
        {
            GameObject bagequipitem = resourceSvc.LoadOrCreate<GameObject>(UIItemPath.BagEquipItem);
            EquipItem equipItem = bagequipitem.GetComponent<EquipItem>();
            equipItem.Init(archiveData.bagEquipData[i], bagPanel.showProp, bagPanel.equipInfoPanel);
            bagPanel.TransformChildAdd(bagPanel.BgContent, bagequipitem.transform);
        }
    }

    private void InitEquipSolt()
    {
        //初始化装备栏
        ArchiveData archiveData = resourceSvc.CurrentArchiveData;
        for (int i = 0; i < bagPanel.soltItem.Length; i++)
        {
            WMData.EquipData equipData = archiveData.equipSoltDatas[i];
            EquipSoltItem equipSoltItem = bagPanel.soltItem[i];
            if (equipData.EquipType == equipSoltItem.equipType)
            {
                equipSoltItem.Init(equipData);
            }
        }
    }
    #endregion
    #region ShopFunction
    public void UpdateOpenWeaponBag()
    {
        shopPanel.TransformChildReset(shopPanel.OpenEquipBagContent);
        ArchiveData archiveData = resourceSvc.CurrentArchiveData;
        for (int i = 0; i < archiveData.bagEquipData.Count; i++)
        {
            if (archiveData.bagEquipData[i].ISOpen)
            {
                continue;
            }
            GameObject bagequipitem = resourceSvc.LoadOrCreate<GameObject>(UIItemPath.ShopOpenWeaponEquipItem);
            ShopOpenWeaponEquipItem equipItem = bagequipitem.GetComponent<ShopOpenWeaponEquipItem>();
            equipItem.Init(archiveData.bagEquipData[i], shopPanel.shopPropPos);
            bagPanel.TransformChildAdd(shopPanel.OpenEquipBagContent, bagequipitem.transform);
        }
    }

    public void UpdateEquipSales()
    {
        shopPanel.TransformChildReset(shopPanel.EquipSalesContent);
        EquipData[] equipDatas = resourceSvc.EquipConfig;
        for (int i = 0; i < equipDatas.Length; i++)
        {
            GameObject shopItem = resourceSvc.LoadOrCreate<GameObject>(UIItemPath.ShopEquipSalesItem);
            ShopEquipSalesItem shopEquipSalesItem = shopItem.GetComponent<ShopEquipSalesItem>();
            shopEquipSalesItem.Init(equipDatas[i].UntID);
            shopPanel.TransformChildAdd(shopPanel.EquipSalesContent, shopItem.transform);
        }
    }
    #endregion
}
