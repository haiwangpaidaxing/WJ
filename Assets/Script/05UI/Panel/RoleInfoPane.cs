using UnityEngine;

public class RoleInfoPane : BasePanel
{

    public UnityEngine.UI.Image HP;

    public UnityEngine.UI.Image MP;

    public virtual void InitGetCompoent()
    {
        UnityEngine.Transform fchpmp = transform.Find("FCHPMP");
        UnityEngine.Transform fhp = fchpmp.Find("FHP");
        HP = fhp.GetComponent<UnityEngine.UI.Image>();
        UnityEngine.Transform fmp = fchpmp.Find("FMP");
        MP = fmp.GetComponent<UnityEngine.UI.Image>();
    }

    public override void Init()
    {
        base.Init();
    }
    public void SetHP(float value,int MaxHP)
    {
        HP.fillAmount = (value / MaxHP);
    }

    public void SetMP(float value,int MaxMP)
    {
       MP.fillAmount = value / MaxMP;
    }
    public override void Clear()
    {
    }
}
