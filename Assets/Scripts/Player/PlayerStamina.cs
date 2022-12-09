using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField]
    float maxStamina = 100;
    [SerializeField]
    float speed = 5;

    public float currentStamina { get; private set; }

    [SerializeField]
    Bar stamina;


    void Awake()
    {
        currentStamina = maxStamina;
    }
    void Start()
    {
        stamina.SetMax(maxStamina);
    }

    void Update()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += speed * Time.deltaTime;
            stamina.SetValue(currentStamina);
        }
    }

    public bool DecreaseStamina(float decreaseStamina)
    {
        if (currentStamina < decreaseStamina)
            return false;

        currentStamina -= decreaseStamina;
        stamina.SetValue(currentStamina);
        return true;
    }
}
