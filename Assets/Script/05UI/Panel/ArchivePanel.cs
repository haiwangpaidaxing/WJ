using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 存档面板
/// </summary>
public class ArchivePanel : BasePanel
{
    public Transform archiveContent;
    public Button rerurnButton;
    public Button createArchiveButton;
    public override void Init()
    {
        //TODO  读取本地存档数据......
        //Debug.Log("读取本地存档数据.....");
        base.Init();
        ArchiveData[] archiveData = resourceSvc.ArchiveDataConfig.dataList.ToArray();
        for (int i = 0; i < archiveData.Length; i++)
        {
            GameObject archiveItemOb = resourceSvc.LoadOrCreate<GameObject>(UIItemPath.ArchiveItem);
            archiveItemOb.transform.SetParent(archiveContent, false);
            ArchiveItem archiveItem = archiveItemOb.GetComponent<ArchiveItem>();
            archiveItem.Init(archiveData[i]);
        }
        ButtonOnClickSoundEffects(rerurnButton, () =>
        {
            LogicSys.Single.EnterLogicPanel();
            //SetPanelState(false);
        });
        ButtonOnClickSoundEffects(createArchiveButton, () =>
        {
            //GameObject archiveItem = resourceSvc.LoadOrCreate<GameObject>(UIPath.ArchiveItem);
            //archiveItem.transform.SetParent(archiveContent,false);
            LogicSys.Single.EnterSelectHeroPanel();
        });
     
    }
    public override void Clear()
    {
        base.Clear();
        ButtonReAllOnClick(rerurnButton);
        ButtonReAllOnClick(createArchiveButton);
    }
}
