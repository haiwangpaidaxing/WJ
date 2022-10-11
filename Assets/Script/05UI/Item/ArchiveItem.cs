
using UnityEngine;
using UnityEngine.UI;

public class ArchiveItem : MonoBehaviour
{

    public UnityEngine.UI.Text ArchiveNameText;

    public UnityEngine.UI.Text ArchiveInfoText;
    public Button currentButton;
    public void Init(ArchiveData archiveData)
    {
        currentButton = GetComponent<Button>();
        currentButton.onClick.AddListener(() =>
        {
            LogicSys.Single.EntGame(archiveData.archiveID);
        });
        ArchiveNameText.text = "�浵" + archiveData.archiveID;
        ArchiveInfoText.text = "��󱣴�ʱ��" + archiveData.saveDataTime;
    }
    public virtual void InitGetCompoent()
    {
        UnityEngine.Transform farchivenametext = transform.Find("FArchiveNameText");
        ArchiveNameText = farchivenametext.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.Transform farchiveinfotext = transform.Find("FArchiveInfoText");
        ArchiveInfoText = farchiveinfotext.GetComponent<UnityEngine.UI.Text>();
    }


}
