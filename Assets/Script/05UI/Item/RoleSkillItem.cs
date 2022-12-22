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
    //����ģ��

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
            return;//û��ƺü��ܵ���Ϊdata null����
        }
        if (cd.fillAmount != (data.currentCD / data.cd))
        {
            cd.fillAmount = (data.currentCD / data.cd);
        }
    }
}
