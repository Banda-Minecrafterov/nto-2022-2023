using System.Collections;
using System.IO;
using UnityEngine;


[RequireComponent(typeof(PlayerMovement))]
public class Player : Character, ISaveLoadData
{
    PlayerMovement movement;

    public static Player player { get; private set; }


    new void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.Player, this);

        player = this;

        movement = GetComponent<PlayerMovement>();

        base.Awake();
    }


    void OnEnable()
    {
        StartCoroutine(Attack());
    }


    IEnumerator Attack()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                movement.enabled = false;
                yield return StartCoroutine(attack[0].Attack());
                movement.enabled = true;
            }
            yield return null;
        }
    }


    public void Save(ref BinaryWriter data)
    {
        Vector2 pos = transform.position;
        data.Write(pos.x);
        data.Write(pos.y);
    }

    public void Load(ref BinaryReader data, int version)
    {
        transform.position = new Vector3(data.ReadSingle(), data.ReadSingle(), 0.0f);
    }
}

