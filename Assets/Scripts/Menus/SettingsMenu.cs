using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    public void Back(GameObject menu)
    {
        Settings(gameObject, menu);
    }
}
