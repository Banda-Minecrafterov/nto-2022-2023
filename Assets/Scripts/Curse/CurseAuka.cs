using UnityEngine;
using UnityEngine.UI;

public class CurseAuka : CurseMaster
{
    [SerializeField]
    GameObject prefab;
    Image image;


    protected override void ApplyEffect()
    {
        Debug.Log("Effect applyed: " + stacks);
        if (stacks == 0)
        {
            if (isIncreasing)
            {
                var block = Instantiate(prefab, GameObject.Find("Canvas").transform);
                block.GetComponent<RectTransform>().SetAsFirstSibling();
                image = block.GetComponent<Image>();
            }
            else
                Destroy(image.gameObject);
        }

        var color = image.color;
        color.a = (float)stacks / S;
        image.color = color;
    }


}
