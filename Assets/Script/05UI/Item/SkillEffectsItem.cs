using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WMEffectsSkill;

public class SkillEffectsItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    Text skillEffectsNmaeText;
    [SerializeField]
    Transform showParent;
    [SerializeField]
    Transform startParent;
    [SerializeField]
     public  EffectsSkillData effectsSkillData;
    public EffectsSkillData EffectsSkillData
    {
        get
        {
            return effectsSkillData;
        }
    }
    public Transform clone;
    public void Init(Transform trParent, EffectsSkillData effectsSkillData)
    {
        skillEffectsNmaeText = GetComponentInChildren<Text>();
        this.effectsSkillData = effectsSkillData;
        skillEffectsNmaeText.text = effectsSkillData.name;
        startParent = transform.parent;
        this.showParent = trParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        clone.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        clone.SetParent(transform, false);
        clone.localPosition = Vector3.zero;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        clone.SetParent(showParent);

    }
}
