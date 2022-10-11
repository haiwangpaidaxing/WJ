using UnityEngine;
using UnityEngine.UI;
using WMEffectsSkill;

public class SkillPanel : BasePanel
{

    public UnityEngine.UI.Button ReturnButton;

    public UnityEngine.UI.ScrollRect SkillList;

    public UnityEngine.Transform SkillListContent;

    public Transform selectSkillEffectsContent;

    public Transform itemConenter;

    public Slider skillEffectsSetValue;
    public Text skillEffectsSetValueText;
    public virtual void InitGetCompoent()
    {
        UnityEngine.Transform freturnbutton = transform.Find("FReturnButton");
        ReturnButton = freturnbutton.GetComponent<UnityEngine.UI.Button>();
        UnityEngine.Transform fskilllist = transform.Find("FSkillList");
        SkillList = fskilllist.GetComponent<UnityEngine.UI.ScrollRect>();
        SkillListContent = SkillList.content;
    }

    public override void Init()
    {
        base.Init();
        EffectsSkillDataConfig effectsSkillDataConfig = resourceSvc.EffectsSkillDataConfig;
        ButtonOnClickSoundEffects(ReturnButton, () =>
        {
            UISvc.Single.SetPanelState(this, UISvc.StateType.Close);
        });
        for (int i = 0; i < effectsSkillDataConfig.effectsSkillDataList.Count; i++)
        {
            //  Debug.Log(Resources.Load<GameObject>(UIItemPath.SkillEffectsItem));
            SkillEffectsItem skillEffectsItem = resourceSvc.LoadOrCreate<GameObject>(UIItemPath.SkillEffectsItem).GetComponent<SkillEffectsItem>();
            //skillEffectsItem.skillEffectsNmaeText.text = effectsSkillDataConfig.effectsSkillDataList[i].name;
            TransformChildAdd(selectSkillEffectsContent, skillEffectsItem.transform);
            skillEffectsItem.Init(itemConenter, effectsSkillDataConfig.effectsSkillDataList[i]);
        }
        skillEffectsSetValue.onValueChanged.AddListener((value) => { skillEffectsSetValueText.text = value.ToString(); });


    }

    public override void Clear()
    {
       TransformChildReset(SkillListContent);
        TransformChildReset(selectSkillEffectsContent);
        ButtonReAllOnClick(ReturnButton);
    }
}
