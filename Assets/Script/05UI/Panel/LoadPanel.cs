using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : BasePanel
{
    public Text progressText;
    public void UpdatProgress(float i)
    {
        progressText.text = i + "%";
    }
}
