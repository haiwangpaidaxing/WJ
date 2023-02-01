using UnityEngine;
using UnityEngine.UI;

public class SelectLevelItem : BasePanelCancelDrag
{

    public UnityEngine.UI.Text LevelNameText;

    public UnityEngine.UI.Button StartGameButton;

    public UnityEngine.UI.Image image;
    public virtual void InitGetCompoent()
    {
        UnityEngine.Transform flevelnametext = transform.Find("FLevelNameText");
        LevelNameText = flevelnametext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform findstartgamebutton = transform.Find("FindStartGameButton");
        StartGameButton = findstartgamebutton.GetComponent<UnityEngine.UI.Button>();
    }

    public override void Init()
    {
    }
    public void Init(string levelNameText, string jumpName)
    {
        base.Init();
        image.sprite = ResourceSvc.Single.Load<Sprite>(LevelImagePath.Level + "/" + jumpName);
        SetText(LevelNameText, levelNameText);

        ButtonOnClickSoundEffects(StartGameButton, () =>
        {
            UISvc.Single.CloseAll();
            resourceSvc.JumpScene(jumpName);
        });
    }
    public override void Clear()
    {
    }

    private void OnDestroy()
    {
        ButtonReAllOnClick(StartGameButton);
    }
}
