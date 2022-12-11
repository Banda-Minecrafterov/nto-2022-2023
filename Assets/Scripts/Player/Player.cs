using Cinemachine;
using System.Collections;
using System.IO;
using UnityEngine;


[RequireComponent(typeof(CharacterStamina))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : Character, ISaveLoadData
{
    public PlayerMovement movement { get; private set; }
    public CharacterStamina stamina  { get; private set; }

    Coroutine attackCoroutine;

    [SerializeField]
    CinemachineVirtualCamera cameraFollowLocal;

    public static bool isAtackable { get; private set; } = true;
    public static CinemachineVirtualCamera cameraFollow { get => player.cameraFollowLocal; }

    public static Player player { get; private set; }


    new void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.Player, this);

        player = this;

        movement = GetComponent<PlayerMovement>();
        stamina  = GetComponent<CharacterStamina>();

        base.Awake();

#if DEBUG
        SaveLoadManager.LoadFiles();
#endif
    }


    void OnEnable()
    {
        isAtackable = true;

        movement.enabled = true;
        if (currentAttack == null)
        {
            attackCoroutine = StartCoroutine(AttackCheck());
        }
    }

    void OnDisable()
    {
        isAtackable = false;

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

                StartAttackAnimation(0);
            }
            yield return null;
        }
    }


    public override void StopAttackAnimation()
    {
        movement.enabled = true;
        attackCoroutine = StartCoroutine(AttackCheck());

        base.StopAttackAnimation();
    }


    public void Dead()
    {
        PauseMenu.LoadMenu();
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
        cameraFollowLocal.ForceCameraPosition(position + cameraFollow.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset, Quaternion.identity);
    }
}

