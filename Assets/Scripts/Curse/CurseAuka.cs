using UnityEngine;
using UnityEngine.UI;

public class CurseAuka : CurseMaster
{
    [SerializeField]
    GameObject block;
    Image image;


    protected override void BeginCurse()
    {
        var block = Instantiate(this.block, GameObject.Find("Canvas").transform);
        block.GetComponent<RectTransform>().SetAsFirstSibling();
        image = block.GetComponent<Image>();
    }

    protected override void ApplyEffect()
    {
        var color = image.color;
        color.a = (float)stacks / S;
        image.color = color;
    }

    protected override void EndCurse()
    {
        Destroy(image.gameObject);
    }
}
