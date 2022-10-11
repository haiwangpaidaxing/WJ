using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRolePanel : BasePanel
{
    public Text info;
    public GameObject content;
    public Button startGameButton;
    public Button returnButton;
    SelectRoleItem[] roleItems;
    [Header("当前选择英雄的ID")]
    public int currentSelectHeroID;
    private SelectRoleItem currentRoleItem;
    public override void Init()
    {
        base.Init();
        roleItems = content.GetComponentsInChildren<SelectRoleItem>();
        ButtonOnClickSoundEffects(returnButton, () =>
        {
            GameObjectChildAll(content);
            LogicSys.Single.EnterSingleGame();
        });
        ButtonOnClickSoundEffects(startGameButton, () =>
        {
            ArchiveData archiveData = new ArchiveData();
            archiveData.playerData.selectRoleId = currentSelectHeroID;
          int archivaeID=  SaveArchive.NewPlayerDataArchive(archiveData);
            LogicSys.Single.EntGame(archivaeID); ;
        });
        ShowRoleInfo();
        startGameButton.gameObject.SetActive(false);
        roleItems = GetComponentsInChildren<SelectRoleItem>();
        for (int i = 0; i < roleItems.Length; i++)
        {
            roleItems[i].OnClickCB = (id, selectItem) =>
            {
                if (currentRoleItem != null)
                {
                    currentRoleItem.SetShadeState(true);
                }
                currentRoleItem = selectItem;
                selectItem.SetShadeState(false);
                startGameButton.gameObject.SetActive(true);
                currentSelectHeroID = id;
            };
        }
    }
    public void ShowRoleInfo()
    {
        info.text = "血量:" + 10 + "\n" + "伤害" + 10;
        //TODO 角色的属性
    }
    public override void Clear()
    {
        base.Clear();
        if (roleItems != null)
        {
            for (int i = 0; i < roleItems.Length; i++)
            {
                roleItems[i].OnClickCB = null;
            }
        }
        ButtonReAllOnClick(returnButton);
        ButtonReAllOnClick(startGameButton);


    }
}
