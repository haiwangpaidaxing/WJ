using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{

    public UnityEngine.UI.ScrollRect EquipSales;

    public UnityEngine.Transform EquipSalesContent;

    public UnityEngine.UI.Button ReturnButton;

    public UnityEngine.UI.Button EquipButton;

    public Button OpenEquipButton;

    public Transform OpenEquipBagContent;

    public GameObject OpenWeaponPanel;

    public Transform shopPropPos;

    public virtual void InitGetCompoent()
    {
        UnityEngine.Transform fequipsales = transform.Find("FEquipSales");
        EquipSales = fequipsales.GetComponent<UnityEngine.UI.ScrollRect>();
        EquipSalesContent = EquipSales.content;
        UnityEngine.Transform freturnbutton = transform.Find("FReturnButton");
        ReturnButton = freturnbutton.GetComponent<UnityEngine.UI.Button>();
        UnityEngine.Transform fequipbutton = transform.Find("FEquipButton");
        EquipButton = fequipbutton.GetComponent<UnityEngine.UI.Button>();
    }

    public override void Init()
    {

        base.Init();
        OpenWeaponPanel.SetActive(false);
        EquipSales.gameObject.SetActive(true);
        ButtonOnClickSoundEffects(ReturnButton, () =>
        {
            UISvc.Single.SetPanelState(this, UISvc.StateType.Close);
        });
        ButtonOnClickSoundEffects(EquipButton, () =>
        {
            OpenWeaponPanel.SetActive(false);
            EquipSales.gameObject.SetActive(true);
        });
        ButtonOnClickSoundEffects(OpenEquipButton, () =>
        {
            OpenWeaponPanel.SetActive(true);
            EquipSales.gameObject.SetActive(false);
            MainSceneSys.Single.UpdateOpenWeaponBag();
        });
      
    }

    public override void Clear()
    {
        ButtonReAllOnClick(EquipButton);
        ButtonReAllOnClick(OpenEquipButton);
        ButtonReAllOnClick(ReturnButton);
    }
}
