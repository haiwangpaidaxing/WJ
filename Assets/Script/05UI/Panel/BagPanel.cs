using UnityEngine;
using WMData;

public class BagPanel : BasePanel
{
    public UnityEngine.UI.Button ReturnButton;

    public UnityEngine.UI.Image RoleImage;

    public UnityEngine.UI.ScrollRect Bg;

    public UnityEngine.Transform BgContent;

    public UnityEngine.UI.Text CriticalChanceText;

    public UnityEngine.UI.Text CriticalDamageText;

    public UnityEngine.UI.Text HPText;

    public UnityEngine.UI.Text Harm;

    public UnityEngine.UI.Text coinCountText;

    public EquipSoltItem[] soltItem;

    public EquipInfoPanel equipInfoPanel;

    public Transform showProp;
    public virtual void InitGetCompoent()
    {
        UnityEngine.Transform freturnbutton = transform.Find("FReturnButton");
        ReturnButton = freturnbutton.GetComponent<UnityEngine.UI.Button>();
        UnityEngine.Transform fcplayerinfo = transform.Find("FCPlayerInfo");
        UnityEngine.Transform froleimage = fcplayerinfo.Find("FRoleImage");
        RoleImage = froleimage.GetComponent<UnityEngine.UI.Image>();
        UnityEngine.Transform fbg = transform.Find("FBg");
        Bg = fbg.GetComponent<UnityEngine.UI.ScrollRect>();
        BgContent = Bg.content;
        UnityEngine.Transform fcroleattributeinfo = transform.Find("FCRoleAttributeInfo");
        UnityEngine.Transform fcriticalchancetext = fcroleattributeinfo.Find("FCriticalChanceText");
        CriticalChanceText = fcriticalchancetext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform fcriticaldamagetext = fcroleattributeinfo.Find("FCriticalDamageText");
        CriticalDamageText = fcriticaldamagetext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform fhptext = fcroleattributeinfo.Find("FHPText");
        HPText = fhptext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform fharm = fcroleattributeinfo.Find("FHarm");
        Harm = fharm.GetComponent<UnityEngine.UI.Text>();
    }

    public override void Init()
    {
        base.Init();
        SaveArchive.bagEquipUpdateCB += UpdaRoleInfo;
        ButtonOnClickSoundEffects(ReturnButton, () =>
        {
            UISvc.Single.SetPanelState(this, UISvc.StateType.Close);
        });
        UpdaRoleInfo(resourceSvc.CurrentArchiveData.equipSoltDatas);
    }
    public void UpdaRoleInfo(EquipData[] equipDatas)
    {

        float harmValue = 0;
        float hpValue = 0;
        float critHarmValue = 0;
        float criticalChanceValue = 0;
        for (int i = 0; i < equipDatas.Length; i++)
        {
            harmValue += equipDatas[i].Attribute_Harm;
            hpValue += equipDatas[i].Attribute_HP;
            critHarmValue += equipDatas[i].Attribute_CritHarm;
            criticalChanceValue += equipDatas[i].Attribute_CriticalChance;
        }

        Harm.text = "ÉËº¦:" + harmValue;
        HPText.text = "ÑªÁ¿:" + hpValue;
        CriticalDamageText.text = "±©»÷ÉËº¦:" + critHarmValue + "%";
        CriticalChanceText.text = "±©»÷ÂÊ" + criticalChanceValue + "%";
    }

    public void UpdateCoin(float value)
    {
        coinCountText.text = value.ToString();
    }
    public override void Clear()
    {
        SaveArchive.bagEquipUpdateCB -= UpdaRoleInfo;
        ButtonReAllOnClick(ReturnButton);
        TransformChildReset(BgContent);
    }
}
