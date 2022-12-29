public class ResourceLoadiProgressPanel : BasePanel {
    
    public UnityEngine.UI.Slider ProgressBar;
    
    public UnityEngine.UI.Text LoadProgressText;
    
    public UnityEngine.UI.Text fileSizeText;

    public UnityEngine.UI.Text debugText;
    public virtual void InitGetCompoent() {
        UnityEngine.Transform fprogressbar = transform.Find("FProgressBar");
        ProgressBar = fprogressbar.GetComponent<UnityEngine.UI.Slider>();
        UnityEngine.Transform floadprogresstext = transform.Find("FLoadProgressText");
        LoadProgressText = floadprogresstext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform ffilesizetext = transform.Find("FFileSizeText");
        fileSizeText = ffilesizetext.GetComponent<UnityEngine.UI.Text>();
    }

    public void Debug(string debug)
    {
        debugText.text = debug;       
    }
    public override void Init() {
    }
    
    public override void Clear() {
    }
}
