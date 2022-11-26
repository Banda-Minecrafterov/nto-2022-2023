using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SavePoint : Interactable
{
    protected override void Interact()
    {
        PauseMenu.SaveMenu();
    }
}
