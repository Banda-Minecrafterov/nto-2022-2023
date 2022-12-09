using Cinemachine;
using System.Collections;
using System.IO;
using UnityEngine;


[RequireComponent(typeof(PlayerStamina))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : Character, ISaveLoadData
{
    public PlayerMovement movement { get; private set; }
    public PlayerStamina  stamina  { get; private set; }

    Coroutine attackCoroutine;

    [SerializeField]
    CinemachineVirtualCamera cameraFollow;

    public static Player player { get; private set; }


    new void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.Player, this);

        player = this;

        movement = GetComponent<PlayerMovement>();
        stamina  = GetComponent<PlayerStamina>();

        base.Awake();

#if DEBUG
        SaveLoadManager.LoadFiles();
#endif
    }


    void OnEnable()
    {
        movement.enabled = true;
        if (currentAttack == null)
        {
            attackCoroutine = StartCoroutine(AttackCheck());
        }
    }

    void OnDisable()
    {
        movement.enabled = false;
        if (currentAttack == null)
        {
            StopCoroutine(attackCoroutine);
        }
    }


    IEnumerator AttackCheck()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                movement.enabled = false;
                StopCoroutine(attackCoroutine);

                StartAttack(0);
            }
            yield return null;
        }
    }


    public override void StopAttack()
    {
        movement.enabled = true;
        attackCoroutine = StartCoroutine(AttackCheck());

        base.StopAttack();
    }


    public void Save(BinaryWriter data)
    {
        Vector3 pos = transform.position;
        data.Write(pos.x);
        data.Write(pos.y);
    }

    public void Load(BinaryReader data, int version)
    {
        Vector3 position = new Vector3(data.ReadSingle(), data.ReadSingle(), 0.0f);

        transform.position = position;
        cameraFollow.ForceCameraPosition(position + cameraFollow.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, Quaternion.identity);
    }
}

