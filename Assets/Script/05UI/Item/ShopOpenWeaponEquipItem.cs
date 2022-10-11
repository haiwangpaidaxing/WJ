using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WMData;

public class ShopOpenWeaponEquipItem : MonoBehaviour, IEquipData, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image equipImage;
    public EquipData EquipData { get; set; }

    public Transform showParent;
    public void Init(EquipData equipData, Transform showParent)
    {
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
        //SaveArchive.BagEquipRemove(EquipData);
        Destroy(equipImage.gameObject);
        Destroy(gameObject);
    }
}
