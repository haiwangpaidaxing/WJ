using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 登录面板
/// </summary>
public class LogicPanel : BasePanel
{
    public Button singleGameButton;
    public Button netGameButton;
    public Button quitButton;
    public override void Init()
    {
        base.Init();
        ButtonOnClickSoundEffects(singleGameButton, () =>
        {
            LogicSys.Single.EnterSingleGame();
        });

        ButtonOnClickSoundEffects(netGameButton, () =>
        {
            LogicSys.Single.EnterLogicPanel();           
        });

        ButtonOnClick(quitButton, () =>
        {
            //TODO播放音效
            Application.Quit();
        });
    }


    public override void Clear()
    {
        base.Clear();
        ButtonReAllOnClick(singleGameButton);
        ButtonReAllOnClick(netGameButton);
        ButtonReAllOnClick(quitButton);
    }
}
