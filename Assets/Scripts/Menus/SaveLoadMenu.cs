using TMPro;
using UnityEngine;

public abstract class SaveLoadMenu : BaseMenu
{
    public int SaveSlotCount { get; private set; }

    [SerializeField]
    protected Transform buttons;

    protected Data[] datas;

    void Start()
    {
        SaveSlotCount = buttons.childCount;
        datas         = new Data[SaveSlotCount];

        for (int i = 0; i < SaveSlotCount; i++)
        {
            datas[i] = GetData(i);

            Transform button = buttons.GetChild(i);
            switch (datas[i].version)
            {
                case Data.ErrorOcured:
                    button.Find("Text").GetComponent<TextMeshProUGUI>().text = "Error";
                    goto case Data.FileNotFound;

                case Data.FileNotFound:
                    button.Find("Ver").gameObject.SetActive(false);
                    break;

                default:
                    InitButton(button, i);
                    break;
            }
        }
    }

    public void InitButton(Transform button, int id)
    {
        button.Find("Text").gameObject.SetActive(false);

        var Ver = button.Find("Ver");
        Ver.gameObject.SetActive(true);
        Ver.GetComponent<TextMeshProUGUI>().text = "Ver: " + datas[id].version;
    }


    public void Settings(GameObject settings)
    {
        Settings(gameObject, settings);
    }


    public abstract Data GetData(int i);
}
