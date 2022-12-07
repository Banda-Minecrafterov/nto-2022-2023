using UnityEngine;


[RequireComponent(typeof(CapsuleCollider2D))]
public class SavePoint : Interactable
{
    protected override void Interact()
    {
        PauseMenu.SaveMenu();
    }
}
