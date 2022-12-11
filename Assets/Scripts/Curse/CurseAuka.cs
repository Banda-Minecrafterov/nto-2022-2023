using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class CurseAuka : CurseMaster
{
    [SerializeField]
    GameObject block;
    [SerializeField]
    Transform canvas;
    Image image;

    AudioSource audio;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    float maxAlpha;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    float maxVolume;

    float origVolume;


    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }


    protected override void BeginCurse()
    {
        image = Instantiate(block, canvas).GetComponent<Image>();
        origVolume = AudioManager.background.volume;
        audio.Play();
    }

    protected override void ApplyEffect()
    {
        float normalizedStacks = (float)stacks / S;

        var color = image.color;
        color.a  = maxAlpha * normalizedStacks;
        image.color = color;

        AudioManager.background.volume = origVolume - 2.0f * origVolume * normalizedStacks;
        audio.volume = maxVolume * normalizedStacks;
    }

    protected override void EndCurse()
    {
        Destroy(image.gameObject);
        audio.volume = 0.0f;
        audio.Stop();
    }
}
