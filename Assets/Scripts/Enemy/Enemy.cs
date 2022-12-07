using System.Collections;
using System.IO;
using UnityEngine;


[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : Character, ISaveLoadData
{
    EnemyMovement path;

    Coroutine startAttacking;


    new void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.Enemy0 + transform.GetSiblingIndex(), this);

        path = GetComponent<EnemyMovement>();

        base.Awake();
    }


    public void StartAttacking()
    {
        startAttacking = StartCoroutine(Attack());
    }

    public bool StopAttacking()
    {
        try
        {
            StopCoroutine(startAttacking);
        }
        catch { return false; }
        return true;
    }


    IEnumerator Attack()
    {
        while (true)
        {
            path.enabled = false;
            yield return StartCoroutine(attack[0].Attack());
            path.enabled = true;
            yield return null;
        }
    }


    public void Save(ref BinaryWriter data)
    {
        data.Write(!gameObject.activeSelf);
    }

    public void Load(ref BinaryReader data, int version)
    {
        if (data.ReadBoolean())
        {
            gameObject.SetActive(false);
        }
    }
}
