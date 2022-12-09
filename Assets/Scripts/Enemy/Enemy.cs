using System.Collections;
using System.IO;
using UnityEngine;


[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : Character, ISaveLoadData
{
    EnemyMovement path;


    new void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.Enemy0 + transform.GetSiblingIndex(), this);

        path = GetComponent<EnemyMovement>();

        base.Awake();
    }


    public void StartAttack()
    {
        path.enabled = false;
        base.StartAttack(0);
    }

    public override void StopAttack()
    {
        path.enabled = true;
        base.StopAttack();
    }


    public void Save(BinaryWriter data)
    {
        data.Write(!gameObject.activeSelf);
    }

    public void Load(BinaryReader data, int version)
    {
        if (data.ReadBoolean())
        {
            gameObject.SetActive(false);
        }
    }
}
