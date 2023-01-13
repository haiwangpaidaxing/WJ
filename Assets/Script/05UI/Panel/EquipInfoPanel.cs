using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WMData;

public class EquipInfoPanel : BasePanel, IPointerDownHandler
{

    public UnityEngine.UI.Image EquipSprite;

    public UnityEngine.UI.Text WeaponName;

    public UnityEngine.UI.Text WeaponDescribe;

    public Transform entryTextConternt;

    public Text harm;
    public Text hp;
    public Text critHarm;
    public Text criticalChance;

    [SerializeField]
    int onClickNumber;

    float onClickTimer;
    public virtual void InitGetCompoent()
    {
        Transform fequipsprite = transform.Find("FEquipSprite");
        EquipSprite = fequipsprite.GetComponent<Image>();
        Transform fweaponname = transform.Find("FWeaponName");
        WeaponName = fweaponname.GetComponent<Text>();
        Transform fweapondescribe = transform.Find("FWeaponDescribe");
        WeaponDescribe = fweapondescribe.GetComponent<Text>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onClickNumber++;
        if (onClickNumber >= 2)
        {
            onClickNumber = 0;
            onClickTimer = 0;
            harm.gameObject.SetActive(false);
            hp.gameObject.SetActive(false);
            criticalChance.gameObject.SetActive(false);
            critHarm.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void Show(WMData.EquipData equipData)
    {
        base.Init();
        harm.gameObject.SetActive(false);
        hp.gameObject.SetActive(false);
        criticalChance.gameObject.SetActive(false);
        critHarm.gameObject.SetActive(false);
      //  gameObject.SetActive(false);
        WeaponName.text = equipData.EquipName;
        WeaponDescribe.text = equipData.Describe;
        SetImageSprite(EquipSprite, PeopPath.Peop + equipData.ResName);
        for (int i = 0; i < equipData.entryKey.Count; i++)
        {
            switch (equipData.entryKey[i])
            {
                case WMData.EntryKey.Harm:
                    harm.text = "ÉËº¦:" + equipData.Attribute_Harm;
                    harm.gameObject.SetActive(true);
                    break;
                case WMData.EntryKey.Hp:
                    hp.text = "ÑªÁ¿:" + equipData.Attribute_HP;
                    hp.gameObject.SetActive(true);
                    break;
                case WMData.EntryKey.CritHarm:
                    critHarm.text = "±©»÷ÉËº¦:" + equipData.Attribute_CritHarm +"%";
                    critHarm.gameObject.SetActive(true);
                    break;
                case WMData.EntryKey.CriticalChance:
                    criticalChance.text = "±©»÷ÂÊ:" + equipData.Attribute_CriticalChance + "%";
                    criticalChance.gameObject.SetActive(true);
                    break;
            }
        }
    }
  
    private void OnDisable()
    {
        base.Clear();
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
