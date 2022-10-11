using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillEffectsSetItem : BasePanel, IDropHandler
{
    [SerializeField]
    Button changeEffectsSkill;
    [SerializeField]
    public Text skillEffectsNameText;
    private Action<WMEffectsSkill.EffectsSkillData> cb;
    [SerializeField]
    WMEffectsSkill.EffectsSkillData effectsSkillData;
    [SerializeField, Header("技能ID")]
    int skillID;
    [SerializeField, Header("技能效果下标")]
    int skillEffectsIndex;
    [SerializeField]
    SkillPanel skillPanel;

    public Transform introductionSkillEffectsPanel;
    public void Init(Action<WMEffectsSkill.EffectsSkillData> cb, int skillEffectsIndex, int skillID, SkillPanel skillPanel)
    {
        this.skillPanel = skillPanel;
        this.skillEffectsIndex = skillEffectsIndex;
        this.skillID = skillID;
        skillEffectsNameText = GetComponentInChildren<Text>();
        changeEffectsSkill = GetComponent<Button>();
        this.cb = cb;
        changeEffectsSkill.onClick.AddListener(() =>
        {

        });
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag.name);
        //  effectsSkillData.value = skillPanel.skillEffectsSetValue.value;
        SkillEffectsItem skillEffectsItem = eventData.pointerDrag.gameObject.GetComponent<SkillEffectsItem>();
        if (skillEffectsItem == null)
        {
            return;
        }

       // WMEffectsSkill.EffectsSkillData effectsSkillData = new WMEffectsSkill.EffectsSkillData();

        skillEffectsItem.effectsSkillData.value = skillPanel.skillEffectsSetValue.value;
        skillEffectsItem.effectsSkillData.skillEffectsIndex = this.skillEffectsIndex;
        skillEffectsItem.effectsSkillData.skillID = this.skillID;
        cb?.Invoke(skillEffectsItem.EffectsSkillData);
        skillEffectsNameText.text = skillEffectsItem.EffectsSkillData.name;

    }
}
