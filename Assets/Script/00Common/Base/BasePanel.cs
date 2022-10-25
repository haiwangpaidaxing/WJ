using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BasePanel : MonoBehaviour, IDragHandler
{
    protected ResourceSvc resourceSvc;
    protected AudioSvc audioSvc;
    [Header("允许窗口拖拽")]
    public bool isDragHandler = false;
    public void SetPanelState(bool value = true)
    {
        if (gameObject.activeSelf != value)
        {
            gameObject.SetActive(value);
        }
        if (value)
        {
            Init();
        }
        else
        {
            Clear();
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragHandler)
        {
            transform.position = eventData.position;
            return;
        }
    }
    public virtual void Init()
    {
        resourceSvc = ResourceSvc.Single;
        audioSvc = AudioSvc.Single;
        //Debug.Log(gameObject.name);
    }

    public virtual void Clear()
    {
        resourceSvc = null;
        audioSvc = null;
    }

    #region Tool Functions

    protected void SetActive(GameObject go, bool isActive = true)
    {
        go.SetActive(isActive);
    }
    protected void SetActive(Transform trans, bool state = true)
    {
        trans.gameObject.SetActive(state);
    }
    protected void SetActive(RectTransform rectTrans, bool state = true)
    {
        rectTrans.gameObject.SetActive(state);
    }
    protected void SetActive(Image img, bool state = true)
    {
        img.transform.gameObject.SetActive(state);
    }
    protected void SetActive(Text txt, bool state = true)
    {
        txt.transform.gameObject.SetActive(state);
    }

    protected void SetText(Text txt, string context = "")
    {
        txt.text = context;
    }
    protected void SetText(Transform trans, int num = 0)
    {
        SetText(trans.GetComponent<Text>(), num);
    }
    protected void SetText(Transform trans, string context = "")
    {
        SetText(trans.GetComponent<Text>(), context);
    }
    protected void SetText(Text txt, int num = 0)
    {
        SetText(txt, num.ToString());
    }
    public void ButtonOnClick(Button button, UnityEngine.Events.UnityAction onClick)
    {
        button.onClick.AddListener(onClick);
    }
    public void ButtonOnClickSoundEffects(Button button, UnityEngine.Events.UnityAction onClick, string audioName = SoundEffects.CommonOnClickBg)
    {
        button.onClick.AddListener(() =>
        {
            audioSvc.PlayUI(audioName);
        }
        );
        button.onClick.AddListener(onClick);

    }
    public void ButtonReAllOnClick(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    public void GameObjectChildAdd(GameObject parent, GameObject child)
    {
        child.transform.parent = parent.transform;
    }
    public void TransformChildAdd(Transform parent, Transform child)
    {
        child.SetParent(parent, false);
    }
    public void TransformChildReset(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
    public void GameObjectChildAll(GameObject parent)
    {
        TransformChildReset(parent.transform);
    }

    public void SetImageSprite(Image image, string resPath)
    {
        image.sprite = resourceSvc.Load<Sprite>(resPath);
    }
    #endregion
}
public abstract class BasePanelCancelDrag : MonoBehaviour
{
    protected ResourceSvc resourceSvc;
    protected AudioSvc audioSvc;
    public void SetPanelState(bool value = true)
    {
        if (gameObject.activeSelf != value)
        {
            gameObject.SetActive(value);
        }
        if (value)
        {
            Init();
        }
        else
        {
            Clear();
        }
    }
    public virtual void Init()
    {
        resourceSvc = ResourceSvc.Single;
        audioSvc = AudioSvc.Single;
        //Debug.Log(gameObject.name);
    }

    public virtual void Clear()
    {
        resourceSvc = null;
        audioSvc = null;
    }

    #region Tool Functions

    protected void SetActive(GameObject go, bool isActive = true)
    {
        go.SetActive(isActive);
    }
    protected void SetActive(Transform trans, bool state = true)
    {
        trans.gameObject.SetActive(state);
    }
    protected void SetActive(RectTransform rectTrans, bool state = true)
    {
        rectTrans.gameObject.SetActive(state);
    }
    protected void SetActive(Image img, bool state = true)
    {
        img.transform.gameObject.SetActive(state);
    }
    protected void SetActive(Text txt, bool state = true)
    {
        txt.transform.gameObject.SetActive(state);
    }

    protected void SetText(Text txt, string context = "")
    {
        txt.text = context;
    }
    protected void SetText(Transform trans, int num = 0)
    {
        SetText(trans.GetComponent<Text>(), num);
    }
    protected void SetText(Transform trans, string context = "")
    {
        SetText(trans.GetComponent<Text>(), context);
    }
    protected void SetText(Text txt, int num = 0)
    {
        SetText(txt, num.ToString());
    }
    public void ButtonOnClick(Button button, UnityEngine.Events.UnityAction onClick)
    {
        button.onClick.AddListener(onClick);
    }
    public void ButtonOnClickSoundEffects(Button button, UnityEngine.Events.UnityAction onClick, string audioName = SoundEffects.CommonOnClickBg)
    {
        button.onClick.AddListener(() =>
        {
            audioSvc.PlayUI(audioName);
        }
        );
        button.onClick.AddListener(onClick);

    }
    public void ButtonReAllOnClick(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    public void GameObjectChildAdd(GameObject parent, GameObject child)
    {
        child.transform.parent = parent.transform;
    }
    public void TransformChildAdd(Transform parent, Transform child)
    {
        child.SetParent(parent, false);
    }
    public void TransformChildReset(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
    public void GameObjectChildAll(GameObject parent)
    {
        TransformChildReset(parent.transform);
    }

    public void SetImageSprite(Image image, string resPath)
    {
        image.sprite = resourceSvc.Load<Sprite>(resPath);
    }
    #endregion
}
