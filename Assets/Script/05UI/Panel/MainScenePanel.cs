public class MainScenePanel : BasePanel
{

    public UnityEngine.UI.Button SetButton;

    public UnityEngine.UI.Button ShopButton;

    public UnityEngine.UI.Button BagButton;

    public UnityEngine.UI.Button SkillButton;

    public UnityEngine.UI.Button ReturnButton;

    public UnityEngine.UI.Button LevelButton;

    public UnityEngine.UI.ScrollRect MapList;

    public UnityEngine.Transform MapListContent;

    public virtual void InitGetCompoent()
    {
        UnityEngine.Transform fcmenu = transform.Find("FCMenu");
        UnityEngine.Transform fsetbutton = fcmenu.Find("FSetButton");
        SetButton = fsetbutton.GetComponent<UnityEngine.UI.Button>();
        UnityEngine.Transform fshopbutton = fcmenu.Find("FShopButton");
        ShopButton = fshopbutton.GetComponent<UnityEngine.UI.Button>();
        UnityEngine.Transform fequipbutton = fcmenu.Find("FEquipButton");
        BagButton = fequipbutton.GetComponent<UnityEngine.UI.Button>();
        UnityEngine.Transform fskillbutton = fcmenu.Find("FSkillButton");
        SkillButton = fskillbutton.GetComponent<UnityEngine.UI.Button>();
        UnityEngine.Transform freturnbutton = fcmenu.Find("FReturnButton");
        ReturnButton = freturnbutton.GetComponent<UnityEngine.UI.Button>();
        UnityEngine.Transform fmaplist = transform.Find("FMapList");
        MapList = fmaplist.GetComponent<UnityEngine.UI.ScrollRect>();
        MapListContent = MapList.content;
    }

    public override void Init()
    {
        base.Init();
        ButtonOnClickSoundEffects(SkillButton, () =>
        {
            MainSceneSys.Single.EnterSkillPanel();
        });
        ButtonOnClickSoundEffects(BagButton, () =>
        {
            MainSceneSys.Single.EnterBagPanel();
        });
        ButtonOnClickSoundEffects(ShopButton, () =>
        {
            MainSceneSys.Single.EnterShopPanel();
        });
        ButtonOnClickSoundEffects(LevelButton, () =>
        {
            MainSceneSys.Single.EnterLevelPanel();
        });
    }

    public override void Clear()
    {
        ButtonReAllOnClick(LevelButton);
        ButtonReAllOnClick(SkillButton);
        ButtonReAllOnClick(BagButton);
        ButtonReAllOnClick(ShopButton);
    }
}
