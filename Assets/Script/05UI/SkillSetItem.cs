using UnityEngine;
using UnityEngine.UI;
public class SkillSetItem : BasePanel {
    
    public UnityEngine.UI.Text SkillName;
    public Transform conetents;
    public virtual void InitGetCompoent() {
        UnityEngine.Transform fskillname = transform.Find("FSkillName");
        SkillName = fskillname.GetComponent<UnityEngine.UI.Text>();
    }
    [SerializeField]
    int skillId;
    public void InitSkillID(int id)
    {
        this.skillId = id;
    }
    public override void Init() {
    }
    
    public override void Clear() {
    }
}
