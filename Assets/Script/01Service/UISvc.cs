using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISvc : MonoSingle<UISvc>
{
    public TipsWindow tipsWindow;
    public enum StateType
    {
        Show, Close, Popup
    }
    Transform uiRoot;
    //Transform uiShow;
    //Transform uiClose;
    Transform uiPopup;
    [SerializeField]
    Transform uiCommon;
    [SerializeField]
    List<BasePanel> showPanel;
    [SerializeField]
    List<BasePanel> closePanel;

    public void AddTips(string hint)
    {
        tipsWindow.AddHint(hint);
    }
    public override void Init()
    {
        showPanel = new List<BasePanel>();
        closePanel = new List<BasePanel>();
        uiRoot = GameObject.Find("UIRoot").transform;
     //   uiCommon = new GameObject("Common").transform;
        uiPopup = new GameObject("Popup").transform;
    //    uiCommon.SetParent(uiRoot, false);
        uiPopup.SetParent(uiRoot, false);
        tipsWindow.gameObject.transform.SetParent(uiRoot,true);
        Debug.Log("UI服务");
    }
    void UIShow(BasePanel pl)
    {
        pl.transform.SetParent(uiCommon,false);
        pl.transform.SetSiblingIndex(uiCommon.childCount);
        pl.SetPanelState(true);
        if (!showPanel.Contains(pl))
        {
            showPanel.Add(pl);
            if (closePanel.Contains(pl))
            {
                closePanel.Remove(pl);
            }
        }
    }
    void UIClose(BasePanel pl)
    {
        pl.transform.SetParent(uiCommon, false);
        pl.SetPanelState(false);
        if (!closePanel.Contains(pl))
        {
            closePanel.Add(pl);
            if (showPanel.Contains(pl))
            {
                showPanel.Remove(pl);
            }
        }
    }
    void UIPopup(BasePanel pl)
    {
        pl.transform.SetParent(uiPopup, false);
        pl.SetPanelState(true);
    }

    public void SetPanelState(BasePanel pl, StateType stateType = StateType.Close)
    {
        switch (stateType)
        {
            case StateType.Show:
                UIShow(pl);
                break;
            case StateType.Close:
                UIClose(pl);
                break;
            case StateType.Popup:
                UIPopup(pl);
                break;
        }
    }
    /// <summary>
    /// 面板为唯一
    /// </summary>
    /// <param name="pl"></param>
    /// <param name="stateType"></param>
    public void SetSinglePanel(BasePanel pl, StateType stateType = StateType.Show)
    {
        if (stateType == StateType.Close) return;
        CloseAll();
        switch (stateType)
        {
            case StateType.Show:
                UIShow(pl);
                break;
            case StateType.Popup:
                UIPopup(pl);
                break;
        }
    }
    public void DestroyPanel(BasePanel basePanel)
    {
        Destroy(basePanel.gameObject);
    }
    public void CloseAll()
    {
        for (int i = 0; i < showPanel.Count; i++)
        {
            SetPanelState(showPanel[i]);
        }
    }
    public T GetPanel<T>(string path, StateType stateType = StateType.Close) where T : BasePanel
    {
        foreach (var item in showPanel)
        {
            if (item is T)
            {
                return item as T;
            }
        }
        foreach (var item in closePanel)
        {
            if (item is T)
            {
                return item as T;
            }
        }
        GameObject panel = ResourceSvc.Single.LoadOrCreate<GameObject>(path);
        T t = panel.GetComponent<T>();
//        panel.transform.SetParent(uiCommon,false);
        SetPanelState(t, stateType);
        return t;
    }

}
