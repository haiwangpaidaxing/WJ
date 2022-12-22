using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WMData;

public class EquipSoltItem : MonoBehaviour, IDropHandler, IEquipData, IPointerDownHandler
{
    float onClickTimer;
    public cfg.Data.EquipType equipType;
    public Image equipImage;
    public Sprite startSprite;
    [SerializeField]
    int onClickNumber;

    public int index;
    public EquipData EquipData { get; set; }
    private void Awake()
    {
        this.startSprite = equipImage.sprite;
    }
    public void CB()
    {
        /// ��������
        ///     ��
        ///�� ���ϰ� ��
        ///��   ��   ��
        ///��        ��
        /// 
        /// ��
    }
    public void Init(EquipData equipData)
    {
        if (equipData.Id == 0)//����Ϊnull
        {
            return;
        }
        this.EquipData = equipData;
        Enter();
    }
    public void Enter()
    {
        if (EquipData.ResName != "")
        {
            equipImage.sprite = ResourceSvc.Single.Load<Sprite>(PeopPath.Peop + EquipData.ResName);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        IEquipData equip = eventData.pointerDrag.GetComponent<IEquipData>();
        if (equip == null)
        {
            return;
        }
        if (!equip.EquipData.ISOpen)
        {
            UISvc.Single.AddTips("װ��δ����");
            //   Debug.Log("װ��δ����");
            return;
        }
        if (equip.EquipData.EquipType != equipType)
        {
            UISvc.Single.AddTips("װ�����Ͳ�һ");
            //   Debug.Log("װ�����Ͳ�һ");
            return;
        }

        if (EquipData.Id != 0)//���ݽ��н���
        {
            SaveArchive.BagEquipAdd(EquipData);
            SaveArchive.BagEquipRemove(equip.EquipData);
            EventCenter.Broadcast((EventType)MyEvent.EType.UpdateBag);
        }
        this.EquipData = equip.EquipData;
        equip.CB();
        equipImage.sprite = ResourceSvc.Single.Load<Sprite>(PeopPath.Peop + EquipData.ResName);
        SaveArchive.EquipSoltAdd(EquipData, index);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onClickNumber++;
        if (onClickNumber >= 2 && EquipData.Id != 0)
        {
            SaveArchive.BagEquipAdd(EquipData);
            this.EquipData = new EquipData();
            onClickNumber = 0;
            onClickTimer = 0;
            equipImage.sprite = startSprite;
            EventCenter.Broadcast((EventType)MyEvent.EType.UpdateBag);
            SaveArchive.BagEquipSoltRemove(EquipData, index);
        }
    }

    private void Update()
    {
        if (onClickNumber >= 1)
        {
            onClickTimer += Time.deltaTime;
            if (onClickTimer >= 1)
            {
                onClickTimer = 0;
                onClickNumber = 0;
            }
        }
    }
}
