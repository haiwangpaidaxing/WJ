using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISvc : MonoSingle<UISvc>
{
    public enum StateType
    {
        Show, Close, Popup
    }
    Transform uiRoot;
    //Transform uiShow;
    //Transform uiClose;
    Transform uiPopup;

    [SerializeField]
    List<BasePanel> showPanel;
    [SerializeField]
    List<BasePanel> closePanel;

    public override void Init()
    {
        showPanel = new List<BasePanel>();
        closePanel = new List<BasePanel>();
        uiRoot = GameObject.Find("UIRoot").transform;
        //uiShow = new GameObject("Show").transform;
        //uiClose = new GameObject("Close").transform;
        uiPopup = new GameObject("Popup").transform;

        //uiShow.SetParent(uiRoot, false);
        //uiClose.SetParent(uiRoot, false);
        uiPopup.SetParent(uiRoot, false);
        //  uiClose.gameObject.SetActive(false);
        Debug.Log("UI服务");
    }
    void UIShow(BasePanel pl)
    {
        pl.transform.SetParent(uiRoot, false);
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
        pl.transform.SetParent(uiRoot, false);
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
        GameObject panel = ResourceSvc.Single.LoadOrCreate<GameObject>(path);
        T t = panel.GetComponent<T>();
        SetPanelState(t, stateType);
        return t;
    }

}
