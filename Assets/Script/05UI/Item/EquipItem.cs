
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WMData;

public interface IEquipData
{
    EquipData EquipData { get; set; }
    public void CB();
}
public class EquipItem : MonoBehaviour, IEquipData, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    public Image equipImage;
    public EquipData EquipData { get; set; }

    EquipInfoPanel infoPanel;
    public Transform showParent;
    public void Init(EquipData equipData, Transform showParent, EquipInfoPanel equipInfoPanel)
    {
        this.infoPanel = equipInfoPanel;
        this.showParent = showParent;
        this.EquipData = equipData;
        equipImage.sprite = ResourceSvc.Single.Load<Sprite>(PeopPath.Peop + equipData.ResName);
    }

    public void OnDrag(PointerEventData eventData)
    {
        equipImage.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        equipImage.transform.SetParent(transform);
        equipImage.transform.localPosition = Vector3.zero;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        equipImage.transform.SetParent(showParent);

    }

    public void CB()
    {
        SaveArchive.BagEquipRemove(EquipData);
        Destroy(equipImage.gameObject);
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        infoPanel.gameObject.SetActive(true);
        infoPanel.Show(EquipData);
    }
}
