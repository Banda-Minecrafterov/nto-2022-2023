using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class CurseTriangle : MonoBehaviour
{
    [HideInInspector]
    public CurseMaster master;

    [SerializeField]
    float minR;


    public void Teleport(Vector2 dir, float R)
    {
        transform.localPosition = dir * Random.Range(minR, R);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            master.Purification();
        }
    }
}
