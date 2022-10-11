using cfg.Data;
using UnityEngine;

public class ShopEquipSalesItem : BasePanelCancelDrag
{

    public UnityEngine.UI.Image EquipImage;

    public UnityEngine.UI.Text EquipNameText;

    public UnityEngine.UI.Text PriceText;

    public UnityEngine.UI.Text DescribeText;

    public UnityEngine.UI.Button BuyButton;

    public int id;

    EquipData equipData;

    public virtual void InitGetCompoent()
    {
        UnityEngine.Transform fcequipbox = transform.Find("FCEquipBox ");
        UnityEngine.Transform fequipimage = fcequipbox.Find("FEquipImage");
        EquipImage = fequipimage.GetComponent<UnityEngine.UI.Image>();
        UnityEngine.Transform fequipnametext = transform.Find("FEquipNameText");
        EquipNameText = fequipnametext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform fpricetext = transform.Find("FPriceText");
        PriceText = fpricetext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform fdescribetext = transform.Find("FDescribeText");
        DescribeText = fdescribetext.GetComponent<UnityEngine.UI.Text>();
    }

    public void Init(int id)
    {
        base.Init();
        this.id = id;
        equipData = resourceSvc.GetEquipDataByID(id);
        BuyButton.onClick.AddListener(OnClickBuyButton);
        SetImageSprite(EquipImage, PeopPath.Peop + equipData.ResName);
        SetText(EquipNameText, equipData.UnitName);
        SetText(PriceText, equipData.PriceWeapons);
        SetText(DescribeText, equipData.Describe);
    }

    public void OnClickBuyButton()
    {
        //TODOBuyEquip
        if (MainSceneSys.Single.playerData.gold - equipData.PriceWeapons < 0)
        {
            //TODO
            Debug.Log("金币不足");
        }
        else
        {
            MainSceneSys.Single.playerData.gold -= equipData.PriceWeapons;
            //ArchiveData archiveData = resourceSvc.CurrentArchiveData;
            //archiveData.playerData = MainSceneSys.Single.playerData;
            //// archiveData.bagEquipData = new System.Collections.Generic.List<EquipData>();
            //archiveData.bagEquipData.Add(resourceSvc.CfgDataEquidToWMDataEquip(equipData));
            //SaveArchive.SavePlayDataArchive(archiveData);
            SaveArchive.BagEquipAdd(resourceSvc.CfgDataEquidToWMDataEquip(equipData));
            Debug.Log("购买成功");
        }
    }
}
