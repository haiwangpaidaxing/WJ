using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectRoleItem : MonoBehaviour, /*IPointerEnterHandler, IPointerExitHandler,*/ IPointerDownHandler
{
    [SerializeField]
    Image heroImage;
    [SerializeField]
    Text nameText;
    [SerializeField]
    string Info;
    [SerializeField]
    GameObject shade;
    public System.Action<int, SelectRoleItem> OnClickCB;
    [SerializeField]
    public int id;

    public void Init(string name, Sprite sprite, int ID)
    {
        this.id = ID;
        nameText.text = name;
        heroImage.sprite = sprite;
        heroImage.SetNativeSize();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        shade.gameObject.SetActive(false);
        AudioSvc.Single.PlayUI(SoundEffects.CommonOnClickBg);
        OnClickCB?.Invoke(id, this);
    }
    public void SetShadeState(bool state = true)
    {
        shade.SetActive(state);
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    close.gameObject.SetActive(false);
    //    AudioSvc.Single.PlayUI(SoundEffects.selectHero);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    close.gameObject.SetActive(true);
    //}
}
