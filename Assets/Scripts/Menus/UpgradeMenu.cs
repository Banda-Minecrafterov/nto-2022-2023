using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : BaseMenu
{
    [SerializeField]
    Button saveButton;
    [SerializeField]
    Transform upgradeButtons;

    Idol idol;

    static UpgradeMenu menu;


    void Awake()
    {
        menu = this;

        for (UpgradeData.Type i = 0; i < UpgradeData.Type.Count; i++)
        {
            UpdateButton(i, UpgradeManager.currentUpgrade[(int)i]);
        }

        gameObject.SetActive(false);
    }


    public void Open(Idol idol)
    {
        this.idol = idol;
        saveButton.interactable = idol.isSaveable;
    }


    public void OpenSaveMenu()
    {
        Settings(gameObject, PauseMenu.saveMenu.gameObject);
        PauseMenu.saveMenu.Open(idol);
    }


    public void Upgrade(Transform button)
    {
        UpgradeData.Type type = (UpgradeData.Type)button.GetSiblingIndex();

        UpdateButton(type, UpgradeManager.Upgrade(type));
        for (UpgradeData.Type i = 0; i < type; i++)
        {
            UpdateButtonInteractable(i);
        }
        for (UpgradeData.Type i = type + 1; i < UpgradeData.Type.Count; i++)
        {
            UpdateButtonInteractable(i);
        }
    }


    public static void UpdateButton(UpgradeData.Type type, UpgradeData data)
    {
        Transform button = menu.upgradeButtons.GetChild((int)type);

        button.GetComponent<Button>().interactable = UpgradeManager.sunEnergy >= data.sunEnergy;
        button.GetChild(1).GetComponent<TMP_Text>().text = data.sunEnergy.ToString();

        switch (type)
        {
            case UpgradeData.Type.Attack:
                float pureAttackPercantage = Player.player.pureAttackPercantage;
                float pureAttackPlus       = Player.player.pureAttackPlus;

                button.GetChild(2).GetComponent<TMP_Text>().text = Character.GetAttack(
                    1.0f, pureAttackPercantage,
                    0.0f, pureAttackPlus).
                    ToString();

                button.GetChild(4).GetComponent<TMP_Text>().text = Character.GetAttack(
                    data.buff.attackPercentage, pureAttackPercantage,
                    data.buff.attackPlus,       pureAttackPlus).
                    ToString();
                break;

            case UpgradeData.Type.Health:
                float pureMaxHealthPercantage = Player.player.pureMaxHealthPercantage;
                float pureMaxHealthPlus       = Player.player.pureMaxHealthPlus;

                button.GetChild(2).GetComponent<TMP_Text>().text = Character.GetMaxHealth(
                    1.0f, pureMaxHealthPercantage,
                    0.0f, pureMaxHealthPlus).
                    ToString();

                button.GetChild(4).GetComponent<TMP_Text>().text = Character.GetMaxHealth(
                    data.buff.maxHealthPercantage, pureMaxHealthPercantage,
                    data.buff.maxHealthPlus,       pureMaxHealthPlus).
                    ToString();
                break;

            //case UpgradeData.Type.Stamina:
            //    break;
        }
    }

    public static void UpdateButtonInteractable(UpgradeData.Type type)
    {
        menu.upgradeButtons.GetChild((int)type).GetComponent<Button>().interactable = UpgradeManager.sunEnergy >= UpgradeManager.currentUpgrade[(int)type].sunEnergy;
    }
}
