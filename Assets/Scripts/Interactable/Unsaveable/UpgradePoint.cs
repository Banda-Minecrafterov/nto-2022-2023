using UnityEngine;


[RequireComponent(typeof(CapsuleCollider2D))]
public class UpgradePoint : Interactable
{
    protected override void Interact()
    {
        PauseMenu.UpgradeMenu();
    }
}
