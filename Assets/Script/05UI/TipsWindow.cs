using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsWindow : MonoBehaviour
{
    public Image bg;
    public Text text;
    public Animator anim;
    Queue<string> m_AllHint;
    bool isInPlay;//是否正在提示中
    public  void Init()
    {
        if (m_AllHint == null)
        {
            m_AllHint = new Queue<string>();
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        if (m_AllHint != null && m_AllHint.Count > 0 && !isInPlay)
        {
            Hint(m_AllHint.Dequeue());
        }
    }

    public  void AddHint(string hint)
    {
        if (m_AllHint==null)
        {
            m_AllHint = new Queue<string>();
        }
        m_AllHint.Enqueue(hint);
    }
    private void Hint(string hint)
    {
        bg.gameObject.SetActive(true);
        isInPlay = true;
        bg.GetComponent<RectTransform>().sizeDelta = new Vector2(35 * hint.Length + 100, 80);
        text.text = hint;
        anim.Play("TipsWindow", 0, 0);
    }

    public void AimOver()
    {
        //动画结束时候调用，帧事件
        isInPlay = false;
        bg.gameObject.SetActive(false);
    }

    protected  void OnReset()
    {
        isInPlay = false;
        m_AllHint.Clear();
        m_AllHint = null;
        text.text = "";
    }
}
