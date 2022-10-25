using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelRoomPanel : BasePanel
{
    private float a;
    public Text infoText;
    public void Show(string info)
    {
        a = 1;
        infoText.text = info;
        infoText.color = Color.white;
    }
    private void Update()
    {
        a = Mathf.Lerp(a, 0, Time.deltaTime * 2);
        infoText.color = new Color(1, 1, 1,a);    
    }
}



