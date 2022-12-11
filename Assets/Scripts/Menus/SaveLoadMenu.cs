using TMPro;
using UnityEngine;

public abstract class SaveLoadMenu : BaseMenu
{
    [SerializeField]
    protected Transform buttons;


    void Awake()
    {
        for (int i = 0; i < SaveLoadManager.saveSlotsCount; i++)
        {
            Transform button = buttons.GetChild(i);
            switch (SaveLoadManager.saveLoadDatas[i].version)
            {
                case SaveLoadData.FileNotFound:
                    //button.Find("Ver").gameObject.SetActive(false);
                    break;

                case SaveLoadData.ErrorOcured:
                    //button.Find("Text").GetComponent<TextMeshProUGUI>().text = "Error";
                    goto case SaveLoadData.FileNotFound;

                default:
                    InitButton(button, i);
                    break;
            }
        }
    }

    public void InitButton(Transform button, int id)
    {
        //button.Find("Text").gameObject.SetActive(false);

        //var Ver = button.Find("Ver");
        //Ver.gameObject.SetActive(true);
        //Ver.GetComponent<TextMeshProUGUI>().text = "Ver: " + SaveLoadManager.saveLoadDatas[id].version;
    }


    public void Settings(GameObject settings)
    {
        Settings(gameObject, settings);
    }
}
