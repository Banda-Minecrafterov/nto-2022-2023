using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastsManager : MonoBehaviour
{
    public static void AddEnemy(Enemy enemy)
    {
        switch (enemy)
        {
            case Vurdalak:
                BeastsMenu.AddEnemy(0);
                break;
        }
    }
}
