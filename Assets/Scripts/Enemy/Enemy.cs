using System.Collections;
using System.IO;
using UnityEngine;


[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : Character, ISaveLoadData
{
    EnemyMovement movement;

    [SerializeField]
    int sunEnergyLocal;

    public int sunEnergy { get => sunEnergyLocal; }


    new void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.Enemy0 + transform.GetSiblingIndex(), this);

        movement = GetComponent<EnemyMovement>();

        base.Awake();
    }


    public void StartAttackAnimation()
    {
        base.StartAttackAnimation(0);
        movement.enabled = false;
    }

    public override void StopAttackAnimation()
    {
        base.StopAttackAnimation();
        movement.enabled = true;
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
