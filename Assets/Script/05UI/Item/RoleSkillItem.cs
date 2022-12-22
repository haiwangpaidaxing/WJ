using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleSkillItem : BasePanel
{
    Button button;
    public Image cd;
    public Image common;
    SkillData data;
    public int operaterID;
    //请勿模仿

    private void Awake()
    {
        button = common.GetComponent<Button>();
        button.onClick.AddListener(() => { InputController.Single.operaterCB?.Invoke(operaterID); });
    }
    public void CheckCD(SkillData skillData)
    {
        this.data = skillData;
    }
    private void Update()
    {
        if (data == null)
        {
            return;//没设计好技能导致为data null报错
        }
        if (cd.fillAmount != (data.currentCD / data.cd))
        {
            cd.fillAmount = (data.currentCD / data.cd);
        }
    }
}
