using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

[RequireComponent(typeof(Character))]
public class CharacterStamina : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    public float currentStamina { get; private set; }

    Character character;

    [SerializeField]
    Bar staminaBar;


    void Awake()
    {
        character = GetComponent<Character>();
        currentStamina = character.maxStamina;
    }

    void Start()
    {
        Init();
    }


    protected void Init()
    {
        staminaBar.SetMax(character.maxHealth);
    }


    void Update()
    {
        if (currentStamina < character.maxStamina)
        {
            currentStamina += speed * Time.deltaTime;
            staminaBar.SetValue(currentStamina);

            if (currentStamina > character.maxStamina)
                currentStamina = character.maxStamina;
        }
    }


    public bool DecreaseStamina(float decreaseStamina)
    {
        if (currentStamina < decreaseStamina)
            return false;

        currentStamina -= decreaseStamina;
        staminaBar.SetValue(currentStamina);
        return true;
    }
}
