public class SelectLevelPanel : BasePanel
{

    public UnityEngine.UI.ScrollRect ScrollView;

    public UnityEngine.Transform ScrollViewContent;

    public UnityEngine.UI.Button returnButton;
    public virtual void InitGetCompoent()
    {
        UnityEngine.Transform fscrollview = transform.Find("FScroll View");
        ScrollView = fscrollview.GetComponent<UnityEngine.UI.ScrollRect>();
        ScrollViewContent = ScrollView.content;
    }

    public override void Init()
    {
        base.Init();
        ButtonOnClickSoundEffects(returnButton, () =>
        {
            UISvc.Single.SetPanelState(this,UISvc.StateType.Close);
        });
    }

  
    public override void Clear()
    {
        ButtonReAllOnClick(returnButton);
    }
}
