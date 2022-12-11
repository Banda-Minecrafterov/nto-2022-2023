using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Idol))]
public class IdolActivate : Interactable
{
    public static List<AudioSource> audioSources { get; private set; } = new List<AudioSource>();

    [SerializeField]
    private AudioSource activate;


    new void Awake()
    {
        audioSources.Add(activate);

        base.Awake();
    }


    protected override void Interact()
    {
        activate.Play();
    }


    public void Activated()
    {
        ShowTip(false);
        Idol idol = GetComponent<Idol>();
        idol.enabled = true;
        idol.Activate();
        audioSources.Remove(activate);
        Destroy(this);
    }
}
