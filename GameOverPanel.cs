public class GameOverPanel : BasePanel {
    
    public UnityEngine.UI.Text Title;
    
    public UnityEngine.UI.Text KillMonsterText;
    
    public UnityEngine.UI.Text KillMonsterCountText;
    
    public UnityEngine.UI.Text GoldText;
    
    public UnityEngine.UI.Text GoldCountText;
    
    public UnityEngine.UI.Button ReturnBtn;
    
    public virtual void InitGetCompoent() {
        UnityEngine.Transform ftitle = transform.Find("FTitle");
        Title = ftitle.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform fckillimage = transform.Find("FCkillImage");
        UnityEngine.Transform killmonstertext = fckillimage.Find("KillMonsterText");
        KillMonsterText = killmonstertext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform fkillmonstercounttext = fckillimage.Find("FKillMonsterCountText");
        KillMonsterCountText = fkillmonstercounttext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform fcgoldimage = transform.Find("FCGoldImage");
        UnityEngine.Transform goldtext = fcgoldimage.Find("GoldText");
        GoldText = goldtext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform fgoldcounttext = fcgoldimage.Find("FGoldCountText");
        GoldCountText = fgoldcounttext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform freturnbtn = transform.Find("FReturnBtn");
        ReturnBtn = freturnbtn.GetComponent<UnityEngine.UI.Button>();
    }
    
    public override void Init() {
    }
    
    public override void Clear() {
    }
}
